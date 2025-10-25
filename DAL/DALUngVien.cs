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

        public IQueryable GetUngTuyenByChucVu(int idChucVu)
            => db.UngViens.Where(x => x.idChucVuUngTuyen == idChucVu);

        public IQueryable GetUngTuyenByTuyenDung(int idTuyenDung)
            => db.UngViens.Where(x => x.idTuyenDung == idTuyenDung);

        public bool Update(DTOUngVien dto)
        {
            try
            {
                var check = db.UngViens.FirstOrDefault(x => x.id == dto.Id);
                if (check != null)
                {
                    check.tenNhanVien = dto.TenNhanVien;
                    check.ngaySinh = dto.NgaySinh;
                    check.diaChi = dto.DiaChi;
                    check.que = dto.Que;
                    check.gioiTinh = dto.GioiTinh;
                    check.email = dto.Email;
                    check.duongDanCV = dto.DuongDanCV;
                    check.idChucVuUngTuyen = dto.IdChucVuUngTuyen;
                    check.idTuyenDung = dto.IdTuyenDung;
                    check.ngayUngTuyen = dto.NgayUngTuyen;
                    check.trangThai = dto.TrangThai;

                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var check = db.UngViens.FirstOrDefault(x => x.id == id);
                if (check != null)
                {
                    db.UngViens.DeleteOnSubmit(check);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public string Add(DTOUngVien dto)
        {
            try
            {
                bool exist = db.UngViens.Any(x => x.email == dto.Email);
                if (!exist)
                {
                    UngVien newItem = new UngVien()
                    {
                        tenNhanVien = dto.TenNhanVien,
                        ngaySinh = dto.NgaySinh,
                        diaChi = dto.DiaChi,
                        que = dto.Que,
                        gioiTinh = dto.GioiTinh,
                        email = dto.Email,
                        duongDanCV = dto.DuongDanCV,
                        idChucVuUngTuyen = dto.IdChucVuUngTuyen,
                        idTuyenDung = dto.IdTuyenDung,
                        ngayUngTuyen = dto.NgayUngTuyen,
                        trangThai = dto.TrangThai
                    };

                    db.UngViens.InsertOnSubmit(newItem);
                    db.SubmitChanges();
                    return "passed";
                }
                return "Email already exists";
            }
            catch
            {
                return "failed";
            }
        }
    }

}
