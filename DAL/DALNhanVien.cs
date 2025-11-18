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
    public class ImageStaff
    {
        public string Id { get; set; }
        public string ImageName { get; set; }
    }
    public class DALNhanVien
    {
        private readonly string connectionString;
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALNhanVien(string conn)
        {
            connectionString = conn;
            _dbContext = new PersonnelManagementDataContextDataContext(conn);
        }

        public int? LayIDPhongBanTheoNhanVien(string idNhanVien)
        {
            string query = "SELECT idPhongBan FROM NhanVien WHERE id = @IDNhanVien";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@IDNhanVien", idNhanVien);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    return Convert.ToInt32(result);
                else
                    return null; // không tìm thấy
            }
        }
        public List<ImageStaff> GetStaffByRole(string idStaff, int idDepartment)
        {
            string role = idStaff.Substring(0, 2);
            if (role == "NV")
            {
                //Role nhân viên
                return (_dbContext.NhanViens.Where(x => x.id == idStaff).Select(x => new ImageStaff
                {
                    Id = x.id,
                    ImageName = x.AnhDaiDien
                })).ToList();
            }
            else if(role == "TP")
            {
                //Role trưởng phòng
                return (_dbContext.NhanViens.Where(x => x.idPhongBan == idDepartment).Select(x => new ImageStaff
                {
                    Id = x.id,
                    ImageName = x.AnhDaiDien
                })).ToList();
            }
            //Role Giám đốc
                return (_dbContext.NhanViens.Select(x => new ImageStaff
                {
                    Id = x.id,
                    ImageName = x.AnhDaiDien
                })).ToList();
        }
        public List<ImageStaff> GetStaffByNameEmailCheckin(string name, string email, bool checkin, int idDepartment, string idStaff)
        {
            string role = idStaff.Substring(0, 2);
            var list = _dbContext.NhanViens.Select(x => x);
            if (role == "NV")
            {
                //Nhân viên yêu cầu có chức vụ là nhân viên
                list = list.Where(x => x.id == idStaff);
            }
            else if(role == "TP")
            {
                //Nhân viên yêu cầu có chức vụ là trưởng phòng
                list = list.Where(x => x.idPhongBan == idDepartment);
            }

            if (!string.IsNullOrEmpty(name))
            {
                string nameLower = name.ToLower();
                list = list.Where(x => x.TenNhanVien.ToLower().Contains(nameLower));
            }

            if (!string.IsNullOrEmpty(email))
            {
                string emailLower = email.ToLower();
                list = list.Where(x => x.Email.ToLower() == emailLower);
            }

            DateTime today = DateTime.Now.Date;

            if (checkin)
            {
                return list
                    .Join(_dbContext.ChamCongs.Where(c => c.GioVao != null && c.NgayChamCong == today),
                        nv => nv.id,
                        cc => cc.idNhanVien,
                        (nv, cc) => new ImageStaff
                        {
                            Id = nv.id,
                            ImageName = nv.AnhDaiDien
                        })
                    .ToList();
            }
            else
            {
                return (
                    from nv in list
                    join cc in _dbContext.ChamCongs.Where(c => c.NgayChamCong == today)
                        on nv.id equals cc.idNhanVien into leftJoin
                    from cc in leftJoin.DefaultIfEmpty()
                    where cc == null
                    select new ImageStaff
                    {
                        Id = nv.id,
                        ImageName = nv.AnhDaiDien
                    }
                ).ToList();
            }
        }


        //public List<ImageStaff> GetStaffByNameEmailCheckin(string name, string email, bool checkin, int idDepartment)
        //{
        //    var list = _dbContext.NhanViens.Where(x=> x.idPhongBan == idDepartment).Select(x=> x);
        //    if(name != null || !string.IsNullOrEmpty(name))
        //    {
        //        list = list.Where(x => x.TenNhanVien.ToLower() == name.ToLower());
        //    }
        //    if (email != null || !string.IsNullOrEmpty(email))
        //    {
        //        list = list.Where(x => x.Email.ToLower() == email.ToLower());
        //    }
        //    if (checkin)
        //    {
        //        List<ImageStaff> result = (list.Join(_dbContext.ChamCongs,
        //                            nv => nv.id,
        //                            cc => cc.idNhanVien,
        //                            (nv, cc) => new { nv, cc }).Where(x => x.cc.GioVao != null).Select(x => new ImageStaff
        //                            {
        //                                Id = x.nv.id,
        //                                ImageName = x.nv.AnhDaiDien
        //                            })).ToList();
        //        return result;
        //    }
        //    else
        //    {
        //        DateTime today = DateTime.Today;

        //        var result = (
        //            from nv in list
        //            join cc in _dbContext.ChamCongs
        //                on new { nvId = nv.id, Ngay = today }
        //                equals new { nvId = cc.idNhanVien, Ngay = cc.NgayChamCong }
        //                into leftJoin
        //            from cc in leftJoin.DefaultIfEmpty()
        //            where cc == null
        //            select new ImageStaff
        //            {
        //                Id = nv.id,
        //                ImageName = nv.AnhDaiDien
        //            }
        //        ).ToList();
        //        return result;


        //    }
        //}

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

        //// Lấy danh sách nhân viên trong phòng mà trưởng phòng đang quản lý
        //public DataTable ComboboxNhanVien()
        //{
        //    string sql = "SELECT id, TenNhanVien FROM NhanVien WHERE DaXoa = 0 ORDER BY TenNhanVien";
        //    using (var cn = new SqlConnection(connectionString))
        //    using (var da = new SqlDataAdapter(sql, cn))
        //    {
        //        var dt = new DataTable();
        //        da.Fill(dt);
        //        return dt;
        //    }
        //}

        // Trả về số buổi nghỉ không có phép đã duyệt trong tháng (đếm)
        private int CountMissingAttendances(string idNhanVien, int thang, int nam)
        {
            string sql = @"
                SELECT COUNT(*) FROM ChamCong cc
                WHERE cc.idNhanVien = @idNhanVien
                  AND MONTH(cc.NgayChamCong) = @Thang
                  AND YEAR(cc.NgayChamCong) = @Nam
                  AND cc.GioVao IS NULL
                  AND cc.GioRa IS NULL
                  AND NOT EXISTS (
                        SELECT 1 FROM NghiPhep np
                        WHERE np.idNhanVien = cc.idNhanVien
                          AND np.TrangThai = N'Đã duyệt'
                          AND cc.NgayChamCong BETWEEN np.NgayBatDau AND np.NgayKetThuc
                  );
            ";
            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@idNhanVien", idNhanVien);
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Tính điểm chuyên cần: default 5 - misses*2, min 0
        public int CalculateDiemChuyenCan(string idNhanVien, int thang, int nam)
        {
            try
            {
                int misses = CountMissingAttendances(idNhanVien, thang, nam);
                int score = 5 - (misses * 2);
                if (score < 0) score = 0;
                if (score > 5) score = 5;
                return score;
            }
            catch
            {
                return 5; // fallback
            }
        }

        // Trả về danh sách nhân viên mà người hiện tại có quyền xem:
        // Nếu người là giám đốc (idChucVu = 1) -> all; else nhân viên trong cùng phòng
        public DataTable GetNhanVienTheoTruongPhong(string idNguoi)
        {
            string sql = @"
                DECLARE @isGiamDoc BIT = 0;
                SELECT @isGiamDoc = CASE WHEN idChucVu = 1 THEN 1 ELSE 0 END;
                SELECT @isGiamDoc = @isGiamDoc FROM NhanVien WHERE id = @idNguoi;

                IF (@isGiamDoc = 1)
                BEGIN
                    SELECT id, TenNhanVien, idChucVu, idPhongBan FROM NhanVien WHERE DaXoa = 0;
                END
                ELSE
                BEGIN
                    SELECT idPhongBan INTO #tmp FROM NhanVien WHERE id = @idNguoi;
                    SELECT nv.id, nv.TenNhanVien, nv.idChucVu, nv.idPhongBan
                    FROM NhanVien nv
                    WHERE nv.DaXoa = 0 AND nv.idPhongBan = (SELECT TOP 1 idPhongBan FROM NhanVien WHERE id = @idNguoi);
                END
            ";
            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@idNguoi", idNguoi);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


        public DTONhanVien GetStaffById(string idStaff)
        {
            var result = _dbContext.NhanViens.FirstOrDefault(x => x.id == idStaff);
            if(result != null)
            {
                DTONhanVien dto = new DTONhanVien
                {
                    ID = result.id,
                    TenNhanVien = result.TenNhanVien,
                    NgaySinh = result.NgaySinh,
                    GioiTinh = result.GioiTinh,
                    DiaChi = result.DiaChi,
                    Que = result.Que,
                    Email = result.Email,
                    IdChucVu = result.idChucVu.ToString(),
                    IdPhongBan = result.idPhongBan.ToString(),
                    AnhDaiDien = result.AnhDaiDien
                };
                return dto;
            }
            return null;
        }

        public DataTable GetById(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT id AS [Mã Nhân viên], TenNhanVien AS [Tên nhân viên], idPhongBan AS [Mã phòng ban], Email
                                 FROM NhanVien
                                 WHERE id = @id";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi lấy thông tin nhân viên theo ID: " + ex.Message);
                }
                return dt;
            }
        }

        public int GetByIdPB(string id)
        {
            int idPhongBan = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT idPhongBan AS [Mã phòng ban]
                                 FROM NhanVien
                                 WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    idPhongBan = (int)cmd.ExecuteScalar();
                }
                try
                {
                    conn.Open();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi lấy thông tin nhân viên theo ID: " + ex.Message);
                }
                return idPhongBan;
            }
        }

        public DataTable LoadNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, TenNhanVien FROM NhanVien WHERE DaXoa = 0";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public DataTable ComboboxNhanVien(int? idPhongBan = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, TenNhanVien FROM NhanVien WHERE (@idPhongBan IS NULL OR idPhongBan = @idPhongBan)";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@idPhongBan", idPhongBan.HasValue ? (object)idPhongBan.Value : DBNull.Value);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 🔹 Lấy thông tin cá nhân của nhân viên
        public DTONhanVien LayThongTin(string idNhanVien)
        {
            DTONhanVien nv = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                                SELECT nv.ID, nv.TenNhanVien, nv.NgaySinh, nv.GioiTinh, 
                                       nv.DiaChi, nv.Que, nv.Email, cv.TenChucVu, pb.TenPhongBan, nv.AnhDaiDien
                                FROM NhanVien nv
                                LEFT JOIN ChucVu cv ON nv.idChucVu = cv.ID
                                LEFT JOIN PhongBan pb ON nv.idPhongBan = pb.ID
                                WHERE nv.ID = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idNhanVien);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            nv = new DTONhanVien
                            {
                                ID = dr["ID"].ToString(),
                                TenNhanVien = dr["TenNhanVien"].ToString(),
                                NgaySinh = Convert.ToDateTime(dr["NgaySinh"]),
                                GioiTinh = dr["GioiTinh"].ToString(),
                                DiaChi = dr["DiaChi"].ToString(),
                                Que = dr["Que"].ToString(),
                                Email = dr["Email"].ToString(),
                                TenChucVu = dr["TenChucVu"].ToString(),
                                TenPhongBan = dr["TenPhongBan"].ToString(),
                                AnhDaiDien = dr["AnhDaiDien"] == DBNull.Value ? null : dr["AnhDaiDien"].ToString()
                            };
                        }
                    }
                }
            }

            return nv;
        }

        // 🔹 Cập nhật thông tin cá nhân
        public void UpdateNhanVien(DTONhanVien nv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE NhanVien 
                 SET TenNhanVien=@ten, NgaySinh=@ngay, GioiTinh=@gt, 
                     DiaChi=@dc, Que=@que, Email=@mail, AnhDaiDien=@anh
                 WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ten", nv.TenNhanVien);
                cmd.Parameters.AddWithValue("@ngay", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@gt", nv.GioiTinh);
                cmd.Parameters.AddWithValue("@dc", nv.DiaChi);
                cmd.Parameters.AddWithValue("@que", nv.Que);
                cmd.Parameters.AddWithValue("@mail", nv.Email);
                cmd.Parameters.AddWithValue("@anh", (object)nv.AnhDaiDien ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", nv.ID);

                conn.Open();
                cmd.ExecuteNonQuery();
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
            if(prefix == "NVM")
            {
                prefix = "MVMKT";
            }

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

        // Lay ds nhan vien
        public List<NhanVien> LayDsNhanVien() => _dbContext.NhanViens.ToList();

        // Lay nhan vien qua id
        public NhanVien LayNhanVienQuaID(string idNhanVien)
        {
            if (idNhanVien != null)
            {
                var nhanVien = _dbContext.NhanViens.FirstOrDefault(nv => nv.id == idNhanVien);

                if (nhanVien != null)
                {
                    return nhanVien;
                }
            }
            return null;
        }

        // Ktra email
        public bool KiemTraEmailTonTai(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM NhanVien WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();

                return count > 0; // true nếu email đã tồn tại
            }
        }
    }


}
