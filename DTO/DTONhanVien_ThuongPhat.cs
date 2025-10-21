using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTONhanVien_ThuongPhat
    {
        public int Id { get; set; }
        public string IdNhanVien { get; set; }
        public string Loai { get; set; }
        public decimal SoTien { get; set; }
        public string LyDo { get; set; }
        public DateTime NgayTao { get; set; }
        public string IdNguoiTao { get; set; }


    }
}
