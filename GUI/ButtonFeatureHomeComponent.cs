using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ButtonFeatureHomeComponent : UserControl
    {
        private Panel _tpHome;
        public ButtonFeatureHomeComponent(Panel tpHome)
        {
            InitializeComponent();
            this.DoubleBuffered = false;
            _tpHome = tpHome;
        }
        private void LoadForm(Form form) => form.Show();
       
        private void btnNghiPhep_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCNghiPhep");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void btnDuyetNghi_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCDuyetNghiPhep");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCCapNhatMatKhau");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void btnHopDong_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCHopDong");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("XemThongTinCaNhan");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("TaoDanhGiaHieuSuat");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void btnTaoKyLuat_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("TaoKyLuat");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("TaoKhenThuong");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void guna2TileButton5_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("ucChamCongQuanLy");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void guna2TileButton6_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("ucChamCongQuanLyHinh");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void guna2TileButton7_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("ucDuyenTuyenDung");
            main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }
    }
}
