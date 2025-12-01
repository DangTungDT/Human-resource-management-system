using DAL;
using DAL.DataContext;
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
        public DALNghiPhep _nghiPhepDAL;

        public BLLNghiPhep(string conn) => _nghiPhepDAL = new DALNghiPhep(conn);

        // Ktra ds Nghi Phep
        public List<NghiPhep> LayDsNghiPhep()
        {
            try
            {
                if (!_nghiPhepDAL.LayDsNghiPhep().Any())
                {
                    return new List<NghiPhep>();
                }
                else return _nghiPhepDAL.LayDsNghiPhep();

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
                var ktraNV = _nghiPhepDAL.KiemTraIDNhanVien(DTO.IDNhanVien);
                if (ktraNV)
                {
                    var KtraThemNP = _nghiPhepDAL.ThemNghiPhep(DTO);
                    if (KtraThemNP)
                    {
                        return true;
                    }
                    else return false;
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
                var ktraNP = _nghiPhepDAL.KiemTraIDNghiPhep(DTO.ID);
                var ktraNV = _nghiPhepDAL.KiemTraIDNhanVien(DTO.IDNhanVien);

                if (ktraNV)
                {
                    if (ktraNP)
                    {
                        var KtraThemNP = _nghiPhepDAL.CapNhatNghiPhep(DTO);
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
                var ktraNP = _nghiPhepDAL.KiemTraIDNghiPhep(DTO.ID);
                var ktraNV = _nghiPhepDAL.KiemTraIDNhanVien(DTO.IDNhanVien);

                if (ktraNV)
                {
                    if (ktraNP)
                    {
                        var KtraThemNP = _nghiPhepDAL.CapNhatTrangThaiNghiPhep(DTO);
                        if (KtraThemNP)
                        {
                            return true;
                        }
                        else return false;
                    }
                    else return false;

                }
                else throw new Exception("Không tìm thấy dữ liệu nhân viên trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật nghỉ phép: " + ex.Message);
            }
        }

        // Ktra cap nhat trang thai nghi phep   
        public bool KtraCapNhatTrangThaiNghiPhepChoNhieuNV()
        {
            try
            {
                var dsNVNghiPhep = _nghiPhepDAL.LayDsNghiPhep().Where(p => p.NgayBatDau.Date > DateTime.Now.Date && p.TrangThai == "Đang yêu cầu").ToList();
                if (dsNVNghiPhep.Any())
                {
                    var KtraThemNP = _nghiPhepDAL.CapNhatTrangThaiNghiPhepChoNhieuNV();
                    if (KtraThemNP)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
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
                var ktraNP = _nghiPhepDAL.KiemTraIDNghiPhep(DTO.ID);
                var ktraNV = _nghiPhepDAL.KiemTraIDNhanVien(DTO.IDNhanVien);

                if (ktraNV)
                {
                    if (ktraNP)
                    {
                        var KtraThemNP = _nghiPhepDAL.XoaNghiPhep(DTO);
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
            return _nghiPhepDAL.LayDanhSachNghiPhep(idNhanVien);
        }

        // Ktra nghi phep qua trang thai dang yeu cau
        public NghiPhep LayNghiPhepDangYeuCau(int id)
        {
            try
            {
                if (id > 0)
                {
                    var nghiPhep = _nghiPhepDAL.LayNghiPhepDangYeuCau(id);
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

        // Ktra nghi phep qua id
        public NghiPhep KtraNghiPhepQuaID(int id)
        {
            try
            {
                if (id > 0)
                {
                    var nghiPhep = _nghiPhepDAL.LayNghiPhepQuaID(id);
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
                    var checkReason = _nghiPhepDAL.TimLyDoNghiTonTai(reason);
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
                    var checkReason = _nghiPhepDAL.TrangThaiDonNP(id);
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
        public bool KtraTrangThaiDonChuaDuyet(string maNV, int thang, int nam)
        {
            try
            {
                var checkStatus = _nghiPhepDAL.TimDonChuaDuyetTrongThang(maNV, thang, nam);
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
                var nghiPhep = _nghiPhepDAL.TimDonNghiPhepChuaDuyet(maNV);
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
        public TinhLuong KtraTinhSoLuongNgayNghiCoPhep(string maNV, DateTime batDau, DateTime ketThuc, string loai)
        {
            try
            {
                return _nghiPhepDAL.TinhSoLuongNgayNghiCoPhep(maNV, batDau, ketThuc, loai);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tính lương ngày nghỉ phép: " + ex.Message);
            }
        }
    }
}
