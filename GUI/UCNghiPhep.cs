using BLL;
using CrystalDecisions.CrystalReports.Engine;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCNghiPhep : UserControl
    {
        public readonly string _idNhanVien;
        public readonly BLLNghiPhep _dbContextNP;
        public readonly BLLNhanVien _dbContextNV;

        public UCNghiPhep(string idNhanVien, string connect)
        {
            InitializeComponent();
            _idNhanVien = idNhanVien.ToUpper();
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

                dtpBatDau.Enabled = false;
                dtpBatDau.Value = DateTime.Today;
                dtpKetThuc.Value = dtpBatDau.Value.AddDays(1);

                grbNhanVien.Text += _dbContextNV.KtraNhanVienQuaID(_idNhanVien).TenNhanVien;

                ChayLaiDuLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trang load: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            try
            {
                if (KiemTraDuLieuDauVao())
                {
                    if (_dbContextNP.KtraThemNghiPhep(new DTONghiPhep(0, _idNhanVien, dtpBatDau.Value, dtpKetThuc.Value, rtLyDo.Text, cmbLoaiNghi.Text, txtTrangThai.Text)))
                    {
                        ChayLaiDuLieu();
                        MessageBox.Show("Thêm dữ liệu thành công.");
                        Empty();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (KiemTraDuLieuDauVao())
                {
                    if (_dbContextNP.KtraCapNhatNghiPhep(new DTONghiPhep(int.Parse(lblIDNV.Text), _idNhanVien, dtpBatDau.Value, dtpKetThuc.Value, rtLyDo.Text, cmbLoaiNghi.Text, txtTrangThai.Text)))
                    {
                        ChayLaiDuLieu();
                        MessageBox.Show("Cập nhật dữ liệu thành công.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dbContextNP.KtraXoaNghiPhep(new DTONghiPhep(int.Parse(lblIDNV.Text), _idNhanVien)))
                {
                    ChayLaiDuLieu();
                    MessageBox.Show("Xóa dữ liệu thành công.");
                    Empty();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }

        private void ChayLaiDuLieu()
        {
            var thangHienTai = DateTime.Now.Month;

            var DsNghiPhep = _dbContextNP.LayDsNghiPhep().Where(p => p.IDNhanVien == _idNhanVien).ToList();

            txtSoNgayNghi.Text = DsNghiPhep.Count.ToString();

            dgvDSLuongCaNhan.DataSource = DsNghiPhep.Select(p => new { p.LyDoNghi, p.LoaiNghiPhep }).ToList();

            if (int.TryParse(SoNgayNghiCoPhep(DsNghiPhep, thangHienTai), out int countCoLuong) && countCoLuong >= 3)
            {
                cmbLoaiNghi.DataSource = new List<string> { "Nghỉ phép không lương" };
            }
            else cmbLoaiNghi.DataSource = new List<string> { "Nghỉ phép có lương", "Nghỉ phép không lương" };


            dgvDSNghiPhepCaNhan.DataSource = DsNghiPhep.Select(p => new { p.ID, p.NgayBatDau, p.NgayKetThuc, p.LyDoNghi, p.LoaiNghiPhep, p.TrangThai }).ToList();

            if (dgvDSNghiPhepCaNhan.Columns["id"].Visible == true)
            {
                dgvDSNghiPhepCaNhan.Columns["id"].Visible = false;
            }

            txtSoNgayCoPhep.Text = DsNghiPhep.Count(p => p.LoaiNghiPhep.Equals("Nghỉ phép có lương", StringComparison.OrdinalIgnoreCase)).ToString();
            txtSoNgayKhongPhep.Text = DsNghiPhep.Count(p => p.LoaiNghiPhep.Equals("Nghỉ phép không lương", StringComparison.OrdinalIgnoreCase)).ToString();
        }

        public void Empty()
        {
            rtLyDo.Text = string.Empty;
            //foreach (var control in grbNhanVien.Controls)
            //{
            //    if (control is RichTextBox richtext) richtext.Text = string.Empty;

            //    if (control is Guna2ComboBox combobox) combobox.SelectedIndex = -1;
            //}
        }

        private void dtpKetThuc_ValueChanged(object sender, EventArgs e)
        {
            if (dtpKetThuc.Value < dtpBatDau.Value)
            {
                MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpKetThuc.Value = dtpBatDau.Value.AddDays(1);
            }
        }

        private void cmbLocThang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var DsNghiPhep = _dbContextNP.LayDsNghiPhep().Where(p => p.IDNhanVien == _idNhanVien).ToList();
            var selectItemFilter = cmbLocThang.SelectedItem == null ? 1 : (int)cmbLocThang.SelectedItem;

            txtSoNgayCoPhep.Text = SoNgayNghiCoPhep(DsNghiPhep, selectItemFilter);

            txtSoNgayKhongPhep.Text = SoNgayNghiKhongPhep(DsNghiPhep, selectItemFilter);

            dgvDSNghiPhepCaNhan.DataSource = _dbContextNP.LayDsNghiPhep().Where(p => p.IDNhanVien == _idNhanVien && p.NgayBatDau.Month == (int)cmbLocThang.SelectedItem && p.NgayBatDau.Year == DateTime.Now.Year)
                                                                        .Select(p => new { p.ID, p.NgayBatDau, p.NgayKetThuc, p.LyDoNghi, p.LoaiNghiPhep, p.TrangThai })
                                                                        .ToList();

            if (dgvDSNghiPhepCaNhan.Columns["id"].Visible == true)
            {
                dgvDSNghiPhepCaNhan.Columns["id"].Visible = false;
            }
        }

        private void dgvDSNghiPhepCaNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                lblIDNV.Text = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["id"].Value.ToString();
                cmbLoaiNghi.Text = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["LoaiNghiPhep"].Value.ToString();
                rtLyDo.Text = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["LyDonghi"].Value.ToString();
                dtpBatDau.Value = DateTime.Parse(dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["NgayBatDau"].Value.ToString());
                dtpKetThuc.Value = DateTime.Parse(dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["NgayKetThuc"].Value.ToString());
            }
        }

        private void btnLoadDuLieu_Click(object sender, EventArgs e) => ChayLaiDuLieu();

        private string SoNgayNghiCoPhep(List<DTONghiPhep> DsNghiPhep, int selectItemFilter)
        {
            return DsNghiPhep.Count(p => p.LoaiNghiPhep.Equals("Nghỉ phép có lương", StringComparison.OrdinalIgnoreCase) && p.NgayBatDau.Month == selectItemFilter && p.NgayBatDau.Year == DateTime.Now.Year).ToString();
        }

        private string SoNgayNghiKhongPhep(List<DTONghiPhep> DsNghiPhep, int selectItemFilter)
        {
            return DsNghiPhep.Count(p => p.LoaiNghiPhep.Equals("Nghỉ phép không lương", StringComparison.OrdinalIgnoreCase) && p.NgayBatDau.Month == selectItemFilter && p.NgayBatDau.Year == DateTime.Now.Year).ToString();
        }

    }
}
