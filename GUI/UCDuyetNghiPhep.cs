using BLL;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.Shared;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private readonly BLLPhongBan _dbContextPB;

        public UCDuyetNghiPhep(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;

            _dbContextNP = new BLLNghiPhep(conn);
            _dbContextNV = new BLLNhanVien(conn);
            _dbContextPB = new BLLPhongBan(conn);
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

                rtGhiChu.ReadOnly = false;

            }
        }

        private void btnDuyet_Click(object sender, EventArgs e) => XuLyButtonDuyetNghiPhep(true);

        private void btnKhongDuyet_Click(object sender, EventArgs e) => XuLyButtonDuyetNghiPhep(false);

        private void XuLyButtonDuyetNghiPhep(bool loai)
        {
            try
            {
                var setTrangThai = loai ? "Đã duyệt" : "Không duyệt";
                var nghiPhepCuaNV = _dbContextNP.KtraTrangThaiNghiPhepDonChuaDuyet(_idSelected);

                NghiPhep nghiPhep = null;
                if (int.TryParse(_idTuyenDung, out int id))
                {
                    nghiPhep = _dbContextNP.KtraNghiPhepQuaID(id);
                }

                if (nghiPhep == null)
                {
                    MessageBox.Show($"Không tìm thấy dữ liệu nghỉ phép để cập nhật trạng thái !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc chắn về cập nhật với trạng thái là '{setTrangThai}' ?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (_dbContextNP.KtraCapNhatTrangThaiNghiPhep(new DTONghiPhep(id, _idNhanVien, setTrangThai, rtGhiChu.Text, DateTime.Now)))
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
                var dsNhanVien = _dbContextNV.KtraDsNhanVien();
                var dsPhongBan = _dbContextPB.KtraDsPhongBan();

                anonymous = _dbContextNP.LayDsNghiPhep()

                        .Join(dsNhanVien,
                        np => np.idNhanVien,
                        nv => nv.id, (np, nv)
                        => new { np, nv }).

                        Join(dsPhongBan,
                        nvnp => nvnp.nv.idPhongBan,
                        pb => pb.id, (nvnp, pb)
                        => new { nvnp.np, nvnp.nv, pb })

                        .Where(p => (_idNhanVien.Contains("TPNS") || _idNhanVien.Contains("GD")
                                        ? p.nv.id.Contains("TP") || p.nv.id.Contains("NVNS") || p.nv.id.Contains("NVGD")
                                        : p.nv.idPhongBan == _dbContextNV.KtraNhanVienQuaID(_idNhanVien).idPhongBan)

                                        && ktraHienThiTP(p.np.idNhanVien)
                                        && p.np.NgayBatDau.Date > DateTime.Now.Date
                                        && p.np.TrangThai.Equals("Đang yêu cầu", StringComparison.OrdinalIgnoreCase)

                                ).Select(p => new
                        {
                            ID = p.np.id,
                            IDNhanVien = p.np.idNhanVien,
                            NhanVien = p.nv.TenNhanVien,
                            LyDoNghi = p.np.LyDoNghi,
                            LoaiNghiPhep = p.np.LoaiNghiPhep,
                            ThoiGianNghi = p.np.NgayBatDau.ToString("dd/MM") + " - " + p.np.NgayKetThuc.ToString("dd/MM"),
                            SoNgayNghi = (p.np.NgayKetThuc.Day - p.np.NgayBatDau.Day + 1).ToString(),
                            NgayNghiCoPhep = SoNgayNghiCoPhep(dsNghiPhep, DateTime.Now.Month, p.np.idNhanVien),
                            NgayNghiKhongPhep = SoNgayNghiKhongPhep(dsNghiPhep, DateTime.Now.Month, p.np.idNhanVien),
                            LoaiTH = p.np.LoaiTruongHop,
                            TrangThai = p.np.TrangThai

                        }).OrderBy(p => p.ID).ThenBy(p => p.LoaiTH).ToList();

                return anonymous;
            }
            catch { return null; }
        }

        public bool ktraHienThiTP(string nv)
        {
            if (nv.Contains("TP") && (_idNhanVien.Contains("TPNS") || _idNhanVien.Contains("GD")))
            {
                return true;
            }
            else if (nv == _idNhanVien)
            {
                return false;
            }
            else return true;
        }

        // Lay so ngay co phep theo thang
        private string SoNgayNghiCoPhep(List<NghiPhep> DsNghiPhep, int thangHienTai, string maNV)
        {
            var nghiCoLuong = DsNghiPhep.Where(p => p.idNhanVien == maNV && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase) && p.LoaiNghiPhep.Equals("Có lương", StringComparison.OrdinalIgnoreCase)).ToList();
            return nghiCoLuong.Where(p => p.NgayBatDau.Month == thangHienTai && p.NgayBatDau.Year == DateTime.Now.Year).ToList()
                                            .Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiCoLuong.Count + "";
        }

        // Lay so ngay khong phep theo thang
        private string SoNgayNghiKhongPhep(List<NghiPhep> DsNghiPhep, int thangHienTai, string maNV)
        {
            var nghiKhongLuong = DsNghiPhep.Where(p => p.idNhanVien == maNV && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase) && p.LoaiNghiPhep.Equals("Không lương", StringComparison.OrdinalIgnoreCase)).ToList();
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
            dgvDsXinNghiPhep.Columns["LoaiTH"].HeaderText = "Loại TH đột xuất";
            dgvDsXinNghiPhep.Columns["TrangThai"].HeaderText = "Trạng thái";

            dgvDsXinNghiPhep.Columns["LyDoNghi"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDsXinNghiPhep.Columns["LyDoNghi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void UCDuyetNghiPhep_Load_1(object sender, EventArgs e)
        {
            rtGhiChu.Enabled = true;
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
