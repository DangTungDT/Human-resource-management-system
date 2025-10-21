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

        public DataTable GetAll(string keyword = "") => dal.GetAll(keyword);
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

            if (chucVu.IdPhongBan <= 0)  return false;

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
    }
}
