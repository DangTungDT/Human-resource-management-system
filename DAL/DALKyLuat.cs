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
    public class DALKyLuat
    {
        private readonly string connectionString;

        public DALKyLuat(string conn)
        {
            connectionString = conn;
        }

        // Lấy danh sách kỷ luật
        public DataTable GetAll(string idPhongBan = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT tp.id, nv.TenNhanVien, pb.TenPhongBan, tp.loai AS HinhThuc, tp.lyDo
                    FROM ThuongPhat tp
                    JOIN NhanVien_ThuongPhat nvtp ON tp.id = nvtp.idThuongPhat
                    JOIN NhanVien nv ON nvtp.idNhanVien = nv.id
                    JOIN PhongBan pb ON nv.idPhongBan = pb.id
                    WHERE tp.loai IN (N'Khiển trách', N'Cảnh cáo', N'Đình chỉ', N'Sa thải', N'Phạt', N'Kỷ luật')";

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

        // Lấy danh sách phòng ban (để hiển thị combobox)
        public DataTable GetDepartments()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, TenPhongBan FROM PhongBan";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // === Thêm mới ===
        public void Insert(DTOKyLuat kl)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Thêm vào ThuongPhat
                SqlCommand cmd = new SqlCommand(@"
                INSERT INTO ThuongPhat (tienThuongPhat, loai, lyDo, idNguoiTao)
                OUTPUT INSERTED.id
                VALUES (0, @loai, @lydo, @idng)", conn);
                cmd.Parameters.AddWithValue("@loai", kl.Loai);
                cmd.Parameters.AddWithValue("@lydo", kl.LyDo);
                cmd.Parameters.AddWithValue("@idng", kl.IdNguoiTao);
                int idThuongPhat = (int)cmd.ExecuteScalar();

                // Liên kết nhân viên
                SqlCommand cmd2 = new SqlCommand(@"
                INSERT INTO NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung)
                VALUES (@idnv, @idtp, @ngay)", conn);
                cmd2.Parameters.AddWithValue("@idnv", kl.IdNhanVien);
                cmd2.Parameters.AddWithValue("@idtp", idThuongPhat);
                cmd2.Parameters.AddWithValue("@ngay", kl.NgayKyLuat);
                cmd2.ExecuteNonQuery();
            }
        }

        // === Cập nhật ===
        public void Update(DTOKyLuat kl)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                UPDATE ThuongPhat 
                SET lyDo = @lydo, loai = @loai
                WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@lydo", kl.LyDo);
                cmd.Parameters.AddWithValue("@loai", kl.Loai);
                cmd.Parameters.AddWithValue("@id", kl.Id);
                cmd.ExecuteNonQuery();

                // Cập nhật ngày áp dụng (nếu cần)
                SqlCommand cmd2 = new SqlCommand(@"
                UPDATE NhanVien_ThuongPhat 
                SET thangApDung = @ngay 
                WHERE idThuongPhat = @idtp", conn);
                cmd2.Parameters.AddWithValue("@ngay", kl.NgayKyLuat);
                cmd2.Parameters.AddWithValue("@idtp", kl.Id);
                cmd2.ExecuteNonQuery();
            }
        }

        // Xóa kỷ luật
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM NhanVien_ThuongPhat WHERE idThuongPhat=@id; DELETE FROM ThuongPhat WHERE id=@id",
                    conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
