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

        public string TenNhanVien { get; set; }

        public DateTime NgaySinh { get; set; }

        public string DiaChi { get; set; }

        public string Que { get; set; }

        public string GioiTinh { get; set; }

        public string Email { get; set; }

        public string DuongDanCV { get; set; }

        public int IdChucVuUngTuyen { get; set; }

        public int IdTuyenDung { get; set; }

        public DateTime NgayUngTuyen { get; set; }

        public string TrangThai { get; set; }

        public bool DaXoa { get; set; }

    }
}
