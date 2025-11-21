using BLL;
using DAL;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2013.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ucXemChamCong : UserControl
    {
        public readonly string _idNhanVien, _conn;

        private readonly BLLChamCong _dbContextCC;
        private readonly BLLNhanVien _dbContextNV;
        private readonly BLLPhongBan _dbContextPB;

        public ucXemChamCong(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;
            _dbContextCC = new BLLChamCong(conn);
            _dbContextNV = new BLLNhanVien(conn);
            _dbContextPB = new BLLPhongBan(conn);
        }

        private void dgvChamCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                txtGioRa.Text = dgvChamCong.Rows[e.RowIndex].Cells["GioRa"].Value?.ToString();
                txtGioVao.Text = dgvChamCong.Rows[e.RowIndex].Cells["GioVao"].Value?.ToString();
                txtNhanVien.Text = dgvChamCong.Rows[e.RowIndex].Cells["TenNhanVien"].Value?.ToString();
                txtTrangThai.Text = dgvChamCong.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString();
                txtGioTangCa.Text = dgvChamCong.Rows[e.RowIndex].Cells["GioTangCa"].Value?.ToString();
                txtTongGioLam.Text = dgvChamCong.Rows[e.RowIndex].Cells["TongGioLam"].Value?.ToString();
                txtNgayChamCong.Text = dgvChamCong.Rows[e.RowIndex].Cells["NgayChamCong"].Value?.ToString();
            }
        }

        private void ucXemChamCong_Load(object sender, EventArgs e)
        {
            LoadGeneral();
        }

        // Xu ly load du lieu
        public object LoadDuLieu(int? locPhongBan = 0)
        {
            try
            {
                object anonymous = new object();

                var dsNhanVien = _dbContextNV.KtraDsNhanVien();
                var dsChamCong = _dbContextCC.LayDsChamCong();
                var dsPhongBan = _dbContextPB.KtraDsPhongBan();
                var nhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien);

                anonymous = dsChamCong.Join(dsNhanVien, cc => cc.idNhanVien, nv => nv.id, (cc, nv) => new { cc, nv })

                    .Join(dsPhongBan, nvcc => nvcc.nv.idPhongBan, pb => pb.id, (nvcc, pb) => new { nvcc.cc, nvcc.nv, pb })
                    .Where(p => (_idNhanVien.Contains("GD")
                    ? cmbPhongBan.SelectedIndex == 0
                        ? true
                        : cmbPhongBan.SelectedItem != null
                            ? p.nv.idPhongBan == locPhongBan
                            : true
                    : p.nv.idPhongBan == nhanVien.idPhongBan) && p.cc.NgayChamCong.Month == DateTime.Now.Date.Month)
                    .Select(s =>
                    {
                        TimeSpan gioVao = s.cc.GioVao ?? TimeSpan.Zero;
                        TimeSpan gioRa = s.cc.GioRa ?? TimeSpan.Zero;
                        TimeSpan tongGio = gioRa - gioVao - TimeSpan.FromHours(1);

                        if (tongGio < TimeSpan.Zero)
                        {
                            tongGio = TimeSpan.Zero;
                        }

                        string trangThai = TrangThai(gioVao, gioRa);
                        return new
                        {
                            ID = s.cc.id,
                            IDNhanVien = s.nv.id,
                            TenNhanVien = s.nv.TenNhanVien,
                            NgayChamCong = s.cc.NgayChamCong.ToString("dd/MM/yyyy"),
                            GioVao = (s.cc.GioVao?.ToString(@"hh\:mm")) ?? "00:00",
                            GioRa = (s.cc.GioRa?.ToString(@"hh\:mm")) ?? "00:00",
                            GioTangCa = (s.cc.GioRa?.ToString(@"hh\:mm")) ?? "00:00",
                            TongGioLam = $"{(int)tongGio.TotalHours:0} giờ",
                            TrangThai = trangThai,
                        };

                    }).OrderByDescending(p => p.NgayChamCong).ToList();

                return anonymous;
            }
            catch
            {
                return null;
            }
        }

        // Kiem tra trang thai
        public string TrangThai(TimeSpan gioVao, TimeSpan gioRa)
        {
            TimeSpan diTre = TimeSpan.Zero;
            TimeSpan veSom = TimeSpan.Zero;

            TimeSpan gioVaoChuan = new TimeSpan(8, 0, 0);
            TimeSpan gioRaChuan = new TimeSpan(17, 0, 0);
            TimeSpan tongGio = gioRa - gioVao - TimeSpan.FromHours(1);

            if (tongGio < TimeSpan.Zero)
            {
                tongGio = TimeSpan.Zero;
            }

            if (gioVao > gioVaoChuan)
            {
                diTre = gioVao - gioVaoChuan;
            }

            if (gioRa < gioRaChuan)
            {
                veSom = gioRaChuan - gioRa;
            }

            string TrangThai;
            if (tongGio.TotalHours < 4)
            {
                TrangThai = "Mất 1 ngày công";
            }
            else if (tongGio.TotalHours < 8)
            {
                TrangThai = "Mất nửa ngày công";
            }
            else if (diTre.TotalMinutes > 0 && diTre.TotalMinutes <= 15)
            {
                TrangThai = $"Đi trễ {diTre.Minutes} phút";
            }
            else if (diTre.TotalMinutes > 15)
            {
                TrangThai = $"Đi trễ quá 15 phút, trừ nửa công";
            }
            else if (veSom.TotalMinutes > 0)
            {
                TrangThai = $"Về sớm {veSom.Minutes} phút, nhắc nhở";
            }
            else TrangThai = "Đủ giờ làm";

            return TrangThai;
        }

        // Thay doi text header datagridview
        public void HeaderTextGeneral()
        {
            dgvChamCong.Columns["ID"].Visible = false;
            dgvChamCong.Columns["IDNhanVien"].Visible = false;
            dgvChamCong.Columns["GioRa"].HeaderText = "Giờ ra";
            dgvChamCong.Columns["GioVao"].HeaderText = "Giờ vào";
            dgvChamCong.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvChamCong.Columns["GioTangCa"].HeaderText = "Giờ tăng ca";
            dgvChamCong.Columns["TongGioLam"].HeaderText = "Tổng giờ làm";
            dgvChamCong.Columns["NgayChamCong"].HeaderText = "Ngày chấm công";
        }

        private void cmbPhongBan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dgvChamCong.DataSource = LoadDuLieu((int)cmbPhongBan.SelectedValue);
        }

        // Ham load chung
        public void LoadGeneral()
        {
            var dsPhongBan = _dbContextPB.KtraDsPhongBan();

            dsPhongBan.Insert(0, new PhongBan { id = 0, TenPhongBan = "Tất cả" });
            cmbPhongBan.DataSource = dsPhongBan;
            cmbPhongBan.ValueMember = "id";
            cmbPhongBan.DisplayMember = "TenPhongBan";
            cmbPhongBan.SelectedIndex = 0;

            dgvChamCong.DataSource = LoadDuLieu();
            HeaderTextGeneral();
        }
    }
}
