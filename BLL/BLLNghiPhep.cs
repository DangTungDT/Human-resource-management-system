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
        public List<NghiPhep> LayDsNghiPhep()
        {
            try
            {
                if (!_dbContext.LayDsNghiPhep().Any())
                {
                    return new List<NghiPhep>();
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
                        else return false;
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

        // Ktra cap nhat trang thai nghi phep
        public bool KtraCapNhatTrangThaiNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var ktraNP = _dbContext.KiemTraIDNghiPhep(DTO.ID);
                var ktraNV = _dbContext.KiemTraIDNhanVien(DTO.IDNhanVien);

                if (ktraNV)
                {
                    if (ktraNP)
                    {
                        var KtraThemNP = _dbContext.CapNhatTrangThaiNghiPhep(DTO);
                        if (KtraThemNP)
                        {
                            return true;
                        }
                        else return false;
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

        public List<DTONghiPhep> LayDanhSachNghiPhepQuaID(string idNhanVien)
        {
            return _dbContext.LayDanhSachNghiPhep(idNhanVien);
        }

        // Ktra nghi phep qua id
        public NghiPhep KtraNghiPhepQuaID(int id)
        {
            try
            {
                if (id > 0)
                {
                    var nghiPhep = _dbContext.LayNghiPhepQuaID(id);
                    if (nghiPhep != null)
                    {
                        return nghiPhep;
                    }
                    else return null;
                }
                else return null;

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm nghỉ phép: " + ex.Message);
            }
        }

        // Ktra ly do nghi bi trung
        public bool KtraLyDoNghi(string reason)
        {
            try
            {
                if (!string.IsNullOrEmpty(reason))
                {
                    var checkReason = _dbContext.TimLyDoNghiTonTai(reason);
                    if (checkReason)
                    {
                        return true;
                    }
                    else return false;
                }
                else throw new Exception("Không có dữ liệu nào truyền vào !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm lý do nghỉ phép: " + ex.Message);
            }
        }

        // Ktra trang thai Nghi Phep
        public bool KtraTrangThaiNP(int id)
        {
            try
            {
                if (id > 0)
                {
                    var checkReason = _dbContext.TrangThaiDonNP(id);
                    if (checkReason)
                    {
                        return true;
                    }
                    else return false;
                }
                else throw new Exception("Không tìm thấy mã nghỉ phép cần xóa !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm trạng thái nghỉ phép: " + ex.Message);
            }
        }

        // Ktra don don nghi phep moi nhat da duoc duyet chua
        public bool KtraTrangThaiDonChuaDuyet(string maNV)
        {
            try
            {
                var checkStatus = _dbContext.TimDonChuaDuyet(maNV);
                if (checkStatus)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm trạng thái nghỉ phép: " + ex.Message);
            }
        }

        // tra ve doi tuong NghiPhep chua duyet
        public NghiPhep KtraTrangThaiNghiPhepDonChuaDuyet(string maNV)
        {
            try
            {
                var nghiPhep = _dbContext.TimDonNghiPhepChuaDuyet(maNV);
                if (nghiPhep != null)
                {
                    return nghiPhep;
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm trạng thái nghỉ phép: " + ex.Message);
            }
        }

        // Ktra so luong ngay nghi
        public bool KtraTinhSoLuongNgayNghiCoPhep(string maNV, int batDau, int ketThuc, string loai)
        {
            try
            {
                var checkQty = _dbContext.TinhSoLuongNgayNghiCoPhep(maNV, batDau, ketThuc, loai);
                if (checkQty == 1)
                {
                    return true;
                }
                else return false;

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm trạng thái nghỉ phép: " + ex.Message);
            }
        }
    }
}
