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
        public string IDNhanVien { get; set; }
        public string IDNguoiDanhGia { get; set; }
        public decimal DiemChuyenCan { get; set; } = 5m;
        public decimal DiemNangLuc { get; set; } = 5m;
        public decimal TongDiem => DiemChuyenCan + DiemNangLuc;
        public int DiemSo { get; set; } = 0; // 1-10 lưu nếu muốn
        public string NhanXet { get; set; }
        public System.DateTime NgayTao { get; set; } = System.DateTime.Now;
    }
}
