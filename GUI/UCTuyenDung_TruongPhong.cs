using BLL;
using DAL;
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
        private string _idNguoiTao { get; set; }

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

            if (_idNhanVien.Contains("GD"))
            {
                ckbDsTDTP.Visible = true;

                if (ckbDsTDTP.Checked)
                {
                    dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung(true);
                }
                else dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
            }
            else ckbDsTDTP.Visible = false;

            var idPhongBan = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).idPhongBan;
            txtPhongBan.Text = _dbContextPB.TimTenPhongBan(idPhongBan);

            txtChucVu.Text = "Nhân viên";

            txtTieuDe.Text = "Tuyển dụng nhân viên phòng " + txtPhongBan.Text.ToLowerInvariant();

            txtNguoiTao.Text = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).TenNhanVien;

            txtTrangThai.Text = "Chờ duyệt";

            var tuyenDung = _dbContextTD.KtraDsTuyenDung().Where(p => p.idNguoiTao == _idNhanVien).ToList();
            var ungVien = _dbContextUV.LayDsUngVien().Where(p => tuyenDung.Select(s => s.id).FirstOrDefault() == p.idTuyenDung && p.trangThai.Equals("Thử việc", StringComparison.OrdinalIgnoreCase)).ToList();
            var soLuongTD = tuyenDung.Select(s => s.soLuong).FirstOrDefault();

            //if (_idNhanVien.Contains("GD"))
            //{
            //    btnThemTD.Enabled = false;
            //    btnCapNhatTD.Enabled = false;
            //    btnXoaTuyenDung.Enabled = false;
            //}
        }

        // Hien thi du lieu len textbox tu datagridview
        private void dgvTPTuyenDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e != null && e.RowIndex > -1)
            {
                _idNguoiTao = dgvTPTuyenDung.Rows[e.RowIndex].Cells["IDNguoiTao"].Value?.ToString();
                _idSelected = Convert.ToInt32(dgvTPTuyenDung.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                txtTieuDe.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["TieuDe"].Value?.ToString();
                txtSoLuong.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["SoLuong"].Value?.ToString();
                txtTrangThai.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString();
                rtGhiChu.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["GhiChu"].Value?.ToString();

                string[] arrType = { "Đang tuyển", "Ngừng tuyển", "GD ngừng tuyển", "Loại" };
                var tuyenDung = _dbContextTD.KtraDsTuyenDung().FirstOrDefault(p => p.id == _idSelected && arrType.Contains(p.trangThai)); //p.idNguoiTao == _idNhanVien && 

                //if (_idNhanVien.Contains("GD"))
                //{
                //    btnThemTD.Enabled = false;
                //    btnCapNhatTD.Enabled = false;
                //    btnXoaTuyenDung.Enabled = false;
                //}
                //else
                {
                    if (tuyenDung != null)
                    {
                        EnableAllField(true);
                        btnXoaTuyenDung.Text = "Ngừng tuyển dụng";

                        if (tuyenDung.trangThai.Equals("Ngừng tuyển", StringComparison.OrdinalIgnoreCase) || tuyenDung.trangThai.Equals("GD ngừng tuyển", StringComparison.OrdinalIgnoreCase))
                        {
                            btnXoaTuyenDung.Enabled = false;
                        }
                        else btnXoaTuyenDung.Enabled = true;
                    }
                    else
                    {
                        EnableAllField(false);
                        btnXoaTuyenDung.Text = "Xóa tuyển dụng";
                        btnXoaTuyenDung.Enabled = true;
                    }

                    if (_idSelected > 0)
                    {
                        dgvDsUngVienTuyen.DataSource = LoadDuLieuUngVienDuocTuyen(_idSelected);
                        ChinhSuaGiaoDien();
                    }
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
                        //btnXoaTuyenDung.Enabled = false;
                    }
                    else
                    {
                        text.ReadOnly = true;
                        rtGhiChu.ReadOnly = false;
                        txtSoLuong.ReadOnly = false;
                        btnCapNhatTD.Enabled = true;
                        //btnXoaTuyenDung.Enabled = true;
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
            txtSoLuong.ReadOnly = false;
            btnCapNhatTD.Enabled = true;
            btnXoaTuyenDung.Enabled = true;
            ckbDsTDTP.Checked = false;
            btnXoaTuyenDung.Text = "Xóa tuyển dụng";
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
                        string trangThai = _idNhanVien.Contains("GD") ? "Đang tuyển" : "Chờ duyệt";
                        string chucVu = _idNhanVien.Contains("GD") ? "giám đốc" : "trưởng phòng";

                        if (tuyenDung)
                        {
                            if (MessageBox.Show($"Bạn có chắc chắn về thêm dữ liệu này ?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                if (_dbContextTD.KtraThemTuyenDung(new DTOTuyenDung(0, Convert.ToInt32(nhanVien.idPhongBan), Convert.ToInt32(nhanVien.idChucVu),
                                        txtTieuDe.Text, nhanVien.id, trangThai, DateTime.Now, Convert.ToInt32(txtSoLuong.Text), rtGhiChu.Text)))
                                {
                                    MessageBox.Show($"Thêm tuyển dụng thành công.");

                                    dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
                                    EmptyQty(txtSoLuong.Text);
                                }
                            }
                            else return;
                        }
                        else MessageBox.Show($"Đẵ có yêu cầu tuyển dụng từ {chucVu} {nhanVien.TenNhanVien} !", "Thông báo", MessageBoxButtons.OK);
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
                        string type = btnXoaTuyenDung.Text.Contains("Ngừng") ? "ngừng" : "xóa";

                        if (MessageBox.Show($"Bạn có chắc chắn muốn {type} yêu cầu đã chọn không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (type == "xóa")
                            {
                                if (_dbContextTD.KtraXoaTuyenDung(new DTOTuyenDung(_idSelected)))
                                {
                                    MessageBox.Show($"Xóa tuyển dụng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CapNhatChung();
                                }
                                else MessageBox.Show("Xóa tuyển dụng thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                var nguoiNgungTuyen = !_idNhanVien.Contains("GD") ? "Ngừng tuyển" : "GD ngừng tuyển";
                                if (_dbContextTD.KtraCapNhatTrangThaiTD(new DTOTuyenDung(_idSelected, nguoiNgungTuyen, DateTime.Now)))
                                {
                                    MessageBox.Show($"Đã ngừng tuyển dụng cho tiêu đề: {_dbContextTD.KtraTuyenDungQuaID(_idSelected).tieuDe}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CapNhatChung();
                                }
                                else MessageBox.Show("Ngừng tuyển dụng thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //error.SetError(txtSoLuongDuyet, "Số lượng tuyển dụng không được vượt quá 10 người !");
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

                var ungVien = _dbContextUV.LayDsUngVien().Where(p => idTuyenDung == p.idTuyenDung && p.trangThai.Equals("Thử việc", StringComparison.OrdinalIgnoreCase)).ToList();

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
                MessageBox.Show($"Lỗi load dữ liệu trưởng phòng tuyển dụng !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Ham Load du lieu chung
        private object LoadDuLieuTuyenDung(bool checkTP = false)
        {
            try
            {
                _idSelected = 0;
                var anonymous = new object();

                var tuyenDungs = _dbContextTD.KtraDsTuyenDung().Where(p => p.trangThai.Equals("Đang tuyển", StringComparison.OrdinalIgnoreCase)).ToList(); //p.idNguoiTao == _idNguoiTao &&
                var idTuyenDung = tuyenDungs.Select(s => s.id).ToList();
                var ungVien = _dbContextUV.LayDsUngVien().Where(p => idTuyenDung.Contains(p.idTuyenDung) && p.trangThai.Equals("Thử việc", StringComparison.OrdinalIgnoreCase)).ToList();
                var soLuongTD = tuyenDungs.Select(s => s.soLuong).FirstOrDefault();

                string setTrangThai = "Chờ duyệt";

                foreach (var tuyenDung in tuyenDungs)
                {
                    if (ungVien.Where(p => p.idTuyenDung == tuyenDung.id).ToList().Count == tuyenDung.soLuong && tuyenDung.trangThai.Equals("Đang tuyển", StringComparison.OrdinalIgnoreCase))
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
                    if (checkTP)
                    {
                        dsTPTuyenDung = _dbContextTD.KtraDsTuyenDung().Where(p => !p.idNguoiTao.StartsWith("GD")).ToList();
                    }
                }
                anonymous = dsTPTuyenDung.OrderBy(p => p.ngayTao).Select(p => new
                {
                    ID = p.id,
                    IDNguoitao = p.idNguoiTao,
                    TieuDe = p.tieuDe,
                    TrangThai = p.trangThai,
                    ChucVu = "Nhân viên",
                    NguoiTao = _dbContextNV.KtraNhanVienQuaID(p.idNguoiTao).TenNhanVien,
                    GhiChu = p.ghiChu,
                    SoLuong = p.soLuong,
                    NgayTao = DateTime.Parse(p.ngayTao.ToString()).ToString("dd/MM/yyyy HH:mm"),
                    PhanHoi = p.ghiChuDuyet,
                    SoLuongDuyet = p.soLuongDuyet,

                }).OrderByDescending(p => p.ID).ToList();

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
                dgvTPTuyenDung.Columns["IDNguoiTao"].Visible = false;

                dgvDsUngVienTuyen.Columns["ID"].Visible = false;

                dgvTPTuyenDung.Columns["TieuDe"].HeaderText = "Tiêu đề";
                dgvTPTuyenDung.Columns["ChucVu"].HeaderText = "Tuyển chức vụ";
                dgvTPTuyenDung.Columns["NguoiTao"].HeaderText = "Người tạo";
                dgvTPTuyenDung.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvTPTuyenDung.Columns["SoLuong"].HeaderText = "Số lượng tuyển";
                dgvTPTuyenDung.Columns["GhiChu"].HeaderText = "Yêu cầu";
                dgvTPTuyenDung.Columns["NgayTao"].HeaderText = "Ngày tuyển dụng";
                dgvTPTuyenDung.Columns["PhanHoi"].HeaderText = "Phản hồi";
                dgvTPTuyenDung.Columns["SoLuongDuyet"].HeaderText = "Sô lượng duyệt";

                dgvDsUngVienTuyen.Columns["TenUngVien"].HeaderText = "Họ tên";
                dgvDsUngVienTuyen.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvDsUngVienTuyen.Columns["NgayThuViec"].HeaderText = "Ngày thử việc";

                dgvTPTuyenDung.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTPTuyenDung.Columns["SoLuongDuyet"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvTPTuyenDung.Columns["TieuDe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvTPTuyenDung.Columns["GhiChu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvTPTuyenDung.Columns["PhanHoi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

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
            //txtTrangThai.Clear();
            txtSoLuong.Clear();
            rtGhiChu.Clear();

            if (_idNhanVien.Contains("GD"))
            {
                ckbDsTDTP.Visible = true;

                if (ckbDsTDTP.Checked)
                {
                    dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung(true);
                }
                else dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
            }
            else ckbDsTDTP.Visible = false;
        }

        private void ckbDsTDTP_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDsTDTP.Checked)
            {
                dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung(true);
            }
            else dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
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
