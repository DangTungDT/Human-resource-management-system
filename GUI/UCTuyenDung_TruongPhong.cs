using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCTuyenDung_TruongPhong : UserControl
    {
        private int _idSelected { get; set; } = 0;
        private readonly string _idNhanVien, _con;

        private readonly BLLChucVu _dbContextCV;
        private readonly BLLUngVien _dbContextUV;
        private readonly BLLNhanVien _dbContextNV;
        private readonly BLLPhongBan _dbContextPB;
        private readonly BLLTuyenDung _dbContextTD;


        public UCTuyenDung_TruongPhong(string idNhanVien, string con)
        {
            InitializeComponent();

            _con = con;
            _idNhanVien = idNhanVien;
            _dbContextCV = new BLLChucVu(con);
            _dbContextUV = new BLLUngVien(con);
            _dbContextNV = new BLLNhanVien(con);
            _dbContextTD = new BLLTuyenDung(con);
            _dbContextPB = new BLLPhongBan(con);
        }

        private void UCTuyenDung_TruongPhong_Load(object sender, EventArgs e)
        {
            dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
            dgvDsUngVienTuyen.DataSource = LoadDuLieuUngVienDuocTuyen(0);
            ChinhSuaGiaoDien();

            var idPhongBan = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).idPhongBan;
            txtPhongBan.Text = _dbContextPB.TimTenPhongBan(idPhongBan);

            txtChucVu.Text = "Nhân viên";

            txtTieuDe.Text = "Tuyển dụng nhân viên phòng " + txtPhongBan.Text.ToLowerInvariant();

            txtNguoiTao.Text = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).TenNhanVien;

            txtTrangThai.Text = "Chờ duyệt";

            var tuyenDung = _dbContextTD.KtraDsTuyenDung().Where(p => p.idNguoiTao == _idNhanVien).ToList();
            var ungVien = _dbContextUV.GetAll().Where(p => tuyenDung.Select(s => s.id).FirstOrDefault() == p.idTuyenDung && p.trangThai.Equals("Thử việc", StringComparison.OrdinalIgnoreCase)).ToList();
            var soLuongTD = tuyenDung.Select(s => s.soLuong).FirstOrDefault();

            if (_idNhanVien.Contains("GD"))
            {
                btnThemTD.Enabled = false;
                btnCapNhatTD.Enabled = false;
                btnXoaLuong.Enabled = false;
            }
        }

        // Hien thi du lieu len textbox tu datagridview
        private void dgvTPTuyenDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e != null && e.RowIndex > -1)
            {
                _idSelected = Convert.ToInt32(dgvTPTuyenDung.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                txtTieuDe.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["TieuDe"].Value?.ToString();
                txtSoLuong.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["SoLuong"].Value?.ToString();
                txtTrangThai.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString();
                rtGhiChu.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["GhiChu"].Value?.ToString();

                string[] arrType = { "Đang tuyển", "Ngừng tuyển", "Loại" };
                var tuyenDung = _dbContextTD.KtraDsTuyenDung().FirstOrDefault(p => p.idNguoiTao == _idNhanVien && p.id == _idSelected && arrType.Contains(p.trangThai));

                if (_idNhanVien.Contains("GD"))
                {
                    btnThemTD.Enabled = false;
                    btnCapNhatTD.Enabled = false;
                    btnXoaLuong.Enabled = false;
                }
                else
                {
                    if (tuyenDung != null)
                    {
                        EnableAllField(true);
                    }
                    else EnableAllField(false);
                }

                if (_idSelected > 0)
                {
                    dgvDsUngVienTuyen.DataSource = LoadDuLieuUngVienDuocTuyen(_idSelected);
                    ChinhSuaGiaoDien();
                }
            }
        }

        public void EnableAllField(bool check)
        {
            foreach (var control in grbTPTuyenDung.Controls)
            {
                if (control is Guna2TextBox text && !string.IsNullOrEmpty(text.Text))
                {
                    if (check)
                    {
                        text.ReadOnly = true;
                        rtGhiChu.ReadOnly = true;
                        btnCapNhatTD.Enabled = false;
                        btnXoaLuong.Enabled = false;
                    }
                    else
                    {
                        text.ReadOnly = true;
                        rtGhiChu.ReadOnly = false;
                        txtSoLuong.ReadOnly = false;
                        btnCapNhatTD.Enabled = true;
                        btnXoaLuong.Enabled = true;
                    }
                }
            }
        }


        // button Load du lieu
        private void btnLoadDuLieu_Click(object sender, EventArgs e)
        {
            _idSelected = 0;
            dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
            dgvDsUngVienTuyen.DataSource = LoadDuLieuUngVienDuocTuyen(0);
            rtGhiChu.ReadOnly = false;
            ChinhSuaGiaoDien();
        }

        // Them tuyen dung
        private void btnThemTD_Click(object sender, EventArgs e)
        {
            try
            {
                var nhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien);
                var tuyenDung = _dbContextTD.TimTuyenDungQuatrangThai(_idNhanVien);

                if (nhanVien != null)
                {
                    if (DisplayUserControlPanel.KiemTraDuLieuDauVao(error, grbTPTuyenDung) && KtraGhiChu())
                    {
                        if (tuyenDung)
                        {
                            if (MessageBox.Show($"Bạn có chắc chắn về thêm dữ liệu này ?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                if (_dbContextTD.KtraThemTuyenDung(new DTOTuyenDung(0, Convert.ToInt32(nhanVien.idPhongBan), Convert.ToInt32(nhanVien.idChucVu),
                                        txtTieuDe.Text, nhanVien.id, "Chờ duyệt", DateTime.Now, Convert.ToInt32(txtSoLuong.Text), rtGhiChu.Text)))
                                {
                                    MessageBox.Show($"Thêm tuyển dụng thành công.");

                                    dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
                                    EmptyQty(txtSoLuong.Text);
                                }
                            }
                            else return;
                        }
                        else MessageBox.Show($"Đẵ có yêu cầu tuyển dụng từ trưởng phòng {nhanVien.TenNhanVien} !", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else MessageBox.Show($"Nhân viên không tồn tại !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm tuyển dụng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Cap nhat tuyen dung
        private void btnCapNhatTD_Click(object sender, EventArgs e)
        {
            try
            {
                var nhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien);
                var tuyenDung = _dbContextTD.KtraTuyenDungQuaIDNV(_idNhanVien);
                var idTuyenDung = Convert.ToInt32(dgvTPTuyenDung.CurrentRow.Cells["ID"].Value.ToString());

                if (idTuyenDung < 1)
                {
                    MessageBox.Show($"Không hiển thị được dữ liệu của tuyển dụng này !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nhanVien != null)
                {
                    if (DisplayUserControlPanel.KiemTraDuLieuDauVao(error, grbTPTuyenDung))
                    {
                        if (!tuyenDung)
                        {
                            if (_dbContextTD.KtraCapNhatTuyenDung(new DTOTuyenDung(idTuyenDung, Convert.ToInt32(nhanVien.idPhongBan), Convert.ToInt32(nhanVien.idChucVu),
                                        txtTieuDe.Text, nhanVien.id, txtTrangThai.Text, DateTime.Now, Convert.ToInt32(txtSoLuong.Text), rtGhiChu.Text)))
                            {
                                MessageBox.Show($"Cập nhật tuyển dụng thành công.");
                                CapNhatChung();
                            }
                        }
                        else MessageBox.Show($"Trưởng phòng đã thêm tuyển dụng !");
                    }
                }
                else MessageBox.Show($"Nhân viên không tồn tại !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật tuyển dụng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Xoa tuyen dung
        private void btnXoaLuong_Click(object sender, EventArgs e)
        {
            try
            {
                var nhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien);
                var tuyenDung = _dbContextTD.KtraTuyenDungQuaIDNV(_idNhanVien);

                if (_idSelected < 1)
                {
                    MessageBox.Show($"Bạn cần chọn yêu cầu cần xóa !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nhanVien != null)
                {
                    if (!tuyenDung)
                    {
                        if (MessageBox.Show($"Bạn có chắc chắn muốn xóa yêu cầu đã chọn không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (_dbContextTD.KtraXoaTuyenDung(new DTOTuyenDung(_idSelected)))
                            {
                                MessageBox.Show($"Xóa tuyển dụng thành công.");
                                CapNhatChung();
                            }
                        }
                    }
                    else MessageBox.Show($"Trưởng phòng đã thêm tuyển dụng !");
                }
                else MessageBox.Show($"Nhân viên không tồn tại !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa tuyển dụng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // chi nhap ki tu so
        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                error.Clear();

                DisplayUserControlPanel.LayKiTuSo(sender);
                sender = txtSoLuong.MaxLength = 2;
                if (int.TryParse(txtSoLuong.Text, out int qty) && qty < 11)
                {
                    if (qty == 0)
                    {
                        txtSoLuong.Clear();
                        error.SetError(txtSoLuong, "Số lượng tuyển dụng không được bằng 0 !");
                    }
                    else txtSoLuong.Text = qty.ToString();
                }
                else
                {
                    txtSoLuong.Clear();
                    error.SetError(txtSoLuong, "Số lượng tuyển dụng không được vượt quá 10 người !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị số lượng !" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Ham load ung vien duoc tuyen
        private object LoadDuLieuUngVienDuocTuyen(int? id)
        {

            try
            {
                var anonymous = new object();
                var tuyenDung = _dbContextTD.KtraDsTuyenDung().LastOrDefault(p => p.idNguoiTao == _idNhanVien);
                //if (tuyenDung == null) return null;
                //var idTuyenDung = id > 0 ? id : tuyenDung.id;
                var idTuyenDung = _idSelected;

                var ungVien = _dbContextUV.GetAll().Where(p => idTuyenDung == p.idTuyenDung && p.trangThai.Equals("Thử việc", StringComparison.OrdinalIgnoreCase)).ToList();

                anonymous = ungVien.Select(p => new
                {
                    ID = p.id,
                    TenUngVien = p.tenNhanVien,
                    TrangThai = p.trangThai,
                    NgayThuViec = p.ngayUngTuyen

                }).ToList();

                lblSoLuongUVTuyen.Text = string.Empty;
                lblSoLuongUVTuyen.Text = $"Số lượng tuyển ứng viên: {ungVien.Count}";

                return anonymous;
            }
            catch
            {
                MessageBox.Show($"Lôi load dữ liệu trưởng phòng tuyển dụng !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Ham Load du lieu chung
        private object LoadDuLieuTuyenDung()
        {
            try
            {
                _idSelected = 0;
                var anonymous = new object();

                var tuyenDungs = _dbContextTD.KtraDsTuyenDung().Where(p => p.idNguoiTao == _idNhanVien).ToList();
                var idTuyenDung = tuyenDungs.Select(s => s.id).ToList();
                var ungVien = _dbContextUV.GetAll().Where(p => idTuyenDung.Contains(p.idTuyenDung) && p.trangThai.Equals("Thử việc", StringComparison.OrdinalIgnoreCase)).ToList();
                var soLuongTD = tuyenDungs.Select(s => s.soLuong).FirstOrDefault();

                string setTrangThai = "Chờ duyệt";

                foreach (var tuyenDung in tuyenDungs)
                {
                    if (ungVien.Where(p => p.idTuyenDung == tuyenDung.id).ToList().Count == tuyenDung.soLuong && !tuyenDung.trangThai.Equals("Ngừng tuyển", StringComparison.OrdinalIgnoreCase))
                    {
                        setTrangThai = "Ngừng tuyển";
                        if (_dbContextTD.KtraCapNhatTrangThaiTD(new DTOTuyenDung(tuyenDung.id, setTrangThai, DateTime.Now)))
                        {
                            EmptyQty(txtSoLuong.Text);
                            rtGhiChu.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show($"Cập nhật tuyển dụng thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                    }
                }

                var dsTPTuyenDung = _dbContextTD.KtraDsTuyenDung().Where(p => p.idNguoiTao == _idNhanVien);
                if (_idNhanVien.Contains("GD"))
                {
                    dsTPTuyenDung = _dbContextTD.KtraDsTuyenDung();
                }
                anonymous = dsTPTuyenDung.OrderByDescending(p => p.id).Select(p => new
                {
                    ID = p.id,
                    TieuDe = p.tieuDe,
                    PhongBan = _dbContextPB.TimTenPhongBan(p.idPhongBan),
                    TrangThai = p.trangThai,
                    ChucVu = "Nhân viên",
                    SoLuong = p.soLuong,
                    NguoiTao = _dbContextNV.KtraNhanVienQuaID(p.idNguoiTao).TenNhanVien,
                    GhiChu = p.ghiChu,
                    NgayTao = DateTime.Parse(p.ngayTao.ToString()).ToString("dd/MM/yyyy HH:mm"),

                }).ToList();

                return anonymous;
            }
            catch
            {
                MessageBox.Show($"Lôi load dữ liệu trưởng phòng tuyển dụng !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        // Hien thi ds ung vien da tuyen dung
        private void dgvDsUngVienTuyen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {

            }
        }

        // Lam trong field So luong
        private void EmptyQty(string qty) => qty = string.Empty;

        // Chinh sua giao dien 
        private void ChinhSuaGiaoDien()
        {
            if (dgvTPTuyenDung.Columns["ID"] != null && dgvDsUngVienTuyen.Columns["ID"] != null)
            {
                dgvTPTuyenDung.Columns["ID"].Visible = false;
                dgvDsUngVienTuyen.Columns["ID"].Visible = false;

                dgvTPTuyenDung.Columns["TieuDe"].HeaderText = "Tiêu đề";
                dgvTPTuyenDung.Columns["PhongBan"].HeaderText = "Phòng ban";
                dgvTPTuyenDung.Columns["ChucVu"].HeaderText = "Chức vụ";
                dgvTPTuyenDung.Columns["NguoiTao"].HeaderText = "Người tạo";
                dgvTPTuyenDung.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvTPTuyenDung.Columns["SoLuong"].HeaderText = "Số lượng tuyển";
                dgvTPTuyenDung.Columns["GhiChu"].HeaderText = "Yêu cầu";
                dgvTPTuyenDung.Columns["NgayTao"].HeaderText = "Ngày tuyển dụng";

                dgvDsUngVienTuyen.Columns["TenUngVien"].HeaderText = "Họ tên";
                dgvDsUngVienTuyen.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvDsUngVienTuyen.Columns["NgayThuViec"].HeaderText = "Ngày thử việc";

                dgvTPTuyenDung.Columns["TieuDe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvTPTuyenDung.Columns["GhiChu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dgvDsUngVienTuyen.Columns["NgayThuViec"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        // Cap nhat chung ho load, giao dien, lam trong du lieu
        private void CapNhatChung()
        {
            dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
            dgvDsUngVienTuyen.DataSource = LoadDuLieuUngVienDuocTuyen(0);
            ChinhSuaGiaoDien();

            txtTieuDe.ReadOnly = false;
            txtTrangThai.ReadOnly = false;
            txtTrangThai.Clear();
            txtSoLuong.Clear();
            rtGhiChu.Clear();
        }

        // Kiem tra du lieu dau vao
        private bool KtraGhiChu()
        {
            error.Clear();
            bool ktra = true;

            if (string.IsNullOrEmpty(rtGhiChu.Text))
            {
                error.SetError(rtGhiChu, "Trống !");
                ktra = false;
            }

            if (rtGhiChu.Text.Length > 255)
            {
                error.SetError(rtGhiChu, "Ghi chú quá dài, độ dài giới hạn ghi chú chỉ 255 ký tự !");
                ktra = false;
            }

            return ktra;
        }
    }
}
