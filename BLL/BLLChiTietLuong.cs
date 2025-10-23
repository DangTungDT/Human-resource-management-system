using DAL;
using DTO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        // Kiem tra du lieu chi tiet luong cua nhan vien do da duoc them hay chua
        public ChiTietLuong KtraDuLieuChiTietLuongNV(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var nhanVien = _dbContext.TimChiTietLuongQuaIDNhanVien(id);
                    if (nhanVien != null)
                    {
                        var dataChiTietLuong = KtraDsChiTietLuong().FirstOrDefault(p => p.idNhanVien == nhanVien.idNhanVien && p.ngayNhanLuong.Year == DateTime.Now.Year && p.ngayNhanLuong.Month == DateTime.Now.Month + 1);
                        if (dataChiTietLuong != null)
                        {
                            return dataChiTietLuong;
                        }
                        return null;
                    }
                    else throw new Exception($"Không tìm thấy dữ liệu chi tiết lương qua id: {id}");
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm id chi tiết lương: " + ex.Message);
            }
        }

        // Ktra them Chi Tiet Luong
        public bool KtraThemChiTietLuong(DTOChiTietLuong DTO)
        {
            try
            {
                var ktraCTL = _dbContext.TimChiTietLuongQuaID(DTO.ID);

                if (ktraCTL == null)
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

        // Ktra cap nhat Chi Tiet Luong 
        public bool KtraCapNhatChiTietLuongGhiChu(DTOChiTietLuong DTO)
        {
            try
            {
                var ktraCTL = _dbContext.TimChiTietLuongQuaID(DTO.ID);

                if (ktraCTL != null)
                {
                    var KtraThemCTL = _dbContext.CapNhatChiTietLuongGhiChu(DTO);
                    if (KtraThemCTL)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi cập nhật chi tiết lương ghi chú !");
                }
                else throw new Exception("Không tìm thấy dữ liệu chi tiết lương trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật chi tiết lương ghi chú: " + ex.Message);
            }
        }

        // Ktra xoa Chi Tiet Luong
        public bool KtraXoaChiTietLuong(ChiTietLuong DTO)
        {
            try
            {
                var ktraCTL = _dbContext.TimChiTietLuongQuaID(DTO.id);

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
                    else return false;
                }
                else throw new Exception($"Kiểm tra lại dữ liệu đầu vào của id được nhập !");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm id chi tiết lương: " + ex.Message);
            }
        }

        // Tim du lieu Chi Tiet Luong qua id ky luong
        public bool KtraChiTietLuongQuaIDKyLuong(int id)
        {
            try
            {
                if (id > 0)
                {
                    var ktraID = _dbContext.TimChiTietLuongQuaIDKyLuong(id);
                    if (ktraID != null)
                    {
                        return true;
                    }
                    else return false;
                }
                else throw new Exception($"Kiểm tra lại dữ liệu đầu vào của id được nhập !");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm id chi tiết lương kỳ lương: " + ex.Message);
            }
        }

        // Tim du lieu Chi Tiet Luong qua id
        public ChiTietLuong KtraChiTietLuongQuaIDNhanVien(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var dataChiTietLuong = KtraDsChiTietLuong().FirstOrDefault(p => p.idNhanVien == id && p.ngayNhanLuong.Year == DateTime.Now.Year && p.ngayNhanLuong.Month == DateTime.Now.Month + 1);
                    if (dataChiTietLuong != null)
                    {
                        return dataChiTietLuong;
                    }
                    else return null;
                }
                else throw new Exception($"Kiểm tra lại dữ liệu đầu vào của id được nhập !");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm id chi tiết lương nhân viên: " + ex.Message);
            }
        }
    }
}
