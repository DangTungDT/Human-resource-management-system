using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPhongBan
    {
        private readonly DALPhongBan dal;

        public BLLPhongBan(string conn)
        {
            dal = new DALPhongBan(conn);
        }


        public DTOPhongBan GetDepartmentByID(int id)
        {
            if (id < 1) return null;
            return dal.GetDepartmentByID(id);
        }
        public DTOPhongBan FindPhongBanByIdChucVu(int id)
        {
            if(id < 1) return null;
            return dal.FindPhongBanByIdChucVu(id);
        }
        public List<PhongBan> KtraDsPhongBan()
        {
            try
            {
                var dsPhongBan = dal.LayDsPhongBan();
                if (dsPhongBan.Any() && dsPhongBan.Count > 0)
                {
                    return dsPhongBan;
                }

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public DataTable GetAllPhongBan() => dal.GetAllPhongBan();

        public DataTable ComboboxPhongBan()
        {
            return dal.ComBoBoxPhongBan();
        }

        public string SavePhongBan(DTOPhongBan pb, bool isNew)
        {
            if(pb.MoTa.Length > 255 && pb.TenPhongBan.Length > 255)
            {
                return "Tên và mô tả phòng ban không được dài quá 255 ký tự!";
            }
            if(pb.TenPhongBan.Length > 255)
            {
                return "Tên phòng ban không được dài quá 255 ký tự";
            }
            if(pb.MoTa.Length > 255 )
            {
                return "Mô tả không được dài quá 255 ký tự";
            }
            return isNew ? dal.InsertPhongBan(pb) : dal.UpdatePhongBan(pb);
        }

        public string DeletePhongBan(int id) => dal.DeletePhongBan(id);

        public string KtraPhongBan(int id)
        {
            try
            {
                if (id > 0)
                {
                    var ktraTenPB = dal.LayTenPhongBan(id);
                    if (ktraTenPB != null && !string.IsNullOrEmpty(ktraTenPB))
                    {
                        return ktraTenPB;
                    }
                    else throw new Exception("Không có dữ liệu nào trong d/s phòng ban !");
                }
                else throw new Exception("id phòng ban không tồn tại !");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string TimTenPhongBan(int id)
        {
            try
            {
                var tenPhongBan = dal.LayTenPhongBan(id);

                if (!string.IsNullOrEmpty(tenPhongBan))
                {
                    return tenPhongBan;
                }
                else throw new Exception("Phòng ban không tồn tại !");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
