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
        public readonly UCNghiPhep _nghiPhep;
        public readonly UCDanhGiaHieuSuat _hieusuat;

        public TestGiaoDien(string id)
        {
            InitializeComponent();
            _hieusuat = new UCDanhGiaHieuSuat(id, ConnectionDB.TakeConnectionString());
        }

        public void DisplayInterface(UserControl uc)
        {
            pnMain.Controls.Clear();
            pnMain.Controls.Add(uc);

            uc.BringToFront();
            uc.Dock = DockStyle.Fill;
            uc.Show();
        }

        private void TestGiaoDien_Load(object sender, EventArgs e)
        {
            DisplayInterface(_hieusuat);
        }
    }
}
