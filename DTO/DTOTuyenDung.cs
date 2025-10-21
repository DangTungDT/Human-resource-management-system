using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOTuyenDung
    {
        public DTOTuyenDung() { }

        public DTOTuyenDung(int id) => ID = id;

        public DTOTuyenDung(int iD, int iDPhongBan, int iDChucVu, string tieuDe, string iDNguoiTao, string trangThai, DateTime ngayTao)
        {
            ID = iD;
            IDPhongBan = iDPhongBan;
            IDChucVu = iDChucVu;
            TieuDe = tieuDe;
            IDNguoiTao = iDNguoiTao;
            TrangThai = trangThai;
            NgayTao = ngayTao;
        }

        public int ID { get; set; }
        public int IDPhongBan { get; set; }
        public int IDChucVu { get; set; }
        public string TieuDe { get; set; }
        public string IDNguoiTao { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
