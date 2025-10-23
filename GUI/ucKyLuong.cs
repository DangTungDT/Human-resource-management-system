using BLL;
using DTO;
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
        private readonly string _idNhanVien, _conn;
        private readonly BLLKyLuong _dbContextKL;

        public ucKyLuong(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;
            _dbContextKL = new BLLKyLuong(conn);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ucKyLuong_Load(object sender, EventArgs e)
        {
            cmbTrangThai.DataSource = new List<string> { "Đã trả", "Chưa giải quyết" };
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
            dtpNgayChiTra.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dbContextKL.KtraThemKyLuong(new DTOKyLuong(0, dtpBatDau.Value, dtpKetThuc.Value, dtpNgayChiTra.Value, cmbTrangThai.Text)))
                {
                    MessageBox.Show("Thêm kỳ lương thành công.");
                    dgvKyLuong.DataSource = ChayLaiDuLieu();
                }
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

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dtpNgayChiTra.Enabled = false;
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

            if (KtraLuongThang13())
            {

            }
            return dsKyLuong;
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

        private bool KtraLuongThang13()
        {
            var lastMonthInYear = _dbContextKL.KtraDsKyLuong().FirstOrDefault(p => p.ngayBatDau.Value.Month == 12 && p.ngayBatDau.Value.Year == DateTime.Now.Year);

            if (lastMonthInYear == null)
            {
                return false;
            }

            return true;
        }
    }
}
