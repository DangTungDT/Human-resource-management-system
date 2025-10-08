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
    public partial class ButtonFeatureReportComponent : UserControl
    {
        private Panel _tpHome;
        public ButtonFeatureReportComponent(Panel tpHome)
        {
            InitializeComponent();
            this.DoubleBuffered = false;
            _tpHome = tpHome;
        }

        private void btnChiTietLuong_Click(object sender, EventArgs e)
        {
            FormBaoCaoChamCongCaNhan form = new FormBaoCaoChamCongCaNhan();
            form.ShowDialog();
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
            FormBaoCaoTuyenDung form = new FormBaoCaoTuyenDung();
            form.ShowDialog();
        }

        private void btnKyLuat_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("BaoCaoHopDong");
            main.ChildFormComponent(_tpHome, "ButtonFeatureReportComponent");
        }

        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("BaoCaoKhenThuong");
            main.ChildFormComponent(_tpHome, "ButtonFeatureReportComponent");
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCReportDanhSachLuongPBan");
            main.ChildFormComponent(_tpHome, "ButtonFeatureReportComponent");
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCReportDanhSachKyLuat");
            main.ChildFormComponent(_tpHome, "ButtonFeatureReportComponent");
        }
    }
}
