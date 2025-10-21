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
    public partial class ucXemChamCong : UserControl
    {
        public readonly string _idNhanVien, _conn;

        List<Timekeeping> list = new List<Timekeeping>()
        {
            new Timekeeping() {Date = new DateTime(2025, 10, 1), Name = "Nguyễn Hữu Cảnh"},
            new Timekeeping() {Date = new DateTime(2025, 10, 2), Name = "Nguyễn Hữu Cảnh"},
            new Timekeeping() {Date = new DateTime(2025, 10, 3), Name = "Nguyễn Hữu Cảnh"},
            new Timekeeping() {Date = new DateTime(2025, 10, 4), Name = "Nguyễn Hữu Cảnh"},
            new Timekeeping() {Date = new DateTime(2025, 10, 5), Name = "Nguyễn Hữu Cảnh"},
            new Timekeeping() {Date = new DateTime(2025, 10, 6), Name = "Nguyễn Hữu Cảnh"},
        };
        public ucXemChamCong(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void ucXemChamCong_Load(object sender, EventArgs e)
        {
            dgvAttendance.DataSource = list;
        }
    }
}
