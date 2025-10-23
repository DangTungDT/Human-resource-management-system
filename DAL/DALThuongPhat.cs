using DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALThuongPhat
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;
        private readonly string connectionString;

        public DALThuongPhat(string stringConnection)
        {
            _dbContext = new PersonnelManagementDataContextDataContext(stringConnection);
            connectionString = stringConnection;
        }
        public List<ThuongPhat> DanhSachThuongPhat() => _dbContext.ThuongPhats.ToList();


        public IQueryable<DTONhanVien> DanhSachNhanVien() => _dbContext.NhanViens.Select(p => new DTONhanVien
        {
            ID = p.Id,
            TenNhanVien = p.TenNhanVien
        });

        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT id, loai, lyDo, tienThuongPhat, idNguoiTao FROM ThuongPhat";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // Thêm mới nhóm thưởng/phạt
        public int Insert(string loai, string lyDo, decimal soTien, string nguoiTao)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ThuongPhat (Loai, LyDo, tienThuongPhat, idNguoiTao) " +
                               "OUTPUT INSERTED.Id VALUES (@Loai, @LyDo, @tienThuongPhat, @idNguoiTao)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Loai", loai);
                cmd.Parameters.AddWithValue("@LyDo", lyDo);
                cmd.Parameters.AddWithValue("@tienThuongPhat", soTien);
                cmd.Parameters.AddWithValue("@idNguoiTao", nguoiTao);
                conn.Open();
                int newId = (int)cmd.ExecuteScalar();
                conn.Close();
                return newId;
            }
        }

        // Cập nhật nhóm
        public void Update(int id, string loai, string lyDo, decimal soTien)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE ThuongPhat SET Loai = @Loai, LyDo = @LyDo, tienThuongPhat = @tienThuongPhat WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Loai", loai);
                cmd.Parameters.AddWithValue("@LyDo", lyDo);
                cmd.Parameters.AddWithValue("@tienThuongPhat", soTien);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // Xóa nhóm
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query2 = "DELETE FROM ThuongPhat WHERE Id = @Id";
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd2.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}
