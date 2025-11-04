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
    public class DALNhanVien_PhuCap
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;
        private readonly string connectionString;

        public DALNhanVien_PhuCap(string conn)
        {
            _dbContext = new PersonnelManagementDataContextDataContext(conn);
            connectionString = conn;
        }

        // Danh sach NV_PC
        public List<NhanVien_PhuCap> DsNhanVien_PhuCap() => _dbContext.NhanVien_PhuCaps.ToList();

        // Tim NV_PC qua id
        public NhanVien_PhuCap TimNhanVien_PhuCapQuaID(string idNhanVien, int idPhuCap) => _dbContext.NhanVien_PhuCaps.FirstOrDefault(p => p.idNhanVien == idNhanVien && p.idPhuCap == idPhuCap);

        // Lấy danh sách chi tiết (dùng cho dgv)
        public DataTable GetNhanVien_PhuCap_WithDetails(int? idPhongBan = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        nv_pc.idNhanVien,
                        nv_pc.idPhuCap,
                        nv.TenNhanVien,
                        pb.TenPhongBan,
                        pc.lyDoPhuCap AS LyDoPhuCap,
                        pc.soTien AS SoTien,
                        nv_pc.trangThai
                    FROM NhanVien_PhuCap nv_pc
                    INNER JOIN NhanVien nv ON nv_pc.idNhanVien = nv.id
                    INNER JOIN PhuCap pc ON nv_pc.idPhuCap = pc.id
                    LEFT JOIN PhongBan pb ON nv.idPhongBan = pb.id
                    WHERE (@idPhongBan IS NULL OR nv.idPhongBan = @idPhongBan)
                    ORDER BY nv.TenNhanVien";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@idPhongBan", idPhongBan.HasValue ? (object)idPhongBan.Value : DBNull.Value);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Tìm bản ghi theo ID NV + ID PC
        public bool Exists(string idNhanVien, int idPhuCap)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM NhanVien_PhuCap WHERE idNhanVien = @idNV AND idPhuCap = @idPC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idNV", idNhanVien);
                cmd.Parameters.AddWithValue("@idPC", idPhuCap);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        // Them NV_PC
        public bool ThemNhanVien_PhuCap(DTONhanVien_PhuCap DTO)
        {
            try
            {
                var nv_pc = new NhanVien_PhuCap()
                {
                    idNhanVien = DTO.IDNhanVien,
                    idPhuCap = DTO.IDPhuCap,
                    trangThai = DTO.TrangThai,
                };

                _dbContext.NhanVien_PhuCaps.InsertOnSubmit(nv_pc);
                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat NV_PC
        public bool CapNhatNhanVien_PhuCap(DTONhanVien_PhuCap DTO)
        {
            try
            {
                var nv_pc = TimNhanVien_PhuCapQuaID(DTO.IDNhanVien, DTO.IDPhuCap);
                if (nv_pc != null)
                {
                    nv_pc.trangThai = DTO.TrangThai;

                    _dbContext.SubmitChanges();

                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        // Xoa NV_PC
        public bool XoaNhanVien_PhuCap(DTONhanVien_PhuCap DTO)
        {
            try
            {
                var nv_pc = TimNhanVien_PhuCapQuaID(DTO.IDNhanVien, DTO.IDPhuCap);
                if (nv_pc != null)
                {
                    _dbContext.NhanVien_PhuCaps.DeleteOnSubmit(nv_pc);
                    _dbContext.SubmitChanges();

                    return true;
                }
                else return false;

            }
            catch { return false; }
        }

        //// Thêm mới
        public bool ThemNhanVien_PhuCap1(DTONhanVien_PhuCap dto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO NhanVien_PhuCap (idNhanVien, idPhuCap, trangThai) VALUES (@idNV, @idPC, @trangThai)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idNV", dto.IDNhanVien);
                cmd.Parameters.AddWithValue("@idPC", dto.IDPhuCap);
                cmd.Parameters.AddWithValue("@trangThai", dto.TrangThai);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Cập nhật trạng thái
        public bool CapNhatNhanVien_PhuCap1(DTONhanVien_PhuCap dto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE NhanVien_PhuCap SET trangThai = @trangThai WHERE idNhanVien = @idNV AND idPhuCap = @idPC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@trangThai", dto.TrangThai);
                cmd.Parameters.AddWithValue("@idNV", dto.IDNhanVien);
                cmd.Parameters.AddWithValue("@idPC", dto.IDPhuCap);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Xóa
        public bool XoaNhanVien_PhuCap1(DTONhanVien_PhuCap dto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM NhanVien_PhuCap WHERE idNhanVien = @idNV AND idPhuCap = @idPC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idNV", dto.IDNhanVien);
                cmd.Parameters.AddWithValue("@idPC", dto.IDPhuCap);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
