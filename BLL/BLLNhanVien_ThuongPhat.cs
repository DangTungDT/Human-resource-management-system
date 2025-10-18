using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL
{
    public class BLLNhanVien_ThuongPhat
    {
        private readonly DALNhanVien_ThuongPhat dal;
        public BLLNhanVien_ThuongPhat(string conn) => dal = new DALNhanVien_ThuongPhat(conn);

        public DataTable GetAll(string loai, string idPhongBan = "") => dal.GetAll(loai, idPhongBan);
        public List<string> GetNhanVienByThuongPhatId(int thuongPhatId)
        {
            // Lấy danh sách nhân viên đã được thưởng/phạt theo nhóm
            return dal.GetNhanVienByThuongPhatId(thuongPhatId);
        }
        public DataTable GetAllLyDo(string loai)
        {
            return dal.GetAllLyDo(loai);
        }

        // ✅ Thêm mới
        public void SaveMulti(string loai, List<string> idNhanViens, string lyDo, decimal tien, DateTime ngayApDung, string idNguoiTao)
        {
            int idThuongPhat = dal.CheckLyDoExists(loai, lyDo);
            if (idThuongPhat == 0)
            {
                idThuongPhat = dal.InsertLyDo(loai, lyDo, tien, idNguoiTao);
            }
            dal.InsertNhanVienThuongPhat_Multi(idThuongPhat, idNhanViens, ngayApDung);
        }



        // ✅ Cập nhật thông minh — chỉ thêm/xóa đúng nhân viên thay đổi
        public void UpdateMultiSmart(string loai, int idThuongPhat, List<string> newNhanViens, string lyDo, decimal soTien, DateTime ngayApDung)
        {
            // Lấy lý do cũ
            var oldLyDo = dal.GetLyDoById(idThuongPhat);

            // Nếu lý do thay đổi
            if (!string.Equals(oldLyDo, lyDo, StringComparison.OrdinalIgnoreCase))
            {
                // ✅ Kiểm tra lý do mới đã tồn tại chưa
                int existingId = dal.CheckLyDoExists(loai, lyDo);
                int newIdThuongPhat = existingId;

                if (existingId == 0)
                {
                    // ❌ Chưa có → tạo mới
                    newIdThuongPhat = dal.InsertLyDo(loai, lyDo, soTien, "GD00000001");
                }
                else
                {
                    // ✅ Đã có → cập nhật số tiền (nếu thay đổi)
                    dal.UpdateThuongPhat(existingId, lyDo, soTien);
                }

                // ✅ Gán nhân viên vào nhóm đó
                dal.InsertNhanVienThuongPhat_Multi(newIdThuongPhat, newNhanViens, ngayApDung);

                // ✅ Xóa nhóm cũ (để tránh trùng)
                dal.DeleteThuongPhat(idThuongPhat);

                return;
            }

            // Nếu lý do không thay đổi → chỉ cập nhật danh sách nhân viên
            dal.UpdateThuongPhat(idThuongPhat, lyDo, soTien);

            var oldNhanViens = dal.GetNhanVienByThuongPhatId(idThuongPhat);
            var toAdd = newNhanViens.Except(oldNhanViens).ToList();
            var toRemove = oldNhanViens.Except(newNhanViens).ToList();

            foreach (var nv in toRemove)
                dal.DeleteNhanVienInThuongPhat(idThuongPhat, nv);

            if (toAdd.Any())
                dal.InsertNhanVienThuongPhat_Multi(idThuongPhat, toAdd, ngayApDung);
        }


        public void Delete(int id) => dal.DeleteThuongPhat(id);
    }
}
