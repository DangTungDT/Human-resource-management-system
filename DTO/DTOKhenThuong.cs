using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOKhenThuong
    {
        public int Id { get; set; }
        public string IdNhanVien { get; set; }
        public decimal SoTien { get; set; }
        public string LyDo { get; set; }
        public DateTime NgayThuong { get; set; }
        public string IdNguoiTao { get; set; }
    }
}
