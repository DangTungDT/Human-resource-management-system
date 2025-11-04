using BLL;
using DAL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class CRUDPhongban : UserControl
    {
        private Guna2TextBox txtTenPhongBan, txtMoTa, txtSearchTen, txtSearchMoTa;
        private Guna2Button btnSave, btnUndo, btnClearFilter;
        private Guna2DataGridView dgv;
        private string connectionString;
        private int? selectedId = null;
        private readonly BLLPhongBan bllPhongBan;
        private DataTable dtPhongBan;

        public CRUDPhongban(string idNhanVien, string conn)
        {
            connectionString = conn;
            InitializeComponent();
            bllPhongBan = new BLLPhongBan(conn);
            BuildUI();
            LoadPhongBan();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            // ===== TIÊU ĐỀ =====
            Label lblTitle = new Label()
            {
                Text = "QUẢN LÝ PHÒNG BAN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ===== THANH TÌM KIẾM (một hàng, đồng màu) =====
            FlowLayoutPanel searchPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(25, 10, 25, 10),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            Label lblSearchTitle = new Label()
            {
                Text = "🔍 TÌM KIẾM PHÒNG BAN",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 6, 15, 0)
            };

            txtSearchTen = new Guna2TextBox()
            {
                PlaceholderText = "Nhập tên phòng ban...",
                Width = 250,
                BorderRadius = 6,
                Margin = new Padding(0, 0, 10, 0)
            };
            txtSearchTen.TextChanged += (s, e) => FilterPhongBan();

            txtSearchMoTa = new Guna2TextBox()
            {
                PlaceholderText = "Nhập mô tả...",
                Width = 250,
                BorderRadius = 6,
                Margin = new Padding(0, 0, 10, 0)
            };
            txtSearchMoTa.TextChanged += (s, e) => FilterPhongBan();

            btnClearFilter = new Guna2Button()
            {
                Text = "🔄 Làm mới",
                BorderRadius = 8,
                FillColor = Color.SteelBlue,
                ForeColor = Color.White,
                Height = 36,
                Width = 120,
                Anchor = AnchorStyles.Left
            };
            btnClearFilter.Click += (s, e) =>
            {
                txtSearchTen.Clear();
                txtSearchMoTa.Clear();
                FilterPhongBan();
            };
            btnClearFilter.MouseEnter += (s, e) => btnClearFilter.FillColor = Color.DodgerBlue;
            btnClearFilter.MouseLeave += (s, e) => btnClearFilter.FillColor = Color.SteelBlue;

            searchPanel.Controls.Add(lblSearchTitle);
            searchPanel.Controls.Add(txtSearchTen);
            searchPanel.Controls.Add(txtSearchMoTa);
            searchPanel.Controls.Add(btnClearFilter);

            // ===== FORM INPUT =====
            txtTenPhongBan = new Guna2TextBox() { PlaceholderText = "Tên phòng ban", Dock = DockStyle.Fill };
            txtMoTa = new Guna2TextBox() { PlaceholderText = "Mô tả", Dock = DockStyle.Fill, Multiline = true, Height = 60 };

            btnSave = new Guna2Button()
            {
                Text = "➕ Thêm mới",
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Width = 140,
                Height = 40
            };
            btnSave.Click += BtnSave_Click;

            btnUndo = new Guna2Button()
            {
                Text = "↩️ Hoàn tác",
                BorderRadius = 8,
                FillColor = Color.Gray,
                ForeColor = Color.White,
                Width = 120,
                Height = 40
            };
            btnUndo.Click += BtnUndo_Click;

            TableLayoutPanel form = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(20, 10, 20, 10),
                ColumnCount = 3,
                RowCount = 3,
                AutoSize = true
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            form.Controls.Add(new Label() { Text = "Tên phòng ban:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
            form.Controls.Add(txtTenPhongBan, 1, 0);
            form.Controls.Add(new Label() { Text = "Mô tả:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            form.Controls.Add(txtMoTa, 1, 1);

            FlowLayoutPanel btnPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            form.Controls.Add(btnPanel, 1, 2);

            // ===== DGV =====
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowTemplate = { Height = 35 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgv.CellClick += Dgv_CellClick;

            // ===== MAIN =====
            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // Tiêu đề
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 55)); // Thanh tìm kiếm
            main.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // Form nhập
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // DGV

            main.Controls.Add(lblTitle, 0, 0);
            main.Controls.Add(searchPanel, 0, 1);
            main.Controls.Add(form, 0, 2);
            main.Controls.Add(dgv, 0, 3);

            this.Controls.Add(main);
        }

        // ======================= LOAD & LỌC =======================

        private void LoadPhongBan()
        {
            dtPhongBan = bllPhongBan.GetAllPhongBan();
            dgv.DataSource = dtPhongBan;

            if (!dgv.Columns.Contains("Xóa"))
            {
                DataGridViewImageColumn colDelete = new DataGridViewImageColumn()
                {
                    Name = "Xóa",
                    HeaderText = "Xóa",
                    Image = Properties.Resources.delete,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 50
                };
                dgv.Columns.Add(colDelete);
                dgv.Columns["Xóa"].DisplayIndex = dgv.Columns.Count - 1;

                if (dgv.Columns["Mã phòng ban"] != null)
                {
                    if (dgv.Columns["Mã phòng ban"].Visible)
                    {
                        dgv.Columns["Mã phòng ban"].Visible = false;
                    }
                }
            }
        }

        private void FilterPhongBan()
        {
            if (dtPhongBan == null) return;

            string filter = "1=1";
            if (!string.IsNullOrWhiteSpace(txtSearchTen.Text))
                filter += $" AND [Tên phòng ban] LIKE '%{txtSearchTen.Text.Replace("'", "''")}%'";

            if (!string.IsNullOrWhiteSpace(txtSearchMoTa.Text))
                filter += $" AND [Mô tả] LIKE '%{txtSearchMoTa.Text.Replace("'", "''")}%'";

            dtPhongBan.DefaultView.RowFilter = filter;
        }

        // ======================= CRUD =======================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhongBan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên phòng ban!", "Thông báo");
                return;
            }

            DTOPhongBan pb = new DTOPhongBan
            {
                Id = selectedId ?? 0,
                TenPhongBan = txtTenPhongBan.Text.Trim(),
                MoTa = txtMoTa.Text.Trim()
            };

            try
            {
                if (selectedId == null)
                {
                    // === Thêm mới ===
                    bllPhongBan.SavePhongBan(pb, isNew: true);
                    MessageBox.Show("✅ Đã thêm phòng ban mới!");
                }
                else
                {
                    // === Cập nhật ===
                    bllPhongBan.SavePhongBan(pb, isNew: false);
                    MessageBox.Show("✏️ Đã cập nhật phòng ban!");
                }

                LoadPhongBan();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phòng ban: " + ex.Message);
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xóa")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã phòng ban"].Value);
                if (MessageBox.Show("Bạn có chắc muốn xóa phòng ban này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (bllPhongBan.DeletePhongBan(id))
                        LoadPhongBan();
                }
                return;
            }

            selectedId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã phòng ban"].Value);
            txtTenPhongBan.Text = dgv.Rows[e.RowIndex].Cells["Tên phòng ban"].Value.ToString();
            txtMoTa.Text = dgv.Rows[e.RowIndex].Cells["Mô tả"].Value.ToString();

            btnSave.Text = "✏️ Cập nhật";
            btnSave.FillColor = Color.Orange;
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hoàn tác dữ liệu đang nhập?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ClearForm();
        }

        private void ClearForm()
        {
            selectedId = null;
            txtTenPhongBan.Clear();
            txtMoTa.Clear();
            btnSave.Text = "➕ Thêm mới";
            btnSave.FillColor = Color.MediumSeaGreen;
            dgv.ClearSelection();
        }
    }
}