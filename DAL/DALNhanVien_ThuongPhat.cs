using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALNhanVien_ThuongPhat
    {
      private readonly string _conn;
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALNhanVien_ThuongPhat(string conn) => _dbContext = new PersonnelManagementDataContextDataContext(conn);

        public List<NhanVien_ThuongPhat> DsNhanVien_ThuongPhat() => _dbContext.NhanVien_ThuongPhats.ToList();

        public DALNhanVien_ThuongPhat(string connectionString) => _conn = connectionString;

        // ✅ Lấy danh sách hiển thị
        public DataTable GetAll(string loai, string idPhongBan = "")
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string sql = @"
                    SELECT tp.id, nv.TenNhanVien, pb.TenPhongBan, tp.tienThuongPhat AS SoTien, 
                           tp.loai AS Loai, tp.lyDo AS LyDo, nvtp.thangApDung AS NgayApDung
                    FROM ThuongPhat tp
                    JOIN NhanVien_ThuongPhat nvtp ON tp.id = nvtp.idThuongPhat
                    JOIN NhanVien nv ON nvtp.idNhanVien = nv.id
                    JOIN PhongBan pb ON nv.idPhongBan = pb.id
                    WHERE tp.loai = @loai";

                if (!string.IsNullOrEmpty(idPhongBan))
                    sql += " AND pb.id = @idpb";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@loai", loai);
                    if (!string.IsNullOrEmpty(idPhongBan))
                        cmd.Parameters.AddWithValue("@idpb", idPhongBan);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable GetAllLyDo(string loai)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string query = @"SELECT id, lyDo, tienThuongPhat 
                         FROM ThuongPhat 
                         WHERE loai = @Loai";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Loai", loai);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

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

        // ✅ Tạo mới lý do
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

        // ✅ Gán nhiều nhân viên vào 1 ThuongPhat
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
                            INSERT INTO NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung)
                            VALUES (@idnv, @idtp, @ngay)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.Add("@idnv", SqlDbType.NVarChar);
                            cmd.Parameters.Add("@idtp", SqlDbType.Int).Value = idThuongPhat;
                            cmd.Parameters.Add("@ngay", SqlDbType.DateTime).Value = ngayApDung;

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

        

        // ✅ Lấy danh sách nhân viên thuộc 1 nhóm thưởng/phạt
        public List<string> GetNhanVienByThuongPhatId(int idThuongPhat)
        {
            List<string> list = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                string sql = "SELECT idNhanVien FROM NhanVien_ThuongPhat WHERE idThuongPhat = @id";
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
                string sql = "DELETE FROM NhanVien_ThuongPhat WHERE idThuongPhat=@idtp AND idNhanVien=@idnv";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idtp", idThuongPhat);
                    cmd.Parameters.AddWithValue("@idnv", idNhanVien);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ Cập nhật thông tin nhóm thưởng/phạt
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

        // ✅ Xóa nhóm thưởng/phạt
        public void DeleteThuongPhat(int id)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
                    DELETE FROM NhanVien_ThuongPhat WHERE idThuongPhat = @id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
