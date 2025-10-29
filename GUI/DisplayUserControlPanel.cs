using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
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

        public static void ChangeFontFamily(Control parent, string newFontFamily)
        {
            parent.Font = new Font(newFontFamily, parent.Font.Size, FontStyle.Regular);

            foreach (Control child in parent.Controls)
            {
                ChangeFontFamily(child, newFontFamily);
            }
        }
    }
    
}   
