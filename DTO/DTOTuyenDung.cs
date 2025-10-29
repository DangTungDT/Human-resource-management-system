﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOTuyenDung
    {
        public DTOTuyenDung() { }

        public DTOTuyenDung(int id) => ID = id;

        public DTOTuyenDung(int id, string trangThai, DateTime capNhatTG)
        {
            ID = id;
            TrangThai = trangThai;
            NgayTao = capNhatTG;
        }

        public DTOTuyenDung(int id, int iDPhongBan, int iDChucVu, string tieuDe, string iDNguoiTao, string trangThai, DateTime ngayTao, int soLuong)
        {
            ID = id;
            IDPhongBan = iDPhongBan;
            IDChucVu = iDChucVu;
            TieuDe = tieuDe;
            IDNguoiTao = iDNguoiTao;
            TrangThai = trangThai;
            NgayTao = ngayTao;
            SoLuong = soLuong;
        }

        public int ID { get; set; }
        public int IDPhongBan { get; set; }
        public int IDChucVu { get; set; }
        public int SoLuong { get; set; }
        public string TieuDe { get; set; }
        public string IDNguoiTao { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
