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
    public partial class TaoKyLuat : UserControl
    {
        // ===== BIẾN TOÀN CỤC =====
        private Guna2ComboBox cbEmployee, cbType;
        private Guna2TextBox txtReason;
        private Guna2DateTimePicker dtDiscipline;
        private Guna2Button btnSave;
        private Guna2DataGridView dgv; // Bảng hiển thị kỷ luật

        public TaoKyLuat()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;

            // ===== TIÊU ĐỀ =====
            Label lblTitle = new Label()
            {
                Text = "Kỷ luật nhân viên",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Top,
                ForeColor = Color.DarkBlue,
                AutoSize = true
            };

            // ===== INPUT CONTROL =====
            cbEmployee = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbEmployee.Items.AddRange(new object[] { "NV001 - Nguyễn Văn A", "NV002 - Trần Thị B" });

            dtDiscipline = new Guna2DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Dock = DockStyle.Fill,
                //FillColor = Color.White,                 // nền trắng
                //ForeColor = Color.LightSkyBlue,         // chữ xanh biển nhạt
                //BorderColor = Color.Black,          // viền nhạt hơn
                //BorderRadius = 5
            };

            cbType = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbType.Items.AddRange(new object[] { "Khiển trách", "Cảnh cáo", "Đình chỉ", "Sa thải" });
            cbType.SelectedIndex = 0;

            txtReason = new Guna2TextBox() { PlaceholderText = "Lý do", Dock = DockStyle.Fill };

            btnSave = new Guna2Button() { Text = "Lưu kỷ luật", Width = 150, Anchor = AnchorStyles.Right };
            btnSave.Click += BtnSave_Click;

            // ===== DATAGRIDVIEW =====
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgv.Columns.Add("Employee", "Nhân viên");
            dgv.Columns.Add("Date", "Ngày");
            dgv.Columns.Add("Type", "Hình thức");
            dgv.Columns.Add("Reason", "Lý do");

            //// commit: thêm cột nút xóa
            //DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            //btnDelete.Name = "Xoa";
            //btnDelete.HeaderText = "Xóa";
            //btnDelete.Text = "Xóa";
            //btnDelete.UseColumnTextForButtonValue = true;
            //dgv.Columns.Add(btnDelete);
            //dgv.CellClick += Dgv_CellClick;     //bắt sự kiện click

            // ===== CỘT ICON XÓA =====
            DataGridViewImageColumn colDelete = new DataGridViewImageColumn();
            colDelete.Name = "Xoa";                     // 👈 BẮT BUỘC
            colDelete.HeaderText = "Xóa";
            colDelete.Image = Properties.Resources.delete;
            colDelete.ImageLayout = DataGridViewImageCellLayout.Zoom;
            colDelete.Width = 50;
            dgv.Columns.Add(colDelete);
            dgv.CellClick += DgvReward_CellClick;     //bắt sự kiện click
            dgv.CellMouseEnter += dgvReward_CellMouseEnter;  //bắt sự kiện hover
            dgv.CellMouseLeave += dgvReward_CellMouseLeave;

            // ===== TABLE LAYOUT FORM NHẬP =====
            TableLayoutPanel formLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 5,
                Padding = new Padding(10, 10, 0, 90),
                AutoScroll = true
            };
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 07));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            formLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13));

            formLayout.Controls.Add(new Label() { Text = "Nhân viên:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 0);
            formLayout.Controls.Add(cbEmployee, 1, 0);

            formLayout.Controls.Add(new Label() { Text = "Ngày:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 1);
            formLayout.Controls.Add(dtDiscipline, 1, 1);

            formLayout.Controls.Add(new Label() { Text = "Hình thức:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 2);
            formLayout.Controls.Add(cbType, 1, 2);

            formLayout.Controls.Add(new Label() { Text = "Lý do:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 3);
            formLayout.Controls.Add(txtReason, 1, 3);

            formLayout.Controls.Add(btnSave, 1, 4);

            // ===== LAYOUT TỔNG =====
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 37));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 63));

            layout.Controls.Add(formLayout, 0, 0);
            layout.Controls.Add(dgv, 0, 1);

            this.Controls.Add(layout);
            this.Controls.Add(lblTitle);
        }

        // ===== SỰ KIỆN LƯU =====
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0) // commit: nếu đã chọn row -> cập nhật
            {
                DataGridViewRow row = dgv.SelectedRows[0];
                row.Cells["Employee"].Value = cbEmployee.Text;
                row.Cells["Date"].Value = dtDiscipline.Value.ToShortDateString();
                row.Cells["Type"].Value = cbType.Text;
                row.Cells["Reason"].Value = txtReason.Text;
                MessageBox.Show("Đã cập nhật thông tin kỷ luật!");
            }
            else // commit: thêm mới
            {
                dgv.Rows.Add(cbEmployee.Text, dtDiscipline.Value.ToShortDateString(), cbType.Text, txtReason.Text);
                MessageBox.Show("Đã lưu thông tin kỷ luật!");
            }

            ClearForm(); // commit: xóa form sau khi lưu
        }

        // ===== CLICK VÀO DGV =====
        //private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn) // commit: xóa row
        //        {
        //            if (MessageBox.Show("Bạn có chắc muốn xóa kỷ luật này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //            {
        //                dgv.Rows.RemoveAt(e.RowIndex);
        //                ClearForm();
        //            }
        //            return;
        //        }

        //        // commit: click row -> load dữ liệu lên form để sửa
        //        DataGridViewRow row = dgv.Rows[e.RowIndex];
        //        cbEmployee.Text = row.Cells["Employee"].Value?.ToString();
        //        dtDiscipline.Value = DateTime.TryParse(row.Cells["Date"].Value?.ToString(), out DateTime dt) ? dt : DateTime.Now;
        //        cbType.Text = row.Cells["Type"].Value?.ToString();
        //        txtReason.Text = row.Cells["Reason"].Value?.ToString();
        //    }
        //}

        private void DgvReward_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua header hoặc click ngoài vùng dữ liệu
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // Xác định có phải click vào cột icon Xóa không
            var column = dgv.Columns[e.ColumnIndex];
            if (column is DataGridViewImageColumn && column.Name == "Xoa")
            {
                // Hiện hộp thoại xác nhận
                var confirm = MessageBox.Show("Bạn có chắc muốn xóa dòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    dgv.Rows.RemoveAt(e.RowIndex);
                }
            }

            // commit: click row -> load dữ liệu lên form
            DataGridViewRow row = dgv.Rows[e.RowIndex];
            cbEmployee.Text = row.Cells["Employee"].Value?.ToString();
            dtDiscipline.Value = DateTime.TryParse(row.Cells["Date"].Value?.ToString(), out DateTime dt) ? dt : DateTime.Now;
            cbType.Text = row.Cells["Type"].Value?.ToString();
            txtReason.Text = row.Cells["Reason"].Value?.ToString();
        }

        //icon “đổi màu” khi rê chuột
        private void dgvReward_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Hand;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.trash; // icon khi hover
            }
        }

        private void dgvReward_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Default;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.delete; // icon bình thường
            }
        }

        // ===== HÀM XÓA FORM =====
        private void ClearForm()
        {
            cbEmployee.SelectedIndex = -1;
            dtDiscipline.Value = DateTime.Now;
            cbType.SelectedIndex = 0;
            txtReason.Clear();
            dgv.ClearSelection(); // commit: bỏ chọn row
        }
    }
}

