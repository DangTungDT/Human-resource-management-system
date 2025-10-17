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
    public class BLLKhenThuong
    {
        private readonly DALKhenThuong dal;

        public BLLKhenThuong(string conn)
        {
            dal = new DALKhenThuong(conn);
        }

        public DataTable GetAll(string idPhongBan = "")
        {
            return dal.GetAll(idPhongBan);
        }

        public void Save(DTOKhenThuong kt, bool isNew)
        {
            if (isNew)
                dal.Insert(kt);
            else
                dal.Update(kt);
        }

        public void Delete(int id)
        {
            dal.Delete(id);
        }
    }
}
