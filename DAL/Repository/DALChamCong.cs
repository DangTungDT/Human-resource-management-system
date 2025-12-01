using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DTO;

namespace DAL
{
    public class DALChamCong
    {
        private PersonnelManagementDataContext _databaseContext;

        public DALChamCong(string conn)
        {
            _databaseContext = new PersonnelManagementDataContext(conn);
        }

        public IQueryable GetAll() => _databaseContext.ChamCongs;

        public IQueryable GetChamCongByIdNhanVien(string idNhanVien) => _databaseContext.ChamCongs.Where(x => x.idNhanVien == idNhanVien);

        public bool UpdateGioRa(string idNhanVien, DateTime ngayChamCong, TimeSpan newGioRa)
        {
            try
            {
                ChamCong check = _databaseContext.ChamCongs.Where(x => x.idNhanVien == idNhanVien && x.NgayChamCong.Date == ngayChamCong.Date).FirstOrDefault();
                if (check != null)
                {
                    check.GioRa = newGioRa;
                    _databaseContext.SubmitChanges();
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
                ChamCong check = _databaseContext.ChamCongs.Where(x => x.id == dto.Id).FirstOrDefault();
                if (check != null)
                {
                    check.NgayChamCong = dto.NgayChamCong;
                    check.GioRa = dto.GioRa;
                    check.GioVao = dto.GioVao;
                    _databaseContext.SubmitChanges();
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
                ChamCong check = _databaseContext.ChamCongs.Where(x => x.id == id).FirstOrDefault();
                if (check != null)
                {
                    _databaseContext.ChamCongs.DeleteOnSubmit(check);
                    _databaseContext.SubmitChanges();
                    return true;
                }
                return false; ;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string Add(DTOChamCong dto)
        {
            try
            {
                bool check = _databaseContext.ChamCongs.Any(x => x.idNhanVien == dto.IdNhanVien && x.NgayChamCong == dto.NgayChamCong);
                if (check)
                {
                    return "data already exists";
                }
                ChamCong newItem = new ChamCong()
                {
                    NgayChamCong = dto.NgayChamCong,
                    GioVao = dto.GioVao,
                    idNhanVien = dto.IdNhanVien
                };
                _databaseContext.ChamCongs.InsertOnSubmit(newItem);
                _databaseContext.SubmitChanges();
                return "data added successfully";
            }
            catch (Exception ex)
            {
                return "failed to add data";
            }
        }

        public bool CheckAttendance(string[,] arrIdStaff)
        {
            foreach (string s in arrIdStaff)
            {
                if (s != null)
                {
                    bool check = _databaseContext.ChamCongs.Any(x => x.idNhanVien == s && x.NgayChamCong.Date == DateTime.Now.Date);
                    if (!check) return false;
                }
                else
                {
                    return true;
                }
                continue;
            }
            return true;
        }

        public bool CheckAttendanceOut(string[,] arrIdStaff)
        {
            foreach (string s in arrIdStaff)
            {
                if (s != null)
                {
                    bool check = _databaseContext.ChamCongs.Any(x => x.idNhanVien == s && x.NgayChamCong.Date == DateTime.Now.Date && x.GioRa == null);
                    if (check) return false;
                }
                continue;
            }
            return true;
        }

        // danh sach cham cong
        public List<ChamCong> dsChamCong() => _databaseContext.ChamCongs.ToList();
        public bool CheckAttendanceOutArr(List<string> arrIdStaff)
        {
            foreach (string s in arrIdStaff)
            {
                if (s != null)
                {
                    bool check = _databaseContext.ChamCongs.Any(x => x.idNhanVien == s && x.NgayChamCong.Date == DateTime.Now.Date && x.GioRa == null);
                    if (check) return false;
                }
                continue;
            }
            return true;
        }

    }
}
