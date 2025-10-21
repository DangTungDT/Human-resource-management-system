using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUngVien
    {
        PersonnelManagementDataContextDataContext db;

        public DALUngVien(string conn)
        {
            db = new PersonnelManagementDataContextDataContext(conn);
        }

        public IQueryable GetAll() => db.UngViens;

        public IQueryable GetUngTuyenByChucVu(int idChucVu) => db.UngViens.Where(x => x.idChucVuUngTuyen == idChucVu);

        public IQueryable GetUngTuyenByTuyenDung(int idTuyenDung) => db.UngViens.Where(x => x.idTuyenDung == idTuyenDung);

        public bool Update(DTOUngVien dto)
        {
            try
            {
                UngVien check = db.UngViens.Where(x => x.id == dto.Id).FirstOrDefault();
                if (check != null)
                {
                    check.hoTen = dto.HoTen;
                    check.email = dto.Email;
                    check.soDienThoai = dto.SoDienThoai;
                    check.duongDanCV = dto.DuongDanCV;
                    check.idChucVuUngTuyen = dto.IdChucVuTuyenDung;
                    check.idTuyenDung = dto.IdTuyenDung;
                    check.ngayUngTuyen = dto.NgayUngTuyen;
                    check.trangThai = dto.TrangThai;
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
                UngVien check = db.UngViens.Where(x => x.id == id).FirstOrDefault();
                if (check != null)
                {
                    db.UngViens.DeleteOnSubmit(check);
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
        public bool Add(DTOUngVien dto)
        {
            try
            {
                bool flag = db.UngViens.Any(x => x.email == dto.Email);
                if(!flag)
                {
                    UngVien newItem = new UngVien()
                    {
                        hoTen = dto.HoTen,
                        email = dto.Email,
                        soDienThoai = dto.SoDienThoai,
                        duongDanCV = dto.DuongDanCV,
                        idChucVuUngTuyen = dto.IdChucVuTuyenDung,
                        idTuyenDung = dto.IdTuyenDung,
                        ngayUngTuyen = dto.NgayUngTuyen,
                        trangThai = dto.TrangThai
                    };
                    db.UngViens.InsertOnSubmit(newItem);
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
    }
}
