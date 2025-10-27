using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTONhanVien_KhauTru
    {
        public DTONhanVien_KhauTru() { }

        public DTONhanVien_KhauTru(int iD, int iDNhanVien, int iDKhauTru, DateTime thangApDung)
        {
            ID = iD;
            IDNhanVien = iDNhanVien;
            IDKhauTru = iDKhauTru;
            ThangApDung = thangApDung;
        }

        public int ID { get; set; }
        public int IDNhanVien { get; set; }
        public int IDKhauTru { get; set; }
      
        public int Id { get; set; }
        public string IdNhanVien { get; set; }
        public int IdKhauTru { get; set; }
        public DateTime ThangApDung { get; set; }
    }
}
