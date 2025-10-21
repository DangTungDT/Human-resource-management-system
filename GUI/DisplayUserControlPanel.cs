using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public static class DisplayUserControlPanel
    {
        // Form hien thi UserControl chung
        public static void ChildUserControl(UserControl user, Panel panel)
        {
            panel.Controls.Clear();
            panel.Controls.Add(user);

            user.BringToFront();
            user.Dock = DockStyle.Fill;
            user.Show();
        }
    }
}
