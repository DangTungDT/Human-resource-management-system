using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLChamCong
    {
        DALChamCong dal;

        public BLLChamCong(string conn)
        {
            dal = new DALChamCong(conn);
        }

        public IQueryable GetAll() => dal.GetAll();

        public IQueryable GetChamCongByIdNhanVien(string idNhanVien)
        {
            if (!string.IsNullOrEmpty(idNhanVien))
            {
                return dal.GetChamCongByIdNhanVien(idNhanVien);
            }
            return null;
        }
        public bool UpdateGioRa(DTOChamCong dto)
        {
            if (dto.IdNhanVien == null || dto.NgayChamCong.Date != DateTime.Now.Date || dto.GioVao > dto.GioRa)
            {
                return false;
            }
            return dal.UpdateGioRa(dto.IdNhanVien, dto.NgayChamCong, dto.GioRa);
        }
        public bool Update(DTOChamCong dto)
        {
            if (dto.IdNhanVien != null || dto.NgayChamCong != DateTime.Now|| dto.GioVao > dto.GioRa)
            {
                return false;
            }
            return dal.Update(dto);
        }
        public bool Delete(int id)
        {
            if (id < 1)
            {
                return false;
            }
            return dal.Delete(id);
        }
        public string Add(DTOChamCong dto)
        {
            if( dto.NgayChamCong == DateTime.Now.Date ||
                dto.GioVao > DateTime.Now.TimeOfDay ||
                !string.IsNullOrEmpty(dto.IdNhanVien))
            {
                return dal.Add(dto);
            }
            return "invalid data";
        }

        public bool CheckAttendance(string[,] arrIdStaff)
        {
            if(arrIdStaff == null)
            {
                return false;
            }
            return dal.CheckAttendance(arrIdStaff);
        }

        public bool CheckAttendanceOut(string[,] arrIdStaff)
        {
            if (arrIdStaff == null)
            {
                return false;
            }
            return dal.CheckAttendanceOut(arrIdStaff);
        }

        public bool CheckAttendanceOutArr(List<string> arrIdStaff)
        {
            if (arrIdStaff == null)
            {
                return false;
            }
            return dal.CheckAttendanceOutArr(arrIdStaff);
        }
    }
}
