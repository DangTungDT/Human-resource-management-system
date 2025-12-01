using CrystalDecisions.CrystalReports.Engine;
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
    public partial class FormBaoCaoChamCongCaNhan : Form
    {
        string _idNhanVien, _conn = "";

        public FormBaoCaoChamCongCaNhan(string idNhanVien, string conn)
        {
            _idNhanVien = idNhanVien;
            _conn = conn;
            InitializeComponent();
        }

        private void FormBaoCaoChamCongCaNhan_Load(object sender, EventArgs e)
        {
            // 1) Lấy dữ liệu từ stored procedure vào DataTable
            string connStr = _conn;
            var dt = new DataTable();

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("sp_ChamCongCaNhan", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdNhanVien", _idNhanVien);

                da.Fill(dt);
            }

            // 2) Load Report (.rpt) và gán DataTable làm DataSource
            var reportPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.FullName, "rptChamCongCaNhan.rpt");
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
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
