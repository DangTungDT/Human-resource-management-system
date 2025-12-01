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
    public class DALKhenThuong
    {
        private readonly string _connectionString;
        public DALKhenThuong(string conn) => _connectionString = conn;

        // Lấy danh sách khen thưởng
        public DataTable GetAll(string idPhongBan = "")
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT tp.id, nv.TenNhanVien, pb.TenPhongBan, tp.tienThuongPhat AS [Số tiền thưởng], 
                           tp.lyDo AS [Lý do], nvtp.thangApDung AS [Ngày áp dụng]
                    FROM ThuongPhat tp
                    JOIN NhanVien_ThuongPhat nvtp ON tp.id = nvtp.idThuongPhat
                    JOIN NhanVien nv ON nvtp.idNhanVien = nv.id
                    JOIN PhongBan pb ON nv.idPhongBan = pb.id
                    WHERE tp.loai = N'Thưởng'";

                if (!string.IsNullOrEmpty(idPhongBan))
                    query += " AND pb.id = @idPB";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(idPhongBan))
                    cmd.Parameters.AddWithValue("@idPB", idPhongBan);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Thêm mới
        public void Insert(DTOKhenThuong kt)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO ThuongPhat (tienThuongPhat, loai, lyDo, idNguoiTao)
                    OUTPUT INSERTED.id
                    VALUES (@tien, N'Thưởng', @lydo, @idng)", conn);
                cmd.Parameters.AddWithValue("@tien", kt.SoTien);
                cmd.Parameters.AddWithValue("@lydo", kt.LyDo);
                cmd.Parameters.AddWithValue("@idng", kt.IdNguoiTao);
                int idThuongPhat = (int)cmd.ExecuteScalar();

                SqlCommand cmd2 = new SqlCommand(@"
                    INSERT INTO NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung)
                    VALUES (@idnv, @idtp, @ngay)", conn);
                cmd2.Parameters.AddWithValue("@idnv", kt.IdNhanVien);
                cmd2.Parameters.AddWithValue("@idtp", idThuongPhat);
                cmd2.Parameters.AddWithValue("@ngay", kt.NgayThuong);
                cmd2.ExecuteNonQuery();
            }
        }

        // Cập nhật
        public void Update(DTOKhenThuong kt)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // 1️⃣ Cập nhật thông tin trong bảng ThuongPhat
                SqlCommand cmd = new SqlCommand(@"
            UPDATE ThuongPhat
            SET lyDo = @lydo, 
                tienThuongPhat = @tien
            WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@lydo", kt.LyDo);
                cmd.Parameters.AddWithValue("@tien", kt.SoTien);
                cmd.Parameters.AddWithValue("@id", kt.Id);
                cmd.ExecuteNonQuery();

                // 2️⃣ Cập nhật ngày áp dụng trong bảng NhanVien_ThuongPhat
                SqlCommand cmd2 = new SqlCommand(@"
            UPDATE NhanVien_ThuongPhat
            SET thangApDung = @ngay
            WHERE idThuongPhat = @id", conn);
                cmd2.Parameters.AddWithValue("@ngay", kt.NgayThuong);
                cmd2.Parameters.AddWithValue("@id", kt.Id);
                cmd2.ExecuteNonQuery();
            }
        }

        // Xóa
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    DELETE FROM NhanVien_ThuongPhat WHERE idThuongPhat=@id;
                    DELETE FROM ThuongPhat WHERE id=@id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
