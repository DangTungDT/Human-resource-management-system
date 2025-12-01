using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormBaoCaoDanhGia : Form
    {
        public FormBaoCaoDanhGia(DataTable dt)
        {
            InitializeComponent();
            LoadReport(dt);
        }

        private void LoadReport(DataTable dt)
        {
            try
            {
                // Khởi tạo ReportDocument
                ReportDocument rpt = new ReportDocument();

                // Đường dẫn file rpt
                string reportPath = Path.Combine(Application.StartupPath, "rptDanhGiaNhanVien.rpt");

                if (!File.Exists(reportPath))
                {
                    //Chạy ở local
                    reportPath = Path.Combine(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "Reports"), "rptDanhGiaNhanVien.rpt");
                }
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show("Không tìm thấy file báo cáo: " + reportPath);
                    return;
                }

                rpt.Load(reportPath);

                // Gắn datasource
                rpt.SetDataSource(dt);

                // Gán report cho CrystalReportViewer
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();

                // Optional: force print layout
                crystalReportViewer1.Zoom(100);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
