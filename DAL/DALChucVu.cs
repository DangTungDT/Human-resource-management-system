using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALChucVu
    {
        private readonly string connectionString;
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALChucVu(string conn)
        {
            connectionString = conn;
            _dbContext = new PersonnelManagementDataContextDataContext(conn);
        }

        public string FindNameById(int id)
        {
            return _dbContext.ChucVus.Where(x => x.id == id).FirstOrDefault().TenChucVu;
        }
        public IQueryable GetPositionByDepartment(int departmentId) => _dbContext.ChucVus.Where(x => x.idPhongBan == departmentId);

        public ChucVu GetPositionByIdStaff(string idStaff) => (from p in _dbContext.ChucVus
                                                                  from s in _dbContext.NhanViens
                                                                  where s.idChucVu == p.id
                                                                  select p).FirstOrDefault();

        // Lấy danh sách chức vụ (theo từ khóa)
        public DataTable GetAll(string keyword = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT cv.id AS [Mã chức vụ], cv.TenChucVu AS [Tên chức vụ], cv.luongCoBan AS [Lương cơ bản],
                           cv.tyLeHoaHong AS [Tỷ lệ hoa hồng], pb.id AS [Phòng ban], cv.moTa AS [Mô tả]
                    FROM ChucVu cv
                    JOIN PhongBan pb ON cv.idPhongBan = pb.id
                    WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query += " AND (cv.TenChucVu LIKE @kw OR pb.TenPhongBan LIKE @kw OR cv.moTa LIKE @kw)";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrWhiteSpace(keyword))
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

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

        // Thêm mới
        public bool Insert(DTOChucVu cv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO ChucVu (TenChucVu, luongCoBan, tyLeHoaHong, moTa, idPhongBan)
                                 VALUES (@Ten, @Luong, @TyLe, @MoTa, @idPB)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ten", cv.TenChucVu);
                    cmd.Parameters.AddWithValue("@Luong", cv.LuongCoBan);
                    cmd.Parameters.AddWithValue("@TyLe", cv.TyLeHoaHong);
                    cmd.Parameters.AddWithValue("@MoTa", cv.MoTa ?? "");
                    cmd.Parameters.AddWithValue("@idPB", cv.IdPhongBan);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Cập nhật
        public bool Update(DTOChucVu cv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE ChucVu SET TenChucVu=@Ten, luongCoBan=@Luong, 
                                tyLeHoaHong=@TyLe, moTa=@MoTa, idPhongBan=@idPB WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ten", cv.TenChucVu);
                    cmd.Parameters.AddWithValue("@Luong", cv.LuongCoBan);
                    cmd.Parameters.AddWithValue("@TyLe", cv.TyLeHoaHong);
                    cmd.Parameters.AddWithValue("@MoTa", cv.MoTa ?? "");
                    cmd.Parameters.AddWithValue("@idPB", cv.IdPhongBan);
                    cmd.Parameters.AddWithValue("@id", cv.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Xóa
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ChucVu WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<ChucVu> LayDsChucVu() => _dbContext.ChucVus.ToList();

        public bool CheckPosition(string namePosition, int departmentId)
        {
            if (_dbContext.ChucVus.Any(x => x.TenChucVu == namePosition && x.idPhongBan == departmentId)) return false;
            return true;
        }
    }
}
