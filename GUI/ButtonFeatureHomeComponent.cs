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
    public partial class ButtonFeatureHomeComponent : UserControl
    {
        private readonly Panel _tpHome;
        public readonly string _idNhanVien, _conn;

        public ButtonFeatureHomeComponent(Panel tpHome, string idNhanVien, string conn)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            _conn = conn;
            _tpHome = tpHome;
            _idNhanVien = idNhanVien;
        }

        private void btnNghiPhep_Click(object sender, EventArgs e)
        {
            UCNghiPhep uc = new UCNghiPhep(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpHome);
        }

        private void btnDuyetNghi_Click(object sender, EventArgs e)
        {
            UCDuyetNghiPhep uc = new UCDuyetNghiPhep(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpHome);
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            XemThongTinCaNhan uc = new XemThongTinCaNhan(_idNhanVien, _tpHome, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpHome);
        }

        private void guna2TileButton5_Click(object sender, EventArgs e)
        {
            ucChamCongQuanLy uc = new ucChamCongQuanLy(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpHome);
        }

        private void guna2TileButton6_Click(object sender, EventArgs e)
        {
            ////ucChamCongQuanLy uc = new ucChamCongQuanLy(_idNhanVien, _conn);
            ////ChildUserControl(uc, _tpReport);

            //var main = this.ParentForm as Main;
            //main?.ShowUserControl("ucChamCongQuanLyHinh");
            //main.ChildFormComponent(_tpHome, "ButtonFeatureHomeComponent");
        }

        private void guna2TileButton7_Click(object sender, EventArgs e)
        {
            ucDuyenTuyenDung uc = new ucDuyenTuyenDung(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpHome);
        }
    }
}
