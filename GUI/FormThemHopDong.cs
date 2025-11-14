using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
using DAL;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using ClosedXML.Excel;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class FormThemHopDong : Form
    {
        DTOUngVien _ungVien = new DTOUngVien();
        BLLHopDongLaoDong _bllHopDong;
        BLLChucVu _bllChucVu;
        BLLPhongBan _bllPhongBan;
        BLLNhanVien _bllNhanVien;

        string _idNewStaff = "";
        DTOPhongBan _dtoPhongBan;
        DTOChucVu _dtoChucVu;
        string conn = "Data Source=DESKTOP-6LE6PT2\\SQLEXPRESS;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False";
        public FormThemHopDong()
        {
            _ungVien = new DTOUngVien()
            {
                Id = 1,
                TenNhanVien = "Tiền Quang Minh Nhân",
                NgaySinh = new DateTime(2000, 10, 20),
                DiaChi = "TP HCM",
                Que = "Tỉnh",
                GioiTinh = "Nữ",
                Email = "uv1@example.com",
                DuongDanCV = "1.png",
                IdChucVuUngTuyen = 4,
                IdTuyenDung = 9,
                NgayUngTuyen = new DateTime(2025, 11, 9),
                TrangThai = "Thử việc",
                DaXoa = false
            };
            _bllHopDong = new BLLHopDongLaoDong(conn);
            _bllChucVu = new BLLChucVu(conn);
            _bllNhanVien = new BLLNhanVien(conn);
            _bllPhongBan = new BLLPhongBan(conn); 
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void FormThemHopDong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void FormThemHopDong_Load(object sender, EventArgs e)
        {
            //dtpNgayKyHopDong.Enabled = false;
            if (!IsValid(_ungVien))
            {
                MessageBox.Show("Dữ liệu ứng viên không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            else
            {
                PopulateFieldsFromDto(_ungVien);
            }
        }

        private void PopulateFieldsFromDto(DTOUngVien uv)
        {
            if (uv == null) return;

            // Text fields
            txtTenNhanVien.Text = uv.TenNhanVien ?? string.Empty;
            txtDiaChi.Text = uv.DiaChi ?? string.Empty;
            txtQue.Text = uv.Que ?? string.Empty;
            txtEmail.Text = uv.Email ?? string.Empty;
            if (uv.NgaySinh != default(DateTime))
            {
                try
                {
                    dtpNgaySinh.Value = uv.NgaySinh;
                }
                catch
                {
                }
            }

            if (!string.IsNullOrWhiteSpace(uv.GioiTinh))
            {
                var g = uv.GioiTinh.Trim().ToLower();
                if (g == "nam" || g == "n" || g == "male" || g == "m")
                {
                    rdoGioiTinhNam.Checked = true;
                    rdoGioiTinhNu.Checked = false;
                }
                else if (g == "nữ" || g == "nu" || g == "nữ".ToLower() || g == "female" || g == "f")
                {
                    rdoGioiTinhNu.Checked = true;
                    rdoGioiTinhNam.Checked = false;
                }
                else
                {
                    // fallback: try contains
                    if (g.Contains("n"))
                    {
                        rdoGioiTinhNam.Checked = true;
                        rdoGioiTinhNu.Checked = false;
                    }
                    else
                    {
                        rdoGioiTinhNu.Checked = true;
                        rdoGioiTinhNam.Checked = false;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(uv.DuongDanCV))
            {
                try
                {
                    Image fileImage = null;
                    try
                    {
                        fileImage = Image.FromFile(Path.Combine(Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "Image"),
                                                        uv.DuongDanCV));
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi lấy ảnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (fileImage != null)
                    {
                        string fileExtension = "jpg,jpeg,png,bmp,gif";
                        string[] arrFileExtension = fileExtension.Split(',');
                        string[] arr = uv.DuongDanCV.Split('.');
                        if (arr.Length < 1)
                        {
                            if (!arrFileExtension.Contains(arr[1]))
                            {
                                MessageBox.Show("File bạn chọn không đúng định dạng, vui lòng chọn file có đuôi \n .jpg .jpeg .png .bmp .gif", "Thông báo");
                                return;
                            }

                        }
                        ptbAnhNhanVien.Image = fileImage;
                        ptbAnhNhanVien.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                catch
                {
                    MessageBox.Show("Tải ảnh thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            _dtoChucVu = _bllChucVu.FindNameById(uv.IdChucVuUngTuyen);
            _dtoPhongBan = _bllPhongBan.FindPhongBanByIdChucVu(uv.IdChucVuUngTuyen);
            dtpNgayKyHopDong.Value = DateTime.Now.Date;
            txtChucVuLamViec.Text =  _dtoChucVu.TenChucVu?? string.Empty;
            txtphongBanLamViec.Text = _dtoPhongBan.TenPhongBan;
            txtLuongNhanVien.Text = _dtoChucVu.LuongCoBan.ToString();
            txtPhuCapNhanVien.Text = "0";
        }

        public bool IsValid(DTOUngVien uv)
        {
            if (uv == null)
                return false;

            // Kiểm tra string null hoặc rỗng
            if (string.IsNullOrWhiteSpace(uv.TenNhanVien)) return false;
            if (string.IsNullOrWhiteSpace(uv.DiaChi)) return false;
            if (string.IsNullOrWhiteSpace(uv.Que)) return false;
            if (string.IsNullOrWhiteSpace(uv.GioiTinh)) return false;
            if (string.IsNullOrWhiteSpace(uv.Email)) return false;
            if (string.IsNullOrWhiteSpace(uv.DuongDanCV)) return false;
            if (string.IsNullOrWhiteSpace(uv.TrangThai)) return false;

            // Kiểm tra DateTime (không được để giá trị default)
            if (uv.NgaySinh == default(DateTime)) return false;
            if (uv.NgayUngTuyen == default(DateTime)) return false;

            // Kiểm tra số (ID phải > 0)
            if (uv.Id <= 0) return false;
            if (uv.IdChucVuUngTuyen <= 0) return false;
            if (uv.IdTuyenDung <= 0) return false;

            // Nếu tất cả đều hợp lệ thì:
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _idNewStaff = _bllNhanVien.CreateIdStaff(txtChucVuLamViec.Text, txtphongBanLamViec.Text);
                var dto = new DTOHopDongLaoDong
                {
                    LoaiHopDong = cmbLoaiHopDong.SelectedItem.ToString(),
                    NgayKy = dtpNgayKyHopDong.Value.Date,
                    NgayBatDau = dtpNgayBatDauHopDong.Value.Date,
                    NgayKetThuc = dtpNgayKetThucHopDong.Value.Date,
                    Luong = decimal.TryParse(txtLuongNhanVien.Text, out decimal lu) ? lu : 0,
                    IdNhanVien = _idNewStaff,
                    MoTa = txtMoTaHopDong.Text
                };

                DTONhanVien nv = new DTONhanVien
                {
                    ID = _idNewStaff,
                    TenNhanVien = _ungVien.TenNhanVien,
                    NgaySinh = _ungVien.NgaySinh,
                    GioiTinh = _ungVien.GioiTinh,
                    DiaChi = _ungVien.DiaChi,
                    Que = _ungVien.Que,
                    Email = _ungVien.Email,
                    IdChucVu = _ungVien.IdChucVuUngTuyen.ToString(),
                    IdPhongBan = _dtoPhongBan.Id.ToString()
                };

                if (!_bllNhanVien.AddNhanVien(nv, txtChucVuLamViec.Text, txtphongBanLamViec.Text))
                {
                    MessageBox.Show("Thêm nhân viên thất bại!", "Thông báo");
                    return;
                }

                var result = _bllHopDong.Create(dto);
                if (result)
                {
                    MessageBox.Show("Tạo hợp đồng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tạo hợp đồng thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình Tạo hợp đồng và thêm nhân viên mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbLoaiHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loai = cmbLoaiHopDong.SelectedItem?.ToString() ?? "";
            cmbThoiHanHopDong.Items.Clear();
            cmbThoiHanHopDong.SelectedIndex = -1;

            switch (loai)
            {
                case "Hợp đồng thử việc":
                    cmbThoiHanHopDong.Items.Add("1 tháng");
                    cmbThoiHanHopDong.Items.Add("2 tháng");
                    break;
                case "Hợp đồng chính thức":
                    cmbThoiHanHopDong.Items.Add("1 năm");
                    cmbThoiHanHopDong.Items.Add("2 năm");
                    cmbThoiHanHopDong.Items.Add("3 năm");
                    break;
                case "Hợp đồng không thời hạn":
                    break;
            }

            cmbThoiHanHopDong.Enabled = cmbThoiHanHopDong.Items.Count > 0;
            dtpNgayKetThucHopDong.Enabled = false;

            // Nếu có sẵn item và muốn chọn mặc định, có thể uncomment:
            // if (cmbThoiHanHopDong.Enabled) cmbThoiHanHopDong.SelectedIndex = 0;
        }

        private void cmbThoiHanHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEndDateFromSelection();
        }

        private void dtpNgayBatDauHopDong_ValueChanged(object sender, EventArgs e)
        {
            UpdateEndDateFromSelection();
        }

        private void UpdateEndDateFromSelection()
        {
            if (!cmbThoiHanHopDong.Enabled)
            {
                dtpNgayKetThucHopDong.Enabled = false;
                return;
            }

            var sel = cmbThoiHanHopDong.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(sel))
            {
                dtpNgayKetThucHopDong.Enabled = false;
                return;
            }

            DateTime start = dtpNgayBatDauHopDong.Value.Date;
            DateTime end = start;

            // Lấy chữ số từ chuỗi (ví dụ "1 tháng" => 1)
            string digits = new string(sel.Where(char.IsDigit).ToArray());
            if (!int.TryParse(digits, out int n))
            {
                dtpNgayKetThucHopDong.Enabled = false;
                return;
            }

            if (sel.IndexOf("tháng", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                end = start.AddMonths(n);
            }
            else if (sel.IndexOf("năm", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                end = start.AddYears(n);
            }
            else
            {
                dtpNgayKetThucHopDong.Enabled = false;
                return;
            }

            dtpNgayKetThucHopDong.Value = end;
            dtpNgayKetThucHopDong.Enabled = false;
        }

        private void txtLuongNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtLuongNhanVien.Text == "")
            {
                txtLuongNhanVien.Text = "0";
            }
        }

        private void txtPhuCapNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtPhuCapNhanVien.Text == "")
            {
                txtLuongNhanVien.Text = "0";
            }
        }

        private void txtPhuCapNhanVien_TextChanged(object sender, EventArgs e)
        {
            var tb = (Guna2TextBox)sender;
            int selStart = tb.SelectionStart;
            string filtered = new string(tb.Text.Where(char.IsDigit).ToArray());
            if (filtered.Length == 0)
            {
                // Nếu không còn chữ số nào => gán "0"
                tb.Text = "0";
                // đặt con trỏ cuối chuỗi (sau ký tự '0')
                tb.SelectionStart = tb.Text.Length;
                return;
            }
            if (tb.Text != filtered)
            {
                tb.Text = filtered;
                // khôi phục vị trí con trỏ
                tb.SelectionStart = Math.Min(selStart, tb.Text.Length);
            }
            if (txtPhuCapNhanVien.Text.Length > 1 && txtPhuCapNhanVien.Text[0] == '0')
            {
                txtPhuCapNhanVien.Text = txtPhuCapNhanVien.Text.Substring(1, txtPhuCapNhanVien.Text.Length - 1);
                txtPhuCapNhanVien.SelectionStart = txtPhuCapNhanVien.Text.Length;
            }
        }

        private void txtLuongNhanVien_TextChanged(object sender, EventArgs e)
        {
            var tb = (Guna2TextBox)sender;
            int selStart = tb.SelectionStart;
            string filtered = new string(tb.Text.Where(char.IsDigit).ToArray());
            if (filtered.Length == 0)
            {
                // Nếu không còn chữ số nào => gán "0"
                tb.Text = "0";
                // đặt con trỏ cuối chuỗi (sau ký tự '0')
                tb.SelectionStart = tb.Text.Length;
                return;
            }
            if (tb.Text != filtered)
            {
                tb.Text = filtered;
                // khôi phục vị trí con trỏ
                tb.SelectionStart = Math.Min(selStart, tb.Text.Length);
            }
            if(txtLuongNhanVien.Text.Length > 1 && txtLuongNhanVien.Text[0] == '0')
            {
                txtLuongNhanVien.Text = txtLuongNhanVien.Text.Substring(1, txtLuongNhanVien.Text.Length - 1);
                txtLuongNhanVien.SelectionStart = txtLuongNhanVien.Text.Length;
            }
        }
    }
}
