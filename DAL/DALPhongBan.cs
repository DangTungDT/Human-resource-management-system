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
        private readonly string connectionString;

        public DALPhongBan(string conn)
        {
            connectionString = conn;
        }

        public DataTable GetAllPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, TenPhongBan FROM PhongBan";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool InsertPhongBan(DTOPhongBan pb)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO PhongBan (TenPhongBan, MoTa) VALUES (@Ten, @MoTa)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", pb.TenPhongBan);
                cmd.Parameters.AddWithValue("@MoTa", pb.MoTa ?? (object)DBNull.Value);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdatePhongBan(DTOPhongBan pb)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE PhongBan SET TenPhongBan=@Ten, MoTa=@MoTa WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ten", pb.TenPhongBan);
                cmd.Parameters.AddWithValue("@MoTa", pb.MoTa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@id", pb.Id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeletePhongBan(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM PhongBan WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
