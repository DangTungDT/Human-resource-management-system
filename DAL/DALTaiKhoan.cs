using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{
    public class DALTaiKhoan
    {
        private readonly string connectionString;
        private readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALTaiKhoan(string conn)
        {
            connectionString = conn;
            _dbContext = new PersonnelManagementDataContextDataContext(conn);
        }

        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT tk.id AS [Mã], tk.taiKhoan AS [Tài khoản],
                                        tk.matKhau AS [Mật khẩu], nv.TenNhanVien AS [Nhân viên],
                                        nv.id AS [Mã Nhân viên]
                                 FROM TaiKhoan tk
                                 LEFT JOIN NhanVien nv ON tk.idNhanVien = nv.id";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void Insert(DTOTaiKhoan tk)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO TaiKhoan (taiKhoan, matKhau, idNhanVien)
                               VALUES (@tk, @mk, @idNV)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tk", tk.TaiKhoan);
                cmd.Parameters.AddWithValue("@mk", tk.MatKhau);
                cmd.Parameters.AddWithValue("@idNV", tk.IdNhanVien);
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public void Update(DTOTaiKhoan tk)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"UPDATE TaiKhoan SET taiKhoan=@tk, matKhau=@mk, idNhanVien=@idNV
                               WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", tk.Id);
                cmd.Parameters.AddWithValue("@tk", tk.TaiKhoan);
                cmd.Parameters.AddWithValue("@mk", tk.MatKhau);
                cmd.Parameters.AddWithValue("@idNV", tk.IdNhanVien);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM TaiKhoan WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void TaoTaiKhoanChoNhanVienMoi(string idNV, string tenNhanVien)
        {
            string tenDangNhap = idNV; // hoặc dùng email, hoặc ghép theo tên
            string matKhauMacDinh = "123456"; // bạn có thể mã hóa bằng SHA256
            string quyenMacDinh = "NhanVien";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kiểm tra nếu nhân viên này đã có tài khoản chưa
                string checkQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE idNV = @idNV";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@idNV", idNV);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    // Chưa có → tạo mới
                    string insertQuery = @"INSERT INTO TaiKhoan (idNV, TenDangNhap, MatKhau, Quyen, TrangThai)
                                   VALUES (@idNV, @TenDangNhap, @MatKhau, @Quyen, 1)";
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@idNV", idNV);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhauMacDinh);
                    cmd.Parameters.AddWithValue("@Quyen", quyenMacDinh);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Lay ds tai khoan
        public List<TaiKhoan> DsTaiKhoan() => _dbContext.TaiKhoans.ToList();

        // Tim tai khoan qua idNhanVien
        public NhanVien TimTaiKhoanQuaIDNVien(string idNHanVien) => _dbContext.NhanViens.FirstOrDefault(p => p.id == idNHanVien);
    }
}
