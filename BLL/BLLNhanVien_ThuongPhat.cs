using DAL;
using DAL.DataContext;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL
{
    public class BLLNhanVien_ThuongPhat
    {
        public readonly DALNhanVien_ThuongPhat _nhanVienThuongPhatDAL;

        public BLLNhanVien_ThuongPhat(string conn)
        {
            _nhanVienThuongPhatDAL = new DALNhanVien_ThuongPhat(conn);
            _nhanVienThuongPhatDAL = new DALNhanVien_ThuongPhat(conn);
        }

        #region === Lấy dữ liệu ===

        public List<NhanVien_ThuongPhat> KtraDsNhanVien_ThuongPhat()
        {
            var list = _nhanVienThuongPhatDAL.DsNhanVien_ThuongPhat().ToList();
            if (list.Any() && list != null)
            {
                try
                {
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi kiểm tra d/s bảng trung gian nhân viên - thưởng phạt: " + ex.Message);
                }
            }
            else return null;
        }
        public List<NhanVien_ThuongPhat> GetAllList()
        {
            try
            {
                var list = _nhanVienThuongPhatDAL.DsNhanVien_ThuongPhat();
                return list?.ToList() ?? new List<NhanVien_ThuongPhat>();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách nhân viên - thưởng/phạt: " + ex.Message);
            }
        }

        public DataTable GetAll(string loai, string idPhongBan = "")
        {
            try
            {
                return _nhanVienThuongPhatDAL.GetAll(loai, idPhongBan);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách thưởng/phạt: " + ex.Message);
            }
        }

        public List<string> GetNhanVienByThuongPhatId(int thuongPhatId)
        {
            try
            {
                return _nhanVienThuongPhatDAL.GetNhanVienByThuongPhatId(thuongPhatId);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách nhân viên của nhóm thưởng/phạt: " + ex.Message);
            }
        }

        public DataTable GetAllLyDo(string loai)
        {
            try
            {
                return _nhanVienThuongPhatDAL.GetAllLyDo(loai);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách lý do thưởng/phạt: " + ex.Message);
            }
        }
        #endregion

        #region === Thêm mới ===
        public void SaveMulti(string loai, List<string> idNhanViens, string lyDo, decimal tien, DateTime ngayApDung, string idNguoiTao)
        {
            if (idNhanViens == null || idNhanViens.Count == 0)
                throw new ArgumentException("Danh sách nhân viên không được để trống.");

            try
            {
                int idThuongPhat = _nhanVienThuongPhatDAL.CheckLyDoExists(loai, lyDo);

                // Nếu chưa có lý do => thêm mới
                if (idThuongPhat == 0)
                {
                    idThuongPhat = _nhanVienThuongPhatDAL.InsertLyDo(loai, lyDo, tien, idNguoiTao);
                }
                else
                {
                    // Nếu có rồi thì cập nhật lại số tiền cho đúng
                    _nhanVienThuongPhatDAL.UpdateThuongPhat(idThuongPhat, lyDo, tien);
                }

                // Loại bỏ trùng nhân viên
                var oldNhanViens = _nhanVienThuongPhatDAL.GetNhanVienByThuongPhatId(idThuongPhat);
                var toAdd = idNhanViens.Except(oldNhanViens).ToList();

                if (toAdd.Any())
                    _nhanVienThuongPhatDAL.InsertNhanVienThuongPhat_Multi(idThuongPhat, toAdd, ngayApDung);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm mới thưởng/phạt: " + ex.Message);
            }
        }
        #endregion

        #region === Cập nhật thông minh ===
        public void UpdateMultiSmart(string loai, int idThuongPhat, List<string> newNhanViens, string lyDo, decimal soTien, DateTime ngayApDung)
        {
            if (newNhanViens == null)
                throw new ArgumentException("Danh sách nhân viên không hợp lệ.");

            try
            {
                var oldLyDo = _nhanVienThuongPhatDAL.GetLyDoById(idThuongPhat);

                // Nếu đổi lý do thì tạo nhóm mới
                if (!string.Equals(oldLyDo, lyDo, StringComparison.OrdinalIgnoreCase))
                {
                    int existingId = _nhanVienThuongPhatDAL.CheckLyDoExists(loai, lyDo);
                    int newId = existingId;

                    if (existingId == 0)
                        newId = _nhanVienThuongPhatDAL.InsertLyDo(loai, lyDo, soTien, "GD00000001");
                    else
                        _nhanVienThuongPhatDAL.UpdateThuongPhat(existingId, lyDo, soTien);

                    _nhanVienThuongPhatDAL.InsertNhanVienThuongPhat_Multi(newId, newNhanViens, ngayApDung);
                    _nhanVienThuongPhatDAL.DeleteThuongPhat(idThuongPhat); // Xóa nhóm cũ
                    return;
                }

                // Không đổi lý do => chỉ cập nhật nhân viên
                _nhanVienThuongPhatDAL.UpdateThuongPhat(idThuongPhat, lyDo, soTien);

                var oldNhanViens = _nhanVienThuongPhatDAL.GetNhanVienByThuongPhatId(idThuongPhat);
                var toAdd = newNhanViens.Except(oldNhanViens).ToList();
                var toRemove = oldNhanViens.Except(newNhanViens).ToList();

                if (toRemove.Any())
                {
                    foreach (var nv in toRemove)
                        _nhanVienThuongPhatDAL.DeleteNhanVienInThuongPhat(idThuongPhat, nv);
                }

                if (toAdd.Any())
                    _nhanVienThuongPhatDAL.InsertNhanVienThuongPhat_Multi(idThuongPhat, toAdd, ngayApDung);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật thưởng/phạt: " + ex.Message);
            }
        }
        #endregion

        #region === Xóa ===
        public void Delete(int id)
        {
            try
            {
                _nhanVienThuongPhatDAL.DeleteThuongPhat(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa thưởng/phạt: " + ex.Message);
            }
        }
        #endregion

        public void XoaNhieuNhanVien_ThuongPhat(List<int> ids)
        {
            _nhanVienThuongPhatDAL.XoaNhieuNhanVien_ThuongPhat(ids);
        }
    }
}
