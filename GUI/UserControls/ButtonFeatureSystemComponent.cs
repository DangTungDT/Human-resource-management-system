using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ButtonFeatureSystemComponent : UserControl
    {
        private readonly Panel _tpSystem;
        public readonly string _idNhanVien, _conn;

        public ButtonFeatureSystemComponent(Panel tpSystem, string idNhanVien, string conn)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            _conn = conn;
            _tpSystem = tpSystem;
            _idNhanVien = idNhanVien;
        }


        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            DoiMatKhauRieng uc = new DoiMatKhauRieng(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpSystem);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // thoát chương trình
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát chương trình không?",
                                          "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            //quay lại form đăng nhập

            DialogResult result = MessageBox.Show("Bạn có chắc muốn quay lại màn hình đăng nhập không?",
                                          "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Tìm form cha (main form) của user control này
                Form parentForm = this.FindForm();

                // Ẩn form hiện tại
                parentForm.Hide();

                // Mở lại form đăng nhập
                FormLogin loginForm = new FormLogin();
                loginForm.Show();
            }
        }

        

    }
}
