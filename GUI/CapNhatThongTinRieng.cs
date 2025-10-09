using Guna.UI2.WinForms;
using System;
using System.Data.SqlClient;
using System.Drawing;
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
        private string imagePath = "";
        private string connectionString = ConnectionDB.conn;

        public CapNhatThongTinRieng(string idNV, Panel panel)
        {
            idNhanVien = idNV;
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
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
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
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
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
            cbGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
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
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
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
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
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
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
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
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Ảnh (*.jpg;*.png)|*.jpg;*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imagePath = dlg.FileName;
                picAvatar.Image = Image.FromFile(imagePath);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE NhanVien 
                                 SET TenNhanVien=@ten, NgaySinh=@ngay, GioiTinh=@gt, DiaChi=@dc, Que=@que, Email=@mail
                                 WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ten", txtName.Text);
                cmd.Parameters.AddWithValue("@ngay", dtDob.Value);
                cmd.Parameters.AddWithValue("@gt", cbGender.Text);
                cmd.Parameters.AddWithValue("@dc", txtAddress.Text);
                cmd.Parameters.AddWithValue("@que", txtQue.Text);
                cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@id", idNhanVien);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("✅ Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                XemThongTinCaNhan xemPage = new XemThongTinCaNhan(idNhanVien, _panel);
                var parent = this.ParentForm as Main;
                parent?.ShowUserControl("XemThongTinCaNhan");
                parent.ChildFormComponent(_panel, "ButtonFeatureViewComponent");
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            //XemThongTinCaNhan xemPage = new XemThongTinCaNhan(idNhanVien);
            //Control parent = this.Parent;
            //parent.Controls.Clear();
            //parent.Controls.Add(xemPage);

            XemThongTinCaNhan xemPage = new XemThongTinCaNhan(idNhanVien, _panel);
            var parent = this.ParentForm as Main;
            parent?.ShowUserControl("XemThongTinCaNhan");
            parent.ChildFormComponent(_panel, "ButtonFeatureViewComponent");
        }

        private void LoadThongTinCaNhan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT TenNhanVien, NgaySinh, GioiTinh, DiaChi, Que, Email FROM NhanVien WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idNhanVien);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtName.Text = reader["TenNhanVien"].ToString();
                    dtDob.Value = Convert.ToDateTime(reader["NgaySinh"]);
                    cbGender.Text = reader["GioiTinh"].ToString();
                    txtAddress.Text = reader["DiaChi"].ToString();
                    txtQue.Text = reader["Que"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                }
                reader.Close();
            }
        }
    }
}

