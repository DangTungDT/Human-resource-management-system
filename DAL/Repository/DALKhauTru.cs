using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALKhauTru
    {
        public readonly PersonnelManagementDataContext _databaseContext;

        public DALKhauTru(string conn) => _databaseContext = new PersonnelManagementDataContext(conn);

        // Danh sach khau tru
        public List<KhauTru> DsKhauTru() => _databaseContext.KhauTrus.ToList();

        // Tim khau tru qua id
        public KhauTru TimKhauTruQuaID(int id) => _databaseContext.KhauTrus.FirstOrDefault(p => p.id == id);

        // Them khau tru
        public bool ThemKhauTru(DTOKhauTru DTO)
        {
            try
            {
                var khauTru = new KhauTru()
                {
                    loaiKhauTru = DTO.LoaiKhauTru,
                    soTien = DTO.SoTien,
                    moTa = DTO.MoTa,
                    idNguoiTao = DTO.IDNguoiTao
                };

                _databaseContext.KhauTrus.InsertOnSubmit(khauTru);
                _databaseContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat khau tru
        public bool CapNhatKhauTru(DTOKhauTru DTO)
        {
            try
            {
                var NghiPhep = _databaseContext.KhauTrus.FirstOrDefault(np => np.id == DTO.ID);
                if (NghiPhep != null)
                {
                    NghiPhep.loaiKhauTru = DTO.LoaiKhauTru;
                    NghiPhep.soTien = DTO.SoTien;
                    NghiPhep.moTa = DTO.MoTa;

                    _databaseContext.SubmitChanges();

                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        // Xoa khau tru
        public bool XoaKhauTru(DTOKhauTru DTO)
        {
            try
            {
                var khauTru = _databaseContext.KhauTrus.FirstOrDefault(p => p.id == DTO.ID);
                if (khauTru != null)
                {
                    _databaseContext.KhauTrus.DeleteOnSubmit(khauTru);
                    _databaseContext.SubmitChanges();

                    return true;
                }
                else return false;

            }
            catch { return false; }
        }
    }


}
