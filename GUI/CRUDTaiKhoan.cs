using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace GUI
{
    public partial class CRUDTaiKhoan : UserControl
    {
        private Guna2TextBox txtUsername, txtPassword, txtSearchUser;
        private Guna2ComboBox cbNhanVien;
        private Guna2Button btnSave, btnUndo, btnClearFilter;
        private Guna2DataGridView dgv;
        private bool isPasswordVisible = false;

        private string connectionString;
        private int? selectedId = null;
        private DataTable dtTaiKhoan;

        public CRUDTaiKhoan(string conn)
        {
            connectionString = conn;
            InitializeComponent();
            BuildUI();
            LoadNhanVien();
            LoadTaiKhoan();
        }

        // ======================= GIAO DIỆN =======================
        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            // ===== TIÊU ĐỀ =====
            Label lblTitle = new Label()
            {
                Text = "QUẢN LÝ TÀI KHOẢN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ===== THANH TÌM KIẾM (1 hàng ngang, đều màu) =====
            FlowLayoutPanel searchPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(25, 10, 25, 10),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            Label lblSearchTitle = new Label()
            {
                Text = "🔍 TÌM KIẾM TÀI KHOẢN",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 6, 15, 0)
            };

            txtSearchUser = new Guna2TextBox()
            {
                PlaceholderText = "Nhập Họ tên, tài khoản hoặc mật khẩu để tìm...",
                Width = 300,
                BorderRadius = 6,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 0, 10, 0)
            };
            txtSearchUser.TextChanged += (s, e) => FilterTaiKhoan();

            btnClearFilter = new Guna2Button()
            {
                Text = "🔄 Làm mới",
                BorderRadius = 8,
                FillColor = Color.SteelBlue,
                ForeColor = Color.White,
                Height = 36,
                Width = 120,
                Anchor = AnchorStyles.Left
            };
            btnClearFilter.Click += (s, e) =>
            {
                txtSearchUser.Clear();
                FilterTaiKhoan();
            };
            btnClearFilter.MouseEnter += (s, e) => btnClearFilter.FillColor = Color.DodgerBlue;
            btnClearFilter.MouseLeave += (s, e) => btnClearFilter.FillColor = Color.SteelBlue;

            searchPanel.Controls.Add(lblSearchTitle);
            searchPanel.Controls.Add(txtSearchUser);
            searchPanel.Controls.Add(btnClearFilter);

            // ===== FORM NHẬP =====
            txtUsername = new Guna2TextBox() { PlaceholderText = "Tài khoản", Dock = DockStyle.Fill };
            txtPassword = new Guna2TextBox()
            {
                PlaceholderText = "Mật khẩu",
                Dock = DockStyle.Fill,
                PasswordChar = '●',
                UseSystemPasswordChar = true,
                IconRight = Properties.Resources.eyebrow,
                IconRightCursor = Cursors.Hand
            };
            txtPassword.IconRightClick += TxtPassword_IconRightClick;

            cbNhanVien = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

            btnSave = new Guna2Button()
            {
                Text = "➕ Thêm mới",
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Width = 140,
                Height = 40
            };
            btnSave.Click += BtnSave_Click;

            btnUndo = new Guna2Button()
            {
                Text = "↩️ Hoàn tác",
                BorderRadius = 8,
                FillColor = Color.Gray,
                ForeColor = Color.White,
                Width = 120,
                Height = 40
            };
            btnUndo.Click += BtnUndo_Click;

            TableLayoutPanel form = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(20, 10, 20, 10),
                ColumnCount = 3,
                RowCount = 4,
                AutoSize = true
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            form.Controls.Add(new Label() { Text = "Tài khoản:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
            form.Controls.Add(txtUsername, 1, 0);
            form.Controls.Add(new Label() { Text = "Mật khẩu:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            form.Controls.Add(txtPassword, 1, 1);
            form.Controls.Add(new Label() { Text = "Nhân viên:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 2);
            form.Controls.Add(cbNhanVien, 1, 2);

            FlowLayoutPanel btnPanel = new FlowLayoutPanel() { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            form.Controls.Add(btnPanel, 1, 3);

            // ===== DGV =====
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgv.CellClick += Dgv_CellClick;

            // ===== MAIN =====
            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            main.Controls.Add(lblTitle, 0, 0);
            main.Controls.Add(searchPanel, 0, 1);
            main.Controls.Add(form, 0, 2);
            main.Controls.Add(dgv, 0, 3);

            this.Controls.Add(main);
        }

        // ======================= HIỂN THỊ MẬT KHẨU =======================
        private void TxtPassword_IconRightClick(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            txtPassword.UseSystemPasswordChar = !isPasswordVisible;
            txtPassword.PasswordChar = isPasswordVisible ? '\0' : '●';
            txtPassword.IconRight = isPasswordVisible ? Properties.Resources.eye : Properties.Resources.eyebrow;
        }

        // ======================= LOAD DANH SÁCH NHÂN VIÊN =======================
        private void LoadNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT id, TenNhanVien 
                         FROM NhanVien 
                         WHERE DaXoa = 0 
                         ORDER BY TenNhanVien";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbNhanVien.DataSource = dt;
                cbNhanVien.DisplayMember = "TenNhanVien";
                cbNhanVien.ValueMember = "id";
                cbNhanVien.SelectedIndex = -1;
            }
        }

        // ======================= LOAD TÀI KHOẢN =======================
        private void LoadTaiKhoan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT tk.id AS [Mã], tk.taiKhoan AS [Tài khoản],
                                        tk.matKhau AS [Mật khẩu], nv.TenNhanVien AS [Nhân viên]
                                 FROM TaiKhoan tk
                                 LEFT JOIN NhanVien nv ON tk.idNhanVien = nv.id";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                dtTaiKhoan = new DataTable();
                da.Fill(dtTaiKhoan);
                dgv.DataSource = dtTaiKhoan;
            }

            if (!dgv.Columns.Contains("Xóa"))
            {
                DataGridViewImageColumn colDelete = new DataGridViewImageColumn()
                {
                    Name = "Xóa",
                    HeaderText = "Xóa",
                    Image = Properties.Resources.delete,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 50
                };
                dgv.Columns.Add(colDelete);
                dgv.Columns["Xóa"].DisplayIndex = dgv.Columns.Count - 1;
            }
        }

        // ======================= LỌC NHANH =======================
        private void FilterTaiKhoan()
        {
            if (dtTaiKhoan == null) return;

            string kw = txtSearchUser.Text.Replace("'", "''");
            string filter = string.IsNullOrWhiteSpace(kw)
                ? ""
                : $"[Tài khoản] LIKE '%{kw}%' OR [Mật khẩu] LIKE '%{kw}%' OR [Nhân viên] LIKE '%{kw}%'";
            dtTaiKhoan.DefaultView.RowFilter = filter;
        }

        // ======================= CRUD =======================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo");
                return;
            }

            string hashedPassword = HashPassword(txtPassword.Text); // 🟢 mã hóa mật khẩu

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;

                if (btnSave.Text.Contains("Thêm"))
                {
                    cmd = new SqlCommand(@"INSERT INTO TaiKhoan (taiKhoan, matKhau, idNhanVien)
                                   VALUES (@User, @Pass, @IdNV)", conn);
                }
                else
                {
                    if (selectedId == null)
                    {
                        MessageBox.Show("Không xác định được bản ghi cần cập nhật!", "Lỗi");
                        return;
                    }

                    cmd = new SqlCommand(@"UPDATE TaiKhoan SET taiKhoan=@User, matKhau=@Pass, idNhanVien=@IdNV WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", selectedId);
                }

                cmd.Parameters.AddWithValue("@User", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Pass", hashedPassword); // 🟢 lưu chuỗi mã hoá
                cmd.Parameters.AddWithValue("@IdNV", cbNhanVien.SelectedValue ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(btnSave.Text.Contains("Thêm") ? "✅ Đã thêm tài khoản mới!" : "✏️ Đã cập nhật tài khoản!");
            LoadTaiKhoan();
            ClearForm();
        }


        // ======================= CLICK DGV =======================
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xóa")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã"].Value);
                if (MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM TaiKhoan WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadTaiKhoan();
                }
                return;
            }

            selectedId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã"].Value);
            txtUsername.Text = dgv.Rows[e.RowIndex].Cells["Tài khoản"].Value.ToString();
            txtPassword.Text = dgv.Rows[e.RowIndex].Cells["Mật khẩu"].Value.ToString();
            cbNhanVien.Text = dgv.Rows[e.RowIndex].Cells["Nhân viên"].Value.ToString();

            btnSave.Text = "✏️ Cập nhật";
            btnSave.FillColor = Color.Orange;
        }

        // ======================= HOÀN TÁC =======================
        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hoàn tác dữ liệu đang nhập?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ClearForm();
        }

        private void ClearForm()
        {
            selectedId = null;
            txtUsername.Clear();
            txtPassword.Clear();
            cbNhanVien.SelectedIndex = -1;
            btnSave.Text = "➕ Thêm mới";
            btnSave.FillColor = Color.MediumSeaGreen;
            dgv.ClearSelection();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2")); // chuyển byte sang dạng hex
                return builder.ToString();
            }
        }


    }
}