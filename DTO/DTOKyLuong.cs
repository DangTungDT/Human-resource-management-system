using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOKyLuong
    {
        public DTOKyLuong() { }

        public DTOKyLuong(int id) => ID = id;

        public DTOKyLuong(int id, DateTime ngayBatDau, DateTime ngayKetThuc, DateTime ngayChiTra, string trangThai)
        {
            ID = id;
            NgayBatDau = ngayBatDau;
            NgayKetThuc = ngayKetThuc;
            NgayChiTra = ngayChiTra;
            TrangThai = trangThai;
        }

        public int ID { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public DateTime NgayChiTra { get; set; }
        public string TrangThai { get; set; }
    }
}
