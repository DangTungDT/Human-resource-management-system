using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOTaiKhoan
    {
        public DTOTaiKhoan() { }

        public DTOTaiKhoan(string idNhanVien, string taiKhoan, string matKhau)
        {
            IdNhanVien = idNhanVien;
            TaiKhoan = taiKhoan;
            MatKhau = matKhau;
        }

        public DTOTaiKhoan(int id, string taiKhoan, string matKhau, string idNhanVien, string tenNhanVien)
        {
            Id = id;
            TaiKhoan = taiKhoan;
            MatKhau = matKhau;
            IdNhanVien = idNhanVien;
            TenNhanVien = tenNhanVien;
        }

        public int Id { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string IdNhanVien { get; set; }
        public string TenNhanVien { get; set; }
    }
}
