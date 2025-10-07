using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTONhanVien
    {
        public DTONhanVien() { }

        public DTONhanVien(string iD, string tenNhanVien)
        {
            ID = iD;
            TenNhanVien = tenNhanVien;
        }

        public string ID { get; set; }
        public string TenNhanVien { get; set; }
    }
}
