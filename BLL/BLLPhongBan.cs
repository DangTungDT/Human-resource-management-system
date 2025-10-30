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

        public bool SavePhongBan(DTOPhongBan pb, bool isNew)
        {
            return isNew ? dal.InsertPhongBan(pb) : dal.UpdatePhongBan(pb);
        }

        public bool DeletePhongBan(int id) => dal.DeletePhongBan(id);

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
    }
}
