using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLTuyenDung
    {
        public readonly DALTuyenDung _dbContext;

        public BLLTuyenDung(string conn) => _dbContext = new DALTuyenDung(conn);

        // Ktra ds Tuyen Dung
        public List<TuyenDung> KtraDsTuyenDung()
        {
            return _dbContext.DsTuyenDung();
        }

        // Ktra them Tuyen Dung
        public bool KtraThemTuyenDung(DTOTuyenDung DTO)
        {
            try
            {
                var ktraTD = _dbContext.TimTuyenDungQuaID(DTO.ID);

                if (ktraTD == null)
                {
                    var KtraThemKD = _dbContext.ThemTuyenDung(DTO);
                    if (KtraThemKD)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi thêm tuyển dụng !");
                }
                else throw new Exception("Không tìm thấy dữ liệu tuyển dụng trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm tuyển dụng: " + ex.Message);
            }
        }

        // Ktra cap nhat Tuyen Dung
        public bool KtraCapNhatTuyenDung(DTOTuyenDung DTO)
        {
            try
            {
                var ktraTD = _dbContext.TimTuyenDungQuaID(DTO.ID);

                if (ktraTD != null)
                {
                    var KtraThemKD = _dbContext.CapNhatTuyenDung(DTO);
                    if (KtraThemKD)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi cập nhật tuyển dụng !");
                }
                else throw new Exception("Không tìm thấy dữ liệu tuyển dụng trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật tuyển dụng: " + ex.Message);
            }
        }

        // Ktra xoa Tuyen Dung
        public bool KtraXoaTuyenDung(DTOTuyenDung DTO)
        {
            try
            {
                var ktraTD = _dbContext.TimTuyenDungQuaID(DTO.ID);

                if (ktraTD != null)
                {

                    var KtraThemTD = _dbContext.XoaTuyenDung(DTO);
                    if (KtraThemTD)
                    {
                        return true;
                    }
                    else throw new Exception("Lỗi xóa tuyển dụng !");
                }
                else throw new Exception("Không tìm thấy dữ liệu tuyển dụng trong hệ thống !");

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật tuyển dụng: " + ex.Message);
            }
        }

        // Tim du lieu Tuyen Dung qua id
        public TuyenDung KtraTuyenDungQuaID(int id)
        {
            try
            {
                if (id > 0)
                {
                    var tuyenDung = _dbContext.TimTuyenDungQuaID(id);

                    if (tuyenDung != null)
                    {
                        return tuyenDung;
                    }
                    else throw new Exception($"Không tìm thấy dữ liệu tuyển dụng qua ID {id}");
                }
                else throw new Exception($"Kiểm tra lại dữ liệu đầu vào của id được nhập !");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm id: " + ex.Message);
            }
        }
    }
}
