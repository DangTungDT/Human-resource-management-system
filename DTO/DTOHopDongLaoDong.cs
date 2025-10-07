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

        public DTOHopDongLaoDong(int iD, string loaiHopDong, string moTa, DateTime ngayKy, DateTime ngayBatDau, DateTime ngayKetThuc, string iDNhanVien)
        {
            ID = iD;
            LoaiHopDong = loaiHopDong;
            MoTa = moTa;
            NgayKy = ngayKy;
            NgayBatDau = ngayBatDau;
            NgayKetThuc = ngayKetThuc;
            IDNhanVien = iDNhanVien;
        }

        public int ID { get; set; }
        public string LoaiHopDong { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayKy { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string IDNhanVien { get; set; }

    }
}
