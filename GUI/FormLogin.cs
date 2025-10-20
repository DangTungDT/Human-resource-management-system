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
        private readonly BLLTaiKhoan _dbContextTK;
        private readonly string _conn = ConnectionDB.TakeConnectionString();
        //private readonly string _conn = "Data Source=LAPTOP-PNFFHRG1\\MSSQLSERVER01;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False";

        public FormLogin(string idNhanVien)
        {
            InitializeComponent();

            _dbContextTK = new BLLTaiKhoan(_conn);
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
                Main formMain = new Main(taiKhoan.id, _conn);
                formMain.Show(); this.Hide();
            }
            else MessageBox.Show("Tài khản nhân viên không tồn tại !");

        }
    }
}
