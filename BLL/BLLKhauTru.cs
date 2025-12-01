using DAL;
using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLKhauTru
    {
        public readonly DALKhauTru _khauTrucDAL;

        public BLLKhauTru(string conn) => _khauTrucDAL = new DALKhauTru(conn);

        // Ktra ds Khau Tru
        public List<KhauTru> KtraDsKhauTru()
        {
            try
            {
                if (!_khauTrucDAL.DsKhauTru().Any())
                {
                    throw new Exception("Không có dữ liệu d/s khấu trừ !");
                }
                else return _khauTrucDAL.DsKhauTru();

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy d/s khấu trừ: " + ex.Message);
            }
        }

        // Ktra them Khau Tru
        public bool KtraThemKhauTru(DTOKhauTru DTO)
        {
            try
            {
                var ktraKT = _khauTrucDAL.TimKhauTruQuaID(DTO.ID);

                if (ktraKT != null)
                {
                    var KtraThemKT = _khauTrucDAL.ThemKhauTru(DTO);
                    if (KtraThemKT)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi thêm khấu trừ !");
                }
                else throw new Exception("Không tìm thấy dữ liệu khấu trừ trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm khấu trừ: " + ex.Message);
            }
        }

        // Ktra cap nhat Khau Tru
        public bool KtraCapNhatKhauTru(DTOKhauTru DTO)
        {
            try
            {
                var ktraKT = _khauTrucDAL.TimKhauTruQuaID(DTO.ID);

                if (ktraKT != null)
                {
                    var KtraThemKT = _khauTrucDAL.CapNhatKhauTru(DTO);
                    if (KtraThemKT)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi cập nhật khấu trừ !");
                }
                else throw new Exception("Không tìm thấy dữ liệu khấu trừ trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật khấu trừ: " + ex.Message);
            }
        }

        // Ktra xoa Khau Tru
        public bool KtraXoaKhauTru(DTOKhauTru DTO)
        {
            try
            {
                var ktraNP = _khauTrucDAL.TimKhauTruQuaID(DTO.ID);

                if (ktraNP != null)
                {

                    var KtraThemKT = _khauTrucDAL.XoaKhauTru(DTO);
                    if (KtraThemKT)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi xóa khấu trừ !");
                }
                else throw new Exception("Không tìm thấy dữ liệu khấu trừ trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật khấu trừ: " + ex.Message);
            }
        }

        // Tim du lieu qua id
        public bool KtraKhauTruQuaID(int id)
        {
            try
            {
                if (id > 0)
                {

                    var ktraID = _khauTrucDAL.TimKhauTruQuaID(id);
                    if (ktraID != null)
                    {
                        return true;
                    }
                    else throw new Exception($"Không tìm thấy dữ liệu khấu trừ qua ID {id}");
                }
                else throw new Exception($"Kiểm tra lại dữ liệu đầu vào của id được nhập !");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm id: " + ex.Message);
            }
        }
    }
}
