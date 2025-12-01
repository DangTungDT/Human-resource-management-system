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
    public class DALNhanVien_KhauTru
    {
        public readonly PersonnelManagementDataContext _databaseContext;

        private readonly string _connectionString;

        public DALNhanVien_KhauTru(string stringConnection)
        {
            _connectionString = stringConnection;
          _databaseContext = new PersonnelManagementDataContext(stringConnection);
        }

       public List<NhanVien_KhauTru> DsNhanVien_KhauTru() => _databaseContext.NhanVien_KhauTrus.ToList();
        public DataTable GetAll(string idPhongBan = "")
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT nkt.id, nv.id AS idNhanVien, nv.TenNhanVien, pb.TenPhongBan, 
                           kt.loaiKhauTru AS LyDo, kt.soTien AS SoTien, nkt.thangApDung
                    FROM NhanVien_KhauTru nkt
                    JOIN NhanVien nv ON nkt.idNhanVien = nv.id
                    JOIN PhongBan pb ON nv.idPhongBan = pb.id
                    JOIN KhauTru kt ON nkt.idKhauTru = kt.id
                    WHERE (@idPhongBan IS NULL OR nv.idPhongBan = @idPhongBan)";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@idPhongBan", string.IsNullOrEmpty(idPhongBan) ? (object)DBNull.Value : idPhongBan);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int InsertLyDoMoi(string lyDo, decimal soTien, string idNguoiTao)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                                INSERT INTO KhauTru (loaiKhauTru, soTien, idNguoiTao)
                                OUTPUT INSERTED.Id
                                VALUES (@loaiKhauTru, @soTien, @idng)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@loaiKhauTru", lyDo);
                cmd.Parameters.AddWithValue("@soTien", soTien);
                cmd.Parameters.AddWithValue("@idng", idNguoiTao);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool Insert(DTONhanVien_KhauTru nkt)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO NhanVien_KhauTru (idNhanVien, idKhauTru, thangApDung) VALUES (@idNhanVien, @idKhauTru, @thangApDung)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idNhanVien", nkt.IdNhanVien);
                cmd.Parameters.AddWithValue("@idKhauTru", nkt.IdKhauTru);
                cmd.Parameters.AddWithValue("@thangApDung", nkt.ThangApDung);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(int id, DTONhanVien_KhauTru nkt)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE NhanVien_KhauTru SET idNhanVien = @idNhanVien, idKhauTru = @idKhauTru, thangApDung = @thangApDung WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@idNhanVien", nkt.IdNhanVien);
                cmd.Parameters.AddWithValue("@idKhauTru", nkt.IdKhauTru);
                cmd.Parameters.AddWithValue("@thangApDung", nkt.ThangApDung);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM NhanVien_KhauTru WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetAllLyDo()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT id, loaiKhauTru, soTien FROM KhauTru";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
