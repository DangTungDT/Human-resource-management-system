using BLL;
using DAL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI
{
    public partial class TaoKyLuat : UserControl
    {
        private Guna2ComboBox cbLyDo;
        private Guna2TextBox txtSoTien;
        private Guna2TextBox txtLyDoMoi;
        private Guna2DateTimePicker dtThangApDung;
        private CheckedListBox lstNhanVien;
        private Guna2Button btnSave, btnUndo, btnDetele;
        private Guna2DataGridView dgv;
        private Guna2ComboBox cbPhongBan;
        private readonly BLLNhanVien bllNhanVien;
        private readonly BLLNhanVien_ThuongPhat bllNhanVienThuongPhat;
        private readonly BLLPhongBan bllPhongBan;

        private string idNguoiTao = "GD00000001";
        private string connectionString;
        private bool isUpdating = false;  // true nếu đang cập nhật
        private int currentGroupId = -1;  // lưu Id nhóm đang edit

        public TaoKyLuat(string idNhanVien, string conn)
        {
            connectionString = conn;
            InitializeComponent();
            bllNhanVien = new BLLNhanVien(conn);
            bllNhanVienThuongPhat = new BLLNhanVien_ThuongPhat(conn);
            bllPhongBan = new BLLPhongBan(conn);
            BuildUI(); 
            LoadPhongBan();     // Load danh sách phòng ban vào cbPhongBan
            LoadLyDo();         // Load danh sách lý do thưởng vào cbLyDo
            LoadNhanVienList(); // Load danh sách nhân viên vào CheckedListBox
            LoadDanhSachKyLuat();
        }

        // ====================== GIAO DIỆN ======================
        private void BuildUI()
        {
            // Toàn bộ UserControl chiếm toàn màn hình và có màu nền sáng dịu
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // ======= TIÊU ĐỀ CHÍNH =======
            Label lblTitle = new Label()
            {
                Text = "📋 KỶ LUẬT NHÂN VIÊN",
                Font = new Font("Times New Roman", 18, FontStyle.Bold),
                ForeColor = Color.Maroon,
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ======= KHỐI FORM BAO NGOÀI =======
            var pnlFormCard = new Guna2Panel()
            {
                BorderRadius = 12,
                FillColor = Color.White,
                Padding = new Padding(30, 20, 30, 20),
                ShadowDecoration = { Depth = 10, Enabled = true },
                Dock = DockStyle.Fill
            };

            // ======= TABLE LAYOUT CHÍNH (GỒM PHẦN TÌM KIẾM + FORM) =======
            TableLayoutPanel tlMainForm = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.White
            };
            tlMainForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // dòng đầu: tìm kiếm
            tlMainForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // dòng thứ hai: nội dung

            // ======= PHẦN TÌM KIẾM PHÒNG BAN =======
            FlowLayoutPanel pnlSearch = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10, 5, 0, 5),
                AutoSize = true
            };

            pnlSearch.Controls.Add(new Label
            {
                Text = "Tìm theo phòng ban:",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                Margin = new Padding(0, 10, 10, 0)
            });

            cbPhongBan = new Guna2ComboBox
            {
                BorderRadius = 8,
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Times New Roman", 10.5f)
            };

            // Nút tìm kiếm
            Guna2Button btnSearch = new Guna2Button
            {
                Size = new Size(38, 36),
                Margin = new Padding(8, 0, 0, 0),
                Image = Properties.Resources.search,
                ImageSize = new Size(18, 18),
                FillColor = Color.FromArgb(90, 100, 255),
                BorderRadius = 8,
                Cursor = Cursors.Hand
            };
            btnSearch.Click += btnTimKiem_Click;

            pnlSearch.Controls.Add(cbPhongBan);
            pnlSearch.Controls.Add(btnSearch);

            // ======= CHIA THÀNH 2 CỘT TRÁI - PHẢI =======
            TableLayoutPanel tlTwoColumn = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(0, 5, 0, 0),
                BackColor = Color.White
            };
            tlTwoColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));
            tlTwoColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52));

            // ======= CỘT TRÁI: NHÂN VIÊN & LÝ DO CÓ SẴN =======
            TableLayoutPanel tlLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = Color.White
            };
            tlLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            tlLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tlLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 110));
            tlLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            Label MakeLabel(string text) => new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            lstNhanVien = new CheckedListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Times New Roman", 10),
                BackColor = Color.White
            };

            cbLyDo = new Guna2ComboBox
            {
                BorderRadius = 8,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 10.5f)
            };
            cbLyDo.SelectedIndexChanged += CbLyDo_SelectedIndexChanged;

            tlLeft.Controls.Add(MakeLabel("Nhân viên:"), 0, 0);
            tlLeft.Controls.Add(lstNhanVien, 1, 0);
            tlLeft.Controls.Add(MakeLabel("Lý do có sẵn:"), 0, 1);
            tlLeft.Controls.Add(cbLyDo, 1, 1);

            // ======= CỘT PHẢI: LÝ DO MỚI - SỐ TIỀN - NGÀY ÁP DỤNG =======
            TableLayoutPanel tlRight = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                BackColor = Color.White
            };
            tlRight.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            tlRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 3; i++)
                tlRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            txtLyDoMoi = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập lý do mới...",
                Font = new Font("Times New Roman", 10.5f)
            };

            txtSoTien = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập số tiền phạt...",
                Font = new Font("Times New Roman", 10.5f)
            };

            dtThangApDung = new Guna2DateTimePicker
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Font = new Font("Times New Roman", 10.5f)
            };

            tlRight.Controls.Add(MakeLabel("Lý do mới:"), 0, 0);
            tlRight.Controls.Add(txtLyDoMoi, 1, 0);
            tlRight.Controls.Add(MakeLabel("Số tiền:"), 0, 1);
            tlRight.Controls.Add(txtSoTien, 1, 1);
            tlRight.Controls.Add(MakeLabel("Ngày áp dụng:"), 0, 2);
            tlRight.Controls.Add(dtThangApDung, 1, 2);

            // Gộp hai phần trái - phải vào layout chính
            tlTwoColumn.Controls.Add(tlLeft, 0, 0);
            tlTwoColumn.Controls.Add(tlRight, 1, 0);

            // Gắn phần tìm kiếm và form vào panel chính
            tlMainForm.Controls.Add(pnlSearch, 0, 0);
            tlMainForm.Controls.Add(tlTwoColumn, 0, 1);
            pnlFormCard.Controls.Add(tlMainForm);

            // ======= NÚT CHỨC NĂNG =======
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 5, 40, 0),
                Height = 55,
                BackColor = Color.Transparent
            };

            btnSave = new Guna2Button
            {
                Text = "💾 Lưu kỷ luật",
                Width = 160,
                Height = 42,
                BorderRadius = 8,
                FillColor = Color.FromArgb(45, 140, 90),
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnSave.Click += BtnSave_Click;

            btnUndo = new Guna2Button
            {
                Text = "↩️ Hoàn tác",
                Width = 140,
                Height = 42,
                BorderRadius = 8,
                FillColor = Color.FromArgb(130, 130, 130),
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnUndo.Click += BtnUndo_Click;

            btnDetele = new Guna2Button
            {
                Text = "Xóa",
                Width = 140,
                Height = 42,
                BorderRadius = 8,
                FillColor = Color.FromArgb(130, 130, 130),
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnDetele.Click += btnDelete_Click;

            pnlButtons.Controls.Add(btnSave);
            pnlButtons.Controls.Add(btnUndo);
            pnlButtons.Controls.Add(btnDetele);

            // ======= BẢNG DỮ LIỆU NHÂN VIÊN BỊ KỶ LUẬT =======
            dgv = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ColumnHeadersHeight = 36,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(230, 235, 245),
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(248, 250, 255)
                }
            };
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(45, 85, 155),
                ForeColor = Color.White,
                Font = new Font("Times New Roman", 10.5f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Times New Roman", 10),
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(94, 148, 255),
                SelectionForeColor = Color.Black
            };
            dgv.CellClick += Dgv_CellClick;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = true;

            // ======= BỐ CỤC TỔNG THỂ TRANG =======
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Tiêu đề
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 265)); // Form nhỏ gọn lại
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Nút chức năng
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  // Bảng dữ liệu

            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(pnlFormCard, 0, 1);
            mainLayout.Controls.Add(pnlButtons, 0, 2);
            mainLayout.Controls.Add(dgv, 0, 3);

            this.Controls.Add(mainLayout);
        }





        // ====================== LOAD DỮ LIỆU ======================

        private void LoadPhongBan()
        {
            var dt = bllPhongBan.ComboboxPhongBan();    // gọi BLL trả về DataTable

            // Tạo một dòng "Xem tất cả" thủ công
            DataRow allRow = dt.NewRow();
            allRow["id"] = DBNull.Value;                          // giá trị rỗng để khi SelectedValue = "" => hiểu là xem tất cả
            allRow["TenPhongBan"] = "Xem tất cả";
            dt.Rows.InsertAt(allRow, 0);                // chèn lên đầu danh sách

            cbPhongBan.DataSource = dt;                 // bind datatable
            cbPhongBan.DisplayMember = "TenPhongBan";   // trường hiển thị cho người dùng
            cbPhongBan.ValueMember = "id";              // giá trị tương ứng (dùng khi lấy SelectedValue)
            cbPhongBan.SelectedIndex = 0;               // mặc định chọn "Xem tất cả"
        }

        // Load danh sách lý do (Thưởng) vào combobox cbLyDo
        private void LoadLyDo()
        {
            var dt = bllNhanVienThuongPhat.GetAllLyDo("Phạt"); // gọi BLL lấy lý do theo loại "Phạt"
            // Copy DataTable để thêm dòng "Thêm lý do mới"
            DataTable dt2 = dt.Copy();
            DataRow r = dt2.NewRow();
            r["id"] = -1;                            // id = -1 biểu thị "Thêm mới"
            r["lyDo"] = "-- Thêm lý do mới --";     // hiển thị cho người dùng
            r["tienThuongPhat"] = 0;                 // giá trị tiền mặc định
            dt2.Rows.Add(r);                         // thêm dòng ở cuối (có thể InsertAt để ở đầu) => dt2.Rows.InsertAt(r, 0);.

            // Bind combobox
            cbLyDo.DisplayMember = "lyDo";
            cbLyDo.ValueMember = "id";
            cbLyDo.DataSource = dt2;
            cbLyDo.SelectedIndex = -1;               // không chọn item nào khi load xong
        }

        // Load danh sách nhân viên vào CheckedListBox (hiển thị "id - TenNhanVien")
        private void LoadNhanVienList()
        {
            var dt = bllNhanVien.ComboboxNhanVien(); // lấy DataTable nhân viên
            lstNhanVien.Items.Clear();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                // Lấy id và name từ row và thêm CLBItem (class helper) vào CheckedListBox
                string id = row["id"].ToString();
                string name = row["TenNhanVien"].ToString();
                lstNhanVien.Items.Add(new CLBItem(id, name));
            }
        }

        private void LoadDanhSachKyLuat(string idPhongBan = "")
        {
            dgv.DataSource = bllNhanVienThuongPhat.GetAll("Phạt", idPhongBan); // bind DataTable trả về từ BLL
            if (!dgv.Columns.Contains("Xoa"))
            {
                DataGridViewImageColumn colDelete = new DataGridViewImageColumn()
                {
                    Name = "Xoa",
                    HeaderText = "Xóa",
                    Image = Properties.Resources.delete,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 50
                };
                dgv.Columns.Add(colDelete);
                dgv.Columns["Xoa"].DisplayIndex = dgv.Columns.Count - 1;
                //// ===== Căn chỉnh giao diện =====
                //dgv.Columns["Xóa"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv.Columns["Xóa"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //// Chiều cao dòng đồng đều, icon vừa khít
                //dgv.RowTemplate.Height = 36; // Hoặc 40 nếu bạn muốn hàng cao hơn
                //dgv.ColumnHeadersHeight = 36;

                //// Tắt tự động co dòng
                //dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                //// Thêm padding để icon không bị dính lề
                //dgv.Columns["Xóa"].DefaultCellStyle.Padding = new Padding(0, 4, 0, 4);
            }
        }

        // ====================== SỰ KIỆN ======================
        // Event: khi combobox lý do thay đổi -> fill tiền hoặc bật ô nhập lý do mới
        private void CbLyDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLyDo.SelectedItem == null) return;  // nếu chưa chọn item thì thoát

            // Khi binding với DataTable, SelectedValue đôi khi là DataRowView (trong quá trình bind)
            if (cbLyDo.SelectedValue == null || cbLyDo.SelectedValue is DataRowView)
                return; // bỏ qua nếu SelectedValue chưa phải là giá trị id thực sự

            int id = Convert.ToInt32(cbLyDo.SelectedValue); // chuyển SelectedValue về int (id)

            if (id == -1) // Nếu chọn "Thêm mới"
            {
                txtLyDoMoi.Enabled = true;           // bật textbox nhập lý do mới
                txtLyDoMoi.FillColor = Color.White;  // set nền trắng (để rõ là có thể nhập)
                txtSoTien.Enabled = true;            // bật textbox số tiền để nhập
                txtSoTien.Text = "";                 // xóa giá trị hiện có
            }
            else
            {
                // Nếu chọn lý do có sẵn
                txtLyDoMoi.Enabled = false;          // tắt nhập lý do mới
                txtLyDoMoi.Text = "";                // clear nội dung ô lý do mới

                // Lấy DataRowView ứng với item được chọn để đọc giá trị tỉ lệ tiền
                var drv = cbLyDo.SelectedItem as DataRowView;
                if (drv != null)
                {
                    decimal tien = 0;
                    // parse giá trị tiền từ trường 'tienThuongPhat' (nếu có)
                    decimal.TryParse(drv["tienThuongPhat"]?.ToString(), out tien);
                    txtSoTien.Text = tien.ToString("0.##"); // format hiển thị
                    txtSoTien.Enabled = false;              // không cho sửa nếu chọn lý do có sẵn
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị được chọn trong combobox
                var selectedValue = cbPhongBan.SelectedValue;
                string idPhongBan = (selectedValue == DBNull.Value || selectedValue == null)
                    ? null
                    : selectedValue.ToString();

                // Lấy tên phòng ban để hiển thị thông báo
                string tenPhongBan = cbPhongBan.Text?.Trim();

                DataTable dtResult;

                // Nếu chọn “Xem tất cả” hoặc chưa chọn phòng ban
                if (string.IsNullOrEmpty(idPhongBan))
                {
                    dtResult = bllNhanVienThuongPhat.GetAll("Phạt");
                    MessageBox.Show("Đang hiển thị danh sách kỷ luật của tất cả phòng ban.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dtResult = bllNhanVienThuongPhat.GetAll("Phạt", idPhongBan);

                    if (dtResult.Rows.Count == 0)
                    {
                        MessageBox.Show($"Không tìm thấy nhân viên bị kỷ luật trong phòng ban '{tenPhongBan}'.",
                                        "Kết quả trống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Đã tìm thấy {dtResult.Rows.Count} nhân viên bị kỷ luật trong phòng ban '{tenPhongBan}'.",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Gán kết quả vào DataGridView
                dgv.DataSource = dtResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tìm kiếm: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Xác định loại từ form
                string loai = this.Tag?.ToString() ?? "Phạt"; // nếu form "Thưởng" thì Tag = "Thưởng"
                List<string> selectedIds = new List<string>();

                foreach (CLBItem item in lstNhanVien.CheckedItems)
                    selectedIds.Add(item.Id);

                if (selectedIds.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một nhân viên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string lyDo;
                decimal soTien;

                // Xử lý lý do (chọn cũ hoặc nhập mới)
                if (cbLyDo.SelectedIndex < 0 || Convert.ToInt32(cbLyDo.SelectedValue) == -1)
                {
                    if (string.IsNullOrWhiteSpace(txtLyDoMoi.Text))
                    {
                        MessageBox.Show("Vui lòng nhập lý do mới.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!decimal.TryParse(txtSoTien.Text, out soTien))
                    {
                        MessageBox.Show("Số tiền không hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    lyDo = txtLyDoMoi.Text.Trim();
                }
                else
                {
                    lyDo = cbLyDo.Text.Trim();
                    soTien = string.IsNullOrEmpty(txtSoTien.Text) ? 0 : Convert.ToDecimal(txtSoTien.Text);
                }

                DateTime ngayApDung = dtThangApDung.Value;

                // === Cập nhật nhóm cũ ===
                if (dgv.CurrentRow != null && dgv.CurrentRow.Cells["id"].Value != null)
                {
                    int idThuongPhat = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value);
                    bllNhanVienThuongPhat.UpdateMultiSmart(loai, idThuongPhat, selectedIds, lyDo, soTien, ngayApDung);

                    MessageBox.Show($"Cập nhật {loai.ToLower()} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // === Tạo mới nhóm ===
                    bllNhanVienThuongPhat.SaveMulti(loai, selectedIds, lyDo, soTien, ngayApDung, idNguoiTao);

                    MessageBox.Show($"Thêm mới {loai.ToLower()} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadLyDo();
                LoadDanhSachKyLuat(); // hoặc LoadDanhSachThuong() nếu là form thưởng
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hoàn tác thay đổi?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ClearForm();
        }

        private void ClearForm()
        {
            for (int i = 0; i < lstNhanVien.Items.Count; i++)
                lstNhanVien.SetItemChecked(i, false);

            cbLyDo.SelectedIndex = 0;
            txtLyDoMoi.Text = "";
            txtSoTien.Text = "";
            dtThangApDung.Value = DateTime.Now;
            btnSave.Text = "Thêm mới";
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgv.Rows[e.RowIndex].IsNewRow) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                // Xử lý xóa
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
                if (MessageBox.Show("Xóa phạt này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bllNhanVienThuongPhat.Delete(id);
                    LoadData();
                }
                return;
            }

            // 1️⃣ Lấy dữ liệu dòng hiện tại
            currentGroupId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
            txtSoTien.Text = dgv.Rows[e.RowIndex].Cells["SoTien"].Value.ToString();
            cbLyDo.Text = dgv.Rows[e.RowIndex].Cells["LyDo"].Value.ToString();
            dtThangApDung.Value = Convert.ToDateTime(dgv.Rows[e.RowIndex].Cells["NgayApDung"].Value);

            // 2️⃣ Lấy danh sách nhân viên thuộc nhóm này
            var empIds = bllNhanVienThuongPhat.GetNhanVienByThuongPhatId(currentGroupId);

            // 3️⃣ Reset check
            for (int i = 0; i < lstNhanVien.Items.Count; i++)
            {
                var item = lstNhanVien.Items[i] as CLBItem;
                lstNhanVien.SetItemChecked(i, empIds.Contains(item.Id));
            }

            // 4️⃣ Chuyển sang chế độ cập nhật
            isUpdating = true;
            btnSave.Text = "Cập nhật";
        }

        //xóa nhiều theo cách xóa từng cái
        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (dgv.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show("Vui lòng chọn ít nhất một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }

        //    var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa các dòng đã chọn không?",
        //                                  "Xác nhận xóa",
        //                                  MessageBoxButtons.YesNo,
        //                                  MessageBoxIcon.Warning);
        //    if (confirm != DialogResult.Yes) return;

        //    // Lấy danh sách ID của các dòng được chọn
        //    List<int> idsToDelete = new List<int>();
        //    foreach (DataGridViewRow row in dgv.SelectedRows)
        //    {
        //        if (row.Cells["ID"].Value != null)
        //        {
        //            idsToDelete.Add(Convert.ToInt32(row.Cells["ID"].Value));
        //        }
        //    }

        //    try
        //    {
        //        // Gọi BLL hoặc DAL để xóa trong DB
        //        foreach (int id in idsToDelete)
        //        {
        //            bllNhanVienThuongPhat.Delete(id); // hoặc bll.Delete(id)
        //        }

        //        // Sau khi xóa thì tải lại dữ liệu
        //        LoadData();

        //        MessageBox.Show("Đã xóa thành công các dòng được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi khi xóa: " + ex.Message);
        //    }
        //}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa các dòng được chọn không?",
                                          "Xác nhận xóa",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            // Lấy danh sách ID từ các dòng được chọn
            List<int> idsToDelete = new List<int>();
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                if (row.Cells["id"].Value != null)
                    idsToDelete.Add(Convert.ToInt32(row.Cells["id"].Value));
            }

            try
            {
                bllNhanVienThuongPhat.XoaNhieuNhanVien_ThuongPhat(idsToDelete);

                // Tải lại dữ liệu sau khi xóa
                LoadData();

                MessageBox.Show("Đã xóa thành công các dòng được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void LoadData(string idPhongBan = "")
        {
            dgv.DataSource = bllNhanVienThuongPhat.GetAll("Phạt", idPhongBan); // bind DataTable trả về từ BLL

            // ensure delete column exists: nếu chưa có cột "Xoa" (icon) thì thêm vào
            if (!dgv.Columns.Contains("Xoa"))
            {
                DataGridViewImageColumn colDelete = new DataGridViewImageColumn
                {
                    Name = "Xoa",
                    HeaderText = "Xóa",
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 50
                };
                // cố gắng set icon từ resources nếu có, nếu không có thì bỏ qua (không ném lỗi)
                try { colDelete.Image = Properties.Resources.delete; } catch { }
                dgv.Columns.Add(colDelete);
            }
        }

        // DataGridView: khi di chuột vào cột Xóa thì đổi con trỏ và icon
        private void Dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            // kiểm tra index hợp lệ và tên cột là "Xoa"
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Hand; // đổi con trỏ chuột
                try { dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.trash; } catch { }
                // cố gắng gán icon trash (nếu resource có), try-catch để tránh ném lỗi
            }
        }

        // DataGridView: khi chuột rời cột Xóa thì phục hồi icon mặc định
        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Default;
                try { dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.delete; } catch { }
            }
        }

        // helper class cho CheckedListBox item: lưu Id + Name, ToString() trả về "id - Name"
        private class CLBItem
        {
            public string Id { get; }
            public string Name { get; }
            public CLBItem(string id, string name) { Id = id; Name = name; }
            public override string ToString() => $"{Id} - {Name}";
        }
    }
}