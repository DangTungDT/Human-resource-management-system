using DAL;
using DAL.DataContext;
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
        public readonly DALHopDongLaoDong _hopDongLaoDongDAL;
        public BLLHopDongLaoDong(string stringConnection)
        {
            _hopDongLaoDongDAL = new DALHopDongLaoDong(stringConnection);
        }

        public IQueryable GetAll() => _hopDongLaoDongDAL.GetAll();
        // Danh sach hop dong lao dong
        public List<HopDongLaoDong> KtraDsHopDongLaoDong()
        {
            var list = _hopDongLaoDongDAL.DsHopDongLaoDong().ToList();
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
            return _hopDongLaoDongDAL.Insert(dto);
        }

        // Sửa hợp đồng
        public bool Edit(DTOHopDongLaoDong dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.Id <= 0) throw new Exception("Id hợp đồng không hợp lệ.");
            if (string.IsNullOrWhiteSpace(dto.IdNhanVien)) throw new Exception("Phải chọn nhân viên.");
            return _hopDongLaoDongDAL.Update(dto);
        }

        // Xóa hợp đồng
        public bool Remove(int id)
        {
            if (id <= 0) throw new Exception("Id hợp đồng không hợp lệ.");
            return _hopDongLaoDongDAL.Delete(id);
        }

        // Tìm kiếm
        public IQueryable Search(string loaiHopDong, string tenNhanVien)
        {
            return _hopDongLaoDongDAL.Search(loaiHopDong, tenNhanVien);
        }

    }
}
