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

namespace GUI
{
    public partial class FrmReportHopDong : Form
    {
        private string connectionString = @"Data Source=DESKTOP-UM1I61K\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;";
        private string maNhanVien; // nhân viên được chọn

        // ✅ Constructor mặc định (để designer chạy được)
        public FrmReportHopDong()
        {
            InitializeComponent();
        }

        // ✅ Constructor có tham số (dùng khi bạn truyền id nhân viên)
        public FrmReportHopDong(string idNhanVien) : this() // gọi lại constructor trên
        {
            maNhanVien = idNhanVien;
        }

        private void FrmReportHopDong_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = GetHopDongNhanVien(maNhanVien);

                ReportDocument report = new ReportDocument();
                report.Load(Application.StartupPath + @"\rptHopDongLaoDong.rpt");
                report.SetDataSource(dt);

                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message);
            }
        }

        private DataTable GetHopDongNhanVien(string idNhanVien)
        {
            string query = @"
            SELECT 
                hd.id AS MaHD,
                nv.TenNhanVien,
                pb.TenPhongBan,
                cv.TenChucVu,
                hd.LoaiHopDong,
                hd.NgayKy,
                hd.NgayBatDau,
                hd.NgayKetThuc,
                cv.luongCoBan,
                hd.MoTa
            FROM HopDongLaoDong hd
            JOIN NhanVien nv ON hd.idNhanVien = nv.id
            JOIN PhongBan pb ON nv.idPhongBan = pb.id
            JOIN ChucVu cv ON nv.idChucVu = cv.id
            WHERE nv.id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", idNhanVien);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
