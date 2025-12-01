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
        private readonly BLLThuongPhat _dbContext;
        public readonly string _idNhanVien, _conn;

        public UCXemKyLuat(string stringConnection, string idNhanVien)
        {
            InitializeComponent();

            _conn = stringConnection;
            _idNhanVien = idNhanVien;
            _dbContext = new BLLThuongPhat(stringConnection);
        }

        private void UCXemKyLuat_Load(object sender, EventArgs e)
        {
            var dsKyLuat = _dbContext.CheckListThuongPhat().Where(p => p.loai == "Phạt").ToList();
            var idNguoiTao = dsKyLuat.Select(p => p.idNguoiTao).ToList();
            var dsNhanVien = _dbContext.CheckListNhanVien().Where(d => idNguoiTao.Contains(d.ID)).ToList();

            dgvDanhSachKyLuat.DataSource = dsKyLuat.Select(p => new
            {
                SoTienPhat = p.tienThuongPhat,
                p.loai,
                p.lyDo,
                NguoiDanhGia = dsNhanVien.FirstOrDefault(q => q.ID == p.idNguoiTao)?.TenNhanVien

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
