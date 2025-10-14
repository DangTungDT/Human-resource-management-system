using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI
{
    public partial class CapNhatThongTinNV : UserControl
    {
        // ===== BIẾN TOÀN CỤC =====
        private Guna2TextBox txtName, txtAddress, txtEmail, txtQue;
        private Guna2ComboBox cbGender, cbPhongBan, cbChucVu;
        private Guna2DateTimePicker dtDob;
        private Guna2Button btnSave, btnToggleView, btnAdd;
        private Guna2DataGridView dgv;
        private Guna2TextBox txtSearchName, txtSearchAddress, txtSearchQue, txtSearchEmail;
        private Guna2ComboBox cbSearchGender;
        private BLLNhanVien nvBus;

        private string connectionString;
        private string selectedId = null;
        private bool isViewingHidden = false;

        public CapNhatThongTinNV(string conn)
        {
            connectionString = conn;
            nvBus = new BLLNhanVien(conn);
            InitializeComponent();
            BuildUI();
            LoadDanhSachNhanVien(false);
            LoadPhongBan();
            LoadChucVu();
        }

        // ======================= DỰNG GIAO DIỆN =======================
        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            Label lblTitle = new Label()
            {
                Text = "CẬP NHẬT THÔNG TIN NHÂN VIÊN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Top,
                ForeColor = Color.DarkBlue,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ===== THANH TÌM KIẾM =====
            FlowLayoutPanel searchPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(20, 5, 20, 5),
                AutoSize = true,
                BackColor = Color.FromArgb(245, 247, 250)
            };

            txtSearchName = new Guna2TextBox() { PlaceholderText = "🔍 Họ tên", Width = 180 };
            cbSearchGender = new Guna2ComboBox() { Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
            cbSearchGender.Items.AddRange(new object[] { "", "Nam", "Nữ", "Khác" });
            txtSearchAddress = new Guna2TextBox() { PlaceholderText = "Địa chỉ", Width = 180 };
            txtSearchQue = new Guna2TextBox() { PlaceholderText = "Quê quán", Width = 180 };
            txtSearchEmail = new Guna2TextBox() { PlaceholderText = "Email", Width = 180 };

            searchPanel.Controls.AddRange(new Control[] {
                new Label() { Text="🔍 TÌM KIẾM:", AutoSize=true, Font=new Font("Segoe UI",10,FontStyle.Bold), ForeColor=Color.DimGray, Margin=new Padding(0,8,10,0) },
                txtSearchName, cbSearchGender, txtSearchAddress, txtSearchQue, txtSearchEmail
            });

            txtSearchName.TextChanged += (s, e) => FilterNhanVien();
            cbSearchGender.SelectedIndexChanged += (s, e) => FilterNhanVien();
            txtSearchAddress.TextChanged += (s, e) => FilterNhanVien();
            txtSearchQue.TextChanged += (s, e) => FilterNhanVien();
            txtSearchEmail.TextChanged += (s, e) => FilterNhanVien();

            // ===== INPUT =====
            txtName = new Guna2TextBox() { PlaceholderText = "Họ tên", Dock = DockStyle.Fill };
            dtDob = new Guna2DateTimePicker() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Dock = DockStyle.Fill };
            cbGender = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            txtAddress = new Guna2TextBox() { PlaceholderText = "Địa chỉ", Dock = DockStyle.Fill };
            txtEmail = new Guna2TextBox() { PlaceholderText = "Email", Dock = DockStyle.Fill };
            txtQue = new Guna2TextBox() { PlaceholderText = "Quê quán", Dock = DockStyle.Fill };
            cbPhongBan = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbChucVu = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

            btnSave = new Guna2Button()
            {
                Text = "💾 Lưu thay đổi",
                Size = new Size(140, 40),
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnSave.Click += BtnSave_Click;

            btnAdd = new Guna2Button()
            {
                Text = "↩️ Hoàn tác",
                Size = new Size(120, 40),
                BorderRadius = 8,
                FillColor = Color.Goldenrod,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnAdd.Click += BtnAdd_Click;

            btnToggleView = new Guna2Button()
            {
                Text = "👁 Hiển thị nhân viên đã ẩn",
                Size = new Size(220, 40),
                BorderRadius = 8,
                FillColor = Color.SteelBlue,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnToggleView.Click += BtnToggleView_Click;

            // ===== FORM =====
            TableLayoutPanel layoutForm = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                ColumnCount = 3,
                RowCount = 9,
                Padding = new Padding(20),
                Height = 420
            };
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 07));
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13));

            layoutForm.Controls.Add(new Label() { Text = "Họ tên:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
            layoutForm.Controls.Add(txtName, 1, 0);
            layoutForm.Controls.Add(new Label() { Text = "Ngày sinh:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            layoutForm.Controls.Add(dtDob, 1, 1);
            layoutForm.Controls.Add(new Label() { Text = "Giới tính:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 2);
            layoutForm.Controls.Add(cbGender, 1, 2);
            layoutForm.Controls.Add(new Label() { Text = "Phòng ban:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 3);
            layoutForm.Controls.Add(cbPhongBan, 1, 3);
            layoutForm.Controls.Add(new Label() { Text = "Chức vụ:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 4);
            layoutForm.Controls.Add(cbChucVu, 1, 4);
            layoutForm.Controls.Add(new Label() { Text = "Địa chỉ:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 5);
            layoutForm.Controls.Add(txtAddress, 1, 5);
            layoutForm.Controls.Add(new Label() { Text = "Quê quán:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 6);
            layoutForm.Controls.Add(txtQue, 1, 6);
            layoutForm.Controls.Add(new Label() { Text = "Email:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 7);
            layoutForm.Controls.Add(txtEmail, 1, 7);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnToggleView);
            layoutForm.Controls.Add(buttonPanel, 1, 8);

            // ===== DGV =====
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                ReadOnly = true
            };

            // ===== Thêm cột Action nếu chưa có =====
            if (!dgv.Columns.Contains("Action"))
            {
                DataGridViewImageColumn colAction = new DataGridViewImageColumn()
                {
                    Name = "Action",
                    HeaderText = "Thao tác",
                    Image = Properties.Resources.delete, // icon thùng rác hoặc hide
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 50
                };
                dgv.Columns.Add(colAction);
            }


            // gắn sự kiện
            dgv.CellClick += Dgv_CellClick;
            dgv.CellMouseEnter += Dgv_CellMouseEnter;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;

            // ===== MAIN =====
            TableLayoutPanel layoutTotal = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Absolute, 430));
            layoutTotal.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            layoutTotal.Controls.Add(lblTitle, 0, 0);
            layoutTotal.Controls.Add(searchPanel, 0, 1);
            layoutTotal.Controls.Add(layoutForm, 0, 2);
            layoutTotal.Controls.Add(dgv, 0, 3);

            this.Controls.Add(layoutTotal);
        }

        // ======================= LOAD DANH SÁCH =======================
        private void LoadDanhSachNhanVien(bool showHidden)
        {
            dgv.DataSource = nvBus.GetDanhSachNhanVien(showHidden);
        }

        private void LoadPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT id, TenPhongBan FROM PhongBan", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbPhongBan.DataSource = dt;
                cbPhongBan.DisplayMember = "TenPhongBan";
                cbPhongBan.ValueMember = "id";
                cbPhongBan.SelectedIndex = -1;
            }
        }

        private void LoadChucVu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT id, TenChucVu FROM ChucVu", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbChucVu.DataSource = dt;
                cbChucVu.DisplayMember = "TenChucVu";
                cbChucVu.ValueMember = "id";
                cbChucVu.SelectedIndex = -1;
            }
        }

        // =======================
        // 3️⃣ CLICK DGV
        // =======================
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Lấy mã nhân viên
            string idNV = dgv.Rows[e.RowIndex].Cells["Mã NV"].Value.ToString();

            // ====== Nếu click vào cột Action ======
            if (dgv.Columns[e.ColumnIndex].Name == "Action")
            {
                if (isViewingHidden) KhoiPhucNhanVien(idNV);
                else AnNhanVien(idNV);
                return;
            }

            // ====== Gán dữ liệu lên input ======
            selectedId = idNV;
            DataGridViewRow row = dgv.Rows[e.RowIndex];
            txtName.Text = row.Cells["Họ tên"].Value.ToString();
            cbGender.Text = row.Cells["Giới tính"].Value.ToString();
            txtAddress.Text = row.Cells["Địa chỉ"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtQue.Text = row.Cells["Quê quán"].Value.ToString();

            if (DateTime.TryParse(row.Cells["Ngày sinh"].Value.ToString(), out DateTime dob))
                dtDob.Value = dob;

            // ====== Lấy Phòng ban & Chức vụ từ DB ======
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT idPhongBan, idChucVu FROM NhanVien WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", idNV);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Phòng ban
                        if (reader["idPhongBan"] != DBNull.Value)
                            cbPhongBan.SelectedValue = reader["idPhongBan"].ToString();
                        else
                            cbPhongBan.SelectedIndex = -1;

                        // Chức vụ
                        if (reader["idChucVu"] != DBNull.Value)
                            cbChucVu.SelectedValue = reader["idChucVu"].ToString();
                        else
                            cbChucVu.SelectedIndex = -1;
                    }
                }
            }

        }


        // =======================
        // 4️⃣ ẨN / KHÔI PHỤC
        // =======================
        private void AnNhanVien(string id)
        {
            if (MessageBox.Show("Bạn có chắc muốn ẩn nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                nvBus.AnNhanVien(id);
                LoadDanhSachNhanVien(false);
            }
        }

        private void KhoiPhucNhanVien(string id)
        {
            if (MessageBox.Show("Bạn có chắc muốn khôi phục nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                nvBus.KhoiPhucNhanVien(id);
                LoadDanhSachNhanVien(true);
            }

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // ===== Kiểm tra dữ liệu =====
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string TenChucVu = cbChucVu.Text;
            string TenPhongBan = cbPhongBan.Text;

            // ===== Tạo đối tượng DTO =====
            DTONhanVien nv = new DTONhanVien
            {
                ID = selectedId, // dùng khi cập nhật
                TenNhanVien = txtName.Text.Trim(),
                NgaySinh = dtDob.Value,
                GioiTinh = cbGender.Text,
                DiaChi = txtAddress.Text.Trim(),
                Que = txtQue.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                IdChucVu = cbChucVu.SelectedValue.ToString(),
                IdPhongBan = cbPhongBan.SelectedValue.ToString()
            };

            // ===== Phân biệt thêm mới / cập nhật =====
            if (string.IsNullOrEmpty(selectedId))
            {
                // Thêm mới
                bool result = nvBus.AddNhanVien(nv, TenChucVu, TenPhongBan);
                if (result)
                {
                    MessageBox.Show("✅ Đã thêm nhân viên và tạo tài khoản!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("❌ Thêm nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Cập nhật
                bool result = nvBus.UpdateNhanVien(nv);
                if (result)
                {
                    MessageBox.Show("✅ Đã cập nhật thông tin nhân viên!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("❌ Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // ===== Làm mới danh sách sau khi lưu =====
            LoadDanhSachNhanVien(false);

            // ===== Xóa trắng form =====
            //BtnAdd_Click(sender,e);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            txtName.Text = txtAddress.Text = txtEmail.Text = txtQue.Text = txtSearchName.Text = txtSearchAddress.Text = txtSearchQue.Text = txtSearchEmail.Text = "";
            cbGender.SelectedIndex = cbSearchGender.SelectedIndex = -1;
            cbPhongBan.SelectedIndex = -1;
            cbChucVu.SelectedIndex = -1;
            dtDob.Value = DateTime.Now;
            selectedId = null;
        }

        private void BtnToggleView_Click(object sender, EventArgs e)
        {
            isViewingHidden = !isViewingHidden;
            btnToggleView.Text = isViewingHidden ? "📋 Hiển thị nhân viên đang làm việc" : "👁 Hiển thị nhân viên đã ẩn";
            LoadDanhSachNhanVien(isViewingHidden);
        }

         //=======================
         // 8️⃣ HIỆU ỨNG ACTION
         // =======================
        private void Dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Action")
            {
                dgv.Cursor = Cursors.Hand;

                // Lấy cell và icon hiện tại
                var cell = (DataGridViewImageCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var currentValue = cell.Value;

                // Hiệu ứng hover riêng theo trạng thái hiển thị
                if (isViewingHidden)
                {
                    // Đang ở chế độ xem nhân viên ẩn → icon là reset
                    cell.Value = Properties.Resources.arrow;
                }
                else
                {
                    // Đang ở chế độ xem nhân viên đang làm việc → icon là delete
                    cell.Value = Properties.Resources.trash;
                }

                // Đổi nền nhẹ
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Gainsboro;
            }
        }

        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Action")
            {
                dgv.Cursor = Cursors.Default;

                var cell = (DataGridViewImageCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Trả lại icon gốc
                if (isViewingHidden)
                {
                    cell.Value = Properties.Resources.reset;
                }
                else
                {
                    cell.Value = Properties.Resources.delete;
                }

                // Trả nền về trắng
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
            }
        }

        private void FilterNhanVien()
        {
            if (dgv.DataSource == null) return;
            DataTable dt = dgv.DataSource as DataTable;
            if (dt == null) return;

            string filter = "1=1";
            if (!string.IsNullOrWhiteSpace(txtSearchName.Text))
                filter += $" AND [Họ tên] LIKE '%{txtSearchName.Text.Replace("'", "''")}%'";
            if (cbSearchGender.SelectedIndex > 0)
                filter += $" AND [Giới tính] = '{cbSearchGender.Text}'";
            if (!string.IsNullOrWhiteSpace(txtSearchAddress.Text))
                filter += $" AND [Địa chỉ] LIKE '%{txtSearchAddress.Text.Replace("'", "''")}%'";
            if (!string.IsNullOrWhiteSpace(txtSearchQue.Text))
                filter += $" AND [Quê quán] LIKE '%{txtSearchQue.Text.Replace("'", "''")}%'";
            if (!string.IsNullOrWhiteSpace(txtSearchEmail.Text))
                filter += $" AND [Email] LIKE '%{txtSearchEmail.Text.Replace("'", "''")}%'";
            (dgv.DataSource as DataTable).DefaultView.RowFilter = filter;
        }
    }
}

