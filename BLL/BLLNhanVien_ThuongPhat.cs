using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLNhanVien_ThuongPhat
    {
        public readonly DALNhanVien_ThuongPhat _dbContext;

        public BLLNhanVien_ThuongPhat(string conn) => _dbContext = new DALNhanVien_ThuongPhat(conn);

        // Danh sach nhan vien_thuong phat
        public List<NhanVien_ThuongPhat> KtraDsNhanVien_ThuongPhat()
        {
            var list = _dbContext.DsNhanVien_ThuongPhat().ToList();
            if (list.Any() && list != null)
            {
                try
                {
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kiểm tra d/s bảng trung gian nhân viên - thưởng phạt: " + ex.Message);
                }
            }
            else return null;
        }
    }
}
