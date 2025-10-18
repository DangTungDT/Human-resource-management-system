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
        private CheckedListBox clbNhanVien;           // CheckedListBox để chọn nhiều nhân viên
        private Guna2DateTimePicker dtNgay;           // DateTimePicker chọn ngày áp dụng
        private Guna2Button btnSave, btnUndo, btnSearch; // Nút lưu, hoàn tác, tìm kiếm
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
                Dock = DockStyle.Top,                   // chiếm trọn chiều ngang, cố định ở trên cùng
                Height = 65,                            // chiều cao của thanh tiêu đề
                Font = new Font("Segoe UI", 20, FontStyle.Bold),  // font chữ lớn, đậm
                ForeColor = Color.FromArgb(50, 70, 140),          // màu chữ xanh navy nhẹ
                TextAlign = ContentAlignment.MiddleCenter,        // căn giữa chữ
                BackColor = Color.White,                          // nền trắng cho tiêu đề
            };

            // === KHỐI PANEL TRẮNG CHỨA CÁC Ô NHẬP LIỆU ===
            Guna2Panel pnlFormCard = new Guna2Panel
            {
                BorderRadius = 12,                                // bo góc nhẹ
                FillColor = Color.White,                          // nền trắng
                ShadowDecoration = { Depth = 10, Enabled = true },// tạo đổ bóng nhẹ
                Dock = DockStyle.Fill,                            // chiếm toàn bộ phần còn lại trong layout
                Padding = new Padding(50, 30, 50, 30)             // khoảng cách giữa mép và nội dung
            };

            // === DÙNG TABLELAYOUT ĐỂ SẮP XẾP LABEL + INPUT THEO 2 CỘT ===
            TableLayoutPanel tlForm = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,         // chiếm hết panel chứa
                ColumnCount = 2,               // 2 cột: label bên trái, input bên phải
                RowCount = 6,                  // 6 dòng nhập liệu
                BackColor = Color.White,
                Padding = new Padding(0, 10, 0, 0)
            };
            tlForm.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160)); // cột label cố định 160px
            tlForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));  // cột input chiếm phần còn lại

            // Mỗi dòng cao 50px, riêng dòng nhân viên cao hơn (100px)
            for (int i = 0; i < 6; i++)
                tlForm.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 1 ? 100 : 50));

            // Hàm helper tạo label chuẩn
            Label MakeLabel(string text) => new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 10.5f),
                TextAlign = ContentAlignment.MiddleRight, // căn phải để sát ô nhập
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            // === CÁC CONTROL INPUT ===

            // Panel chứa combo box và nút tìm kiếm cạnh nhau
            FlowLayoutPanel pnlPhongBanSearch = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };

            // ComboBox chọn phòng ban
            cbPhongBan = new Guna2ComboBox
            {
                BorderRadius = 8,
                Size = new Size(250, 36),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };

            // Nút tìm kiếm
            Guna2Button btnSearch = new Guna2Button
            {
                Size = new Size(40, 36),
                Margin = new Padding(8, 0, 0, 0),
                Image = Properties.Resources.search, // icon mặc định
                ImageSize = new Size(18, 18),
                FillColor = Color.MediumSlateBlue,
                BorderRadius = 8,
                Cursor = Cursors.Hand // giúp hiển thị bàn tay khi hover
            };

            // Sự kiện click
            btnSearch.Click += btnTimKiem_Click;

            // Sự kiện hover vào
            btnSearch.MouseEnter += (s, e) =>
            {
                btnSearch.Image = Properties.Resources.magnifying_glass; // đổi icon khi hover
                btnSearch.FillColor = Color.SlateBlue; // tùy chọn: đổi màu nền
            };

            // Sự kiện rời chuột
            btnSearch.MouseLeave += (s, e) =>
            {
                btnSearch.Image = Properties.Resources.search; // trở lại icon cũ
                btnSearch.FillColor = Color.MediumSlateBlue; // khôi phục màu
            };

            // Thêm combo và nút vào panel
            pnlPhongBanSearch.Controls.Add(cbPhongBan);
            pnlPhongBanSearch.Controls.Add(btnSearch);

            // CheckedListBox để chọn nhiều nhân viên trong phòng ban
            clbNhanVien = new CheckedListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White
            };

            // ComboBox chọn lý do thưởng có sẵn
            cbLyDo = new Guna2ComboBox
            {
                BorderRadius = 8,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            cbLyDo.SelectedIndexChanged += CbLyDo_SelectedIndexChanged; // sự kiện đổi lý do

            // Textbox để nhập lý do mới (khi người dùng chọn “Thêm lý do mới”)
            txtNewLyDo = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập lý do mới...",
                Font = new Font("Segoe UI", 10),
                DisabledState = { FillColor = Color.FromArgb(245, 245, 245) } // màu nền khi disable
            };

            // Textbox nhập số tiền thưởng
            txtAmount = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập số tiền thưởng...",
                Font = new Font("Segoe UI", 10)
            };

            // DateTimePicker chọn ngày áp dụng thưởng
            dtNgay = new Guna2DateTimePicker
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy", // định dạng ngày VN
                Font = new Font("Segoe UI", 10)
            };

            // === THÊM CÁC CONTROL VÀO TABLELAYOUT ===
            // Thêm panel chứa cả 2 vào TableLayoutPanel (thay vì chỉ cbPhongBan)
            tlForm.Controls.Add(MakeLabel("Tìm theo phòng ban:"), 0, 0);
            tlForm.Controls.Add(pnlPhongBanSearch, 1, 0);
            tlForm.Controls.Add(MakeLabel("Nhân viên:"), 0, 1);
            tlForm.Controls.Add(clbNhanVien, 1, 1);
            tlForm.Controls.Add(MakeLabel("Lý do có sẵn:"), 0, 2);
            tlForm.Controls.Add(cbLyDo, 1, 2);
            tlForm.Controls.Add(MakeLabel("Hoặc lý do mới:"), 0, 3);
            tlForm.Controls.Add(txtNewLyDo, 1, 3);
            tlForm.Controls.Add(MakeLabel("Số tiền:"), 0, 4);
            tlForm.Controls.Add(txtAmount, 1, 4);
            tlForm.Controls.Add(MakeLabel("Ngày áp dụng:"), 0, 5);
            tlForm.Controls.Add(dtNgay, 1, 5);

            pnlFormCard.Controls.Add(tlForm); // đưa layout vào panel card

            // === KHU NÚT CHỨC NĂNG (Lưu / Hoàn tác) ===
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.RightToLeft, // nút Lưu nằm ngoài cùng bên phải
                Padding = new Padding(0, 10, 40, 0),
                BackColor = Color.Transparent,
                Height = 60
            };

            btnSave = new Guna2Button
            {
                Text = "💾 Lưu thưởng",
                Width = 180,
                Height = 45,
                BorderRadius = 8,
                FillColor = Color.FromArgb(45, 140, 90), // xanh lá đậm
                Font = new Font("Segoe UI Semibold", 10.5f),
                ForeColor = Color.White
            };
            btnSave.Click += btnSave_Click; // sự kiện lưu

            btnUndo = new Guna2Button
            {
                Text = "↩️ Hoàn tác",
                Width = 160,
                Height = 45,
                BorderRadius = 8,
                FillColor = Color.FromArgb(130, 130, 130),
                Font = new Font("Segoe UI Semibold", 10.5f),
                ForeColor = Color.White
            };
            btnUndo.Click += BtnUndo_Click;

            // Thêm 2 nút vào panel
            pnlButtons.Controls.Add(btnSave);
            pnlButtons.Controls.Add(btnUndo);

            // === TẠO DATAGRIDVIEW DƯỚI CÙNG ĐỂ HIỂN THỊ DANH SÁCH THƯỞNG ===
            dgv = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ColumnHeadersHeight = 38,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(230, 235, 245),
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(248, 250, 255) // màu xen kẽ hàng
                }
            };

            // Header của DataGridView: màu xanh đậm, chữ trắng
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(45, 85, 155),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Style cho ô dữ liệu
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(94, 148, 255), // màu khi chọn
                SelectionForeColor = Color.Black
            };

            dgv.CellClick += Dgv_CellClick;
            dgv.CellMouseEnter += Dgv_CellMouseEnter;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;
            DinhDangCotDgv();

            // === GHÉP TẤT CẢ THÀNH GIAO DIỆN CHÍNH ===
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };

            // Xác định kích thước từng vùng
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));   // tiêu đề
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 430));  // form nhập liệu
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70));    // vùng nút
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 350));   // vùng DataGridView

            // Thêm vào layout chính
            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(pnlFormCard, 0, 1);
            mainLayout.Controls.Add(pnlButtons, 0, 2);
            mainLayout.Controls.Add(dgv, 0, 3);

            // Đưa layout vào UserControl
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
            clbNhanVien.Items.Clear();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                // Lấy id và name từ row và thêm CLBItem (class helper) vào CheckedListBox
                string id = row["id"].ToString();
                string name = row["TenNhanVien"].ToString();
                clbNhanVien.Items.Add(new CLBItem(id, name));
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
            foreach (CLBItem item in clbNhanVien.CheckedItems)
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
                bll.SaveMulti("Thưởng",selectedNhanViens, lyDo,soTien, ngayApDung, idNguoiTao);
            }
            else
            {
                bll.UpdateMultiSmart("Thưởng",currentGroupId, selectedNhanViens, lyDo, soTien , ngayApDung);
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

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                // Xử lý xóa
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
                if (MessageBox.Show("Xóa nhóm thưởng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bll.Delete(id);
                    LoadData();
                }
                return;
            }

            // 1️⃣ Lấy dữ liệu dòng hiện tại
            currentGroupId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
            txtAmount.Text = dgv.Rows[e.RowIndex].Cells["SoTien"].Value.ToString();
            cbLyDo.Text = dgv.Rows[e.RowIndex].Cells["LyDo"].Value.ToString();
            dtNgay.Value = Convert.ToDateTime(dgv.Rows[e.RowIndex].Cells["NgayApDung"].Value);

            // 2️⃣ Lấy danh sách nhân viên thuộc nhóm này
            var empIds = bll.GetNhanVienByThuongPhatId(currentGroupId);

            // 3️⃣ Reset check
            for (int i = 0; i < clbNhanVien.Items.Count; i++)
            {
                var item = clbNhanVien.Items[i] as CLBItem;
                clbNhanVien.SetItemChecked(i, empIds.Contains(item.Id));
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
            for (int i = 0; i < clbNhanVien.Items.Count; i++) clbNhanVien.SetItemChecked(i, false);
            cbLyDo.SelectedIndex = 0;   // chọn item đầu (có thể là "-- Thêm lý do mới --" hoặc một lý do)
            txtNewLyDo.Text = "";
            txtAmount.Text = "";
            dtNgay.Value = DateTime.Now; // đặt lại ngày hiện tại
            btnSave.Text = "Thêm mới";
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
