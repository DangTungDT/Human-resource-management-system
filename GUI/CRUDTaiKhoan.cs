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

using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class CRUDTaiKhoan : UserControl
    {
        private Guna2TextBox txtUsername, txtPassword;
        private Guna2ComboBox cbNhanVien;
        private Guna2Button btnSave, btnUndo;
        private Guna2DataGridView dgv;
        private bool isPasswordVisible = false;

        private string connectionString = @"Data Source=DESKTOP-UM1I61K\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;";
        private int? selectedId = null;

        public CRUDTaiKhoan()
        {
            InitializeComponent();
            BuildUI();
            LoadNhanVien();
            LoadTaiKhoan();
        }

        // ======================= DỰNG GIAO DIỆN =======================
        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            Label lblTitle = new Label()
            {
                Text = "QUẢN LÝ TÀI KHOẢN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ==== INPUT ====
            txtUsername = new Guna2TextBox()
            {
                PlaceholderText = "Tài khoản",
                Dock = DockStyle.Fill
            };

            txtPassword = new Guna2TextBox()
            {
                PlaceholderText = "Mật khẩu",
                Dock = DockStyle.Fill,
                PasswordChar = '●',
                UseSystemPasswordChar = true,
                IconRight = Properties.Resources.eyebrow, // 👁‍🗨 icon mặc định ẩn
                IconRightCursor = Cursors.Hand
            };
            txtPassword.IconRightClick += TxtPassword_IconRightClick;

            cbNhanVien = new Guna2ComboBox()
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // ==== NÚT ====
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

            // ==== FORM INPUT ====
            TableLayoutPanel form = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                ColumnCount = 3,
                RowCount = 5,
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

            FlowLayoutPanel btnPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill
            };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            form.Controls.Add(btnPanel, 1, 3);

            // ==== DGV ====
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowTemplate = { Height = 35 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgv.CellClick += Dgv_CellClick;

            // ==== MAIN ====
            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 300));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.Controls.Add(form, 0, 0);
            main.Controls.Add(dgv, 0, 1);

            this.Controls.Add(main);
            this.Controls.Add(lblTitle);
        }

        // ======================= ICON CLICK (ẨN/HIỆN MẬT KHẨU) =======================
        private void TxtPassword_IconRightClick(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                txtPassword.PasswordChar = '\0';
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.IconRight = Properties.Resources.eye; // 👁 hiện
            }
            else
            {
                txtPassword.PasswordChar = '●';
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.IconRight = Properties.Resources.eyebrow; // 👁‍🗨 ẩn
            }
        }

        // ======================= LOAD NHÂN VIÊN =======================
        private void LoadNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, TenNhanVien FROM NhanVien WHERE DaXoa = 0";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbNhanVien.DataSource = dt;
                cbNhanVien.DisplayMember = "TenNhanVien";
                cbNhanVien.ValueMember = "id";
            }
        }

        // ======================= LOAD TÀI KHOẢN =======================
        private void LoadTaiKhoan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT tk.id AS [Mã tài khoản], tk.taiKhoan AS [Tên đăng nhập], 
                                 tk.matKhau AS [Mật khẩu], nv.TenNhanVien AS [Nhân viên]
                                 FROM TaiKhoan tk
                                 LEFT JOIN NhanVien nv ON tk.idNhanVien = nv.id";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }

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
            }
        }

        // ======================= THÊM / CẬP NHẬT =======================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo");
                return;
            }

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
                cmd.Parameters.AddWithValue("@Pass", txtPassword.Text);
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

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã tài khoản"].Value);
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

            // Click chọn để sửa
            selectedId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã tài khoản"].Value);
            txtUsername.Text = dgv.Rows[e.RowIndex].Cells["Tên đăng nhập"].Value.ToString();
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
    }
}
