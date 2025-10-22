using DAL;
using DTO;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BLL
{
    public class BLLTaiKhoan
    {
        private readonly DALTaiKhoan _dalTK;
        private readonly DALNhanVien _dalNV;

        public BLLTaiKhoan(string conn)
        {
            _dalNV = new DALNhanVien(conn);
            _dalTK = new DALTaiKhoan(conn);
        }

        public DataTable GetAllAccounts() => _dalTK.GetAll();

        public void SaveAccount(DTOTaiKhoan tk, bool isNew)
        {
            tk.MatKhau = HashPassword(tk.MatKhau);
            if (isNew)
                _dalTK.Insert(tk);
            else
                _dalTK.Update(tk);
        }

        public void DeleteAccount(int id) => _dalTK.Delete(id);

        public void CreateDefaultAccount(string idNV, string tenNhanVien)
        {
            _dalTK.CreateDefaultAccount(idNV, tenNhanVien);
        }

        public bool ValidateLogin(string taiKhoan, string matKhau)
        {
            DataTable dt = _dalTK.GetByTaiKhoan(taiKhoan);
            string hashedPassword = HashPassword(matKhau);
            return dt.Rows.Count > 0 && dt.Rows[0].Field<string>("Mật khẩu") == hashedPassword;
        }

        public DataTable GetByTaiKhoan(string taiKhoan) => _dalTK.GetByTaiKhoan(taiKhoan);

        public DataTable GetById(string id) => _dalTK.GetById(id);

        public bool UpdateMatKhau(int id, string matKhauMoi)
        {
            matKhauMoi = HashPassword(matKhauMoi); // Mã hóa mật khẩu mới
            _dalTK.UpdateMatKhau(id, matKhauMoi);
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
                var taiKhoan = _dalTK.DsTaiKhoan().FirstOrDefault(p => p.taiKhoan1 == userName && p.matKhau == password);
                if (taiKhoan != null)
                {
                    return _dalNV.LayNhanVienQuaID(taiKhoan.idNhanVien);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy tìm tài khản nhân viên: " + ex.Message);
            }
        }
    }
}