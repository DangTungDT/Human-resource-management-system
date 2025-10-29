using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOChiTietLuong
    {
        public DTOChiTietLuong() { }

        public DTOChiTietLuong(int id) => ID = id;

        public DTOChiTietLuong(int id, string ghiChu)
        {
            ID = id;
            GhiChu = ghiChu;
        }

        public DTOChiTietLuong(int iD, DateTime ngayNhanLuong, decimal luongTruocKhauTru, decimal luongSauKhauTru, decimal tongKhauTru, decimal tongPhuCap, decimal tongKhenThuong, decimal tongTienPhat, string trangThai, string ghiChu, string iDNhanVien, int iDKyLuong, bool capNhatLuong)
        {
            ID = iD;
            NgayNhanLuong = ngayNhanLuong;
            LuongTruocKhauTru = luongTruocKhauTru;
            LuongSauKhauTru = luongSauKhauTru;
            TongKhauTru = tongKhauTru;
            TongPhuCap = tongPhuCap;
            TongKhenThuong = tongKhenThuong;
            TongTienPhat = tongTienPhat;
            TrangThai = trangThai;
            GhiChu = ghiChu;
            IDNhanVien = iDNhanVien;
            IDKyLuong = iDKyLuong;
            CapNhatLuong = capNhatLuong;
        }

        public int ID { get; set; }
        public DateTime NgayNhanLuong { get; set; }
        public decimal LuongTruocKhauTru { get; set; }
        public decimal LuongSauKhauTru { get; set; }
        public decimal TongKhauTru { get; set; }
        public decimal TongPhuCap { get; set; }
        public decimal TongKhenThuong { get; set; }
        public decimal TongTienPhat { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string IDNhanVien { get; set; }
        public int IDKyLuong { get; set; }
        public bool CapNhatLuong { get; set; }

    }
}
