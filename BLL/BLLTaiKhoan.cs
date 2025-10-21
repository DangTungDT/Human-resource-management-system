using DAL;
using DTO;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    public class BLLTaiKhoan
    {
        private readonly DALTaiKhoan _dal;

        public BLLTaiKhoan(string conn)
        {
            _dal = new DALTaiKhoan(conn);
        }

        public DataTable GetAllAccounts() => _dal.GetAll();

        public void SaveAccount(DTOTaiKhoan tk, bool isNew)
        {
            tk.MatKhau = HashPassword(tk.MatKhau);
            if (isNew)
                _dal.Insert(tk);
            else
                _dal.Update(tk);
        }

        public void DeleteAccount(int id) => _dal.Delete(id);

        public void CreateDefaultAccount(string idNV, string tenNhanVien)
        {
            _dal.CreateDefaultAccount(idNV, tenNhanVien);
        }

        public bool ValidateLogin(string taiKhoan, string matKhau)
        {
            DataTable dt = _dal.GetByTaiKhoan(taiKhoan);
            string hashedPassword = HashPassword(matKhau);
            return dt.Rows.Count > 0 && dt.Rows[0].Field<string>("Mật khẩu") == hashedPassword;
        }

        public DataTable GetByTaiKhoan(string taiKhoan) => _dal.GetByTaiKhoan(taiKhoan);

        public DataTable GetById(string id) => _dal.GetById(id);

        public bool UpdateMatKhau(int id, string matKhauMoi)
        {
            matKhauMoi = HashPassword(matKhauMoi); // Mã hóa mật khẩu mới
            _dal.UpdateMatKhau(id, matKhauMoi);
            return true; // Giả định thành công, có thể kiểm tra thêm
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return string.Concat(bytes.Select(b => b.ToString("x2")));
            }
        }
    }
}