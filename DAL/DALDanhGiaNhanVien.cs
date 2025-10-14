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
        public readonly PersonnelManagementDataContextDataContext _dbContext;
        private readonly string connectionString;
        public DALDanhGiaNhanVien(string stringConnection)
        {
            _dbContext = new PersonnelManagementDataContextDataContext(stringConnection);
            connectionString = stringConnection;
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
        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
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
            using (SqlConnection conn = new SqlConnection(connectionString))
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
            using (SqlConnection conn = new SqlConnection(connectionString))
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM DanhGiaNhanVien WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
