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
                try
                {
                    conn.Open();
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi lấy danh sách tài khoản: " + ex.Message);
                }
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
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi thêm tài khoản: " + ex.Message);
                }
            }
        }

        public void Update(DTOTaiKhoan tk)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"UPDATE TaiKhoan SET taiKhoan = @tk, matKhau = @mk, idNhanVien = @idNV
                               WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", tk.Id);
                cmd.Parameters.AddWithValue("@tk", tk.TaiKhoan);
                cmd.Parameters.AddWithValue("@mk", tk.MatKhau);
                cmd.Parameters.AddWithValue("@idNV", tk.IdNhanVien);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi cập nhật tài khoản: " + ex.Message);
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM TaiKhoan WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi xóa tài khoản: " + ex.Message);
                }
            }
        }

        public void CreateDefaultAccount(string idNV, string tenNhanVien)
        {
            // Kiểm tra nếu nhân viên này đã có tài khoản
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE idNhanVien = @idNV";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@idNV", idNV);
                conn.Open();
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    string defaultUsername = idNV; // Sử dụng idNV làm tài khoản mặc định
                    string defaultPassword = "1"; // Mật khẩu mặc định (nên mã hóa)
                    string insertQuery = @"INSERT INTO TaiKhoan (taiKhoan, matKhau, idNhanVien)
                                   VALUES (@tk, @mk, @idNV)";
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@tk", defaultUsername);
                    cmd.Parameters.AddWithValue("@mk", defaultPassword); // Nên mã hóa trước khi lưu
                    cmd.Parameters.AddWithValue("@idNV", idNV);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetByTaiKhoan(string taiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT tk.id AS [Mã], tk.taiKhoan AS [Tài khoản],
                                        tk.matKhau AS [Mật khẩu], nv.TenNhanVien AS [Nhân viên],
                                        nv.id AS [Mã Nhân viên]
                                 FROM TaiKhoan tk
                                 LEFT JOIN NhanVien nv ON tk.idNhanVien = nv.id
                                 WHERE tk.taiKhoan = @taiKhoan";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@taiKhoan", taiKhoan);
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi lấy tài khoản theo tài khoản: " + ex.Message);
                }
                return dt;
            }
        }

        public DataTable GetById(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT tk.id AS [Mã], tk.taiKhoan AS [Tài khoản],
                                        tk.matKhau AS [Mật khẩu], nv.TenNhanVien AS [Nhân viên],
                                        nv.id AS [Mã Nhân viên]
                                 FROM TaiKhoan tk
                                 LEFT JOIN NhanVien nv ON tk.idNhanVien = nv.id
                                 WHERE tk.id = @id";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi lấy tài khoản theo ID: " + ex.Message);
                }
                return dt;
            }
        }

        public void UpdateMatKhau(int id, string matKhauMoi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"UPDATE TaiKhoan SET matKhau = @mk WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@mk", matKhauMoi);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi cập nhật mật khẩu: " + ex.Message);
                }
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
        public List<TaiKhoan> DsTaiKhoan()
        {
            var list =  _dbContext.TaiKhoans.ToList();
            return list;
        }

        // Tim tai khoan qua idNhanVien
        public NhanVien TimTaiKhoanQuaIDNVien(string idNHanVien) => _dbContext.NhanViens.FirstOrDefault(p => p.id == idNHanVien);
    }
    
}
