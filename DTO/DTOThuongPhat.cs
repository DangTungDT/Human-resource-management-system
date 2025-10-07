using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOThuongPhat
    {
        public DTOThuongPhat() { }

        public DTOThuongPhat(int iD, decimal tienThuongPhat, string loai, string lyDo, string iDNguoiTao)
        {
            ID = iD;
            TienThuongPhat = tienThuongPhat;
            Loai = loai;
            LyDo = lyDo;
            IDNguoiTao = iDNguoiTao;
        }

        public int ID { get; set; }
        public decimal TienThuongPhat { get; set; }
        public string Loai { get; set; }
        public string LyDo { get; set; }
        public string IDNguoiTao { get; set; }
    }
}
