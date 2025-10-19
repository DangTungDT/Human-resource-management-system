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
    public partial class TestGiaoDien : Form
    {
        public string conn = "Data Source=LAPTOP-PNFFHRG1\\MSSQLSERVER01;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False";
        public readonly UCNghiPhep _nghiPhep;
        public readonly UCDanhGiaHieuSuat _hieusuat;

        public TestGiaoDien(string idNhanvien)
        {
            InitializeComponent();

            _nghiPhep = new UCNghiPhep(idNhanvien, conn);
            _hieusuat = new UCDanhGiaHieuSuat(idNhanvien, conn);
            //_nghiPhep = new UCNghiPhep(idNhanvien, ConnectionDB.TakeConnectionString());
        }

        private void DisplayInterface(UserControl uc)
        {
            pnMain.Controls.Clear();
            pnMain.Controls.Add(uc);

            uc.BringToFront();
            uc.Dock = DockStyle.Fill;
            uc.Show();
        }

        private void TestGiaoDien_Load(object sender, EventArgs e) => DisplayInterface(_nghiPhep);
    }
}
