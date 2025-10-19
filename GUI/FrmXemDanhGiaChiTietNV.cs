using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.ModelBinding;
using System.Windows.Forms;

namespace GUI
{
    public partial class FrmXemDanhGiaChiTietNV : Form
    {
        private readonly string _idNhanVien;
        private readonly string _idNhguoiDanhGia;
        private readonly BLLNhanVien _dbContextNV;
        private readonly BLLHopDongLaoDong _dbContextHD;
        private readonly BLLDanhGiaNhanVien _dbContextDG;

        public FrmXemDanhGiaChiTietNV(string conn, string idNhanVien, string idNhguoiDanhGia)
        {
            InitializeComponent();
            _idNhanVien = idNhanVien;
            _idNhguoiDanhGia = idNhguoiDanhGia;
            _dbContextNV = new BLLNhanVien(conn);
            _dbContextHD = new BLLHopDongLaoDong(conn);
            _dbContextDG = new BLLDanhGiaNhanVien(conn);
        }

        private void FrmXemDanhGiaChiTietNV_Load(object sender, EventArgs e)
        {
            cmbNam.Items.Add("");
            LoadThangNamPhanLoai(0, 0, 3);

            grbNhanVien.Text = "Nhân viên: " + _dbContextNV.KtraDsNhanVien().FirstOrDefault(p => p.id == _idNhanVien).TenNhanVien;

            if (!string.IsNullOrEmpty(_idNhanVien))
            {
                var namLamViec = _dbContextHD.KtraDsHopDongLaoDong().FirstOrDefault(p => p.IDNhanVien == _idNhanVien);

                var namBD = namLamViec.NgayBatDau.Year;
                var namHT = DateTime.Now.Year;

                for (int i = namBD; i <= namHT; i++) cmbNam.Items.Add(i);

                if (cmbNam.Text != string.Empty)
                {
                    var namChon = Convert.ToInt32(cmbNam.Text);
                    cmbThang.DataSource = LoadThang(namChon);
                    cmbThang.SelectedIndex = -1;
                }
            }

            cmbPhanLoaiDiem.Items.AddRange(new[] { "Tốt", "Không tốt" });
        }

        // Load du lieu len dgv
        private object ChayLaiDuLieu(int nam, int thang, int phanLoai)
        {
            var anonymous = new object();
            var nhanVien = _dbContextNV.KtraDsNhanVien().FirstOrDefault(p => p.id == _idNhguoiDanhGia);
            var dsNhanVienDG = _dbContextDG.KtraDsDanhGiaNhanVien().Where(p => p.IDNhanVien == _idNhanVien).ToList();

            if (nam > 0 && thang > 0 && phanLoai == 1)
            {
                dsNhanVienDG = dsNhanVienDG.Where(p => p.NgayTao.Year == nam && p.NgayTao.Month == thang && p.DiemSo >= 7).ToList();
            }
            else if (nam > 0 && thang > 0 && phanLoai == 2)
            {
                dsNhanVienDG = dsNhanVienDG.Where(p => p.NgayTao.Year == nam && p.NgayTao.Month == thang && p.DiemSo < 7).ToList();
            }
            else if (nam > 0 && thang == 0 && phanLoai == 1)
            {
                dsNhanVienDG = dsNhanVienDG.Where(p => p.NgayTao.Year == nam && p.DiemSo >= 7).ToList();
            }
            else if (nam > 0 && thang == 0 && phanLoai == 2)
            {
                dsNhanVienDG = dsNhanVienDG.Where(p => p.NgayTao.Year == nam && p.DiemSo < 7).ToList();
            }
            else if (nam == 0 && thang == 0 && phanLoai == 1)
            {
                dsNhanVienDG = dsNhanVienDG.Where(p => p.DiemSo >= 7).ToList();
            }
            else if (nam == 0 && thang == 0 && phanLoai == 2)
            {
                dsNhanVienDG = dsNhanVienDG.Where(p => p.DiemSo < 7).ToList();
            }
            else if (nam > 0 && thang > 0)
            {
                dsNhanVienDG = dsNhanVienDG.Where(p => p.NgayTao.Year == nam && p.NgayTao.Month == thang).ToList();
            }
            else if (nam > 0)
            {
                dsNhanVienDG = dsNhanVienDG.Where(p => p.NgayTao.Year == nam).ToList();

            }

            anonymous = dsNhanVienDG.OrderByDescending(p => p.NgayTao).Select(p => new
            {
                NguoiDanhGia = nhanVien.TenNhanVien,
                DiemSo = p.DiemSo,
                NgayTao = p.NgayTao,
                NhanXet = p.NhanXet

            }).ToList();

            return anonymous;
        }

        // Load lai thang sau khi chon nam
        public List<int> LoadThang(int nam)
        {
            if (nam > 0)
            {
                return _dbContextDG.KtraDsDanhGiaNhanVien().Where(p => p.IDNhanVien == _idNhanVien && p.NgayTao.Year == nam)
                                                                                .Select(p => p.NgayTao.Month)
                                                                                .OrderBy(p => p)
                                                                                .ToList();
            }

            return new List<int>();
        }

        private void dgvDSDanhGiaNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                txtDiemSo.Text = dgvDSDanhGiaNV.Rows[e.RowIndex].Cells["DiemSo"]?.Value.ToString() ?? string.Empty;
                rtNhanXet.Text = dgvDSDanhGiaNV.Rows[e.RowIndex].Cells["NhanXet"]?.Value.ToString() ?? string.Empty;
                txtNgayTao.Text = DateTime.Parse(dgvDSDanhGiaNV.Rows[e.RowIndex].Cells["NgayTao"]?.Value.ToString()).ToShortDateString() ?? string.Empty;
            }
        }

        // Xu ly load du lieu nam len dgv
        private void cmbNam_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbNam.Text != string.Empty)
            {
                var namChon = Convert.ToInt32(cmbNam?.Text ?? "0");
                var thangList = LoadThang(namChon);

                cmbThang.DataSource = thangList;
                cmbThang.SelectedIndex = -1;

                LoadThangNamPhanLoai(namChon, 0, 3);
            }
            else
            {
                cmbThang.DataSource = null;
                cmbThang.Items.Clear();
                cmbThang.Text = string.Empty;

                LoadThangNamPhanLoai(0, 0, 3);
            }

            cmbPhanLoaiDiem.SelectedIndex = -1;
        }

        // Xu ly load du lieu thang, nam, phan loai diem > 7 hoac < 7 len dgv
        private void cmbPhanLoaiDiem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var nam = int.TryParse(cmbNam.Text, out int namChon);
            var thang = int.TryParse(cmbThang.Text, out int thangChon);

            if (namChon > 0 && thangChon > 0)
            {
                if (string.Equals(cmbPhanLoaiDiem.Text, "Tốt", StringComparison.OrdinalIgnoreCase))
                {
                    LoadThangNamPhanLoai(namChon, thangChon, 1);
                }
                else LoadThangNamPhanLoai(namChon, thangChon, 2);
            }
            else if (namChon > 0 && thangChon == 0)
            {
                if (string.Equals(cmbPhanLoaiDiem.Text, "Tốt", StringComparison.OrdinalIgnoreCase))
                {
                    LoadThangNamPhanLoai(namChon, 0, 1);
                }
                else LoadThangNamPhanLoai(namChon, 0, 2);
            }
            else
            {
                if (string.Equals(cmbPhanLoaiDiem.Text, "Tốt", StringComparison.OrdinalIgnoreCase))
                {
                    LoadThangNamPhanLoai(0, 0, 1);
                }
                else LoadThangNamPhanLoai(0, 0, 2);
            }
        }

        // Xu ly load du lieu thang len dgv
        private void cmbThang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbThang.Text != string.Empty)
            {
                var namChon = Convert.ToInt32(cmbNam?.Text ?? "0");
                var thangChon = Convert.ToInt32(cmbThang.Text);

                LoadThangNamPhanLoai(namChon, thangChon, 3);
            }

            cmbPhanLoaiDiem.SelectedIndex = -1;
        }

        private void LoadThangNamPhanLoai(int nam, int thang, int phanLoai) => dgvDSDanhGiaNV.DataSource = ChayLaiDuLieu(nam, thang, phanLoai);
    }
}
