using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTONhanVien_PhuCap
    {
        public DTONhanVien_PhuCap() { }

        public DTONhanVien_PhuCap(string idNhanVien, int idPhuCap)
        {
            IDNhanVien = idNhanVien;
            IDPhuCap = idPhuCap;
        }

        public DTONhanVien_PhuCap(string iDNhanVien, int iDPhuCap, string trangThai)
        {
            IDNhanVien = iDNhanVien;
            IDPhuCap = iDPhuCap;
            TrangThai = trangThai;
        }

        public string IDNhanVien { get; set; }
        public int IDPhuCap { get; set; }
        public string TrangThai { get; set; }
    }
}
