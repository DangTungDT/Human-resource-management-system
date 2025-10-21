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
    public partial class ucChiTietLuong : UserControl
    {
        public readonly string _idNhanVien, _conn;

        List<SalaryDetail> list = new List<SalaryDetail>()
        {
            new SalaryDetail() {PayPeriod = new DateTime(2025, 9, 1), NameEmployee = "Đặng Thanh Tùng", Status = "Hoàn tất"},
            new SalaryDetail() {PayPeriod = new DateTime(2025, 9, 1), NameEmployee = "Nguyễn Văn A", Status = "Hoàn tất"},
            new SalaryDetail() {PayPeriod = new DateTime(2025, 9, 1), NameEmployee = "Đặng Thành Tuấn", Status = "Hoàn tất"},
            new SalaryDetail() {PayPeriod = new DateTime(2025, 9, 1), NameEmployee = "Đặng Kim B", Status = "Hoàn tất"},
        };
        public ucChiTietLuong(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;
        }

        private void ucChiTietLuong_Load(object sender, EventArgs e)
        {
            dgvSalaryDetails.DataSource = list;
        }
    }
}
