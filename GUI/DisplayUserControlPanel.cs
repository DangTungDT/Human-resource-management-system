using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static void LayKiTuSo(object sender)
            {
            var text = sender as Guna2TextBox;
            if (text == null) return;

            if (Regex.IsMatch(text.Text, @"\D"))
            {
                var index = Math.Max(0, text.SelectionStart - 1);
                text.Text = Regex.Replace(text.Text, "[^0-9]", "");
                text.SelectionStart = Math.Min(index, text.Text.Length);
            }
        }

        public static bool KiemTraDuLieuDauVao(ErrorProvider error, Control Toolbox)
        {
            bool ktra = true;
            error.Clear();

            foreach (var control in Toolbox.Controls)
            {
                if (control is Guna2ComboBox combobox && string.IsNullOrWhiteSpace(combobox.Text))
                {
                    error.SetError(combobox, $"'{combobox.Name.Substring(3)}' trống !");
                    ktra = false;
                }

                if (control is Guna2TextBox text && string.IsNullOrWhiteSpace(text.Text))
                {
                    error.SetError(text, $"Trống !");
                    //error.SetError(text, $"{text.Name.Substring(3)} trống !");
                    ktra = false;
                }
            }

            return ktra;
        }
    }

}
