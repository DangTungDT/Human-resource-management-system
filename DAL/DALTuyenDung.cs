﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALTuyenDung
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALTuyenDung(string conn) => _dbContext = new PersonnelManagementDataContextDataContext(conn);

        // Danh sach Tuyen Dung
        public List<TuyenDung> DsTuyenDung() => _dbContext.TuyenDungs.ToList();

        // Tim Tuyen Dung qua id
        public TuyenDung TimTuyenDungQuaID(int id) => _dbContext.TuyenDungs.FirstOrDefault(p => p.id == id);

        // Tim Tuyen Dung qua idNguoiTao
        public TuyenDung TimTuyenDungQuaIDNV(string id) => _dbContext.TuyenDungs.FirstOrDefault(p => p.idNguoiTao == id);

        // Them Tuyen Dung 
        public bool ThemTuyenDung(DTOTuyenDung DTO)
        {
            try
            {
                var tuyenDung = new TuyenDung()
                {
                    tieuDe = DTO.TieuDe,
                    idPhongBan = DTO.IDPhongBan,
                    idChucVu = DTO.IDChucVu,
                    idNguoiTao = DTO.IDNguoiTao,
                    trangThai = DTO.TrangThai,
                    ngayTao = DTO.NgayTao,
                    soLuong = DTO.SoLuong
                };

                _dbContext.TuyenDungs.InsertOnSubmit(tuyenDung);
                _dbContext.SubmitChanges();

                return true;
            }
            catch { return false; }
        }

        // Cap nhat Tuyen Dung
        public bool CapNhatTuyenDung(DTOTuyenDung DTO)
        {
            try
            {
                var tuyenDung = TimTuyenDungQuaID(DTO.ID);
                if (tuyenDung != null)
                {
                    tuyenDung.tieuDe = DTO.TieuDe;
                    tuyenDung.idPhongBan = DTO.IDPhongBan;
                    tuyenDung.idChucVu = DTO.IDChucVu;
                    tuyenDung.idNguoiTao = DTO.IDNguoiTao;
                    tuyenDung.trangThai = DTO.TrangThai;
                    tuyenDung.ngayTao = DTO.NgayTao;
                    tuyenDung.soLuong = DTO.SoLuong;

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
        // Cap nhat trang thai Tuyen Dung 
        public bool CapNhatTrangThai(DTOTuyenDung DTO)
        {
            try
            {
                var tuyenDung = TimTuyenDungQuaID(DTO.ID);
                if (tuyenDung != null)
                {
                    tuyenDung.trangThai = DTO.TrangThai;
                    tuyenDung.ngayTao = DTO.NgayTao;

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

        // Xoa Tuyen Dung
        public bool XoaTuyenDung(DTOTuyenDung DTO)
        {
            try
            {
                var tuyenDung = TimTuyenDungQuaID(DTO.ID);
                if (tuyenDung != null)
                {
                    _dbContext.TuyenDungs.DeleteOnSubmit(tuyenDung);
                    _dbContext.SubmitChanges();

                    return true;
                }
                else return false;

            }
            catch { return false; }
        }
    }
}
