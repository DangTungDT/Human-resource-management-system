using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTONhanVien_ThuongPhat
    {
        public DTONhanVien_ThuongPhat() { }

        public DTONhanVien_ThuongPhat(int id) => ID = id;

        public DTONhanVien_ThuongPhat(int iD, string iDNhanVien, int iDThuongPhat, DateTime thangApDung)
        {
            ID = iD;
            IDNhanVien = iDNhanVien;
            IDThuongPhat = iDThuongPhat;
            ThangApDung = thangApDung;
        }

        public int ID { get; set; }
        public string IDNhanVien { get; set; }
        public int IDThuongPhat { get; set; }
        public DateTime ThangApDung { get; set; }
    }
}
