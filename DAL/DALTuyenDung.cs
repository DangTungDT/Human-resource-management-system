using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class DALTuyenDung
    {
        private readonly string connectionString;
        public readonly PersonnelManagementDataContextDataContext _dbContext;

        public DALTuyenDung(string conn)
        {
            _dbContext = new PersonnelManagementDataContextDataContext(conn);
            connectionString = conn;
        }

        // Danh sach Tuyen Dung
        public List<TuyenDung> DsTuyenDung()
        {
            using (var db = new PersonnelManagementDataContextDataContext(_dbContext.Connection.ConnectionString))
            {
                return db.TuyenDungs.ToList();
            }
        }

        // Tim Tuyen Dung qua id
        public TuyenDung TimTuyenDungQuaID(int id) => _dbContext.TuyenDungs.FirstOrDefault(p => p.id == id);

        // Tim Tuyen Dung qua idNguoiTao
        public TuyenDung TimTuyenDungQuaIDNV(string id) => _dbContext.TuyenDungs.FirstOrDefault(p => p.idNguoiTao == id);

        // Tim Tuyen Dung qua trang thai
        public TuyenDung TimTuyenDungQuaTrangThai(string id) => _dbContext.TuyenDungs.FirstOrDefault(p => p.idNguoiTao == id && p.trangThai == "Chờ duyệt");

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
                    soLuong = DTO.SoLuong,
                    ghiChu = DTO.GhiChu
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
                    tuyenDung.ngayTao = DTO.NgayTao;
                    tuyenDung.soLuong = DTO.SoLuong;
                    tuyenDung.ghiChu = DTO.GhiChu;

                    _dbContext.SubmitChanges(); 

                    return true;
                }
                else return false;
            }
            catch { return false; }

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

                    //using (var db = new PersonnelManagementDataContextDataContext(_dbContext.Connection.ConnectionString))
                    //{
                    //    db.SubmitChanges();
                    //}
                    _dbContext.SubmitChanges();

                    return true;
                }
                else return false;
            }
            catch { return false; }
        }

        // Cap nhat trang thai Tuyen Dung 
        public bool CapNhatDuyetTuyenDung(DTOTuyenDung DTO)
        {
            try
            {
                var tuyenDung = TimTuyenDungQuaID(DTO.ID);
                if (tuyenDung != null)
                {

                    tuyenDung.tieuDe = DTO.TieuDe;
                    tuyenDung.soLuongDuyet = DTO.SoLuong;
                    tuyenDung.ghiChuDuyet = DTO.GhiChu;
                    tuyenDung.trangThai = DTO.TrangThai.Equals("Loại", StringComparison.CurrentCultureIgnoreCase) ? "Ngừng tuyển" : DTO.TrangThai;
                    tuyenDung.ngayTao = DTO.NgayTao;

                    _dbContext.SubmitChanges();

                    return true;
                }
                else return false;
            }
            catch { return false; }
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

        public DataTable BaoCaoTuyenDungTheoQuy(string quy, int nam, string phongBan, string viTri)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCaoTuyenDungTheoQuy", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Quy", (object)quy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Nam", nam);
                cmd.Parameters.AddWithValue("@PhongBan", (object)phongBan ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ViTri", (object)viTri ?? DBNull.Value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
