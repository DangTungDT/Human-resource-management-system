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
    public class BLLChucVu
    {
        private readonly DALChucVu dal;

        public BLLChucVu(string conn)
        {
            dal = new DALChucVu(conn);
        }

        public DataTable GetAll(string keyword = "") => dal.GetAll(keyword);
        public DataTable GetDepartments() => dal.GetDepartments();
        public void SaveChucVu(DTOChucVu cv, bool isNew)
        {
            if (isNew)
                dal.Insert(cv);
            else
                dal.Update(cv);
        }

        public void Delete(int id) => dal.Delete(id);
    }
}
