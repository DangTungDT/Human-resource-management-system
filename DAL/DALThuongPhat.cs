using DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALThuongPhat
    {
        public readonly PersonnelManagementDataContextDataContext _dbContext;
        private readonly string connectionString;

        public DALThuongPhat(string stringConnection)
        {
            _dbContext = new PersonnelManagementDataContextDataContext(stringConnection);
            connectionString = stringConnection;
        }
        public IQueryable<DTOThuongPhat> DanhSachThuongPhat() => _dbContext.ThuongPhats.Select(p => new DTOThuongPhat
        {
            ID = p.id,
            TienThuongPhat = p.tienThuongPhat,
            Loai = p.loai,
            LyDo = p.lyDo,
            IDNguoiTao = p.idNguoiTao
        });

        public IQueryable<DTONhanVien> DanhSachNhanVien() => _dbContext.NhanViens.Select(p => new DTONhanVien
        {
            ID = p.id,
            TenNhanVien = p.TenNhanVien
        });

        public DataTable GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT id, loai, lyDo, tienThuongPhat, idNguoiTao FROM ThuongPhat";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


    }
}
