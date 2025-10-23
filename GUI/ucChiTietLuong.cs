using BLL;
using DAL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.ModelBinding;
using System.Web.WebSockets;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;

namespace GUI
{
    public partial class ucChiTietLuong : UserControl
    {
        private List<string> _layDsChiTietLuong = new List<string>();

        private string _idSelected { get; set; }
        public readonly string _idNhanVien, _conn;

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
                var data = (List<NhanVienLuongCT>)ChayLaiDuLieu(null);

                foreach (var nv in data)
                {
                    nv.Checked = _dbContextCTL.KtraDsChiTietLuong()
                        .Any(ct => ct.idNhanVien == nv.ID &&
                                   ct.ngayNhanLuong.Month == DateTime.Now.Month + 1 &&
                                   ct.ngayNhanLuong.Year == DateTime.Now.Year);
                }

                dgvSalaryDetails.DataSource = data;
            }
            else dgvSalaryDetails.DataSource = ChayLaiDuLieu(null);

            if (dgvSalaryDetails.Columns["ID"] != null)
            {
                if (dgvSalaryDetails.Columns["ID"].Visible == true)
                {
                    dgvSalaryDetails.Columns["ID"].Visible = false;
                }

                if (dgvSalaryDetails.Columns["Checked"].ReadOnly == true)
                {
                    dgvSalaryDetails.Columns["Checked"].ReadOnly = false;
                }
            }

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void dgvSalaryDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                _idSelected = dgvSalaryDetails.Rows[e.RowIndex].Cells["ID"]?.Value.ToString();
                txtTenNhanVien.Text = dgvSalaryDetails.Rows[e.RowIndex].Cells["NhanVien"]?.Value.ToString() ?? "Không hiẻn thị";


                if (!string.IsNullOrEmpty(_idSelected))
                {
                    double tongTienThuong = 0;
                    double tongTienPhat = 0;
                    double tongKhauTru = 0;
                    double tongPhuCap = 0;

                    _dbContextNV_TP.KtraDsNhanVien_ThuongPhat().Where(p => p.idNhanVien == _idSelected).Select(p => p.idThuongPhat).ToList().ForEach(id =>
                    {
                        tongTienThuong += (double)_dbContextTP.CheckListThuongPhat().Where(p => p.loai.Equals("Thưởng", StringComparison.OrdinalIgnoreCase) && p.id == id).Sum(s => s.tienThuongPhat);
                        tongTienPhat += (double)_dbContextTP.CheckListThuongPhat().Where(p => p.loai.Equals("Phạt", StringComparison.OrdinalIgnoreCase) && p.id == id).Sum(s => s.tienThuongPhat);
                    });

                    var timeCurrent = DateTime.Now;
                    var luongTruocKT = (double)_dbContextHD.KtraDsHopDongLaoDong().FirstOrDefault(p => p.idNhanVien == _idSelected).LuongThoaThuan;

                    _dbContextNV_KT.KtraDsNhanVien_KhauTru().Where(p => p.idNhanVien == _idSelected).ToList().ForEach(id =>
                    {
                        _dbContextKT.KtraDsKhauTru().Where(p => p.id == id.idKhauTru).ToList().ForEach(kt =>
                        {
                            tongKhauTru += (double)kt.soTien;
                        });
                    });

                    _dbContextNV_PC.KtraDsNV_PC().Where(p => p.idNhanVien == _idSelected).ToList().ForEach(id =>
                    {
                        _dbContextPC.KtraDsPhuCap().Where(p => p.id == id.idPhuCap).ToList().ForEach(pc =>
                        {
                            tongPhuCap += (double)pc.soTien;
                        });
                    });

                    var luongSauKT = luongTruocKT - tongKhauTru;

                    txtTrangThai.Text = "Đang chi trả";
                    txtPhuCap.Text = tongPhuCap.ToString();
                    txtTienPhat.Text = tongTienPhat.ToString();
                    txtTongKhauTru.Text = tongKhauTru.ToString();
                    txtTienThuong.Text = tongTienThuong.ToString();
                    txtLuongSauKhauTru.Text = luongSauKT.ToString();
                    txtLuongTruocKhauTru.Text = luongTruocKT.ToString();
                    txtThucLanh.Text = string.Format(new CultureInfo("vi-VN"), "{0:C0}", luongSauKT + tongPhuCap + tongTienThuong - tongTienPhat);


                    var NgayKyLuong = _dbContextKL.KtraDsKyLuong().FirstOrDefault(p => p.ngayBatDau != null && p.ngayBatDau.Value.Year == timeCurrent.Year && p.ngayBatDau.Value.Month == timeCurrent.Month);
                    if (NgayKyLuong != null)
                    {
                        txtNgayNhanLuong.Text = DateTime.Parse(NgayKyLuong.ngayChiTra.ToString()).ToShortDateString();
                    }

                    var idPhongBan = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).idPhongBan;
                    var tenPhongBan = _dbContextPB.KtraPhongBan(idPhongBan).ToLowerInvariant();
                    txtPhongBanNV.Text = tenPhongBan;
                }

                btnUpdate.Enabled = false;
                btnDelete.Enabled = true;

                foreach (var control in guna2Panel1.Controls)
                {
                    if (control is Guna2TextBox text && !string.IsNullOrEmpty(text.Text))
                    {
                        text.ReadOnly = false;
                    }
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
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

                    var timeCurrent = DateTime.Now;
                    var idKyLuong = _dbContextKL.KtraDsKyLuong().FirstOrDefault(p => p.ngayBatDau != null && p.ngayBatDau.Value.Year == timeCurrent.Year && p.ngayBatDau.Value.Month == timeCurrent.Month).id;

                    var dsDaThemThanhCong = new List<string>();

                    foreach (var idNhanVien in _layDsChiTietLuong)
                    {

                        decimal tongTienThuong = 0;
                        decimal tongTienPhat = 0;
                        decimal tongKhauTru = 0;
                        decimal tongPhuCap = 0;

                        _dbContextNV_TP.KtraDsNhanVien_ThuongPhat().Where(p => p.idNhanVien == idNhanVien).Select(p => p.idThuongPhat).ToList().ForEach(id =>
                        {
                            tongTienThuong += _dbContextTP.CheckListThuongPhat().Where(p => p.loai.Equals("Thưởng", StringComparison.OrdinalIgnoreCase) && p.id == id).Sum(s => s.tienThuongPhat);
                            tongTienPhat += _dbContextTP.CheckListThuongPhat().Where(p => p.loai.Equals("Phạt", StringComparison.OrdinalIgnoreCase) && p.id == id).Sum(s => s.tienThuongPhat);
                        });


                        decimal luongTruocKT = _dbContextHD.KtraDsHopDongLaoDong().FirstOrDefault(p => p.idNhanVien == idNhanVien).LuongThoaThuan;

                        _dbContextNV_KT.KtraDsNhanVien_KhauTru().Where(p => p.idNhanVien == idNhanVien).ToList().ForEach(id =>
                        {
                            _dbContextKT.KtraDsKhauTru().Where(p => p.id == id.idKhauTru).ToList().ForEach(kt =>
                            {
                                tongTienPhat += kt.soTien;
                            });
                        });

                        _dbContextNV_PC.KtraDsNV_PC().Where(p => p.idNhanVien == idNhanVien).ToList().ForEach(id =>
                        {
                            _dbContextPC.KtraDsPhuCap().Where(p => p.id == id.idPhuCap).ToList().ForEach(pc =>
                            {
                                tongPhuCap += pc.soTien;
                            });
                        });

                        var luongSauKT = luongTruocKT - tongKhauTru;

                        if (DateTime.TryParse(txtNgayNhanLuong.Text, out DateTime ngayNhanLuong))
                        {
                            if (_dbContextCTL.KtraDuLieuChiTietLuongNV(idNhanVien) == null)
                            {
                                var themThanhCong = _dbContextCTL.KtraThemChiTietLuong(new DTOChiTietLuong(0, ngayNhanLuong, luongTruocKT, luongSauKT, tongKhauTru, tongPhuCap, tongTienThuong, tongTienPhat, txtTrangThai?.Text ?? null, rtGhiChu?.Text ?? null, idNhanVien, idKyLuong));
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

                    foreach (var idNhanVien in dsDaThemThanhCong)
                    {
                        _layDsChiTietLuong.Remove(idNhanVien);
                    }

                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
            finally { Empty(); }
        }

        public void Empty()
        {
            foreach (var control in guna2Panel1.Controls)
            {
                if (control is Guna2TextBox text && !string.IsNullOrEmpty(text.Text))
                {
                    text.Text = string.Empty;
                }
            }
        }

        private void cmbPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Load du lieu theo chuc vu GD, TP
        private object ChayLaiDuLieu(List<NhanVien> dsNhanVienLoc = null)
        {
            string[] loaiChucVu = { "GD", "TP" };

            var anonymous = new object();
            var dsNhanVien = _dbContextNV.KtraDsNhanVien();
            var dsChiTietLuong = _dbContextCTL.KtraDsChiTietLuong();
            var idPhongBan = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).idPhongBan;
            var tenPhongBan = _dbContextPB.KtraPhongBan(idPhongBan).ToLowerInvariant();
            var dsTruongPhong = _dbContextNV.KtraDsNhanVien().Where(p => p.id.Contains("TP")).ToList();
            var dsNhanVienPB = _dbContextNV.KtraDsNhanVien().Where(p => p.idPhongBan == idPhongBan).ToList();

            foreach (var loai in loaiChucVu)
            {
                if (_idNhanVien.StartsWith(loai))
                {
                    var stringNVFilter = _idNhanVien.Substring(loai.Length);
                    var isDsNhanVien = dsNhanVienLoc == null ? dsNhanVienPB : dsNhanVienLoc;

                    if (loai == "GD")
                    {
                        anonymous = dsNhanVien.Where(p => !p.id.StartsWith(loai)).GroupBy(p => p.id).Select(p => new
                        {
                            ID = p.Key,
                            NhanVien = p.Select(s => s.TenNhanVien).FirstOrDefault(),
                            GioiTinh = p.Select(s => s.GioiTinh).FirstOrDefault(),
                            NgaySinh = p.Select(s => s.NgaySinh).FirstOrDefault(),
                            Email = p.Select(s => s.Email).FirstOrDefault(),
                            ChucVu = p.Select(s => s.ChucVu.TenChucVu).FirstOrDefault(),
                            //    Checked = _dbContextCTL.KtraDsChiTietLuong()
                            //    .Any(ct => ct._idSelected == p.Key && ct.ngayNhanLuong.Month == DateTime.Now.Month + 1 && ct.ngayNhanLuong.Year == DateTime.Now.Year)
                        }).ToList();
                    }
                    else
                    {
                        anonymous = isDsNhanVien
                            .Where(p => !p.id.StartsWith(loai))
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
                                    ct.ngayNhanLuong.Month == DateTime.Now.Month + 1 &&
                                    ct.ngayNhanLuong.Year == DateTime.Now.Year)
                            })
                            .ToList();

                        txtPhongBanNV.Text = tenPhongBan;
                    }

                    //cmbTenNV.DataSource = loai == "GD" ? dsNhanVien : isDsNhanVien;
                    //cmbTenNV.DisplayMember = "TenNhanVien";
                    //cmbTenNV.ValueMember = "ID";

                    //lblDsNV.Text = $"{moDau} {tenPhongBan} đánh giá {chucVu} trong tháng {DateTime.Now.Month}";
                    break;
                }
            }

            var timeCurrent = DateTime.Now;
            var NgayKyLuong = _dbContextKL.KtraDsKyLuong().FirstOrDefault(p => p.ngayBatDau != null && p.ngayBatDau.Value.Year == timeCurrent.Year && p.ngayBatDau.Value.Month == timeCurrent.Month);
            if (NgayKyLuong != null)
            {
                txtNgayNhanLuong.Text = DateTime.Parse(NgayKyLuong.ngayChiTra.ToString()).ToShortDateString();
            }

            return anonymous;
        }

        private void txtLuongTruocKhauTru_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);


        public void LayKiTuSo(object sender)
        {
            var text = sender as Guna2TextBox;
            if (text == null) return;

            if (Regex.IsMatch(text.Text, @"\D"))
            {
                var index = Math.Max(0, text.SelectionStart - 1);
                text.Text = Regex.Replace(text.Text, "[^0-9]", "");
                text.SelectionStart = Math.Min(index, text.Text.Length);
            }
        }

        private void txtLuongTruocKhauTru_TextChanged(object sender, EventArgs e) => LayKiTuSo(sender);

        private void cmbLuongTraSau_TextChanged(object sender, EventArgs e) => LayKiTuSo(sender);

        private void txtTongKhauTru_TextChanged(object sender, EventArgs e) => LayKiTuSo(sender);

        private void txtPhuCap_TextChanged(object sender, EventArgs e) => LayKiTuSo(sender);

        private void txtTienPhat_TextChanged(object sender, EventArgs e) => LayKiTuSo(sender);

        private void txtTienThuong_TextChanged(object sender, EventArgs e) => LayKiTuSo(sender);

        private void cmbLuongTraSau_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        private void txtTongKhauTru_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        private void txtPhuCap_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        private void txtTienPhat_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        private void txtTienThuong_KeyPress(object sender, KeyPressEventArgs e) => NganNhapChu(e);

        public void NganNhapChu(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvSalaryDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && dgvSalaryDetails.Columns[e.ColumnIndex].Name == "Checked")
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dgvSalaryDetails.Rows[e.RowIndex].Cells["Checked"];
                bool isChecked = (bool)(checkBoxCell.EditedFormattedValue ?? false);

                string idNhanVien = dgvSalaryDetails.Rows[e.RowIndex].Cells["ID"].Value?.ToString();

                if (!string.IsNullOrEmpty(idNhanVien))
                {
                    if (isChecked)
                    {
                        if (!_layDsChiTietLuong.Contains(idNhanVien))
                        {
                            _layDsChiTietLuong.Add(idNhanVien);

                        }

                    }
                    else
                    {
                        _layDsChiTietLuong.Remove(idNhanVien);
                    }
                }
                //btnAdd.Click += btnAdd_Click;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void guna2HtmlLabel17_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                var chiTietLuong = _dbContextCTL.KtraChiTietLuongQuaIDNhanVien(_idSelected);
                if (chiTietLuong != null)
                {
                    var dsXoa = _dbContextCTL.KtraDsChiTietLuong().FirstOrDefault(p => p.idNhanVien == chiTietLuong.idNhanVien && p.ngayNhanLuong.Year == DateTime.Now.Year && p.ngayNhanLuong.Month == DateTime.Now.Month + 1);

                    //var dsXoa = dgvSalaryDetails.Rows
                    //    .Cast<DataGridViewRow>()
                    //    .Where(r => Convert.ToBoolean(r.Cells["Checked"].Value) == true)
                    //    .Select(r => r.Cells["ID"].Value?.ToString())
                    //    .Where(id => !string.IsNullOrEmpty(id))
                    //    .ToList();

                    if (dsXoa == null)
                    {
                        MessageBox.Show($"Nhân viên {_dbContextNV.KtraNhanVienQuaID(_idSelected).TenNhanVien} chưa được thêm. !");
                        return;
                    }

                    var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa các bản ghi đã chọn không?",
                                                  "Xác nhận xóa",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

                    if (confirm != DialogResult.Yes)
                        return;


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

                }
                else MessageBox.Show($"Nhân viên {_dbContextNV.KtraNhanVienQuaID(_idSelected).TenNhanVien} chưa được thêm. !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        // Ktra cac field rong
        public bool KiemTraDuLieuDauVao()
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
    }
}
