using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLHopDongLaoDong
    {
        public readonly DALHopDongLaoDong _dbContext;
        public BLLHopDongLaoDong(string stringConnection)
        {
            _dbContext = new DALHopDongLaoDong(stringConnection);
        }
        // Danh sach hop dong lao dong
        public List<DTOHopDongLaoDong> CheckListHopDongLaoDong()
        {
            var list = _dbContext.DanhSachHopDongLaoDong().ToList();
            if (list.Any() && list != null)
            {
                try
                {
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kiểm tra d/s hợp đồng lao động: " + ex.Message);
                }
            }
            else return null;
        }
    }
}
