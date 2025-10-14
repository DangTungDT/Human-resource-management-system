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
    public class DALNhanVien
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;
        private readonly string connectionString;
        public DALNhanVien( string conn)
        {
            //_dbContext = new PersonnelManagementDataContextDataContext(stringConnection);
            connectionString = conn;
        }

        public DataTable GetAll(bool showHidden)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = showHidden
                    ? @"SELECT id AS [Mã NV], TenNhanVien AS [Họ tên], NgaySinh AS [Ngày sinh], GioiTinh AS [Giới tính],
                               DiaChi AS [Địa chỉ], Que AS [Quê quán], Email AS [Email], N'Đã ẩn' AS [Trạng thái]
                        FROM NhanVien WHERE DaXoa = 1"
                    : @"SELECT id AS [Mã NV], TenNhanVien AS [Họ tên], NgaySinh AS [Ngày sinh], GioiTinh AS [Giới tính],
                               DiaChi AS [Địa chỉ], Que AS [Quê quán], Email AS [Email], N'Đang làm việc' AS [Trạng thái]
                        FROM NhanVien WHERE DaXoa = 0";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool Insert(DTONhanVien nv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO NhanVien (id, TenNhanVien, NgaySinh, GioiTinh, DiaChi, Que, Email, idChucVu, idPhongBan, DaXoa)
                           VALUES (@id, @Ten, @Ngay, @GT, @DC, @Que, @Email, @idCV, @idPB, 0)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", nv.ID);
                    cmd.Parameters.AddWithValue("@Ten", nv.TenNhanVien);
                    cmd.Parameters.AddWithValue("@Ngay", nv.NgaySinh);
                    cmd.Parameters.AddWithValue("@GT", nv.GioiTinh);
                    cmd.Parameters.AddWithValue("@DC", nv.DiaChi);
                    cmd.Parameters.AddWithValue("@Que", nv.Que);
                    cmd.Parameters.AddWithValue("@Email", nv.Email);
                    cmd.Parameters.AddWithValue("@idCV", nv.IdChucVu);
                    cmd.Parameters.AddWithValue("@idPB", nv.IdPhongBan);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                // Bạn có thể log lỗi nếu cần
                Console.WriteLine("Lỗi khi thêm nhân viên: " + ex.Message);
                return false;
            }
        }

        public bool Update(DTONhanVien nv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"UPDATE NhanVien SET TenNhanVien=@Ten, NgaySinh=@Ngay, GioiTinh=@GT,
                           DiaChi=@DC, Que=@Que, Email=@Email, idChucVu=@idCV, idPhongBan=@idPB
                           WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", nv.ID);
                    cmd.Parameters.AddWithValue("@Ten", nv.TenNhanVien);
                    cmd.Parameters.AddWithValue("@Ngay", nv.NgaySinh);
                    cmd.Parameters.AddWithValue("@GT", nv.GioiTinh);
                    cmd.Parameters.AddWithValue("@DC", nv.DiaChi);
                    cmd.Parameters.AddWithValue("@Que", nv.Que);
                    cmd.Parameters.AddWithValue("@Email", nv.Email);
                    cmd.Parameters.AddWithValue("@idCV", nv.IdChucVu);
                    cmd.Parameters.AddWithValue("@idPB", nv.IdPhongBan);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật nhân viên: " + ex.Message);
                return false;
            }
        }

        public void AnNhanVien(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE NhanVien SET DaXoa=1 WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void KhoiPhucNhanVien(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE NhanVien SET DaXoa=0 WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public string SinhMaNhanVien(string tenChucVu, string tenPhongBan)
        {
            // Lấy chữ cái đầu của từng từ trong chức vụ
            string prefixCV = new string(tenChucVu.Split(' ')
                                    .Where(s => !string.IsNullOrEmpty(s))
                                    .Select(s => s[0])
                                    .ToArray()).ToUpper();

            

            // Gộp lại: VD "Nhân viên Marketing" => NVM
            string prefix = prefixCV;

            // Đảm bảo tổng độ dài = 10 ký tự
            int totalLength = 10;
            int numLength = totalLength - prefix.Length;
            if (numLength <= 0)
                throw new Exception("Prefix quá dài, không thể sinh mã 10 ký tự!");

            int nextNum = 1;
            int soLuongPB = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1️⃣ Lấy mã NV cuối cùng theo prefix
                string queryLastId = "SELECT TOP 1 id FROM NhanVien WHERE id LIKE @pre + '%' ORDER BY id DESC";
                SqlCommand cmdLast = new SqlCommand(queryLastId, conn);
                cmdLast.Parameters.AddWithValue("@pre", prefix);
                var result = cmdLast.ExecuteScalar();

                if (result != null)
                {
                    string lastId = result.ToString();
                    string numStr = lastId.Substring(prefix.Length);
                    if (int.TryParse(numStr, out int lastNum))
                        nextNum = lastNum + 1;
                }

                // 2️⃣ Đếm số nhân viên thuộc phòng ban này
                string queryCount = @"SELECT COUNT(*) FROM NhanVien n 
                              JOIN PhongBan pb ON n.idPhongBan = pb.id 
                              WHERE pb.TenPhongBan = @tenPB";
                SqlCommand cmdCount = new SqlCommand(queryCount, conn);
                cmdCount.Parameters.AddWithValue("@tenPB", tenPhongBan);
                soLuongPB = Convert.ToInt32(cmdCount.ExecuteScalar());

                // Nếu phòng ban có sẵn nhân viên thì cộng thêm
                nextNum += soLuongPB;
            }

            // Sinh mã mới: prefix + phần số (zero-padding)
            string maNV = prefix + nextNum.ToString().PadLeft(numLength, '0');
            return maNV;
        }

    }
}
