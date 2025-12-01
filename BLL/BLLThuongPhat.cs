using DAL;
using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLThuongPhat
    {
        private readonly DALThuongPhat _thuongPhatDAL;
        public BLLThuongPhat(string stringConnection)
        {
            _thuongPhatDAL = new DALThuongPhat(stringConnection);
            _thuongPhatDAL = new DALThuongPhat(stringConnection);
        }

        // Danh sach thuong phat
        public List<ThuongPhat> CheckListThuongPhat()
        {
            var list = _thuongPhatDAL.DanhSachThuongPhat().ToList();
            if (list.Any() && list != null)
            {
                try
                {
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kiểm tra d/s thưởng phạt: " + ex.Message);
                }
            }
            else return null;
        }

        // Danh sach nhan vien     
        public List<DTONhanVien> CheckListNhanVien()
        {
            var list = _thuongPhatDAL.DanhSachNhanVien().ToList();
            if (list.Any() && list != null)
            {
                try
                {
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kiểm tra d/s nhân viên: " + ex.Message);
                }
            }
            else return null;
        }

        public DataTable GetAll()
        {
            return _thuongPhatDAL.GetAll();
        }


        public int Insert(string loai, string lyDo, decimal soTien, string nguoiTao) => _thuongPhatDAL.Insert(loai, lyDo, soTien, nguoiTao);
        public void Update(int id, string loai, string lyDo, decimal soTien) => _thuongPhatDAL.Update(id,loai, lyDo, soTien);
        public void Delete(int id) => _thuongPhatDAL.Delete(id);
    }
}
