using BLL;
using DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCBaoCaoHopDong : UserControl
    {
        private readonly string _connectionString;

        private readonly BLLPhongBan _dbContextPB;
        private readonly BLLHopDongLaoDong _dbContextHD;
        public UCBaoCaoHopDong(string conn)
        {
            InitializeComponent();
            _connectionString = conn;

            _dbContextPB = new BLLPhongBan(conn);
            _dbContextHD = new BLLHopDongLaoDong(conn);
        }

        private void UCBaoCaoHopDong_Load(object sender, EventArgs e)
        {
            var dsPhongBan = _dbContextPB.KtraDsPhongBan(); 
            dsPhongBan.Insert(0, new PhongBan() { id = 0, TenPhongBan = "Tất cả" });

            cmbPhongBan.DataSource = dsPhongBan;
            cmbPhongBan.ValueMember = "id";
            cmbPhongBan.DisplayMember = "TenPhongBan";

            var dsHopDong = _dbContextHD.KtraDsHopDongLaoDong().GroupBy(p => p.LoaiHopDong).Distinct().Select(p => new HopDongLaoDong
            {
                id = p.First().id,
                LoaiHopDong = p.Key

            }).ToList();    

            dsHopDong.Insert(0, new HopDongLaoDong() { id = 0, LoaiHopDong = "Tất cả" });

            cmbLoaiHopDong.DataSource = dsHopDong;
            cmbLoaiHopDong.ValueMember = "id";
            cmbLoaiHopDong.DisplayMember = "LoaiHopDong";

            reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.rptHopDongLaoDong.rdlc";
            reportViewer1.RefreshReport();
        }

        private DataTable GetDataFromSP(DateTime? tuNgay = null, DateTime? denNgay = null, string loaiHopDong = null, int? idNhanVien = null, int? idPhongBan = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_BaoCao_HopDongNhanVien", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                cmd.Parameters.AddWithValue("@DenNgay", denNgay);
                cmd.Parameters.AddWithValue("@IdPhongBan", idPhongBan);
                cmd.Parameters.AddWithValue("@IdNhanVien", idNhanVien);
                cmd.Parameters.AddWithValue("@LoaiHopDong", loaiHopDong);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            var phongBan = int.TryParse(cmbPhongBan?.SelectedValue.ToString(), out int idPhongBan);
            var hopDong = cmbLoaiHopDong?.Text ?? null;
            var dtBatDau = dtpTuNgay?.Value ?? null;
            var dtKetThuc = dtpDenNgay?.Value ?? null;

            DataTable dt = GetDataFromSP(dtBatDau, dtKetThuc, hopDong, null, idPhongBan);
        }
    }
}
