using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOPhongBan
    {
        public int Id { get; set; }
        public string TenPhongBan { get; set; }
        public string MoTa { get; set; }

        public DTOPhongBan() { }

        public DTOPhongBan(int id, string tenPhongBan, string moTa)
        {
            Id = id;
            TenPhongBan = tenPhongBan;
            MoTa = moTa;
        }
    }
}
