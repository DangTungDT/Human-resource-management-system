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
    public partial class ucUngVien : UserControl
    {
        public readonly string _idNhanVien, _conn;

        List<Candidate> list = new List<Candidate>()
        {
            new Candidate() {AppliCationDate = new DateTime(2025, 10, 1), Name = "Lý Văn T", Status = "Đang duyệt"},
            new Candidate() {AppliCationDate = new DateTime(2025, 10, 1), Name = "Lý Văn A", Status = "Đang duyệt"},
            new Candidate() {AppliCationDate = new DateTime(2025, 10, 1), Name = "Lý Văn D", Status = "Đang duyệt"},
            new Candidate() {AppliCationDate = new DateTime(2025, 10, 1), Name = "Lý Văn E", Status = "Đang duyệt"},
            new Candidate() {AppliCationDate = new DateTime(2025, 10, 1), Name = "Lý Văn Q", Status = "Đang duyệt"}
        };
        public ucUngVien(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;
        }

        private void ucUngVien_Load(object sender, EventArgs e)
        {
            dgvCandidate.DataSource = list;
        }
    }
}
