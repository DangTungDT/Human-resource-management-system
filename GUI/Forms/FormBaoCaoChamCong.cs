using BLL;
using CrystalDecisions.CrystalReports.Engine;
using DAL;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormBaoCaoChamCong : Form
    {
        BLLPhongBan _bllPhongBan;
        string _idNhanVien;
        string _conn;
        public FormBaoCaoChamCong(string idNhanVien, string conn)
        {
            _bllPhongBan = new BLLPhongBan(conn);
            _idNhanVien = idNhanVien;
            _conn = conn;
            InitializeComponent();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void FormBaoCaoChamCong_Load(object sender, EventArgs e)
        {
            cmbThang.SelectedItem = "1";
            cmbNam.SelectedItem = "2025";

            cmbPhongBan.DataSource = _bllPhongBan.GetAllPhongBan();
            cmbPhongBan.ValueMember = "Mã phòng ban";
            cmbPhongBan.DisplayMember = "Tên phòng ban";
            cmbPhongBan.SelectedIndex = 1;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // 1) Lấy dữ liệu từ stored procedure vào DataTable
            string connStr = _conn;
            var dt = new DataTable();

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("sp_ChamCong", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Thang", cmbThang.SelectedItem);
                cmd.Parameters.AddWithValue("@Nam", cmbNam.SelectedItem);
                cmd.Parameters.AddWithValue("@IdPhongBan", cmbPhongBan.SelectedValue);

                da.Fill(dt);
            }

            // 2) Load Report (.rpt) và gán DataTable làm DataSource
            var reportPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.FullName, "report", "crpChamCong.rpt");
            var rpt = new ReportDocument();
            try
            {
                rpt.Load(reportPath);
            }
            catch
            {
                reportPath = Path.Combine(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "Reports"), "rptChamCongCaNhan.rpt");
                rpt.Load(reportPath);
            }
            rpt.SetDataSource(dt);

            // 3) Gán report cho CrystalReportViewer và làm mới hiển thị
            crystalReportViewerChamCong.ReportSource = rpt;
            crystalReportViewerChamCong.Refresh();
        }
    }
}
