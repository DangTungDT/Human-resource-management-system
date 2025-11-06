using BLL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCDuyetNghiPhep : UserControl
    {
        private readonly string _conn;

        private string _idSelected { get; set; }
        private string _idNhanVien { get; set; }
        private string _idTuyenDung { get; set; }

        private readonly BLLNghiPhep _dbContextNP;
        private readonly BLLNhanVien _dbContextNV;

        public UCDuyetNghiPhep(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;

            _dbContextNP = new BLLNghiPhep(conn);
            _dbContextNV = new BLLNhanVien(conn);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            btnDuyet.Enabled = false;
            btnKhongDuyet.Enabled = false;
            dgvDsXinNghiPhep.DataSource = LoadDuLieu();

        }

        private void dgvDsXinNghiPhep_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                _idTuyenDung = dgvDsXinNghiPhep.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                _idSelected = dgvDsXinNghiPhep.Rows[e.RowIndex].Cells["IDNhanVien"].Value.ToString();
                txtNguoiTao.Text = dgvDsXinNghiPhep.Rows[e.RowIndex].Cells["NhanVien"].Value.ToString();
                txtLoaiNghiPhep.Text = dgvDsXinNghiPhep.Rows[e.RowIndex].Cells["LoaiNghiPhep"].Value.ToString();
                rtGhiChu.Text = dgvDsXinNghiPhep.Rows[e.RowIndex].Cells["LyDoNghi"].Value.ToString();
                txtTrangThai.Text = dgvDsXinNghiPhep.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();
                txtThoiGian.Text = dgvDsXinNghiPhep.Rows[e.RowIndex].Cells["ThoiGianNghi"].Value.ToString();

                if (_idSelected.Contains("TPNS") && _idNhanVien == _idSelected)
                {
                    btnDuyet.Enabled = false;
                    btnKhongDuyet.Enabled = false;
                }
                else
                {
                    btnDuyet.Enabled = true;
                    btnKhongDuyet.Enabled = true;
                }
            }
        }

        private void btnDuyet_Click(object sender, EventArgs e) => XuLyButtonDuyetNghiPhep(true);

        private void btnKhongDuyet_Click(object sender, EventArgs e) => XuLyButtonDuyetNghiPhep(false);

        private void XuLyButtonDuyetNghiPhep(bool loai)
        {
            try
            {
                var setTrangThai = loai ? "Duyệt" : "Không duyệt";
                var nghiPhepCuaNV = _dbContextNP.KtraTrangThaiNghiPhepDonChuaDuyet(_idSelected);

                var nghiPhep = _dbContextNP.KtraNghiPhepQuaID(Convert.ToInt32(_idTuyenDung));

                if (nghiPhep == null)
                {
                    MessageBox.Show($"Không tìm thấy dữ liệu nghỉ phép để cập nhật trạng thái !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc chắn về cập nhật với trạng thái là '{setTrangThai}' ?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_dbContextNP.KtraCapNhatTrangThaiNghiPhep(new DTONghiPhep(Convert.ToInt32(_idTuyenDung), _idNhanVien, setTrangThai)))
                    {
                        MessageBox.Show($"Cập nhật trạng thái thành công. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadGeneral();
                        btnDuyet.Enabled = false;
                        btnKhongDuyet.Enabled = false;
                    }
                    else MessageBox.Show($"Cập nhật trạng thái tuyển dụng không thành công !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xử lý trạng thái: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private object LoadDuLieu()
        {
            try
            {
                object anonymous = new object();
                var dsNghiPhep = _dbContextNP.LayDsNghiPhep();
                anonymous = _dbContextNP.LayDsNghiPhep().Where(p => p.TrangThai.Equals("Đang yêu cầu", StringComparison.OrdinalIgnoreCase)).Select(p => new
                {
                    ID = p.id,
                    IDNhanVien = p.idNhanVien,
                    NhanVien = _dbContextNV.KtraNhanVienQuaID(p.idNhanVien).TenNhanVien,
                    LyDoNghi = p.LyDoNghi,
                    LoaiNghiPhep = p.LoaiNghiPhep,
                    ThoiGianNghi = p.NgayBatDau.ToString("dd/MM") + " - " + p.NgayKetThuc.ToString("dd/MM"),
                    SoNgayNghi = (p.NgayKetThuc.Day - p.NgayBatDau.Day + 1).ToString(),
                    NgayNghiCoPhep = SoNgayNghiCoPhep(dsNghiPhep, DateTime.Now.Month, p.idNhanVien),
                    NgayNghiKhongPhep = SoNgayNghiKhongPhep(dsNghiPhep, DateTime.Now.Month, p.idNhanVien),
                    TrangThai = p.TrangThai

                }).ToList();

                return anonymous;
            }
            catch { return null; }
        }

        // Lay so ngay co phep theo thang
        private string SoNgayNghiCoPhep(List<NghiPhep> DsNghiPhep, int thangHienTai, string maNV)
        {
            var nghiCoLuong = DsNghiPhep.Where(p => p.idNhanVien == maNV && p.TrangThai.Equals("Duyệt", StringComparison.OrdinalIgnoreCase) && p.LoaiNghiPhep.Equals("Có phép", StringComparison.OrdinalIgnoreCase)).ToList();
            return nghiCoLuong.Where(p => p.NgayBatDau.Month == thangHienTai && p.NgayBatDau.Year == DateTime.Now.Year).ToList()
                                            .Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiCoLuong.Count + "";
        }

        // Lay so ngay khong phep theo thang
        private string SoNgayNghiKhongPhep(List<NghiPhep> DsNghiPhep, int thangHienTai, string maNV)
        {
            var nghiKhongLuong = DsNghiPhep.Where(p => p.idNhanVien == maNV && p.TrangThai.Equals("Duyệt", StringComparison.OrdinalIgnoreCase) && p.LoaiNghiPhep.Equals("Không phép", StringComparison.OrdinalIgnoreCase)).ToList();
            return nghiKhongLuong.Where(p => p.NgayBatDau.Month == thangHienTai && p.NgayBatDau.Year == DateTime.Now.Year).ToList()
                                            .Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiKhongLuong.Count + "";
        }

        private void ConvertHeaderTextDGV()
        {
            dgvDsXinNghiPhep.Columns["ID"].Visible = false;
            dgvDsXinNghiPhep.Columns["IDNhanVien"].Visible = false;
            dgvDsXinNghiPhep.Columns["NhanVien"].HeaderText = "Nhân viên";
            dgvDsXinNghiPhep.Columns["LyDoNghi"].HeaderText = "Lý do nghỉ";
            dgvDsXinNghiPhep.Columns["LoaiNghiPhep"].HeaderText = "Loại nghỉ phép";
            dgvDsXinNghiPhep.Columns["ThoiGianNghi"].HeaderText = "Thời gian nghỉ";
            dgvDsXinNghiPhep.Columns["SoNgayNghi"].HeaderText = "Số ngày xin nghỉ";
            dgvDsXinNghiPhep.Columns["NgayNghiCoPhep"].HeaderText = $"Số ngày có phép tháng {DateTime.Now.Month}";
            dgvDsXinNghiPhep.Columns["NgayNghiKhongPhep"].HeaderText = $"Số ngày không phép tháng {DateTime.Now.Month}";
            dgvDsXinNghiPhep.Columns["TrangThai"].HeaderText = "Trạng thái";
        }

        private void UCDuyetNghiPhep_Load_1(object sender, EventArgs e)
        {
            LoadGeneral();
        }

        public void LoadGeneral()
        {
            btnDuyet.Enabled = false;
            btnKhongDuyet.Enabled = false;
            dgvDsXinNghiPhep.DataSource = LoadDuLieu();
            ConvertHeaderTextDGV();
            _idSelected = "0";
        }
    }
}
