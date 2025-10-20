using DAL;
using DTO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLChiTietLuong
    {
        public readonly DALChiTietLuong _dbContext;

        public BLLChiTietLuong(string conn) => _dbContext = new DALChiTietLuong(conn);

        // Ktra ds Chi Tiet Luong
        public List<ChiTietLuong> KtraDsChiTietLuong()
        {
            try
            {
                if (!_dbContext.DsChiTietLuong().Any())
                {
                    throw new Exception("Không có dữ liệu d/s chi tiết lương !");
                }
                else return _dbContext.DsChiTietLuong();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy d/s chi tiết lương: " + ex.Message);
            }
        }

        // Ktra them Chi Tiet Luong
        public bool KtraThemChiTietLuong(DTOChiTietLuong DTO)
        {
            try
            {
                var ktraCTL = _dbContext.TimChiTietLuongQuaID(DTO.ID);

                if (ktraCTL != null)
                {
                    var KtraThemCTL = _dbContext.ThemChiTietLuong(DTO);
                    if (KtraThemCTL)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi thêm chi tiết lương !");
                }
                else throw new Exception("Không tìm thấy dữ liệu chi tiết lương !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm chi tiết lương: " + ex.Message);
            }
        }

        // Ktra cap nhat Chi Tiet Luong 
        public bool KtraCapNhatChiTietLuong(DTOChiTietLuong DTO)
        {
            try
            {
                var ktraCTL = _dbContext.TimChiTietLuongQuaID(DTO.ID);

                if (ktraCTL != null)
                {
                    var KtraThemCTL = _dbContext.CapNhatChiTietLuong(DTO);
                    if (KtraThemCTL)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi cập nhật chi tiết lương !");
                }
                else throw new Exception("Không tìm thấy dữ liệu chi tiết lương trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật chi tiết lương: " + ex.Message);
            }
        }

        // Ktra xoa Chi Tiet Luong
        public bool KtraXoaChiTietLuong(DTOChiTietLuong DTO)
        {
            try
            {
                var ktraCTL = _dbContext.TimChiTietLuongQuaID(DTO.ID);

                if (ktraCTL != null)
                {
                    var KtraThemCTL = _dbContext.XoaChiTietLuong(DTO);
                    if (KtraThemCTL)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi xóa chi tiết lương !");
                }
                else throw new Exception("Không tìm thấy dữ liệu chi tiết lương trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật chi tiết lương: " + ex.Message);
            }
        }

        // Tim du lieu Chi Tiet Luong qua id
        public bool KtraChiTietLuongQuaID(int id)
        {
            try
            {
                if (id > 0)
                {
                    var ktraID = _dbContext.TimChiTietLuongQuaID(id);
                    if (ktraID != null)
                    {
                        return true;
                    }
                    else throw new Exception($"Không tìm thấy dữ liệu chi tiết lương qua id: {id}");
                }
                else throw new Exception($"Kiểm tra lại dữ liệu đầu vào của id được nhập !");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm id chi tiết lương: " + ex.Message);
            }
        }
    }
}
