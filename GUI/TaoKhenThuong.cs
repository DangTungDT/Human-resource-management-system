using BLL;                           // Tham chiếu tới thư viện BLL (Business Logic Layer) của project
using DTO;                           // Tham chiếu tới Data Transfer Objects - các model dữ liệu
using Guna.UI2.WinForms;             // Thư viện UI Guna2 dùng cho controls đẹp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    // UserControl dùng để "Tạo khen thưởng" cho nhân viên
    public partial class TaoKhenThuong : UserControl
    {
        // === Các trường BLL ===
        private readonly BLLNhanVien_ThuongPhat bll;   // BLL xử lý nghiệp vụ liên quan khen thưởng / phạt
        private readonly BLLPhongBan bllPhongBan;     // BLL xử lý nghiệp vụ Phòng ban (để load combobox)
        private readonly BLLNhanVien bllNhanVien;     // BLL xử lý nghiệp vụ nhân viên (danh sách nhân viên)

        // === Controls UI (khai báo ở đây để dùng trong toàn class) ===
        private Guna2ComboBox cbPhongBan;             // Combobox chọn phòng ban
        private Guna2ComboBox cbLyDo;                 // Combobox chọn lý do thưởng có sẵn
        private Guna2TextBox txtAmount, txtNewLyDo;   // Textbox nhập số tiền, textbox nhập lý do mới
        private CheckedListBox lstNhanVien;           // CheckedListBox để chọn nhiều nhân viên
        private Guna2DateTimePicker dtNgay;           // DateTimePicker chọn ngày áp dụng
        private Guna2Button btnSave, btnUndo, btnSearch, btnDetele; // Nút lưu, hoàn tác, tìm kiếm, xóa
        private Guna2DataGridView dgv;                // DataGridView hiển thị danh sách khen thưởng (hoặc danh sách đã áp dụng)

        private string idNguoiTao = "GD00000001";     // ID người tạo tạm đặt (có thể lấy từ session/user)
        private bool isUpdating = false;  // true nếu đang cập nhật
        private int currentGroupId = -1;  // lưu Id nhóm đang edit

        // Constructor: nhận connection string, khởi tạo BLL và build giao diện
        public TaoKhenThuong(string conn)
        {
            InitializeComponent();   // Khởi tạo các thành phần (nếu dùng designer)
            // Khởi tạo các service BLL với connection string truyền vào
            bll = new BLLNhanVien_ThuongPhat(conn);
            bllPhongBan = new BLLPhongBan(conn);
            bllNhanVien = new BLLNhanVien(conn);

            BuildUI();          // Tạo UI bằng code (không dùng designer)
            LoadPhongBan();     // Load danh sách phòng ban vào cbPhongBan
            LoadLyDo();         // Load danh sách lý do thưởng vào cbLyDo
            LoadNhanVienList(); // Load danh sách nhân viên vào CheckedListBox
            LoadData();         // Load dữ liệu hiển thị lên dgv
        }
        public TaoKhenThuong(string idNhanVien, string conn)
        {
            InitializeComponent();   // Khởi tạo các thành phần (nếu dùng designer)
            // Khởi tạo các service BLL với connection string truyền vào
            bll = new BLLNhanVien_ThuongPhat(conn);
            bllPhongBan = new BLLPhongBan(conn);
            bllNhanVien = new BLLNhanVien(conn);

            BuildUI();          // Tạo UI bằng code (không dùng designer)
            LoadPhongBan();     // Load danh sách phòng ban vào cbPhongBan
            LoadLyDo();         // Load danh sách lý do thưởng vào cbLyDo
            LoadNhanVienList(); // Load danh sách nhân viên vào CheckedListBox
            LoadData();         // Load dữ liệu hiển thị lên dgv
        }

        // === XÂY DỰNG GIAO DIỆN CHÍNH CHO USERCONTROL "Tạo Khen Thưởng" ===
        private void BuildUI()
        {
            // Dock toàn bộ UserControl để tự động chiếm hết vùng chứa
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250); // màu nền tổng thể nhạt, tone xám sáng

            // === TIÊU ĐỀ TRÊN CÙNG ===
            Label lblTitle = new Label
            {
                Text = "🎖️ TẠO KHEN THƯỞNG NHÂN VIÊN", // tiêu đề trang
                Dock = DockStyle.Top,
                Height = 65,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 70, 140),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White,
            };

            // === KHỐI PANEL TRẮNG CHỨA FORM ===
            var pnlFormCard = new Guna2Panel()
            {
                BorderRadius = 12,
                FillColor = Color.White,
                Padding = new Padding(30, 20, 30, 20),
                ShadowDecoration = { Depth = 10, Enabled = true },
                Dock = DockStyle.Fill
            };

            // === TABLELAYOUT CHÍNH (GỒM PHẦN TÌM KIẾM + FORM) ===
            TableLayoutPanel tlMainForm = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.White
            };
            tlMainForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // dòng tìm kiếm
            tlMainForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // dòng form nội dung

            // === PHẦN TÌM KIẾM PHÒNG BAN ===
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
                Font = new Font("Segoe UI Semibold", 10.5f),
                Margin = new Padding(0, 10, 10, 0)
            });

            cbPhongBan = new Guna2ComboBox
            {
                BorderRadius = 8,
                Width = 220,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };

            Guna2Button btnSearch = new Guna2Button
            {
                Size = new Size(38, 36),
                Margin = new Padding(8, 0, 0, 0),
                Image = Properties.Resources.search,
                ImageSize = new Size(18, 18),
                FillColor = Color.MediumSlateBlue,
                BorderRadius = 8,
                Cursor = Cursors.Hand
            };
            btnSearch.Click += btnTimKiem_Click;
            pnlSearch.Controls.Add(cbPhongBan);
            pnlSearch.Controls.Add(btnSearch);

            // === CHIA 2 CỘT TRÁI - PHẢI ===
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

            // === CỘT TRÁI: NHÂN VIÊN + LÝ DO CÓ SẴN ===
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
                Font = new Font("Segoe UI Semibold", 10.5f),
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
                Font = new Font("Segoe UI", 10)
            };
            cbLyDo.SelectedIndexChanged += CbLyDo_SelectedIndexChanged;

            tlLeft.Controls.Add(MakeLabel("Nhân viên:"), 0, 0);
            tlLeft.Controls.Add(lstNhanVien, 1, 0);
            tlLeft.Controls.Add(MakeLabel("Lý do có sẵn:"), 0, 1);
            tlLeft.Controls.Add(cbLyDo, 1, 1);

            // === CỘT PHẢI: LÝ DO MỚI - SỐ TIỀN - NGÀY ÁP DỤNG ===
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

            txtNewLyDo = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập lý do mới...",
                Font = new Font("Segoe UI", 10)
            };

            txtAmount = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập số tiền thưởng...",
                Font = new Font("Segoe UI", 10)
            };

            dtNgay = new Guna2DateTimePicker
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Font = new Font("Segoe UI", 10)
            };

            tlRight.Controls.Add(MakeLabel("Lý do mới:"), 0, 0);
            tlRight.Controls.Add(txtNewLyDo, 1, 0);
            tlRight.Controls.Add(MakeLabel("Số tiền:"), 0, 1);
            tlRight.Controls.Add(txtAmount, 1, 1);
            tlRight.Controls.Add(MakeLabel("Ngày áp dụng:"), 0, 2);
            tlRight.Controls.Add(dtNgay, 1, 2);

            // Ghép 2 phần trái-phải
            tlTwoColumn.Controls.Add(tlLeft, 0, 0);
            tlTwoColumn.Controls.Add(tlRight, 1, 0);

            // Gắn phần tìm kiếm và form vào panel chính
            tlMainForm.Controls.Add(pnlSearch, 0, 0);
            tlMainForm.Controls.Add(tlTwoColumn, 0, 1);
            pnlFormCard.Controls.Add(tlMainForm);

            // === NÚT CHỨC NĂNG (Lưu / Hoàn tác) ===
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
                Text = "💾 Lưu thưởng",
                Width = 160,
                Height = 42,
                BorderRadius = 8,
                FillColor = Color.FromArgb(45, 140, 90),
                Font = new Font("Segoe UI Semibold", 10.5f),
                ForeColor = Color.White
            };
            btnSave.Click += btnSave_Click;

            btnUndo = new Guna2Button
            {
                Text = "↩️ Hoàn tác",
                Width = 140,
                Height = 42,
                BorderRadius = 8,
                FillColor = Color.FromArgb(130, 130, 130),
                Font = new Font("Segoe UI Semibold", 10.5f),
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

            // === BẢNG DỮ LIỆU NHÂN VIÊN ĐƯỢC THƯỞNG ===
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
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(94, 148, 255),
                SelectionForeColor = Color.Black
            };
            dgv.CellClick += Dgv_CellClick;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = true;

            // === GHÉP TẤT CẢ THÀNH GIAO DIỆN CHÍNH ===
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));  // tiêu đề
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 265)); // form
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // nút
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  // bảng

            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(pnlFormCard, 0, 1);
            mainLayout.Controls.Add(pnlButtons, 0, 2);
            mainLayout.Controls.Add(dgv, 0, 3);

            this.Controls.Add(mainLayout);
        }


        private void DinhDangCotDgv()
        {
            if (dgv.Columns.Count == 0) return;

            dgv.Columns["id"].Visible = false;

            dgv.Columns["TenNhanVien"].HeaderText = "Nhân viên";
            dgv.Columns["TenPhongBan"].HeaderText = "Phòng ban";
            dgv.Columns["LyDo"].HeaderText = "Lý do";
            dgv.Columns["SoTien"].HeaderText = "Số tiền";
            dgv.Columns["Loai"].HeaderText = "Loại";
            dgv.Columns["NgayApDung"].HeaderText = "Ngày áp dụng";

            dgv.Columns["SoTien"].DefaultCellStyle.Format = "N0";
            dgv.Columns["NgayApDung"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 85, 155);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;

            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        // Load danh sách phòng ban vào combobox
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
            var dt = bll.GetAllLyDo("Thưởng"); // gọi BLL lấy lý do theo loại "Thưởng"
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

        // Load dữ liệu hiển thị lên dgv (ví dụ: danh sách nhóm thưởng hoặc records)
        private void LoadData(string idPhongBan = "")
        {
            dgv.DataSource = bll.GetAll("Thưởng", idPhongBan); // bind DataTable trả về từ BLL

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
                txtNewLyDo.Enabled = true;           // bật textbox nhập lý do mới
                txtNewLyDo.FillColor = Color.White;  // set nền trắng (để rõ là có thể nhập)
                txtAmount.Enabled = true;            // bật textbox số tiền để nhập
                txtAmount.Text = "";                 // xóa giá trị hiện có
            }
            else
            {
                // Nếu chọn lý do có sẵn
                txtNewLyDo.Enabled = false;          // tắt nhập lý do mới
                txtNewLyDo.Text = "";                // clear nội dung ô lý do mới

                // Lấy DataRowView ứng với item được chọn để đọc giá trị tỉ lệ tiền
                var drv = cbLyDo.SelectedItem as DataRowView;
                if (drv != null)
                {
                    decimal tien = 0;
                    // parse giá trị tiền từ trường 'tienThuongPhat' (nếu có)
                    decimal.TryParse(drv["tienThuongPhat"]?.ToString(), out tien);
                    txtAmount.Text = tien.ToString("0.##"); // format hiển thị
                    txtAmount.Enabled = false;              // không cho sửa nếu chọn lý do có sẵn
                }
            }
        }


        // Lưu: lấy danh sách nhân viên được check, nếu chọn lý do mới thì insert lý do mới trước,
        // sau đó gọi BLL.SaveMulti để lưu nhiều nhân viên cùng lúc
        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> selectedNhanViens = new List<string>();
            foreach (CLBItem item in lstNhanVien.CheckedItems)
            {
                selectedNhanViens.Add(item.Id);
            }

            // 🟢 Xác định lý do được sử dụng
            string lyDo = string.Empty;

            if (!string.IsNullOrWhiteSpace(txtNewLyDo.Text))
            {
                // Nếu nhập lý do mới
                lyDo = txtNewLyDo.Text.Trim();

                // Cập nhật combobox (nếu cần)
                LoadLyDo();
            }
            else if (cbLyDo.SelectedItem != null)
            {
                // Nếu chọn lý do có sẵn
                lyDo = cbLyDo.Text;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập lý do!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal soTien = decimal.Parse(txtAmount.Text);
            DateTime ngayApDung = dtNgay.Value;

            if (!isUpdating)
            {
                bll.SaveMulti("Thưởng", selectedNhanViens, lyDo, soTien, ngayApDung, idNguoiTao);
            }
            else
            {
                bll.UpdateMultiSmart("Thưởng", currentGroupId, selectedNhanViens, lyDo, soTien, ngayApDung);
                isUpdating = false;
                btnSave.Text = "Thêm mới";
            }
            LoadLyDo();
            LoadData();
            ClearForm();
        }

        // DataGridView: xử lý click (dùng để xóa record khi click vào icon Xóa)
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgv.Rows[e.RowIndex].IsNewRow) return;

            // 🗑 Nếu click vào cột Xóa
            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
                if (MessageBox.Show("Xóa khen thưởng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    bll.Delete(id);
                    LoadData();
                }
                return;
            }

            // 1️⃣ Lấy dữ liệu dòng hiện tại
            currentGroupId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
            txtAmount.Text = dgv.Rows[e.RowIndex].Cells["SoTien"].Value?.ToString() ?? "";
            cbLyDo.Text = dgv.Rows[e.RowIndex].Cells["LyDo"].Value?.ToString() ?? "";
            dtNgay.Value = Convert.ToDateTime(dgv.Rows[e.RowIndex].Cells["NgayApDung"].Value);

            // 2️⃣ Lấy danh sách nhân viên thuộc nhóm thưởng/phạt này
            List<string> empIds = bll.GetNhanVienByThuongPhatId(currentGroupId) ?? new List<string>();

            // 3️⃣ Reset toàn bộ check trạng thái trong CheckedListBox
            for (int i = 0; i < lstNhanVien.Items.Count; i++)
            {
                var item = lstNhanVien.Items[i] as CLBItem;
                if (item == null) continue;

                // So sánh ID (theo chuỗi, không phân biệt hoa thường)
                bool isChecked = empIds.Any(x =>
                    x.Equals(item.Id.ToString(), StringComparison.OrdinalIgnoreCase));

                lstNhanVien.SetItemChecked(i, isChecked);
            }

            // 4️⃣ Chuyển sang chế độ cập nhật
            isUpdating = true;
            btnSave.Text = "Cập nhật";
        }


        // Nút hoàn tác: reset form
        private void BtnUndo_Click(object sender, EventArgs e) => ClearForm();

        // ClearForm: bỏ chọn nhân viên, đặt các control về giá trị mặc định
        private void ClearForm()
        {
            for (int i = 0; i < lstNhanVien.Items.Count; i++) lstNhanVien.SetItemChecked(i, false);
            cbLyDo.SelectedIndex = 0;   // chọn item đầu (có thể là "-- Thêm lý do mới --" hoặc một lý do)
            txtNewLyDo.Text = "";
            txtAmount.Text = "";
            dtNgay.Value = DateTime.Now; // đặt lại ngày hiện tại
            btnSave.Text = "Thêm mới";
        }

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
                bll.XoaNhieuNhanVien_ThuongPhat(idsToDelete);

                // Tải lại dữ liệu sau khi xóa
                LoadData();

                MessageBox.Show("Đã xóa thành công các dòng được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
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
                    dtResult = bll.GetAll("Thưởng");
                    MessageBox.Show("Đang hiển thị danh sách khen thưởng của tất cả phòng ban.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dtResult = bll.GetAll("Thưởng", idPhongBan);

                    if (dtResult.Rows.Count == 0)
                    {
                        MessageBox.Show($"Không tìm thấy nhân viên được khen thưởng trong phòng ban '{tenPhongBan}'.",
                                        "Kết quả trống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Đã tìm thấy {dtResult.Rows.Count} nhân viên được khen thưởng trong phòng ban '{tenPhongBan}'.",
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
