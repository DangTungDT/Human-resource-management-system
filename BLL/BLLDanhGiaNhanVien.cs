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
        public readonly DALDanhGiaNhanVien _dbContext;
        private readonly DALDanhGiaNhanVien dal;
        public BLLDanhGiaNhanVien(string stringConnection)
        {
            _dbContext = new DALDanhGiaNhanVien(stringConnection);
            dal = new DALDanhGiaNhanVien(stringConnection);
        }

        // Danh sach danh gia nhan vien
        public List<DTODanhGiaNhanVien> KtraDsDanhGiaNhanVien()
        {
            var list = _dbContext.DanhSachDanhGiaNV()?.ToList();
            if (list != null && list.Any())
            {
                return list;
            }
            return new List<DTODanhGiaNhanVien>();
        }
        public DataTable GetAll(int thang = 0, int nam = 0, int? pb = 0) => dal.GetAll(thang, nam, pb);
        public DataTable GetAllPB(string idDangNhap,int thang,int nam,string searchTen = null,int? pb = null,int? chucVu = null)
        {
            return dal.GetAllPB(idDangNhap, thang, nam, searchTen, pb, chucVu);
        }

        public int Insert(DTODanhGiaNhanVien dg) => dal.Insert(dg);

        public bool Update(DTODanhGiaNhanVien dg) => dal.Update(dg);

        public void Save(DTODanhGiaNhanVien dg, bool isNew)
        {
            if (isNew) dal.Insert(dg);
            else dal.Update(dg);
        }

        public void Delete(int id) => dal.Delete(id);

        // Lấy danh sách đánh giá của 1 nhân viên theo tháng
        public List<DTODanhGiaNhanVien> GetByEmployeeAndMonth(string maNV, int month, int year)
        {
            return dal.GetByEmployeeAndMonth(maNV, month, year);
        }

        // Lấy thống kê điểm trung bình theo tháng của tất cả nhân viên
        public DataTable GetMonthlySummary(int month, int year)
        {
            return dal.GetMonthlySummary(month, year);
        }

        // Lấy toàn bộ thống kê theo từng tháng trong năm (phục vụ biểu đồ)
        public DataTable GetYearlySummary(int year)
        {
            return dal.GetYearlySummary(year);
        }

        public DataTable GetDetailedReport(int thang, int nam, string idPhongBan)
        {
            return dal.BaoCaoDanhGiaChiTiet(thang, nam, idPhongBan);
        }
    }
}
