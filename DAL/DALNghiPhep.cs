using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class TinhLuong
    {
        public int CoPhep { get; set; } = 0;
        public int KhongPhep { get; set; } = 0;
        public string Loai { get; set; }
    }

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

        // Lay NghiPhep qua ID
        public NghiPhep LayNghiPhepQuaID(int id)
        {
            try
            {
                var nghiPhep = _dbContext.NghiPheps.FirstOrDefault(p => p.id == id && p.TrangThai == "Đang yêu cầu");
                if (nghiPhep != null)
                {
                    return nghiPhep;
                }
                return null;
            }
            catch { return null; }
        }

        public List<NghiPhep> LayDsNghiPhep()
        {
            var db = new PersonnelManagementDataContextDataContext(connectionString);
            return db.NghiPheps.ToList();
        }

        // Lay danh sach Nghi Phep
        //public List<DTONghiPhep> LayDsNghiPhep() => _dbContext.NghiPheps.Select(np => new DTONghiPhep
        //{
        //    ID = np.id,
        //    IDNhanVien = np.idNhanVien,
        //    NgayBatDau = np.NgayBatDau,
        //    NgayKetThuc = np.NgayKetThuc,
        //    LyDoNghi = np.LyDoNghi,
        //    LoaiNghiPhep = np.LoaiNghiPhep,
        //    TrangThai = np.TrangThai

        //}).ToList();

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

                //NghiPhep.NgayBatDau = DTO.NgayBatDau;
                //NghiPhep.NgayKetThuc = DTO.NgayKetThuc;
                NghiPhep.LyDoNghi = DTO.LyDoNghi;
                //NghiPhep.LoaiNghiPhep = DTO.LoaiNghiPhep;
                //NghiPhep.TrangThai = DTO.TrangThai;

                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat trang thai Nghi Phep
        public bool CapNhatTrangThaiNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var NghiPhep = _dbContext.NghiPheps.FirstOrDefault(np => np.id == DTO.ID);

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

        // Tim noi dung ton tai
        public bool TimLyDoNghiTonTai(string reason)
        {
            var checkReason = _dbContext.NghiPheps.Any(np => np.LyDoNghi.ToLower() == reason.ToLower());
            if (checkReason)
            {
                return true;
            }
            return false;
        }

        // Tim don nghi phep duoc duyet, khong duyet
        public bool TrangThaiDonNP(int id)
        {
            var checkStatus = _dbContext.NghiPheps.Any(p => p.id == id && p.TrangThai != "Đang yêu cầu");
            if (checkStatus)
            {
                return true;
            }
            return false;
        }

        // Tim don chua duoc duyet
        public bool TimDonChuaDuyet(string maNV)
        {
            var checkStatus = _dbContext.NghiPheps.Any(p => p.idNhanVien == maNV && p.TrangThai.ToLower().Trim() == "đang yêu cầu");
            if (checkStatus)
            {
                return true;
            }
            return false;
        }

        // Tim doi tuong don chua duoc duyet
        public NghiPhep TimDonNghiPhepChuaDuyet(string maNV)
        {
            var nghiPhep = _dbContext.NghiPheps.FirstOrDefault(p => p.idNhanVien == maNV && p.TrangThai.ToLower().Trim() == "đang yêu cầu");
            if (nghiPhep != null)
            {
                return nghiPhep;
            }
            return null;
        }

        // Tinh so luong ngay nghi
        public TinhLuong TinhSoLuongNgayNghiCoPhep(string maNV, int batDau, int ketThuc, string loai)
        {
            TinhLuong luong = new TinhLuong();
            var nghiPhep = _dbContext.NghiPheps.Where(p => p.LoaiNghiPhep == "Có phép" && p.idNhanVien == maNV && p.LoaiNghiPhep == loai && p.NgayBatDau.Month == DateTime.Now.Month).ToList();

            int soNgayDaNghiCoLuong = 0;
            var soNgayXin = ketThuc - batDau + 1;

            if (nghiPhep.Count == 0)
            {
                soNgayDaNghiCoLuong = 0;
            }
            else soNgayDaNghiCoLuong = nghiPhep.Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiPhep.Count;

            bool nghi12Phep = (nghiPhep.Where(p => p.NgayBatDau.Year == DateTime.Now.Year).Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiPhep.Count) < 13;
            var s = (nghiPhep.Where(p => p.NgayBatDau.Year == DateTime.Now.Year).Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiPhep.Count);
            if (nghi12Phep)
            {
                if (loai.Equals("Có phép", StringComparison.OrdinalIgnoreCase))
                {
                    luong.Loai = loai;
                    int tongNgayNghiTrongThang = soNgayXin + soNgayDaNghiCoLuong;

                    if (tongNgayNghiTrongThang <= 3)
                    {
                        luong.CoPhep = soNgayXin;
                    }
                    else if (tongNgayNghiTrongThang > 3)
                    {
                        luong.CoPhep = 3 - soNgayDaNghiCoLuong;
                        luong.KhongPhep = tongNgayNghiTrongThang - 3;
                    }

                    return luong;
                }
            }

            return new TinhLuong() { KhongPhep = soNgayXin, Loai = loai };
        }
    }


    //// Tinh so luong ngay nghi
    //public int TinhSoLuongNgayNghiCoPhep(string maNV, int batDau, int ketThuc, string loai)
    //{
    //    var nghiPhep = _dbContext.NghiPheps.Where(p => p.idNhanVien == maNV && p.LoaiNghiPhep == loai && p.NgayBatDau.Month == DateTime.Now.Month).ToList();

    //    int soNgayDaNghiCoLuong = 0;
    //    var soNgayXin = ketThuc - batDau + 1;

    //    if (nghiPhep.Count == 0)
    //    {
    //        soNgayDaNghiCoLuong = 0;
    //    }
    //    else soNgayDaNghiCoLuong = nghiPhep.Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiPhep.Count;

    //    if (loai.Equals("Nghỉ phép có lương", StringComparison.OrdinalIgnoreCase))

    //    {
    //        int tongNgayNghiTrongThang = soNgayXin + soNgayDaNghiCoLuong;

    //        return tongNgayNghiTrongThang > 3 ? -1 : 1;
    //    }
    //    else return soNgayXin > 3 ? -1 : 1;

    //}
}

