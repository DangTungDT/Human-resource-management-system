using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOPhuCap
    {
        public DTOPhuCap() { }

        public DTOPhuCap(int id) => ID = id;

        public DTOPhuCap(int iD, decimal soTien, string lyDoPhuCap)
        {
            ID = iD;
            SoTien = soTien;
            LyDoPhuCap = lyDoPhuCap;
        }

        public int ID { get; set; }
        public decimal SoTien { get; set; }
        public string LyDoPhuCap { get; set; }
    }
}
