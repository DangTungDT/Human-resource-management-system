using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOKhauTru
    {
        public DTOKhauTru() { }

        public DTOKhauTru(int id) => ID = id;

        public DTOKhauTru(int id, string loaiKhauTru, decimal soTien, string moTa, string iDNguoiTao)
        {
            ID = id;
            LoaiKhauTru = loaiKhauTru;
            SoTien = soTien;
            MoTa = moTa;
            IDNguoiTao = iDNguoiTao;
        }

        public int ID { get; set; }
        public string LoaiKhauTru { get; set; }
        public decimal SoTien { get; set; }
        public string MoTa { get; set; }
        public string IDNguoiTao { get; set; }
    }
}
