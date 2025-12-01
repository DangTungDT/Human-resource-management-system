using DAL;
using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLKyLuong
    {
        public readonly DALKyLuong _kyLuongDAL;

        public BLLKyLuong(string conn) => _kyLuongDAL = new DALKyLuong(conn);

        // Ktra ds Ky Luong
        public List<KyLuong> KtraDsKyLuong()
        {
            try
            {
                if (!_kyLuongDAL.DsKyLuong().Any())
                {
                    throw new Exception("Không có dữ liệu d/s kỳ lương !");
                }
                else return _kyLuongDAL.DsKyLuong();

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy d/s kỳ lương: " + ex.Message);
            }
        }

        // Ktra them Ky Luong
        public bool KtraThemKyLuong(DTOKyLuong DTO)
        {
            try
            {
                var ktraKL = _kyLuongDAL.TimKyLuongQuaID(DTO.ID);

                if (ktraKL == null)
                {
                    var KtraThemKL = _kyLuongDAL.ThemKyLuong(DTO);
                    if (KtraThemKL)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi thêm kỳ lương !");
                }
                else throw new Exception("Không tìm thấy dữ liệu kỳ lương trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm kỳ lương: " + ex.Message);
            }
        }

        // Ktra cap nhat Ky Luong
        public bool KtraCapNhatKyLuong(DTOKyLuong DTO)
        {
            try
            {
                var ktraKL = _kyLuongDAL.TimKyLuongQuaID(DTO.ID);

                if (ktraKL != null)
                {
                    var KtraThemKL = _kyLuongDAL.CapNhatKyLuong(DTO);
                    if (KtraThemKL)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi cập nhật kỳ lương !");
                }
                else throw new Exception("Không tìm thấy dữ liệu kỳ lương trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật khấu trừ: " + ex.Message);
            }
        }

        // Ktra xoa Ky Luong
        public bool KtraXoaKyLuong(DTOKyLuong DTO)
        {
            try
            {
                var ktraNL = _kyLuongDAL.TimKyLuongQuaID(DTO.ID);

                if (ktraNL != null)
                {
                    var KtraThemKL = _kyLuongDAL.XoakyLuong(DTO);
                    if (KtraThemKL)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi xóa kỳ lương !");
                }
                else throw new Exception("Không tìm thấy dữ liệu kỳ lương trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật khấu trừ: " + ex.Message);
            }
        }

        // Tim du lieu Ky Luong qua id
        public KyLuong KtraKyLuongQuaID(int id)
        {
            try
            {
                if (id > 0)
                {
                    var kyLuong = _kyLuongDAL.TimKyLuongQuaID(id);
                    if (kyLuong != null)
                    {
                        return kyLuong;
                    }
                    else return null;
                    //else throw new Exception($"Không tìm thấy dữ liệu kỳ lương qua ID {id}");
                }
                else throw new Exception($"Kiểm tra lại dữ liệu đầu vào của id được nhập !");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm id kỳ lương: " + ex.Message);
            }
        }
    }
}
