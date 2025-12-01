using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Reports;

namespace GUI
{
    public partial class FrmReportKhenThuong : Form
    {
        private string connectionString = ConnectionDB.conn;
        private string maKT;

        public FrmReportKhenThuong(string maKT)
        {
            InitializeComponent();
            this.maKT = maKT;
        }

        private void FrmReportKhenThuong_Load(object sender, EventArgs e)
        {
            try
            {
                CrystalReportKhenThuong rpt = new CrystalReportKhenThuong();

                // Truyền tham số MaKT vào report
                rpt.SetParameterValue("MaKT", int.Parse(maKT));

                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message);
            }
        }
    }
}

