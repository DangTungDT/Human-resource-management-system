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
        private readonly string _idNhanVien;
        private readonly BLLThuongPhat _dbContextTP;
        private readonly BLLDanhGiaNhanVien _dbContext;

        public UCDanhGiaHieuSuat(string idNhanVien, string stringConnection)
        {
            InitializeComponent();

            _idNhanVien = idNhanVien;
            _dbContextTP = new BLLThuongPhat(stringConnection);
            _dbContext = new BLLDanhGiaNhanVien(stringConnection);
        }

        private void UCDanhGiaHieuSuat_Load(object sender, EventArgs e)
        {
            ChayLaiDuLieu();
        }

        private void dgvDSHieuSuatNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                txtDiemTB.Text = dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["DiemTB"].Value?.ToString() ?? string.Empty;
                rtNhanXet.Text = dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["NhanXet"].Value?.ToString() ?? string.Empty;
                txtNhanVien.Text = dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["NhanVien"].Value?.ToString() ?? string.Empty;
                txtDiemSo.Text = dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["DiemDanhGia"].Value?.ToString() ?? string.Empty;
                dtpNgayTao.Value = DateTime.Parse(dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["NgayTao"].Value?.ToString());
                grbDanhGiaHS.Text = "NGƯỜI ĐÁNH GIÁ: " + dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["NguoiDanhGia"].Value?.ToString() ?? string.Empty;
            }
        }

        // Load du lieu
        private void ChayLaiDuLieu()
        {
            var dsHieuSuat = _dbContext.CheckListDanhGiaNhanVien();
            var dsNhanVien = _dbContextTP.CheckListNhanVien().ToList();

            dgvDSHieuSuatNV.DataSource = dsHieuSuat.GroupBy(g => g.IDNhanVien).Select(p => new
            {
                NhanVien = dsNhanVien.Where(n => n.ID == p.FirstOrDefault(a => a.IDNhanVien == p.Key)?.IDNhanVien).Select(s => s.TenNhanVien).FirstOrDefault(),
                DiemTB = p.Average(a => a.DiemSo),
                DiemDanhGia = string.Join(", ", p.Select(s => s.DiemSo)),
                NhanXet = p.FirstOrDefault(a => a.IDNhanVien == p.Key)?.NhanXet,
                NgayTao = p.FirstOrDefault(a => a.IDNhanVien == p.Key)?.NgayTao,
                NguoiDanhGia = string.Join(", ", p.Select(s => dsNhanVien.FirstOrDefault(a => a.ID == s.IDNguoiDanhGia)?.TenNhanVien)),

            }).ToList();
        }

        private void btnLoadDuLieu_Click(object sender, EventArgs e) => ChayLaiDuLieu();
    }
}
