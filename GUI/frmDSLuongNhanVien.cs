using DAL;
using Microsoft.Reporting.WinForms;
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

namespace GUI
{
    public partial class frmDSLuongNhanVien : Form
    {
        private readonly string _con;
        public frmDSLuongNhanVien(string con)
        {
            InitializeComponent();
            _con = con;
        }

        private void frmDSLuongNhanVien_Load(object sender, EventArgs e)
        {
            DataTable dt = GetDataFromSP();

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.rptBaoCaoDSLuongNV.rdlc";


            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }

        private DataTable GetDataFromSP(string idNhanVien = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_con))
            using (SqlCommand cmd = new SqlCommand("sp_BaoCao_Luong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
