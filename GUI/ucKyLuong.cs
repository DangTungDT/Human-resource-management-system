using BLL;
using DTO;
using Microsoft.Win32.SafeHandles;
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
    public partial class ucKyLuong : UserControl
    {
        public string _idSelected { get; set; }
        public bool _isChecked { get; set; } = true;

        private readonly string _idNhanVien, _conn;
        private readonly BLLKyLuong _dbContextKL;
        private readonly BLLChiTietLuong _dbContextCTL;

        public ucKyLuong(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;
            _dbContextKL = new BLLKyLuong(conn);
            _dbContextCTL = new BLLChiTietLuong(conn);
        }


        private void ucKyLuong_Load(object sender, EventArgs e)
        {
            cmbTrangThai.DataSource = new List<string> { "Chưa giải quyết" };
            dgvKyLuong.DataSource = ChayLaiDuLieu();

            if (dgvKyLuong.Columns["id"] != null)
            {
                if (dgvKyLuong.Columns["id"].Visible == true)
                {
                    dgvKyLuong.Columns["id"].Visible = false;
                }
            }
        }

        private void btnXuLy_Click(object sender, EventArgs e)
        {
            if (_isChecked)
            {
                btnXuLy.Text = "Đóng";
                dtpNgayChiTra.Enabled = true;
                cmbTrangThai.DataSource = new List<string> { "Đang giải quyết", "Chưa giải quyết" };
                _isChecked = false;
            }
            else
            {
                btnXuLy.Text = "Mở";
                dtpNgayChiTra.Enabled = false;
                cmbTrangThai.DataSource = new List<string> { "Chưa giải quyết" };
                _isChecked = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ktraDuLieu())
                {
                    if (_dbContextKL.KtraThemKyLuong(new DTOKyLuong(0, dtpBatDau.Value, dtpKetThuc.Value, dtpNgayChiTra.Value, cmbTrangThai.Text)))
                    {
                        MessageBox.Show("Thêm kỳ lương thành công.");
                        dgvKyLuong.DataSource = ChayLaiDuLieu();
                    }
                }
                else MessageBox.Show($"Đã thêm kỳ lương tháng {DateTime.Now.ToString("MM/yyyy")}");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ktraDuLieu())
                {
                    if (dtpNgayChiTra.Enabled)
                    {
                        if (int.TryParse(_idSelected, out int id) && id > 0)
                        {
                            if (_dbContextKL.KtraCapNhatKyLuong(new DTOKyLuong(id, dtpBatDau.Value, dtpKetThuc.Value, dtpNgayChiTra.Value, cmbTrangThai.Text)))
                            {
                                MessageBox.Show("Cập nhật kỳ lương thành công.");
                                dgvKyLuong.DataSource = ChayLaiDuLieu();
                            }
                        }
                        else MessageBox.Show($"Không tìm thấy id dữ liệu !");
                    }
                    else MessageBox.Show($"Bạn cần cấp phép chỉnh sửa !");
                }
                else MessageBox.Show($"Đã thêm kỳ lương tháng {DateTime.Now.ToString("MM/yyyy")}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                _isChecked = true;
                btnXuLy.Text = "Mở";
                dtpNgayChiTra.Enabled = false;
                cmbTrangThai.DataSource = new List<string> { "Chưa giải quyết" };
            }
        }

        public object ChayLaiDuLieu()
        {
            var dsKyLuong = _dbContextKL.KtraDsKyLuong().OrderByDescending(p => p.ngayBatDau).ToList();

            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            dtpBatDau.Value = new DateTime(year, month, 1);
            dtpKetThuc.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            dtpNgayChiTra.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month)).AddDays(5);

            return dsKyLuong;
        }

        private void dgvKyLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                _idSelected = dgvKyLuong.Rows[e.RowIndex].Cells["id"].Value.ToString();
                cmbTrangThai.Text = dgvKyLuong.Rows[e.RowIndex].Cells["trangThai"].Value.ToString();
                dtpBatDau.Value = DateTime.Parse(dgvKyLuong.Rows[e.RowIndex].Cells["ngayBatDau"].Value.ToString());
                dtpKetThuc.Value = DateTime.Parse(dgvKyLuong.Rows[e.RowIndex].Cells["ngayKetThuc"].Value.ToString());
                dtpNgayChiTra.Value = DateTime.Parse(dgvKyLuong.Rows[e.RowIndex].Cells["ngayChiTra"].Value.ToString());

            }
        }

        private bool ktraDuLieu()
        {
            var isThangNull = _dbContextKL.KtraDsKyLuong().FirstOrDefault(p => p.ngayBatDau.Value.Month == DateTime.Now.Month);

            if (isThangNull == null)
            {
                return true;
            }

            return false;
        }

        private void dtpNgayChiTra_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgayChiTra_CloseUp(object sender, EventArgs e)
        {
            if (dtpNgayChiTra.Value.Date < dtpKetThuc.Value.Date)
            {
                MessageBox.Show("Không được chọn thời gian nhỏ hơn ngày kết thúc !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpNgayChiTra.Value = dtpKetThuc.Value.AddDays(1);
            }
            else if (dtpNgayChiTra.Value.Date > new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(9))
            {
                MessageBox.Show("Không được chọn thời gian lớn hơn ngày 10 của tháng sau !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpNgayChiTra.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(4);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(_idSelected, out int id) && id > 0)
                {
                    if (MessageBox.Show("Bạn có chắc muốn xóa kỳ lương này không ?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var ktraCTL = _dbContextCTL.KtraChiTietLuongQuaIDKyLuong(id);
                        if (!ktraCTL)
                        {
                            if (_dbContextKL.KtraXoaKyLuong(new DTOKyLuong(id)))
                            {
                                MessageBox.Show("Xóa kỳ lương thành công.");
                                dgvKyLuong.DataSource = ChayLaiDuLieu();
                            }
                        }
                        else MessageBox.Show($"Chi tiết lương đã áp dụng kỳ lương này từ ngày {dtpBatDau.Value.ToShortDateString()} !", "Phản hồi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else MessageBox.Show($"Không tìm thấy id dữ liệu !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }

    }
}
