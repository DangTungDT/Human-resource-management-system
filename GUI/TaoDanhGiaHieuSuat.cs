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
        // ===== BIẾN TOÀN CỤC =====
        private Guna2ComboBox cbEmployee;
        private Guna2DateTimePicker dtReview;
        private NumericUpDown numScore;
        private Guna2TextBox txtNote;
        private Guna2Button btnSave;
        private Guna2DataGridView dgv;

        public TaoDanhGiaHieuSuat()
        {
            InitializeComponent();
            //gọi BuildUI() để dựng giao diện
            BuildUI();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill; // chiếm hết parent container

            // ===== TIÊU ĐỀ =====
            Label lblTitle = new Label()
            {
                Text = "Đánh giá hiệu suất nhân viên",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Top,
                ForeColor = Color.DarkBlue,
                AutoSize = true
            };

            // ===== INPUT CONTROL =====
            cbEmployee = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbEmployee.Items.AddRange(new object[] { "NV001 - Nguyễn Văn A", "NV002 - Trần Thị B" });

            dtReview = new Guna2DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Dock = DockStyle.Fill
            };

            numScore = new NumericUpDown() { Minimum = 0, Maximum = 100, Dock = DockStyle.Fill };
            txtNote = new Guna2TextBox() { PlaceholderText = "Nhận xét", Dock = DockStyle.Fill };

            btnSave = new Guna2Button() { Text = "Lưu đánh giá", Width = 150, Anchor = AnchorStyles.Right };
            btnSave.Click += BtnSave_Click; // commit: xử lý lưu dữ liệu

            // ===== DATAGRIDVIEW =====
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 30 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            // Thêm cột
            dgv.Columns.Add("Employee", "Nhân viên");
            dgv.Columns.Add("Date", "Ngày");
            dgv.Columns.Add("Score", "Điểm");
            dgv.Columns.Add("Note", "Nhận xét");

            // ===== COMMIT: Thêm cột nút xóa =====
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "Xóa";
            btnDelete.Text = "Xóa";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.Width = 60;
            dgv.Columns.Add(btnDelete);

            dgv.CellClick += Dgv_CellClick; // commit: click row để load dữ liệu lên form

            // ===== TABLE LAYOUT FORM NHẬP =====
            TableLayoutPanel formLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 5,
                Padding = new Padding(10, 10, 0, 90),
                AutoScroll = true
            };
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 07)); // cột label
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80)); // cột input
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13)); // cột trống

            formLayout.Controls.Add(new Label() { Text = "Nhân viên:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 0);
            formLayout.Controls.Add(cbEmployee, 1, 0);

            formLayout.Controls.Add(new Label() { Text = "Ngày đánh giá:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 1);
            formLayout.Controls.Add(dtReview, 1, 1);

            formLayout.Controls.Add(new Label() { Text = "Điểm:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 2);
            formLayout.Controls.Add(numScore, 1, 2);

            formLayout.Controls.Add(new Label() { Text = "Nhận xét:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 3);
            formLayout.Controls.Add(txtNote, 1, 3);

            formLayout.Controls.Add(btnSave, 1, 4);

            // ===== LAYOUT TỔNG =====
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 35)); // form chiếm 35%
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 65)); // dgv chiếm 65%

            layout.Controls.Add(formLayout, 0, 0);
            layout.Controls.Add(dgv, 0, 1);

            this.Controls.Add(layout);
            this.Controls.Add(lblTitle);
        }

        // ===== XỬ LÝ NÚT LƯU =====
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0) // commit: nếu đã chọn row -> cập nhật
            {
                DataGridViewRow row = dgv.SelectedRows[0];
                row.Cells["Employee"].Value = cbEmployee.Text;
                row.Cells["Date"].Value = dtReview.Value.ToShortDateString();
                row.Cells["Score"].Value = numScore.Value;
                row.Cells["Note"].Value = txtNote.Text;
                MessageBox.Show("Đã cập nhật đánh giá!");
            }
            else // commit: thêm mới
            {
                dgv.Rows.Add(cbEmployee.Text, dtReview.Value.ToShortDateString(), numScore.Value, txtNote.Text);
                MessageBox.Show("Đã lưu đánh giá mới!");
            }

            ClearForm(); // commit: xóa form sau khi lưu
        }

        // ===== CLICK VÀO DGV =====
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // commit: tránh click header
            {
                // commit: XỬ LÝ NÚT XÓA
                if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (MessageBox.Show("Bạn có chắc muốn xóa đánh giá này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        dgv.Rows.RemoveAt(e.RowIndex);
                        ClearForm();
                    }
                    return;
                }

                DataGridViewRow row = dgv.Rows[e.RowIndex];
                cbEmployee.Text = row.Cells["Employee"].Value?.ToString();
                dtReview.Value = DateTime.TryParse(row.Cells["Date"].Value?.ToString(), out DateTime dt) ? dt : DateTime.Now;
                numScore.Value = row.Cells["Score"].Value != null ? Convert.ToDecimal(row.Cells["Score"].Value) : 0;
                txtNote.Text = row.Cells["Note"].Value?.ToString();
            }
        }

        // ===== HÀM XÓA FORM =====
        private void ClearForm()
        {
            cbEmployee.SelectedIndex = -1;
            dtReview.Value = DateTime.Now;
            numScore.Value = 0;
            txtNote.Clear();
            dgv.ClearSelection(); // commit: bỏ chọn row
        }
    }

}
