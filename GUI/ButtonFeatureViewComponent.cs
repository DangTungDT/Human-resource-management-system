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
    public partial class ButtonFeatureViewComponent : UserControl
    {
        private Panel _tpView;
        public ButtonFeatureViewComponent(Panel tpView)
        {
            InitializeComponent();
            this.DoubleBuffered = false;
            _tpView = tpView;
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCDanhGiaHieuSuat");
            main.ChildFormComponent(_tpView, "ButtonFeatureViewComponent");
        }

        private void btnChiTietLuong_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCChiTietLuongCaNhan");
            main.ChildFormComponent(_tpView, "ButtonFeatureViewComponent");
        }

        private void btnKyLuat_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCXemKyLuat");
            main.ChildFormComponent(_tpView, "ButtonFeatureViewComponent");
        }

        private void btnThongTinCaNhan_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("XemThongTinCaNhan");
            main.ChildFormComponent(_tpView, "ButtonFeatureViewComponent");
        }

        private void btnNghiPhepCaNhan_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCNghiPhep");
            main.ChildFormComponent(_tpView, "ButtonFeatureViewComponent");
        }

        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("ucXemTuyenDung");
            main.ChildFormComponent(_tpView, "ButtonFeatureViewComponent");
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("ucXemChamCong");
            main.ChildFormComponent(_tpView, "ButtonFeatureViewComponent");
        }
    }
}
