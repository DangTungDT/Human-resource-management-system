using BLL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCDanhGiaHieuSuat : UserControl
    {
        private string _idView { get; set; }
        private string _idSelected { get; set; }

        private readonly string _conn;
        private readonly string _idNhanVien; // truong phong, giam doc
        private readonly BLLNhanVien _dbContextNV;
        private readonly BLLPhongBan _dbContextPB;
        private readonly BLLHopDongLaoDong _dbContextHD;
        private readonly BLLDanhGiaNhanVien _dbContextDG;

        private FrmXemDanhGiaChiTietNV _formCTiet;

        public UCDanhGiaHieuSuat(string idNhanVien, string stringConnection)
        {
            InitializeComponent();

            _conn = stringConnection;
            _idNhanVien = idNhanVien.ToUpper();
            _dbContextNV = new BLLNhanVien(stringConnection);
            _dbContextPB = new BLLPhongBan(stringConnection);
            _dbContextHD = new BLLHopDongLaoDong(stringConnection);
            _dbContextDG = new BLLDanhGiaNhanVien(stringConnection);
        }

        private void UCDanhGiaHieuSuat_Load(object sender, EventArgs e)
        {

            dgvDSHieuSuatNV.DataSource = ChayLaiDuLieu(null);

            if (dgvDSHieuSuatNV.Columns["ID"] != null)
            {
                if (dgvDSHieuSuatNV.Columns["ID"].Visible == true)
                {
                    dgvDSHieuSuatNV.Columns["ID"].Visible = false;
                }
            }

            grbDanhGiaHS.Text = $"NGƯỜI ĐÁNH GIÁ: {_dbContextNV.KtraNhanVienQuaID(_idNhanVien).TenNhanVien}";
        }

        private void dgvDSHieuSuatNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e != null && e.RowIndex > -1)
            {
                cmbNam.Items.Clear();
                cmbQuy.Items.Clear();

                _idSelected = dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["ID"].Value?.ToString() ?? string.Empty;
                txtEmail.Text = dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["Email"].Value?.ToString() ?? string.Empty;
                txtChucVu.Text = dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["ChucVu"].Value?.ToString() ?? string.Empty;
                txtDiemDG.Text = dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["DiemTBCaNam"].Value?.ToString() ?? string.Empty;
                txtNhanVien.Text = dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["NhanVien"].Value?.ToString() ?? string.Empty;
                txtGioiTinh.Text = dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["GioiTinh"].Value?.ToString() ?? string.Empty;
                txtNgaySinh.Text = DateTime.Parse(dgvDSHieuSuatNV.Rows[e.RowIndex].Cells["NgaySinh"].Value?.ToString()).ToShortDateString();

                var dsNhanVien = _dbContextNV.KtraDsNhanVien().Where(p => _dbContextNV.KtraNhanVienQuaID(_idNhanVien).IdPhongBan == p.IdPhongBan).ToList();

                if (!string.IsNullOrEmpty(_idSelected))
                {
                    var namLamViec = _dbContextHD.KtraDsHopDongLaoDong().FirstOrDefault(p => p.IdNhanVien == _idSelected);

                    var namBD = namLamViec.NgayBatDau.Year;
                    var namHT = DateTime.Now.Year;

                    for (int i = namBD; i <= namHT; i++) cmbNam.Items.Add(i);
                    cmbNam.Text = cmbNam.Items[cmbNam.Items.Count - 1].ToString();
                }
            }
        }

        // Load du lieu theo chuc vu GD, TP
        private object ChayLaiDuLieu(List<NhanVien> dsNhanVienLoc = null)
        {
            var moDau = "";
            var chucVu = "";
            string phongBanCV = "";
            string[] loaiChucVu = { "GD", "TP" };

            var anonymous = new object();
            var dsHieuSuat = _dbContextDG.KtraDsDanhGiaNhanVien();
            var idPhongBan = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).IdPhongBan;
            var tenPhongBan = _dbContextPB.KtraPhongBan(idPhongBan).ToLowerInvariant();
            var dsTruongPhong = _dbContextNV.KtraDsNhanVien().Where(p => p.Id.Contains("TP")).ToList();
            var dsNhanVienPB = _dbContextNV.KtraDsNhanVien().Where(p => p.IdPhongBan == idPhongBan).ToList();

            foreach (var loai in loaiChucVu)
            {
                if (_idNhanVien.StartsWith(loai))
                {
                    var stringNVFilter = _idNhanVien.Substring(loai.Length);
                    phongBanCV = new string(stringNVFilter.TakeWhile(char.IsLetter).ToArray());
                    var dsNhanVien = dsNhanVienLoc == null ? loai == "GD" ? dsTruongPhong : dsNhanVienPB : dsNhanVienLoc;

                    if (loai == "GD")
                    {
                        anonymous = dsNhanVien.Where(p => dsHieuSuat.Where(h => h.IDNguoiDanhGia == _idNhanVien).Select(s => s.IDNhanVien).Contains(p.Id))
                           .GroupBy(p => p.Id).Select(p => new
                           {
                               ID = p.Key,
                               NhanVien = _dbContextNV.KtraDsNhanVien().Where(n => n.Id == p.Key).FirstOrDefault().TenNhanVien,
                               DiemTBCaNam = Math.Round(dsHieuSuat.Where(w => w.IDNhanVien == p.Key).Average(s => s.DiemSo), 2),
                               GioiTinh = dsTruongPhong.Where(w => w.Id == p.Key).Select(s => s.GioiTinh).FirstOrDefault(),
                               NgaySinh = dsTruongPhong.Where(w => w.Id == p.Key).Select(s => s.NgaySinh).FirstOrDefault(),
                               Email = dsTruongPhong.Where(w => w.Id == p.Key).Select(s => s.Email).FirstOrDefault(),
                               ChucVu = dsTruongPhong.Where(w => w.Id == p.Key).Select(s => s.ChucVu.TenChucVu).FirstOrDefault(),

                           }).ToList();

                        chucVu = "trưởng phòng các ban";
                        moDau = "Danh sách";
                    }
                    else
                    {
                        anonymous = dsNhanVien.Where(p => !p.Id.StartsWith(loai) && dsHieuSuat.Where(h => h.IDNguoiDanhGia == _idNhanVien).Select(s => s.IDNhanVien).Contains(p.Id))
                           .GroupBy(p => p.Id).Select(p => new
                           {
                               ID = p.Key,
                               NhanVien = _dbContextNV.KtraDsNhanVien().Where(n => n.Id == p.Key).FirstOrDefault().TenNhanVien,
                               DiemTBCaNam = Math.Round(dsHieuSuat.Where(w => w.IDNhanVien == p.Key && w.NgayTao.Year == DateTime.Now.Year).Average(s => s.DiemSo), 2),
                               GioiTinh = dsNhanVienPB.Where(w => w.Id == p.Key).Select(s => s.GioiTinh).FirstOrDefault(),
                               NgaySinh = dsNhanVienPB.Where(w => w.Id == p.Key).Select(s => s.NgaySinh).FirstOrDefault(),
                               Email = dsNhanVienPB.Where(w => w.Id == p.Key).Select(s => s.Email).FirstOrDefault(),
                               ChucVu = dsNhanVienPB.Where(w => w.Id == p.Key).Select(s => s.ChucVu.TenChucVu).FirstOrDefault(),

                           }).ToList();

                        chucVu = "nhân viên";
                        moDau = "Danh sách trưởng phòng";
                    }

                    lblDsNV.Text = $"{moDau} {tenPhongBan} đánh giá {chucVu} trong tháng {DateTime.Now.Month}";
                    break;
                }
            }

            return anonymous;
        }

        private void btnLoadDuLieu_Click(object sender, EventArgs e) => dgvDSHieuSuatNV.DataSource = ChayLaiDuLieu(null);

        private void cmbNam_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmbQuy_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        // Loc du lieu qua email
        private void txtTimEmailNV_TextChanged(object sender, EventArgs e)
        {
            var timEmail = txtTimEmailNV.Text.Trim();
            LocDsTheoTenEmail(timEmail, "email");
        }

        // Loc du lieu qua ten
        private void txtTimTenNV_TextChanged(object sender, EventArgs e)
        {
            var timTen = txtTimTenNV.Text.Trim();
            LocDsTheoTenEmail(timTen, "name");
        }

        // Ham dung loc chung cho ten, email
        private void LocDsTheoTenEmail(string loc, string phanLoai)
        {
            var dsNV = new List<NhanVien>();
            string locChuoi = LocKyTuKhongDau(loc);

            if (string.IsNullOrWhiteSpace(locChuoi))
            {
                dgvDSHieuSuatNV.DataSource = ChayLaiDuLieu(null);
            }
            else
            {
                if (_idNhanVien.Contains("GD"))
                {
                    var dsTruongPhong = _dbContextNV.KtraDsNhanVien().Where(p => p.Id.Contains("TP")).ToList();

                    if (phanLoai == "name")
                    {
                        dsNV = _dbContextNV.KtraDsNhanVien().Where(p => LocKyTuKhongDau(p.TenNhanVien).Contains(locChuoi) && dsTruongPhong.Select(s => s.Id).Contains(p.Id)).ToList();
                    }
                    else dsNV = _dbContextNV.KtraDsNhanVien().Where(p => LocKyTuKhongDau(p.Email).Contains(locChuoi) && dsTruongPhong.Select(s => s.Id).Contains(p.Id)).ToList();
                }
                else
                {
                    var idPhongBan = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).IdPhongBan;
                    var dsNhanVienPB = _dbContextNV.KtraDsNhanVien().Where(p => p.IdPhongBan == idPhongBan).ToList();

                    if (phanLoai == "name")
                    {
                        dsNV = dsNhanVienPB.Where(p => LocKyTuKhongDau(p.TenNhanVien).Contains(locChuoi)).ToList();
                    }
                    else dsNV = dsNhanVienPB.Where(p => LocKyTuKhongDau(p.Email).Contains(locChuoi)).ToList();
                }

                //dgvDSHieuSuatNV.DataSource = dsNV.Count > 0 ? ChayLaiDuLieu(dsNV) : dsNV.Count == 0 ? ChayLaiDuLieu(new List<NhanVien>()) : ChayLaiDuLieu(null);

                if (dsNV.Count > 0)
                {
                    dgvDSHieuSuatNV.DataSource = ChayLaiDuLieu(dsNV);
                }
                else
                {
                    if (dsNV.Count == 0)
                    {
                        dgvDSHieuSuatNV.DataSource = ChayLaiDuLieu(new List<NhanVien>());
                    }
                    else dgvDSHieuSuatNV.DataSource = ChayLaiDuLieu(null);
                }

                if (dgvDSHieuSuatNV.Columns["ID"].Visible == true)
                {
                    dgvDSHieuSuatNV.Columns["ID"].Visible = false;
                }
            }
        }


        // Thay doi text co cac ki tu co dau thanh khong dau
        public string LocKyTuKhongDau(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            var normalized = value.ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }

            return builder.ToString().Replace(" ", "").Replace("đ", "d");
        }

        private void cmbQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsThang = new List<int>();

            var nam = Convert.ToInt32(cmbNam.Text);
            var quy = Convert.ToInt32(cmbQuy.Text);

            if (_idSelected == string.Empty) return;
            var nhanVien = _dbContextDG.KtraDsDanhGiaNhanVien().Where(p => p.IDNhanVien == _idSelected).ToList();

            if (nam > 0 && quy > 0)
            {

                switch (quy)
                {
                    case 1:
                        dsThang = new List<int> { 1, 2, 3 };
                        break;

                    case 2:
                        dsThang = new List<int> { 4, 5, 6 };
                        break;

                    case 3:
                        dsThang = new List<int> { 7, 8, 9 };
                        break;

                    default:
                        dsThang = new List<int> { 10, 11, 12 };
                        break;
                }

                txtLocDTB.Text = TinhDiemTB(nhanVien, dsThang, nam).ToString();

                if (int.TryParse(txtLocDTB.Text, out int DTB) && DTB >= 7)
                {
                    txtMucTieu.Text = "Phát huy";
                }
                else txtMucTieu.Text = "Cải thiện";
            }
            else txtLocDTB.Text = "0";
        }

        public double TinhDiemTB(List<DTODanhGiaNhanVien> nhanVien, List<int> dsThangTheoQuy, int year)
        {
            if (nhanVien.Count == 0) return 0;

            var thangTrongQuy = new List<DTODanhGiaNhanVien>();
            foreach (var thang in dsThangTheoQuy)
            {
                var nhanVienCoDiem = nhanVien.FirstOrDefault(p => p.NgayTao.Month == thang && p.NgayTao.Year == year);
                if (nhanVienCoDiem != null)
                {
                    thangTrongQuy.Add(nhanVienCoDiem);
                }
            }

            return thangTrongQuy.Count > 0 ? Math.Round(thangTrongQuy.Average(p => p.DiemSo), 2) : 0;
        }

        private void cmbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbQuy.Items.Clear();
            var thoiGianHienTai = DateTime.Now;
            var namChon = Convert.ToInt32(cmbNam.Text);
            var nhanVien = _dbContextDG.KtraDsDanhGiaNhanVien().Where(p => p.IDNhanVien == _idSelected).ToList();

            int soQuyHienTai;
            if (namChon != DateTime.Now.Year)
            {
                soQuyHienTai = 4;
            }
            else
            {
                if (thoiGianHienTai.Date != new DateTime(thoiGianHienTai.Year, 12, 31))
                {
                    soQuyHienTai = thoiGianHienTai.Month / 3;
                }
                else soQuyHienTai = 4;
            }

            for (int i = 1; i <= soQuyHienTai; i++) cmbQuy.Items.Add(i);
            cmbQuy.Text = cmbQuy.Items[cmbNam.Items.Count].ToString();
            txtLocSoLanDG.Text = nhanVien.Where(p => p.NgayTao.Year == namChon).ToList().Count.ToString();
        }

        private void dgvDSHieuSuatNV_DoubleClick(object sender, EventArgs e)
        {
            var nhanVien = _dbContextNV.KtraDsNhanVien().FirstOrDefault(p => p.Id == _idSelected);
            if (nhanVien != null)
            {
                if (MessageBox.Show($"Bạn có muốn xem đánh giá chi tiết của nhân viên {nhanVien.TenNhanVien} không ?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_formCTiet != null && !_formCTiet.IsDisposed && _idView == _idSelected)
                    {
                        MessageBox.Show($"Hiện bạn đang xem trang cá nhân của {nhanVien.TenNhanVien}.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (_formCTiet != null && !_formCTiet.IsDisposed)
                        {
                            _formCTiet.Close();
                        }

                        _formCTiet = new FrmXemDanhGiaChiTietNV(_conn, nhanVien.Id, _idNhanVien);
                        _formCTiet.Show();

                        _idView = nhanVien.Id;

                    }
                }
            }
            else MessageBox.Show($"Không tìm thấy nhân viên trong dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
