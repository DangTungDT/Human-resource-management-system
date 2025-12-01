using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class CapNhatThongTinRieng : UserControl
    {
        private Guna2TextBox txtName, txtAddress, txtQue, txtEmail;
        private Guna2ComboBox cbGender;
        private Guna2DateTimePicker dtDob;
        private Guna2Button btnSave, btnBack, btnUpload;
        private Guna2CirclePictureBox picAvatar;
        private Panel _panel;

        private string idNhanVien;
        private string imagePath, _imageName = "";
        private string connectionString;
        private BLLNhanVien bllNhanVien;

        public CapNhatThongTinRieng(string idNV, Panel panel, string conn)
        {
            connectionString = conn;
            idNhanVien = idNV;
            bllNhanVien = new BLLNhanVien(conn);
            _panel = panel;
            InitializeComponent();
            BuildUI();
            LoadThongTinCaNhan();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 246, 248);

            // ===== TIÊU ĐỀ =====
            Label lblTitle = new Label()
            {
                Text = "CẬP NHẬT THÔNG TIN CÁ NHÂN",
                Font = new Font("Times New Roman", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 60, 120),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ===== AVATAR =====
            picAvatar = new Guna2CirclePictureBox()
            {
                Size = new Size(200, 160),
                Image = Properties.Resources.user,
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.Top,
                ShadowDecoration = { Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle },
                Margin = new Padding(20)
            };

            // ===== NÚT TẢI ẢNH =====
            btnUpload = new Guna2Button()
            {
                Text = "📁 Tải ảnh lên",
                BorderRadius = 8,
                FillColor = Color.SteelBlue,
                Font = new Font("Times New Roman", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Width = 140,
                Height = 35,
                Cursor = Cursors.Hand
            };
            btnUpload.HoverState.FillColor = Color.MediumSeaGreen;
            btnUpload.Click += BtnUpload_Click;

            FlowLayoutPanel avatarPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                WrapContents = false,
                Anchor = AnchorStyles.None,
                Padding = new Padding(20, 40, 20, 0)
            };
            avatarPanel.Controls.Add(picAvatar);
            avatarPanel.Controls.Add(btnUpload);
            avatarPanel.SetFlowBreak(btnUpload, true);

            // ===== FORM NHẬP =====
            txtName = new Guna2TextBox() { PlaceholderText = "Họ tên", BorderRadius = 6, Margin = new Padding(0, 8, 0, 8) };
            dtDob = new Guna2DateTimePicker() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", BorderRadius = 6, Margin = new Padding(0, 8, 0, 8) };
            cbGender = new Guna2ComboBox() { BorderRadius = 6, DropDownStyle = ComboBoxStyle.DropDownList, Width = 200, Margin = new Padding(0, 8, 0, 8) };
            cbGender.Items.AddRange(new object[] { "Nam", "Nữ" });
            txtAddress = new Guna2TextBox() { PlaceholderText = "Địa chỉ", BorderRadius = 6, Margin = new Padding(0, 8, 0, 8) };
            txtQue = new Guna2TextBox() { PlaceholderText = "Quê quán", BorderRadius = 6, Margin = new Padding(0, 8, 0, 8) };
            txtEmail = new Guna2TextBox() { PlaceholderText = "Email", BorderRadius = 6, Margin = new Padding(0, 8, 0, 8) };

            TableLayoutPanel formLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(20, 30, 50, 10),
                AutoSize = true
            };
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));

            AddRow(formLayout, "Họ tên:", txtName, 0);
            AddRow(formLayout, "Ngày sinh:", dtDob, 1);
            AddRow(formLayout, "Giới tính:", cbGender, 2);
            AddRow(formLayout, "Địa chỉ:", txtAddress, 3);
            AddRow(formLayout, "Quê quán:", txtQue, 4);
            AddRow(formLayout, "Email:", txtEmail, 5);

            // ===== PANEL TRÁI - PHẢI =====
            TableLayoutPanel mainContent = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                Padding = new Padding(100, 20, 100, 0),
                AutoSize = true
            };
            mainContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            mainContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            mainContent.Controls.Add(avatarPanel, 0, 0);
            mainContent.Controls.Add(formLayout, 1, 0);

            // ===== NÚT LƯU & QUAY LẠI =====
            btnSave = new Guna2Button()
            {
                Text = "💾 Lưu thay đổi",
                BorderRadius = 10,
                FillColor = Color.MediumSeaGreen,
                Font = new Font("Times New Roman", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Width = 160,
                Height = 40,
                Cursor = Cursors.Hand
            };
            btnSave.HoverState.FillColor = Color.SeaGreen;
            btnSave.Click += BtnSave_Click;

            btnBack = new Guna2Button()
            {
                Text = "⬅️ Quay lại",
                BorderRadius = 10,
                FillColor = Color.SteelBlue,
                Font = new Font("Times New Roman", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Width = 140,
                Height = 40,
                Cursor = Cursors.Hand
            };
            btnBack.HoverState.FillColor = Color.RoyalBlue;
            btnBack.Click += BtnBack_Click;

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                Padding = new Padding(0, 20, 700, 0)
            };
            buttonPanel.Controls.Add(btnBack);
            buttonPanel.Controls.Add(btnSave);

            //// Căn giữa theo chiều ngang
            //buttonPanel.Resize += (s, e) =>
            //{
            //    buttonPanel.Left = (this.Width - buttonPanel.Width) / 2-20;
            //};

            // ===== GỘP TỔNG =====
            Panel container = new Panel() { Dock = DockStyle.Fill, AutoScroll = true };
            container.Controls.Add(buttonPanel);
            container.Controls.Add(mainContent);
            container.Controls.Add(lblTitle);

            this.Controls.Add(container);
        }

        private void AddRow(TableLayoutPanel table, string labelText, Control input, int row)
        {
            Label lbl = new Label()
            {
                Text = labelText,
                Font = new Font("Times New Roman", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 60, 120),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 5, 10, 0)
            };
            table.Controls.Add(lbl, 0, row);
            table.Controls.Add(input, 1, row);
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dlg.FileName;
                    _imageName = dlg.SafeFileName;
                    // ✅ Dùng stream để tránh khóa file
                    using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        picAvatar.Image = Image.FromStream(stream);
                    }
                }
            }
        }

        private string SaveImageToFolder(string imagePath, string employeeId)
        {
            if (string.IsNullOrEmpty(imagePath))
                return null;

            string folderPath = Path.Combine(Application.StartupPath, "image");
            if (folderPath.Contains("bin"))
            {
                //Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
                folderPath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "image");
            }

            // ✅ Tạo thư mục nếu chưa có
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // ✅ Lấy phần mở rộng của file (jpg/png/...)
            string extension = Path.GetExtension(_imageName);
            string newFileName = employeeId + extension; // ví dụ: NV001.jpg
            string destPath = Path.Combine(folderPath, newFileName);
            _imageName = newFileName;

            destPath = Path.Combine(folderPath, newFileName);

            // ✅ Sao chép ảnh vào thư mục phần mềm
            File.Copy(imagePath, destPath, true);

            // ✅ Trả về đường dẫn tương đối (Images\NV001.jpg)
            return newFileName;
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ====== KIỂM TRA DỮ LIỆU ======
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("⚠️ Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                if (cbGender.SelectedIndex == -1)
                {
                    MessageBox.Show("⚠️ Vui lòng chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbGender.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show("⚠️ Vui lòng nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAddress.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtQue.Text))
                {
                    MessageBox.Show("⚠️ Vui lòng nhập quê quán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQue.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("⚠️ Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                // ====== KIỂM TRA ĐỊNH DẠNG EMAIL ======
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("⚠️ Địa chỉ email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                // ====== KIỂM TRA NGÀY SINH (>= 16 TUỔI) ======
                int tuoi = DateTime.Now.Year - dtDob.Value.Year;
                if (dtDob.Value > DateTime.Now.AddYears(-tuoi)) tuoi--; // Điều chỉnh nếu chưa qua sinh nhật
                if (tuoi < 16)
                {
                    MessageBox.Show("⚠️ Nhân viên phải từ 16 tuổi trở lên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtDob.Focus();
                    return;
                }

                // ====== NẾU DỮ LIỆU HỢP LỆ THÌ LƯU ======
                string savedFileName = SaveImageToFolder(imagePath, idNhanVien);


                DTONhanVien nv = new DTONhanVien
                {
                    ID = idNhanVien,
                    TenNhanVien = txtName.Text,
                    NgaySinh = dtDob.Value,
                    GioiTinh = cbGender.Text,
                    DiaChi = txtAddress.Text,
                    Que = txtQue.Text,
                    Email = txtEmail.Text,
                    AnhDaiDien = _imageName // 🟢 lưu đường dẫn ảnh
                };

                bllNhanVien.CapNhatThongTin(nv);

                MessageBox.Show("✅ Cập nhật thành công!", "Thông báo");

                var parentForm = this.FindForm();
                var parentControl = this.Parent;

                //MessageBox.Show($"Form cha: {parentForm?.Name}\nPanel cha: {parentControl?.Name}");

                // 🟢 Làm mới form hiển thị
                _panel.Controls.Clear();
                var xemThongTin = new XemThongTinCaNhan(idNhanVien, _panel, connectionString);
                _panel.Controls.Add(xemThongTin);

                XemThongTinCaNhan xemPage = new XemThongTinCaNhan(idNhanVien, _panel, connectionString);
                DisplayUserControlPanel.ChildUserControl(xemPage, _panel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            XemThongTinCaNhan xemPage = new XemThongTinCaNhan(idNhanVien, _panel, connectionString);
            DisplayUserControlPanel.ChildUserControl(xemPage, _panel);
        }

        private void LoadThongTinCaNhan()
        {
            var nv = bllNhanVien.LayThongTin(idNhanVien);
            if (nv != null)
            {
                txtName.Text = nv.TenNhanVien;
                dtDob.Value = nv.NgaySinh;
                cbGender.Text = nv.GioiTinh;
                txtAddress.Text = nv.DiaChi;
                txtQue.Text = nv.Que;
                txtEmail.Text = nv.Email;
                _imageName = nv.AnhDaiDien;
            }
            if (!string.IsNullOrEmpty(nv.AnhDaiDien))
            {
                string urlFolderImage = Path.Combine(AppContext.BaseDirectory, "image", nv.AnhDaiDien);
                string fullPath = Path.Combine(Application.StartupPath, urlFolderImage);
                if (fullPath.Contains("bin"))
                {
                    //Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
                    fullPath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName, "image", nv.AnhDaiDien);
                }
                if (File.Exists(fullPath))
                {
                    using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        picAvatar.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    picAvatar.Image = Properties.Resources.user; // ảnh mặc định
                }
            }
            else
            {
                picAvatar.Image = Properties.Resources.user;
            }

        }
    }
}

