using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALDanhGiaNhanVien
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;   

        public DALDanhGiaNhanVien(string stringConnection) => _dbContext = new PersonnelManagementDataContextDataContext(stringConnection);

        public IQueryable<DTODanhGiaNhanVien> DanhSachDanhGiaNV() => _dbContext.DanhGiaNhanViens.Select(p => new DTODanhGiaNhanVien
        {
            ID = p.id,
            DiemSo = p.DiemSo,
            NhanXet = p.NhanXet,
            NgayTao = p.ngayTao,
            IDNhanVien = p.idNhanVien,
            IDNguoiDanhGia = p.idNguoiDanhGia
        });
    }
}
