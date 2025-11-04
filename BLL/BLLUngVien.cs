using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public IQueryable GetUCIsDeleted(bool isDeleted, bool inComplete) => dal.GetUCIsDeleted(isDeleted, inComplete);

        public IQueryable GetAll() => dal.GetAll();

        public IQueryable GetFind(string status, string name, int idChucVu)
        {
            //Dữ liệu đầu vào không đúng
            if(status == null || name == null || idChucVu < 0)
            {
                return GetAll();
            }

            return dal.GetFind(status, name, idChucVu);

        }

        public IQueryable GetUngVienStatus(bool flag) => dal.GetUngVienStatus(flag);

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
            if (IsValid(dto) == "Isvalid data")
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
            string result = IsValid(dto);
            if (result == "Isvalid data") return dal.Add(dto);

            return result;
        }

        public static string IsValid(DTOUngVien uv)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|googlemail\.com|[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$";
            string[] trangThaiHopLe = { "đang xét duyệt", "phỏng vấn" ,"loại", "thử việc", "đậu", "trúng tuyển" };

            if (uv == null) return "Invalid data";
            if (string.IsNullOrWhiteSpace(uv.TenNhanVien)) return "Invalid data";
            if (string.IsNullOrWhiteSpace(uv.Email)) return "Invalid data";
            if (!Regex.IsMatch(uv.Email, emailPattern)) return "Invalid data";
            if (uv.NgaySinh > DateTime.Now) return "Invalid data";
            if (string.IsNullOrWhiteSpace(uv.GioiTinh)) return "Invalid data";
            if (uv.IdChucVuUngTuyen < 1) return "Invalid data";
            if (uv.IdTuyenDung < 1) return "Invalid data";
            if (uv.NgayUngTuyen > DateTime.Now) return "Invalid data";
            if (string.IsNullOrWhiteSpace(uv.TrangThai) || !trangThaiHopLe.Contains(uv.TrangThai.ToLower())) return "Invalid data";

            return "Isvalid data";
        }
    }

}
