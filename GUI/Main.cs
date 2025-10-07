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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void OpenChildControl(UserControl uc)
        {
            pnContent.Padding = new Padding(0, 10, 0, 0);
            pnContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnContent.Controls.Add(uc);
            uc.BringToFront();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            OpenChildControl(new ucDuyenTuyenDung());
        }
    }
}
