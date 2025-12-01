using DAL;
using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLNhanVien
    {
        public readonly DALNhanVien _nhanVienDAL;
        public readonly BLLTaiKhoan _taiKhoanBLL;

        public BLLNhanVien(string conn)
        {
            _nhanVienDAL = new DALNhanVien(conn);
            _taiKhoanBLL = new BLLTaiKhoan(conn);
        }

        public IQueryable<NhanVien> LayNhanVienChamCongVe(string idStaff, int idDepartment)
        {
            return _nhanVienDAL.LayNhanVienChamCongVe(idStaff, idDepartment);
        }

        public IQueryable<NhanVien> LayNhanVienQuanLy(string idStaff, int idDepartment)
        {
            return _nhanVienDAL.LayNhanVienQuanLy(idStaff, idDepartment);
        }


        public List<ImageStaff> GetStaffByRole(string idStaff, int idDepartment)
        {
            //Phòng ban không tồn tại
            if (idDepartment < 1)
            {
                return null;
            }

            return _nhanVienDAL.GetStaffByRole(idStaff, idDepartment);

        }
        public DataTable GetDanhSachNhanVien(bool showHidden)
        {
            return _nhanVienDAL.GetAll(showHidden);
        }

        public DTONhanVien GetStaffById(string idStaff)
        {
            if (idStaff != null)
            {
                if (!string.IsNullOrEmpty(idStaff))
                {
                    return _nhanVienDAL.GetStaffById(idStaff);
                }
            }
            return null;
        }

        public DataTable GetById(string id)
        {
            return _nhanVienDAL.GetById(id);
        }

        public DataTable ComboboxNhanVien()
        {
            return _nhanVienDAL.LoadNhanVien();
        }

        public DataTable ComboboxNhanVien(int? idPhongBan = null)
        {
            return _nhanVienDAL.ComboboxNhanVien(idPhongBan);
        }

        public DTONhanVien LayThongTin(string idNV)
        {
            return _nhanVienDAL.LayThongTin(idNV);
        }

        public void CapNhatThongTin(DTONhanVien nv)
        {
            if (string.IsNullOrWhiteSpace(nv.TenNhanVien))
                throw new Exception("Tên nhân viên không được để trống!");

            _nhanVienDAL.UpdateNhanVien(nv);
        }

        public string CreateIdStaff(string tenChucVu, string tenPhongBan)
        {
            if(tenChucVu != null && tenPhongBan != null) return _nhanVienDAL.SinhMaNhanVien(tenChucVu, tenPhongBan);
            return "";
        }

        public bool AddNhanVien(DTONhanVien nv, string tenChucVu, string TenPhongBan)
        {
            nv.ID = _nhanVienDAL.SinhMaNhanVien(tenChucVu, TenPhongBan);
            bool added = _nhanVienDAL.Insert(nv);
            if (added)
                _taiKhoanBLL.CreateDefaultAccount(nv.ID, nv.TenNhanVien, tenChucVu);
            return added;
        }

        public bool UpdateNhanVien(DTONhanVien nv)
        {
            return _nhanVienDAL.Update(nv);
        }

        public void AnNhanVien(string id)
        {
            _nhanVienDAL.AnNhanVien(id);
        }

        public void KhoiPhucNhanVien(string id)
        {
            _nhanVienDAL.KhoiPhucNhanVien(id);
        }

        public NhanVien KtraNhanVienQuaID(string id)
        {
            try
            {
                if (id != null)
                {
                    return _nhanVienDAL.LayNhanVienQuaID(id);
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Ktra ds Nhan Vien
        public List<NhanVien> KtraDsNhanVien()
        {
            try
            {   
                if (_nhanVienDAL.LayDsNhanVien().Any())
                {
                    return _nhanVienDAL.LayDsNhanVien();
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy d/s nhân viên : " + ex.Message);
            }
        }

        public List<ImageStaff> GetStaffByNameEmailCheckin(string name, string email, bool checkin, int idDepartment, string idStaff)
        {
            if (name == null || email == null)
            {
                return null;
            }
            return _nhanVienDAL.GetStaffByNameEmailCheckin(name, email, checkin, idDepartment, idStaff);
        }
        //kiểm tra email
        public bool KiemTraEmailTonTai(string email)
        {
            return _nhanVienDAL.KiemTraEmailTonTai(email);
        }
    }
}
