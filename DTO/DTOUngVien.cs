using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOUngVien
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DuongDanCV { get; set; }
        public int IdChucVuTuyenDung { get; set; }
        public DateTime NgayUngTuyen { get; set; }
        public string TrangThai { get; set; }
        public int IdTuyenDung { get; set; }

    }
}
