using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class DALNghiPhep
    {
        private string connectionString;
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALNghiPhep(string conn)
        {
            connectionString = conn;
            _dbContext = new PersonnelManagementDataContextDataContext(conn);
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

        // Lay danh sach Nghi Phep
        public List<DTONghiPhep> LayDsNghiPhep() => _dbContext.NghiPheps.Select(np => new DTONghiPhep
        {
            ID = np.id,
            IDNhanVien = np.idNhanVien,
            NgayBatDau = np.NgayBatDau,
            NgayKetThuc = np.NgayKetThuc,
            LyDoNghi = np.LyDoNghi,
            LoaiNghiPhep = np.LoaiNghiPhep,
            TrangThai = np.TrangThai

        }).ToList();

        // Them Nghi Phep
        public bool ThemNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var NghiPhep = new NghiPhep()
                {
                    idNhanVien = DTO.IDNhanVien.ToUpper(),
                    NgayBatDau = DTO.NgayBatDau,
                    NgayKetThuc = DTO.NgayKetThuc,
                    LyDoNghi = DTO.LyDoNghi,
                    LoaiNghiPhep = DTO.LoaiNghiPhep,
                    TrangThai = DTO.TrangThai
                };

                _dbContext.NghiPheps.InsertOnSubmit(NghiPhep);
                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat Nghi Phep
        public bool CapNhatNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var NghiPhep = _dbContext.NghiPheps.FirstOrDefault(np => np.id == DTO.ID);

                NghiPhep.NgayBatDau = DTO.NgayBatDau;
                NghiPhep.NgayKetThuc = DTO.NgayKetThuc;
                NghiPhep.LyDoNghi = DTO.LyDoNghi;
                NghiPhep.LoaiNghiPhep = DTO.LoaiNghiPhep;
                NghiPhep.TrangThai = DTO.TrangThai;

                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Xoa Nghi Phep
        public bool XoaNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var NghiPhep = _dbContext.NghiPheps.FirstOrDefault(p => p.id == DTO.ID);

                _dbContext.NghiPheps.DeleteOnSubmit(NghiPhep);
                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Kiem tra ID nhan vien trong db
        public bool KiemTraIDNhanVien(string idNhanVien)
        {
            var checkIDNV = _dbContext.NhanViens.FirstOrDefault(np => np.id == idNhanVien);
            if (checkIDNV == null) return false;
            return true;
        }

        // Kiem tra ID nghi phep trong db
        public bool KiemTraIDNghiPhep(int idNghiPhep)
        {
            var checkIDNP = _dbContext.NghiPheps.FirstOrDefault(np => np.id == idNghiPhep);
            if (checkIDNP == null) return false;
            return true;
        }
    }
}
