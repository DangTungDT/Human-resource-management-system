using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCChiTietluongCaNhan : UserControl
    {
        public readonly string _idNhanVien, _conn;
        private readonly BLLNhanVien _dbContextNV;
        private readonly BLLChiTietLuong _dbContextCTL;

        public UCChiTietluongCaNhan(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;
            _dbContextNV = new BLLNhanVien(conn);
            _dbContextCTL = new BLLChiTietLuong(conn);
        }

        private void UCChiTietluongCaNhan_Load(object sender, EventArgs e)
        {
            var currentDate = DateTime.Now;
            lblNgaylam.Text = new DateTime(currentDate.Year, currentDate.Month, 1).ToShortDateString() + " - " + new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month)).ToShortDateString();
            dgvDsLuongCaNhan.DataSource = LoadDuLieu();
        }

        private void dgvDsLuongCaNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                lblTenNV.Text = dgvDsLuongCaNhan.Rows[e.RowIndex].Cells["NhanVien"]?.Value.ToString() ?? string.Empty;
                lblTrangThai.Text = dgvDsLuongCaNhan.Rows[e.RowIndex].Cells["TrangThai"]?.Value.ToString() ?? string.Empty;
                lblNgayNhanLuong.Text = DateTime.Parse(dgvDsLuongCaNhan.Rows[e.RowIndex].Cells["NgayNhanLuong"]?.Value.ToString()).ToShortDateString() ?? string.Empty;

                txtLuongTruoc.Text = DinhDangChung("LuongCoBan", e) ?? string.Empty;
                txtLuongSau.Text = DinhDangChung("LuongSauKhauTru", e) ?? string.Empty;
                txtTongKhauTru.Text = DinhDangChung("KhauTru", e) ?? string.Empty;
                txtKhenThuong.Text = DinhDangChung("KhenThuong", e) ?? string.Empty;
                txtTienPhat.Text = DinhDangChung("TienPhat", e) ?? string.Empty;
                txtPhuCap.Text = DinhDangChung("PhuCap", e) ?? string.Empty;

                if (dgvDsLuongCaNhan.Rows[e.RowIndex].Cells["GhiChu"]?.Value == null)
                {
                    rtGhiChu.Text = string.Empty;
                }
                else rtGhiChu.Text = dgvDsLuongCaNhan.Rows[e.RowIndex].Cells["GhiChu"]?.Value.ToString();
            }
        }

        public string DinhDangChung(string amount, DataGridViewCellEventArgs e)
        {
            return string.Format("{0:N0} VND", dgvDsLuongCaNhan.Rows[e.RowIndex].Cells[amount]?.Value);
        }
        public object LoadDuLieu()
        {
            var anonymous = new object();
            var NhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien);

            var dsChiTietLuong = _dbContextCTL.KtraDsChiTietLuong().Where(p => p.idNhanVien == _idNhanVien && p.ngayNhanLuong.Month == DateTime.Now.Month).Select(p => new
            {
                NhanVien = NhanVien.TenNhanVien,
                TrangThai = p.trangThai,
                NgayNhanLuong = p.ngayNhanLuong,
                LuongCoBan = p.luongTruocKhauTru,
                LuongSauKhauTru = p.luongSauKhauTru,
                KhauTru = p.tongKhauTru,
                PhuCap = p.tongPhuCap,
                KhenThuong = p.tongKhenThuong,
                TienPhat = p.tongTienPhat,
                GhiChu = p.ghiChu == string.Empty ? string.Empty : p.ghiChu

            }).ToList();

            return dsChiTietLuong;
        }
    }
}
