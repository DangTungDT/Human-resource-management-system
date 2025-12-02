using BLL;
using DocumentFormat.OpenXml.Drawing.Charts;
using Guna.UI2.WinForms;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GUI
{
    public partial class FormLogin : Form
    {
        private readonly BLLTaiKhoan _dbContextTK;
        private readonly string _conn = ConnectionDB.TakeConnectionString();
        
        public FormLogin()
        {
            InitializeComponent();
            _dbContextTK = new BLLTaiKhoan(_conn);
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUsername.MaxLength = 21;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            if (DisplayUserControlPanel.KiemTraDuLieuDauVao(errorProvider1, this))
            {
                errorProvider1.Clear();

                var password = _dbContextTK.HashPassword(txtPassword.Text.Trim());
                var taiKhoan = _dbContextTK.KtraDuLieuTaiKhoan(txtUsername.Text.Trim(), password);

                var checkMatkhau = _dbContextTK.DsTaiKhoan().FirstOrDefault(p => p.matKhau == password);
                var checkUerName = _dbContextTK.DsTaiKhoan().FirstOrDefault(p => p.taiKhoan1 == txtUsername.Text.Trim());

                if (checkUerName == null)
                {
                    errorProvider1.SetError(txtUsername, "Mã tài khoản không hợp lệ !");
                    return;
                }
                else if (checkMatkhau == null)
                {
                    errorProvider1.SetError(txtPassword, "Mật khẩu không hợp lệ !");
                    return;
                }
                else if (taiKhoan != null)
                {
                    Main formMain = new Main(taiKhoan.id, _conn);
                    formMain.Show(); this.Hide();
                }
                else MessageBox.Show("Tài khản nhân viên không tồn tại !");
            }
        }

        private void lblQuenMatKhau_Click(object sender, EventArgs e)
        {
            QuenMatKhau formQuenMatKhau = new QuenMatKhau(_conn);
            formQuenMatKhau.ShowDialog();
            this.Hide();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtUsername.Text.Length > 20)
            {
                errorProvider1.SetError(txtUsername, "Tên tài khoản quá dài !");
                return;
            }

        }

        private void FormLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar.Equals(Key.Enter))
            {
                MessageBox.Show("");
            }
        }

        private void FormLogin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
