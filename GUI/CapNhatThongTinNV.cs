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
        //biến điều khiển (UI controls) được khai báo ở mức class-level để sau này có thể dùng trong nhiều hàm khác (như lấy dữ liệu, set dữ liệu từ DB).
        private Guna2TextBox txtName, txtAddress, txtQue, txtEmail, txtPhone;
        private Guna2ComboBox cbGender;
        private Guna2DateTimePicker dtDob;
        private Guna2Button btnSave;
        private Guna2DataGridView dgv; // commit: thêm dgv hiển thị danh sách nhân viên

        public CapNhatThongTinNV()
        {
            InitializeComponent();
            //gọi BuildUI() để dựng giao diện.
            BuildUI();
        }

        private void BuildUI()
        {
            //tự động chiếm hết không gian của form/parent container.
            this.Dock = DockStyle.Fill;

            //Tạo label tiêu đề, in đậm, màu xanh đậm, nằm trên cùng (DockStyle.Top).
            Label lblTitle = new Label()
            {
                Text = "Cập nhật thông tin cá nhân",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                ForeColor = Color.DarkBlue,
                AutoSize = true
            };

            // Các input control
            //  nhập họ tên, có placeholder “Họ tên”, tự động lấp đầy chiều ngang.
            txtName = new Guna2TextBox() { PlaceholderText = "Họ tên", Dock = DockStyle.Fill };
            //DateTimePicker chọn ngày sinh, hiển thị theo định dạng dd / MM / yyyy.
            dtDob = new Guna2DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Dock = DockStyle.Fill
            };
            //Combobox chọn giới tính, mặc định hiển thị "Chọn giới tính".
            cbGender = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbGender.Items.AddRange(new object[] { "Chọn giới tính", "Nam", "Nữ", "Khác" });
            cbGender.SelectedIndex = 0;

            //Các textbox nhập địa chỉ, email và số điện thoại.
            txtAddress = new Guna2TextBox() { PlaceholderText = "Địa chỉ", Dock = DockStyle.Fill };
            txtEmail = new Guna2TextBox() { PlaceholderText = "Email", Dock = DockStyle.Fill };
            txtPhone = new Guna2TextBox() { PlaceholderText = "Số điện thoại", Dock = DockStyle.Fill };

            //Nút “Lưu”, gắn sự kiện click → hiện hộp thoại thông báo đã lưu.
            btnSave = new Guna2Button()
            {
                Text = "💾 Lưu",
                AutoSize = false,
                Size = new Size(120, 40),
                Anchor = AnchorStyles.Right,
                BorderRadius = 6,
                //FillColor = Color.MediumSeaGreen,
                //Font = new Font("Segoe UI", 10, FontStyle.Bold),
                //ForeColor = Color.White
            };
            btnSave.Click += BtnSave_Click; // commit: lưu/cập nhật thông tin

            // Tạo TableLayoutPanel
            //  Bảng chia layout, có 3 cột và 8 hàng, thêm padding 20px, hỗ trợ cuộn dọc nếu nội dung dài
            TableLayoutPanel layoutForm = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 7,
                Padding = new Padding(10,10,0,90),
                AutoScroll = true
            };
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 07)); // cột label
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80)); // cột input
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13)); // cột trống

            // Thêm từng hàng (label + input)
            layoutForm.Controls.Add(new Label() { Text = "Họ tên:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 0);
            layoutForm.Controls.Add(txtName, 1, 0);

            layoutForm.Controls.Add(new Label() { Text = "Ngày sinh:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 1);
            layoutForm.Controls.Add(dtDob, 1, 1);

            layoutForm.Controls.Add(new Label() { Text = "Giới tính:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 2);
            layoutForm.Controls.Add(cbGender, 1, 2);

            layoutForm.Controls.Add(new Label() { Text = "Địa chỉ:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 3);
            layoutForm.Controls.Add(txtAddress, 1, 3);

            layoutForm.Controls.Add(new Label() { Text = "Email:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 4);
            layoutForm.Controls.Add(txtEmail, 1, 4);

            layoutForm.Controls.Add(new Label() { Text = "Số điện thoại:", Anchor = AnchorStyles.Left, AutoSize = true, ForeColor = Color.DarkBlue }, 0, 5);
            layoutForm.Controls.Add(txtPhone, 1, 5);

            layoutForm.Controls.Add(btnSave, 1, 6);

            // ===== DATAGRIDVIEW =====
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgv.Columns.Add("Name", "Họ tên");
            dgv.Columns.Add("DOB", "Ngày sinh");
            dgv.Columns.Add("Gender", "Giới tính");
            dgv.Columns.Add("Address", "Địa chỉ");
            dgv.Columns.Add("Email", "Email");
            dgv.Columns.Add("Phone", "SĐT");

            // commit: thêm cột nút xóa
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "Xóa";
            btnDelete.Text = "Xóa";
            btnDelete.UseColumnTextForButtonValue = true;
            dgv.Columns.Add(btnDelete);

            dgv.CellClick += Dgv_CellClick; // commit: click row để load dữ liệu hoặc xóa

            // ===== LAYOUT TỔNG =====
            TableLayoutPanel layoutTotal = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // form chiếm 40%
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // dgv chiếm 60%

            layoutTotal.Controls.Add(layoutForm, 0, 0);
            layoutTotal.Controls.Add(dgv, 0, 1);

            this.Controls.Add(layoutTotal);
            this.Controls.Add(lblTitle);

        }

        // ===== XỬ LÝ NÚT LƯU =====
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0) // commit: cập nhật nếu đã chọn row
            {
                DataGridViewRow row = dgv.SelectedRows[0];
                row.Cells["Name"].Value = txtName.Text;
                row.Cells["DOB"].Value = dtDob.Value.ToShortDateString();
                row.Cells["Gender"].Value = cbGender.Text;
                row.Cells["Address"].Value = txtAddress.Text;
                row.Cells["Email"].Value = txtEmail.Text;
                row.Cells["Phone"].Value = txtPhone.Text;
                MessageBox.Show("Đã cập nhật thông tin!");
            }
            else // commit: thêm mới
            {
                dgv.Rows.Add(txtName.Text, dtDob.Value.ToShortDateString(), cbGender.Text, txtAddress.Text, txtEmail.Text, txtPhone.Text);
                MessageBox.Show("Đã thêm nhân viên mới!");
            }

            ClearForm(); // commit: xóa form sau khi lưu
        }

        // ===== CLICK VÀO DGV =====
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn) // commit: xóa row
                {
                    if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        dgv.Rows.RemoveAt(e.RowIndex);
                        ClearForm();
                    }
                    return;
                }

                // commit: click row -> load dữ liệu lên form
                DataGridViewRow row = dgv.Rows[e.RowIndex];
                txtName.Text = row.Cells["Name"].Value?.ToString();
                dtDob.Value = DateTime.TryParse(row.Cells["DOB"].Value?.ToString(), out DateTime dt) ? dt : DateTime.Now;
                cbGender.Text = row.Cells["Gender"].Value?.ToString();
                txtAddress.Text = row.Cells["Address"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
            }
        }

        // ===== HÀM XÓA FORM =====
        private void ClearForm()
        {
            txtName.Clear();
            dtDob.Value = DateTime.Now;
            cbGender.SelectedIndex = 0;
            txtAddress.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            dgv.ClearSelection(); // commit: bỏ chọn row
        }
    }
}

