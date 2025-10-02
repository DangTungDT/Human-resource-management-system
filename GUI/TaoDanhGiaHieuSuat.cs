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
    public partial class TaoDanhGiaHieuSuat : UserControl
    {
        private Guna2DataGridView dgv;
        public TaoDanhGiaHieuSuat()
        {
            InitializeComponent();
            BuildUI();
        }
        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;

            Label lblTitle = new Label()
            {
                Text = "Đánh giá hiệu suất nhân viên",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                ForeColor = Color.DarkBlue,
                AutoSize = true
            };

            // Các input control
            var cbEmployee = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbEmployee.Items.AddRange(new object[] { "NV001 - Nguyễn Văn A", "NV002 - Trần Thị B" });

            var dtReview = new Guna2DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Dock = DockStyle.Fill
            };

            var numScore = new NumericUpDown() { Minimum = 0, Maximum = 100, Dock = DockStyle.Fill };

            var txtNote = new Guna2TextBox() { PlaceholderText = "Nhận xét", Dock = DockStyle.Fill };

            var btnSave = new Guna2Button() { Text = "Lưu đánh giá", Dock = DockStyle.Right, Width = 150 };

            var dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgv.Columns.Add("Employee", "Nhân viên");
            dgv.Columns.Add("Date", "Ngày");
            dgv.Columns.Add("Score", "Điểm");
            dgv.Columns.Add("Note", "Nhận xét");

            btnSave.Click += (s, e) =>
            {
                dgv.Rows.Add(cbEmployee.Text, dtReview.Value.ToShortDateString(), numScore.Value, txtNote.Text);
            };

            // Tạo TableLayoutPanel cho form nhập
            TableLayoutPanel formLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 5,
                Padding = new Padding(20)
            };
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            formLayout.Controls.Add(new Label() { Text = "Nhân viên:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 0);
            formLayout.Controls.Add(cbEmployee, 1, 0);

            formLayout.Controls.Add(new Label() { Text = "Ngày đánh giá:", Anchor = AnchorStyles.Left, AutoSize = true , ForeColor = Color.DarkBlue }, 0, 1);
            formLayout.Controls.Add(dtReview, 1, 1);

            formLayout.Controls.Add(new Label() { Text = "Điểm:", Anchor = AnchorStyles.Left, AutoSize = true , ForeColor = Color.DarkBlue }, 0, 2);
            formLayout.Controls.Add(numScore, 1, 2);

            formLayout.Controls.Add(new Label() { Text = "Nhận xét:", Anchor = AnchorStyles.Left, AutoSize = true , ForeColor = Color.DarkBlue }, 0, 3);
            formLayout.Controls.Add(txtNote, 1, 3);

            formLayout.Controls.Add(btnSave, 1, 4);

            // Layout tổng
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 35)); // Form chiếm 35%
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 65)); // Grid chiếm 65%

            layout.Controls.Add(formLayout, 0, 0);
            layout.Controls.Add(dgv, 0, 1);

            this.Controls.Add(layout);
            this.Controls.Add(lblTitle);
        }
    }
}
