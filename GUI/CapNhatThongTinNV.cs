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

using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class CapNhatThongTinNV : UserControl
    {
        // ===== BIẾN TOÀN CỤC =====
        private Guna2TextBox txtName, txtAddress, txtEmail, txtQue;
        private Guna2ComboBox cbGender;
        private Guna2DateTimePicker dtDob;
        private Guna2Button btnSave, btnToggleView, btnAdd;
        private Guna2DataGridView dgv;

        private string connectionString = @"Data Source=DESKTOP-UM1I61K\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;";
        private string selectedId = null;
        private bool isViewingHidden = false;

        public CapNhatThongTinNV()
        {
            InitializeComponent();
            BuildUI();
            LoadDanhSachNhanVien(false);
        }

        // =======================
        // 1️⃣ DỰNG GIAO DIỆN
        // =======================
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

            // ==== INPUT CONTROL ====
            txtName = new Guna2TextBox() { PlaceholderText = "Họ tên", Dock = DockStyle.Fill };
            dtDob = new Guna2DateTimePicker() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Dock = DockStyle.Fill };
            cbGender = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            txtAddress = new Guna2TextBox() { PlaceholderText = "Địa chỉ", Dock = DockStyle.Fill };
            txtEmail = new Guna2TextBox() { PlaceholderText = "Email", Dock = DockStyle.Fill };
            txtQue = new Guna2TextBox() { PlaceholderText = "Quê quán", Dock = DockStyle.Fill };

            // ==== NÚT ====
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
            btnSave.MouseEnter += (s, e) => btnSave.FillColor = Color.SeaGreen;
            btnSave.MouseLeave += (s, e) => btnSave.FillColor = Color.MediumSeaGreen;

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
            btnToggleView.MouseEnter += (s, e) => btnToggleView.FillColor = Color.DodgerBlue;
            btnToggleView.MouseLeave += (s, e) => btnToggleView.FillColor = Color.SteelBlue;

            btnAdd = new Guna2Button()
            {
                Text = "➕ Thêm nhân viên mới",
                Size = new Size(200, 40),
                BorderRadius = 8,
                FillColor = Color.Goldenrod,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnAdd.Click += BtnAdd_Click;
            btnAdd.MouseEnter += (s, e) => btnAdd.FillColor = Color.DarkGoldenrod;
            btnAdd.MouseLeave += (s, e) => btnAdd.FillColor = Color.Goldenrod;

            // ==== LAYOUT FORM ====
            TableLayoutPanel layoutForm = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 7,
                Padding = new Padding(20),
                Height = 350
            };
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));

            layoutForm.Controls.Add(new Label() { Text = "Họ tên:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
            layoutForm.Controls.Add(txtName, 1, 0);
            layoutForm.Controls.Add(new Label() { Text = "Ngày sinh:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            layoutForm.Controls.Add(dtDob, 1, 1);
            layoutForm.Controls.Add(new Label() { Text = "Giới tính:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 2);
            layoutForm.Controls.Add(cbGender, 1, 2);
            layoutForm.Controls.Add(new Label() { Text = "Địa chỉ:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 3);
            layoutForm.Controls.Add(txtAddress, 1, 3);
            layoutForm.Controls.Add(new Label() { Text = "Quê quán:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 4);
            layoutForm.Controls.Add(txtQue, 1, 4);
            layoutForm.Controls.Add(new Label() { Text = "Email:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 5);
            layoutForm.Controls.Add(txtEmail, 1, 5);

            // ==== HÀNG NÚT ====
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 10, 0, 0)
            };
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnToggleView);
            layoutForm.Controls.Add(buttonPanel, 1, 6);

            // ==== DATAGRIDVIEW ====
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            dgv.CellClick += Dgv_CellClick;
            dgv.CellMouseEnter += Dgv_CellMouseEnter;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;

            // ==== CỘT ACTION ====
            DataGridViewImageColumn colAction = new DataGridViewImageColumn()
            {
                Name = "Action",
                HeaderText = "Thao tác",
                Image = Properties.Resources.delete,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 50
            };
            dgv.Columns.Add(colAction);
            dgv.Columns["Action"].DisplayIndex = dgv.Columns.Count - 1; // luôn ở cuối

            // ==== LAYOUT CHÍNH ====
            TableLayoutPanel layoutTotal = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Absolute, 380));
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            layoutTotal.Controls.Add(lblTitle, 0, 0);
            layoutTotal.Controls.Add(layoutForm, 0, 1);
            layoutTotal.Controls.Add(dgv, 0, 2);

            this.Controls.Add(layoutTotal);
        }

        // =======================
        // 2️⃣ LOAD DỮ LIỆU
        // =======================
        private void LoadDanhSachNhanVien(bool showHidden)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = showHidden
                    ? @"SELECT id AS [Mã NV], TenNhanVien AS [Họ tên], NgaySinh AS [Ngày sinh],
                               GioiTinh AS [Giới tính], DiaChi AS [Địa chỉ], Que AS [Quê quán],
                               Email AS [Email], N'Đã ẩn' AS [Trạng thái]
                        FROM NhanVien WHERE DaXoa = 1"
                    : @"SELECT id AS [Mã NV], TenNhanVien AS [Họ tên], NgaySinh AS [Ngày sinh],
                               GioiTinh AS [Giới tính], DiaChi AS [Địa chỉ], Que AS [Quê quán],
                               Email AS [Email], N'Đang làm việc' AS [Trạng thái]
                        FROM NhanVien WHERE DaXoa = 0";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;

                // cập nhật icon & màu trạng thái
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    var cell = row.Cells["Action"] as DataGridViewImageCell;
                    if (cell != null)
                        cell.Value = showHidden ? Properties.Resources.reset : Properties.Resources.delete;

                    string status = row.Cells["Trạng thái"].Value.ToString();
                    row.Cells["Trạng thái"].Style.ForeColor = (status == "Đang làm việc") ? Color.Green : Color.Red;
                }
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

        // =======================
        // 5️⃣ CẬP NHẬT THÔNG TIN
        // =======================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedId))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để cập nhật!", "Thông báo");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE NhanVien SET TenNhanVien=@Ten, NgaySinh=@Ngay, GioiTinh=@GT,
                                 DiaChi=@DC, Que=@Que, Email=@Email WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", txtName.Text);
                cmd.Parameters.AddWithValue("@Ngay", dtDob.Value);
                cmd.Parameters.AddWithValue("@GT", cbGender.Text);
                cmd.Parameters.AddWithValue("@DC", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Que", txtQue.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@id", selectedId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cập nhật thành công!");
            LoadDanhSachNhanVien(isViewingHidden);
        }

        // =======================
        // 6️⃣ THÊM NHÂN VIÊN MỚI
        // =======================
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            txtName.Text = txtAddress.Text = txtEmail.Text = txtQue.Text = "";
            cbGender.SelectedIndex = -1;
            dtDob.Value = DateTime.Now;
            selectedId = null;
            MessageBox.Show("Nhập thông tin mới vào các ô và bấm 'Lưu thay đổi' để thêm nhân viên mới.");
        }

        // =======================
        // 7️⃣ CHUYỂN CHẾ ĐỘ XEM
        // =======================
        private void BtnToggleView_Click(object sender, EventArgs e)
        {
            isViewingHidden = !isViewingHidden;
            btnToggleView.Text = isViewingHidden ? "📋 Hiển thị nhân viên đang làm việc" : "👁 Hiển thị nhân viên đã ẩn";
            LoadDanhSachNhanVien(isViewingHidden);
        }

        // =======================
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
    }
}

