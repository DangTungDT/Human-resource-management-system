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
    public partial class ucKyLuong : UserControl
    {
        public readonly string _idNhanVien, _conn;

        List<PayPeriod> list = new List<PayPeriod>()
        {
            new PayPeriod() {StartDate = new DateTime(2025, 7, 1), EndDate = new DateTime(2025, 8, 1), Status = "Hoàn tất" },
            new PayPeriod() {StartDate = new DateTime(2025, 8, 1), EndDate = new DateTime(2025, 9, 1), Status = "Hoàn tất" },
            new PayPeriod() {StartDate = new DateTime(2025, 9, 1), EndDate = new DateTime(2025, 10, 1), Status = "Hoàn tất" },
            new PayPeriod() {StartDate = new DateTime(2025, 10, 1), EndDate = new DateTime(2025, 11, 1), Status = "Đang chi trả" }
        };
        public ucKyLuong(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucKyLuong_Load(object sender, EventArgs e)
        {
            dgvPayPeriod.DataSource = list;
        }
    }
}
