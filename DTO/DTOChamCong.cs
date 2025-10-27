using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOChamCong
    {
        public int Id { get; set; }
        public DateTime NgayChamCong { get; set; }
        public TimeSpan GioRa { get; set; }
        public TimeSpan GioVao { get; set; }
        public string IdNhanVien { get; set; }
    }
}
