using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTONhanVien
    {
        public DTONhanVien() { }

        public DTONhanVien(string iD, string tenNhanVien)
        {
            ID = iD;
            TenNhanVien = tenNhanVien;
        }

        public string ID { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string Que { get; set; }
        public string Email { get; set; }
        public string IdChucVu { get; set; }
        public string TenChucVu { get; set; }
        public string IdPhongBan { get; set; }
        public string TenPhongBan { get; set; }
        //public string AnhDaiDien { get; set; } // nếu sau này bạn lưu ảnh
    }
}
