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

        public DALThuongPhat() => _dbContext = new PersonnelManagementDataContextDataContext();


        public IQueryable<DTOThuongPhat> DanhSachThuongPhat() => _dbContext.ThuongPhats.Select(p => new DTOThuongPhat
        {
            ID = p.id,
            TienThuongPhat = p.tienThuongPhat,
            Loai = p.loai,
            LyDo = p.lyDo,
            IDNguoiTao = p.idNguoiTao
        });

        public IQueryable<DTONhanVien> DanhSachNhanVien() => _dbContext.NhanViens.Select(p => new DTONhanVien
        {
            ID = p.id,
            TenNhanVien = p.TenNhanVien
        });

    }
}
