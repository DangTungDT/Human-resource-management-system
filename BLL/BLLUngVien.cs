using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLUngVien
    {
        private DALUngVien dal;

        public BLLUngVien(string conn)
        {
            dal = new DALUngVien(conn);
        }

        public IQueryable GetAll() => dal.GetAll();

        public IQueryable GetUngTuyenByChucVu(int idChucVu)
        {
            if (idChucVu > 0) return dal.GetUngTuyenByChucVu(idChucVu);
            return null;
        }

        public IQueryable GetUngTuyenByTuyenDung(int idTuyenDung)
        {
            if (idTuyenDung > 0) return dal.GetUngTuyenByTuyenDung(idTuyenDung);
            return null;
        }

        public bool Update(DTOUngVien dto)
        {
            if (IsValid(dto))
                return dal.Update(dto);
            return false;
        }

        public bool Delete(int id)
        {
            if (id > 0)
                return dal.Delete(id);
            return false;
        }

        public string Add(DTOUngVien dto)
        {
            if (IsValid(dto))
                return dal.Add(dto);
            return "Invalid data";
        }

        public static bool IsValid(DTOUngVien uv)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|googlemail\.com|[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$";
            string[] trangThaiHopLe = { "Đang xét duyệt", "Loại", "Thử việc", "Đậu" };

            if (uv == null) return false;
            if (string.IsNullOrWhiteSpace(uv.TenNhanVien)) return false;
            if (string.IsNullOrWhiteSpace(uv.Email)) return false;
            if (!Regex.IsMatch(uv.Email, emailPattern)) return false;
            if (uv.NgaySinh > DateTime.Now) return false;
            if (string.IsNullOrWhiteSpace(uv.GioiTinh)) return false;
            if (uv.IdChucVuUngTuyen < 1) return false;
            if (uv.IdTuyenDung < 1) return false;
            if (uv.NgayUngTuyen > DateTime.Now) return false;
            if (string.IsNullOrWhiteSpace(uv.TrangThai) || !trangThaiHopLe.Contains(uv.TrangThai)) return false;

            return true;
        }
    }

}
