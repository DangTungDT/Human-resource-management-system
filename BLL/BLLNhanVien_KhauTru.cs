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
    public class BLLNhanVien_KhauTru
    {
        public readonly DALNhanVien_KhauTru _NhanVienKhauTrucDAL;

        public BLLNhanVien_KhauTru(string stringConnection)
        {
            dal = new DALNhanVien_KhauTru(stringConnection);
            _NhanVienKhauTrucDAL = new DALNhanVien_KhauTru(stringConnection);
        }

        // Danh sach nhan vien_khau tru
        public List<NhanVien_KhauTru> KtraDsNhanVien_KhauTru()
        {
            var list = _NhanVienKhauTrucDAL.DsNhanVien_KhauTru().ToList();
            if (list.Any() && list != null)
            {
                try
                {
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kiểm tra d/s bảng trung gian nhân viên - khấu trừ: " + ex.Message);
                }
            }
            else return null;
        }
    
        private readonly DALNhanVien_KhauTru dal;


        public DataTable GetAll(string idPhongBan = "")
        {
            return dal.GetAll(idPhongBan);
        }

        public int InsertLyDoMoi(string lyDo, decimal soTien, string idNguoiTao)
        {
            try
            {
                return dal.InsertLyDoMoi(lyDo, soTien, idNguoiTao); // trả về id mới
            }
            catch
            {
                return -1;
            }
        }
        public bool Insert(DTONhanVien_KhauTru nkt)
        {
            if (string.IsNullOrWhiteSpace(nkt.IdNhanVien))
                return false;
            if (nkt.IdKhauTru <= 0)
                return false;
            return dal.Insert(nkt);
        }

        public bool Update(int id, DTONhanVien_KhauTru nkt)
        {
            if (string.IsNullOrWhiteSpace(nkt.IdNhanVien))
                return false;
            if (nkt.IdKhauTru <= 0)
                return false;
            return dal.Update(id, nkt);
        }

        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        public DataTable GetAllLyDo()
        {
            return dal.GetAllLyDo();
        }

        // Helper method to access connection string from DAL
        public string GetConnectionString()
        {
            return dal.GetType().GetField("connectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(dal).ToString();
        }
    }
}
