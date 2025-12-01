using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Lifetime;

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
        private string _connectionString;
        public readonly PersonnelManagementDataContext _databaseContext;

        public DALNghiPhep(string conn)
        {
            _connectionString = conn;
            _databaseContext = new PersonnelManagementDataContext(conn);
        }


        public List<DTONghiPhep> LayDanhSachNghiPhep(string idNhanVien)
        {
            List<DTONghiPhep> list = new List<DTONghiPhep>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
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
                var nghiPhep = _databaseContext.NghiPheps.FirstOrDefault(p => p.id == id);
                if (nghiPhep != null)
                {
                    return nghiPhep;
                }
                return null;
            }
            catch { return null; }
        }

        // Lay NghiPhep qua trang thai dang yeu cau
        public NghiPhep LayNghiPhepDangYeuCau(int id)
        {
            try
            {
                var nghiPhep = _databaseContext.NghiPheps.FirstOrDefault(p => p.id == id && p.TrangThai == "Đang yêu cầu");
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
            return _databaseContext.NghiPheps.ToList();
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
                var s = DTO.NgayKetThuc;

                var NghiPhep = new NghiPhep()
                {
                    idNhanVien = DTO.IDNhanVien,
                    NgayBatDau = DTO.NgayBatDau,
                    NgayKetThuc = DTO.NgayKetThuc,
                    LyDoNghi = DTO.LyDoNghi,
                    LoaiNghiPhep = DTO.LoaiNghiPhep,
                    TrangThai = DTO.TrangThai,
                    LoaiTruongHop = DTO.LoaiTruongHop == null || string.IsNullOrEmpty(DTO.LoaiTruongHop)
                                    ? "Nghỉ thường" :
                                    DTO.LoaiTruongHop
                };

                _databaseContext.NghiPheps.InsertOnSubmit(NghiPhep);
                _databaseContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat Nghi Phep
        public bool CapNhatNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var NghiPhep = _databaseContext.NghiPheps.FirstOrDefault(np => np.id == DTO.ID);

                NghiPhep.LoaiNghiPhep = DTO.LoaiNghiPhep;
                NghiPhep.LyDoNghi = DTO.LyDoNghi;
                NghiPhep.LoaiTruongHop = DTO.LoaiTruongHop == null || string.IsNullOrEmpty(DTO.LoaiTruongHop)
                                        ? "Nghỉ thường" :
                                        DTO.LoaiTruongHop;

                _databaseContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat trang thai Nghi Phep
        public bool CapNhatTrangThaiNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var NghiPhep = _databaseContext.NghiPheps.FirstOrDefault(np => np.id == DTO.ID);

                NghiPhep.NgayDanhGia = DTO.NgayDanhGia;
                NghiPhep.LyDoNghi = DTO.LyDoNghi;
                NghiPhep.TrangThai = DTO.TrangThai;

                _databaseContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat trang thai Nghi Phep
        public bool CapNhatTrangThaiNghiPhepChoNhieuNV()
        {
            try
            {
                var dsNhaNVienNP = _databaseContext.NghiPheps.Where(p => p.NgayBatDau.Date < DateTime.Now.Date && p.TrangThai == "Đang yêu cầu").ToList();

                if (dsNhaNVienNP.Any())
                {
                    dsNhaNVienNP.ForEach(p =>
                    {
                        p.TrangThai = "Không duyệt";
                        p.NgayDanhGia = DateTime.Now.Date;
                    });

                    _databaseContext.SubmitChanges();

                    return true;
                }
                else return false;
            }
            catch { return false; }
        }

        // Xoa Nghi Phep
        public bool XoaNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var NghiPhep = _databaseContext.NghiPheps.FirstOrDefault(p => p.id == DTO.ID);

                _databaseContext.NghiPheps.DeleteOnSubmit(NghiPhep);
                _databaseContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Kiem tra ID nhan vien trong db
        public bool KiemTraIDNhanVien(string idNhanVien)
        {
            var checkIDNV = _databaseContext.NhanViens.FirstOrDefault(np => np.id == idNhanVien);
            if (checkIDNV == null) return false;
            return true;
        }

        // Kiem tra ID nghi phep trong db
        public bool KiemTraIDNghiPhep(int idNghiPhep)
        {
            var checkIDNP = _databaseContext.NghiPheps.FirstOrDefault(np => np.id == idNghiPhep);
            if (checkIDNP == null) return false;
            return true;
        }

        // Tim noi dung ton tai
        public bool TimLyDoNghiTonTai(string reason)
        {
            var checkReason = _databaseContext.NghiPheps.Any(np => np.LyDoNghi.ToLower() == reason.ToLower());
            if (checkReason)
            {
                return true;
            }
            return false;
        }

        // Tim don nghi phep duoc duyet, khong duyet
        public bool TrangThaiDonNP(int id)
        {
            var checkStatus = _databaseContext.NghiPheps.Any(p => p.id == id && p.TrangThai != "Đang yêu cầu");
            if (checkStatus)
            {
                return true;
            }
            return false;
        }

        // Tim don chua duoc duyet
        public bool TimDonChuaDuyetTrongThang(string maNV, int thang, int nam)
        {
            var checkStatus = _databaseContext.NghiPheps.Any(p => p.idNhanVien == maNV && p.TrangThai.ToLower().Trim() == "đang yêu cầu" && p.NgayBatDau.Month == thang && p.NgayBatDau.Year == nam);
            if (checkStatus)
            {
                return true;
            }
            return false;
        }

        // Tim doi tuong don chua duoc duyet
        public NghiPhep TimDonNghiPhepChuaDuyet(string maNV)
        {
            var nghiPhep = _databaseContext.NghiPheps.FirstOrDefault(p => p.idNhanVien == maNV && p.TrangThai.ToLower().Trim() == "đang yêu cầu");
            if (nghiPhep != null)
            {
                return nghiPhep;
            }
            return null;
        }

        // Tinh so luong ngay nghi
        public TinhLuong TinhSoLuongNgayNghiCoPhep(string maNV, DateTime batDau, DateTime ketThuc, string loai)
        {
            TinhLuong luong = new TinhLuong();
            var nghiPhep = _databaseContext.NghiPheps.Where(p => p.LoaiNghiPhep == "Có lương" && p.idNhanVien == maNV && p.LoaiNghiPhep == loai && p.NgayBatDau.Month == DateTime.Now.Month).ToList();

            int soNgayDaNghiCoLuong = 0;
            int soNgayXin = (ketThuc.Date - batDau.Date).Days + 1;

            if (nghiPhep.Count == 0)
            {
                soNgayDaNghiCoLuong = 0;
            }
            else soNgayDaNghiCoLuong = nghiPhep.Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiPhep.Count;

            bool nghi12Phep = (nghiPhep.Where(p => p.NgayBatDau.Year == DateTime.Now.Year).Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiPhep.Count) < 13;
            if (nghi12Phep)
            {
                if (loai.Equals("Có lương", StringComparison.OrdinalIgnoreCase))
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

}

