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
            if (dto.Id < 1 || dto.GioVao > dto.GioRa)
            {
                return false;
            }
            return dal.UpdateGioRa(dto.Id, dto.GioRa);
        }
        public bool Update(DTOChamCong dto)
        {
            if (dto.Id < 1 || dto.GioVao > dto.GioRa)
            {
                return false;
            }
            return dal.UpdateGioRa(dto.Id, dto.GioRa);
        }
        public bool Delete(int id)
        {
            if (id < 1)
            {
                return false;
            }
            return dal.Delete(id);
        }
        public bool Add(DTOChamCong dto)
        {
            if( dto.NgayChamCong == DateTime.Now.Date ||
                dto.GioVao > TimeSpan.Parse(DateTime.Now.ToShortTimeString()) ||
                !string.IsNullOrEmpty(dto.IdNhanVien))
            {
                return dal.Add(dto);
            }
            return false;
        }
    }
}
