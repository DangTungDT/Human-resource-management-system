using BLL;
using DAL;
using DAL.DataContext;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;

namespace GUI
{
    public partial class ucChiTietLuong : UserControl
    {
        private string _idSelected { get; set; }
        private readonly string _idNhanVien, _conn;

        private readonly BLLPhuCap _dbContextPC;
        private readonly BLLKyLuong _dbContextKL;
        private readonly BLLKhauTru _dbContextKT;
        private readonly BLLNhanVien _dbContextNV;
        private readonly BLLPhongBan _dbContextPB;
        private readonly BLLThuongPhat _dbContextTP;
        private readonly BLLChiTietLuong _dbContextCTL;
        private readonly BLLHopDongLaoDong _dbContextHD;
        private readonly BLLNhanVien_PhuCap _dbContextNV_PC;
        private readonly BLLNhanVien_KhauTru _dbContextNV_KT;
        private readonly BLLNhanVien_ThuongPhat _dbContextNV_TP;

        private List<string> _layDsChiTietLuong = new List<string>();
        private List<string> _listReadonly = new List<string> { "txtThucLanh", "txtPhongBanNV", "txtTenNhanVien", "txtNgayNhanLuong", "txtTrangThai" };

        public ucChiTietLuong(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;

            _dbContextPC = new BLLPhuCap(conn);
            _dbContextKT = new BLLKhauTru(conn);
            _dbContextKL = new BLLKyLuong(conn);
            _dbContextPB = new BLLPhongBan(conn);
            _dbContextNV = new BLLNhanVien(conn);
            _dbContextTP = new BLLThuongPhat(conn);
            _dbContextCTL = new BLLChiTietLuong(conn);
            _dbContextHD = new BLLHopDongLaoDong(conn);
            _dbContextNV_PC = new BLLNhanVien_PhuCap(conn);
            _dbContextNV_KT = new BLLNhanVien_KhauTru(conn);
            _dbContextNV_TP = new BLLNhanVien_ThuongPhat(conn);

        }

        private void ucChiTietLuong_Load(object sender, EventArgs e)
        {
            if (ChayLaiDuLieu(null) is List<NhanVienLuongCT>)
            {
                dgvSalaryDetails.DataSource = (List<NhanVienLuongCT>)ChayLaiDuLieu();
                dgvSalaryDetails.Columns["Checked"].ReadOnly = false;
            }
            else dgvSalaryDetails.DataSource = ChayLaiDuLieu(null);


            if (dgvSalaryDetails.Columns["ID"] != null)
            {
                if (dgvSalaryDetails.Columns["ID"].Visible)
                {
                    dgvSalaryDetails.Columns["ID"].Visible = false;
                }

                dgvSalaryDetails.RowsDefaultCellStyle.Font = new Font("Times", 10);
                dgvSalaryDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Times", 11, FontStyle.Bold);

                dgvSalaryDetails.Columns["ChucVu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvSalaryDetails.Columns["Checked"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvSalaryDetails.Columns["NhanVien"].HeaderText = "Nhân viên";
                dgvSalaryDetails.Columns["GioiTinh"].HeaderText = "Giới tính";
                dgvSalaryDetails.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvSalaryDetails.Columns["ChucVu"].HeaderText = "Chức vụ";
                dgvSalaryDetails.Columns["Checked"].HeaderText = "Xác nhận";
            }

            var loc = new List<string> { "Admin", "Giám đốc" };
            var listPhongBan = _dbContextPB.KtraDsPhongBan().Where(p => !loc.Contains(p.TenPhongBan)).ToList();

            listPhongBan.Insert(0, new PhongBan { id = 0, TenPhongBan = "" });

            cmbPhongBan.DataSource = listPhongBan;
            cmbPhongBan.DisplayMember = "TenPhongBan";
            cmbPhongBan.ValueMember = "id";

            cmbPhongBan.SelectedIndex = 0;

            btnCapNhat.Enabled = false;
            btnXoaLuong.Enabled = false;
        }

        private void dgvSalaryDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                _idSelected = dgvSalaryDetails.Rows[e.RowIndex].Cells["ID"]?.Value.ToString();

                if (e.RowIndex >= 0 && dgvSalaryDetails.Columns[e.ColumnIndex].Name == "Checked")
                {
                    DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dgvSalaryDetails.Rows[e.RowIndex].Cells["Checked"];
                    bool isChecked = (bool)(checkBoxCell.EditedFormattedValue ?? false);

                    if (!string.IsNullOrEmpty(_idSelected))
                    {
                        if (!isChecked)
                        {
                            if (!_layDsChiTietLuong.Contains(_idSelected))
                            {
                                _layDsChiTietLuong.Add(_idSelected);
                            }
                        }
                        else _layDsChiTietLuong.Remove(_idSelected);
                    }
                }

                HienThiDGVDuocChon(_idSelected, e);
            }

        }

        public void HienThiDGVDuocChon(string idSelected, DataGridViewCellEventArgs e)
        {
            var changeManagerVal = _dbContextCTL.KtraDsChiTietLuong().Where(p => p.capNhatLuong).ToList();

            var nv_pc = _dbContextNV_PC.KtraDsNV_PC();
            var nv_kt = _dbContextNV_KT.KtraDsNhanVien_KhauTru();
            var thangSau = DateTime.Now.Month != 12 ? DateTime.Now.Month + 1 : 1;
            var xacNhanKyLuong = _dbContextKL.KtraDsKyLuong().FirstOrDefault(p => p.ngayChiTra.Value.Month == thangSau && p.ngayChiTra.Value.Year == DateTime.Now.Year);

            if (xacNhanKyLuong != null)
            {
                txtTenNhanVien.Text = dgvSalaryDetails.Rows[e.RowIndex].Cells["NhanVien"]?.Value.ToString();

                if (!string.IsNullOrEmpty(idSelected))
                {
                    double tongTienThuong = 0;
                    double tongTienPhat = 0;
                    double tongKhauTru = 0;
                    double tongPhuCap = 0;

                    _dbContextNV_TP.KtraDsNhanVien_ThuongPhat().Where(p => p.idNhanVien == idSelected).Select(p => p.idThuongPhat).ToList().ForEach(id =>
                    {
                        tongTienThuong += (double)_dbContextTP.CheckListThuongPhat().Where(p => p.loai.Equals("Thưởng", StringComparison.OrdinalIgnoreCase) && p.id == id).Sum(s => s.tienThuongPhat);
                        tongTienPhat += (double)_dbContextTP.CheckListThuongPhat().Where(p => p.loai.Equals("Phạt", StringComparison.OrdinalIgnoreCase) && p.id == id).Sum(s => s.tienThuongPhat);
                    });

                    var timeCurrent = DateTime.Now;
                    var luongTruocKT = _dbContextHD.KtraDsHopDongLaoDong().FirstOrDefault(p => p.idNhanVien == idSelected);


                    if (nv_kt != null)
                    {
                        nv_kt.Where(p => p.idNhanVien == idSelected).ToList().ForEach(id =>
                        {
                            _dbContextKT.KtraDsKhauTru().Where(p => p.id == id.idKhauTru).ToList().ForEach(kt =>
                            {
                                tongKhauTru += (double)kt.soTien;
                            });
                        });
                    }

                    if (nv_pc != null)
                    {
                        nv_pc.Where(p => p.idNhanVien == idSelected).ToList().ForEach(id =>
                        {
                            _dbContextPC.KtraDsPhuCap().Where(p => p.id == id.idPhuCap).ToList().ForEach(pc =>
                            {
                                tongPhuCap += (double)pc.soTien;
                            });
                        });
                    }

                    double luongSauKT = 0;

                    if (luongTruocKT != null)
                    {
                        luongSauKT = (double)luongTruocKT.Luong - tongKhauTru;
                    }
                    else luongSauKT = 0;

                    var kyLuong = _dbContextCTL.KtraDsChiTietLuong().FirstOrDefault(ct => ct.idNhanVien == idSelected && ct.ngayNhanLuong.Month == thangSau && ct.ngayNhanLuong.Year == DateTime.Now.Year);

                    var trangThaiKyLuong = kyLuong != null ? _dbContextKL.KtraKyLuongQuaID(kyLuong.idKyLuong) : null;

                    string trangThai, phuCap, tienPhat, khauTru, tienThuong, luongSau, luongTruoc, thucLanh, ghiChu = "";

                    var nhanVienCTL = changeManagerVal.FirstOrDefault(p => p.idNhanVien == idSelected);
                    if (nhanVienCTL == null)
                    {
                        trangThai = trangThaiKyLuong != null ? kyLuong.trangThai : "Chưa giải quyết";
                        phuCap = string.Format(new CultureInfo("vi-VN"), "{0:N0}", tongPhuCap);
                        tienPhat = string.Format(new CultureInfo("vi-VN"), "{0:N0}", tongTienPhat);
                        khauTru = string.Format(new CultureInfo("vi-VN"), "{0:N0}", tongKhauTru);
                        tienThuong = string.Format(new CultureInfo("vi-VN"), "{0:N0}", tongTienThuong);
                        luongSau = string.Format(new CultureInfo("vi-VN"), "{0:N0}", luongSauKT);
                        luongTruoc = string.Format(new CultureInfo("vi-VN"), "{0:N0}", luongTruocKT != null ? luongTruocKT.Luong : 0);
                        thucLanh = string.Format(new CultureInfo("vi-VN"), "{0:C0}", luongSauKT + tongPhuCap + tongTienThuong - tongTienPhat - tongKhauTru);
                    }
                    else
                    {
                        ghiChu = nhanVienCTL.ghiChu;
                        trangThai = trangThaiKyLuong != null ? kyLuong.trangThai : "Chưa giải quyết";
                        phuCap = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.tongPhuCap);
                        tienPhat = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.tongTienPhat);
                        khauTru = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.tongKhauTru);
                        tienThuong = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.tongKhenThuong);
                        luongSau = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.luongSauKhauTru);
                        luongTruoc = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.luongTruocKhauTru);
                        thucLanh = string.Format(new CultureInfo("vi-VN"), "{0:C0}", nhanVienCTL.luongSauKhauTru + nhanVienCTL.tongPhuCap + nhanVienCTL.tongKhenThuong - nhanVienCTL.tongTienPhat - nhanVienCTL.tongKhauTru);
                    }

                    rtGhiChu.Text = ghiChu;
                    txtPhuCap.Text = phuCap;
                    txtTienPhat.Text = tienPhat;
                    txtThucLanh.Text = thucLanh;
                    txtTongKhauTru.Text = khauTru;
                    txtTrangThai.Text = trangThai;
                    txtTienThuong.Text = tienThuong;
                    txtLuongSauKhauTru.Text = luongSau;
                    txtLuongTruocKhauTru.Text = luongTruoc;

                    var NgayKyLuong = _dbContextKL.KtraDsKyLuong().FirstOrDefault(p => p.ngayBatDau != null && p.ngayBatDau.Value.Year == timeCurrent.Year && p.ngayBatDau.Value.Month == timeCurrent.Month);
                    if (NgayKyLuong != null)
                    {
                        grbLuongChiTiet.Text = "NGÀY NHẬN LƯƠNG: " + DateTime.Parse(NgayKyLuong.ngayChiTra.ToString()).ToShortDateString();
                    }

                    var idPhongBan = _dbContextNV.KtraNhanVienQuaID(idSelected).idPhongBan;
                    var tenPhongBan = _dbContextPB.KtraPhongBan(idPhongBan);
                    var dsNhanVienPB = _dbContextNV.KtraDsNhanVien().Where(p => p.idPhongBan == idPhongBan).ToList();
                    txtPhongBanNV.Text = tenPhongBan;
                }

            }
            else MessageBox.Show($"Chưa có dữ liệu kỳ lương tháng {DateTime.Now.Month}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            btnCapNhat.Enabled = _idNhanVien.Contains("NVNS") ? false : true;
            btnXoaLuong.Enabled = true;

            foreach (var control in grbLuongChiTiet.Controls)
            {
                if (control is Guna2TextBox text && !string.IsNullOrEmpty(text.Text))
                {
                    if (_listReadonly.Contains(text.Name))
                    {
                        continue;
                    }

                    text.ReadOnly = false;
                }
            }
        }

        public void Empty()
        {
            foreach (var control in grbLuongChiTiet.Controls)
            {
                if (control is Guna2TextBox text && !string.IsNullOrEmpty(text.Text))
                {
                    text.Text = string.Empty;
                    text.ReadOnly = true;
                }
            }
            rtGhiChu.Text = string.Empty;
        }

        // Load du lieu theo chuc vu GD, TP
        private object ChayLaiDuLieu(List<NhanVien> dsNhanVienLoc = null)
        {
            var anonymous = new object();
            var dsNhanVien = _dbContextNV.KtraDsNhanVien();

            var stringNVFilter = _idNhanVien.Substring(2);
            var isDsNhanVien = dsNhanVienLoc == null ? dsNhanVien : dsNhanVienLoc;
            var thangSau = DateTime.Now.Month != 12 ? DateTime.Now.Month + 1 : 1;

            string[] loaiChucVu = { "GD", "TPNS", "NVNS" };

            anonymous = isDsNhanVien
                .Where(p => !p.id.StartsWith("GD"))
                .Select(p => new NhanVienLuongCT
                {
                    ID = p.id,
                    NhanVien = p.TenNhanVien,
                    GioiTinh = p.GioiTinh,
                    NgaySinh = p.NgaySinh,
                    Email = p.Email,
                    ChucVu = p.ChucVu.TenChucVu,
                    Checked = _dbContextCTL.KtraDsChiTietLuong().Any(ct =>
                        ct.idNhanVien == p.id &&
                        ct.ngayNhanLuong.Month == thangSau &&
                        ct.ngayNhanLuong.Year == DateTime.Now.Year)

                }).ToList();

            foreach (var chucVu in loaiChucVu)
            {
                if (_idNhanVien.StartsWith(chucVu))
                {
                    if (chucVu == "TPNS")
                    {
                        btnChot.Visible = false;
                    }
                    else if (chucVu == "NVNS")
                    {
                        btnCapNhat.Enabled = false;
                        btnChot.Visible = false;
                    }

                    break;
                }
            }

            var timeCurrent = DateTime.Now;
            var NgayKyLuong = _dbContextKL.KtraDsKyLuong().FirstOrDefault(p => p.ngayBatDau != null && p.ngayBatDau.Value.Year == timeCurrent.Year && p.ngayBatDau.Value.Month == timeCurrent.Month);
            if (NgayKyLuong != null)
            {
                grbLuongChiTiet.Text = "NGÀY NHẬN LƯƠNG: " + DateTime.Parse(NgayKyLuong.ngayChiTra.ToString()).ToShortDateString();
            }

            return anonymous;
        }


        private void txtTenNV_TextChanged(object sender, EventArgs e)
        {
            var locTenNVKhongDau = LocKyTuKhongDau(txtTenNV.Text.Trim().ToLowerInvariant());

            var dsNhanVien = _dbContextNV.KtraDsNhanVien().Where(p =>
            {
                var tenNVKhongDau = LocKyTuKhongDau(p.TenNhanVien.Trim().ToLowerInvariant());
                return tenNVKhongDau.Contains(locTenNVKhongDau);
            }).ToList();

            dgvSalaryDetails.DataSource = ChayLaiDuLieu(dsNhanVien);
        }

        // Thay doi text co cac ki tu co dau thanh khong dau
        private string LocKyTuKhongDau(string value)
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

        private void cmbTrangThai_SelectionChangeCommitted(object sender, EventArgs e)
        {
            List<NhanVien> dsNhanVien = null;
            var thangSau = DateTime.Now.Month != 12 ? DateTime.Now.Month + 1 : 1;

            var trangThaiSelected = cmbTrangThai.SelectedItem.ToString();

            if (trangThaiSelected.Equals("Đang giải quyết", StringComparison.OrdinalIgnoreCase))
            {
                dsNhanVien = _dbContextNV.KtraDsNhanVien().Where(p =>
                {
                    return _dbContextCTL.KtraDsChiTietLuong().Any(ct => ct.idNhanVien == p.id && ct.ngayNhanLuong.Month == thangSau && ct.ngayNhanLuong.Year == DateTime.Now.Year);
                }).ToList();
            }
            else
            {
                dsNhanVien = _dbContextNV.KtraDsNhanVien().Where(p =>
                {
                    return !_dbContextCTL.KtraDsChiTietLuong().Any(ct => ct.idNhanVien == p.id && ct.ngayNhanLuong.Month == thangSau && ct.ngayNhanLuong.Year == DateTime.Now.Year);
                }).ToList();
            }

            dgvSalaryDetails.DataSource = ChayLaiDuLieu(dsNhanVien);
        }

        private void cmbPhongBan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            List<NhanVien> dsNhanVien = null;
            var idPhongBan = Convert.ToInt32(cmbPhongBan.SelectedValue);
            if (idPhongBan > 0)
            {
                dsNhanVien = _dbContextNV.KtraDsNhanVien().Where(p =>
                {
                    return _dbContextPB.KtraDsPhongBan().Any(k => k.id == p.idPhongBan && p.idPhongBan == idPhongBan);

                }).ToList();
            }

            txtTenNV.Text = string.Empty;
            ckChonNhanhPB.Checked = false;
            cmbTrangThai.SelectedIndex = -1;
            dgvSalaryDetails.DataSource = dsNhanVien == null ? ChayLaiDuLieu() : ChayLaiDuLieu(dsNhanVien);
        }


        private void btnLoadDuLieu_Click(object sender, EventArgs e)
        {
            Empty();
            dgvSalaryDetails.DataSource = ChayLaiDuLieu();
            btnCapNhat.Enabled = false;
            btnXoaLuong.Enabled = false;
        }


        private void btnThemLuong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_layDsChiTietLuong.Any())
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một nhân viên!");
                    return;
                }

                if (KiemTraDuLieuDauVao())
                {
                    var nv_pc = _dbContextNV_PC.KtraDsNV_PC();
                    var nv_kt = _dbContextNV_KT.KtraDsNhanVien_KhauTru();
                    var nv_tp = _dbContextNV_TP.KtraDsNhanVien_ThuongPhat();

                    var timeCurrent = DateTime.Now;
                    var idKyLuong = _dbContextKL.KtraDsKyLuong().FirstOrDefault(p => p.ngayBatDau != null && p.ngayBatDau.Value.Year == timeCurrent.Year && p.ngayBatDau.Value.Month == timeCurrent.Month).id;

                    var dsDaThemThanhCong = new List<string>();

                    foreach (var idNhanVien in _layDsChiTietLuong)
                    {

                        decimal tongPhuCap = 0;
                        decimal tongKhauTru = 0;
                        decimal tongTienPhat = 0;
                        decimal tongTienThuong = 0;

                        if (nv_tp != null)
                        {
                            nv_tp.Where(p => p.idNhanVien == idNhanVien).Select(p => p.idThuongPhat).ToList().ForEach(id =>
                            {
                                tongTienThuong += _dbContextTP.CheckListThuongPhat().Where(p => p.loai.Equals("Thưởng", StringComparison.OrdinalIgnoreCase) && p.id == id).Sum(s => s.tienThuongPhat);
                                tongTienPhat += _dbContextTP.CheckListThuongPhat().Where(p => p.loai.Equals("Phạt", StringComparison.OrdinalIgnoreCase) && p.id == id).Sum(s => s.tienThuongPhat);
                            });
                        }

                        decimal luongTruocKT = _dbContextHD.KtraDsHopDongLaoDong().FirstOrDefault(p => p.idNhanVien == idNhanVien).Luong;

                        if (nv_kt != null)
                        {

                            nv_kt.Where(p => p.idNhanVien == idNhanVien).ToList().ForEach(id =>
                            {
                                _dbContextKT.KtraDsKhauTru().Where(p => p.id == id.idKhauTru).ToList().ForEach(kt =>
                                {
                                    tongTienPhat += kt.soTien;
                                });
                            });
                        }

                        if (nv_pc != null)
                        {

                            nv_pc.Where(p => p.idNhanVien == idNhanVien).ToList().ForEach(id =>
                            {
                                _dbContextPC.KtraDsPhuCap().Where(p => p.id == id.idPhuCap).ToList().ForEach(pc =>
                                {
                                    tongPhuCap += pc.soTien;
                                });
                            });
                        }

                        var luongSauKT = luongTruocKT - tongKhauTru;

                        if (DateTime.TryParse(grbLuongChiTiet.Text.Split(':')[1].Trim(), out DateTime ngayNhanLuong))
                        {
                            if (_dbContextCTL.KtraDuLieuChiTietLuongNV(idNhanVien) == null)
                            {
                                var themThanhCong = _dbContextCTL.KtraThemChiTietLuong(new DTOChiTietLuong(0, ngayNhanLuong, luongTruocKT, luongSauKT, tongKhauTru, tongPhuCap, tongTienThuong, tongTienPhat, "Đang giải quyết", rtGhiChu?.Text ?? null, idNhanVien, idKyLuong, true));
                                if (themThanhCong)
                                {
                                    dsDaThemThanhCong.Add(idNhanVien);
                                }
                            }
                            else MessageBox.Show($"Nhân viên đã được thêm vào bảng lương tháng {DateTime.Now.Month} !");
                        }
                        else MessageBox.Show("Ngày nhận lương không hợp lệ !");
                    }

                    dgvSalaryDetails.DataSource = ChayLaiDuLieu();
                    MessageBox.Show("Thêm dữ liệu thành công.");
                    Empty();

                    foreach (var idNhanVien in dsDaThemThanhCong)
                    {
                        _layDsChiTietLuong.Remove(idNhanVien);
                    }

                    btnCapNhat.Enabled = false;
                    btnXoaLuong.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                var chiTietLuong = _dbContextCTL.KtraChiTietLuongQuaIDNhanVien(_idSelected);
                if (chiTietLuong != null)
                {
                    var ktraCTL = _dbContextCTL.KtraDsChiTietLuong().FirstOrDefault(p => p.idNhanVien == chiTietLuong.idNhanVien && p.ngayNhanLuong.Year == DateTime.Now.Year && p.ngayNhanLuong.Month == DateTime.Now.Month + 1);

                    if (ktraCTL == null)
                    {
                        MessageBox.Show($"Nhân viên {_dbContextNV.KtraNhanVienQuaID(_idSelected).TenNhanVien} chưa được chọn. !");
                        return;
                    }

                    var timeCurrent = DateTime.Now;
                    var chiTietLuongSelected = _dbContextKL.KtraDsKyLuong().FirstOrDefault(p => p.ngayBatDau != null && p.ngayBatDau.Value.Year == timeCurrent.Year && p.ngayBatDau.Value.Month == timeCurrent.Month);

                    bool isGiamDoc = false;
                    int id = chiTietLuong.id;
                    int idKyLuong = chiTietLuong.idKyLuong;

                    if (_idNhanVien.Contains("GD") || _idNhanVien.Contains("TPNS"))
                    {
                        isGiamDoc = true;
                    }

                    decimal luongTruocKT = Convert.ToDecimal(txtLuongTruocKhauTru.Text);
                    decimal luongSauKT = Convert.ToDecimal(txtLuongSauKhauTru.Text);
                    decimal tongKhauTru = Convert.ToDecimal(txtTongKhauTru.Text);
                    decimal tongPhuCap = Convert.ToDecimal(txtPhuCap.Text);
                    decimal tongTienThuong = Convert.ToDecimal(txtTienThuong.Text);
                    decimal tongTienPhat = Convert.ToDecimal(txtTienPhat.Text);

                    var chucVu = _idNhanVien.Contains("GD") ? "giám đốc" : "trưởng phòng nhân sự";
                    var ghiChu = $"Có sự thay đổi chi tiết lương từ {chucVu}";

                    if (DateTime.TryParse(grbLuongChiTiet.Text.Split(':')[1].Trim(), out DateTime ngayNhanLuong))
                    {
                        if (_dbContextCTL.KtraDuLieuChiTietLuongNV(_idSelected) != null)
                        {
                            if (MessageBox.Show($"Bạn có chắc chắn về sự thay đổi bảng lương của {_dbContextNV.KtraNhanVienQuaID(_idSelected).TenNhanVien} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }

                            if (_dbContextCTL.KtraCapNhatChiTietLuong(new DTOChiTietLuong(id, ngayNhanLuong, luongTruocKT, luongSauKT, tongKhauTru, tongPhuCap, tongTienThuong, tongTienPhat, "Đang giải quyết", ghiChu, _idSelected, idKyLuong, isGiamDoc)))
                            {
                                MessageBox.Show("Cập nhật chi tiết lương thành công. ");

                                dgvSalaryDetails.DataSource = ChayLaiDuLieu();

                                var thangSau = DateTime.Now.Month != 12 ? DateTime.Now.Month + 1 : 1;
                                var kyLuong = _dbContextCTL.KtraDsChiTietLuong().FirstOrDefault(ct => ct.idNhanVien == _idSelected && ct.ngayNhanLuong.Month == thangSau && ct.ngayNhanLuong.Year == DateTime.Now.Year);
                                var trangThaiKyLuong = kyLuong != null ? _dbContextKL.KtraKyLuongQuaID(kyLuong.idKyLuong) : null;
                                var nhanVienCTL = _dbContextCTL.KtraDsChiTietLuong().Where(p => p.capNhatLuong).FirstOrDefault(p => p.idNhanVien == _idSelected);

                                if (nhanVienCTL != null)
                                {
                                    rtGhiChu.Text = nhanVienCTL.ghiChu;
                                    txtPhuCap.Text = trangThaiKyLuong != null ? kyLuong.trangThai : "Chưa giải quyết";
                                    txtPhuCap.Text = string.Format(new CultureInfo("vi-VN"), "{0:C0}", nhanVienCTL.tongPhuCap);
                                    txtTienPhat.Text = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.tongTienPhat);
                                    txtTongKhauTru.Text = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.tongKhauTru);
                                    txtTienThuong.Text = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.tongKhenThuong);
                                    txtLuongSauKhauTru.Text = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.luongSauKhauTru);
                                    txtLuongTruocKhauTru.Text = string.Format(new CultureInfo("vi-VN"), "{0:N0}", nhanVienCTL.luongTruocKhauTru);
                                    txtThucLanh.Text = string.Format(new CultureInfo("vi-VN"), "{0:C0}", nhanVienCTL.luongSauKhauTru + nhanVienCTL.tongPhuCap + nhanVienCTL.tongKhenThuong - nhanVienCTL.tongTienPhat - nhanVienCTL.tongKhauTru);

                                }
                            }

                        }
                    }
                    else MessageBox.Show("Cập nhật thất bại !");
                }
                else MessageBox.Show("Hãy thêm bảng lương trước khi cập nhật !");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoaLuong_Click(object sender, EventArgs e)
        {
            try
            {
                var chiTietLuong = _dbContextCTL.KtraChiTietLuongQuaIDNhanVien(_idSelected);
                if (chiTietLuong != null)
                {
                    var dsXoa = _dbContextCTL.KtraDsChiTietLuong().FirstOrDefault(p => p.idNhanVien == chiTietLuong.idNhanVien && p.ngayNhanLuong.Year == DateTime.Now.Year && p.ngayNhanLuong.Month == DateTime.Now.Month + 1);

                    if (dsXoa == null)
                    {
                        MessageBox.Show($"Nhân viên {_dbContextNV.KtraNhanVienQuaID(_idSelected).TenNhanVien} chưa được thêm. !");
                        return;
                    }

                    if (MessageBox.Show($"Bạn có chắc chắn muốn hủy bỏ {_dbContextNV.KtraNhanVienQuaID(_idSelected).TenNhanVien} đã chọn không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    var chiTiet = _dbContextCTL.KtraDuLieuChiTietLuongNV(_idSelected);
                    if (chiTiet != null)
                    {
                        if (_dbContextCTL.KtraXoaChiTietLuong(chiTiet))
                        {
                            var row = dgvSalaryDetails.Rows
                                .Cast<DataGridViewRow>()
                                .FirstOrDefault(r => r.Cells["ID"].Value?.ToString() == _idSelected);
                            if (row != null)
                                row.Cells["Checked"].Value = false;
                        }
                    }

                    dgvSalaryDetails.DataSource = ChayLaiDuLieu();

                    MessageBox.Show("Xóa dữ liệu thành công!");
                    Empty();

                }
                else MessageBox.Show($"Nhân viên {_dbContextNV.KtraNhanVienQuaID(_idSelected).TenNhanVien} chưa có dữ liệu bảng lương tháng {DateTime.Now.Month} !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void txtLuongSauKhauTru_TextChanged(object sender, EventArgs e) => DisplayUserControlPanel.LayKiTuSo(sender);

        private void txtLuongSauKhauTru_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        private void txtLuongTruocKhauTru_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        private void txtLuongTruocKhauTru_TextChanged(object sender, EventArgs e) => DisplayUserControlPanel.LayKiTuSo(sender);

        private void cmbLuongTraSau_TextChanged(object sender, EventArgs e) => DisplayUserControlPanel.LayKiTuSo(sender);

        private void txtTongKhauTru_TextChanged(object sender, EventArgs e) => DisplayUserControlPanel.LayKiTuSo(sender);

        private void txtPhuCap_TextChanged(object sender, EventArgs e) => DisplayUserControlPanel.LayKiTuSo(sender);

        private void txtTienPhat_TextChanged(object sender, EventArgs e) => DisplayUserControlPanel.LayKiTuSo(sender);

        private void txtTienThuong_TextChanged(object sender, EventArgs e) => DisplayUserControlPanel.LayKiTuSo(sender);

        private void cmbLuongTraSau_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        private void txtTongKhauTru_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        private void txtPhuCap_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        private void txtTienPhat_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        private void txtTienThuong_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        // Ktra cac field rong
        private bool KiemTraDuLieuDauVao()
        {
            bool ktra = true;
            error.Clear();

            foreach (var control in guna2Panel1.Controls)
            {
                if (control is Guna2ComboBox combobox && string.IsNullOrWhiteSpace(combobox.Text))
                {
                    error.SetError(combobox, $"'{combobox.Name.Substring(3)}' trống !");
                    ktra = false;
                }

                if (control is Guna2TextBox text && string.IsNullOrWhiteSpace(text.Text))
                {
                    error.SetError(text, $"{text.Name.Substring(3)} trống !");
                    ktra = false;
                }
            }

            return ktra;
        }

        private void ckChonNhanhPB_CheckedChanged(object sender, EventArgs e)
        {

            List<NhanVien> dsNhanVien = null;
            var idPhongBan = Convert.ToInt32(cmbPhongBan.SelectedValue);
            if (idPhongBan > 0)
            {
                dsNhanVien = _dbContextNV.KtraDsNhanVien().Where(p =>
                {
                    return _dbContextPB.KtraDsPhongBan().Any(k => k.id == p.idPhongBan && p.idPhongBan == idPhongBan);

                }).ToList();
            }

            txtTenNV.Text = string.Empty;

            dgvSalaryDetails.DataSource = dsNhanVien == null ? ChayLaiDuLieu() : ChayLaiDuLieu(dsNhanVien);

            _layDsChiTietLuong.Clear();

            foreach (DataGridViewRow row in dgvSalaryDetails.Rows)
            {
                if (row.Cells["Checked"] is DataGridViewCheckBoxCell checkBoxCell)
                {
                    var isCheckedValue = Convert.ToBoolean(checkBoxCell.Value);
                    if (!isCheckedValue)
                    {
                        checkBoxCell.Value = ckChonNhanhPB.Checked;

                        var idValue = row.Cells["ID"]?.Value?.ToString();
                        if (!string.IsNullOrEmpty(idValue))
                        {
                            if (ckChonNhanhPB.Checked)
                            {
                                if (!_layDsChiTietLuong.Contains(idValue))
                                    _layDsChiTietLuong.Add(idValue);
                            }
                            else _layDsChiTietLuong.Remove(idValue);
                        }
                    }
                }
            }

            dgvSalaryDetails.RefreshEdit();
        }

        public void NganNhapChu(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
