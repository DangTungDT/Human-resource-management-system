using DAL;
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
        private readonly DALChucVu dal;

        public BLLChucVu(string conn)
        {
            dal = new DALChucVu(conn);
        }

        //Tìm 1 value dự trên id ChucVu
        public DTOChucVu FindNameById(int id) => dal.FindNameById(id);
        public ChucVu GetPositionByIdStaff(string idStaff)
        {
            if (string.IsNullOrEmpty(idStaff)) return null;
            return dal.GetPositionByIdStaff(idStaff);
        }

        //
        public bool CheckPosition(string namePosition, int departmentId)
        {
            if (string.IsNullOrEmpty(namePosition) || departmentId < 1) return false;
            return dal.CheckPosition(namePosition, departmentId);
        }

        public DataTable GetAll(string keyword = "") => dal.GetAll(keyword);

        public IQueryable GetPositionByDepartment(int id) => dal.GetPositionByDepartment(id);
        public DataTable GetDepartments() => dal.GetDepartments();

        public bool Insert(DTOChucVu cv)
        {
            if (!KiemTraHopLe(cv)) return false;
            return dal.Insert(cv);
        }

        public bool Update(DTOChucVu cv)
        {
            if (!KiemTraHopLe(cv)) return false;
            return dal.Update(cv);
        }

        public void Delete(int id) => dal.Delete(id);

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
                if (!dal.LayDsChucVu().Any())
                {
                    throw new Exception("Không có dữ liệu nào trong d/s chức vụ !");
                }
                else return dal.LayDsChucVu();

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
                var tenChucVu = dal.LayTenChucVu(id);

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
