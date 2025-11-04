using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLHopDongLaoDong
    {
        public readonly DALHopDongLaoDong _dbContext;
        public BLLHopDongLaoDong(string stringConnection)
        {
            _dbContext = new DALHopDongLaoDong(stringConnection);
        }

        public IQueryable GetAll() => _dbContext.GetAll();
        // Danh sach hop dong lao dong
        public List<HopDongLaoDong> KtraDsHopDongLaoDong()
        {
            var list = _dbContext.DsHopDongLaoDong().ToList();
            if (list.Any() && list != null)
            {
                try
                {
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kiểm tra d/s hợp đồng lao động: " + ex.Message);
                }
            }
            else return null;
        }

        // Thêm hợp đồng (trả về true/false)
        public bool Create(DTOHopDongLaoDong dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.LoaiHopDong)) throw new Exception("Loại hợp đồng không được để trống.");
            if (string.IsNullOrWhiteSpace(dto.IdNhanVien)) throw new Exception("Phải chọn nhân viên.");
            if (dto.NgayBatDau > dto.NgayKetThuc && dto.NgayKetThuc != null) throw new Exception("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.");
            return _dbContext.Insert(dto);
        }

        // Sửa hợp đồng
        public bool Edit(DTOHopDongLaoDong dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Id <= 0) throw new Exception("Id hợp đồng không hợp lệ.");
            if (string.IsNullOrWhiteSpace(dto.IdNhanVien)) throw new Exception("Phải chọn nhân viên.");
            return _dbContext.Update(dto);
        }

        // Xóa hợp đồng
        public bool Remove(int id)
        {
            if (id <= 0) throw new Exception("Id hợp đồng không hợp lệ.");
            return _dbContext.Delete(id);
        }

        // Tìm kiếm
        public IQueryable Search(string loaiHopDong, string tenNhanVien)
        {
            return _dbContext.Search(loaiHopDong, tenNhanVien);
        }

    }
}
