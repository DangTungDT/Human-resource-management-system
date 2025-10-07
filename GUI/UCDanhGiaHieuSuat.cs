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
    public partial class UCDanhGiaHieuSuat : UserControl
    {
        private BLLDanhGiaNhanVien _dbContext = new BLLDanhGiaNhanVien();
        private BLLThuongPhat _dbContextTP = new BLLThuongPhat();

        public UCDanhGiaHieuSuat()
        {
            InitializeComponent();
        }

        private void UCDanhGiaHieuSuat_Load(object sender, EventArgs e)
        {
            var dsHieuSuat = _dbContext.CheckListDanhGiaNhanVien();
            var dsNhanVien = _dbContextTP.CheckListNhanVien().ToList();

            dgvDSHieuSuat.DataSource = dsHieuSuat.GroupBy(g => g.IDNhanVien).Select(p => new
            {
                NhanVien = dsNhanVien.Where(n => n.ID == p.FirstOrDefault(a => a.IDNhanVien == p.Key)?.IDNhanVien).Select(s => s.TenNhanVien).FirstOrDefault(),
                DTB = p.Average(a => a.DiemSo),
                DiemSo = string.Join(", ", p.Select(s => s.DiemSo)),
                NhanXet = p.FirstOrDefault(a => a.IDNhanVien == p.Key)?.NhanXet,
                NgayTao = p.FirstOrDefault(a => a.IDNhanVien == p.Key)?.NgayTao,
                NguoiDanhGia = string.Join(", ", p.Select(s => dsNhanVien.FirstOrDefault(a => a.ID == s.IDNguoiDanhGia)?.TenNhanVien)),

            }).ToList();
        }

        private void dgvDSHieuSuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                txtNhanVien.Text = dgvDSHieuSuat.Rows[e.RowIndex].Cells[0].Value?.ToString();
                txtDTB.Text = dgvDSHieuSuat.Rows[e.RowIndex].Cells[1].Value?.ToString();
                txtDiemSo.Text = dgvDSHieuSuat.Rows[e.RowIndex].Cells[2].Value?.ToString();
                rtxtNhanXet.Text = dgvDSHieuSuat.Rows[e.RowIndex].Cells[3].Value?.ToString();
                txtNgayTao.Text = dgvDSHieuSuat.Rows[e.RowIndex].Cells[4].Value?.ToString();
                lblNguoiDanhGia.Text = dgvDSHieuSuat.Rows[e.RowIndex].Cells[5].Value?.ToString();
            }
        }
    }
}
