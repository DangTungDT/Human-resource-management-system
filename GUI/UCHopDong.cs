using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCHopDong : UserControl
    {
        private BLLHopDongLaoDong _dbContext;
        private BLLThuongPhat _dbContextTP;

        public UCHopDong(string stringConnection)
        {
            _dbContext = new BLLHopDongLaoDong(stringConnection);
            _dbContextTP = new BLLThuongPhat(stringConnection);
            InitializeComponent();
        }

        private void UCHopDong_Load(object sender, EventArgs e)
        {
            var dsHieuSuat = _dbContext.CheckListHopDongLaoDong();
            var dsNhanVien = _dbContextTP.CheckListNhanVien();

            dgvDanhSachHDLD.DataSource = _dbContext.CheckListHopDongLaoDong().Select(p => new
            {
                p.IDNhanVien,
                NhanVien = dsNhanVien.FirstOrDefault(q => q.ID == p.IDNhanVien)?.TenNhanVien ?? string.Empty,
                p.LoaiHopDong,
                p.MoTa,
                p.NgayKy,
                p.NgayBatDau,
                p.NgayKetThuc

            }).ToList();

            cmbLoaiHD.DataSource = _dbContext.CheckListHopDongLaoDong().Select(p => p.LoaiHopDong).Distinct().ToList();
            cmbMoTa.DataSource = _dbContext.CheckListHopDongLaoDong().Select(p => p.MoTa).Distinct().ToList();
            //cmbLoaiHD.DisplayMember = "LoaiHopDong";
            //cmbLoaiHD.ValueMember = "LoaiHopDong";
        }

        private void dgvDanhSachHDLD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                txtMaNV.Text = dgvDanhSachHDLD.Rows[e.RowIndex].Cells[0].Value?.ToString();
                cmbLoaiHD.Text = dgvDanhSachHDLD.Rows[e.RowIndex].Cells[2].Value?.ToString();
                cmbMoTa.Text = dgvDanhSachHDLD.Rows[e.RowIndex].Cells[3].Value?.ToString();
                dtpNgayKy.Text = dgvDanhSachHDLD.Rows[e.RowIndex].Cells[4].Value?.ToString();
                dtpBatDau.Text = dgvDanhSachHDLD.Rows[e.RowIndex].Cells[5].Value?.ToString();
                dtpKetThuc.Text = dgvDanhSachHDLD.Rows[e.RowIndex].Cells[6].Value?.ToString();
            }
        }
    }
}
