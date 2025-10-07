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
    public partial class UCXemKyLuat : UserControl
    {
        private BLLThuongPhat _dbContext = new BLLThuongPhat();

        public UCXemKyLuat()
        {
            InitializeComponent();
        }

        private void UCXemKyLuat_Load(object sender, EventArgs e)
        {
            var dsKyLuat = _dbContext.CheckListThuongPhat().Where(p => p.Loai == "Phạt").ToList();
            var idNguoiTao = dsKyLuat.Select(p => p.IDNguoiTao).ToList();
            var dsNhanVien = _dbContext.CheckListNhanVien().Where(d => idNguoiTao.Contains(d.ID)).ToList();

            dgvDanhSachKyLuat.DataSource = dsKyLuat.Select(p => new
            {
                SoTienPhat = p.TienThuongPhat,
                p.Loai,
                p.LyDo,
                NguoiDanhGia = dsNhanVien.FirstOrDefault(q => q.ID == p.IDNguoiTao)?.TenNhanVien

            }).ToList();
        }


        private void dgvDanhSachKyLuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                lblTienPhat.Text = dgvDanhSachKyLuat.Rows[e.RowIndex].Cells[0].Value?.ToString();
                lblLoai.Text = dgvDanhSachKyLuat.Rows[e.RowIndex].Cells[1].Value?.ToString();
                lblLyDo.Text = dgvDanhSachKyLuat.Rows[e.RowIndex].Cells[2].Value?.ToString();
                lblNguoiTao.Text = dgvDanhSachKyLuat.Rows[e.RowIndex].Cells[3].Value?.ToString();
            }
        }
    }
}
