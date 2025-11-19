using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALDanhGiaNhanVien
    {
        private readonly string connectionString;
        private readonly DALNhanVien _dbContextNV;
        private readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALDanhGiaNhanVien(string stringConnection)
        {
            connectionString = stringConnection;
            _dbContextNV = new DALNhanVien(stringConnection);
            _dbContext = new PersonnelManagementDataContextDataContext(stringConnection);
        }

        public IQueryable<DTODanhGiaNhanVien> DanhSachDanhGiaNV() => _dbContext.DanhGiaNhanViens.Select(p => new DTODanhGiaNhanVien
        {
            ID = p.id,
            DiemSo = p.DiemSo,
            NhanXet = p.NhanXet,
            NgayTao = p.ngayTao,
            IDNhanVien = p.idNhanVien,
            IDNguoiDanhGia = p.idNguoiDanhGia
        });

        // === Lấy danh sách đánh giá ===
        // Lấy tất cả đánh giá kèm thông tin nhân viên. Nếu thang>0 thì tính DiemChuyenCan theo tháng đó.
        public DataTable GetAll(int thang = 0, int nam = 0, int? pb =0)
        {
            // Subquery tính số lần missing (không chấm công và không có phép đã duyệt) trong tháng
            string sql = @"
                SELECT dg.id AS ID,
                       dg.idNhanVien AS IDNhanVien,
                       nv.TenNhanVien AS TenNhanVien,
                       -- DiemChuyenCan sẽ lấy từ bảng (nếu null thì 5) OR tính động
                       ISNULL(dg.DiemChuyenCan, 5) AS DiemChuyenCanStored,
                       ISNULL(dg.DiemNangLuc, 5) AS DiemNangLucStored,
                       dg.DiemSo,
                       dg.NhanXet,
                       dg.ngayTao AS NgayTao,
                       dg.idNguoiDanhGia AS IDNguoiDanhGia,
                       ISNULL(m.Misses, 0) AS Misses
                FROM DanhGiaNhanVien dg
                LEFT JOIN NhanVien nv ON nv.id = dg.idNhanVien
                LEFT JOIN (
                    SELECT cc.idNhanVien, COUNT(*) AS Misses
                    FROM ChamCong cc
                    WHERE MONTH(cc.NgayChamCong) = @Thang
                      AND YEAR(cc.NgayChamCong) = @Nam
                      AND cc.GioVao IS NULL
                      AND cc.GioRa IS NULL
                      AND NOT EXISTS (
                            SELECT 1 FROM NghiPhep np
                            WHERE np.idNhanVien = cc.idNhanVien
                              AND np.TrangThai = N'Đã duyệt'
                              AND cc.NgayChamCong BETWEEN np.NgayBatDau AND np.NgayKetThuc
                      )
                    GROUP BY cc.idNhanVien
                ) m ON m.idNhanVien = dg.idNhanVien
                ORDER BY nv.TenNhanVien;
            ";

            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                // Nếu không pass tháng/năm thì set 0 -> subquery không có tháng hợp lệ.
                // Thay vì bẻ code SQL dài, ta yêu cầu caller truyền tháng/năm hợp lý.
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);

                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAllPB(string idDangNhap, int thang, int nam, string searchTen = null, int? pb = null, int? chucVu = null)
        {
            string spName = "sp_GetDanhGiaNhanVien";
            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(spName, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDangNhap", idDangNhap);
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);
                cmd.Parameters.AddWithValue("@SearchTen", (object)searchTen ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SearchPhongBan", (object)pb ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SearchChucVu", (object)chucVu ?? DBNull.Value);

                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }




        public int Insert(DTODanhGiaNhanVien dg)
        {
            string sql = @"
                INSERT INTO DanhGiaNhanVien (DiemSo, DiemChuyenCan, DiemNangLuc, NhanXet, ngayTao, idNhanVien, idNguoiDanhGia)
                VALUES (@DiemSo, @DiemChuyenCan, @DiemNangLuc, @NhanXet, @NgayTao, @IDNhanVien, @IDNguoiDanhGia);
                SELECT SCOPE_IDENTITY();
            ";
            using (var cn = new SqlConnection(connectionString  ))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@DiemSo", dg.DiemSo);
                cmd.Parameters.AddWithValue("@DiemChuyenCan", dg.DiemChuyenCan);
                cmd.Parameters.AddWithValue("@DiemNangLuc", dg.DiemNangLuc);
                cmd.Parameters.AddWithValue("@NhanXet", (object)dg.NhanXet ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayTao", dg.NgayTao);
                cmd.Parameters.AddWithValue("@IDNhanVien", dg.IDNhanVien);
                cmd.Parameters.AddWithValue("@IDNguoiDanhGia", dg.IDNguoiDanhGia);

                cn.Open();
                var res = cmd.ExecuteScalar();
                return Convert.ToInt32(res);
            }
        }

        public bool Update(DTODanhGiaNhanVien dg)
        {
            string sql = @"
                UPDATE DanhGiaNhanVien
                SET DiemSo = @DiemSo,
                    DiemChuyenCan = @DiemChuyenCan,
                    DiemNangLuc = @DiemNangLuc,
                    NhanXet = @NhanXet,
                    ngayTao = @NgayTao,
                    idNhanVien = @IDNhanVien,
                    idNguoiDanhGia = @IDNguoiDanhGia
                WHERE id = @ID;
            ";
            using (var cn = new SqlConnection(  connectionString    ))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@DiemSo", dg.DiemSo);
                cmd.Parameters.AddWithValue("@DiemChuyenCan", dg.DiemChuyenCan);
                cmd.Parameters.AddWithValue("@DiemNangLuc", dg.DiemNangLuc);
                cmd.Parameters.AddWithValue("@NhanXet", (object)dg.NhanXet ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayTao", dg.NgayTao);
                cmd.Parameters.AddWithValue("@IDNhanVien", dg.IDNhanVien);
                cmd.Parameters.AddWithValue("@IDNguoiDanhGia", dg.IDNguoiDanhGia);
                cmd.Parameters.AddWithValue("@ID", dg.ID);

                cn.Open();
                int aff = cmd.ExecuteNonQuery();
                return aff > 0;
            }
        }

        public bool Delete(int id)
        {
            string sql = "DELETE FROM DanhGiaNhanVien WHERE id = @ID";
            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                cn.Open();
                int aff = cmd.ExecuteNonQuery();
                return aff > 0;
            }
        }

        // === Thống kê điểm trung bình theo phòng ban ===
        public DataTable ThongKeTrungBinhTheoPhongBan(int thang, int nam)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                                SELECT pb.TenPhongBan,
                                       AVG(CAST(dg.DiemSo AS DECIMAL(5,2))) AS DiemTrungBinh
                                FROM DanhGiaNhanVien dg
                                JOIN NhanVien nv ON dg.idNhanVien = nv.id
                                JOIN PhongBan pb ON nv.idPhongBan = pb.id
                                WHERE MONTH(dg.ngayTao) = @thang AND YEAR(dg.ngayTao) = @nam
                                GROUP BY pb.TenPhongBan
                                ORDER BY pb.TenPhongBan";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@thang", thang);
                da.SelectCommand.Parameters.AddWithValue("@nam", nam);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // === Báo cáo chi tiết theo tháng / năm / phòng ban ===
        public DataTable BaoCaoDanhGiaChiTiet(int thang, int nam, string idPhongBan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                                SELECT nv.TenNhanVien AS [Nhân viên],
                                       pb.TenPhongBan AS [Phòng ban],
                                       dg.ngayTao AS [Ngày đánh giá],
                                       dg.DiemSo AS [Điểm số],
                                       dg.NhanXet AS [Nhận xét]
                                FROM DanhGiaNhanVien dg
                                JOIN NhanVien nv ON dg.idNhanVien = nv.id
                                JOIN PhongBan pb ON nv.idPhongBan = pb.id
                                WHERE MONTH(dg.ngayTao) = @thang AND YEAR(dg.ngayTao) = @nam";

                if (!string.IsNullOrEmpty(idPhongBan))
                    query += " AND pb.id = @idPhongBan";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@thang", thang);
                da.SelectCommand.Parameters.AddWithValue("@nam", nam);
                if (!string.IsNullOrEmpty(idPhongBan))
                    da.SelectCommand.Parameters.AddWithValue("@idPhongBan", idPhongBan);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // === Lấy danh sách đánh giá của 1 nhân viên theo tháng ===
        public List<DTODanhGiaNhanVien> GetByEmployeeAndMonth(string maNV, int month, int year)
        {
            List<DTODanhGiaNhanVien> list = new List<DTODanhGiaNhanVien>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT * FROM DanhGiaNhanVien
            WHERE idNhanVien = @maNV 
              AND MONTH(ngayTao) = @month 
              AND YEAR(ngayTao) = @year";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@maNV", maNV);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new DTODanhGiaNhanVien
                    {
                        ID = Convert.ToInt32(dr["id"]),
                        DiemSo = Convert.ToInt32(dr["DiemSo"]),
                        NhanXet = dr["NhanXet"].ToString(),
                        NgayTao = Convert.ToDateTime(dr["ngayTao"]),
                        IDNhanVien = dr["idNhanVien"].ToString(),
                        IDNguoiDanhGia = dr["idNguoiDanhGia"].ToString()
                    });
                }
            }
            return list;
        }

        // === Lấy thống kê trung bình theo tháng ===
        public DataTable GetMonthlySummary(int month, int year)
        {
            return ThongKeTrungBinhTheoPhongBan(month, year);
        }

        // === Lấy thống kê trung bình theo từng tháng trong năm ===
        public DataTable GetYearlySummary(int year)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT MONTH(dg.ngayTao) AS Thang,
                   AVG(CAST(dg.DiemSo AS DECIMAL(5,2))) AS DiemTrungBinh
            FROM DanhGiaNhanVien dg
            WHERE YEAR(dg.ngayTao) = @year
            GROUP BY MONTH(dg.ngayTao)
            ORDER BY Thang";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@year", year);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

    }
}
