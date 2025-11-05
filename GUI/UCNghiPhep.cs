using DAL;
using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCNghiPhep : UserControl
    {
        private string _idSelected { get; set; }
        public readonly string _idNhanVien;
        public readonly BLLChucVu _dbContextCV;
        public readonly BLLNghiPhep _dbContextNP;
        public readonly BLLNhanVien _dbContextNV;

        public UCNghiPhep(string idNhanVien, string connect)
        {
            InitializeComponent();
            _idNhanVien = idNhanVien.ToUpper();
            _dbContextCV = new BLLChucVu(connect);
            _dbContextNV = new BLLNhanVien(connect);
            _dbContextNP = new BLLNghiPhep(connect);
        }

        // Ktra cac field rong
        public bool KiemTraDuLieuDauVao()
        {
            bool ktra = true;
            error.Clear();

            foreach (var control in grbNhanVien.Controls)
            {
                if (control is RichTextBox richtext && string.IsNullOrWhiteSpace(richtext.Text))
                {
                    error.SetError(richtext, $"{richtext.Text} Trống !");
                    ktra = false;
                }

                if (control is Guna2ComboBox combobox && string.IsNullOrWhiteSpace(combobox.Text))
                {
                    error.SetError(combobox, $"'{combobox.Text}' Trống !");
                    ktra = false;
                }
            }

            return ktra;
        }

        private void UCNghiPhep_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i <= 12; i++)
                {
                    cmbLocThang.Items.Add(i);
                }

                dtpBatDau.Value = DateTime.Now;
                dtpKetThuc.Value = dtpBatDau.Value.AddDays(1);

                grbNhanVien.Text += _dbContextNV.KtraNhanVienQuaID(_idNhanVien).TenNhanVien;

                lblLichSu.Text = $"Lịch sử nghỉ phép tháng {DateTime.Now.Month}";

                ChayLaiDuLieu();

            }
            catch (Exception ex)

            {
                MessageBox.Show("Lỗi trang load: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Gui don nghi phep
        private void btnGui_Click(object sender, EventArgs e)
        {
            try
            {
                string loai = "Nghỉ phép có lương";
                var thangHienTai = DateTime.Now.Month;

                var DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList();

                var ktraDuyet = _dbContextNP.KtraTrangThaiDonChuaDuyet(_idNhanVien);
                if (!ktraDuyet)
                {
                    if (KiemTraDuLieuDauVao())
                    {
                        if (int.TryParse(SoNgayNghiCoPhep(DsNghiPhepTheoIDNV, thangHienTai), out int countCoLuong) && countCoLuong >= 3)
                        {
                            loai = "Nghỉ phép không lương";
                        }

                        string coLuong = "Số ngày nghỉ phép vượt quá quy định (số ngày nghỉ > 3/nghỉ phép)";
                        string khongLuong = "";
                        string ktraLoai = loai.Contains("có") ? coLuong : khongLuong;

                        var tinhNgayNghi = _dbContextNP.KtraTinhSoLuongNgayNghiCoPhep(_idNhanVien, dtpBatDau.Value.Day, dtpKetThuc.Value.Day, loai);
                        if (tinhNgayNghi)
                        {
                            if (KtraDuLieuBDKT(true))
                            {
                                if (_dbContextNP.KtraThemNghiPhep(new DTONghiPhep(0, _idNhanVien, DateTime.Parse(dtpBatDau.Value.ToShortDateString()), DateTime.Parse(dtpKetThuc.Value.ToShortDateString()), rtLyDo.Text, cmbLoaiNghi.Text, txtTrangThai.Text)))
                                {
                                    ChayLaiDuLieu();
                                    MessageBox.Show("Thêm dữ liệu thành công.");
                                    Empty();
                                }
                                else MessageBox.Show("Thêm nghỉ phép thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show($"Ngày bắt đầu phải lớn hơn ngày kết thúc đã duyệt !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                dtpBatDau.Value = DsNghiPhepTheoIDNV.Max(p => p.NgayKetThuc.AddDays(1));
                                dtpKetThuc.Value = dtpBatDau.Value;
                            }
                        }
                        else MessageBox.Show($"{ktraLoai} !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else MessageBox.Show("Đã tồn tại đơn nghỉ phép mới chưa được duyệt, vui lòng chờ đợi ý kiến từ bộ phận nhân sự !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }

        // Cap nhat don nghi phep
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_idSelected))
                {
                    MessageBox.Show("Vui lòng chọn dữ liệu nghỉ phép cần cập nhật !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var thangHienTai = DateTime.Now.Month;
                string khongLuong = "";
                string loai = "Nghỉ phép có lương";
                string coLuong = "Số ngày nghỉ phép vượt quá quy định (số ngày nghỉ > 3/nghỉ phép)";

                string ktraLoai = loai.Contains("có") ? coLuong : khongLuong;
                var ktraDuyet = _dbContextNP.KtraTrangThaiNP(Convert.ToInt32(_idSelected));
                var DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList();

                if (ktraDuyet)
                {
                    if (KiemTraDuLieuDauVao())
                    {
                        if (int.TryParse(SoNgayNghiCoPhep(DsNghiPhepTheoIDNV, thangHienTai), out int countCoLuong) && countCoLuong >= 3)
                        {
                            loai = "Nghỉ phép không lương";
                        }

                        var tinhNgayNghi = _dbContextNP.KtraTinhSoLuongNgayNghiCoPhep(_idNhanVien, dtpBatDau.Value.Day, dtpKetThuc.Value.Day, loai);
                        if (tinhNgayNghi)
                        {
                            if (KtraDuLieuBDKT(false))
                            {
                                if (_dbContextNP.KtraCapNhatNghiPhep(new DTONghiPhep(int.Parse(lblIDNV.Text), _idNhanVien, dtpBatDau.Value, dtpKetThuc.Value, rtLyDo.Text, cmbLoaiNghi.Text, txtTrangThai.Text)))
                                {
                                    ChayLaiDuLieu();
                                    MessageBox.Show("Cập nhật dữ liệu thành công.");
                                }
                                else MessageBox.Show("Cập nhật nghỉ phép thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show($"Ngày bắt đầu phải lớn hơn ngày kết thúc đã duyệt !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                dtpBatDau.Value = DsNghiPhepTheoIDNV.Max(p => p.NgayKetThuc.AddDays(1));
                                dtpKetThuc.Value = dtpBatDau.Value;
                            }
                        }
                        else MessageBox.Show($"{ktraLoai} !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else MessageBox.Show("Đơn nghỉ phép này của bạn đã được cập nhật trạng thái !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }

        // Xoa don nghi phep
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_idSelected))
                {
                    MessageBox.Show("Vui lòng chọn dữ liệu nghỉ phép cần xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var ktraDuyet = _dbContextNP.KtraTrangThaiNP(Convert.ToInt32(_idSelected));
                if (!ktraDuyet)
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa yêu cầu đã chọn không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (_dbContextNP.KtraXoaNghiPhep(new DTONghiPhep(int.Parse(_idSelected), _idNhanVien)))
                        {
                            ChayLaiDuLieu();
                            MessageBox.Show("Xóa dữ liệu thành công.");
                            Empty();
                            _idSelected = null;
                        }
                        else MessageBox.Show("Xóa nghỉ phép thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else return;
                }
                else MessageBox.Show("Đơn nghỉ phép này của bạn đã được cập nhật trạng thái !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message); return;
            }
        }

        // Ham Load du lieu
        private void ChayLaiDuLieu()
        {
            _idSelected = null;

            var thangHienTai = DateTime.Now.Month;

            var DsNghiPhepTheoIDNV = new List<NghiPhep>();
            var idNhanVienNP = _dbContextNP.LayDsNghiPhep().FirstOrDefault(p => p.idNhanVien == _idNhanVien);

            if (_idNhanVien.Contains("GD"))
            {
                DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep();
            }
            else DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList();

            txtSoNgayNghi.Text = DsNghiPhepTheoIDNV.Count(p => p.TrangThai.Equals("Duyệt", StringComparison.OrdinalIgnoreCase)).ToString();

            if (int.TryParse(SoNgayNghiCoPhep(DsNghiPhepTheoIDNV, thangHienTai), out int countCoLuong) && countCoLuong >= 3)
            {
                cmbLoaiNghi.DataSource = new List<string> { "Nghỉ phép không lương" };
            }
            else cmbLoaiNghi.DataSource = new List<string> { "Nghỉ phép có lương", "Nghỉ phép không lương" };

            dgvDSNghiPhepCaNhan.DataSource = DsNghiPhepTheoIDNV.Select(p => new { p.id, p.NgayBatDau, p.NgayKetThuc, SoNgayNghi = (p.NgayKetThuc.Day - p.NgayBatDau.Day + 1).ToString(), p.LyDoNghi, p.LoaiNghiPhep, p.TrangThai, }).OrderByDescending(p => p.id).ToList();

            if (dgvDSNghiPhepCaNhan.Columns["id"].Visible)
            {
                dgvDSNghiPhepCaNhan.Columns["id"].Visible = false;
            }

            txtSoNgayCoPhep.Text = DsNghiPhepTheoIDNV.Count(p => p.LoaiNghiPhep.Equals("Nghỉ phép có lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Duyệt", StringComparison.OrdinalIgnoreCase)).ToString();
            txtSoNgayKhongPhep.Text = DsNghiPhepTheoIDNV.Count(p => p.LoaiNghiPhep.Equals("Nghỉ phép không lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Duyệt", StringComparison.OrdinalIgnoreCase)).ToString();

            var nhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien);
            var ngayTrongThang = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var luongCBNV = _dbContextCV.LayDsChucVu().FirstOrDefault(p => p.id == nhanVien.idChucVu).luongCoBan;
            var tinhTienPhat = (double)luongCBNV / ngayTrongThang;

            int soLuong = DsNghiPhepTheoIDNV.Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + DsNghiPhepTheoIDNV.Count;
            dgvDSLichSuNP.DataSource = DsNghiPhepTheoIDNV.Where(p => p.LoaiNghiPhep.Equals("Nghỉ phép không lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Duyệt", StringComparison.OrdinalIgnoreCase) && p.NgayBatDau.Month == DateTime.Now.Month && p.NgayBatDau.Year == DateTime.Now.Year)
                                                   .Select(p => new { NgayNghi = $"{p.NgayBatDau.Day}/{thangHienTai} - {p.NgayKetThuc.Day}/{thangHienTai}", TruTien = "- " + string.Format(new CultureInfo("vi-VN"), "{0:C0}", tinhTienPhat * (p.NgayKetThuc.Day - p.NgayBatDau.Day + 1)) }).ToList();

            if (idNhanVienNP != null)
            {
                var ngayKetThuc = _dbContextNP.LayDsNghiPhep().Max(p => p.NgayKetThuc);
                dtpBatDau.Value = ngayKetThuc.AddDays(1);
                dtpKetThuc.Value = dtpBatDau.Value;
            }
            else
            {
                dtpBatDau.Value = DateTime.Now.AddDays(1);
                dtpKetThuc.Value = dtpBatDau.Value;
            }
            LoadHeaderText();
        }

        // Ham tra ve field trong
        public void Empty() => rtLyDo.Text = string.Empty;

        // Bang loc thang 
        private void cmbLocThang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var DsNghiPhep = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList();
            var selectItemFilter = cmbLocThang.SelectedItem == null ? DateTime.Now.Month : (int)cmbLocThang.SelectedItem;

            txtSoNgayCoPhep.Text = SoNgayNghiCoPhep(DsNghiPhep, selectItemFilter);

            txtSoNgayKhongPhep.Text = SoNgayNghiKhongPhep(DsNghiPhep, selectItemFilter);

            dgvDSNghiPhepCaNhan.DataSource = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien && p.NgayBatDau.Month == (int)cmbLocThang.SelectedItem && p.NgayBatDau.Year == DateTime.Now.Year)
                                                                        .Select(p => new { p.id, p.NgayBatDau, p.NgayKetThuc, SoNgayNghi = (p.NgayKetThuc.Day - p.NgayBatDau.Day + 1).ToString(), p.LyDoNghi, p.LoaiNghiPhep, p.TrangThai, })
                                                                        .OrderByDescending(p => p.id)
                                                                        .ToList();

            if (dgvDSNghiPhepCaNhan.Columns["id"].Visible)
            {
                dgvDSNghiPhepCaNhan.Columns["id"].Visible = false;
            }
        }

        private void dgvDSNghiPhepCaNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                _idSelected = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["id"]?.Value.ToString();
                lblIDNV.Text = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["id"]?.Value.ToString();
                rtLyDo.Text = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["LyDonghi"]?.Value.ToString();
                cmbLoaiNghi.Text = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["LoaiNghiPhep"]?.Value.ToString();
                dtpBatDau.Value = DateTime.Parse(dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["NgayBatDau"]?.Value.ToString());
                dtpKetThuc.Value = DateTime.Parse(dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["NgayKetThuc"]?.Value.ToString());

                var trangThai = _dbContextNP.KtraNghiPhepQuaID(Convert.ToInt32(_idSelected));
                if (trangThai != null)
                {
                    dtpBatDau.Enabled = false;
                    dtpKetThuc.Enabled = false;
                }
                else
                {
                    dtpBatDau.Enabled = true;
                    dtpKetThuc.Enabled = true;
                }
            }
        }

        // Reload du lieu
        private void btnLoadDuLieu_Click(object sender, EventArgs e) => ChayLaiDuLieu();

        // Lay so ngay co phep theo thang
        private string SoNgayNghiCoPhep(List<NghiPhep> DsNghiPhep, int thangHienTai)
        {
            var nghiCoLuong = DsNghiPhep.Where(p => p.LoaiNghiPhep.Equals("Nghỉ phép có lương", StringComparison.OrdinalIgnoreCase)).ToList();
            return nghiCoLuong.Where(p => p.NgayBatDau.Month == thangHienTai && p.NgayBatDau.Year == DateTime.Now.Year).ToList()
                                            .Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiCoLuong.Count + "";
        }

        // Lay so ngay khong phep theo thang
        private string SoNgayNghiKhongPhep(List<NghiPhep> DsNghiPhep, int thangHienTai)
        {
            var nghiKhongLuong = DsNghiPhep.Where(p => p.LoaiNghiPhep.Equals("Nghỉ phép không lương", StringComparison.OrdinalIgnoreCase)).ToList();
            return nghiKhongLuong.Where(p => p.NgayBatDau.Month == thangHienTai && p.NgayBatDau.Year == DateTime.Now.Year).ToList()
                                            .Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiKhongLuong.Count + "";
        }

        private void dgvDSLichSuNP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {

            }
        }

        // Xu ly click ngay bat dau
        private void dtpBatDau_CloseUp(object sender, EventArgs e)
        {
            var ngayKetThuc = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).Max(p => p.NgayKetThuc);
            if (dtpBatDau.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Không được chọn thời gian trong quá khứ !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpBatDau.Value = ngayKetThuc.AddDays(1);
            }
            else if (dtpBatDau.Value.Date > dtpKetThuc.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpBatDau.Value = ngayKetThuc.AddDays(1);
            }
            else if (dtpBatDau.Value.Month != DateTime.Now.Month)
            {
                MessageBox.Show("Chỉ được đệ đơn nghỉ phép trong tháng hiện tại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpBatDau.Value = ngayKetThuc.AddDays(1);
            }
            else if (dtpBatDau.Value.Year != DateTime.Now.Year)
            {
                MessageBox.Show("Chỉ được đệ đơn nghỉ phép trong năm hiện tại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpBatDau.Value = ngayKetThuc.AddDays(1);
            }
        }

        // Xu ly click ngay ket thuc
        private void dtpKetThuc_CloseUp(object sender, EventArgs e)
        {
            if (dtpKetThuc.Value.Date < dtpBatDau.Value.Date)
            {
                MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpKetThuc.Value = dtpBatDau.Value.AddDays(1);
            }
            else if (dtpKetThuc.Value.Month != DateTime.Now.Month)
            {
                MessageBox.Show("Chỉ được đệ đơn nghỉ phép trong tháng hiện tại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpKetThuc.Value = DateTime.Now;
            }
            else if (dtpKetThuc.Value.Year != DateTime.Now.Year)
            {
                MessageBox.Show("Chỉ được đệ đơn nghỉ phép trong năm hiện tại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpKetThuc.Value = DateTime.Now;
            }
        }

        private void cmbLocThang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void LoadHeaderText()
        {
            //dgvDSLichSuNP.Columns["LyDoNghi"].HeaderText = "Lý do nghỉ";
            dgvDSLichSuNP.Columns["TruTien"].HeaderText = "Số tiền trừ";
            dgvDSLichSuNP.Columns["NgayNghi"].HeaderText = "Ngày nghỉ";

            dgvDSNghiPhepCaNhan.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
            dgvDSNghiPhepCaNhan.Columns["NgayKetThuc"].HeaderText = "Ngày kết thúc";
            dgvDSNghiPhepCaNhan.Columns["SoNgayNghi"].HeaderText = "Số ngày nghỉ";
            dgvDSNghiPhepCaNhan.Columns["LyDoNghi"].HeaderText = "Lý do nghỉ";
            dgvDSNghiPhepCaNhan.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvDSNghiPhepCaNhan.Columns["LoaiNghiPhep"].HeaderText = "Loại nghỉ phép";

            dgvDSNghiPhepCaNhan.Columns["LyDoNghi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        public bool KtraDuLieuBDKT(bool loai)
        {
            var dataExist = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList();
            if (dataExist.Count > 0)
            {
                var ngayKetThuc = dataExist.Max(p => p.NgayKetThuc);

                if (loai)
                {
                    if (dtpBatDau.Value.Date <= ngayKetThuc)
                    {
                        return false;
                    }
                }
                else
                {
                    if (dtpBatDau.Value.Date < ngayKetThuc)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
