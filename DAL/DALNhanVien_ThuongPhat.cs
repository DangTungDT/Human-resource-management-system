using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALNhanVien_ThuongPhat
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALNhanVien_ThuongPhat(string conn) => _dbContext = new PersonnelManagementDataContextDataContext(conn);

        public List<NhanVien_ThuongPhat> DsNhanVien_ThuongPhat() => _dbContext.NhanVien_ThuongPhats.ToList();

    }
}
