using BLL;
using CrystalDecisions.Web.HtmlReportRender;
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
    public partial class frmBaoCaoHopDong : Form
    {
        private string _idNhanVien { get; set; }
        private string _idPhongBan { get; set; }

        //"Data Source=LAPTOP-PNFFHRG1\\MSSQLSERVER01;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False"
        private readonly string _con ;

        private readonly BLLPhongBan _dbContextPB;
        private readonly BLLNhanVien _dbContextNV;
        private readonly BLLChucVu _dbContextCV;
        private readonly BLLChiTietLuong _dbContextCTL;
        private readonly BLLHopDongLaoDong _dbContextHD;
        public frmBaoCaoHopDong(string conn)
        {
            InitializeComponent();

            _con = conn;
            _dbContextCV = new BLLChucVu(conn);
            _dbContextPB = new BLLPhongBan(conn);
            _dbContextNV = new BLLNhanVien(conn);
            _dbContextCTL = new BLLChiTietLuong(conn);
            _dbContextHD = new BLLHopDongLaoDong(conn);
        }

        private void frmBaoCaoHopDong_Load(object sender, EventArgs e)
        {
            var dsPhongBan = _dbContextPB.KtraDsPhongBan();
            cmbPhongBan.DataSource = dsPhongBan;
            cmbPhongBan.ValueMember = "id";
            cmbPhongBan.DisplayMember = "TenPhongBan";
            cmbPhongBan.SelectedIndex = -1;

            var dsHopDong = _dbContextHD.KtraDsHopDongLaoDong().GroupBy(p => p.LoaiHopDong).Distinct().Select(p => new HopDongLaoDong
            {
                id = p.First().id,
                LoaiHopDong = p.Key

            }).ToList();

            cmbLoaiHopDong.DataSource = dsHopDong;
            cmbLoaiHopDong.ValueMember = "id";
            cmbLoaiHopDong.DisplayMember = "LoaiHopDong";
            cmbLoaiHopDong.SelectedIndex = -1;

            reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.rptHopDongLaoDong.rdlc";
            ReportDataSource rds = new ReportDataSource("DataSet1");
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            int? phongBan = null;
            if (int.TryParse(cmbPhongBan?.SelectedValue.ToString(), out int idPhongBan) && idPhongBan > 0)
            {
                phongBan = idPhongBan;
            }

            var hopDong = cmbLoaiHopDong.Text.Equals("Tất cả", StringComparison.OrdinalIgnoreCase) ? null : cmbLoaiHopDong.Text;
            var idNhanVien = string.IsNullOrEmpty(_idNhanVien) ? null : _idNhanVien;

            DataTable dt = GetDataFromSP(idNhanVien);

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.rptHopDongLaoDong.rdlc";

            // Tạo ReportDataSource
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }

        private DataTable GetDataFromSP(string idNhanVien = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_con))
            using (SqlCommand cmd = new SqlCommand("sp_BaoCao_HopDongNhanVien", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdNhanVien", idNhanVien);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        private void cmbPhongBan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _idPhongBan = cmbPhongBan.SelectedValue.ToString();
            cmbLoaiHopDong.SelectedIndex = -1;
            cmbNhanVienPB.SelectedIndex = -1;
        }

        private void cmbNhanVienPB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _idNhanVien = cmbNhanVienPB.SelectedValue.ToString();
        }

        private void cmbLoaiHopDong_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_idPhongBan)) return;

            var dsNhanVien = _dbContextNV.KtraDsNhanVien();
            var dsChucVu = _dbContextCV.LayDsChucVu();
            var dsChiTietLuong = _dbContextCTL.KtraDsChiTietLuong();

            var dsHopDongNV = _dbContextHD.KtraDsHopDongLaoDong().Join(dsNhanVien, hd => hd.idNhanVien, nv => nv.id, (hd, nv) => new { nv, hd })
                                                                .Where(p => p.hd.idNhanVien == p.nv.id
                                                                            && p.nv.idPhongBan == Convert.ToInt32(_idPhongBan)
                                                                            && dsChucVu.Select(s => s.id).Contains(p.nv.idChucVu)
                                                                            && dsChiTietLuong.Select(s => s.idNhanVien).Contains(p.nv.id)

                                                                ).Select(p => p.hd).ToList();


            if ((int)cmbLoaiHopDong.SelectedValue == 1)
            {
                dsHopDongNV = dsHopDongNV.Where(p => p.LoaiHopDong.Equals("hợp đồng thử việc", StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else dsHopDongNV = dsHopDongNV.Where(p => p.LoaiHopDong.Equals("hợp đồng nhân viên chính thức", StringComparison.OrdinalIgnoreCase)).ToList();

            dsNhanVien = dsNhanVien.Where(p => dsHopDongNV.Select(s => s.idNhanVien).Contains(p.id)).ToList();
            cmbNhanVienPB.DataSource = dsNhanVien;
            cmbNhanVienPB.ValueMember = "id";
            cmbNhanVienPB.DisplayMember = "TenNhanVien";
            cmbNhanVienPB.SelectedIndex = -1;
        }
    }
}
