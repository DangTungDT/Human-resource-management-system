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
    public class BLLPhongBan
    {
        private readonly DALPhongBan dal;

        public BLLPhongBan(string conn)
        {
            dal = new DALPhongBan(conn);
        }

        public DataTable GetAllPhongBan() => dal.GetAllPhongBan();

        public bool SavePhongBan(DTOPhongBan pb, bool isNew)
        {
            return isNew ? dal.InsertPhongBan(pb) : dal.UpdatePhongBan(pb);
        }

        public bool DeletePhongBan(int id) => dal.DeletePhongBan(id);
    }
}
