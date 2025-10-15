using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Schema;

namespace DAL
{
    public class DALNghiPhep
    {
        public PersonnelManagementDataContextDataContext _dbContext;

        public DALNghiPhep(string conn) => _dbContext = new PersonnelManagementDataContextDataContext(conn);


        // Lay danh sach Nghi Phep
        public List<DTONghiPhep> LayDsNghiPhep() => _dbContext.NghiPheps.Select(np => new DTONghiPhep
        {
            ID = np.id,
            IDNhanVien = np.idNhanVien,
            NgayBatDau = np.NgayBatDau,
            NgayKetThuc = np.NgayKetThuc,
            LyDoNghi = np.LyDoNghi,
            LoaiNghiPhep = np.LoaiNghiPhep,
            TrangThai = np.TrangThai

        }).ToList();

        // Them Nghi Phep
        public bool ThemNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var NghiPhep = new NghiPhep()
                {
                    idNhanVien = DTO.IDNhanVien.ToUpper(),
                    NgayBatDau = DTO.NgayBatDau,
                    NgayKetThuc = DTO.NgayKetThuc,
                    LyDoNghi = DTO.LyDoNghi,
                    LoaiNghiPhep = DTO.LoaiNghiPhep,
                    TrangThai = DTO.TrangThai
                };

                _dbContext.NghiPheps.InsertOnSubmit(NghiPhep);
                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat Nghi Phep
        public bool CapNhatNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var NghiPhep = _dbContext.NghiPheps.FirstOrDefault(np => np.id == DTO.ID);

                NghiPhep.NgayBatDau = DTO.NgayBatDau;
                NghiPhep.NgayKetThuc = DTO.NgayKetThuc;
                NghiPhep.LyDoNghi = DTO.LyDoNghi;
                NghiPhep.LoaiNghiPhep = DTO.LoaiNghiPhep;
                NghiPhep.TrangThai = DTO.TrangThai;

                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Xoa Nghi Phep
        public bool XoaNghiPhep(DTONghiPhep DTO)
        {
            try
            {
                var NghiPhep = _dbContext.NghiPheps.FirstOrDefault(p => p.id == DTO.ID);

                _dbContext.NghiPheps.DeleteOnSubmit(NghiPhep);
                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Kiem tra ID nhan vien trong db
        public bool KiemTraIDNhanVien(string idNhanVien)
        {
            var checkIDNV = _dbContext.NhanViens.FirstOrDefault(np => np.id == idNhanVien);
            if (checkIDNV == null) return false;
            return true;
        }

        // Kiem tra ID nghi phep trong db
        public bool KiemTraIDNghiPhep(int idNghiPhep)
        {
            var checkIDNP = _dbContext.NghiPheps.FirstOrDefault(np => np.id == idNghiPhep);
            if (checkIDNP == null) return false;
            return true;
        }
    }
}
