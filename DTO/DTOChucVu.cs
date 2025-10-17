using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOChucVu
    {
        public int Id { get; set; }
        public string TenChucVu { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal TyLeHoaHong { get; set; }
        public string MoTa { get; set; }
        public int IdPhongBan { get; set; }
    }
}
