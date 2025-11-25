using BLL;
using CrystalDecisions.ReportAppServer.DataDefModel;
using DAL;
using DocumentFormat.OpenXml.Office2013.Drawing.Chart;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class frmKhenThuongNhanVien : Form
    {

        private readonly string _con;
        private string _idNhanVien { get; set; }
        private bool _isSelectModel { get; set; } = false;

        private readonly BLLPhongBan _dbContextPB;
        private readonly BLLNhanVien _dbContextNV;
        private readonly BLLThuongPhat _dbContextTP;
        private readonly BLLNhanVien_ThuongPhat _dbContextNV_TP;
        public frmKhenThuongNhanVien(string conn)
        {
            InitializeComponent();

            _con = conn;
            _dbContextPB = new BLLPhongBan(conn);
            _dbContextNV = new BLLNhanVien(conn);
            _dbContextTP = new BLLThuongPhat(conn);
            _dbContextNV_TP = new BLLNhanVien_ThuongPhat(conn);
        }

        private void frmKhenThuongNhanVien_Load(object sender, EventArgs e)
        {
            cmbMoHinh.Items.Add("Cá nhân");
            //cmbMoHinh.Items.Add("Tập thể");

            var dsPhongBan = _dbContextPB.KtraDsPhongBan();
            cmbPhongBan.DataSource = dsPhongBan;
            cmbPhongBan.ValueMember = "id";
            cmbPhongBan.DisplayMember = "TenPhongBan";
            cmbPhongBan.SelectedIndex = -1;

            var dsNV_TP = _dbContextNV_TP.KtraDsNhanVien_ThuongPhat();
            cmbNam.DataSource = dsNV_TP.Where(p => p.thangApDung != null).Select(p => p.thangApDung.Value.Year).Distinct().ToList();
            cmbThang.DataSource = dsNV_TP.Where(p => p.thangApDung.Value.Date.Year == DateTime.Now.Year).Select(p => p.thangApDung.Value.Date.Month).Distinct().ToList();
        }
        private void cmbMoHinh_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _isSelectModel = false;
            if (cmbMoHinh.SelectedItem.ToString() == "Cá nhân")
            {
                _isSelectModel = true;
                cmbNhanVienPB.Enabled = true;
                cmbPhongBan.SelectedIndex = -1;

                cmbNhanVienPB.DataSource = new List<NhanVien>();
            }
            else
            {
                cmbNhanVienPB.Enabled = false;
                cmbNhanVienPB.SelectedIndex = -1;
            }
        }

        private void cmbPhongBan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!_isSelectModel) return;

            txtTimNV.Visible = false;
            cmbNhanVienPB.Visible = true;
            int idPhongBan = (int)cmbPhongBan.SelectedValue;

            int nam = cmbNam.SelectedValue != null ? (int)cmbNam.SelectedValue : 0;
            int thang = cmbThang.SelectedValue != null ? (int)cmbThang.SelectedValue : 0;

            var dsNhanVien = _dbContextNV.KtraDsNhanVien();
            var dsThuongPhat = _dbContextTP.CheckListThuongPhat();

            var dsNV_TP = _dbContextNV_TP.KtraDsNhanVien_ThuongPhat()
                                        .Join(dsThuongPhat, nv_tp => nv_tp.idThuongPhat, tp => tp.id, (nv_tp, tp) => new { nv_tp, tp })
                                        .Join(dsNhanVien, p => p.nv_tp.idNhanVien, nv => nv.id, (p, nv) => new { p.nv_tp, p.tp, nv })
                                        .Where(p => p.nv.id == p.nv_tp.idNhanVien
                                                    && p.tp.id == p.nv_tp.idThuongPhat
                                                    && p.nv.idPhongBan == idPhongBan
                                                    && p.tp.loai.Equals("Thưởng", StringComparison.OrdinalIgnoreCase)
                                                    && thang > 0 && nam > 0 ? p.nv_tp.thangApDung.Value.Date.Month == thang && p.nv_tp.thangApDung.Value.Date.Year == nam : false)

                                        .GroupBy(p => p.nv.TenNhanVien)
                                        .Select(p => new NhanVien { id = p.FirstOrDefault().nv.id, TenNhanVien = p.Key }).ToList();


            dsNV_TP.Insert(0, new NhanVien { id = "", TenNhanVien = "Tìm NV theo tên hoặc mã" });
            dsNhanVien = dsNV_TP;

            cmbNhanVienPB.DataSource = dsNhanVien;
            cmbNhanVienPB.ValueMember = "id";
            cmbNhanVienPB.DisplayMember = "TenNhanVien";
            cmbNhanVienPB.SelectedIndex = -1;
        }

        private void cmbNhanVienPB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _idNhanVien = cmbNhanVienPB.SelectedValue.ToString();
            if (cmbNhanVienPB.SelectedValue.ToString() == "")
            {
                txtTimNV.Visible = true;
                cmbNhanVienPB.Visible = false;
            }
            else cmbNhanVienPB.Visible = true;
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            if (cmbNhanVienPB.SelectedValue != null && cmbNhanVienPB.SelectedValue.ToString() == "")
            {
                _idNhanVien = txtTimNV.Text.Trim();
            }

            if (cmbPhongBan.SelectedValue == null) return;

            if (string.IsNullOrEmpty(_idNhanVien)) return;



            int? phongBan = null;
            if (int.TryParse(cmbPhongBan.SelectedValue.ToString(), out int idPhongBan) && idPhongBan > 0)
            {
                phongBan = idPhongBan;
            }

            int nam = cmbNam.SelectedValue != null ? (int)cmbNam.SelectedValue : 0;
            int thang = cmbThang.SelectedValue != null ? (int)cmbThang.SelectedValue : 0;

            var dsNhanVien = _dbContextNV.KtraDsNhanVien();
            var dsThuongPhat = _dbContextTP.CheckListThuongPhat();

            var nhanVien = _dbContextNV_TP.KtraDsNhanVien_ThuongPhat()
                                        .Join(dsThuongPhat, nv_tp => nv_tp.idThuongPhat, tp => tp.id, (nv_tp, tp) => new { nv_tp, tp })
                                        .Join(dsNhanVien, p => p.nv_tp.idNhanVien, nv => nv.id, (p, nv) => new { p.nv_tp, p.tp, nv })
                                        .Where(p => p.nv.id == p.nv_tp.idNhanVien
                                                    && p.tp.id == p.nv_tp.idThuongPhat
                                                    && p.nv.idPhongBan == idPhongBan
                                                    && p.tp.loai.Equals("Thưởng", StringComparison.OrdinalIgnoreCase)
                                                    && thang > 0 && nam > 0 ? p.nv_tp.thangApDung.Value.Date.Month == thang && p.nv_tp.thangApDung.Value.Date.Year == nam : false)

                                        .Select(p => new NhanVien { id = p.nv.id, TenNhanVien = p.nv.TenNhanVien })
                                        .FirstOrDefault(p => p.id == _idNhanVien || p.TenNhanVien == _idNhanVien);

            if (nhanVien == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên phù hợp !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dt = GetDataFromSP(nhanVien.id);

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.rptBaoCaoKhenThuongCaNhan.rdlc";


            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }

        private DataTable GetDataFromSP(string idNhanVien = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_con))
            using (SqlCommand cmd = new SqlCommand("sp_BaoCao_KhenThuong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdNhanVien", idNhanVien);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        private void cmbThang_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
    }
}
