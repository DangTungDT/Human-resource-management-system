using BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class DoiMatKhau : Form
    {
        private readonly string _taiKhoan;
        private readonly string _connectionString;
        private readonly BLLTaiKhoan _taiKhoanBLL;

        public DoiMatKhau(string taiKhoan, string conn)
        {
            this._taiKhoan = taiKhoan;
            _connectionString = conn;
            _taiKhoanBLL = new BLLTaiKhoan(conn);
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Đổi mật khẩu";
            this.Size = new Size(400, 200);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblMatKhauMoi = new Label
            {
                Text = "Mật khẩu mới:",
                Location = new Point(50, 50),
                AutoSize = true
            };
            TextBox txtMatKhauMoi = new TextBox
            {
                Name = "txtMatKhauMoi",
                Location = new Point(150, 50),
                Width = 180,
                UseSystemPasswordChar = true
            };
            Button btnLuu = new Button
            {
                Text = "Lưu",
                Location = new Point(150, 100),
                Width = 180
            };

            btnLuu.Click += BtnLuu_Click;
            btnLuu.Cursor = Cursors.Hand;
            this.Controls.Add(lblMatKhauMoi);
            this.Controls.Add(txtMatKhauMoi);
            this.Controls.Add(btnLuu);
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string matKhauMoi = (this.Controls["txtMatKhauMoi"] as TextBox)?.Text.Trim();
            if (string.IsNullOrEmpty(matKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = _taiKhoanBLL.GetByTaiKhoan(_taiKhoan);
            if (dt.Rows.Count > 0)
            {
                int id = Convert.ToInt32(dt.Rows[0]["Mã"]);
                if (_taiKhoanBLL.UpdateMatKhau(id, matKhauMoi))
                {
                    MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 🔹 Đóng form đổi mật khẩu
                    this.Hide();

                    // 🔹 Mở lại form đăng nhập
                    FormLogin frmDangNhap = new FormLogin();
                    frmDangNhap.Show();

                    // 🔹 Sau khi mở form đăng nhập thì đóng form hiện tại
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
