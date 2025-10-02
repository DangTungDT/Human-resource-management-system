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
using System.Xml.Linq;

namespace GUI
{
    public partial class CapNhatThongTinNV : UserControl
    {
        private Guna2TextBox txtName, txtAddress, txtQue, txtEmail, txtPhone;
        private Guna2ComboBox cbGender;
        private Guna2DateTimePicker dtDob;
        private Guna2Button btnSave;
        public CapNhatThongTinNV()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;

            Label lblTitle = new Label()
            {
                Text = "Cập nhật thông tin cá nhân",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                ForeColor = Color.DarkBlue,
                AutoSize = true
            };

            // Các input control
            var txtName = new Guna2TextBox() { PlaceholderText = "Họ tên", Dock = DockStyle.Fill };
            var dtDob = new Guna2DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Dock = DockStyle.Fill
            };
            var cbGender = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbGender.Items.AddRange(new object[] { "Chọn giới tính", "Nam", "Nữ", "Khác" });
            cbGender.SelectedIndex = 0;

            var txtAddress = new Guna2TextBox() { PlaceholderText = "Địa chỉ", Dock = DockStyle.Fill };
            var txtEmail = new Guna2TextBox() { PlaceholderText = "Email", Dock = DockStyle.Fill };
            var txtPhone = new Guna2TextBox() { PlaceholderText = "Số điện thoại", Dock = DockStyle.Fill };

            var btnSave = new Guna2Button() { Text = "Lưu", Dock = DockStyle.Right, Width = 120 };
            btnSave.Click += (s, e) => MessageBox.Show("Đã lưu thông tin!");

            // Tạo TableLayoutPanel
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 8,
                Padding = new Padding(20),
                AutoScroll = true
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 07)); // Cột label
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 93)); // Cột input
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Trống

            // Thêm từng hàng (label + input)
            layout.Controls.Add(new Label() { Text = "Họ tên:", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.DarkBlue }, 0, 0);
            layout.Controls.Add(txtName, 1, 0);

            layout.Controls.Add(new Label() { Text = "Ngày sinh:", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.DarkBlue }, 0, 1);
            layout.Controls.Add(dtDob, 1, 1);

            layout.Controls.Add(new Label() { Text = "Giới tính:", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.DarkBlue }, 0, 2);
            layout.Controls.Add(cbGender, 1, 2);

            layout.Controls.Add(new Label() { Text = "Địa chỉ:", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.DarkBlue }, 0, 3);
            layout.Controls.Add(txtAddress, 1, 3);

            layout.Controls.Add(new Label() { Text = "Email:", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.DarkBlue }, 0, 4);
            layout.Controls.Add(txtEmail, 1, 4);

            layout.Controls.Add(new Label() { Text = "Số điện thoại:", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.DarkBlue }, 0, 5);
            layout.Controls.Add(txtPhone, 1, 5);

            btnSave.Anchor = AnchorStyles.Right;
            layout.Controls.Add(btnSave, 1, 6);

            // Thêm tiêu đề + layout chính
            var mainPanel = new Panel() { Dock = DockStyle.Fill, AutoScroll = true };
            mainPanel.Controls.Add(layout);

            this.Controls.Add(mainPanel);
            this.Controls.Add(lblTitle);
        }


    }
}
