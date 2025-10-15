using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALNghiPhep
    {
        private string connectionString;

        public DALNghiPhep(string conn)
        {
            connectionString = conn;
        }

        public List<DTONghiPhep> LayDanhSachNghiPhep(string idNhanVien)
        {
            List<DTONghiPhep> list = new List<DTONghiPhep>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT NgayBatDau, NgayKetThuc, LyDoNghi, LoaiNghiPhep, TrangThai
                    FROM NghiPhep
                    WHERE idNhanVien = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idNhanVien);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new DTONghiPhep()
                    {
                        NgayBatDau = reader.GetDateTime(0),
                        NgayKetThuc = reader.GetDateTime(1),
                        LyDoNghi = reader.GetString(2),
                        LoaiNghiPhep = reader.GetString(3),
                        TrangThai = reader.GetString(4)
                    });
                }
                conn.Close();
            }
            return list;
        }
    }
}
