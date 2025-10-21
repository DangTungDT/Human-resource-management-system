using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public  class DALChamCong
    {
        PersonnelManagementDataContextDataContext db;

        public DALChamCong(string conn)
        {
            db = new PersonnelManagementDataContextDataContext(conn);
        }

        public IQueryable GetAll() => db.ChamCongs;

        public IQueryable GetChamCongByIdNhanVien(string idNhanVien) => db.ChamCongs.Where(x => x.idNhanVien == idNhanVien);
        public bool UpdateGioRa(int id, TimeSpan newGioRa)
        {
            try
            {
                ChamCong check = db.ChamCongs.Where(x => x.id == id).FirstOrDefault();
                if (check != null)
                {
                    check.GioRa = newGioRa;
                    db.SubmitChanges();
                    return true;
                }
                return false; ;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(DTOChamCong dto)
        {
            try
            {
                ChamCong check = db.ChamCongs.Where(x => x.id == dto.Id).FirstOrDefault();
                if (check != null)
                {
                    check.NgayChamCong = dto.NgayChamCong;
                    check.GioRa = dto.GioRa;
                    check.GioVao = dto.GioVao;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                ChamCong check = db.ChamCongs.Where(x=> x.id == id).FirstOrDefault();
                if (check != null)
                {
                    db.ChamCongs.DeleteOnSubmit(check);
                    db.SubmitChanges();
                    return true;
                }
                return false; ;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Add(DTOChamCong dto)
        {
            try
            {
                bool check = db.ChamCongs.Any(x => x.idNhanVien == dto.IdNhanVien && x.NgayChamCong == dto.NgayChamCong);
                if (!check)
                {
                    return false;
                }
                ChamCong newItem = new ChamCong()
                {
                    NgayChamCong = dto.NgayChamCong,
                    GioRa = dto.GioRa,
                    idNhanVien = dto.IdNhanVien
                };
                db.ChamCongs.InsertOnSubmit(newItem);
                db.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
