using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALHopDongLaoDong
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALHopDongLaoDong(string stringConnection)
        {
            _dbContext = new PersonnelManagementDataContextDataContext(stringConnection);
        }

        // Danh sach hop dong lao dong
        public List<HopDongLaoDong> DsHopDongLaoDong() => _dbContext.HopDongLaoDongs.ToList();
      
        public IQueryable<DTOHopDongLaoDong> DanhSachHopDongLaoDong() =>
        _dbContext.HopDongLaoDongs.Select(p => new DTOHopDongLaoDong
        {
            Id = p.id,
            LoaiHopDong = p.LoaiHopDong,
            NgayKy = p.NgayKy,
            NgayBatDau = p.NgayBatDau,
            NgayKetThuc = p.NgayKetThuc,
            Luong = p.LuongThoaThuan,
            HinhAnh = p.HinhAnh,
            IdNhanVien = p.idNhanVien,
            MoTa = p.MoTa
        });
       
    }
}
