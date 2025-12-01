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
    public class DALPhongBan
    {
        private readonly string _connectionString;
        private readonly PersonnelManagementDataContext _databaseContext;


        public DTOPhongBan GetDepartmentByID(int id)
        {
            DTOPhongBan phongBan = _databaseContext.PhongBans.Where(pb => pb.id == id).Select(pb => new DTOPhongBan
            {
                Id = pb.id,
                TenPhongBan = pb.TenPhongBan,
                MoTa = pb.Mota
            }).FirstOrDefault();
            return phongBan;
        }
        public DALPhongBan(string conn)
        {
            _connectionString = conn;
            _databaseContext = new PersonnelManagementDataContext(conn);
        }

        public DTOPhongBan FindPhongBanByIdChucVu(int id)
        {
            DTOPhongBan phongBan = (from pb in _databaseContext.PhongBans
                                    join cv in _databaseContext.ChucVus on pb.id equals cv.idPhongBan
                                    where cv.id == id
                                    select new DTOPhongBan
                                    {
                                        Id = pb.id,
                                        TenPhongBan = pb.TenPhongBan,
                                        MoTa = pb.Mota
                                    }).FirstOrDefault();
            return phongBan;
        }
        public DataTable GetAllPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT id AS [Mã phòng ban], TenPhongBan AS [Tên phòng ban], MoTa AS [Mô tả] FROM PhongBan";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Lấy danh sách phòng ban (để hiển thị combobox)
        public DataTable ComBoBoxPhongBan()
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

        public string InsertPhongBan(DTOPhongBan pb)
        {
            bool checkTenPhongBan = _databaseContext.PhongBans.Any(x => x.TenPhongBan == pb.TenPhongBan);
            if(checkTenPhongBan)
            {
                //Tên phòng ban mới đã tồn tại trong database
                return "Tên phòng ban đã tồn tại không thể thêm";
            }
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO PhongBan (TenPhongBan, MoTa) VALUES (@Ten, @MoTa)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", pb.TenPhongBan);
                cmd.Parameters.AddWithValue("@MoTa", pb.MoTa ?? (object)DBNull.Value);
                conn.Open();
                if(cmd.ExecuteNonQuery() > 0)
                {
                    return "Thêm phòng ban thành công!";
                }
                return "Thêm phòng ban thất bại!";
            }
        }

        public string UpdatePhongBan(DTOPhongBan pb)
        {
            bool checkTenPhongBan = _databaseContext.PhongBans.Any(x => x.TenPhongBan == pb.TenPhongBan && x.id != pb.Id);
            if (checkTenPhongBan)
            {
                //Tên phòng ban mới đã tồn tại trong database
                return "Tên phòng ban mới đã bị trùng, vui lòng nhập tên khác!";
            }
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE PhongBan SET TenPhongBan=@Ten, MoTa=@MoTa WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", pb.TenPhongBan);
                cmd.Parameters.AddWithValue("@MoTa", pb.MoTa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@id", pb.Id);
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return "Cập nhật thông tin phòng ban thành công!";
                }
                return "Cập nhật phòng ban thất bại!";
            }
        }

        public string DeletePhongBan(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var ktraIDPB = _databaseContext.ChucVus.FirstOrDefault(cv => cv.idPhongBan == id);
                var ktraIDNV = _databaseContext.NhanViens.FirstOrDefault(cv => cv.idPhongBan == id);

                if (ktraIDPB != null && ktraIDPB != null)
                {
                    return "Phòng ban hiện đang có dữ liệu nhân viên và chức vụ, nên không thể xóa";
                }
                if(ktraIDPB != null)
                {
                    return "Phòng ban hiện đang có dữ liệu chức vụ, nên không thể xóa";
                }
                if (ktraIDPB != null)
                {
                    return "Phòng ban hiện đang có dữ liệu nhân viên, nên không thể xóa";
                }

                string query = "DELETE FROM PhongBan WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                if(cmd.ExecuteNonQuery() > 0)
                {
                    return "Xóa phòng ban thành công!";
                }
                return "Xóa phòng ban thất bại!";
            }
        }

        // Lay ten phong ban
        public string LayTenPhongBan(int id) => _databaseContext.PhongBans.FirstOrDefault(p => p.id == id).TenPhongBan ?? string.Empty;

        // Lay ds phong ban
        public List<PhongBan> LayDsPhongBan() => _databaseContext.PhongBans.ToList();
    }
}
