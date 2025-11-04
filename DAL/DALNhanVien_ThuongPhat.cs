using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DTO;

namespace DAL
{
    public class DALNhanVien_ThuongPhat
    {
        private readonly string _conn;
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALNhanVien_ThuongPhat(string connectionString)
        {
            _conn = connectionString;
            _dbContext = new PersonnelManagementDataContextDataContext(connectionString);
        }

        public List<NhanVien_ThuongPhat> DsNhanVien_ThuongPhat()
            => _dbContext.NhanVien_ThuongPhats.ToList();

        // ✅ Lấy danh sách hiển thị
        
        public DataTable GetAll(string loai, string idPhongBan = "")
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string sql = @"
                                SELECT 
                                    nvtp.id AS ID,
                                    nv.id AS IdNhanVien,
                                    nv.TenNhanVien,
                                    pb.TenPhongBan,
                                    tp.tienThuongPhat AS SoTien,
                                    tp.loai AS Loai,
                                    tp.lyDo AS LyDo,
                                    nvtp.thangApDung AS NgayApDung
                                FROM NhanVien_ThuongPhat nvtp
                                INNER JOIN ThuongPhat tp ON tp.id = nvtp.idThuongPhat
                                INNER JOIN NhanVien nv ON nv.id = nvtp.idNhanVien
                                INNER JOIN PhongBan pb ON pb.id = nv.idPhongBan
                                WHERE tp.loai = @loai";

                if (!string.IsNullOrEmpty(idPhongBan))
                    sql += " AND pb.id = @idpb";

                sql += " ORDER BY nvtp.thangApDung DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@loai", SqlDbType.NVarChar).Value = loai;
                    if (!string.IsNullOrEmpty(idPhongBan))
                        cmd.Parameters.Add("@idpb", SqlDbType.NVarChar).Value = idPhongBan;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }



        // ✅ Lấy danh sách lý do theo loại
        public DataTable GetAllLyDo(string loai)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string query = @"SELECT id, lyDo, tienThuongPhat FROM ThuongPhat WHERE loai = @Loai";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Loai", loai);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ✅ Lấy lý do theo ID
        public string GetLyDoById(int idThuongPhat)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string query = "SELECT lyDo FROM ThuongPhat WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idThuongPhat);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result == null ? "" : result.ToString();
            }
        }

        // ✅ Kiểm tra lý do có tồn tại
        public int CheckLyDoExists(string loai, string lyDo)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string sql = "SELECT id FROM ThuongPhat WHERE loai=@loai AND lyDo=@lydo";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@loai", loai);
                    cmd.Parameters.AddWithValue("@lydo", lyDo);
                    conn.Open();
                    var r = cmd.ExecuteScalar();
                    return r == null ? 0 : Convert.ToInt32(r);
                }
            }
        }

        // ✅ Tạo mới lý do (trả về id mới)
        public int InsertLyDo(string loai, string lyDo, decimal tien, string idNguoiTao)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string sql = @"
                    INSERT INTO ThuongPhat (tienThuongPhat, loai, lyDo, idNguoiTao)
                    OUTPUT INSERTED.id
                    VALUES (@tien, @loai, @lydo, @idng)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tien", tien);
                    cmd.Parameters.AddWithValue("@loai", loai);
                    cmd.Parameters.AddWithValue("@lydo", lyDo);
                    cmd.Parameters.AddWithValue("@idng", idNguoiTao);
                    conn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        // ✅ Thêm nhiều nhân viên cho cùng 1 thưởng/phạt — id tự động tăng
        public void InsertNhanVienThuongPhat_Multi(int idThuongPhat, List<string> idNhanViens, DateTime ngayApDung)
        {
            if (idNhanViens == null || idNhanViens.Count == 0) return;

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = @"
                        IF NOT EXISTS (
                            SELECT 1 FROM NhanVien_ThuongPhat 
                            WHERE idNhanVien = @idnv 
                              AND idThuongPhat = @idtp 
                              AND thangApDung = @ngay
                        )
                        INSERT INTO NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung)
                        VALUES (@idnv, @idtp, @ngay);";

                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.Add("@idnv", SqlDbType.VarChar);
                            cmd.Parameters.Add("@idtp", SqlDbType.Int).Value = idThuongPhat;
                            cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = ngayApDung;

                            foreach (var idnv in idNhanViens)
                            {
                                cmd.Parameters["@idnv"].Value = idnv;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        // ✅ Lấy danh sách nhân viên theo 1 nhóm thưởng/phạt
        public List<string> GetNhanVienByThuongPhatId(int idThuongPhat)
        {
            var list = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string sql = "SELECT idNhanVien FROM NhanVien_ThuongPhat WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idThuongPhat);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        list.Add(rd["idNhanVien"].ToString());
                    }
                }
            }
            return list;
        }

        // ✅ Xóa nhân viên khỏi nhóm thưởng/phạt
        public void DeleteNhanVienInThuongPhat(int idThuongPhat, string idNhanVien)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string sql = "DELETE FROM NhanVien_ThuongPhat WHERE id=@id AND idNhanVien=@idnv";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idThuongPhat);
                    cmd.Parameters.AddWithValue("@idnv", idNhanVien);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ Cập nhật lý do, tiền
        public void UpdateThuongPhat(int id, string lyDo, decimal tien)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string sql = "UPDATE ThuongPhat SET lyDo=@lydo, tienThuongPhat=@tien WHERE id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@lydo", lyDo);
                    cmd.Parameters.AddWithValue("@tien", tien);
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ Xóa nhóm thưởng/phạt (bao gồm nhân viên)
        public void DeleteThuongPhat(int id)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sql1 = "DELETE FROM NhanVien_ThuongPhat WHERE id = @id";
                        using (SqlCommand cmd1 = new SqlCommand(sql1, conn, tx))
                        {
                            cmd1.Parameters.AddWithValue("@id", id);
                            cmd1.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public void XoaNhieuNhanVien_ThuongPhat(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return;

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                string idList = string.Join(",", ids);

                string query = $"DELETE FROM NhanVien_ThuongPhat WHERE id IN ({idList})";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
