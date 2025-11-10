using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
using DAL;

namespace GUI
{
    public partial class FormThemHopDong : Form
    {
        DTOUngVien _ungVien = new DTOUngVien();
        BLLHopDongLaoDong _bllHopDong;
        public FormThemHopDong(DTOUngVien dto, string conn)
        {
            _ungVien = dto;
            _bllHopDong = new BLLHopDongLaoDong(conn);
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void FormThemHopDong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void FormThemHopDong_Load(object sender, EventArgs e)
        {
            if(!IsValid(_ungVien))
            {
                MessageBox.Show("Dữ liệu ứng viên không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            }
        }

        public bool IsValid(DTOUngVien uv)
        {
            if (uv == null)
                return false;

            // Kiểm tra string null hoặc rỗng
            if (string.IsNullOrWhiteSpace(uv.TenNhanVien)) return false;
            if (string.IsNullOrWhiteSpace(uv.DiaChi)) return false;
            if (string.IsNullOrWhiteSpace(uv.Que)) return false;
            if (string.IsNullOrWhiteSpace(uv.GioiTinh)) return false;
            if (string.IsNullOrWhiteSpace(uv.Email)) return false;
            if (string.IsNullOrWhiteSpace(uv.DuongDanCV)) return false;
            if (string.IsNullOrWhiteSpace(uv.TrangThai)) return false;

            // Kiểm tra DateTime (không được để giá trị default)
            if (uv.NgaySinh == default(DateTime)) return false;
            if (uv.NgayUngTuyen == default(DateTime)) return false;

            // Kiểm tra số (ID phải > 0)
            if (uv.Id <= 0) return false;
            if (uv.IdChucVuUngTuyen <= 0) return false;
            if (uv.IdTuyenDung <= 0) return false;

            // Nếu tất cả đều hợp lệ thì:
            return true;
        }

    }
}
