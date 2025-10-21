using DAL;
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
        private readonly DALPhuCap dal;

        public BLLPhuCap(string stringConnection)
        {
            dal = new DALPhuCap(stringConnection);
        }

        public DataTable GetAll() => dal.GetAll();

        public bool Insert(DTOPhuCap pc)
        {
            if (string.IsNullOrWhiteSpace(pc.LyDoPhuCap))
                return false;
            if (pc.SoTien <= 0)
                return false;
            return dal.Insert(pc);
        }

        public bool Update(int id, DTOPhuCap pc)
        {
            if (string.IsNullOrWhiteSpace(pc.LyDoPhuCap))
                return false;
            if (pc.SoTien <= 0)
                return false;
            return dal.Update(id, pc);
        }

        public bool Delete(int id) => dal.Delete(id);

        public DataTable Search(string keyword) => dal.Search(keyword);
    }
}