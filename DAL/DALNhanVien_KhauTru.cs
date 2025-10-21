using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALNhanVien_KhauTru
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALNhanVien_KhauTru(string conn) => _dbContext = new PersonnelManagementDataContextDataContext(conn);

        public List<NhanVien_KhauTru> DsNhanVien_KhauTru() => _dbContext.NhanVien_KhauTrus.ToList();
    }
}
