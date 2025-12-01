using DAL.DataContext;
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
        private readonly string _connectionString;
        private readonly PersonnelManagementDataContext _databaseContext;

        public DALDanhGiaNhanVien(string stringConnection)
        {
            _connectionString = stringConnection;
            _databaseContext = new PersonnelManagementDataContext(stringConnection);
        }

        public IQueryable<DTODanhGiaNhanVien> DanhSachDanhGiaNV() => _databaseContext.DanhGiaNhanViens.Select(p => new DTODanhGiaNhanVien
        {
            ID = p.id,
            DiemSo = p.DiemSo,
            NhanXet = p.NhanXet,
            NgayTao = p.ngayTao,
            IDNhanVien = p.idNhanVien,
            IDNguoiDanhGia = p.idNguoiDanhGia
        });

        // === Lấy danh sách đánh giá ===
        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT dg.id AS [Mã đánh giá],
                           nv.TenNhanVien AS [Nhân viên],
                           dg.ngayTao AS [Ngày đánh giá],
                           dg.DiemSo AS [Điểm số],
                           dg.NhanXet AS [Nhận xét]
                    FROM DanhGiaNhanVien dg
                    JOIN NhanVien nv ON dg.idNhanVien = nv.id";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // === Thêm mới ===
        public void Insert(DTODanhGiaNhanVien dg)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO DanhGiaNhanVien (DiemSo, NhanXet, ngayTao, idNhanVien, idNguoiDanhGia)
                    VALUES (@diem, @nhanxet, @ngay, @idnv, @idng)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@diem", dg.DiemSo);
                cmd.Parameters.AddWithValue("@nhanxet", dg.NhanXet);
                cmd.Parameters.AddWithValue("@ngay", dg.NgayTao);
                cmd.Parameters.AddWithValue("@idnv", dg.IDNhanVien);
                cmd.Parameters.AddWithValue("@idng", dg.IDNguoiDanhGia);
                cmd.ExecuteNonQuery();
            }
        }

        // === Cập nhật ===
        public void Update(DTODanhGiaNhanVien dg)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE DanhGiaNhanVien
                    SET DiemSo=@diem, NhanXet=@nhanxet, ngayTao=@ngay, idNhanVien=@idnv, idNguoiDanhGia=@idng
                    WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", dg.ID);
                cmd.Parameters.AddWithValue("@diem", dg.DiemSo);
                cmd.Parameters.AddWithValue("@nhanxet", dg.NhanXet);
                cmd.Parameters.AddWithValue("@ngay", dg.NgayTao);
                cmd.Parameters.AddWithValue("@idnv", dg.IDNhanVien);
                cmd.Parameters.AddWithValue("@idng", dg.IDNguoiDanhGia);
                cmd.ExecuteNonQuery();
            }
        }

        // === Xóa ===
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM DanhGiaNhanVien WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // === Thống kê điểm trung bình theo phòng ban ===
        public DataTable ThongKeTrungBinhTheoPhongBan(int thang, int nam)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
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
            using (SqlConnection conn = new SqlConnection(_connectionString))
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
            using (SqlConnection conn = new SqlConnection(_connectionString))
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
            using (SqlConnection conn = new SqlConnection(_connectionString))
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
