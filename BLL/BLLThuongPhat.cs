using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLThuongPhat
    {
        public readonly DALThuongPhat _dbContext;
        public BLLThuongPhat(string stringConnection)
        {
            _dbContext = new DALThuongPhat(stringConnection);
        }

        // Danh sach thuong phat
        public List<DTOThuongPhat> CheckListThuongPhat()
        {
            var list = _dbContext.DanhSachThuongPhat().ToList();
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
            var list = _dbContext.DanhSachNhanVien().ToList();
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
    }
}
