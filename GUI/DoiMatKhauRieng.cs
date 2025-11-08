using BLL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class DoiMatKhauRieng : UserControl
    {
        private Guna2TextBox txtOldPass, txtNewPass, txtConfirmPass;
        private Guna2Button btnSave, btnClear;
        private string idNhanVien;
        private BLLTaiKhoan taiKhoanBus;

        public DoiMatKhauRieng(string idNV, string conn)
        {
            InitializeComponent();
            idNhanVien = idNV;
            taiKhoanBus = new BLLTaiKhoan(conn);
            BuildUI();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // ====== Tiêu đề ======
            Label lblTitle = new Label()
            {
                Text = "🔒 ĐỔI MẬT KHẨU CÁ NHÂN",
                Font = new Font("Times New Roman", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 42, 62),
                Dock = DockStyle.Top,
                Height = 70, // giảm chiều cao cho sát top hơn
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 10, 0, 0)
            };

            // ====== Card chính ======
            Guna2Panel cardPanel = new Guna2Panel()
            {
                BorderRadius = 20,
                FillColor = Color.White,
                ShadowDecoration = { Enabled = true, Shadow = new Padding(4) },
                Padding = new Padding(50),
                Size = new Size(600, 450), // ✅ to hơn
                Anchor = AnchorStyles.None,
            };

            // ====== Label + Input ======
            Label lblOld = new Label() { Text = "Mật khẩu hiện tại", AutoSize = true, Font = new Font("Segoe UI", 11), ForeColor = Color.DimGray };
            Label lblNew = new Label() { Text = "Mật khẩu mới", AutoSize = true, Font = new Font("Segoe UI", 11), ForeColor = Color.DimGray };
            Label lblConfirm = new Label() { Text = "Xác nhận mật khẩu mới", AutoSize = true, Font = new Font("Segoe UI", 11), ForeColor = Color.DimGray };

            txtOldPass = new Guna2TextBox()
            {
                PlaceholderText = "Nhập mật khẩu hiện tại...",
                PasswordChar = '●',
                Width = 400,
                Height = 45,
                Font = new Font("Times New Roman", 12),
                BorderRadius = 10,
                IconLeft = Properties.Resources.locked
            };

            txtNewPass = new Guna2TextBox()
            {
                PlaceholderText = "Nhập mật khẩu mới...",
                PasswordChar = '●',
                Width = 400,
                Height = 45,
                Font = new Font("Times New Roman", 12),
                BorderRadius = 10,
                IconLeft = Properties.Resources.key
            };

            txtConfirmPass = new Guna2TextBox()
            {
                PlaceholderText = "Nhập lại mật khẩu mới...",
                PasswordChar = '●',
                Width = 400,
                Height = 45,
                Font = new Font("Times New Roman", 12),
                BorderRadius = 10,
                IconLeft = Properties.Resources.check__1_
            };

            // ====== Icon con mắt 👁 ẩn/hiện mật khẩu ======
            //txtOldPass.IconRight = Properties.Resources.eyebrow;
            //txtOldPass.IconRightClick += (s, e) => TogglePasswordVisibility(txtOldPass);

            //txtNewPass.IconRight = Properties.Resources.eyebrow;
            //txtNewPass.IconRightClick += (s, e) => TogglePasswordVisibility(txtNewPass);

            //txtConfirmPass.IconRight = Properties.Resources.eyebrow;
            //txtConfirmPass.IconRightClick += (s, e) => TogglePasswordVisibility(txtConfirmPass);

            // ====== Buttons ======
            btnSave = new Guna2Button()
            {
                Text = "💾 Lưu thay đổi",
                FillColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                BorderRadius = 10,
                Width = 180,
                Height = 50,
                Font = new Font("Times New Roman", 13, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.HoverState.FillColor = Color.FromArgb(56, 142, 60);
            btnSave.Click += BtnSave_Click;

            btnClear = new Guna2Button()
            {
                Text = "↩️ Làm mới",
                FillColor = Color.FromArgb(255, 160, 0),
                ForeColor = Color.White,
                BorderRadius = 10,
                Width = 150,
                Height = 50,
                Font = new Font("Times New Roman", 13, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClear.HoverState.FillColor = Color.FromArgb(255, 143, 0);
            btnClear.Click += (s, e) => { txtOldPass.Text = txtNewPass.Text = txtConfirmPass.Text = ""; };

            // ====== Layout trong card ======
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                AutoSize = true,
                RowCount = 7,
            };

            layout.Controls.Add(lblOld, 0, 0);
            layout.Controls.Add(txtOldPass, 0, 1);
            layout.Controls.Add(lblNew, 0, 2);
            layout.Controls.Add(txtNewPass, 0, 3);
            layout.Controls.Add(lblConfirm, 0, 4);
            layout.Controls.Add(txtConfirmPass, 0, 5);

            FlowLayoutPanel panelBtn = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Margin = new Padding(0, 25, 0, 0),
                Dock = DockStyle.Bottom
            };
            panelBtn.Controls.Add(btnSave);
            panelBtn.Controls.Add(btnClear);

            layout.Controls.Add(panelBtn, 0, 6);
            cardPanel.Controls.Add(layout);

            // ====== Container căn giữa (sát top hơn) ======
            Panel container = new Panel()
            {
                Dock = DockStyle.Fill
            };
            container.Controls.Add(cardPanel);

            // ✅ Căn giữa, nhưng cao hơn một chút (top sát hơn)
            cardPanel.Location = new Point(
                (container.Width - cardPanel.Width) / 2,
                (container.Height - cardPanel.Height) / 2 - 40 // đẩy card lên cao hơn
            );
            cardPanel.Anchor = AnchorStyles.None;

            this.Controls.Add(container);
            this.Controls.Add(lblTitle);
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            string oldPass = txtOldPass.Text.Trim();
            string newPass = txtNewPass.Text.Trim();
            string confirmPass = txtConfirmPass.Text.Trim();

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Kiểm tra mật khẩu cũ có đúng không
            if (!taiKhoanBus.KiemTraMatKhauCu(idNhanVien, oldPass))
            {
                MessageBox.Show("Mật khẩu hiện tại không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ✅ Kiểm tra nếu mật khẩu mới trùng mật khẩu cũ
            if (oldPass == newPass)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Cập nhật mật khẩu mới
            bool result = taiKhoanBus.DoiMatKhau(idNhanVien, newPass);

            if (result)
            {
                MessageBox.Show("✅ Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOldPass.Text = txtNewPass.Text = txtConfirmPass.Text = "";
            }
            else
            {
                MessageBox.Show("❌ Đổi mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TogglePasswordVisibility(Guna2TextBox textBox)
        {
            if (textBox.PasswordChar == '\0')
            {
                // Đang hiện → Ẩn đi
                textBox.PasswordChar = '●';// Ẩn mật khẩu
                textBox.IconRight = Properties.Resources.eyebrow;
            }
            else
            {
                // Đang ẩn → Hiện ra
                textBox.PasswordChar = '\0'; //Hiện mật khẩu
                textBox.IconRight = Properties.Resources.eye;
            }
        }
    }
}
