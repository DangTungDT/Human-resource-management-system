using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLNhanVien_KhauTru
    {
        public readonly DALNhanVien_KhauTru _dbContext;

        public BLLNhanVien_KhauTru(string conn) => _dbContext = new DALNhanVien_KhauTru(conn);

        // Danh sach nhan vien_khau tru
        public List<NhanVien_KhauTru> KtraDsNhanVien_KhauTru()
        {
            var list = _dbContext.DsNhanVien_KhauTru().ToList();
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
    }
}
