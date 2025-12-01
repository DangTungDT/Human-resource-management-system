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
        private readonly DALKhenThuong _khenThuongDAL;

        public BLLKhenThuong(string conn)
        {
            _khenThuongDAL = new DALKhenThuong(conn);
        }

        public DataTable GetAll(string idPhongBan = "")
        {
            return _khenThuongDAL.GetAll(idPhongBan);
        }

        public void Save(DTOKhenThuong kt, bool isNew)
        {
            if (isNew)
                _khenThuongDAL.Insert(kt);
            else
                _khenThuongDAL.Update(kt);
        }

        public void Delete(int id)
        {
            _khenThuongDAL.Delete(id);
        }
    }
}