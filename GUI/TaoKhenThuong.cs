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
    public partial class TaoKhenThuong : UserControl
    {
        // ===== KHAI BÁO BIẾN =====
        private Guna2TextBox txtReason;
        private Guna2ComboBox cbEmployee,cbRewardType;
        private Guna2DateTimePicker dtRewardDate;
        private Guna2Button btnSave;
        private Guna2DataGridView dgvReward;

        public TaoKhenThuong()
        {
            InitializeComponent();
            BuildUI(); // commit: Xây dựng giao diện khi tạo UserControl
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill; // commit: Chiếm toàn bộ khung cha

            // ===== TIÊU ĐỀ =====
            Label lblTitle = new Label()
            {
                Text = "Tạo khen thưởng",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Top,
                AutoSize = true,
                ForeColor = Color.DarkBlue // commit: màu xanh đậm
            };

            // ===== INPUT CONTROLS =====
            cbEmployee = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbEmployee.Items.AddRange(new object[] { "NV001 - Nguyễn Văn A", "NV002 - Trần Thị B" });

            cbRewardType = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbRewardType.Items.AddRange(new object[] { "Thưởng tiền", "Thưởng hiện vật", "Thăng chức", "Khác" });
            cbRewardType.SelectedIndex = 0;

            dtRewardDate = new Guna2DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Dock = DockStyle.Fill
            };

            txtReason = new Guna2TextBox() { PlaceholderText = "Lý do khen thưởng", Dock = DockStyle.Fill, Multiline = true, Height = 80 };

            btnSave = new Guna2Button() { Text = "Lưu", Width = 120, Anchor = AnchorStyles.Right };
            btnSave.Click += BtnSave_Click;

            // ===== FORM NHẬP DỮ LIỆU (TableLayoutPanel) =====
            TableLayoutPanel formLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 5,
                Padding = new Padding(10, 10, 0, 90),
                AutoScroll = true0.
            };
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 07));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13));

            formLayout.Controls.Add(new Label() { Text = "Nhân viên:", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.DarkBlue }, 0, 0);
            formLayout.Controls.Add(cbEmployee, 1, 0);
            formLayout.Controls.Add(new Label() { Text = "Hình thức:", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.DarkBlue }, 0, 1);
            formLayout.Controls.Add(cbRewardType, 1, 1);
            formLayout.Controls.Add(new Label() { Text = "Ngày thưởng:", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.DarkBlue }, 0, 2);
            formLayout.Controls.Add(dtRewardDate, 1, 2);
            formLayout.Controls.Add(new Label() { Text = "Lý do:", AutoSize = true, Anchor = AnchorStyles.Left, ForeColor = Color.DarkBlue }, 0, 3);
            formLayout.Controls.Add(txtReason, 1, 3);
            formLayout.Controls.Add(btnSave, 1, 4);

            // ===== DATAGRIDVIEW HIỂN THỊ DANH SÁCH =====
            dgvReward = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            dgvReward.Columns.Add("Employee", "Nhân viên");
            dgvReward.Columns.Add("RewardType", "Hình thức");
            dgvReward.Columns.Add("RewardDate", "Ngày thưởng");
            dgvReward.Columns.Add("Reason", "Lý do");

            // commit: Thêm nút Xóa
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "Xóa";
            btnDelete.Text = "Xóa";
            btnDelete.UseColumnTextForButtonValue = true;
            dgvReward.Columns.Add(btnDelete);
            dgvReward.CellClick += DgvReward_CellClick;

            // ===== LAYOUT TỔNG CHIA 2 =====
            TableLayoutPanel mainLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,  // commit: 2 phần -> Form nhập / DataGridView
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45));  // Form nhập
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 55));  // DataGridView

            mainLayout.Controls.Add(formLayout, 0, 0);
            mainLayout.Controls.Add(dgvReward, 0, 1);

            // ===== THÊM TIÊU ĐỀ + LAYOUT TỔNG =====
            this.Controls.Add(mainLayout);
            this.Controls.Add(lblTitle); // commit: Tiêu đề luôn ở trên cùng
        }

        // ===== XỬ LÝ NÚT LƯU =====
        private void BtnSave_Click(object sender, EventArgs e)
        {
            dgvReward.Rows.Add(
                cbEmployee.Text,
                cbRewardType.SelectedItem?.ToString(),
                dtRewardDate.Value.ToString("dd/MM/yyyy"),
                txtReason.Text
            );

            MessageBox.Show("Đã lưu thông tin khen thưởng!");
            ClearForm();
        }

        // ===== XÓA FORM =====
        private void ClearForm()
        {
            cbEmployee.SelectedIndex = -1;
            cbRewardType.SelectedIndex = 0;
            dtRewardDate.Value = DateTime.Now;
            txtReason.Clear();
        }

        // ===== XỬ LÝ NÚT XÓA TRONG DATAGRIDVIEW =====
        private void DgvReward_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvReward.Columns["Xóa"].Index)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa dòng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dgvReward.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
    }

}
