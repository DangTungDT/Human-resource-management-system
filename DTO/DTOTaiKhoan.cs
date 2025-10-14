using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOTaiKhoan
    {
        public int Id { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string IdNhanVien { get; set; }
        public string TenNhanVien { get; set; }
    }
}
