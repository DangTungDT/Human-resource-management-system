using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BLL
{
    public class BLLNghiPhep
    {
        public DALNghiPhep _dbContext;

        public BLLNghiPhep(string conn) => _dbContext = new DALNghiPhep(conn);

        // Ktra ds Nghi Phep
        public List<DTONghiPhep> LayDsNghiPhep()
        {
            try
            {
                if (!_dbContext.LayDsNghiPhep().Any())
                {
                    throw new Exception("Không có dữ liệu nào trong d/s nghỉ phép !");
                }
                else return _dbContext.LayDsNghiPhep();

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy d/s nghỉ phép: " + ex.Message);
            }
        }

        // Ktra them Nghi Phep
        public bool KtraThemNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var ktraNV = _dbContext.KiemTraIDNhanVien(DTO.IDNhanVien);

                if (ktraNV)
                {
                    var KtraThemNP = _dbContext.ThemNghiPhep(DTO);
                    if (KtraThemNP)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi thêm nghỉ phép !");
                }
                else throw new Exception("Không tìm thấy dữ liệu nhân viên trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm nghỉ phép: " + ex.Message);
            }
        }

        // Ktra cap nhat nghi phep
        public bool KtraCapNhatNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var ktraNP = _dbContext.KiemTraIDNghiPhep(DTO.ID);
                var ktraNV = _dbContext.KiemTraIDNhanVien(DTO.IDNhanVien);

                if (ktraNV)
                {
                    if (ktraNP)
                    {
                        var KtraThemNP = _dbContext.CapNhatNghiPhep(DTO);
                        if (KtraThemNP)
                        {
                            return true;
                        }
                        else throw new Exception("Lỗi cập nhạt nghỉ phép !");
                    }
                    else throw new Exception("Không tìm thấy dữ liệu nghỉ phép trong hệ thống !");

                }
                else throw new Exception("Không tìm thấy dữ liệu nhân viên trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật nghỉ phép: " + ex.Message);
            }
        }

        // Ktra Xoa nghi phep
        public bool KtraXoaNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var ktraNP = _dbContext.KiemTraIDNghiPhep(DTO.ID);
                var ktraNV = _dbContext.KiemTraIDNhanVien(DTO.IDNhanVien);

                if (ktraNV)
                {
                    if (ktraNP)
                    {
                        var KtraThemNP = _dbContext.XoaNghiPhep(DTO);
                        if (KtraThemNP)
                        {
                            return true;
                        }
                        else throw new Exception("Lỗi xóa nghỉ phép !");
                    }
                    else throw new Exception("Không tìm thấy dữ liệu nghỉ phép trong hệ thống !");

                }
                else throw new Exception("Không tìm thấy dữ liệu nhân viên trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật nghỉ phép: " + ex.Message);
            }
        }

        public List<DTONghiPhep> LayDanhSachNghiPhep(string idNhanVien)
        {
            return _dbContext.LayDanhSachNghiPhep(idNhanVien);
        }
    }
}
