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
    public class BLLPhuCap
    {
        private readonly DALPhuCap _phuCapDAL;

        public BLLPhuCap(string stringConnection)
        {
            _phuCapDAL = new DALPhuCap(stringConnection);
            _phuCapDAL = new DALPhuCap(stringConnection);
        }

        // Danh sach phu cap
        public List<PhuCap> KtraDsPhuCap()
        {
            try
            {
                var list = _phuCapDAL.DsPhuCap().ToList();

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

       

        public DataTable GetAll() => _phuCapDAL.GetAll();

        public int InsertPhuCapMoi(string lyDoPhuCap, decimal soTien)
        {
            int idpc = _phuCapDAL.InsertPhuCapMoi( lyDoPhuCap, soTien);
            return idpc; 
        }

        public bool Insert(DTOPhuCap pc)
        {
            if (string.IsNullOrWhiteSpace(pc.LyDoPhuCap))
                return false;
            if (pc.SoTien <= 0)
                return false;
            return _phuCapDAL.Insert(pc);
        }

        public bool Update(int id, DTOPhuCap pc)
        {
            if (string.IsNullOrWhiteSpace(pc.LyDoPhuCap))
                return false;
            if (pc.SoTien <= 0)
                return false;
            return _phuCapDAL.Update(id, pc);
        }

        public bool Delete(int id) => _phuCapDAL.Delete(id);

        public DataTable Search(string keyword) => _phuCapDAL.Search(keyword);
    }

}
