using BLL;
using DAL;
using DocumentFormat.OpenXml.Wordprocessing;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCNghiPhep : UserControl
    {
        private string _id { get; set; }
        private string _lyDo { get; set; }
        private string _idSelected { get; set; }
        private string[] truongHopNghi { get; set; } = {
                    "Nghỉ phép năm",
                    "Nghỉ ốm",
                    "Nghỉ thai sản",
                    "Nghỉ do tang lễ",
                    "Nghỉ kết hôn",
                    "Nghỉ chăm sóc con ốm",
                    "Nghỉ công tác",
                    "Nghỉ lễ, Tết",
                    "Nghỉ bù",
                    "Nghỉ do tai nạn lao động",
                    "Nghỉ học tập / đào tạo",
                    "Nghỉ tạm hoãn hợp đồng",
                    "Khác ..."
                };

        public readonly string _idNhanVien;
        public readonly BLLChucVu _dbContextCV;
        public readonly BLLNghiPhep _dbContextNP;
        public readonly BLLNhanVien _dbContextNV;
        public readonly BLLHopDongLaoDong _dbContextHD;

        public UCNghiPhep(string idNhanVien, string connect)
        {
            InitializeComponent();

            _idNhanVien = idNhanVien;
            _dbContextCV = new BLLChucVu(connect);
            _dbContextNV = new BLLNhanVien(connect);
            _dbContextNP = new BLLNghiPhep(connect);
            _dbContextHD = new BLLHopDongLaoDong(connect);
        }

        // Ktra cac field rong
        public bool KiemTraDuLieuDauVao()
        {
            bool ktra = true;
            error.Clear();

            foreach (var control in grbNhanVien.Controls)
            {
                if (control is RichTextBox richtext && string.IsNullOrWhiteSpace(richtext.Text))
                {
                    error.SetError(richtext, $"{richtext.Text} Trống !");
                    ktra = false;
                }

                if (control is Guna2ComboBox combobox && string.IsNullOrWhiteSpace(combobox.Text))
                {
                    if (combobox.Name == "cmbLoaiTH")
                    {
                        continue;
                    }

                    error.SetError(combobox, $"Trống !");
                    ktra = false;
                }
            }

            return ktra;
        }

        private void UCNghiPhep_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i <= 12; i++)
                {
                    cmbLocThang.Items.Add(i);
                }

                var namHopDong = _dbContextHD.KtraDsHopDongLaoDong().Min(p => p.NgayKy.Date.Year);
                for (int i = namHopDong; i <= DateTime.Now.AddYears(1).Year; i++)
                {
                    cmbLocNam.Items.Add(i);
                }

                cmbLocThang.Text = DateTime.Now.Month.ToString();
                cmbLocNam.Text = DateTime.Now.Year.ToString();

                grbNhanVien.Text += _dbContextNV.KtraNhanVienQuaID(_idNhanVien).TenNhanVien;

                lblLichSu.Text = $"Lịch sử nghỉ phép tháng {DateTime.Now.Month}";

                ChayLaiDuLieu();

                if (!_idNhanVien.Contains("NV"))
                {
                    cmbTHNghi.Items.Add("Nghỉ thường");
                    cmbTHNghi.Items.Add("Nghỉ đột xuất");
                }
                else cmbTHNghi.Items.Add("Nghỉ thường");

                cmbLoaiTH.Enabled = false;
                //cmbTHNghi.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trang load: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Gui don nghi phep
        private void btnGui_Click(object sender, EventArgs e)
        {
            try
            {
                error.Clear();
                if (dtpBatDau.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Không được chọn thời gian trong quá khứ !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtpBatDau.Value = DateTime.Now;
                    dtpKetThuc.Value = DateTime.Now;
                    cmbTHNghi.SelectedIndex = -1;
                    return;
                }

                if (dtpKetThuc.Value.Date < dtpBatDau.Value.Date)
                {
                    MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtpKetThuc.Value = dtpBatDau.Value.AddDays(1);
                    cmbTHNghi.SelectedIndex = -1;
                    return;
                }

                if (cmbTHNghi.SelectedItem != null && cmbTHNghi.SelectedItem.ToString().Equals("Nghỉ đột xuất", StringComparison.OrdinalIgnoreCase) && cmbLoaiTH.SelectedItem == null)
                {
                    MessageBox.Show("Cần chọn loại trường hợp nghỉ đột xuất !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    error.SetError(cmbLoaiTH, "Chọn loại nghỉ!");
                    cmbTHNghi.SelectedIndex = -1;
                    return;
                }

                if (dtpBatDau.Value.Date.Month < DateTime.Now.Date.Month)
                {
                    MessageBox.Show("Không được thêm đơn nghỉ phép tháng trong quá khứ !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbTHNghi.SelectedIndex = -1;
                    return;
                }

                if (dtpBatDau.Value.Date.Year < DateTime.Now.Date.Year)
                {
                    MessageBox.Show("Không được thêm đơn nghỉ phép năm trong quá khứ !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbTHNghi.SelectedIndex = -1;
                    return;
                }

                string loai = "Có lương";
                var thangHienTai = DateTime.Now.Month;
                var DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien && p.NgayKetThuc.Month == DateTime.Now.Month).ToList();
                var nghiPhep = _dbContextNP.LayDsNghiPhep().Where(p => p.LoaiNghiPhep == "Có lương" && p.TrangThai == "Đã duyệt" && p.idNhanVien == _idNhanVien && p.LoaiNghiPhep == loai && p.NgayBatDau.Month == DateTime.Now.Month).ToList();

                if (KiemTraDuLieuDauVao())
                {
                    var ktraDuyet = _dbContextNP.KtraTrangThaiDonChuaDuyet(_idNhanVien, dtpBatDau.Value.Month, dtpBatDau.Value.Year);
                    if (!ktraDuyet)
                    {
                        if (int.TryParse(SoNgayNghiCoPhep(DsNghiPhepTheoIDNV, thangHienTai), out int countCoLuong) && countCoLuong >= 3)
                        {
                            loai = "Không lương";
                        }

                        string loaiTH = cmbLoaiTH.Text;
                        if (cmbLoaiTH.SelectedItem != null && cmbLoaiTH.SelectedItem.ToString().Equals("Khác ...", StringComparison.OrdinalIgnoreCase))
                        {
                            loaiTH = txtLoaiTHKhac.Text;
                        }

                        string coLuong = "Số ngày nghỉ phép vượt quá quy định (số ngày nghỉ > 3/đơn nghỉ phép)";
                        string khongLuong = "";
                        string ktraLoai = loai.Contains("có") ? coLuong : khongLuong;

                        var tinhNgayNghi = _dbContextNP.KtraTinhSoLuongNgayNghiCoPhep(_idNhanVien, dtpBatDau.Value, dtpKetThuc.Value, loai);
                        if ((tinhNgayNghi.CoPhep > 0 && tinhNgayNghi.KhongPhep == 0) || (tinhNgayNghi.CoPhep == 0 && tinhNgayNghi.KhongPhep > 0))
                        {
                            if (KtraDuLieuBDKT())
                            {

                                if (_dbContextNP.KtraThemNghiPhep(new DTONghiPhep(0, _idNhanVien, DateTime.Parse(dtpBatDau.Value.ToShortDateString()), DateTime.Parse(dtpKetThuc.Value.ToShortDateString()), rtLyDo.Text, cmbLoaiNghi.Text, txtTrangThai.Text, loaiTH)))
                                {
                                    ChayLaiDuLieu();
                                    MessageBox.Show("Thêm dữ liệu thành công.");
                                    Empty();
                                }
                                else MessageBox.Show("Thêm nghỉ phép thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show($"Dữ liệu đơn xin phép của ngày bắt đầu hoặc ngày kết thúc đã trùng với đơn đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else if (tinhNgayNghi.CoPhep > 0 && tinhNgayNghi.KhongPhep > 0) // Xu ly them 2 don co phep va khong cung luc
                        {
                            if (KtraDuLieuBDKT())
                            {
                                if (cmbLoaiNghi.Text.Equals("Có lương", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (MessageBox.Show($"Vì số lượng nghỉ phép có lương tháng này vượt quá 3 lần, bạn có muốn đơn nghỉ phép được chuyển sang bớt không phép không ? \nNếu chọn không, vui lòng chọn lại ngày kết thúc của đơn xin phép.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _lyDo = rtLyDo.Text;
                                        string[] loaiNghi = { "Có lương", "Không lương" };

                                        DateTime batDau = dtpBatDau.Value.Date;
                                        DateTime saveKT = dtpKetThuc.Value.Date;
                                        DateTime ketThuc = dtpKetThuc.Value.Date.AddDays(-tinhNgayNghi.KhongPhep);

                                        foreach (var item in loaiNghi)
                                        {
                                            if (_dbContextNP.KtraThemNghiPhep(new DTONghiPhep(0, _idNhanVien, DateTime.Parse(batDau.ToString("yyyy-MM-dd")), DateTime.Parse(ketThuc.ToString("yyyy-MM-dd")), _lyDo, item, txtTrangThai.Text, loaiTH)))
                                            {
                                                ChayLaiDuLieu();
                                            }
                                            else MessageBox.Show("Thêm nghỉ phép thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                            batDau = ketThuc.AddDays(1);
                                            ketThuc = saveKT;
                                        }

                                        MessageBox.Show("Thêm dữ liệu thành công.");
                                        Empty();
                                        _lyDo = null; return;
                                    }
                                    else return;
                                }
                                else
                                {
                                    if (_dbContextNP.KtraThemNghiPhep(new DTONghiPhep(0, _idNhanVien, DateTime.Parse(dtpBatDau.Value.Date.ToString("yyyy-MM-dd")), DateTime.Parse(dtpKetThuc.Value.Date.ToString("yyyy-MM-dd")), rtLyDo.Text, cmbLoaiNghi.Text, txtTrangThai.Text, cmbLoaiTH.Text)))
                                    {
                                        ChayLaiDuLieu();
                                        MessageBox.Show("Thêm dữ liệu thành công.");
                                        Empty(); return;
                                    }
                                    else MessageBox.Show("Thêm nghỉ phép thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Dữ liệu đơn xin phép trong tháng {dtpBatDau.Value.Month} của ngày bắt đầu đã trùng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                dtpBatDau.Value = DsNghiPhepTheoIDNV.Max(p => p.NgayKetThuc.AddDays(1));
                                dtpKetThuc.Value = dtpBatDau.Value;
                            }
                        }
                        else MessageBox.Show($"{ktraLoai} !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else MessageBox.Show("Đã tồn tại đơn nghỉ phép mới chưa được duyệt, vui lòng chờ đợi ý kiến từ bộ phận nhân sự !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }

        // Cap nhat don nghi phep
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_id) || !int.TryParse(_id, out int id))
                {
                    MessageBox.Show("Vui lòng chọn dữ liệu nghỉ phép cần cập nhật !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string loaiTH = cmbLoaiTH.Text;
                if (cmbLoaiTH.SelectedItem != null && cmbLoaiTH.SelectedItem.ToString().Equals("Khác ...", StringComparison.OrdinalIgnoreCase))
                {
                    loaiTH = txtLoaiTHKhac.Text;
                }

                var ktraDuyet = _dbContextNP.KtraTrangThaiNP(id);
                var DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList();

                if (!ktraDuyet)
                {
                    if (KiemTraDuLieuDauVao())
                    {
                        if (cmbLoaiNghi.Text.Equals("Có lương", StringComparison.OrdinalIgnoreCase))
                        {
                            var nghiPhepCapNhat = _dbContextNP.KtraNghiPhepQuaID(id);
                            int soNgayNghiPhepCN = (nghiPhepCapNhat.NgayKetThuc.Date - nghiPhepCapNhat.NgayBatDau.Date).Days + 1;
                            int soLuongCoPheptrongThang = DsNghiPhepTheoIDNV.Where(p => p.NgayBatDau.Date.Month == dtpBatDau.Value.Date.Month && p.LoaiNghiPhep.Equals("Có lương", StringComparison.OrdinalIgnoreCase)).Count();

                            if (soLuongCoPheptrongThang + soNgayNghiPhepCN >= 3)
                            {
                                MessageBox.Show("Số lượng nghỉ đã vượt quá theo quy chế nghỉ có phép 3 ngày/tháng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        if (_dbContextNP.KtraCapNhatNghiPhep(new DTONghiPhep(id, _idNhanVien, cmbLoaiNghi.Text, rtLyDo.Text, loaiTH)))
                        {
                            ChayLaiDuLieu();
                            MessageBox.Show("Cập nhật dữ liệu thành công.");
                        }
                        else MessageBox.Show("Cập nhật nghỉ phép thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else MessageBox.Show("Đơn nghỉ phép này của bạn đã được cập nhật trạng thái !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }

        // Xoa don nghi phep
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_id))
                {
                    MessageBox.Show("Vui lòng chọn dữ liệu nghỉ phép cần xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var ktraDuyet = _dbContextNP.KtraTrangThaiNP(Convert.ToInt32(_id));
                if (!ktraDuyet)
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa yêu cầu đã chọn không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (_dbContextNP.KtraXoaNghiPhep(new DTONghiPhep(int.Parse(_id), _idNhanVien)))
                        {
                            ChayLaiDuLieu();
                            MessageBox.Show("Xóa dữ liệu thành công.");
                            Empty();
                            _id = null;
                            dtpBatDau.Enabled = true;
                            dtpKetThuc.Enabled = true;
                        }
                        else MessageBox.Show("Xóa nghỉ phép thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else return;
                }
                else MessageBox.Show("Đơn nghỉ phép này của bạn đã được cập nhật trạng thái !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message); return;
            }
        }

        // Ham Load du lieu
        private void ChayLaiDuLieu()
        {
            cmbLoaiTH.Items.Clear();
            cmbLoaiTH.Enabled = false;
            cmbTHNghi.SelectedIndex = -1;

            _dbContextNP.KtraCapNhatTrangThaiNghiPhepChoNhieuNV();

            _id = null;
            var thangHienTai = DateTime.Now.Month;

            var DsNghiPhepTheoIDNV = new List<NghiPhep>();
            var idNhanVienNP = _dbContextNP.LayDsNghiPhep().FirstOrDefault(p => p.idNhanVien == _idNhanVien);

            if (_idNhanVien.Contains("GD"))
            {
                DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => !p.idNhanVien.StartsWith("GD")).ToList();

                txtSoNgayNghi.Text = "0";
                txtSoNgayCoPhep.Text = "0";
                txtSoNgayKhongPhep.Text = "0";
            }
            else DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList();

            var LoadData = DsNghiPhepTheoIDNV.Where(p => p.NgayBatDau.Month == DateTime.Now.Month && p.NgayBatDau.Year == DateTime.Now.Year).ToList();
            if (cmbLocThang.SelectedItem != null)
            {
                LoadData = DsNghiPhepTheoIDNV.Where(p => p.NgayBatDau.Month == (int)cmbLocThang.SelectedItem && p.NgayBatDau.Year == DateTime.Now.Year).ToList();
            }

            txtSoNgayNghi.Text = LoadData.Count(p => p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase)).ToString();
            txtSoNgayCoPhep.Text = LoadData.Count(p => p.LoaiNghiPhep.Equals("Có lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase)).ToString();
            txtSoNgayKhongPhep.Text = LoadData.Count(p => p.LoaiNghiPhep.Equals("Không lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase)).ToString();

            dgvDSNghiPhepCaNhan.DataSource = LoadData.Select(p => new
            {
                p.id,
                p.idNhanVien,
                TenNhanVien = _dbContextNV.KtraNhanVienQuaID(p.idNhanVien).TenNhanVien,
                p.NgayBatDau,
                p.NgayKetThuc,
                SoNgayNghi = ((p.NgayKetThuc.Date - p.NgayBatDau.Date).Days + 1).ToString(),
                p.LyDoNghi,
                p.LoaiNghiPhep,
                p.LoaiTruongHop,
                p.TrangThai,

            }).OrderByDescending(p => p.NgayBatDau).OrderByDescending(p => p.TrangThai.Equals("Đang yêu cầu", StringComparison.OrdinalIgnoreCase)).ToList();

            var ngayKetThuc = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien && p.NgayKetThuc.Month == dtpBatDau.Value.Date.Month);
            if (idNhanVienNP != null)
            {
                if (cmbTHNghi.SelectedItem != null && cmbTHNghi.SelectedItem.ToString().Equals("Nghỉ thường", StringComparison.OrdinalIgnoreCase))
                {
                    dtpBatDau.Value = DateTime.Now.AddDays(7);
                    dtpKetThuc.Value = DateTime.Now.AddDays(7);
                }
                else
                {
                    if (ngayKetThuc.Any())
                    {
                        dtpBatDau.Value = ngayKetThuc.Max(p => p.NgayKetThuc).AddDays(1);
                    }
                    else dtpBatDau.Value = DateTime.Now;

                    dtpKetThuc.Value = dtpBatDau.Value;
                }
            }
            else
            {
                if (ngayKetThuc.Any())
                {
                    dtpBatDau.Value = ngayKetThuc.Max(p => p.NgayKetThuc).AddDays(1);
                }
                else dtpBatDau.Value = DateTime.Now;

                dtpKetThuc.Value = dtpBatDau.Value;
            }

            if (int.TryParse(SoNgayNghiCoPhep(DsNghiPhepTheoIDNV, dtpBatDau.Value.Month), out int countCoLuong) && countCoLuong >= 3)
            {
                cmbLoaiNghi.DataSource = new List<string> { "Không lương" };
            }
            else cmbLoaiNghi.DataSource = new List<string> { "Có lương", "Không lương" };


            if (dgvDSNghiPhepCaNhan.Columns["id"].Visible)
            {
                dgvDSNghiPhepCaNhan.Columns["id"].Visible = false;
                dgvDSNghiPhepCaNhan.Columns["idNhanVien"].Visible = false;
            }

            var nhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien);
            var ngayTrongThang = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var luongCBNV = _dbContextCV.LayDsChucVu().FirstOrDefault(p => p.id == nhanVien.idChucVu).luongCoBan;
            var tinhTienPhat = (double)luongCBNV / ngayTrongThang;

            if (_idNhanVien.Contains("GD"))
            {
                if (!string.IsNullOrEmpty(_id))
                {
                    dgvDSLichSuNP.DataSource = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _id && p.LoaiNghiPhep.Equals("Không lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase) && p.NgayBatDau.Month == DateTime.Now.Month && p.NgayBatDau.Year == DateTime.Now.Year)
                                                          .Select(p => new
                                                          {
                                                              NgayNghi = $"{p.NgayBatDau.Day}/{thangHienTai} - {p.NgayKetThuc.Day}/{thangHienTai}",
                                                              TruTien = "- " + string.Format(new CultureInfo("vi-VN"), "{0:C0}", tinhTienPhat * (p.NgayKetThuc.Day - p.NgayBatDau.Day + 1))

                                                          }).ToList();
                }
                else dgvDSLichSuNP.DataSource = new List<NghiPhep>().Select(p => new { NgayNghi = "", TruTien = "" }).ToList();

            }
            else dgvDSLichSuNP.DataSource = DsNghiPhepTheoIDNV.Where(p => p.LoaiNghiPhep.Equals("Không lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase) && p.NgayBatDau.Month == DateTime.Now.Month && p.NgayBatDau.Year == DateTime.Now.Year)
                                                           .Select(p => new
                                                           {
                                                               NgayNghi = $"{p.NgayBatDau.Day}/{thangHienTai} - {p.NgayKetThuc.Day}/{thangHienTai}",
                                                               TruTien = "- " + string.Format(new CultureInfo("vi-VN"), "{0:C0}", tinhTienPhat * (p.NgayKetThuc.Day - p.NgayBatDau.Day + 1))

                                                           }).ToList();

            if (_idNhanVien.Contains("GD"))
            {
                btnGui.Visible = false;
                btnSua.Visible = false;
                btnXoa.Visible = false;

            }
            else
            {
                btnGui.Visible = true;
                btnSua.Visible = true;
                btnXoa.Visible = true;
            }

            dgvDSNghiPhepCaNhan.Columns["LyDoNghi"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dgvDSNghiPhepCaNhan..AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            LoadHeaderText();
        }

        // Ham tra ve field trong
        public void Empty()
        {
            rtLyDo.Text = string.Empty;
            txtLoaiTHKhac.Clear();
        }

        // Bang loc thang
        private void cmbLocThang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lblLichSu.Text = "Lịch sử nghỉ phép tháng " + cmbLocThang.Text;
            var DsNghiPhep = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList();
            var selectItemFilter = cmbLocThang.SelectedItem == null ? DateTime.Now.Month : (int)cmbLocThang.SelectedItem;

            var DsNghiPhepTheoIDNV = new List<NghiPhep>();
            var tinhSoNgayNghiTheoThang = new List<NghiPhep>();
            var idNhanVienNP = _dbContextNP.LayDsNghiPhep().FirstOrDefault(p => p.idNhanVien == _idNhanVien);
            var nhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien.Contains("GD") ? _idSelected : _idNhanVien);

            if (_idNhanVien.Contains("GD") && string.IsNullOrEmpty(_idSelected))
            {
                tinhSoNgayNghiTheoThang = new List<NghiPhep>();
                DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => !p.idNhanVien.StartsWith("GD")).ToList();
            }
            else
            {
                DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList();
                tinhSoNgayNghiTheoThang = DsNghiPhepTheoIDNV.Where(p => p.idNhanVien == _idNhanVien && p.NgayBatDau.Month == (int)cmbLocThang.SelectedItem && p.NgayBatDau.Year == (int)cmbLocNam.SelectedItem).ToList();
            }

            txtSoNgayNghi.Text = tinhSoNgayNghiTheoThang.Count(p => p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase)).ToString();
            txtSoNgayCoPhep.Text = tinhSoNgayNghiTheoThang.Count(p => p.LoaiNghiPhep.Equals("Có lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase)).ToString();
            txtSoNgayKhongPhep.Text = tinhSoNgayNghiTheoThang.Count(p => p.LoaiNghiPhep.Equals("Không lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase)).ToString();

            dtpBatDau.Enabled = true;
            dtpKetThuc.Enabled = true;
            if (cmbLocThang.SelectedItem != null)
            {
                dtpBatDau.Value = new DateTime(DateTime.Now.Year, (int)cmbLocThang.SelectedItem, 1);
                dtpKetThuc.Value = dtpBatDau.Value.Date;
            }
            else
            {
                dtpBatDau.Value = DateTime.Now.AddDays(1);
                dtpKetThuc.Value = DateTime.Now.AddDays(1);
            }

            dgvDSLichSuNP.DataSource = new List<NghiPhep>().Select(p => new { NgayNghi = "", TruTien = "" }).ToList();
            dgvDSNghiPhepCaNhan.DataSource = _dbContextNP.LayDsNghiPhep().Where(p => checkIDNVdgvDSNghiPhepCaNhan(p.idNhanVien) && p.NgayBatDau.Month == (int)cmbLocThang.SelectedItem && p.NgayBatDau.Year == (int)cmbLocNam.SelectedItem)
                                                                        .Select(p => new
                                                                        {
                                                                            p.id,
                                                                            p.idNhanVien,
                                                                            TenNhanVien = _dbContextNV.KtraNhanVienQuaID(p.idNhanVien).TenNhanVien,
                                                                            p.NgayBatDau,
                                                                            p.NgayKetThuc,
                                                                            SoNgayNghi = ((p.NgayKetThuc - p.NgayBatDau).Days + 1).ToString(),
                                                                            p.LyDoNghi,
                                                                            p.LoaiNghiPhep,
                                                                            p.LoaiTruongHop,
                                                                            p.TrangThai
                                                                        })
                                                                        .OrderByDescending(p => p.NgayBatDau).OrderByDescending(p => p.TrangThai.Equals("Đang yêu cầu", StringComparison.OrdinalIgnoreCase))
                                                                        .ToList();

            if (cmbLocThang.SelectedItem != null)
            {
                if (int.TryParse(SoNgayNghiCoPhep(DsNghiPhepTheoIDNV, (int)cmbLocThang.SelectedItem), out int countCoLuong) && countCoLuong >= 3)
                {
                    cmbLoaiNghi.DataSource = new List<string> { "Không lương" };
                }
                else cmbLoaiNghi.DataSource = new List<string> { "Có lương", "Không lương" };
            }

            cmbLoaiTH.Items.Clear();
            cmbLoaiTH.Enabled = false;
            cmbTHNghi.SelectedIndex = -1;
            LoadHeaderText();
        }

        public bool checkIDNVdgvDSNghiPhepCaNhan(string idNhanVien) => _idNhanVien.Contains("GD") ? _idNhanVien != null : _idNhanVien == idNhanVien;

        private void dgvDSNghiPhepCaNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbLoaiTH.Items.Clear();
            if (e != null && e.RowIndex > -1)
            {
                _id = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["id"]?.Value.ToString();
                lblIDNP.Text = _id;
                _idSelected = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["idNhanVien"]?.Value.ToString();
                rtLyDo.Text = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["LyDonghi"]?.Value.ToString();
                cmbLoaiNghi.Text = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["LoaiNghiPhep"]?.Value.ToString();

                if (dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["LoaiTruongHop"]?.Value.ToString() == "Nghỉ thường")
                {
                    //cmbTHNghi.SelectedIndex = -1;
                    cmbLoaiTH.Visible = true;
                    txtLoaiTHKhac.Visible = false;

                    cmbTHNghi.Text = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["LoaiTruongHop"]?.Value.ToString();
                    if (cmbTHNghi.SelectedItem == null)
                    {
                        cmbLoaiTH.Enabled = true;
                        cmbLoaiTH.SelectedIndex = -1;
                    }
                    else cmbLoaiTH.Enabled = false;
                }
                else
                {
                    cmbTHNghi.Text = "Nghỉ đột xuất";
                    cmbLoaiTH.Enabled = true;

                    cmbLoaiTH.Items.AddRange(truongHopNghi);
                    cmbLoaiTH.Text = dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["LoaiTruongHop"]?.Value.ToString();
                }

                dtpBatDau.Value = DateTime.Parse(dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["NgayBatDau"]?.Value.ToString());
                dtpKetThuc.Value = DateTime.Parse(dgvDSNghiPhepCaNhan.Rows[e.RowIndex].Cells["NgayKetThuc"]?.Value.ToString());

                var trangThai = _dbContextNP.KtraTrangThaiNP(Convert.ToInt32(_id));
                if (!trangThai)
                {
                    dtpBatDau.Enabled = false;
                    dtpKetThuc.Enabled = false;
                }
                else
                {
                    dtpBatDau.Enabled = true;
                    dtpKetThuc.Enabled = true;
                }

                var thangHienTai = DateTime.Now.Month;

                var DsNghiPhepTheoIDNV = new List<NghiPhep>();
                var selectedNP = _dbContextNP.KtraNghiPhepQuaID(Convert.ToInt32(_id));
                var idNhanVienNP = _dbContextNP.LayDsNghiPhep().FirstOrDefault(p => p.idNhanVien == _idNhanVien);

                if (_idNhanVien.Contains("GD"))
                {
                    DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => !p.idNhanVien.StartsWith("GD")).ToList();
                }
                else DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList();

                var nhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien.Contains("GD") ? _idSelected : _idNhanVien);
                var ngayTrongThang = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                var luongCBNV = _dbContextCV.LayDsChucVu().FirstOrDefault(p => p.id == nhanVien.idChucVu).luongCoBan;
                var tinhTienPhat = (double)luongCBNV / ngayTrongThang;

                if (_idNhanVien.Contains("GD"))
                {
                    if (!string.IsNullOrEmpty(_id))
                    {
                        dgvDSLichSuNP.DataSource = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idSelected && p.LoaiNghiPhep.Equals("Không lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase) && p.NgayBatDau.Month == selectedNP.NgayBatDau.Month && p.NgayBatDau.Year == DateTime.Now.Year)
                                                              .Select(p => new
                                                              {
                                                                  NgayNghi = $"{p.NgayBatDau.Day}/{p.NgayBatDau.Month} - {p.NgayKetThuc.Day}/{p.NgayKetThuc.Month}",
                                                                  TruTien = "- " + string.Format(new CultureInfo("vi-VN"), "{0:C0}", tinhTienPhat * (p.NgayKetThuc.Day - p.NgayBatDau.Day + 1))

                                                              }).ToList();
                    }
                    else dgvDSLichSuNP.DataSource = new List<NghiPhep>().Select(p => new { NgayNghi = "", TruTien = "" }).ToList();

                }
                else dgvDSLichSuNP.DataSource = DsNghiPhepTheoIDNV.Where(p => p.LoaiNghiPhep.Equals("Không lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase) && p.NgayBatDau.Month == selectedNP.NgayBatDau.Month && p.NgayBatDau.Year == DateTime.Now.Year)
                                                               .Select(p => new
                                                               {
                                                                   NgayNghi = $"{p.NgayBatDau.Day}/{p.NgayBatDau.Month} - {p.NgayKetThuc.Day}/{p.NgayKetThuc.Month}",
                                                                   TruTien = "- " + string.Format(new CultureInfo("vi-VN"), "{0:C0}", tinhTienPhat * (p.NgayKetThuc.Day - p.NgayBatDau.Day + 1))

                                                               }).ToList();

                if (_idNhanVien.Contains("GD"))
                {
                    DsNghiPhepTheoIDNV = _dbContextNP.LayDsNghiPhep().Where(p => !p.idNhanVien.StartsWith("GD")).ToList();
                }

                if (int.TryParse(SoNgayNghiCoPhep(DsNghiPhepTheoIDNV, dtpBatDau.Value.Month), out int countCoLuong) && countCoLuong >= 3)
                {
                    cmbLoaiNghi.DataSource = new List<string> { "Không lương" };
                }
                else cmbLoaiNghi.DataSource = new List<string> { "Có lương", "Không lương" };

                var tinhSoNgayNghiTheoThang = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == nhanVien.id && p.NgayBatDau.Month == selectedNP.NgayBatDau.Month && p.NgayBatDau.Year == DateTime.Now.Year && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase));

                var LoadData = DsNghiPhepTheoIDNV.Where(p => p.NgayBatDau.Month == DateTime.Now.Month && p.NgayBatDau.Year == DateTime.Now.Year).ToList();
                if (cmbLocThang.SelectedItem != null)
                {
                    LoadData = DsNghiPhepTheoIDNV.Where(p => p.NgayBatDau.Month == (int)cmbLocThang.SelectedItem && p.NgayBatDau.Year == DateTime.Now.Year).ToList();
                }

                txtSoNgayNghi.Text = LoadData.Count(p => p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase)).ToString();
                txtSoNgayCoPhep.Text = LoadData.Count(p => p.LoaiNghiPhep.Equals("Có lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase)).ToString();
                txtSoNgayKhongPhep.Text = LoadData.Count(p => p.LoaiNghiPhep.Equals("Không lương", StringComparison.OrdinalIgnoreCase) && p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase)).ToString();
            }
        }

        // Reload du lieu
        private void btnLoadDuLieu_Click(object sender, EventArgs e)
        {
            ChayLaiDuLieu();
            dtpBatDau.Enabled = true;
            dtpKetThuc.Enabled = true;
        }

        // Lay so ngay co phep theo thang
        private string SoNgayNghiCoPhep(List<NghiPhep> DsNghiPhep, int thangHienTai)
        {
            var nghiCoLuong = DsNghiPhep.Where(p => p.LoaiNghiPhep.Equals("Có lương", StringComparison.OrdinalIgnoreCase) &&
                                                    p.NgayBatDau.Month == thangHienTai && p.NgayBatDau.Year == DateTime.Now.Year).ToList();

            return nghiCoLuong.Sum(p => (p.NgayKetThuc - p.NgayBatDau).Days) + 1 + "";
        }

        // Lay so ngay khong phep theo thang
        private string SoNgayNghiKhongPhep(List<NghiPhep> DsNghiPhep, int thangHienTai)
        {
            var nghiCoLuong = DsNghiPhep.Where(p => p.TrangThai.Equals("Đã duyệt", StringComparison.OrdinalIgnoreCase) && p.LoaiNghiPhep.Equals("Không lương", StringComparison.OrdinalIgnoreCase) &&
                                          p.NgayBatDau.Month == thangHienTai && p.NgayBatDau.Year == DateTime.Now.Year).ToList();

            return nghiCoLuong.Sum(p => p.NgayKetThuc.Day - p.NgayBatDau.Day) + nghiCoLuong.Count + "";
        }

        // Xu ly click ngay bat dau
        private void dtpBatDau_CloseUp(object sender, EventArgs e)
        {
            error.Clear();
            if (!_idNhanVien.Contains("GD"))
            {
                var ngayKetThuc = _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien && p.NgayKetThuc.Month == dtpBatDau.Value.Date.Month);

                if (cmbTHNghi.SelectedItem != null && cmbTHNghi.SelectedItem.ToString().Equals("Nghỉ đột xuất", StringComparison.OrdinalIgnoreCase))
                {
                    if (dtpBatDau.Value.Date < DateTime.Now.Date)
                    {
                        error.SetError(dtpBatDau, "Không được chọn thời gian trong quá khứ !");
                        if (ngayKetThuc.Any())
                        {
                            dtpBatDau.Value = ngayKetThuc.Max(p => p.NgayKetThuc).AddDays(1);
                        }
                        else dtpBatDau.Value = DateTime.Now;
                    }
                    else if (dtpBatDau.Value.Date > dtpKetThuc.Value.Date)
                    {
                        error.SetError(dtpBatDau, "Ngày bắt đầu không được lớn hơn ngày kết thúc !");
                        if (ngayKetThuc.Any())
                        {
                            dtpBatDau.Value = ngayKetThuc.Max(p => p.NgayKetThuc).AddDays(1);
                            dtpKetThuc.Value = ngayKetThuc.Max(p => p.NgayKetThuc).AddDays(1);
                        }
                        else
                        {
                            dtpBatDau.Value = DateTime.Now;
                            dtpKetThuc.Value = dtpBatDau.Value.Date;
                        }
                    }
                }
                else
                {
                    var sau1Tuan = DateTime.Now.Date.AddDays(7);
                    if (sau1Tuan > dtpBatDau.Value.Date)
                    {
                        error.SetError(dtpBatDau, "Cần chọn ngày nghỉ phép sau 1 tuần kể từ ngày hiện tại !");
                        dtpBatDau.Value = sau1Tuan;
                        dtpKetThuc.Value = sau1Tuan;
                    }
                    else if (dtpBatDau.Value.Date > dtpKetThuc.Value.Date)
                    {
                        error.SetError(dtpBatDau, "Ngày bắt đầu không được lớn hơn ngày kết thúc !");
                        dtpBatDau.Value = sau1Tuan;
                        dtpKetThuc.Value = sau1Tuan;
                    }
                }
            }
        }

        // Xu ly click ngay ket thuc
        private void dtpKetThuc_CloseUp(object sender, EventArgs e)
        {
            var date = DateTime.Now.Date;
            if (dtpKetThuc.Value.Date < dtpBatDau.Value.Date)
            {
                error.SetError(dtpKetThuc, "Ngày kết thúc không được nhỏ hơn ngày bắt đầu !");
                dtpKetThuc.Value = dtpBatDau.Value;
            }
            else if (dtpKetThuc.Value.Date > date.AddMonths(3))
            {
                MessageBox.Show($"Đơn nghỉ phép chỉ được xử lý từ {date.ToString("dd/MM/yyyy")} - {date.AddMonths(3).ToString("dd/MM/yyyy")} !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpKetThuc.Value = dtpBatDau.Value.Date;
                return;
            }
        }

        public void LoadHeaderText()
        {
            //dgvDSLichSuNP.Columns["LyDoNghi"].HeaderText = "Lý do nghỉ";

            dgvDSLichSuNP.Columns["TruTien"].HeaderText = "Số tiền trừ";
            dgvDSLichSuNP.Columns["NgayNghi"].HeaderText = "Ngày nghỉ";

            dgvDSNghiPhepCaNhan.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
            dgvDSNghiPhepCaNhan.Columns["NgayKetThuc"].HeaderText = "Ngày kết thúc";
            dgvDSNghiPhepCaNhan.Columns["SoNgayNghi"].HeaderText = "Số ngày nghỉ";
            dgvDSNghiPhepCaNhan.Columns["LyDoNghi"].HeaderText = "Lý do nghỉ";
            dgvDSNghiPhepCaNhan.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvDSNghiPhepCaNhan.Columns["LoaiNghiPhep"].HeaderText = "Loại nghỉ phép";
            dgvDSNghiPhepCaNhan.Columns["LoaiTruongHop"].HeaderText = "Loại TH đột xuất";

            if (_idNhanVien.Contains("GD"))
            {
                dgvDSNghiPhepCaNhan.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
            }
            else dgvDSNghiPhepCaNhan.Columns["TenNhanVien"].Visible = false;

            dgvDSNghiPhepCaNhan.Columns["LyDoNghi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        public bool KtraDuLieuBDKT()
        {
            bool ktra = true;
            DateTime batDau = dtpBatDau.Value.Date;
            DateTime ketThuc = dtpKetThuc.Value.Date;

            _dbContextNP.LayDsNghiPhep().Where(p => p.idNhanVien == _idNhanVien).ToList().ForEach(p =>
            {
                var bdDaCo = p.NgayBatDau.Date;
                var ktDaCo = p.NgayKetThuc.Date;

                if ((batDau >= bdDaCo && batDau <= ktDaCo) || (batDau <= bdDaCo && ketThuc >= ktDaCo) || (batDau <= bdDaCo && ketThuc >= ktDaCo))
                {
                    ktra = false;
                }
            });

            return ktra;
        }

        private void cmbTHNghi_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                dtpBatDau.Enabled = true;
                dtpKetThuc.Enabled = true;
                cmbLoaiTH.Items.Clear();

                if (cmbTHNghi.SelectedItem != null && cmbTHNghi.SelectedItem.ToString().Equals("Nghỉ đột xuất", StringComparison.OrdinalIgnoreCase))
                {
                    cmbLoaiNghi.Text = "Không lương";
                    cmbLoaiTH.Enabled = true;

                    cmbLoaiTH.Items.AddRange(truongHopNghi);

                    dtpBatDau.Value = DateTime.Now;
                    dtpKetThuc.Value = DateTime.Now;
                }
                else
                {
                    cmbLoaiTH.Text = string.Empty;
                    cmbLoaiTH.Enabled = false;
                    txtLoaiTHKhac.Visible = false;
                    cmbLoaiTH.Visible = true;

                    dtpBatDau.Value = DateTime.Now.AddDays(7);
                    dtpKetThuc.Value = DateTime.Now.AddDays(7);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmbLoaiTH_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbLoaiTH.SelectedItem.ToString().Equals("Khác ...", StringComparison.OrdinalIgnoreCase))
            {
                cmbLoaiTH.Visible = false;
                txtLoaiTHKhac.Visible = true;
            }
        }

        private void cmbLocNam_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbLocThang.SelectedItem != null)
            {
                cmbLocThang.SelectedIndex = -1;
            }
        }
    }
}
