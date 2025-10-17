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
    public class BLLKyLuat
    {
        private readonly DALKyLuat dal;

        public BLLKyLuat(string conn)
        {
            dal = new DALKyLuat(conn);
        }

        public DataTable GetAll(string idPhongBan = "")
        {
            return dal.GetAll(idPhongBan);
        }
        public DataTable GetDepartments() => dal.GetDepartments();

        public void Save(DTOKyLuat kl, bool isNew)
        {
            if (isNew)
                dal.Insert(kl);
            else
                dal.Update(kl);
        }

        public void Delete(int id)
        {
            dal.Delete(id);
        }
    }
}
