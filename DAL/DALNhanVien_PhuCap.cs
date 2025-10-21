using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALNhanVien_PhuCap
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALNhanVien_PhuCap(string conn) => _dbContext = new PersonnelManagementDataContextDataContext(conn);

        // Danh sach NV_PC
        public List<NhanVien_PhuCap> DsNhanVien_PhuCap() => _dbContext.NhanVien_PhuCaps.ToList();

        // Tim NV_PC qua id
        public NhanVien_PhuCap TimNhanVien_PhuCapQuaID(string idNhanVien, int idPhuCap) => _dbContext.NhanVien_PhuCaps.FirstOrDefault(p => p.idNhanVien == idNhanVien && p.idPhuCap == idPhuCap);

        // Them NV_PC
        public bool ThemNhanVien_PhuCap(DTONhanVien_PhuCap DTO)
        {
            try
            {
                var nv_pc = new NhanVien_PhuCap()
                {
                    idNhanVien = DTO.IDNhanVien,
                    idPhuCap = DTO.IDPhuCap,
                    trangThai = DTO.TrangThai,
                };

                _dbContext.NhanVien_PhuCaps.InsertOnSubmit(nv_pc);
                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat NV_PC
        public bool CapNhatNhanVien_PhuCap(DTONhanVien_PhuCap DTO)
        {
            try
            {
                var nv_pc = TimNhanVien_PhuCapQuaID(DTO.IDNhanVien, DTO.IDPhuCap);
                if (nv_pc != null)
                {
                    nv_pc.trangThai = DTO.TrangThai;

                    _dbContext.SubmitChanges();

                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        // Xoa NV_PC
        public bool XoaNhanVien_PhuCap(DTONhanVien_PhuCap DTO)
        {
            try
            {
                var nv_pc = TimNhanVien_PhuCapQuaID(DTO.IDNhanVien, DTO.IDPhuCap);
                if (nv_pc != null)
                {
                    _dbContext.NhanVien_PhuCaps.DeleteOnSubmit(nv_pc);
                    _dbContext.SubmitChanges();

                    return true;
                }
                else return false;

            }
            catch { return false; }
        }
    }
}
