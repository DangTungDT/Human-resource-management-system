using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DAL
{
    public class DALKyLuong
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALKyLuong(string conn) => _dbContext = new PersonnelManagementDataContextDataContext(conn);

        // Danh sach Ky Luong
        public List<KyLuong> DsKyLuong() => _dbContext.KyLuongs.ToList();

        // Tim Ky Luong qua id
        public KyLuong TimKyLuongQuaID(int id) => _dbContext.KyLuongs.FirstOrDefault(p => p.id == id);

        // Them Ky Luong
        public bool ThemKyLuong(DTOKyLuong DTO)
        {
            try
            {
                var kyLuong = new KyLuong()
                {
                    ngayBatDau = DTO.NgayBatDau,
                    ngayKetThuc = DTO.NgayKetThuc,
                    ngayChiTra = DTO.NgayChiTra,
                    trangThai = DTO.TrangThai
                };

                _dbContext.KyLuongs.InsertOnSubmit(kyLuong);
                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat Ky Luong
        public bool CapNhatKyLuong(DTOKyLuong DTO)
        {
            try
            {
                var kyLuong = TimKyLuongQuaID(DTO.ID);
                if (kyLuong != null)
                {
                    kyLuong.ngayBatDau = DTO.NgayBatDau;
                    kyLuong.ngayKetThuc = DTO.NgayKetThuc;
                    kyLuong.ngayChiTra = DTO.NgayChiTra;
                    kyLuong.trangThai = DTO.TrangThai;

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

        // Xoa Ky Luong
        public bool XoakyLuong(DTOKyLuong DTO)
        {
            try
            {
                var kyLuong = TimKyLuongQuaID(DTO.ID);
                if (kyLuong != null)
                {
                    _dbContext.KyLuongs.DeleteOnSubmit(kyLuong);
                    _dbContext.SubmitChanges();

                    return true;
                }
                else return false;

            }
            catch { return false; }
        }
    }
}

