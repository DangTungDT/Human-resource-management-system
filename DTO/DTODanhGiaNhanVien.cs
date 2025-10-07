using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTODanhGiaNhanVien
    {
        public DTODanhGiaNhanVien() { }

        public DTODanhGiaNhanVien(int iD, int diemSo, string nhanXet, DateTime ngayTao, string iDNhanVien, string iDNguoiDanhGia)
        {
            ID = iD;
            DiemSo = diemSo;
            NhanXet = nhanXet;
            NgayTao = ngayTao;
            IDNhanVien = iDNhanVien;
            IDNguoiDanhGia = iDNguoiDanhGia;
        }

        public int ID { get; set; }
        public int DiemSo { get; set; }
        public string NhanXet { get; set; }
        public DateTime NgayTao { get; set; }
        public string IDNhanVien { get; set; }
        public string IDNguoiDanhGia { get; set; }
    }
}
