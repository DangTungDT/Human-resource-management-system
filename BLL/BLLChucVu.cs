using DAL;
using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLChucVu
    {
        private readonly DALChucVu _chucVuDAL;

        public BLLChucVu(string conn)
        {
            _chucVuDAL = new DALChucVu(conn);
        }

        //Tìm 1 value dự trên id ChucVu
        public DTOChucVu FindNameById(int id) => _chucVuDAL.FindNameById(id);
        public ChucVu GetPositionByIdStaff(string idStaff)
        {
            if (string.IsNullOrEmpty(idStaff)) return null;
            return _chucVuDAL.GetPositionByIdStaff(idStaff);
        }

        //
        public bool CheckPosition(string namePosition, int departmentId)
        {
            if (string.IsNullOrEmpty(namePosition) || departmentId < 1) return false;
            return _chucVuDAL.CheckPosition(namePosition, departmentId);
        }

        public DataTable GetAll(string keyword = "") => _chucVuDAL.GetAll(keyword);

        public IQueryable GetPositionByDepartment(int id) => _chucVuDAL.GetPositionByDepartment(id);
        public DataTable GetDepartments() => _chucVuDAL.GetDepartments();

        public bool Insert(DTOChucVu cv)
        {
            if (!KiemTraHopLe(cv)) return false;
            return _chucVuDAL.Insert(cv);
        }

        public bool Update(DTOChucVu cv)
        {
            if (!KiemTraHopLe(cv)) return false;
            return _chucVuDAL.Update(cv);
        }

        public void Delete(int id) => _chucVuDAL.Delete(id);

        public static bool KiemTraHopLe(DTOChucVu chucVu)
        {
            if (chucVu == null) return false;

            if (string.IsNullOrWhiteSpace(chucVu.TenChucVu)) return false;

            if (chucVu.LuongCoBan < 0) return false;

            if (chucVu.TyLeHoaHong < 0) return false;

            if (chucVu.IdPhongBan <= 0) return false;

            return true;
        }
        // Ktra ds Nghi Phep
        public List<ChucVu> LayDsChucVu()
        {
            try
            {
                if (!_chucVuDAL.LayDsChucVu().Any())
                {
                    throw new Exception("Không có dữ liệu nào trong d/s chức vụ !");
                }
                else return _chucVuDAL.LayDsChucVu();

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy d/s chức vụ: " + ex.Message);
            }
        }

        public string TimTenChucVu(int id)
        {
            try
            {
                var tenChucVu = _chucVuDAL.LayTenChucVu(id);

                if (!string.IsNullOrEmpty(tenChucVu))
                {
                    return tenChucVu;
                }
                else throw new Exception("Chức vụ không tồn tại !");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
