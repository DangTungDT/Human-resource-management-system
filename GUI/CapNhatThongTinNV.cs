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

//namespace GUI
//{
//    public partial class CapNhatThongTinNV : UserControl
//    {
//        // ===== BIẾN TOÀN CỤC =====
//        private Guna2TextBox txtName, txtAddress, txtEmail, txtQue;
//        private Guna2ComboBox cbGender;
//        private Guna2DateTimePicker dtDob;
//        private Guna2Button btnSave, btnToggleView, btnAdd;
//        private Guna2DataGridView dgv;
//        private Guna2TextBox txtSearchName, txtSearchAddress, txtSearchQue, txtSearchEmail;
//        private Guna2ComboBox cbSearchGender;

//        private string connectionString = ConnectionDB.conn;
//        private string selectedId = null;
//        private bool isViewingHidden = false;

//        public CapNhatThongTinNV()
//        {
//            InitializeComponent();
//            BuildUI();
//            LoadDanhSachNhanVien(false);
//        }

//        // =======================
//        // 1️⃣ DỰNG GIAO DIỆN
//        // =======================
//        private void BuildUI()
//        {
//            this.Dock = DockStyle.Fill;
//            this.BackColor = Color.WhiteSmoke;

//            Label lblTitle = new Label()
//            {
//                Text = "CẬP NHẬT THÔNG TIN NHÂN VIÊN",
//                Font = new Font("Segoe UI", 14, FontStyle.Bold),
//                Dock = DockStyle.Top,
//                ForeColor = Color.DarkBlue,
//                Height = 40,
//                TextAlign = ContentAlignment.MiddleCenter
//            };

//            // ===== TIÊU ĐỀ THANH TÌM KIẾM =====
//            Label lblSearchTitle = new Label()
//            {
//                Text = "🔍 TÌM KIẾM NHÂN VIÊN",
//                Font = new Font("Segoe UI", 10, FontStyle.Bold),
//                ForeColor = Color.DimGray,
//                Dock = DockStyle.Top,
//                Height = 30,
//                TextAlign = ContentAlignment.MiddleLeft,
//                Padding = new Padding(30, 0, 0, 0),
//                BackColor = Color.FromArgb(240, 242, 245)
//            };

//            // ==== THANH TÌM KIẾM ==== 
//            FlowLayoutPanel searchPanel = new FlowLayoutPanel()
//            {
//                Dock = DockStyle.Top,
//                Padding = new Padding(20, 5, 20, 5),
//                AutoSize = true,
//                BackColor = Color.White
//            };

//            // 🔹 Khởi tạo control tìm kiếm
//            txtSearchName = new Guna2TextBox() { PlaceholderText = "🔍 Tìm theo họ tên", Width = 200 };
//            cbSearchGender = new Guna2ComboBox() { Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
//            cbSearchGender.Items.AddRange(new object[] { "", "Nam", "Nữ", "Khác" });

//            txtSearchAddress = new Guna2TextBox() { PlaceholderText = "Địa chỉ", Width = 200 };
//            txtSearchQue = new Guna2TextBox() { PlaceholderText = "Quê quán", Width = 200 };
//            txtSearchEmail = new Guna2TextBox() { PlaceholderText = "Email", Width = 200 };

//            searchPanel.Controls.AddRange(new Control[] {
//    txtSearchName, cbSearchGender, txtSearchAddress, txtSearchQue, txtSearchEmail
//});

//            // Sự kiện tìm kiếm
//            txtSearchName.TextChanged += (s, e) => FilterNhanVien();
//            cbSearchGender.SelectedIndexChanged += (s, e) => FilterNhanVien();
//            txtSearchAddress.TextChanged += (s, e) => FilterNhanVien();
//            txtSearchQue.TextChanged += (s, e) => FilterNhanVien();
//            txtSearchEmail.TextChanged += (s, e) => FilterNhanVien();

//            // ==== INPUT CONTROL ====
//            txtName = new Guna2TextBox() { PlaceholderText = "Họ tên", Dock = DockStyle.Fill };
//            dtDob = new Guna2DateTimePicker() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Dock = DockStyle.Fill };
//            cbGender = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
//            cbGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
//            txtAddress = new Guna2TextBox() { PlaceholderText = "Địa chỉ", Dock = DockStyle.Fill };
//            txtEmail = new Guna2TextBox() { PlaceholderText = "Email", Dock = DockStyle.Fill };
//            txtQue = new Guna2TextBox() { PlaceholderText = "Quê quán", Dock = DockStyle.Fill };

//            // ==== NÚT ====
//            btnSave = new Guna2Button()
//            {
//                Text = "💾 Lưu thay đổi",
//                Size = new Size(140, 40),
//                BorderRadius = 8,
//                FillColor = Color.MediumSeaGreen,
//                ForeColor = Color.White,
//                Cursor = Cursors.Hand
//            };
//            btnSave.Click += BtnSave_Click;
//            btnSave.MouseEnter += (s, e) => btnSave.FillColor = Color.SeaGreen;
//            btnSave.MouseLeave += (s, e) => btnSave.FillColor = Color.MediumSeaGreen;

//            btnToggleView = new Guna2Button()
//            {
//                Text = "👁 Hiển thị nhân viên đã ẩn",
//                Size = new Size(220, 40),
//                BorderRadius = 8,
//                FillColor = Color.SteelBlue,
//                ForeColor = Color.White,
//                Cursor = Cursors.Hand
//            };
//            btnToggleView.Click += BtnToggleView_Click;
//            btnToggleView.MouseEnter += (s, e) => btnToggleView.FillColor = Color.DodgerBlue;
//            btnToggleView.MouseLeave += (s, e) => btnToggleView.FillColor = Color.SteelBlue;

//            btnAdd = new Guna2Button()
//            {
//                Text = "➕ Thêm nhân viên mới",
//                Size = new Size(200, 40),
//                BorderRadius = 8,
//                FillColor = Color.Goldenrod,
//                ForeColor = Color.White,
//                Cursor = Cursors.Hand
//            };
//            btnAdd.Click += BtnAdd_Click;
//            btnAdd.MouseEnter += (s, e) => btnAdd.FillColor = Color.DarkGoldenrod;
//            btnAdd.MouseLeave += (s, e) => btnAdd.FillColor = Color.Goldenrod;

//            // ==== LAYOUT FORM ====
//            TableLayoutPanel layoutForm = new TableLayoutPanel()
//            {
//                Dock = DockStyle.Top,
//                ColumnCount = 2,
//                RowCount = 7,
//                Padding = new Padding(20),
//                Height = 350
//            };
//            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
//            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));

//            layoutForm.Controls.Add(new Label() { Text = "Họ tên:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
//            layoutForm.Controls.Add(txtName, 1, 0);
//            layoutForm.Controls.Add(new Label() { Text = "Ngày sinh:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
//            layoutForm.Controls.Add(dtDob, 1, 1);
//            layoutForm.Controls.Add(new Label() { Text = "Giới tính:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 2);
//            layoutForm.Controls.Add(cbGender, 1, 2);
//            layoutForm.Controls.Add(new Label() { Text = "Địa chỉ:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 3);
//            layoutForm.Controls.Add(txtAddress, 1, 3);
//            layoutForm.Controls.Add(new Label() { Text = "Quê quán:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 4);
//            layoutForm.Controls.Add(txtQue, 1, 4);
//            layoutForm.Controls.Add(new Label() { Text = "Email:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 5);
//            layoutForm.Controls.Add(txtEmail, 1, 5);

//            // ==== HÀNG NÚT ====
//            FlowLayoutPanel buttonPanel = new FlowLayoutPanel()
//            {
//                Dock = DockStyle.Fill,
//                FlowDirection = FlowDirection.LeftToRight,
//                Padding = new Padding(0, 10, 0, 0)
//            };
//            buttonPanel.Controls.Add(btnSave);
//            buttonPanel.Controls.Add(btnAdd);
//            buttonPanel.Controls.Add(btnToggleView);
//            layoutForm.Controls.Add(buttonPanel, 1, 6);

//            // ==== DATAGRIDVIEW ====
//            dgv = new Guna2DataGridView()
//            {
//                Dock = DockStyle.Fill,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
//                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
//                MultiSelect = false,
//                AllowUserToAddRows = false,
//                ReadOnly = true
//            };
//            dgv.CellClick += Dgv_CellClick;
//            dgv.CellMouseEnter += Dgv_CellMouseEnter;
//            dgv.CellMouseLeave += Dgv_CellMouseLeave;

//            // ==== CỘT ACTION ====
//            DataGridViewImageColumn colAction = new DataGridViewImageColumn()
//            {
//                Name = "Action",
//                HeaderText = "Thao tác",
//                Image = Properties.Resources.delete,
//                ImageLayout = DataGridViewImageCellLayout.Zoom,
//                Width = 50
//            };
//            dgv.Columns.Add(colAction);
//            dgv.Columns["Action"].DisplayIndex = dgv.Columns.Count - 1; // luôn ở cuối

//            // ==== LAYOUT CHÍNH ====
//            TableLayoutPanel layoutTotal = new TableLayoutPanel()
//            {
//                Dock = DockStyle.Fill,
//                RowCount = 5,
//                ColumnCount = 1
//            };
//            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));   // tiêu đề
//            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));   // nhãn tìm kiếm
//            layoutTotal.RowStyles.Add(new RowStyle(SizeType.AutoSize));       // thanh tìm kiếm
//            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Absolute, 380));  // form nhập
//            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Percent, 100));   // bảng

//            layoutTotal.Controls.Add(lblTitle, 0, 0);
//            layoutTotal.Controls.Add(lblSearchTitle, 0, 1);
//            layoutTotal.Controls.Add(searchPanel, 0, 2);
//            layoutTotal.Controls.Add(layoutForm, 0, 3);
//            layoutTotal.Controls.Add(dgv, 0, 4);

//            this.Controls.Add(layoutTotal);
//        }

//        // =======================
//        // 2️⃣ LOAD DỮ LIỆU
//        // =======================
//        private void LoadDanhSachNhanVien(bool showHidden)
//        {
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                string query = showHidden
//                    ? @"SELECT id AS [Mã NV], TenNhanVien AS [Họ tên], NgaySinh AS [Ngày sinh],
//                               GioiTinh AS [Giới tính], DiaChi AS [Địa chỉ], Que AS [Quê quán],
//                               Email AS [Email], N'Đã ẩn' AS [Trạng thái]
//                        FROM NhanVien WHERE DaXoa = 1"
//                    : @"SELECT id AS [Mã NV], TenNhanVien AS [Họ tên], NgaySinh AS [Ngày sinh],
//                               GioiTinh AS [Giới tính], DiaChi AS [Địa chỉ], Que AS [Quê quán],
//                               Email AS [Email], N'Đang làm việc' AS [Trạng thái]
//                        FROM NhanVien WHERE DaXoa = 0";

//                SqlDataAdapter da = new SqlDataAdapter(query, conn);
//                DataTable dt = new DataTable();
//                da.Fill(dt);
//                dgv.DataSource = dt;

//                // cập nhật icon & màu trạng thái
//                foreach (DataGridViewRow row in dgv.Rows)
//                {
//                    var cell = row.Cells["Action"] as DataGridViewImageCell;
//                    if (cell != null)
//                        cell.Value = showHidden ? Properties.Resources.reset : Properties.Resources.delete;

//                    string status = row.Cells["Trạng thái"].Value.ToString();
//                    row.Cells["Trạng thái"].Style.ForeColor = (status == "Đang làm việc") ? Color.Green : Color.Red;
//                }
//            }
//        }

//        // =======================
//        // 3️⃣ CLICK DGV
//        // =======================
//        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

//            string idNV = dgv.Rows[e.RowIndex].Cells["Mã NV"].Value.ToString();

//            if (dgv.Columns[e.ColumnIndex].Name == "Action")
//            {
//                if (isViewingHidden) KhoiPhucNhanVien(idNV);
//                else AnNhanVien(idNV);
//                return;
//            }

//            selectedId = idNV;
//            DataGridViewRow row = dgv.Rows[e.RowIndex];
//            txtName.Text = row.Cells["Họ tên"].Value.ToString();
//            cbGender.Text = row.Cells["Giới tính"].Value.ToString();
//            txtAddress.Text = row.Cells["Địa chỉ"].Value.ToString();
//            txtEmail.Text = row.Cells["Email"].Value.ToString();
//            txtQue.Text = row.Cells["Quê quán"].Value.ToString();
//            if (DateTime.TryParse(row.Cells["Ngày sinh"].Value.ToString(), out DateTime dob))
//                dtDob.Value = dob;
//        }

//        // =======================
//        // 4️⃣ ẨN / KHÔI PHỤC
//        // =======================
//        private void AnNhanVien(string id)
//        {
//            if (MessageBox.Show("Bạn có chắc muốn ẩn nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//            {
//                using (SqlConnection conn = new SqlConnection(connectionString))
//                {
//                    SqlCommand cmd = new SqlCommand("UPDATE NhanVien SET DaXoa = 1 WHERE id = @id", conn);
//                    cmd.Parameters.AddWithValue("@id", id);
//                    conn.Open();
//                    cmd.ExecuteNonQuery();
//                }
//                LoadDanhSachNhanVien(false);
//            }
//        }

//        private void KhoiPhucNhanVien(string id)
//        {
//            if (MessageBox.Show("Bạn có chắc muốn khôi phục nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//            {
//                using (SqlConnection conn = new SqlConnection(connectionString))
//                {
//                    SqlCommand cmd = new SqlCommand("UPDATE NhanVien SET DaXoa = 0 WHERE id = @id", conn);
//                    cmd.Parameters.AddWithValue("@id", id);
//                    conn.Open();
//                    cmd.ExecuteNonQuery();
//                }
//                LoadDanhSachNhanVien(true);
//            }

//        }

//        // =======================
//        // 5️⃣ CẬP NHẬT THÔNG TIN
//        // =======================
//        private void BtnSave_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(selectedId))
//            {
//                MessageBox.Show("Vui lòng chọn nhân viên để cập nhật!", "Thông báo");
//                return;
//            }

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                string query = @"UPDATE NhanVien SET TenNhanVien=@Ten, NgaySinh=@Ngay, GioiTinh=@GT,
//                                 DiaChi=@DC, Que=@Que, Email=@Email WHERE id=@id";
//                SqlCommand cmd = new SqlCommand(query, conn);
//                cmd.Parameters.AddWithValue("@Ten", txtName.Text);
//                cmd.Parameters.AddWithValue("@Ngay", dtDob.Value);
//                cmd.Parameters.AddWithValue("@GT", cbGender.Text);
//                cmd.Parameters.AddWithValue("@DC", txtAddress.Text);
//                cmd.Parameters.AddWithValue("@Que", txtQue.Text);
//                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
//                cmd.Parameters.AddWithValue("@id", selectedId);

//                conn.Open();
//                cmd.ExecuteNonQuery();
//            }

//            MessageBox.Show("Cập nhật thành công!");
//            LoadDanhSachNhanVien(isViewingHidden);
//        }

//        // =======================
//        // 6️⃣ THÊM NHÂN VIÊN MỚI
//        // =======================
//        private void BtnAdd_Click(object sender, EventArgs e)
//        {
//            txtName.Text = txtAddress.Text = txtEmail.Text = txtQue.Text = "";
//            cbGender.SelectedIndex = -1;
//            dtDob.Value = DateTime.Now;
//            selectedId = null;
//            MessageBox.Show("Nhập thông tin mới vào các ô và bấm 'Lưu thay đổi' để thêm nhân viên mới.");
//        }

//        // =======================
//        // 7️⃣ CHUYỂN CHẾ ĐỘ XEM
//        // =======================
//        private void BtnToggleView_Click(object sender, EventArgs e)
//        {
//            isViewingHidden = !isViewingHidden;
//            btnToggleView.Text = isViewingHidden ? "📋 Hiển thị nhân viên đang làm việc" : "👁 Hiển thị nhân viên đã ẩn";
//            LoadDanhSachNhanVien(isViewingHidden);
//        }

//        // =======================
//        // 8️⃣ HIỆU ỨNG ACTION
//        // =======================
//        private void Dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Action")
//            {
//                dgv.Cursor = Cursors.Hand;

//                // Lấy cell và icon hiện tại
//                var cell = (DataGridViewImageCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
//                var currentValue = cell.Value;

//                // Hiệu ứng hover riêng theo trạng thái hiển thị
//                if (isViewingHidden)
//                {
//                    // Đang ở chế độ xem nhân viên ẩn → icon là reset
//                    cell.Value = Properties.Resources.arrow;
//                }
//                else
//                {
//                    // Đang ở chế độ xem nhân viên đang làm việc → icon là delete
//                    cell.Value = Properties.Resources.trash;
//                }

//                // Đổi nền nhẹ
//                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Gainsboro;
//            }
//        }

//        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Action")
//            {
//                dgv.Cursor = Cursors.Default;

//                var cell = (DataGridViewImageCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

//                // Trả lại icon gốc
//                if (isViewingHidden)
//                {
//                    cell.Value = Properties.Resources.reset;
//                }
//                else
//                {
//                    cell.Value = Properties.Resources.delete;
//                }

//                // Trả nền về trắng
//                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
//            }
//        }

//        // Hàm lọc kết quả
//        private void FilterNhanVien()
//        {
//            if (dgv.DataSource == null) return;
//            DataTable dt = dgv.DataSource as DataTable;
//            if (dt == null) return;

//            string filter = "1=1";

//            if (!string.IsNullOrWhiteSpace(txtSearchName.Text))
//                filter += $" AND [Họ tên] LIKE '%{txtSearchName.Text.Replace("'", "''")}%'";

//            if (cbSearchGender.SelectedIndex > 0)
//                filter += $" AND [Giới tính] = '{cbSearchGender.Text}'";

//            if (!string.IsNullOrWhiteSpace(txtSearchAddress.Text))
//                filter += $" AND [Địa chỉ] LIKE '%{txtSearchAddress.Text.Replace("'", "''")}%'";

//            if (!string.IsNullOrWhiteSpace(txtSearchQue.Text))
//                filter += $" AND [Quê quán] LIKE '%{txtSearchQue.Text.Replace("'", "''")}%'";

//            if (!string.IsNullOrWhiteSpace(txtSearchEmail.Text))
//                filter += $" AND [Email] LIKE '%{txtSearchEmail.Text.Replace("'", "''")}%'";

//            (dgv.DataSource as DataTable).DefaultView.RowFilter = filter;
//        }
//    }
//}

namespace GUI
{
    public partial class CapNhatThongTinNV : UserControl
    {
        // ===== BIẾN TOÀN CỤC =====
        private Guna2TextBox txtName, txtAddress, txtEmail, txtQue;
        private Guna2ComboBox cbGender, cbPhongBan, cbChucVu;
        private Guna2DateTimePicker dtDob;
        private Guna2Button btnSave, btnToggleView, btnAdd;
        private Guna2DataGridView dgv;
        private Guna2TextBox txtSearchName, txtSearchAddress, txtSearchQue, txtSearchEmail;
        private Guna2ComboBox cbSearchGender;

        private string connectionString = ConnectionDB.conn;
        private string selectedId = null;
        private bool isViewingHidden = false;

        public CapNhatThongTinNV()
        {
            InitializeComponent();
            BuildUI();
            LoadDanhSachNhanVien(false);
            LoadPhongBan();
            LoadChucVu();
        }

        // ======================= DỰNG GIAO DIỆN =======================
        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            Label lblTitle = new Label()
            {
                Text = "CẬP NHẬT THÔNG TIN NHÂN VIÊN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Top,
                ForeColor = Color.DarkBlue,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ===== THANH TÌM KIẾM =====
            FlowLayoutPanel searchPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(20, 5, 20, 5),
                AutoSize = true,
                BackColor = Color.FromArgb(245, 247, 250)
            };

            txtSearchName = new Guna2TextBox() { PlaceholderText = "🔍 Họ tên", Width = 180 };
            cbSearchGender = new Guna2ComboBox() { Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
            cbSearchGender.Items.AddRange(new object[] { "", "Nam", "Nữ", "Khác" });
            txtSearchAddress = new Guna2TextBox() { PlaceholderText = "Địa chỉ", Width = 180 };
            txtSearchQue = new Guna2TextBox() { PlaceholderText = "Quê quán", Width = 180 };
            txtSearchEmail = new Guna2TextBox() { PlaceholderText = "Email", Width = 180 };

            searchPanel.Controls.AddRange(new Control[] {
                new Label() { Text="🔍 TÌM KIẾM:", AutoSize=true, Font=new Font("Segoe UI",10,FontStyle.Bold), ForeColor=Color.DimGray, Margin=new Padding(0,8,10,0) },
                txtSearchName, cbSearchGender, txtSearchAddress, txtSearchQue, txtSearchEmail
            });

            txtSearchName.TextChanged += (s, e) => FilterNhanVien();
            cbSearchGender.SelectedIndexChanged += (s, e) => FilterNhanVien();
            txtSearchAddress.TextChanged += (s, e) => FilterNhanVien();
            txtSearchQue.TextChanged += (s, e) => FilterNhanVien();
            txtSearchEmail.TextChanged += (s, e) => FilterNhanVien();

            // ===== INPUT =====
            txtName = new Guna2TextBox() { PlaceholderText = "Họ tên", Dock = DockStyle.Fill };
            dtDob = new Guna2DateTimePicker() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Dock = DockStyle.Fill };
            cbGender = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            txtAddress = new Guna2TextBox() { PlaceholderText = "Địa chỉ", Dock = DockStyle.Fill };
            txtEmail = new Guna2TextBox() { PlaceholderText = "Email", Dock = DockStyle.Fill };
            txtQue = new Guna2TextBox() { PlaceholderText = "Quê quán", Dock = DockStyle.Fill };
            cbPhongBan = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbChucVu = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

            btnSave = new Guna2Button()
            {
                Text = "💾 Lưu thay đổi",
                Size = new Size(140, 40),
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnSave.Click += BtnSave_Click;

            btnAdd = new Guna2Button()
            {
                Text = "↩️ Hoàn tác",
                Size = new Size(120, 40),
                BorderRadius = 8,
                FillColor = Color.Goldenrod,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnAdd.Click += BtnAdd_Click;

            btnToggleView = new Guna2Button()
            {
                Text = "👁 Hiển thị nhân viên đã ẩn",
                Size = new Size(220, 40),
                BorderRadius = 8,
                FillColor = Color.SteelBlue,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnToggleView.Click += BtnToggleView_Click;

            // ===== FORM =====
            TableLayoutPanel layoutForm = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                ColumnCount = 3,
                RowCount = 9,
                Padding = new Padding(20),
                Height = 420
            };
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 07));
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13));

            layoutForm.Controls.Add(new Label() { Text = "Họ tên:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
            layoutForm.Controls.Add(txtName, 1, 0);
            layoutForm.Controls.Add(new Label() { Text = "Ngày sinh:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            layoutForm.Controls.Add(dtDob, 1, 1);
            layoutForm.Controls.Add(new Label() { Text = "Giới tính:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 2);
            layoutForm.Controls.Add(cbGender, 1, 2);
            layoutForm.Controls.Add(new Label() { Text = "Phòng ban:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 3);
            layoutForm.Controls.Add(cbPhongBan, 1, 3);
            layoutForm.Controls.Add(new Label() { Text = "Chức vụ:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 4);
            layoutForm.Controls.Add(cbChucVu, 1, 4);
            layoutForm.Controls.Add(new Label() { Text = "Địa chỉ:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 5);
            layoutForm.Controls.Add(txtAddress, 1, 5);
            layoutForm.Controls.Add(new Label() { Text = "Quê quán:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 6);
            layoutForm.Controls.Add(txtQue, 1, 6);
            layoutForm.Controls.Add(new Label() { Text = "Email:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 7);
            layoutForm.Controls.Add(txtEmail, 1, 7);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnToggleView);
            layoutForm.Controls.Add(buttonPanel, 1, 8);

            // ===== DGV =====
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                ReadOnly = true
            };

            // ==== CỘT ACTION ==== 
            DataGridViewImageColumn colAction = new DataGridViewImageColumn()
            {
                Name = "Action",
                HeaderText = "Thao tác",
                Image = Properties.Resources.delete, // icon mặc định
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 50
            };
            dgv.Columns.Add(colAction);
            dgv.Columns["Action"].DisplayIndex = dgv.Columns.Count - 1;

            // gắn sự kiện
            dgv.CellClick += Dgv_CellClick;
            dgv.CellMouseEnter += Dgv_CellMouseEnter;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;

            // ===== MAIN =====
            TableLayoutPanel layoutTotal = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Absolute, 430));
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            layoutTotal.Controls.Add(lblTitle, 0, 0);
            layoutTotal.Controls.Add(searchPanel, 0, 1);
            layoutTotal.Controls.Add(layoutForm, 0, 2);
            layoutTotal.Controls.Add(dgv, 0, 3);

            this.Controls.Add(layoutTotal);
        }

        // ======================= LOAD DANH SÁCH =======================
        private void LoadDanhSachNhanVien(bool showHidden)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = showHidden
                    ? @"SELECT id AS [Mã NV], TenNhanVien AS [Họ tên], NgaySinh AS [Ngày sinh], GioiTinh AS [Giới tính],
                               DiaChi AS [Địa chỉ], Que AS [Quê quán], Email AS [Email], N'Đã ẩn' AS [Trạng thái]
                        FROM NhanVien WHERE DaXoa = 1"
                    : @"SELECT id AS [Mã NV], TenNhanVien AS [Họ tên], NgaySinh AS [Ngày sinh], GioiTinh AS [Giới tính],
                               DiaChi AS [Địa chỉ], Que AS [Quê quán], Email AS [Email], N'Đang làm việc' AS [Trạng thái]
                        FROM NhanVien WHERE DaXoa = 0";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        private void LoadPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT id, TenPhongBan FROM PhongBan", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbPhongBan.DataSource = dt;
                cbPhongBan.DisplayMember = "TenPhongBan";
                cbPhongBan.ValueMember = "id";
                cbPhongBan.SelectedIndex = -1;
            }
        }

        private void LoadChucVu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT id, TenChucVu FROM ChucVu", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbChucVu.DataSource = dt;
                cbChucVu.DisplayMember = "TenChucVu";
                cbChucVu.ValueMember = "id";
                cbChucVu.SelectedIndex = -1;
            }
        }

        // =======================
        // 3️⃣ CLICK DGV
        // =======================
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string idNV = dgv.Rows[e.RowIndex].Cells["Mã NV"].Value.ToString();

            if (dgv.Columns[e.ColumnIndex].Name == "Action")
            {
                if (isViewingHidden) KhoiPhucNhanVien(idNV);
                else AnNhanVien(idNV);
                return;
            }

            selectedId = idNV;
            DataGridViewRow row = dgv.Rows[e.RowIndex];
            txtName.Text = row.Cells["Họ tên"].Value.ToString();
            cbGender.Text = row.Cells["Giới tính"].Value.ToString();
            txtAddress.Text = row.Cells["Địa chỉ"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtQue.Text = row.Cells["Quê quán"].Value.ToString();
            if (DateTime.TryParse(row.Cells["Ngày sinh"].Value.ToString(), out DateTime dob))
                dtDob.Value = dob;
        }

        // =======================
        // 4️⃣ ẨN / KHÔI PHỤC
        // =======================
        private void AnNhanVien(string id)
        {
            if (MessageBox.Show("Bạn có chắc muốn ẩn nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE NhanVien SET DaXoa = 1 WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadDanhSachNhanVien(false);
            }
        }

        private void KhoiPhucNhanVien(string id)
        {
            if (MessageBox.Show("Bạn có chắc muốn khôi phục nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE NhanVien SET DaXoa = 0 WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadDanhSachNhanVien(true);
            }

        }

        // ======================= SINH MÃ NHÂN VIÊN =======================
        private string SinhMaNhanVien(string tenChucVu, string tenPhongBan)
        {
            // Lấy chữ cái đầu của từng từ trong chức vụ
            string prefixCV = new string(tenChucVu.Split(' ')
                                    .Where(s => !string.IsNullOrEmpty(s))
                                    .Select(s => s[0]).ToArray()).ToUpper();

            //// Lấy chữ cái đầu của từng từ trong phòng ban
            //string prefixPB = new string(tenPhongBan.Split(' ')
            //                        .Where(s => !string.IsNullOrEmpty(s))
            //                        .Select(s => s[0]).ToArray()).ToUpper();

            string prefix = prefixCV;

            // Xác định số ký tự còn lại cho phần số thứ tự
            int totalLength = 10;
            int numLength = totalLength - prefix.Length;
            if (numLength <= 0) throw new Exception("Prefix quá dài, không thể sinh mã 10 ký tự!");

            int nextNum = 1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 id FROM NhanVien WHERE id LIKE @pre + '%' ORDER BY id DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pre", prefix);
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string lastId = result.ToString();
                    string numStr = lastId.Substring(prefix.Length); // lấy phần số
                    int.TryParse(numStr, out nextNum);
                    nextNum++;
                }
            }

            // Sinh mã, zero-pad phần số để đủ độ dài
            string maNV = prefix + nextNum.ToString().PadLeft(numLength, '0');
            return maNV;
        }

        // ======================= LƯU / CẬP NHẬT =======================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cbPhongBan.SelectedIndex < 0 || cbChucVu.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn phòng ban và chức vụ!", "Thông báo");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;

                if (string.IsNullOrEmpty(selectedId))
                {
                    // === Thêm mới ===
                    string maNV = SinhMaNhanVien(cbChucVu.Text, cbPhongBan.Text);
                    cmd = new SqlCommand(@"INSERT INTO NhanVien (id, TenNhanVien, NgaySinh, GioiTinh, DiaChi, Que, Email, idChucVu, idPhongBan, DaXoa)
                                           VALUES (@id, @Ten, @Ngay, @GT, @DC, @Que, @Email, @idCV, @idPB, 0)", conn);
                    cmd.Parameters.AddWithValue("@id", maNV);
                }
                else
                {
                    // === Cập nhật ===
                    cmd = new SqlCommand(@"UPDATE NhanVien SET TenNhanVien=@Ten, NgaySinh=@Ngay, GioiTinh=@GT,
                                           DiaChi=@DC, Que=@Que, Email=@Email, idChucVu=@idCV, idPhongBan=@idPB
                                           WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", selectedId);
                }

                cmd.Parameters.AddWithValue("@Ten", txtName.Text);
                cmd.Parameters.AddWithValue("@Ngay", dtDob.Value);
                cmd.Parameters.AddWithValue("@GT", cbGender.Text);
                cmd.Parameters.AddWithValue("@DC", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Que", txtQue.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@idCV", cbChucVu.SelectedValue);
                cmd.Parameters.AddWithValue("@idPB", cbPhongBan.SelectedValue);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(string.IsNullOrEmpty(selectedId) ? "✅ Đã thêm nhân viên mới!" : "✏️ Đã cập nhật thông tin!");
            LoadDanhSachNhanVien(isViewingHidden);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            txtName.Text = txtAddress.Text = txtEmail.Text = txtQue.Text = "";
            cbGender.SelectedIndex = -1;
            cbPhongBan.SelectedIndex = -1;
            cbChucVu.SelectedIndex = -1;
            dtDob.Value = DateTime.Now;
            selectedId = null;
        }

        private void BtnToggleView_Click(object sender, EventArgs e)
        {
            isViewingHidden = !isViewingHidden;
            btnToggleView.Text = isViewingHidden ? "📋 Hiển thị nhân viên đang làm việc" : "👁 Hiển thị nhân viên đã ẩn";
            LoadDanhSachNhanVien(isViewingHidden);
        }

         //=======================
                // 8️⃣ HIỆU ỨNG ACTION
                // =======================
                private void Dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Action")
            {
                dgv.Cursor = Cursors.Hand;

                // Lấy cell và icon hiện tại
                var cell = (DataGridViewImageCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var currentValue = cell.Value;

                // Hiệu ứng hover riêng theo trạng thái hiển thị
                if (isViewingHidden)
                {
                    // Đang ở chế độ xem nhân viên ẩn → icon là reset
                    cell.Value = Properties.Resources.arrow;
                }
                else
                {
                    // Đang ở chế độ xem nhân viên đang làm việc → icon là delete
                    cell.Value = Properties.Resources.trash;
                }

                // Đổi nền nhẹ
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Gainsboro;
            }
        }

        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Action")
            {
                dgv.Cursor = Cursors.Default;

                var cell = (DataGridViewImageCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Trả lại icon gốc
                if (isViewingHidden)
                {
                    cell.Value = Properties.Resources.reset;
                }
                else
                {
                    cell.Value = Properties.Resources.delete;
                }

                // Trả nền về trắng
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
            }
        }

        private void FilterNhanVien()
        {
            if (dgv.DataSource == null) return;
            DataTable dt = dgv.DataSource as DataTable;
            if (dt == null) return;

            string filter = "1=1";
            if (!string.IsNullOrWhiteSpace(txtSearchName.Text))
                filter += $" AND [Họ tên] LIKE '%{txtSearchName.Text.Replace("'", "''")}%'";
            if (cbSearchGender.SelectedIndex > 0)
                filter += $" AND [Giới tính] = '{cbSearchGender.Text}'";
            if (!string.IsNullOrWhiteSpace(txtSearchAddress.Text))
                filter += $" AND [Địa chỉ] LIKE '%{txtSearchAddress.Text.Replace("'", "''")}%'";
            if (!string.IsNullOrWhiteSpace(txtSearchQue.Text))
                filter += $" AND [Quê quán] LIKE '%{txtSearchQue.Text.Replace("'", "''")}%'";
            if (!string.IsNullOrWhiteSpace(txtSearchEmail.Text))
                filter += $" AND [Email] LIKE '%{txtSearchEmail.Text.Replace("'", "''")}%'";
            (dgv.DataSource as DataTable).DefaultView.RowFilter = filter;
        }
    }
}

