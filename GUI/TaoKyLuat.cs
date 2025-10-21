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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI
{
    public partial class TaoKyLuat : UserControl
    {
        private Guna2ComboBox cbEmployee, cbType, cbPhongBan;
        private Guna2DateTimePicker dtDiscipline;
        private Guna2TextBox txtReason;
        private Guna2Button btnSave, btnUndo, btnSearch;
        private Guna2DataGridView dgv;

        private string connectionString;
        private string idNguoiTao = "GD00000001";
        private int? selectedId = null;
        private BLLKyLuat bllKyLuat;
        private BLLNhanVien bllNhanVien;

        public TaoKyLuat(string idNhanVien, string conn)
        {
            connectionString = conn;
            InitializeComponent();
            bllKyLuat = new BLLKyLuat(conn);
            bllNhanVien = new BLLNhanVien(conn);
            BuildUI();
            LoadPhongBan();
            LoadNhanVien();
            LoadKyLuat(); // ✅ hiển thị tất cả ngay khi mở form
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            Label lblTitle = new Label()
            {
                Text = "KỶ LUẬT NHÂN VIÊN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ==== THANH TÌM KIẾM ====
            Label lblSearch = new Label()
            {
                Text = "📋 Tìm kiếm theo phòng ban:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Margin = new Padding(10, 10, 0, 0)
            };

            cbPhongBan = new Guna2ComboBox()
            {
                Width = 250,
                BorderRadius = 6,
                Margin = new Padding(10, 5, 10, 5),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            btnSearch = new Guna2Button()
            {
                Text = "🔍 Tìm kiếm",
                BorderRadius = 8,
                FillColor = Color.SteelBlue,
                ForeColor = Color.White,
                Height = 36,
                Width = 120,
                Margin = new Padding(10, 5, 0, 5)
            };
            btnSearch.Click += BtnSearch_Click;

            FlowLayoutPanel searchPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(20, 5, 0, 10)
            };
            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(cbPhongBan);
            searchPanel.Controls.Add(btnSearch);

            // ==== FORM INPUT ====
            cbEmployee = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbType = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            //cbType.Items.AddRange(new object[] { "Khiển trách", "Cảnh cáo", "Đình chỉ", "Sa thải" });
            cbType.Items.AddRange(new object[] { "Phạt", "Kỷ luật" });
            cbType.SelectedIndex = 0;

            dtDiscipline = new Guna2DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Dock = DockStyle.Fill
            };

            txtReason = new Guna2TextBox()
            {
                PlaceholderText = "Lý do kỷ luật",
                Dock = DockStyle.Fill,
                Multiline = true,
                Height = 60
            };

            btnSave = new Guna2Button()
            {
                Text = "💾 Lưu kỷ luật",
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Width = 150,
                Height = 40,
                Cursor = Cursors.Hand
            };
            btnSave.Click += BtnSave_Click;

            btnUndo = new Guna2Button()
            {
                Text = "↩️ Hoàn tác",
                BorderRadius = 8,
                FillColor = Color.Gray,
                ForeColor = Color.White,
                Width = 150,
                Height = 40,
                Cursor = Cursors.Hand
            };
            btnUndo.Click += BtnUndo_Click;

            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 35 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            dgv.CellClick += Dgv_CellClick;
            dgv.CellMouseEnter += Dgv_CellMouseEnter;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;

            TableLayoutPanel form = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(10, 10, 0, 90),
                AutoScroll = true,
                ColumnCount = 3,
                RowCount = 5,
                Height = 250
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23));

            form.Controls.Add(new Label() { Text = "Nhân viên:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
            form.Controls.Add(cbEmployee, 1, 0);
            form.Controls.Add(new Label() { Text = "Ngày kỷ luật:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            form.Controls.Add(dtDiscipline, 1, 1);
            form.Controls.Add(new Label() { Text = "Hình thức:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 2);
            form.Controls.Add(cbType, 1, 2);
            form.Controls.Add(new Label() { Text = "Lý do:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 3);
            form.Controls.Add(txtReason, 1, 3);

            FlowLayoutPanel btnPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            form.Controls.Add(btnPanel, 1, 4);

            // ==== MAIN ====
            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); // thanh tìm kiếm
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            main.Controls.Add(lblTitle, 0, 0);
            main.Controls.Add(searchPanel, 0, 1);

            TableLayoutPanel content = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 300));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            content.Controls.Add(form, 0, 0);
            content.Controls.Add(dgv, 0, 1);

            main.Controls.Add(content, 0, 2);

            this.Controls.Add(main);
        }

        // ======================= LOAD NHÂN VIÊN =======================
        private void LoadNhanVien()
        {
            cbEmployee.DataSource = bllNhanVien.ComboboxNhanVien();
            cbEmployee.DisplayMember = "TenNhanVien";
            cbEmployee.ValueMember = "id";
        }

        // ======================= LOAD PHÒNG BAN =======================
        private void LoadPhongBan()
        {
            cbPhongBan.DataSource = bllKyLuat.GetDepartments();
            cbPhongBan.DisplayMember = "TenPhongBan";
            cbPhongBan.ValueMember = "id";
            cbPhongBan.SelectedIndex = 0; // ✅ Mặc định chọn “Tất cả”
        }

        // ======================= TÌM KIẾM =======================
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (cbPhongBan.SelectedValue == null || cbPhongBan.SelectedIndex == 0)
                LoadKyLuat(); // ✅ chọn “Tất cả” thì hiển thị hết
            else
                LoadKyLuat(cbPhongBan.SelectedValue.ToString());
        }

        // ======================= LOAD KỶ LUẬT =======================
        private void LoadKyLuat(string idPhongBan = "")
        {
            dgv.DataSource = bllKyLuat.GetAll(idPhongBan);

            if (!dgv.Columns.Contains("Xoa"))
            {
                DataGridViewImageColumn colDelete = new DataGridViewImageColumn()
                {
                    Name = "Xoa",
                    HeaderText = "Xóa",
                    Image = Properties.Resources.delete,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 50
                };
                dgv.Columns.Add(colDelete);
                dgv.Columns["Xoa"].DisplayIndex = dgv.Columns.Count - 1;
            }
        }

        // ======================= LƯU HOẶC CẬP NHẬT =======================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cbEmployee.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Cảnh báo");
                return;
            }

            // Khởi tạo DTO
            DTOKyLuat kl = new DTOKyLuat
            {
                Id = selectedId ?? 0,
                IdNhanVien = cbEmployee.SelectedValue.ToString(),
                Loai = cbType.Text,
                LyDo = txtReason.Text,
                NgayKyLuat = dtDiscipline.Value,
                IdNguoiTao = idNguoiTao
            };

            bool isNew = (selectedId == null);
            bllKyLuat.Save(kl, isNew);

            MessageBox.Show(isNew ? "✅ Đã thêm kỷ luật mới!" : "✏️ Đã cập nhật kỷ luật!");
            LoadKyLuat();
            ClearForm();
        }

        // ======================= CLICK DGV =======================
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
                if (MessageBox.Show("Bạn có chắc muốn xóa kỷ luật này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bllKyLuat.Delete(id);
                    LoadKyLuat();
                }
                return;
            }

            // ✏️ Chọn hàng để sửa
            DataGridViewRow row = dgv.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["id"].Value);
            cbEmployee.Text = row.Cells["TenNhanVien"].Value?.ToString();
            cbType.Text = row.Cells["HinhThuc"].Value?.ToString();
            txtReason.Text = row.Cells["lyDo"].Value?.ToString();

            btnSave.Text = "✏️ Cập nhật";
            btnSave.FillColor = Color.Orange;
        }

        // ======================= HOVER ICON =======================
        private void Dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Hand;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.trash;
            }
        }

        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Default;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.delete;
            }
        }


        // ======================= HOÀN TÁC =======================
        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hoàn tác và xóa dữ liệu đang nhập?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ClearForm();
        }

        private void ClearForm()
        {
            cbEmployee.SelectedIndex = -1;
            cbType.SelectedIndex = 0;
            txtReason.Clear();
            dtDiscipline.Value = DateTime.Now;
            selectedId = null;
            btnSave.Text = "💾 Lưu kỷ luật";
            btnSave.FillColor = Color.MediumSeaGreen;
            dgv.ClearSelection();
        }
    }
}


