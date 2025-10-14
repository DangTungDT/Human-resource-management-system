using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            tk.MatKhau = HashPassword(tk.MatKhau); // Mã hoá mật khẩu SHA256

            if (isNew)
                _dal.Insert(tk);
            else
                _dal.Update(tk);
        }

        public void DeleteAccount(int id) => _dal.Delete(id);

        public void CreateDefaultAccount(string idNV)
        {
            DTOTaiKhoan tk = new DTOTaiKhoan
            {
                TaiKhoan = idNV,
                IdNhanVien = idNV,
                MatKhau = HashPassword("1")
            };
            _dal.Insert(tk);
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
