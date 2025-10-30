using BLL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Guna.UI2.WinForms;
namespace GUI
{
    public partial class ucUngVien : UserControl
    {
        private string _idNhanVien, _conn, _urlImage = "";
        int _idUngVien = 0;
        BLLUngVien _bllUngVien;
        BLLChucVu _bllChucVu;
        BLLTuyenDung _bllTuyenDung;
        DTOUngVien _oldUngVien;

        public ucUngVien(string idNhanVien, string conn)
        {
            _bllUngVien = new BLLUngVien(conn);
            _bllChucVu = new BLLChucVu(conn);
            _bllTuyenDung = new BLLTuyenDung(conn);
            _idNhanVien = idNhanVien;
            InitializeComponent();
        }

        void CleanInput()
        {
            _urlImage = "";
            _idUngVien = 0;
            ptbImageCV.Image = null;
            txtName.Text = "";
            dtpDateOfBirth.Value = DateTime.Parse("01/01/2000");
            txtHometown.Text = "";
            cbTuyenDung.SelectedValue = 1;
            rdoMale.Checked = true;
            txtEmail.Text = "";
            txtAddress.Text = "";
            cbChucVuUngTuyen.SelectedValue = 1;
            dtpNgayUngTuyen.Value = DateTime.Now;

        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép phím điều khiển như Backspace, Delete
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Cho phép chữ, số, dấu chấm, gạch dưới, gạch ngang, và ký tự @
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != '_' &&
                e.KeyChar != '-' &&
                e.KeyChar != '@')
            {
                e.Handled = true; // chặn ký tự không hợp lệ
            }
        }

        private void txtImageCV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void txtHometown_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtFindName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string gioiTinh = "";
            int tuoi = DateTime.Now.Year - dtpDateOfBirth.Value.Year;
            string[] arr = Path.GetFileName(_urlImage).Split('.');
            string nameImg = Guid.NewGuid().ToString() + "." + arr[1];
            string fileExtension = "jpg,jpeg,png,bmp,gif";
            string[] arrFileExtension = fileExtension.Split(',');
            string[] arrNameFile = nameImg.Split('.');
            if (!arrFileExtension.Contains(arr[1]))
            {
                MessageBox.Show("File bạn chọn không đúng định dạng, vui lòng chọn file có đuôi \n .jpg .jpeg .png .bmp .gif", "Thông báo");
                return;
            }
            if (dtpDateOfBirth.Value > DateTime.Today.AddYears(-tuoi))
                tuoi--;
            if(tuoi< 16)
            {
                MessageBox.Show("Tuổi của ứng viên không được dưới 16 tuổi!", "Thông báo");
                return;
            }
            if (_urlImage == "")
            {
                MessageBox.Show("Vui lòng chọn hình ảnh", "Thông báo");
                return;
            }
            
            if (rdoFemale.Checked)
            {
                gioiTinh = "nu";
            }else if(rdoMale.Checked)
            {
                gioiTinh = "nam";
            }
            DTOUngVien dto = new DTOUngVien()
            {
                TenNhanVien = txtName.Text,
                NgaySinh = dtpDateOfBirth.Value,
                DiaChi = txtAddress.Text,
                Que = txtHometown.Text,
                GioiTinh = gioiTinh,
                Email = txtEmail.Text,
                DuongDanCV = nameImg,
                IdChucVuUngTuyen = int.Parse(cbChucVuUngTuyen.SelectedValue.ToString()),
                IdTuyenDung = int.Parse(cbTuyenDung.SelectedValue.ToString()),
                NgayUngTuyen = dtpNgayUngTuyen.Value,
                TrangThai = "Đang xét duyệt"
            };

            if(_bllUngVien.Add(dto).ToLower() == "passed")
            {
                
                string folder = Directory.GetParent(Application.StartupPath).Parent.Parent.FullName;
                string folderPath = Path.Combine(folder, "Image");
                string destPath = Path.Combine(folderPath, nameImg);
                File.Copy(_urlImage, destPath);
                MessageBox.Show("Thêm thành công!", "Thông báo");
                dgvUngVien.DataSource = _bllUngVien.GetAll();

                CleanInput();
            }
            else if(_bllUngVien.Add(dto).ToLower() == "failed")
            {
                MessageBox.Show("Thêm thất bại!", "Thông báo");
            }
            else if (_bllUngVien.Add(dto).ToLower() == "email already exists")
            {
                MessageBox.Show("Email đã được sử dụng, vui lòng nhập email khác", "Thông báo");
            }
            else
            {
                MessageBox.Show("Dữ liệu không hợp lệ, vui lòng kiểm tra lại!", "Thông báo");
            }
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            //DateTime ngaySinh = dtpDateOfBirth.Value;
            //DateTime ngayHienTai = DateTime.Today;

            //int tuoi = ngayHienTai.Year - ngaySinh.Year;
            //if (ngaySinh > ngayHienTai.AddYears(-tuoi))
            //    tuoi--;

            //if (tuoi < 16)
            //{
            //    MessageBox.Show("Nhân viên phải từ 16 tuổi trở lên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    // Đặt lại giá trị DateTimePicker về ngày hợp lệ (ví dụ 16 năm trước)
            //    dtpDateOfBirth.Value = ngayHienTai.AddYears(-16);
            //}
        }

        private void dtpNgayUngTuyen_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(_idUngVien == 0)
            {
                MessageBox.Show("Vui lòng chọn ứng viên trước khi cập nhật!", "Thông báo");
                return;
            }
            if(_urlImage == "")
            {
                MessageBox.Show("Vui lòng chọn ảnh!", "Thông báo");
                return;
            }
            string gioiTinh = "";
            if (rdoFemale.Checked)
            {
                gioiTinh = "nu";
            }
            else
            {
                gioiTinh = "nam";
            }
            DTOUngVien dto = new DTOUngVien()
            {
                Id = _oldUngVien.Id,
                TenNhanVien = txtName.Text,
                NgaySinh = dtpDateOfBirth.Value,
                DiaChi = txtAddress.Text,
                Que = txtHometown.Text,
                GioiTinh = gioiTinh,
                Email = txtEmail.Text,
                DuongDanCV = Path.GetFileName(_urlImage),
                IdChucVuUngTuyen = int.Parse(cbChucVuUngTuyen.SelectedValue.ToString()),
                IdTuyenDung = int.Parse(cbTuyenDung.SelectedValue.ToString()),
                NgayUngTuyen = dtpNgayUngTuyen.Value,
                TrangThai = cbStatus.Text
            };
            if (dto.TenNhanVien != _oldUngVien.TenNhanVien ||
                dto.NgaySinh != _oldUngVien.NgaySinh ||
                dto.DiaChi != _oldUngVien.DiaChi ||
                dto.Que != _oldUngVien.Que ||
                dto.GioiTinh != _oldUngVien.GioiTinh ||
                dto.Email != _oldUngVien.Email ||
                dto.DuongDanCV != _oldUngVien.DuongDanCV ||
                dto.IdChucVuUngTuyen != _oldUngVien.IdChucVuUngTuyen ||
                dto.IdTuyenDung != _oldUngVien.IdTuyenDung ||
                dto.NgayUngTuyen != _oldUngVien.NgayUngTuyen ||
                dto.TrangThai != _oldUngVien.TrangThai)
            {
                if (dto.DuongDanCV != _oldUngVien.DuongDanCV)
                {
                    string fileExtension = "jpg,jpeg,png,bmp,gif";
                    string[] arrFileExtension = fileExtension.Split(',');
                    string[] arr = _urlImage.Split('.');
                    if (!arrFileExtension.Contains(arr[1]))
                    {
                        MessageBox.Show("File bạn chọn không đúng định dạng, vui lòng chọn file có đuôi \n .jpg .jpeg .png .bmp .gif", "Thông báo");
                        return;
                    }
                    string path = Directory.GetParent(Application.StartupPath).Parent.Parent.FullName;
                    string pathFolder = Path.Combine(path, "Image");
                    dto.DuongDanCV = Guid.NewGuid().ToString() +"." + arr[1];
                    string des = Path.Combine(pathFolder, dto.DuongDanCV);
                    File.Copy(_urlImage, des);
                }
                if (_bllUngVien.Update(dto))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    dgvUngVien.DataSource = _bllUngVien.GetAll();
                    CleanInput();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Không có thay đổi nào.", "Thông báo");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(_idUngVien != 0)
            {
                DialogResult check = MessageBox.Show("Bạn có muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (check == DialogResult.No)
                {
                    return;
                }
                if (_idUngVien > 0)
                {
                    if (_bllUngVien.Delete(_idUngVien))
                    {
                        _idUngVien = 0;
                        MessageBox.Show("Xóa thành công!", "Thông báo");
                        dgvUngVien.DataSource = _bllUngVien.GetAll();
                        CleanInput();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Thông báo");
                    }

                }
                else if (_idUngVien == 0)
                {
                    MessageBox.Show("Vui lòng chọn đối tượng để xóa!", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đối tượng để xóa!", "Thông báo");
            }
        }
        private void dgvUngVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvUngVien.Rows[e.RowIndex].Cells[1].Value != null &&
                dgvUngVien.Rows[e.RowIndex].Cells[2].Value.ToString() != "" &&
                dgvUngVien.Rows[e.RowIndex].Cells.Count > 0
                )
            {
                string gioiTinh = "";
                DataGridViewRow row = dgvUngVien.Rows[e.RowIndex];
                int dong = dgvUngVien.CurrentCell.RowIndex;
                _idUngVien = int.Parse(row.Cells["id"].Value.ToString());
                txtName.Text = row.Cells["tenNhanVien"].Value.ToString();
                dtpDateOfBirth.Text = row.Cells["ngaySinh"].Value.ToString();
                txtAddress.Text = row.Cells["diaChi"].Value.ToString();
                txtHometown.Text = row.Cells["que"].Value.ToString();
                if(row.Cells["gioiTinh"].Value.ToString().ToLower() == "nu")
                {
                    gioiTinh = "nu";
                    rdoFemale.Checked = true;
                }else
                {
                    gioiTinh = "nam";
                    rdoMale.Checked = true;
                }
                txtEmail.Text = row.Cells["email"].Value.ToString();
                Image fileImage = null;
                try
                {
                    fileImage = Image.FromFile(Path.Combine(Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "Image"),
                                                    row.Cells["duongDanCV"].Value.ToString()));
                    _urlImage = Path.Combine(Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "Image"),
                                                    row.Cells["duongDanCV"].Value.ToString());
                }
                catch
                {
                    _urlImage = "";
                }
                if(fileImage != null)
                {
                    string fileExtension = "jpg,jpeg,png,bmp,gif";
                    string[] arrFileExtension = fileExtension.Split(',');
                    string[] arr = _urlImage.Split('.');
                    if (arr.Length < 1)
                    {
                        if(!arrFileExtension.Contains(arr[1]))
                        {
                            MessageBox.Show("File bạn chọn không đúng định dạng, vui lòng chọn file có đuôi \n .jpg .jpeg .png .bmp .gif", "Thông báo");
                            return;
                        }
                        
                    }
                    ptbImageCV.Image = fileImage;
                    ptbImageCV.SizeMode = PictureBoxSizeMode.Zoom;
                }
                cbChucVuUngTuyen.SelectedValue = int.Parse(row.Cells["idChucVuUngTuyen"].Value.ToString());
                cbTuyenDung.SelectedValue = int.Parse(row.Cells["idTuyenDung"].Value.ToString());
                dtpNgayUngTuyen.Text = row.Cells["ngayUngTuyen"].Value.ToString();
                cbStatus.SelectedItem = row.Cells["trangThai"].Value.ToString();


                _oldUngVien = new DTOUngVien()
                {
                    Id = _idUngVien,
                    TenNhanVien = row.Cells["tenNhanVien"].Value.ToString(),
                    NgaySinh = DateTime.Parse(row.Cells["ngaySinh"].Value.ToString()),
                    DiaChi = row.Cells["diaChi"].Value.ToString(),
                    Que = row.Cells["que"].Value.ToString(),
                    GioiTinh = gioiTinh,
                    Email = row.Cells["email"].Value.ToString(),
                    DuongDanCV = row.Cells["duongDanCV"].Value.ToString(),
                    IdChucVuUngTuyen = int.Parse(cbChucVuUngTuyen.SelectedValue.ToString()),
                    IdTuyenDung = int.Parse(row.Cells["idTuyenDung"].Value.ToString()),
                    NgayUngTuyen = DateTime.Parse(row.Cells["ngayUngTuyen"].Value.ToString()),
                    TrangThai = row.Cells["trangThai"].Value.ToString()
                };
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            dgvUngVien.DataSource = _bllUngVien.GetUngTuyenByChucVu(int.Parse(cmbFindPosition.SelectedValue.ToString()));

        }

        private void btnResetDGV_Click(object sender, EventArgs e)
        {
            dgvUngVien.DataSource = _bllUngVien.GetAll();

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Gán hình vào PictureBox
                ptbImageCV.Image = Image.FromFile(ofd.FileName);

                // Tuỳ chọn hiển thị (co giãn vừa khung)
                ptbImageCV.SizeMode = PictureBoxSizeMode.Zoom;

                _urlImage = ofd.FileName;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if(_idUngVien == 0)
            {
                MessageBox.Show("Vui lòng chọn ứng viên để duyệt!", "Thông báo");
                return;
            }
            DTOUngVien dto = new DTOUngVien()
            {
                Id = _idUngVien,
                TenNhanVien = _oldUngVien.TenNhanVien,
                NgaySinh = _oldUngVien.NgaySinh,
                DiaChi = _oldUngVien.DiaChi,
                Que = _oldUngVien.Que,
                GioiTinh = _oldUngVien.GioiTinh,
                Email = _oldUngVien.Email,
                DuongDanCV = _oldUngVien.DuongDanCV,
                IdChucVuUngTuyen = _oldUngVien.IdChucVuUngTuyen,
                IdTuyenDung = _oldUngVien.IdTuyenDung,
                NgayUngTuyen = _oldUngVien.NgayUngTuyen,
                TrangThai = _oldUngVien.TrangThai
            };
            dto.Id = _idUngVien;
            dto.TrangThai = "Phỏng vấn";
            if(_oldUngVien.TrangThai == "Phỏng vấn")
            {
                dto.TrangThai = "Thử việc";
            }else if (_oldUngVien.TrangThai == "Thử việc")
            {
                dto.TrangThai = "Trúng tuyển";

            }
            else if (_oldUngVien.TrangThai == "Trúng tuyển")
            {
                MessageBox.Show("Ứng viên đã trúng tuyển, không thể duyệt nữa!", "Thông báo");
                return;
            }
            else if (_oldUngVien.TrangThai == "Loại")
            {
                MessageBox.Show("Ứng viên đã bị loại, không thể duyệt nữa!", "Thông báo");
                return;
            }

            if (_bllUngVien.Update(dto))
            {
                MessageBox.Show($"Duyệt thành công, ứng viên hiện đang ở trạng thái\n{dto.TrangThai}!", "Thông báo");
                LoadDgvUngVien();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Duyệt thất bại!", "Thông báo");
            }

        }

        private void btnEliminat_Click(object sender, EventArgs e)
        {
            DialogResult resultCheck = MessageBox.Show("Bạn có muốn loại ứng viên?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(resultCheck == DialogResult.Yes)
            {
                if (_idUngVien == 0)
                {
                    MessageBox.Show("Vui lòng chọn ứng viên để loại!", "Thông báo");
                    return;
                }
                if (_oldUngVien.TrangThai == "Loại")
                {
                    MessageBox.Show("Ứng viên đã bị loại, không thể loại nữa!", "Thông báo");
                    return;
                }
                else if (_oldUngVien.TrangThai == "Trúng tuyển")
                {
                    MessageBox.Show("Ứng viên đã trúng tuyển, không thể loại!", "Thông báo");
                    return;
                }
                DTOUngVien dto = new DTOUngVien()
                {
                    Id = _idUngVien,
                    TenNhanVien = _oldUngVien.TenNhanVien,
                    NgaySinh = _oldUngVien.NgaySinh,
                    DiaChi = _oldUngVien.DiaChi,
                    Que = _oldUngVien.Que,
                    GioiTinh = _oldUngVien.GioiTinh,
                    Email = _oldUngVien.Email,
                    DuongDanCV = _oldUngVien.DuongDanCV,
                    IdChucVuUngTuyen = _oldUngVien.IdChucVuUngTuyen,
                    IdTuyenDung = _oldUngVien.IdTuyenDung,
                    NgayUngTuyen = _oldUngVien.NgayUngTuyen,
                    TrangThai = "Loại"
                };

                if (_bllUngVien.Update(dto))
                {
                    MessageBox.Show($"Loại thành công!", "Thông báo");
                    LoadDgvUngVien();
                    CleanInput();
                }
                else
                {
                    MessageBox.Show("Loại thất bại!", "Thông báo");
                }
            }
        }

        private void dtpDateOfBirth_Leave(object sender, EventArgs e)
        {
            DateTime ngaySinh = dtpDateOfBirth.Value;
            DateTime ngayHienTai = DateTime.Today;

            int tuoi = ngayHienTai.Year - ngaySinh.Year;
            if (ngaySinh > ngayHienTai.AddYears(-tuoi))
                tuoi--;

            if (tuoi < 16)
            {
                MessageBox.Show("Nhân viên phải từ 16 tuổi trở lên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Đặt lại giá trị DateTimePicker về ngày hợp lệ (ví dụ 16 năm trước)
                dtpDateOfBirth.Value = ngayHienTai.AddYears(-16);
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucUngVien_Load(object sender, EventArgs e)
        {
            var position = _bllChucVu.GetPositionByIdStaff(_idNhanVien);


            rdoMale.Checked = true;
            
            cbChucVuUngTuyen.DataSource = _bllChucVu.GetAll();
            cbChucVuUngTuyen.DisplayMember = "Tên chức vụ";
            cbChucVuUngTuyen.ValueMember = "Mã chức vụ";

            cbTuyenDung.DataSource = _bllTuyenDung.KtraDsTuyenDung();
            cbTuyenDung.DisplayMember = "tieuDe";
            cbTuyenDung.ValueMember = "id";

            cmbFindPosition.DataSource = _bllChucVu.GetAll();
            cmbFindPosition.DisplayMember = "Tên chức vụ";
            cmbFindPosition.ValueMember = "Mã chức vụ";
            dtpNgayUngTuyen.Value = DateTime.Now;


            LoadDgvUngVien();

        }

        private void LoadDgvUngVien()
        {
            dgvUngVien.DataSource = _bllUngVien.GetAll();
            dgvUngVien.Columns["idChucVuUngTuyen"].Visible = false;
            dgvUngVien.Columns["idTuyenDung"].Visible = false;
            dgvUngVien.Columns["id"].Visible = false;
            dgvUngVien.Columns["duongDanCV"].Visible = false;

            dgvUngVien.Columns["tenNhanVien"].HeaderText = "Tên ứng viên";
            dgvUngVien.Columns["ngaySinh"].HeaderText = "Ngày sinh";
            dgvUngVien.Columns["diaChi"].HeaderText = "Địa chỉ";
            dgvUngVien.Columns["que"].HeaderText = "Quê quán";
            dgvUngVien.Columns["gioiTinh"].HeaderText = "Giới tính";
            dgvUngVien.Columns["email"].HeaderText = "Email";
            dgvUngVien.Columns["ngayUngTuyen"].HeaderText = "Ngày ứng tuyển";
            dgvUngVien.Columns["trangThai"].HeaderText = "Trạng thái";
            dgvUngVien.Columns["tenChucVu"].HeaderText = "Chức vụ ứng tuyển";
            dgvUngVien.Columns["tieuDeTuyenDung"].HeaderText = "Tuyển dụng";

        }
    }
}
