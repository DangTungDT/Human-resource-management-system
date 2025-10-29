using BLL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCTuyenDung_TruongPhong : UserControl
    {
        private int countUpdateStatus { get; set; } = 0;
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
            dgvDsUngVienTuyen.DataSource = LoadDuLieuUngVienDuocTuyen();

            if (dgvTPTuyenDung.Columns["ID"] != null && dgvDsUngVienTuyen.Columns["ID"] != null)
            {
                dgvTPTuyenDung.Columns["ID"].Visible = false;
                dgvDsUngVienTuyen.Columns["ID"].Visible = false;

                dgvTPTuyenDung.Columns["TieuDe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dgvTPTuyenDung.Columns["TieuDe"].HeaderText = "Tiêu đề";
                dgvTPTuyenDung.Columns["PhongBan"].HeaderText = "Phòng ban";
                dgvTPTuyenDung.Columns["ChucVu"].HeaderText = "Chức vụ";
                dgvTPTuyenDung.Columns["NguoiTao"].HeaderText = "Người tạo";
                dgvTPTuyenDung.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvTPTuyenDung.Columns["SoLuong"].HeaderText = "Số lượng tuyển";

                dgvDsUngVienTuyen.Columns["TenUngVien"].HeaderText = "Họ tên";
                dgvDsUngVienTuyen.Columns["TrangThai"].HeaderText = "Trạng thái";
            }

            var idPhongBan = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).idPhongBan;
            txtPhongBan.Text = _dbContextPB.TimTenPhongBan(idPhongBan);

            txtChucVu.Text = "Nhân viên";

            txtTieuDe.Text = "Tuyển dụng nhân viên cho phòng " + txtPhongBan.Text.ToLowerInvariant();

            txtNguoiTao.Text = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).TenNhanVien;

            txtTrangThai.Text = "Yêu cầu tuyển dụng";
        }

        // Hien thi du lieu len textbox tu datagridview
        private void dgvTPTuyenDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                txtSoLuong.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["SoLuong"].Value?.ToString();
            }
        }

        // button Load du lieu
        private void btnLoadDuLieu_Click(object sender, EventArgs e)
        {
            dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
            dgvDsUngVienTuyen.DataSource = LoadDuLieuUngVienDuocTuyen();
        }

        // Them tuyen dung
        private void btnThemTD_Click(object sender, EventArgs e)
        {
            try
            {
                var nhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien);
                var tuyenDung = _dbContextTD.KtraTuyenDungQuaIDNV(_idNhanVien);

                if (string.IsNullOrWhiteSpace(txtSoLuong.Text))
                {
                    MessageBox.Show($"Trưởng phòng cần thêm số lượng tuyển ứng viên !");
                    return;
                }

                if (nhanVien != null)
                {
                    if (tuyenDung)
                    {
                        if (_dbContextTD.KtraThemTuyenDung(new DTOTuyenDung(0, Convert.ToInt32(nhanVien.idPhongBan), Convert.ToInt32(nhanVien.idChucVu),
                                    txtTieuDe.Text, nhanVien.id, txtTrangThai.Text, DateTime.Now, Convert.ToInt32(txtSoLuong.Text))))
                        {
                            MessageBox.Show($"Thêm tuyển dụng thành công.");

                            dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
                            EmptyQty(txtSoLuong.Text);
                        }
                    }
                    else MessageBox.Show($"Trưởng phòng đã thêm tuyển dụng !");
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

                if (string.IsNullOrWhiteSpace(txtSoLuong.Text))
                {
                    MessageBox.Show($"Trưởng phòng cần cập nhật số lượng tuyển ứng viên !");
                    return;
                }

                if (nhanVien != null)
                {
                    if (!tuyenDung)
                    {
                        if (_dbContextTD.KtraCapNhatTuyenDung(new DTOTuyenDung(idTuyenDung, Convert.ToInt32(nhanVien.idPhongBan), Convert.ToInt32(nhanVien.idChucVu),
                                    txtTieuDe.Text, nhanVien.id, txtTrangThai.Text, DateTime.Now, Convert.ToInt32(txtSoLuong.Text))))
                        {
                            MessageBox.Show($"Cập nhật tuyển dụng thành công.");

                            dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
                            EmptyQty(txtSoLuong.Text);
                        }
                    }
                    else MessageBox.Show($"Trưởng phòng đã thêm tuyển dụng !");
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
            HamXoaChung();
            countUpdateStatus = 0;
        }

        public void HamXoaChung()
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
                    if (!tuyenDung)
                    {
                        if (_dbContextTD.KtraXoaTuyenDung(new DTOTuyenDung(idTuyenDung)))
                        {
                            MessageBox.Show($"Xóa tuyển dụng thành công.");

                            dgvTPTuyenDung.DataSource = LoadDuLieuTuyenDung();
                            EmptyQty(txtSoLuong.Text);
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
        private object LoadDuLieuUngVienDuocTuyen()
        {
            var anonymous = new object();

            try
            {
                var tuyenDung = _dbContextTD.KtraDsTuyenDung().FirstOrDefault(p => p.idNguoiTao == _idNhanVien);
                var ungVien = _dbContextUV.GetAll().Where(p => tuyenDung.id == p.idTuyenDung && p.trangThai.Equals("Thử việc", StringComparison.OrdinalIgnoreCase)).ToList();

                anonymous = ungVien.Select(p => new
                {
                    ID = p.id,
                    TenUngVien = p.tenNhanVien,
                    TrangThai = p.trangThai

                }).ToList();

                lblSoLuongUVTuyen.Text = string.Empty;
                lblSoLuongUVTuyen.Text = $"Số lượng tuyển ứng viên: {ungVien.Count}";

                //if (ungVien.Count == tuyenDung.soLuong)
                //{

                //    var idNhanVien = _dbContextNV.KtraNhanVienQuaID(_idNhanVien).id;
                //    var idTuyenDung = _dbContextTD.TImTuyenDungQuaIDNV(idNhanVien).id;

                //    if (idTuyenDung > 0)
                //    {
                //        if (_dbContextTD.KtraCapNhatTrangThaiRD(new DTOTuyenDung(idTuyenDung, "Ngừng ứng tuyển", DateTime.Now)))
                //        {
                //            MessageBox.Show($"Cập nhật trạng thái tuyển dụng thành công.");

                //            dgvTPTuyenDung.DataSource = anonymous;
                //            EmptyQty(txtSoLuong.Text);

                //        }
                //    }
                //    else MessageBox.Show($"Không tìm thấy tuyển dụng !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

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
            var anonymous = new object();
            var tuyenDung = _dbContextTD.KtraDsTuyenDung().Where(p => p.idNguoiTao == _idNhanVien);

            try
            {
                anonymous = tuyenDung.OrderByDescending(p => p.id).Select(p => new
                {
                    ID = p.id,
                    TieuDe = p.tieuDe,
                    PhongBan = _dbContextPB.TimTenPhongBan(p.idPhongBan),
                    ChucVu = string.Join(" ", _dbContextCV.TimTenChucVu(p.idChucVu).Split(' ').Take(2)),
                    NguoiTao = _dbContextNV.KtraNhanVienQuaID(p.idNguoiTao).TenNhanVien,
                    TrangThai = p.trangThai,
                    SoLuong = p.soLuong,

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
    }
}
