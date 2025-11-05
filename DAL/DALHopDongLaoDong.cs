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

        public IQueryable GetAll()
        {
            var list = from hd in _dbContext.HopDongLaoDongs
                       join nv in _dbContext.NhanViens on hd.idNhanVien equals nv.id
                       select new
                       {
                           hd.id,
                           nv.TenNhanVien,
                           nv.NgaySinh,
                           hd.LoaiHopDong,
                           hd.NgayKy,
                           hd.NgayBatDau,
                           hd.NgayKetThuc,
                           hd.Luong,
                           hd.HinhAnh,
                           hd.idNhanVien,
                           hd.MoTa
                       };
            return list;
        }
        public List<HopDongLaoDong> DsHopDongLaoDong() => _dbContext.HopDongLaoDongs.ToList();

        public IQueryable<DTOHopDongLaoDong> DanhSachHopDongLaoDong() =>
        _dbContext.HopDongLaoDongs.Select(p => new DTOHopDongLaoDong
        {
            Id = p.id,
            LoaiHopDong = p.LoaiHopDong,
            NgayKy = p.NgayKy,
            NgayBatDau = p.NgayBatDau,
            NgayKetThuc = p.NgayKetThuc,
            Luong = p.Luong,
            HinhAnh = p.HinhAnh,
            IdNhanVien = p.idNhanVien,
            MoTa = p.MoTa
        });

        // Thêm hợp đồng
        public bool Insert(DTOHopDongLaoDong dto)
        {
            try
            {
                var entity = new HopDongLaoDong
                {
                    LoaiHopDong = dto.LoaiHopDong,
                    NgayKy = dto.NgayKy,
                    NgayBatDau = dto.NgayBatDau,
                    NgayKetThuc = dto.NgayKetThuc,
                    Luong = dto.Luong,
                    HinhAnh = dto.HinhAnh,
                    idNhanVien = dto.IdNhanVien,
                    MoTa = dto.MoTa
                };

                _dbContext.HopDongLaoDongs.InsertOnSubmit(entity);
                _dbContext.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Cập nhật hợp đồng
        public bool Update(DTOHopDongLaoDong dto)
        {
            try
            {
                var entity = _dbContext.HopDongLaoDongs.FirstOrDefault(x => x.id == dto.Id);
                if (entity == null) return false;

                entity.LoaiHopDong = dto.LoaiHopDong;
                entity.NgayKy = dto.NgayKy;
                entity.NgayBatDau = dto.NgayBatDau;
                entity.NgayKetThuc = dto.NgayKetThuc;
                entity.Luong = dto.Luong;
                entity.HinhAnh = dto.HinhAnh;
                entity.idNhanVien = dto.IdNhanVien;
                entity.MoTa = dto.MoTa;

                _dbContext.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Xóa hợp đồng theo id
        public bool Delete(int id)
        {
            try
            {
                var entity = _dbContext.HopDongLaoDongs.FirstOrDefault(x => x.id == id);
                if (entity == null) return false;
                _dbContext.HopDongLaoDongs.DeleteOnSubmit(entity);
                _dbContext.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Tìm kiếm hợp đồng theo loại hợp đồng và/hoặc tên nhân viên (có thể là rỗng)
        public IQueryable Search(string loaiHopDong, string tenNhanVien)
        {
            var q = from hd in _dbContext.HopDongLaoDongs
                    join nv in _dbContext.NhanViens on hd.idNhanVien equals nv.id
                    select new
                    {
                        hd.id,
                        nv.TenNhanVien,
                        nv.NgaySinh,
                        hd.LoaiHopDong,
                        hd.NgayKy,
                        hd.NgayBatDau,
                        hd.NgayKetThuc,
                        hd.Luong,
                        hd.HinhAnh,
                        hd.idNhanVien,
                        hd.MoTa
                    };

            if (!string.IsNullOrWhiteSpace(loaiHopDong))
            {
                q = q.Where(x => x.LoaiHopDong == loaiHopDong);
            }

            if (!string.IsNullOrWhiteSpace(tenNhanVien))
            {
                string lowerName = tenNhanVien.ToLower();
                q = q.Where(x => x.TenNhanVien.ToLower().Contains(lowerName));
            }

            return q;
        }

    }
}
