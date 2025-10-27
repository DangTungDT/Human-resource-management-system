using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhanVienLuongCT
    {
        public string ID { get; set; }
        public string NhanVien { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
        public string ChucVu { get; set; }
        public bool Checked { get; set; }
    }
}
