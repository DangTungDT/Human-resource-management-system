using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUngVien
    {
        private PersonnelManagementDataContext _databaseContext;

        public DALUngVien(string conn)
        {
            _databaseContext = new PersonnelManagementDataContext(conn);
        }


        //Cập nhật trạng thái cho ứng viên thành "Loại" và xóa mềm
        public bool TuChoiUngVienConLai(int idTuyenDung)
        {
            try
            {
                // Lấy danh sách ứng viên thỏa điều kiện và materialize bằng ToList()
                var listUngVien = _databaseContext.UngViens
                    .Where(x => x.idTuyenDung == idTuyenDung && x.trangThai.ToLower() != "thử việc".ToLower())
                    .ToList();

                // Nếu không có bản ghi thì trả về true (không có gì để làm)
                if (!listUngVien.Any())
                    return true;

                // Cập nhật các trường cần thiết
                foreach (var item in listUngVien)
                {
                    item.trangThai = "Loại";
                    item.daXoa = true;
                }

                // Lưu thay đổi vào DB
                _databaseContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Lấy ứng viên thử việc theo idTuyenDung
        public List<DTOUngVien> GetUngVienstatusThuViec(int idTuyenDung)
        {
            return _databaseContext.UngViens.Where(x=> x.trangThai.ToLower() == "thử việc".ToLower() && x.idTuyenDung == idTuyenDung).Select(x=> new DTOUngVien()
            {
                Id = x.id,
                TenNhanVien = x.tenNhanVien,
                NgaySinh = x.ngaySinh,
                DiaChi = x.diaChi,
                Que = x.que,
                GioiTinh = x.gioiTinh,
                Email = x.email,
                DuongDanCV = x.duongDanCV,
                IdChucVuUngTuyen = x.idChucVuUngTuyen,
                IdTuyenDung = x.idTuyenDung,
                NgayUngTuyen = (DateTime)x.ngayUngTuyen,
                TrangThai = x.trangThai,
                DaXoa = x.daXoa
            }).ToList();
        }

        public bool UpdateIsDelete(int id)
        {
            try
            {
                if (id < 1) return false;
                var ungVien = _databaseContext.UngViens.FirstOrDefault(x => x.id == id);
                if (ungVien == null) return false;
                ungVien.daXoa = true;
                _databaseContext.SubmitChanges();
                return true;
            }
            catch (ChangeConflictException)
            {
                // Xử lý xung đột nếu cần: ví dụ resolve hoặc ghi log
                _databaseContext.ChangeConflicts.ResolveAll(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
                return false;
            }
            catch (Exception)
            {
                // Ghi log, thông báo, v.v.
                return false;
            }
        }

        public IQueryable GetFind(string status, string name, int idChucVu)
        {
            try
            {
                var listUngVien = from uv in _databaseContext.UngViens
                                  join cv in _databaseContext.ChucVus on uv.idChucVuUngTuyen equals cv.id
                                  join td in _databaseContext.TuyenDungs on uv.idTuyenDung equals td.id
                                  select new
                                  {
                                      uv.id,
                                      uv.tenNhanVien,
                                      uv.ngaySinh,
                                      uv.diaChi,
                                      uv.que,
                                      uv.gioiTinh,
                                      uv.email,
                                      uv.duongDanCV,
                                      uv.idChucVuUngTuyen,
                                      uv.idTuyenDung,
                                      uv.ngayUngTuyen,
                                      tenChucVu = cv.TenChucVu,
                                      tieuDeTuyenDung = td.tieuDe,
                                      uv.trangThai,
                                      uv.daXoa
                                  };
                if (status != "") listUngVien = listUngVien.Where(x => x.trangThai == status);
                if (name != "") listUngVien = listUngVien.Where(x => x.tenNhanVien == name);
                if (idChucVu != 0) listUngVien = listUngVien.Where(x => x.idChucVuUngTuyen == idChucVu);

                return listUngVien;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable GetUCIsDeleted(bool isDeleted, bool inComplete)
        {
            try
            {
                var listUngVien = from uv in _databaseContext.UngViens
                                  join cv in _databaseContext.ChucVus on uv.idChucVuUngTuyen equals cv.id
                                  join td in _databaseContext.TuyenDungs on uv.idTuyenDung equals td.id
                                  where uv.daXoa == isDeleted
                                  select new
                                  {
                                      uv.id,
                                      uv.tenNhanVien,
                                      uv.ngaySinh,
                                      uv.diaChi,
                                      uv.que,
                                      uv.gioiTinh,
                                      uv.email,
                                      uv.duongDanCV,
                                      uv.idChucVuUngTuyen,
                                      uv.idTuyenDung,
                                      uv.ngayUngTuyen,
                                      tenChucVu = cv.TenChucVu,
                                      tieuDeTuyenDung = td.tieuDe,
                                      uv.trangThai,
                                      uv.daXoa
                                  };
                if (inComplete)
                {
                    // gán lại để filter có hiệu lực
                    listUngVien = listUngVien.Where(x => x.trangThai.ToLower() == "thử việc".ToLower());
                }
                return listUngVien;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable GetAll()
        {
            try
            {
                var listUngVien = from uv in _databaseContext.UngViens
                                  join cv in _databaseContext.ChucVus on uv.idChucVuUngTuyen equals cv.id
                                  join td in _databaseContext.TuyenDungs on uv.idTuyenDung equals td.id
                                  select new
                                  {
                                      uv.id,
                                      uv.tenNhanVien,
                                      uv.ngaySinh,
                                      uv.diaChi,
                                      uv.que,
                                      uv.gioiTinh,
                                      uv.email,
                                      uv.duongDanCV,
                                      uv.idChucVuUngTuyen,
                                      uv.idTuyenDung,
                                      uv.ngayUngTuyen,
                                      tenChucVu = cv.TenChucVu,
                                      tieuDeTuyenDung = td.tieuDe,
                                      uv.trangThai,
                                      uv.daXoa
                                  };
                return listUngVien;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable GetUngVienStatus(bool flag)
        {
            try
            {
                string requestStatus = "Thử việc";
                if (!flag)
                {
                    requestStatus = "Loại";
                }
                var listUngVien = from uv in _databaseContext.UngViens
                                  join cv in _databaseContext.ChucVus on uv.idChucVuUngTuyen equals cv.id
                                  join td in _databaseContext.TuyenDungs on uv.idTuyenDung equals td.id
                                  where uv.trangThai.ToLower() == requestStatus.ToLower()
                                  select new
                                  {
                                      uv.id,
                                      uv.tenNhanVien,
                                      uv.ngaySinh,
                                      uv.diaChi,
                                      uv.que,
                                      uv.gioiTinh,
                                      uv.email,
                                      uv.duongDanCV,
                                      uv.idChucVuUngTuyen,
                                      uv.idTuyenDung,
                                      uv.ngayUngTuyen,
                                      tenChucVu = cv.TenChucVu,
                                      tieuDeTuyenDung = td.tieuDe,
                                      uv.trangThai,
                                      uv.daXoa
                                  };
                return listUngVien;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable GetUngTuyenByChucVu(int idChucVu)
        {
            try
            {
                return _databaseContext.UngViens.Where(x => x.idChucVuUngTuyen == idChucVu);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable GetUngTuyenByTuyenDung(int idTuyenDung)
        {
            try
            {
                return _databaseContext.UngViens.Where(x => x.idTuyenDung == idTuyenDung);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Update(DTOUngVien dto)
        {
            try
            {
                var check = _databaseContext.UngViens.FirstOrDefault(x => x.id == dto.Id);
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
                    check.daXoa = dto.DaXoa;

                    _databaseContext.SubmitChanges();
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
                var check = _databaseContext.UngViens.FirstOrDefault(x => x.id == id);
                if (check != null)
                {
                    _databaseContext.UngViens.DeleteOnSubmit(check);
                    _databaseContext.SubmitChanges();
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
                bool exist = _databaseContext.UngViens.Any(x => x.email == dto.Email);
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

                    _databaseContext.UngViens.InsertOnSubmit(newItem);
                    _databaseContext.SubmitChanges();
                    return "passed";
                }
                return "Email already exists";
            }
            catch
            {
                return "failed";
            }
        }

        public List<UngVien> LayDsUngvien()
        {
            try
            {
                return _databaseContext.UngViens.ToList();
            }
            catch
            {
                return new List<UngVien>();
            }
        }
    }

}