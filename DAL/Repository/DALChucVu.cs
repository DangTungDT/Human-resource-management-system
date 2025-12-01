using DAL.DataContext;
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
        private readonly PersonnelManagementDataContext _databaseContext;
        private string _connectionString;
        public DALChucVu(string conn)
        {
            _connectionString = conn;
            _databaseContext = new PersonnelManagementDataContext(conn);
        }

        public DTOChucVu FindNameById(int id)
        {
            return _databaseContext.ChucVus.Where(x => x.id == id).Select(x=> new DTOChucVu()
            {
                Id = id,
                TenChucVu = x.TenChucVu,
                LuongCoBan = x.luongCoBan,
                TyLeHoaHong = decimal.Parse(x.tyLeHoaHong.ToString()),
                MoTa = x.moTa,
                IdPhongBan = x.idPhongBan
            }).FirstOrDefault();
        }
        public IQueryable GetPositionByDepartment(int departmentId) => _databaseContext.ChucVus.Where(x => x.idPhongBan == departmentId);

        public ChucVu GetPositionByIdStaff(string idStaff) => (from p in _databaseContext.ChucVus
                                                               from s in _databaseContext.NhanViens
                                                               where s.idChucVu == p.id
                                                               select p).FirstOrDefault();

        // Lấy danh sách chức vụ (theo từ khóa)
        public DataTable GetAll(string keyword = "")
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
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
            using (SqlConnection conn = new SqlConnection(_connectionString))
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
                using (SqlConnection conn = new SqlConnection(_connectionString))
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
                using (SqlConnection conn = new SqlConnection(_connectionString))
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
            bool checkNV = _databaseContext.NhanViens.Any(p => p.idChucVu == id);
            bool checkUV = _databaseContext.UngViens.Any(p => p.idChucVuUngTuyen == id);
            bool checkTD = _databaseContext.TuyenDungs.Any(p => p.idChucVu == id);

            if (!checkNV && !checkUV && !checkTD)
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM ChucVu WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<ChucVu> LayDsChucVu() => _databaseContext.ChucVus.ToList();

        public bool CheckPosition(string namePosition, int departmentId)
        {
            if (_databaseContext.ChucVus.Any(x => x.TenChucVu == namePosition && x.idPhongBan == departmentId)) return false;
            return true;
        }

        public string LayTenChucVu(int id) => _databaseContext.ChucVus.FirstOrDefault(p => p.id == id).TenChucVu ?? string.Empty;
    }
}
