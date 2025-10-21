using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLNhanVien_PhuCap
    {
        public readonly DALNhanVien_PhuCap _dbContext;

        public BLLNhanVien_PhuCap(string conn) => _dbContext = new DALNhanVien_PhuCap(conn);

        // Ktra ds NV_PC
        public List<NhanVien_PhuCap> KtraDsNV_PC()
        {
            try
            {
                if (!_dbContext.DsNhanVien_PhuCap().Any())
                {
                    throw new Exception("Không có dữ liệu d/s bảng trung gian nhân viên, phụ cấp !");
                }
                else return _dbContext.DsNhanVien_PhuCap();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy d/s bảng trung gian nhân viên, phụ cấp : " + ex.Message);
            }
        }

        // Ktra them NV_PC
        public bool KtraThemNhanVien_PhuCap(DTONhanVien_PhuCap DTO)
        {
            try
            {
                var ktraNV_PC = _dbContext.TimNhanVien_PhuCapQuaID(DTO.IDNhanVien, DTO.IDPhuCap);

                if (ktraNV_PC != null)
                {
                    var KtraThemKV_PC = _dbContext.ThemNhanVien_PhuCap(DTO);
                    if (KtraThemKV_PC)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi thêm bảng trung gian nhân viên, phụ cấp !");
                }
                else throw new Exception("Không tìm thấy dữ liệu bảng trung gian nhân viên, phụ cấp trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm bảng trung gian nhân viên, phụ cấp: " + ex.Message);
            }
        }

        // Ktra cap nhat NV_PC
        public bool KtraCapNhatNhanVien_PhuCap(DTONhanVien_PhuCap DTO)
        {
            try
            {
                var ktraNV_PC = _dbContext.TimNhanVien_PhuCapQuaID(DTO.IDNhanVien, DTO.IDPhuCap);

                if (ktraNV_PC != null)
                {
                    var KtraThemNV_PC = _dbContext.CapNhatNhanVien_PhuCap(DTO);
                    if (KtraThemNV_PC)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi cập nhật bảng trung gian nhân viên, phụ cấp !");
                }
                else throw new Exception("Không tìm thấy dữ liệu bảng trung gian nhân viên, phụ cấp trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật bảng trung gian nhân viên, phụ cấp: " + ex.Message);
            }
        }

        // Ktra xoa NV_PC
        public bool KtraXoaNhanVien_PhuCap(DTONhanVien_PhuCap DTO)
        {
            try
            {
                var ktraNV_PC = _dbContext.TimNhanVien_PhuCapQuaID(DTO.IDNhanVien, DTO.IDPhuCap);

                if (ktraNV_PC != null)
                {
                    var KtraThemNV_PC = _dbContext.XoaNhanVien_PhuCap(DTO);
                    if (KtraThemNV_PC)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi xóa bảng trung gian nhân viên, phụ cấp !");
                }
                else throw new Exception("Không tìm thấy dữ liệu bảng trung gian nhân viên, phụ cấp trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật bảng trung gian nhân viên, phụ cấp: " + ex.Message);
            }
        }

        // Tim du lieu NV_PC qua id
        public bool KtraNhanVien_PhuCapQuaID(string idNhanVien, int idPhuCap)
        {
            try
            {
                if (!string.IsNullOrEmpty(idNhanVien) && idPhuCap > 0)
                {
                    var ktraID = _dbContext.TimNhanVien_PhuCapQuaID(idNhanVien, idPhuCap);
                    if (ktraID != null)
                    {
                        return true;
                    }
                    else throw new Exception($"Không tìm thấy dữ liệu bảng trung gian nhân viên, phụ cấp qua ID nhân viên: {idNhanVien} và ID phụ cấp: {idPhuCap}");
                }
                else throw new Exception($"Kiểm tra lại dữ liệu đầu vào của id được nhập !");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm id bảng trung gian nhân viên, phụ cấp: " + ex.Message);
            }
        }
    }
}
