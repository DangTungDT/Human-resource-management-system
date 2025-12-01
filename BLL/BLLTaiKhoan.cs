using DAL;
using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BLL
{
    public class BLLTaiKhoan
    {
        private readonly DALTaiKhoan _taiKhoanDAL;
        private readonly DALNhanVien _nhanVienDAL;

        public BLLTaiKhoan(string conn)
        {
            _nhanVienDAL = new DALNhanVien(conn);
            _taiKhoanDAL = new DALTaiKhoan(conn);
        }

        public DataTable GetAllAccounts() => _taiKhoanDAL.GetAll();

        public void SaveAccount(DTOTaiKhoan tk, bool isNew)
        {
            tk.MatKhau = HashPassword(tk.MatKhau);
            if (isNew)
                _taiKhoanDAL.Insert(tk);
            else
                _taiKhoanDAL.Update(tk);
        }

        public void DeleteAccount(int id) => _taiKhoanDAL.Delete(id);

        public void CreateDefaultAccount(string idNV, string tenNhanVien, string tenChucVu)
        {
            _taiKhoanDAL.CreateDefaultAccount(idNV, tenNhanVien, tenChucVu);
        }

        public bool ValidateLogin(string taiKhoan, string matKhau)
        {
            DataTable dt = _taiKhoanDAL.GetByTaiKhoan(taiKhoan);
            string hashedPassword = HashPassword(matKhau);
            return dt.Rows.Count > 0 && dt.Rows[0].Field<string>("Mật khẩu") == hashedPassword;
        }

        public DataTable GetByTaiKhoan(string taiKhoan) => _taiKhoanDAL.GetByTaiKhoan(taiKhoan);

        public DataTable GetById(string id) => _taiKhoanDAL.GetById(id);

        public bool UpdateMatKhau(int id, string matKhauMoi)
        {
            matKhauMoi = HashPassword(matKhauMoi); // Mã hóa mật khẩu mới
            _taiKhoanDAL.UpdateMatKhau(id, matKhauMoi);
            return true; // Giả định thành công, có thể kiểm tra thêm
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return string.Concat(bytes.Select(b => b.ToString("x2")));
            }
        }

        // Kiem tra du lieu dau vao cua id nhan vien, ten tai khoan, mat khau
        public NhanVien KtraDuLieuTaiKhoan(string userName, string password)
        {
            try
            {
                var taiKhoan = _taiKhoanDAL.DsTaiKhoan().FirstOrDefault(p => p.taiKhoan1 == userName && p.matKhau == password);
                if (taiKhoan != null)
                {
                    return _nhanVienDAL.LayNhanVienQuaID(taiKhoan.idNhanVien);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy tìm tài khản nhân viên: " + ex.Message);
            }
        }

        // ds tai khoan
        public List<TaiKhoan> DsTaiKhoan() => _taiKhoanDAL.DsTaiKhoan();

        public bool KiemTraMatKhauCu(string idNhanVien, string matKhau)
        {
            return _taiKhoanDAL.KiemTraMatKhauCu(idNhanVien, matKhau);
        }

        public bool DoiMatKhau(string idNhanVien, string matKhauMoi)
        {
            return _taiKhoanDAL.DoiMatKhau(idNhanVien, matKhauMoi);
        }

        public bool IsUsernameExists(string username, int? excludeId = null)
        {
            return _taiKhoanDAL.IsUsernameExists(username, excludeId);
        }
    }
}