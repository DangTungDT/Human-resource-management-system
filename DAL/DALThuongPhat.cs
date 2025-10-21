using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALThuongPhat
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALThuongPhat(string stringConnection)
        {
            _dbContext = new PersonnelManagementDataContextDataContext(stringConnection);
        }
        public List<ThuongPhat> DanhSachThuongPhat() => _dbContext.ThuongPhats.ToList();


        public IQueryable<DTONhanVien> DanhSachNhanVien() => _dbContext.NhanViens.Select(p => new DTONhanVien
        {
            ID = p.Id,
            TenNhanVien = p.TenNhanVien
        });

    }
}
