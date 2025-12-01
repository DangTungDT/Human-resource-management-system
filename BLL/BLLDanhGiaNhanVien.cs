using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLDanhGiaNhanVien
    {
        private readonly DALDanhGiaNhanVien _danhGiaNhanVienDAL;
        public BLLDanhGiaNhanVien(string stringConnection)
        {
            _danhGiaNhanVienDAL = new DALDanhGiaNhanVien(stringConnection);
        }

        // Danh sach danh gia nhan vien
        public List<DTODanhGiaNhanVien> KtraDsDanhGiaNhanVien()
        {
            var list = _danhGiaNhanVienDAL.DanhSachDanhGiaNV()?.ToList();
            if (list != null && list.Any())
            {
                return list;
            }
            return new List<DTODanhGiaNhanVien>();
        }

        public DataTable GetAll() => _danhGiaNhanVienDAL.GetAll();

        public void Save(DTODanhGiaNhanVien dg, bool isNew)
        {
            if (isNew) _danhGiaNhanVienDAL.Insert(dg);
            else _danhGiaNhanVienDAL.Update(dg);
        }

        public void Delete(int id) => _danhGiaNhanVienDAL.Delete(id);

        // Lấy danh sách đánh giá của 1 nhân viên theo tháng
        public List<DTODanhGiaNhanVien> GetByEmployeeAndMonth(string maNV, int month, int year)
        {
            return _danhGiaNhanVienDAL.GetByEmployeeAndMonth(maNV, month, year);
        }

        // Lấy thống kê điểm trung bình theo tháng của tất cả nhân viên
        public DataTable GetMonthlySummary(int month, int year)
        {
            return _danhGiaNhanVienDAL.GetMonthlySummary(month, year);
        }

        // Lấy toàn bộ thống kê theo từng tháng trong năm (phục vụ biểu đồ)
        public DataTable GetYearlySummary(int year)
        {
            return _danhGiaNhanVienDAL.GetYearlySummary(year);
        }

        public DataTable GetDetailedReport(int thang, int nam, string idPhongBan)
        {
            return _danhGiaNhanVienDAL.BaoCaoDanhGiaChiTiet(thang, nam, idPhongBan);
        }
    }
}
