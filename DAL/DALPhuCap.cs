using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPhuCap
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALPhuCap(string conn) => _dbContext = new PersonnelManagementDataContextDataContext(conn);

        public List<PhuCap> DsPhuCap() => _dbContext.PhuCaps.ToList();
    }
}
