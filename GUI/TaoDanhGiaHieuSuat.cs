using BLL;
using ClosedXML.Excel;
using DAL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class TaoDanhGiaHieuSuat : UserControl
    {
        private Guna2TextBox txtSearch;
        private NumericUpDown nudThang, nudNam;
        private Guna2Button btnReload, btnSaveAll, btnExport;
        private Guna2DataGridView dgv;
        private string connectionString;
        private string idNguoiDanhGia;
        private int? idPhongBanCuaTP = null;
        private BLLDanhGiaNhanVien bllDanhGia;
        private BLLNhanVien bllNhanVien;
        private DALNhanVien dalNhanVien;
        private bool isHandling = false;

        public TaoDanhGiaHieuSuat(string idNguoiDanhGia, string conn)
        {
            this.idNguoiDanhGia = idNguoiDanhGia;
            this.connectionString = conn;

            // KHÔNG gọi InitializeComponent() ở đây nữa
            // InitializeComponent();  <-- XÓA DÒNG NÀY HOẶC COMMENT LẠI

            // Khởi tạo BLL/DAL trước
            bllDanhGia = new BLLDanhGiaNhanVien(conn);
            bllNhanVien = new BLLNhanVien(conn);
            dalNhanVien = new DALNhanVien(conn);

            // Lấy phòng ban của TP
            if (!idNguoiDanhGia.StartsWith("GD") && idNguoiDanhGia.StartsWith("TP"))
            {
                idPhongBanCuaTP = dalNhanVien.LayIDPhongBanTheoNhanVien(idNguoiDanhGia); // sửa tên DAL nếu sai chính tả
            }

            // BÂY GIỜ MỚI ĐƯỢC GỌI
            InitializeComponent(); // <-- ĐẶT LẠI Ở ĐÂY

            // Sau khi InitializeComponent() xong → tất cả control đã được tạo
            BuildUI(); // vẫn giữ BuildUI như cũ

            // Cuối cùng mới load dữ liệu (lúc này dgv đã tồn tại)
            this.Load += (s, e) => LoadDanhGia(); // Dùng event Load của UserControl để load dữ liệu
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // ===== TITLE ĐẸP =====
            var lblTitle = new Label
            {
                Text = "ĐÁNH GIÁ HIỆU SUẤT NHÂN VIÊN",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 138),
                Dock = DockStyle.Top,
                Height = 100,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White
            };

            // ===== PANEL TÌM KIẾM ĐẸP (Tăng chiều cao, chỉnh layout để thẳng hàng) =====
            var searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height =100,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(20, 18, 20, 18), // Tăng padding để cân bằng
                Margin = new Padding(0),
                BorderStyle = BorderStyle.None
            };
            // Bo tròn border
            flow.Paint += (s, e) =>
            {
                var rect = flow.ClientRectangle;
                rect.Inflate(-1, -1);
                using (var pen = new Pen(Color.FromArgb(220, 220, 220), 2))
                    e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            };

            // Icon tìm kiếm (tăng kích thước, căn giữa)
            var lblSearchIcon = new Label
            {
                Text = "🔍",
                Font = new Font("Segoe UI Emoji", 20F),
                AutoSize = true,
                ForeColor = Color.Gray,
                Margin = new Padding(10, 15, 0, 0) // Căn cao hơn
            };

            txtSearch = new Guna2TextBox
            {
                Width = 280, // Tăng rộng để không lệch
                Height = 40,
                PlaceholderText = " Nhập tên nhân viên hoặc nhận xét để tìm kiếm...",
                BorderRadius = 25,
                BorderColor = Color.FromArgb(200, 200, 200),
                Font = new Font("Segoe UI", 12F),
                Animated = true,
                Padding = new Padding(10),
                //Margin = new Padding(0, 0, 20, 0) // Thêm margin phải để cân bằng
            };
            txtSearch.TextChanged += (s, e) => Filter();

            // Nhóm Tháng + Năm (tăng rộng, font to, căn giữa)
            var pnlThoiGian = new Panel
            {
                Width = 400, // Tăng rộng để hiển thị đầy đủ
                Height = 40,
                BackColor = Color.White,
                Margin = new Padding(20, 0, 20, 0) // Cân bằng khoảng cách
            };
            var flowTime = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15, 8, 15, 8)
            };

            nudThang = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 12,
                Value = DateTime.Now.AddMonths(-1).Month,
                Width = 90, // Tăng rộng
                Height = 50,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Center
            };
            nudThang.ValueChanged += (s, e) => LoadDanhGia();

            nudNam = new NumericUpDown
            {
                Minimum = 2010,
                Maximum = 2100,
                Value = DateTime.Now.Year,
                Width = 120, // Tăng rộng để hiển thị năm đầy đủ
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Center,
                Margin = new Padding(20, 0, 0, 0)
            };
            nudNam.ValueChanged += (s, e) => LoadDanhGia();

            flowTime.Controls.Add(new Label { Text = "Tháng", AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Gray, Margin = new Padding(0, 12, 8, 0) });
            flowTime.Controls.Add(nudThang);
            flowTime.Controls.Add(new Label { Text = "Năm", AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.Gray, Margin = new Padding(20, 12, 8, 0) });
            flowTime.Controls.Add(nudNam);
            pnlThoiGian.Controls.Add(flowTime);

            // Nút bấm
            btnReload = CreateStyledButton(" Tải lại", Color.FromArgb(0, 123, 255), "🔄");
            btnSaveAll = CreateStyledButton(" Lưu tất cả", Color.FromArgb(40, 167, 69), "💾");
            btnExport = CreateStyledButton(" Xuất Excel", Color.FromArgb(255, 193, 7), "📊");

            btnReload.Click += (s, e) => LoadDanhGia();
            btnSaveAll.Click += (s, e) => SaveAll();
            btnExport.Click += (s, e) => ExportExcel();

            // Thêm vào flow (thêm margin để không sát nhau)
            flow.Controls.Add(lblSearchIcon);
            flow.Controls.Add(txtSearch);
            flow.Controls.Add(pnlThoiGian);
            flow.Controls.Add(btnReload);
            flow.Controls.Add(btnSaveAll);
            flow.Controls.Add(btnExport);

            searchPanel.Controls.Add(flow);

            // ===== DATAGRIDVIEW ĐẸP HƠN (xen kẽ trắng nhạt, header đậm, checkbox bo tròn) =====
            dgv = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                RowTemplate = { Height = 60 }, // Tăng cao rows
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 55,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(25, 50, 120), // Header đậm hơn
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Padding = new Padding(15, 0, 15, 0)
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 11F),
                    SelectionBackColor = Color.FromArgb(0, 123, 255),
                    SelectionForeColor = Color.White,
                    Padding = new Padding(15, 5, 15, 5) // Thêm padding cells
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(245, 247, 250) // Xen kẽ nhạt đẹp
                }
            };

            dgv.CellValueChanged += Dgv_CellValueChanged;
            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty) dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            

            // LAYOUT CHÍNH (tăng chiều cao searchPanel)
            var mainLayout = new TableLayoutPanel { Dock = DockStyle.Fill };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(searchPanel, 0, 1);
            mainLayout.Controls.Add(dgv, 0, 2);

            this.Controls.Add(mainLayout);
        }


        // Hàm tạo nút đẹp
        private Guna2Button CreateStyledButton(string text, Color fillColor, string emoji)
        {
            var btn = new Guna2Button
            {
                Text = emoji + text,
                FillColor = fillColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BorderRadius = 24,
                Width = 160,
                Height = 48,
                Margin = new Padding(12, 0, 0, 0),
                Animated = true
            };
            btn.MouseEnter += (s, e) => btn.FillColor = ControlPaint.Light(fillColor, 0.2f);
            btn.MouseLeave += (s, e) => btn.FillColor = fillColor;
            return btn;
        }

        private void LoadDanhGia()
        {
            int thang = (int)nudThang.Value;
            int nam = (int)nudNam.Value;

            // Dùng method bạn đã có trong code cũ
            DataTable dt = bllDanhGia.GetAllPB(idNguoiDanhGia, thang, nam, txtSearch.Text, idPhongBanCuaTP, null);

            if (dt == null || dt.Rows.Count == 0)
            {
                dgv.DataSource = null;
                return;
            }

            var display = new DataTable();
            display.Columns.Add("STT", typeof(int));
            display.Columns.Add("IDNhanVien", typeof(string));
            display.Columns.Add("TenNhanVien", typeof(string));
            if (idNguoiDanhGia.StartsWith("GD"))
            {
                display.Columns.Add("PhongBan", typeof(string));
                display.Columns.Add("ChucVu", typeof(string));
            }
            display.Columns.Add("DiemCC", typeof(int));
            display.Columns.Add("NL_Te", typeof(bool));
            display.Columns.Add("NL_TB", typeof(bool));
            display.Columns.Add("NL_Tot", typeof(bool));
            display.Columns.Add("DiemNL", typeof(int));
            display.Columns.Add("TongDiem", typeof(int));
            display.Columns.Add("XepLoai", typeof(string));
            display.Columns.Add("NhanXet", typeof(string));

            // Lấy đánh giá mới nhất của từng nhân viên trong tháng
            var unique = dt.AsEnumerable()
                .GroupBy(r => r["IDNhanVien"])
                .Select(g => g.OrderByDescending(x => x.Field<DateTime?>("NgayTao") ?? DateTime.MinValue).First());

            int stt = 1;
            foreach (DataRow r in unique)
            {
                string idNV = r["IDNhanVien"].ToString();
                int misses = Convert.ToInt32(r["Misses"] ?? 0);
                int diemCC = Math.Max(0, 5 - misses * 2);

                int? diemNL_old = r["DiemNangLucStored"] as int?;
                int diemNL = diemNL_old ?? 5;
                bool te = diemNL == 1;
                bool tb = diemNL == 2;
                bool tot = diemNL == 5 || diemNL_old == null;

                int tong = diemCC + diemNL;
                string xepLoai = tong <= 6 ? "Tệ" : (tong <= 8 ? "Trung bình" : "Tốt");
                string nhanXet = r["NhanXet"]?.ToString() ?? "";
                if (string.IsNullOrWhiteSpace(nhanXet))
                {
                    nhanXet = tong <= 6 ? "Cần cải thiện nghiêm túc về chuyên cần và hiệu suất làm việc."
                            : tong <= 8 ? "Đạt yêu cầu, cần cố gắng hơn."
                            : "Nhân viên xuất sắc, làm việc hiệu quả!";
                }

                var row = display.NewRow();
                row["STT"] = stt++;
                row["IDNhanVien"] = idNV;
                row["TenNhanVien"] = r["TenNhanVien"];
                if (idNguoiDanhGia.StartsWith("GD"))
                {
                    row["PhongBan"] = r["TenPhongBan"];
                    row["ChucVu"] = r["TenChucVu"];
                }
                row["DiemCC"] = diemCC;
                row["NL_Te"] = te;
                row["NL_TB"] = tb;
                row["NL_Tot"] = tot;
                row["DiemNL"] = diemNL;
                row["TongDiem"] = tong;
                row["XepLoai"] = xepLoai;
                row["NhanXet"] = nhanXet;

                display.Rows.Add(row);
            }

            dgv.DataSource = display;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Quan trọng!

            // Bạn tự chỉnh số pixel tùy thích ở đây (rất dễ đọc, dễ sửa)
            SetColWidth("STT", 80);
            SetColWidth("IDNhanVien", 120);   // Mã NV
            SetColWidth("TenNhanVien", 220);   // Họ tên
            SetColWidth("DiemCC", 85);   // Điểm CC (hoặc DiemChuyenCan)
            SetColWidth("DiemChuyenCan", 85);   // nếu tên cột là thế này
            SetColWidth("NL_Te", 65);   // Tệ
            SetColWidth("NL_TB", 65);   // TB
            SetColWidth("NL_Tot", 65);   // Tốt
            SetColWidth("XepLoai", 100);   // Xếp loại
            SetColWidth("NhanXet", 350);   // Nhận xét (rộng để đọc)

            // Căn giữa các cột nhỏ cho đẹp
            CenterSmallColumns();

            // Bật lại Fill để các cột còn lại tự co giãn khi resize form
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ConfigDGV();
            Filter();
        }

        // Hàm tiện lợi để bạn tự set width (chỉ gọi 1 dòng là xong)
        private void SetColWidth(string columnNameContains, int width)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Name.IndexOf(columnNameContains, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    col.Width = width;
                    col.MinimumWidth = width;     // Không cho co nhỏ hơn
                    col.FillWeight = 1;           // Giữ nguyên kích thước khi resize form
                    break;
                }
            }
        }

        // Căn giữa tự động các cột nhỏ (STT, Điểm CC, Tệ/TB/Tốt, Xếp loại)
        private void CenterSmallColumns()
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Width <= 110) // các cột nhỏ
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void ConfigDGV()
        {
            dgv.Columns["DiemNL"].Visible = false;
            dgv.Columns["TongDiem"].Visible = false;

            dgv.Columns["STT"].Width = 50;
            dgv.Columns["IDNhanVien"].HeaderText = "Mã NV";
            dgv.Columns["TenNhanVien"].HeaderText = "Họ tên";
            if (idNguoiDanhGia.StartsWith("GD"))
            {
                dgv.Columns["PhongBan"].HeaderText = "Phòng ban";
                dgv.Columns["ChucVu"].HeaderText = "Chức vụ";
            }
            dgv.Columns["DiemCC"].HeaderText = "Điểm CC";
            dgv.Columns["NL_Te"].HeaderText = "Tệ";
            dgv.Columns["NL_TB"].HeaderText = "TB";
            dgv.Columns["NL_Tot"].HeaderText = "Tốt";
            dgv.Columns["XepLoai"].HeaderText = "Xếp loại";
            dgv.Columns["NhanXet"].HeaderText = "Nhận xét";

            // Chỉ cho sửa checkbox + nhận xét
            dgv.CellBeginEdit += (s, e) =>
            {
                string col = dgv.Columns[e.ColumnIndex].Name;
                if (!new[] { "NL_Te", "NL_TB", "NL_Tot", "NhanXet" }.Contains(col))
                    e.Cancel = true;
            };

            // Tô màu theo xếp loại
            dgv.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                string xl = dgv.Rows[e.RowIndex].Cells["XepLoai"].Value?.ToString();
                Color bg = xl == "Tốt" ? Color.FromArgb(220, 255, 220) :
                           xl == "Trung bình" ? Color.FromArgb(255, 255, 200) :
                           Color.FromArgb(255, 220, 220);
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = bg;
            };
        }

        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isHandling || e.RowIndex < 0) return;
            isHandling = true;

            var row = dgv.Rows[e.RowIndex];
            string col = dgv.Columns[e.ColumnIndex].Name;

            if (col.StartsWith("NL_"))
            {
                bool val = Convert.ToBoolean(row.Cells[col].Value);
                if (val)
                {
                    row.Cells["NL_Te"].Value = col == "NL_Te";
                    row.Cells["NL_TB"].Value = col == "NL_TB";
                    row.Cells["NL_Tot"].Value = col == "NL_Tot";

                    row.Cells["DiemNL"].Value = col == "NL_Te" ? 1 : col == "NL_TB" ? 2 : 5;
                }
                else
                {
                    row.Cells["NL_Tot"].Value = true;
                    row.Cells["DiemNL"].Value = 5;
                }
                UpdateRow(row.Index);
            }
            else if (col == "NhanXet")
            {
                UpdateRow(e.RowIndex);
            }

            isHandling = false;
        }

        private void UpdateRow(int rowIndex)
        {
            var row = dgv.Rows[rowIndex];
            int diemCC = Convert.ToInt32(row.Cells["DiemCC"].Value);
            int diemNL = Convert.ToInt32(row.Cells["DiemNL"].Value);
            int tong = diemCC + diemNL;
            string xepLoai = tong <= 6 ? "Tệ" : tong <= 8 ? "Trung bình" : "Tốt";

            row.Cells["TongDiem"].Value = tong;
            row.Cells["XepLoai"].Value = xepLoai;

            if (string.IsNullOrWhiteSpace(row.Cells["NhanXet"].Value?.ToString()))
            {
                string nx = tong <= 6 ? "Cần cải thiện nghiêm túc về chuyên cần và hiệu suất."
                          : tong <= 8 ? "Đạt yêu cầu, cần cố gắng hơn."
                          : "Nhân viên xuất sắc, làm việc hiệu quả!";
                row.Cells["NhanXet"].Value = nx;
            }
        }

        private void SaveAll()
        {
            int saved = 0;
            int updated = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                string idNV = row.Cells["IDNhanVien"].Value.ToString();
                int diemCC = Convert.ToInt32(row.Cells["DiemCC"].Value);
                int diemNL = Convert.ToInt32(row.Cells["DiemNL"].Value);
                int tongDiem = diemCC + diemNL;
                string nhanXet = row.Cells["NhanXet"].Value?.ToString() ?? "";
                DateTime ngayTao = new DateTime((int)nudNam.Value, (int)nudThang.Value, DateTime.DaysInMonth((int)nudNam.Value, (int)nudThang.Value)); // Ngày cuối tháng

                var dto = new DTODanhGiaNhanVien
                {
                    IDNhanVien = idNV,
                    IDNguoiDanhGia = idNguoiDanhGia,
                    DiemChuyenCan = diemCC,
                    DiemNangLuc = diemNL,
                    DiemSo = tongDiem,
                    NhanXet = nhanXet,
                    NgayTao = ngayTao // Quan trọng: ngày thuộc đúng tháng đang đánh giá
                };

                try
                {
                    // Kiểm tra đã có đánh giá tháng này chưa
                    if (bllDanhGia.KiemTraDaDanhGiaThang(idNV, (int)nudThang.Value, (int)nudNam.Value))
                    {
                        bllDanhGia.Update(dto); // CẬP NHẬT bản ghi cũ
                        updated++;
                    }
                    else
                    {
                        bllDanhGia.Insert(dto); // THÊM MỚI
                        saved++;
                    }
                }
                catch (Exception ex)
                {
                            MessageBox.Show($"Lỗi lưu nhân viên {idNV}: {ex.Message}");
                }
            }

                MessageBox.Show($"Hoàn tất!\n✓ Thêm mới: {saved} nhân viên\n✓ Cập nhật: {updated} nhân viên", 
                         "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDanhGia(); // Reload → hiển thị đúng dữ liệu vừa sửa

            // ===== TỰ ĐỘNG THƯỞNG + KỶ LUẬT =====
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_TuDongThuongPhatKyLuat", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Thang", (int)nudThang.Value);
                    cmd.Parameters.AddWithValue("@Nam", (int)nudNam.Value);
                    cmd.Parameters.AddWithValue("@idNguoiLap", idNguoiDanhGia);

                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            int thuong = r.GetInt32(0);
                            int kyluat = r.GetInt32(1);
                            string msg = "";
                            if (thuong > 0) msg += $"✓ Đã thưởng {thuong} nhân viên xuất sắc 2 tháng liên tiếp!\n";
                            if (kyluat > 0) msg += $"⚠ Đã lập kỷ luật cho {kyluat} nhân viên đánh giá TỆ 2 tháng liên tiếp!";
                            if (!string.IsNullOrEmpty(msg))
                                MessageBox.Show(msg, "Tự động thưởng & kỷ luật", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tự động thưởng/phạt: " + ex.Message);
            }
        }

        private void ExportExcel()
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"DanhGia_HieuSuat_Thang{(int)nudThang.Value}_Nam{(int)nudNam.Value}.xlsx",
                Title = "Xuất đánh giá hiệu suất ra Excel"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Đánh giá hiệu suất");

                        // ===== TIÊU ĐỀ BẢNG =====
                        ws.Cell(1, 1).Value = $"BẢNG ĐÁNH GIÁ HIỆU SUẤT NHÂN VIÊN - THÁNG {nudThang.Value:00}/{nudNam.Value}";
                        ws.Cell(1, 1).Style.Font.Bold = true;
                        ws.Cell(1, 1).Style.Font.FontSize = 16;
                        ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range(1, 1, 1, dgv.Columns.Count).Merge();

                        // ===== HEADER CỘT =====
                        int colIdx = 1;
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            if (col.Visible)
                            {
                                ws.Cell(3, colIdx).Value = col.HeaderText;
                                ws.Cell(3, colIdx).Style.Font.Bold = true;
                                ws.Cell(3, colIdx).Style.Fill.BackgroundColor = XLColor.FromArgb(25, 50, 120);
                                ws.Cell(3, colIdx).Style.Font.FontColor = XLColor.White;
                                ws.Cell(3, colIdx).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                colIdx++;
                            }
                        }

                        // ===== DỮ LIỆU =====
                        int rowIdx = 4;
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            colIdx = 1;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (dgv.Columns[cell.ColumnIndex].Visible)
                                {
                                    var xlCell = ws.Cell(rowIdx, colIdx);

                                    // Xử lý checkbox năng lực → hiện chữ thay vì True/False
                                    if (dgv.Columns[cell.ColumnIndex].Name.Contains("NL_"))
                                    {
                                        xlCell.Value = Convert.ToBoolean(cell.Value) ? "✓" : "";
                                        xlCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    }
                                    else
                                    {
                                        xlCell.Value = cell.FormattedValue?.ToString() ?? "";
                                    }

                                    // Tô màu nền theo xếp loại
                                    string xepLoai = row.Cells["XepLoai"]?.Value?.ToString() ?? "";
                                    if (xepLoai == "Tốt") xlCell.Style.Fill.BackgroundColor = XLColor.FromArgb(220, 255, 220);
                                    else if (xepLoai == "Trung bình") xlCell.Style.Fill.BackgroundColor = XLColor.FromArgb(255, 255, 200);
                                    else if (xepLoai == "Tệ") xlCell.Style.Fill.BackgroundColor = XLColor.FromArgb(255, 220, 220);

                                    colIdx++;
                                }
                            }
                            rowIdx++;
                        }

                        // ===== ĐỊNH DẠNG ĐẸP =====
                        var range = ws.Range(3, 1, rowIdx - 1, colIdx - 1);
                        range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        ws.Columns().AdjustToContents();
                        ws.Rows().AdjustToContents();

                        wb.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show($"Xuất Excel thành công!\nFile: {sfd.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true }); // Mở file luôn
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Filter()
        {
            if (dgv.DataSource == null) return;
            var view = ((DataTable)dgv.DataSource).DefaultView;
            string kw = txtSearch.Text.Trim().Replace("'", "''");
            view.RowFilter = string.IsNullOrEmpty(kw) ? "" : $"TenNhanVien LIKE '%{kw}%' OR NhanXet LIKE '%{kw}%'";
        }
    }
}