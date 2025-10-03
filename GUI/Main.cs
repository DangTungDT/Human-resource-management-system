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

        public void ChildFormComponent(UserControl user)
        {
            tpHome.Controls.Clear();
            tpHome.Controls.Add(user);

            user.BringToFront();
            user.Dock = DockStyle.Fill;
            user.Show();
        }

        public void ChildFormMain(UserControl user)
        {
            pnMain.Controls.Clear();
            pnMain.Controls.Add(user);

            user.BringToFront();
            user.Dock = DockStyle.Fill;
            user.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ChildFormComponent(new ButtonFeatureHomeComponent());
        }
    }
}
