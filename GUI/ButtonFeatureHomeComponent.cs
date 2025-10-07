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
    }
}
