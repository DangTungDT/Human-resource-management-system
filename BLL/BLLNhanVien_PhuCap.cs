using DAL;
using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class BLLNhanVien_PhuCap
    {
        private readonly DALNhanVien_PhuCap _NhanVienPhuCapDAL;

        public BLLNhanVien_PhuCap(string conn)
        {
            _NhanVienPhuCapDAL = new DALNhanVien_PhuCap(conn);
            _NhanVienPhuCapDAL = new DALNhanVien_PhuCap(conn);
        }

        // Lấy danh sách chi tiết
        public DataTable GetAllWithDetails(int? idPhongBan = null)
        {
            try
            {
                return _NhanVienPhuCapDAL.GetNhanVien_PhuCap_WithDetails(idPhongBan);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách phụ cấp: " + ex.Message);
            }
        }

        // Ktra ds NV_PC
        public List<NhanVien_PhuCap> KtraDsNV_PC()
        {
            try
            {
                if (!_NhanVienPhuCapDAL.DsNhanVien_PhuCap().Any())
                {
                    throw new Exception("Không có dữ liệu d/s bảng trung gian nhân viên, phụ cấp !");
                }
                else return _NhanVienPhuCapDAL.DsNhanVien_PhuCap();
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
                var ktraNV_PC = _NhanVienPhuCapDAL.TimNhanVien_PhuCapQuaID(DTO.IDNhanVien, DTO.IDPhuCap);

                if (ktraNV_PC != null)
                {
                    var KtraThemKV_PC = _NhanVienPhuCapDAL.ThemNhanVien_PhuCap(DTO);
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
                var ktraNV_PC = _NhanVienPhuCapDAL.TimNhanVien_PhuCapQuaID(DTO.IDNhanVien, DTO.IDPhuCap);

                if (ktraNV_PC != null)
                {
                    var KtraThemNV_PC = _NhanVienPhuCapDAL.CapNhatNhanVien_PhuCap(DTO);
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
                var ktraNV_PC = _NhanVienPhuCapDAL.TimNhanVien_PhuCapQuaID(DTO.IDNhanVien, DTO.IDPhuCap);

                if (ktraNV_PC != null)
                {
                    var KtraThemNV_PC = _NhanVienPhuCapDAL.XoaNhanVien_PhuCap(DTO);
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
                    var ktraID = _NhanVienPhuCapDAL.TimNhanVien_PhuCapQuaID(idNhanVien, idPhuCap);
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

        // Kiểm tra tồn tại trước khi thêm
        public bool KtraThemNhanVien_PhuCap1(DTONhanVien_PhuCap dto)
        {
            if (string.IsNullOrWhiteSpace(dto.IDNhanVien) || dto.IDPhuCap <= 0)
                return false;

            if (_NhanVienPhuCapDAL.Exists(dto.IDNhanVien, dto.IDPhuCap))
                return false; // Đã tồn tại

            return _NhanVienPhuCapDAL.ThemNhanVien_PhuCap1(dto);
        }

        // Cập nhật trạng thái
        public bool KtraCapNhatNhanVien_PhuCap1(DTONhanVien_PhuCap dto)
        {
            if (string.IsNullOrWhiteSpace(dto.IDNhanVien) || dto.IDPhuCap <= 0)
                return false;

            if (!_NhanVienPhuCapDAL.Exists(dto.IDNhanVien, dto.IDPhuCap))
                return false;

            return _NhanVienPhuCapDAL.CapNhatNhanVien_PhuCap1(dto);
        }

        // Xóa
        public bool KtraXoaNhanVien_PhuCap1(DTONhanVien_PhuCap dto)
        {
            if (string.IsNullOrWhiteSpace(dto.IDNhanVien) || dto.IDPhuCap <= 0)
                return false;

            return _NhanVienPhuCapDAL.XoaNhanVien_PhuCap1(dto);
        }


    }
}
