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

        //private void FormTest_Load(object sender, EventArgs e)
        //{   //"TPCNTT0001","Data Source=DESKTOP-UM1I61K\\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False"
        //    //"Data Source=DESKTOP-6LE6PT2\\SQLEXPRESS;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False"
        //    pnMain.Controls.Clear();
        //    ucUngVien uc = new ucUngVien("", "Data Source=DESKTOP-UM1I61K\\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False");
        //    uc.Dock = DockStyle.Fill;
        //    pnMain.Controls.Add(uc);
        //}

        private void FormTest_Load(object sender, EventArgs e)
        {   //"TPCNTT0001","Data Source=DESKTOP-UM1I61K\\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False"
            //"Data Source=DESKTOP-6LE6PT2\\SQLEXPRESS;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False"
            pnMain.Controls.Clear();
            //ThongKeTuyenDung uc = new ThongKeTuyenDung("TPNS000001", "Data Source=DESKTOP-UM1I61K\\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False");
             TaoDanhGiaHieuSuat uc = new TaoDanhGiaHieuSuat("TPCNTT0022", "Data Source=DESKTOP-UM1I61K\\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False");
            
            uc.Dock = DockStyle.Fill;
            pnMain.Controls.Add(uc);
        }
    }
}
