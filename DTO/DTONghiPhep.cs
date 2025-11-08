using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTONghiPhep
    {
        public DTONghiPhep(int id, string idNhanVien, DateTime ngayBatDau, DateTime ngayKetThuc, string lyDoNghi, string loaiNghiPhep, string trangThai)
        {
            ID = id;
            IDNhanVien = idNhanVien;
            NgayBatDau = ngayBatDau;
            NgayKetThuc = ngayKetThuc;
            LyDoNghi = lyDoNghi;
            LoaiNghiPhep = loaiNghiPhep;
            TrangThai = trangThai;
        }

        public DTONghiPhep() { }

        public DTONghiPhep(int id, string idNhanVien)
        {
            ID = id;
            IDNhanVien = idNhanVien;
        }

        public DTONghiPhep(int id, string idNhanVien, string trangThai)
        {
            ID = id;
            IDNhanVien = idNhanVien;
            TrangThai = trangThai;
        }

        public int ID { get; set; }
        public string IDNhanVien { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string LyDoNghi { get; set; }
        public string LoaiNghiPhep { get; set; }
        public string TrangThai { get; set; }
    }
}
