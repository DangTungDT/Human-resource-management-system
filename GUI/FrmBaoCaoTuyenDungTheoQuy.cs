using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace GUI
{
    public partial class FrmBaoCaoTuyenDungTheoQuy : Form
    {
        private readonly DataTable data;
        private readonly string quy;
        private readonly int nam;

        public FrmBaoCaoTuyenDungTheoQuy(DataTable dt, string quy, int nam)
        {
            InitializeComponent();
            this.data = dt;
            this.quy = quy;
            this.nam = nam;
        }

        private void FrmBaoCaoTuyenDungTheoQuy_Load(object sender, EventArgs e)
        {
            try
            {
                ReportDocument rpt = new ReportDocument();
                string path = Path.Combine(Application.StartupPath, @"GUI\rptTuyenDungTheoQuy.rpt");
                rpt.Load(path);
                rpt.SetDataSource(data);

                string tieuDe = string.IsNullOrEmpty(quy)
                    ? $"BÁO CÁO TUYỂN DỤNG NĂM {nam}"
                    : $"BÁO CÁO TUYỂN DỤNG THEO QUÝ {quy} NĂM {nam}";

                rpt.SetParameterValue("pTieuDe", tieuDe);
                rpt.SetParameterValue("pTenCongTy", "CÔNG TY TNHH ABC");

                crystalReportViewer1.ReportSource = rpt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message);
            }
        }
    }
    
}
