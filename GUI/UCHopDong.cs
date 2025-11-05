using BLL;
using DAL;
using DTO;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class UCHopDong : UserControl
    {
        private BLLHopDongLaoDong _bllHopDongLaoDong;
        private BLLThuongPhat _bllThuongPhat;
        private BLLNhanVien _bllNhanVien;
        private BLLChucVu _bllChucVu;
        string _urlImage = "";
        int _idHopDong = -1;
        DTOHopDongLaoDong _oldValue = new DTOHopDongLaoDong();
        public UCHopDong(string idNhanVien, string stringConnection)
        {
            _bllHopDongLaoDong = new BLLHopDongLaoDong(stringConnection);
            _bllThuongPhat = new BLLThuongPhat(stringConnection);
            _bllNhanVien = new BLLNhanVien(stringConnection);
            _bllChucVu = new BLLChucVu(stringConnection);
            InitializeComponent();
        }

        private void UCHopDong_Load(object sender, EventArgs e)
        {
            //Load dữ liệu cho combobox 
            cmbNhanVien.DataSource = _bllNhanVien.ComboboxNhanVien();
            cmbNhanVien.ValueMember = "id";
            cmbNhanVien.DisplayMember = "TenNhanVien";

            LoadDGV();
        }

        void LoadDGV()
        {
            dgvHopDong.DataSource = _bllHopDongLaoDong.GetAll();

            //Ẩn những column không cần thiết
            if (dgvHopDong.Columns.Contains("id"))
                dgvHopDong.Columns["id"].Visible = false;
            if (dgvHopDong.Columns.Contains("idNhanVien"))
                dgvHopDong.Columns["idNhanVien"].Visible = false;
            if (dgvHopDong.Columns.Contains("HinhAnh"))
                dgvHopDong.Columns["HinhAnh"].Visible = false;
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Gán hình vào PictureBox
                ptbHinhHopDong.Image = System.Drawing.Image.FromFile(ofd.FileName);

                // Tuỳ chọn hiển thị (co giãn vừa khung)
                ptbHinhHopDong.SizeMode = PictureBoxSizeMode.Zoom;

                _urlImage = ofd.FileName;
            }
        }

        private void dgvHopDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvHopDong.Rows[e.RowIndex].Cells.Count > 0 &&
                dgvHopDong.Rows[e.RowIndex].Cells["id"].Value != null)
            {
                //Gán giá trị vào control input và biến
                DataGridViewRow row = dgvHopDong.Rows[e.RowIndex];
                _idHopDong = int.Parse(row.Cells["id"].Value.ToString());
                cmbContractType.SelectedItem = row.Cells["LoaiHopDong"].Value.ToString();
                dtpNgayKy.Text = row.Cells["NgayKy"].Value.ToString();
                dtpNgayBatDau.Text = row.Cells["NgayBatDau"].Value.ToString();
                if (row.Cells["NgayKetThuc"].Value == null)
                {
                    dtpNgayKetThuc.Text = "";
                }
                else
                {
                    dtpNgayKetThuc.Text = row.Cells["NgayKetThuc"].Value.ToString();
                }
                txtLuong.Text = row.Cells["Luong"].Value.ToString();
                cmbNhanVien.SelectedValue = row.Cells["idNhanVien"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
                System.Drawing.Image fileImage = null;
                try
                {
                    var imageRelative = row.Cells["HinhAnh"].Value?.ToString();
                    if (!string.IsNullOrEmpty(imageRelative))
                    {
                        var candidate = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "Image", imageRelative);
                        fileImage = System.Drawing.Image.FromFile(candidate);
                        _urlImage = candidate;
                    }
                    else _urlImage = "";
                }
                catch
                {
                    _urlImage = "";
                }
                if (fileImage != null)
                {
                    ptbHinhHopDong.Image = fileImage;
                    ptbHinhHopDong.SizeMode = PictureBoxSizeMode.Zoom;
                }

                //Tạo biến để giữ giá trị khi chọn
                _oldValue = new DTOHopDongLaoDong()
                {
                    Id = _idHopDong,
                    LoaiHopDong = row.Cells["LoaiHopDong"].Value.ToString(),
                    NgayKy = DateTime.Parse(row.Cells["NgayKy"].Value.ToString()),
                    NgayBatDau = DateTime.Parse(row.Cells["NgayBatDau"].Value.ToString()),
                    NgayKetThuc = string.IsNullOrEmpty(row.Cells["NgayKetThuc"].Value?.ToString()) ? (DateTime?)null : DateTime.Parse(row.Cells["NgayKetThuc"].Value.ToString()),
                    Luong = decimal.Parse(row.Cells["Luong"].Value.ToString()),
                    HinhAnh = row.Cells["HinhAnh"].Value?.ToString(),
                    IdNhanVien = row.Cells["idNhanVien"].Value.ToString(),
                    MoTa = row.Cells["MoTa"].Value.ToString()
                };
            }
        }

        private void cmbNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedValue != null)
            {
                var val = cmbNhanVien.SelectedValue.ToString();
                var pos = _bllChucVu.GetPositionByIdStaff(val);
                if (pos != null) txtLuong.Text = pos.luongCoBan.ToString();
            }
        }
        private void ClearInputs()
        {
            cmbContractType.SelectedIndex = -1;
            dtpNgayKy.Value = DateTime.Now;
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now;
            txtLuong.Text = "";
            cmbNhanVien.SelectedIndex = -1;
            txtMoTa.Text = "";
            ptbHinhHopDong.Image = null;
            _urlImage = "";
            _idHopDong = -1;
            _oldValue = new DTOHopDongLaoDong();
        }

        private string SaveImageToProjectFolder(string sourceImagePath)
        {
            if (string.IsNullOrEmpty(sourceImagePath) || !File.Exists(sourceImagePath)) return null;
            try
            {
                var imageFolder = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "Image");
                if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);
                var fileName = Path.GetFileName(sourceImagePath);
                var dest = Path.Combine(imageFolder, fileName);

                // nếu file trùng tên thì đổi tên để tránh ghi đè
                if (File.Exists(dest))
                {
                    var name = Path.GetFileNameWithoutExtension(fileName);
                    var ext = Path.GetExtension(fileName);
                    dest = Path.Combine(imageFolder, $"{name}_{DateTime.Now.Ticks}{ext}");
                }

                File.Copy(sourceImagePath, dest);
                return Path.GetFileName(dest); 
            }
            catch
            {
                return null;
            }
        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbContractType.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn loại hợp đồng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbNhanVien.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new DTOHopDongLaoDong
                {
                    LoaiHopDong = cmbContractType.SelectedItem.ToString(),
                    NgayKy = dtpNgayKy.Value.Date,
                    NgayBatDau = dtpNgayBatDau.Value.Date,
                    NgayKetThuc = dtpNgayKetThuc.Value.Date,
                    Luong = decimal.TryParse(txtLuong.Text, out decimal lu) ? lu : 0,
                    IdNhanVien = cmbNhanVien.SelectedValue.ToString(),
                    MoTa = txtMoTa.Text
                };

                // Lưu ảnh nếu có
                if (!string.IsNullOrEmpty(_urlImage) && File.Exists(_urlImage))
                {
                    var saved = SaveImageToProjectFolder(_urlImage);
                    if (!string.IsNullOrEmpty(saved)) dto.HinhAnh = saved;
                }

                var result = _bllHopDongLaoDong.Create(dto);
                if (result)
                {
                    MessageBox.Show("Tạo hợp đồng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDGV();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Tạo hợp đồng thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idHopDong <= 0)
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var dto = new DTOHopDongLaoDong
                {
                    Id = _idHopDong,
                    LoaiHopDong = cmbContractType.SelectedItem?.ToString(),
                    NgayKy = dtpNgayKy.Value.Date,
                    NgayBatDau = dtpNgayBatDau.Value.Date,
                    NgayKetThuc = dtpNgayKetThuc.Value.Date,
                    Luong = decimal.TryParse(txtLuong.Text, out decimal lu) ? lu : 0,
                    IdNhanVien = cmbNhanVien.SelectedValue?.ToString(),
                    MoTa = txtMoTa.Text
                };

                if (cmbContractType.SelectedItem.ToString().ToLower() == "không xác định thời hạn")
                {
                    dto.NgayKetThuc = null;
                }

                //Nếu ảnh thay đổi (đường dẫn khác so với cũ) lưu ảnh mới và cập nhật tên
                if (!string.IsNullOrEmpty(_urlImage) && File.Exists(_urlImage))
                {
                    var saved = SaveImageToProjectFolder(_urlImage);
                    if (!string.IsNullOrEmpty(saved)) dto.HinhAnh = saved;
                }
                else
                {
                    //Giữ lại image cũ nếu không chọn ảnh mới
                    dto.HinhAnh = _oldValue?.HinhAnh;
                }

                var result = _bllHopDongLaoDong.Edit(dto);
                if (result)
                {
                    MessageBox.Show("Cập nhật hợp đồng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDGV();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Cập nhật hợp đồng thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idHopDong <= 0)
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show("Bạn có chắc muốn xóa hợp đồng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                var result = _bllHopDongLaoDong.Remove(_idHopDong);
                if (result)
                {
                    MessageBox.Show("Xóa hợp đồng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDGV();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa hợp đồng thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                string loai = cmbFindType?.SelectedItem?.ToString() ?? ""; 
                string ten = txtFindEmployee?.Text ?? "";

                if (string.IsNullOrWhiteSpace(loai) && string.IsNullOrWhiteSpace(ten))
                {
                    MessageBox.Show("Vui lòng nhập tiêu chí tìm kiếm (loại hợp đồng hoặc tên nhân viên).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvHopDong.DataSource = _bllHopDongLaoDong.Search(loai, ten);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDGV();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lại danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtLuong_TextChanged(object sender, EventArgs e)
        {
            DisplayUserControlPanel.LayKiTuSo(sender);
        }
    }
}