using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTONhanVien_KhauTru
    {
        public int Id { get; set; }
        public string IdNhanVien { get; set; }
        public int IdKhauTru { get; set; }
        public DateTime ThangApDung { get; set; }
    }
}
