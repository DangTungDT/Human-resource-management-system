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
        public ButtonFeatureHomeComponent()
        {
            InitializeComponent();
        }


        private void btnNghiPhep_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            if (main != null)
            {
                main.ChildFormMain(new NghiPhep());
            }
        }

        private void btnDuyetNghi_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            if (main != null)
            {
                main.ChildFormMain(new DuyetNghiPhep());
            }
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            if (main != null)
            {
                main.ChildFormMain(new CapNhatMatkhau());
            }
        }
    }
}
