using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DataContext;
using DTO;

namespace BLL
{
    public class BLLChamCong
    {
        DALChamCong _chamCongDAL;

        public BLLChamCong(string conn)
        {
            _chamCongDAL = new DALChamCong(conn);
        }

        public IQueryable GetAll() => _chamCongDAL.GetAll();


        public IQueryable GetChamCongByIdNhanVien(string idNhanVien)
        {
            if (!string.IsNullOrEmpty(idNhanVien))
            {
                return _chamCongDAL.GetChamCongByIdNhanVien(idNhanVien);
            }
            return null;
        }
        public bool UpdateGioRa(DTOChamCong dto)
        {
            if (dto.IdNhanVien == null || dto.NgayChamCong.Date != DateTime.Now.Date || dto.GioVao > dto.GioRa)
            {
                return false;
            }
            return _chamCongDAL.UpdateGioRa(dto.IdNhanVien, dto.NgayChamCong, dto.GioRa);
        }
        public bool Update(DTOChamCong dto)
        {
            if (dto.IdNhanVien != null || dto.NgayChamCong != DateTime.Now || dto.GioVao > dto.GioRa)
            {
                return false;
            }
            return _chamCongDAL.Update(dto);
        }
        public bool Delete(int id)
        {
            if (id < 1)
            {
                return false;
            }
            return _chamCongDAL.Delete(id);
        }
        public string Add(DTOChamCong dto)
        {
            if (dto.NgayChamCong == DateTime.Now.Date ||
                dto.GioVao > DateTime.Now.TimeOfDay ||
                !string.IsNullOrEmpty(dto.IdNhanVien))
            {
                return _chamCongDAL.Add(dto);
            }
            return "invalid data";
        }

        public bool CheckAttendance(string[,] arrIdStaff)
        {
            if (arrIdStaff == null)
            {
                return false;
            }
            return _chamCongDAL.CheckAttendance(arrIdStaff);
        }

        public bool CheckAttendanceOut(string[,] arrIdStaff)
        {
            if (arrIdStaff == null)
            {
                return false;
            }
            return _chamCongDAL.CheckAttendanceOut(arrIdStaff);
        }

        public List<ChamCong> LayDsChamCong() => _chamCongDAL.dsChamCong();
        public bool CheckAttendanceOutArr(List<string> arrIdStaff)
        {
            if (arrIdStaff == null)
            {
                return false;
            }
            return _chamCongDAL.CheckAttendanceOutArr(arrIdStaff);
        }
    }
}
