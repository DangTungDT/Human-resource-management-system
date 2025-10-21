using DAL;
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
        public readonly DALNhanVien _dal;
        public readonly BLLTaiKhoan _tkBus;

        public BLLNhanVien(string conn)
        {
            _dal = new DALNhanVien(conn);
            _tkBus = new BLLTaiKhoan(conn);
        }

        public DataTable GetDanhSachNhanVien(bool showHidden)
        {
            return _dal.GetAll(showHidden);
        }

        public DataTable GetById(string id)
        {
            return _dal.GetById(id);
        }

        public DataTable ComboboxNhanVien()
        {
            return _dal.LoadNhanVien();
        }

        public DataTable ComboboxNhanVien(int? idPhongBan = null)
        {
            return _dal.ComboboxNhanVien(idPhongBan);
        }

        public DTONhanVien LayThongTin(string idNV)
        {
            return _dal.LayThongTin(idNV);
        }

        public void CapNhatThongTin(DTONhanVien nv)
        {
            if (string.IsNullOrWhiteSpace(nv.TenNhanVien))
                throw new Exception("Tên nhân viên không được để trống!");

            _dal.UpdateNhanVien(nv);
        }

        public bool AddNhanVien(DTONhanVien nv, string tenChucVu, string TenPhongBan)
        {
            nv.ID = _dal.SinhMaNhanVien(tenChucVu, TenPhongBan);
            bool added = _dal.Insert(nv);
            if (added)
                _tkBus.CreateDefaultAccount(nv.ID, nv.TenNhanVien);
            return added;
        }

        public bool UpdateNhanVien(DTONhanVien nv)
        {
            return _dal.Update(nv);
        }

        public void AnNhanVien(string id)
        {
            _dal.AnNhanVien(id);
        }

        public void KhoiPhucNhanVien(string id)
        {
            _dal.KhoiPhucNhanVien(id);
        }

        public NhanVien KtraNhanVienQuaID(string id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("Không có dữ liệu nào trong d/s nhân viên qua id được truyền !");
                }

                return _dal.LayNhanVienQuaID(id);
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
                if (_dal.LayDsNhanVien().Any())
                {
                    return _dal.LayDsNhanVien();
                }
                else throw new Exception("Không có dữ liệu nào trong d/s nhân viên !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy d/s nhân viên : " + ex.Message);
            }
        }
    }
}
