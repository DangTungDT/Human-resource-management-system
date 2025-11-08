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
    public class DALPhuCap
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        private readonly string connectionString;

        public DALPhuCap(string stringConnection)
        {
            connectionString = stringConnection;
           _dbContext = new PersonnelManagementDataContextDataContext(stringConnection);
          
        }

      public List<PhuCap> DsPhuCap() => _dbContext.PhuCaps.ToList();
        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PhuCap", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int InsertPhuCapMoi(string lyDoPhuCap, decimal soTien)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO PhuCap (soTien, lyDoPhuCap) 
            VALUES (@soTien, @lyDoPhuCap);
            SELECT SCOPE_IDENTITY();
        ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lyDoPhuCap", lyDoPhuCap);
                    cmd.Parameters.AddWithValue("@soTien", soTien);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    conn.Close();

                    if (result != null && result != DBNull.Value)
                        return Convert.ToInt32(Convert.ToDecimal(result)); // SCOPE_IDENTITY trả kiểu decimal
                    else
                        return -1;
                }
            }
        }
        public bool Insert(DTOPhuCap pc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO PhuCap (soTien, lyDoPhuCap) VALUES (@soTien, @lyDo)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@soTien", pc.SoTien);
                cmd.Parameters.AddWithValue("@lyDo", pc.LyDoPhuCap);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(int id, DTOPhuCap pc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE PhuCap SET soTien = @soTien, lyDoPhuCap = @lyDo WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@soTien", pc.SoTien);
                cmd.Parameters.AddWithValue("@lyDo", pc.LyDoPhuCap);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM PhuCap WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable Search(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PhuCap WHERE lyDoPhuCap LIKE @kw";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
