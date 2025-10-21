using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPhuCap
    {
        public readonly DALPhuCap _dbContext;

        public BLLPhuCap(string conn) => _dbContext = new DALPhuCap(conn);

        // Danh sach phu cap
        public List<PhuCap> KtraDsPhuCap()
        {
            try
            {
                var list = _dbContext.DsPhuCap().ToList();

                if (list.Any() && list != null)
                {
                    return list;
                }
                else return null;

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra d/s phụ cấp: " + ex.Message);
            }

        }
    }
}
