using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOHopDongLaoDong
    {
        public DTOHopDongLaoDong() { }

        public DTOHopDongLaoDong(
            int id,
            string loaiHopDong,
            DateTime ngayKy,
            DateTime ngayBatDau,
            DateTime? ngayKetThuc,
            decimal luong,
            string hinhAnh,
            string idNhanVien,
            string moTa)
        {
            Id = id;
            LoaiHopDong = loaiHopDong;
            NgayKy = ngayKy;
            NgayBatDau = ngayBatDau;
            NgayKetThuc = ngayKetThuc;
            Luong = luong;
            HinhAnh = hinhAnh;
            IdNhanVien = idNhanVien;
            MoTa = moTa;
        }

        public int Id { get; set; }
        public string LoaiHopDong { get; set; }
        public DateTime NgayKy { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public decimal Luong { get; set; }
        public string HinhAnh { get; set; }
        public string IdNhanVien { get; set; }
        public string MoTa { get; set; }
    }

}
