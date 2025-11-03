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
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            //"Data Source=DESKTOP-6LE6PT2\\SQLEXPRESS;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False"
            pnMain.Controls.Clear();
            ucChamCongQuanLy uc = new ucChamCongQuanLy("TPNS000001", 1, "Data Source=DESKTOP-6LE6PT2\\SQLEXPRESS;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False");
            uc.Dock = DockStyle.Fill;
            pnMain.Controls.Add(uc);
        }
    }
}
