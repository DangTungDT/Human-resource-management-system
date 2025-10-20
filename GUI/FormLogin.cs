using BLL;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GUI
{
    public partial class FormLogin : Form
    {
        public string _getConnect { get; }
        public readonly BLLTaiKhoan _dbContextTK;
        public readonly string _conn = ConnectionDB.TakeConnectionString();

        public FormLogin(string idNhanVien)
        {
            InitializeComponent();

            _getConnect = _conn;
            _dbContextTK = new BLLTaiKhoan(_getConnect);
        }


        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập !");
                return;
            }

            var password = _dbContextTK.HashPassword(txtPassword.Text);
            var taiKhoan = _dbContextTK.KtraDuLieuTaiKhoan(txtUsername.Text, password);

            if (taiKhoan != null)
            {
                Main formMain = new Main(taiKhoan.id, _getConnect);
                formMain.Show(); this.Hide();
            }
            else MessageBox.Show("Tài khản nhân viên không tồn tại !");

        }
    }
}
