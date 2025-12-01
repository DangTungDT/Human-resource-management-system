using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class DALChiTietLuong
    {
        public readonly PersonnelManagementDataContext _databaseContext;

        public DALChiTietLuong(string conn) => _databaseContext = new PersonnelManagementDataContext(conn);

        // Danh sach Chi Tiet Luong
        public List<ChiTietLuong> DsChiTietLuong() => _databaseContext.ChiTietLuongs.ToList();

        // Tim Chi Tiet Luong qua id 
        public ChiTietLuong TimChiTietLuongQuaID(int id) => _databaseContext.ChiTietLuongs.FirstOrDefault(p => p.id == id);

        // Tim Chi Tiet Luong qua id ky luong
        public ChiTietLuong TimChiTietLuongQuaIDKyLuong(int idKyLuong) => _databaseContext.ChiTietLuongs.FirstOrDefault(p => p.idKyLuong == idKyLuong);

        // Tim Nhan vien Chi TIet Luong
        public ChiTietLuong TimChiTietLuongQuaIDNhanVien(string idNhanVien) => _databaseContext.ChiTietLuongs.FirstOrDefault(p => p.idNhanVien == idNhanVien);


        // Them Chi Tiet Luong
        public bool ThemChiTietLuong(DTOChiTietLuong DTO)
        {
            try
            {
                var chiTietLuong = new ChiTietLuong()
                {
                    ngayNhanLuong = DTO.NgayNhanLuong,
                    luongTruocKhauTru = DTO.LuongTruocKhauTru,
                    luongSauKhauTru = DTO.LuongSauKhauTru,
                    tongKhauTru = DTO.TongKhauTru,
                    tongPhuCap = DTO.TongPhuCap,
                    tongKhenThuong = DTO.TongKhenThuong,
                    tongTienPhat = DTO.TongTienPhat,
                    trangThai = DTO.TrangThai,
                    ghiChu = DTO.GhiChu,
                    idNhanVien = DTO.IDNhanVien,
                    idKyLuong = DTO.IDKyLuong,
                    capNhatLuong = DTO.CapNhatLuong
                };
                _databaseContext.ChiTietLuongs.InsertOnSubmit(chiTietLuong);
                _databaseContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        public bool ThemDsChiTietLuong(DTOChiTietLuong DTO)
        {
            try
            {
                var layDsNVien = new List<ChiTietLuong>()
                {
                    new ChiTietLuong()
                    {
                        ngayNhanLuong = DTO.NgayNhanLuong,
                        luongTruocKhauTru = DTO.LuongTruocKhauTru,
                        luongSauKhauTru = DTO.LuongSauKhauTru,
                        tongKhauTru = DTO.TongKhauTru,
                        tongPhuCap = DTO.TongPhuCap,
                        tongKhenThuong = DTO.TongKhenThuong,
                        tongTienPhat = DTO.TongTienPhat,
                        trangThai = DTO.TrangThai,
                        ghiChu = DTO.GhiChu,
                        idNhanVien = DTO.IDNhanVien,
                        idKyLuong = DTO.IDKyLuong,
                capNhatLuong = DTO.CapNhatLuong
                    }
                };

                _databaseContext.ChiTietLuongs.InsertAllOnSubmit(layDsNVien);
                _databaseContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat Chi Tiet Luong
        public bool CapNhatChiTietLuong(DTOChiTietLuong DTO)
        {
            try
            {
                var chiTietLuong = TimChiTietLuongQuaID(DTO.ID);
                if (chiTietLuong != null)
                {
                    chiTietLuong.ngayNhanLuong = DTO.NgayNhanLuong;
                    chiTietLuong.luongTruocKhauTru = DTO.LuongTruocKhauTru;
                    chiTietLuong.luongSauKhauTru = DTO.LuongSauKhauTru;
                    chiTietLuong.tongKhauTru = DTO.TongKhauTru;
                    chiTietLuong.tongPhuCap = DTO.TongPhuCap;
                    chiTietLuong.tongKhenThuong = DTO.TongKhenThuong;
                    chiTietLuong.tongTienPhat = DTO.TongTienPhat;
                    chiTietLuong.trangThai = DTO.TrangThai;
                    chiTietLuong.ghiChu = DTO.GhiChu;
                    chiTietLuong.capNhatLuong = DTO.CapNhatLuong;

                    _databaseContext.SubmitChanges();

                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }


        public bool CapNhatChiTietLuongGhiChu(DTOChiTietLuong DTO)
        {
            try
            {
                var chiTietLuong = TimChiTietLuongQuaID(DTO.ID);
                if (chiTietLuong != null)
                {
                    chiTietLuong.ghiChu = DTO.GhiChu;

                    _databaseContext.SubmitChanges();

                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        // Xoa Chi Tiet Luong
        public bool XoaChiTietLuong(ChiTietLuong DTO)
        {
            try
            {
                var chiTietLuong = TimChiTietLuongQuaID(DTO.id);
                if (chiTietLuong != null)
                {
                    _databaseContext.ChiTietLuongs.DeleteOnSubmit(chiTietLuong);
                    _databaseContext.SubmitChanges();

                    return true;
                }
                else return false;

            }
            catch { return false; }
        }
    }
}
