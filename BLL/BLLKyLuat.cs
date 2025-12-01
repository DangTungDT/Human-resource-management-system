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
        private readonly DALKyLuat _kyLuatDAL;

        public BLLKyLuat(string conn)
        {
            _kyLuatDAL = new DALKyLuat(conn);
        }

        public DataTable GetAll(string idPhongBan = "")
        {
            return _kyLuatDAL.GetAll(idPhongBan);
        }
        public DataTable GetDepartments() => _kyLuatDAL.GetDepartments();

        public void Save(DTOKyLuat kl, bool isNew)
        {
            if (isNew)
                _kyLuatDAL.Insert(kl);
            else
                _kyLuatDAL.Update(kl);
        }

        public void Delete(int id)
        {
            _kyLuatDAL.Delete(id);
        }
    }
}