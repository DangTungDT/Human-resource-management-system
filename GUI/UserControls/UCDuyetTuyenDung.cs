using BLL;
using DAL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCDuyetTuyenDung : UserControl
    {
        private string _idSelected { get; set; }
        private readonly string _idNhanVien, _con;

        private readonly BLLChucVu _dbContextCV;
        private readonly BLLUngVien _dbContextUV;
        private readonly BLLNhanVien _dbContextNV;
        private readonly BLLPhongBan _dbContextPB;
        private readonly BLLTuyenDung _dbContextTD;

        public UCDuyetTuyenDung(string idNhanVien, string con)
        {
            InitializeComponent();

            _con = con;
            _idNhanVien = idNhanVien;
            _dbContextCV = new BLLChucVu(con);
            _dbContextUV = new BLLUngVien(con);
            _dbContextNV = new BLLNhanVien(con);
            _dbContextPB = new BLLPhongBan(con);
            _dbContextTD = new BLLTuyenDung(con);
        }

        private void UCDuyetTuyenDung_Load(object sender, EventArgs e)
        {
            dgvTPTuyenDung.DataSource = LoadDuLieu();
            ChinhSuaGiaoDien();
            btnDuyet.Enabled = false;
            btnKhongDuyet.Enabled = false;
        }

        private void dgvTPTuyenDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                var idTPNS = dgvTPTuyenDung.Rows[e.RowIndex].Cells["IDNhanVien"].Value?.ToString();

                _idSelected = dgvTPTuyenDung.Rows[e.RowIndex].Cells["ID"].Value?.ToString();
                rtGhiChu.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["GhiChu"].Value?.ToString();
                txtTieuDe.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["TieuDe"].Value?.ToString();
                txtSoLuongDuyet.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["SoLuong"].Value?.ToString();
                txtNguoiTao.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["NguoiTao"].Value?.ToString();
                txtTrangThai.Text = dgvTPTuyenDung.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString();

                if (idTPNS.Contains("TPNS") && _idNhanVien == idTPNS)
                {
                    btnDuyet.Enabled = false;
                    btnKhongDuyet.Enabled = false;
                }
                else
                {
                    btnDuyet.Enabled = true;
                    btnKhongDuyet.Enabled = true;
                }
            }
        }

        // Load du lieu dgv
        private object LoadDuLieu()
        {
            _idSelected = null;
            btnDuyet.Enabled = false;
            btnKhongDuyet.Enabled = false;

            return _dbContextTD.KtraDsTuyenDung().Where(p => p.trangThai.Equals("Chờ duyệt", StringComparison.OrdinalIgnoreCase)).Select(p => new
            {
                ID = p.id,
                IDNhanVien = p.idNguoiTao,
                TieuDe = p.tieuDe,
                NguoiTao = _dbContextNV.KtraNhanVienQuaID(p.idNguoiTao).TenNhanVien,
                ChucVu = _dbContextCV.TimTenChucVu(p.idChucVu),
                TrangThai = p.trangThai,
                SoLuong = p.soLuong,
                GhiChu = p.ghiChu

            }).ToList();
        }

        // Load du lieu dgv
        private void btnLoad_Click(object sender, EventArgs e) => dgvTPTuyenDung.DataSource = LoadDuLieu();

        // Xu ly button duyet
        private void btnDuyet_Click(object sender, EventArgs e) => XuLyButtonTrangThaiTD(true);

        // Xu ly button khong duyet
        private void btnKhongDuyet_Click(object sender, EventArgs e) => XuLyButtonTrangThaiTD(false);

        // Xu ly button chung cho Duyet, Khong Duyet
        private void XuLyButtonTrangThaiTD(bool ops)
        {
            try
            {
                if (string.IsNullOrEmpty(_idSelected))
                {
                    MessageBox.Show($"Bạn cần chọn yêu cầu của trưởng phòng để xử lý !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var setTrangThai = ops ? "Đang tuyển" : "Loại";
                var buttonText = ops ? "Duyệt" : "Không duyệt";
                var tuyenDung = _dbContextTD.KtraTuyenDungQuaID(Convert.ToInt32(_idSelected));

                string ghiChu = "Không duyệt";
                if (tuyenDung.ghiChu.Equals(rtGhiChu.Text, StringComparison.OrdinalIgnoreCase))
                {
                    if (buttonText.Equals("Duyệt", StringComparison.OrdinalIgnoreCase))
                    {
                        ghiChu = "Duyệt";
                    }
                    else ghiChu = "Không duyệt";
                }
                else ghiChu = rtGhiChu.Text;

                if (tuyenDung == null)
                {
                    MessageBox.Show($"Không tìm thấy dữ liệu của tuyển dụng để cập nhật trạng thái !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show($"Bạn có chắc chắn về cập nhật với trạng thái là '{buttonText}' ?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (DisplayUserControlPanel.KiemTraDuLieuDauVao(error, pnlDuyet) && KtraGhiChu())
                    {
                        if (_dbContextTD.KtraCapNhatDuyetTuyenDung(new DTOTuyenDung(tuyenDung.id, txtTieuDe.Text, Convert.ToInt32(txtSoLuongDuyet.Text), setTrangThai, ghiChu, DateTime.Now)))
                        {
                            MessageBox.Show($"Cập nhật duyệt tuyển dụng thành công.");
                            CapNhatChung();
                            btnDuyet.Enabled = false;
                            btnKhongDuyet.Enabled = false;
                        }
                        else MessageBox.Show($"Cập nhật duyệt tuyển dụng thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else return;
            }
            catch
            {
                MessageBox.Show($"Lỗi cập nhật trạng thái duyệt tuyển dụng !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Lam rong du lieu trong field
        public void Empty()
        {
            foreach (var control in pnlDuyet.Controls)
            {
                if (control is Guna2TextBox text && !string.IsNullOrEmpty(text.Text))
                {
                    text.Text = string.Empty;
                }
            }
            rtGhiChu.Text = string.Empty;
        }

        // Cap nhat chung ho load, giao dien, lam trong du lieu
        public void CapNhatChung()
        {
            dgvTPTuyenDung.DataSource = LoadDuLieu();
            ChinhSuaGiaoDien();
            Empty();
        }

        // Chinh sua giao dien 
        private void ChinhSuaGiaoDien()
        {
            dgvTPTuyenDung.Columns["ID"].Visible = false;
            dgvTPTuyenDung.Columns["IDNhanVien"].Visible = false;
            dgvTPTuyenDung.Columns["TieuDe"].HeaderText = "Tiêu đề";
            dgvTPTuyenDung.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvTPTuyenDung.Columns["ChucVu"].HeaderText = "Chức vụ";
            dgvTPTuyenDung.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvTPTuyenDung.Columns["NguoiTao"].HeaderText = "Người tạo";
            dgvTPTuyenDung.Columns["TrangThai"].HeaderText = "Trạng thái";

            dgvTPTuyenDung.Columns["SoLuong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvTPTuyenDung.Columns["TrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvTPTuyenDung.Columns["GhiChu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTPTuyenDung.Columns["GhiChu"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTPTuyenDung.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                error.Clear();

                DisplayUserControlPanel.LayKiTuSo(sender);
                sender = txtSoLuongDuyet.MaxLength = 2;
                if (int.TryParse(txtSoLuongDuyet.Text, out int qty) && qty < 11)
                {
                    if (qty == 0)
                    {
                        txtSoLuongDuyet.Clear();
                        error.SetError(txtSoLuongDuyet, "Số lượng tuyển dụng không được bằng 0 !");
                    }
                    else txtSoLuongDuyet.Text = qty.ToString();
                }
                else
                {
                    txtSoLuongDuyet.Clear();
                    //error.SetError(txtSoLuongDuyet, "Số lượng tuyển dụng không được vượt quá 10 người !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị số lượng !" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
