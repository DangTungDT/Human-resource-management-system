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
        private readonly DALNhanVien _dal;
        private readonly BLLTaiKhoan _tkBus;

        public BLLNhanVien(string conn)
        {
            _dal = new DALNhanVien(conn);
            _tkBus = new BLLTaiKhoan(conn);
        }

        public DataTable GetDanhSachNhanVien(bool showHidden)
        {
            return _dal.GetAll(showHidden);
        }

        public DataTable ComboboxNhanVien()
        {
            return _dal.LoadNhanVien();
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
                _tkBus.CreateDefaultAccount(nv.ID);
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

        

    }
}
