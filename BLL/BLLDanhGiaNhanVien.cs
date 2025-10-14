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
        public List<DTODanhGiaNhanVien> CheckListDanhGiaNhanVien()
        {
            var list = _dbContext.DanhSachDanhGiaNV().ToList();
            if (list.Any() && list != null)
            {
                try
                {
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kiểm tra d/s thưởng phạt: " + ex.Message);
                }
            }
            else return null;
        }

        public DataTable GetAll() => dal.GetAll();

        public void Save(DTODanhGiaNhanVien dg, bool isNew)
        {
            if (isNew) dal.Insert(dg);
            else dal.Update(dg);
        }

        public void Delete(int id) => dal.Delete(id);
    }
}
