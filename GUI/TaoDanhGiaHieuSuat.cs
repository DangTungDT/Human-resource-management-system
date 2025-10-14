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
    public partial class TaoDanhGiaHieuSuat : UserControl
    {
        private Guna2ComboBox cbEmployee;
        private Guna2DateTimePicker dtReview;
        private NumericUpDown numScore;
        private Guna2TextBox txtNote, txtSearch;
        private Guna2Button btnSave, btnUndo, btnReload;
        private Guna2DataGridView dgv;

        private string connectionString;
        private string idNguoiDanhGia = "GD00000001"; // người đánh giá giả định
        private int? selectedId = null;
        private DataTable dtDanhGia; // lưu dữ liệu toàn bộ để lọc tại chỗ
        private BLLDanhGiaNhanVien bllDanhGia;

        public TaoDanhGiaHieuSuat(string conn)
        {
            connectionString = conn;
            InitializeComponent();
            bllDanhGia = new BLLDanhGiaNhanVien(conn);
            BuildUI();
            LoadNhanVien();
            LoadDanhGia(); // tải dữ liệu ban đầu
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            // ====== TIÊU ĐỀ ======
            Label lblTitle = new Label()
            {
                Text = "ĐÁNH GIÁ HIỆU SUẤT NHÂN VIÊN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ===== THANH TÌM KIẾM =====
            Label lblSearch = new Label()
            {
                Text = "🔍 Tìm kiếm đánh giá:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                AutoSize = true,
                Margin = new Padding(5, 10, 10, 0)
            };

            txtSearch = new Guna2TextBox()
            {
                PlaceholderText = "Nhập tên nhân viên, nhận xét hoặc điểm để tìm...",
                BorderRadius = 8,
                Width = 320,
                Height = 36,
                Margin = new Padding(10, 5, 10, 5)
            };
            txtSearch.TextChanged += (s, e) => FilterDanhGia();

            btnReload = new Guna2Button()
            {
                Text = "🔄 Làm mới dữ liệu",
                BorderRadius = 8,
                FillColor = Color.SteelBlue,
                ForeColor = Color.White,
                Width = 160,
                Height = 36,
                Margin = new Padding(10, 5, 0, 5)
            };
            btnReload.Click += (s, e) => { txtSearch.Clear(); LoadDanhGia(); };

            FlowLayoutPanel searchPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Height = 55,
                Padding = new Padding(20, 5, 20, 5),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.FromArgb(245, 246, 250)
            };
            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(btnReload);

            // ===== PHẦN FORM VÀ DGV =====
            cbEmployee = new Guna2ComboBox()
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            dtReview = new Guna2DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Dock = DockStyle.Fill
            };
            numScore = new NumericUpDown() { Minimum = 0, Maximum = 100, Dock = DockStyle.Fill };
            txtNote = new Guna2TextBox()
            {
                PlaceholderText = "Nhận xét...",
                Dock = DockStyle.Fill,
                Multiline = true,
                Height = 60
            };

            btnSave = new Guna2Button()
            {
                Text = "💾 Lưu đánh giá",
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Width = 150,
                Height = 40
            };
            btnSave.Click += BtnSave_Click;

            btnUndo = new Guna2Button()
            {
                Text = "↩️ Hoàn tác",
                BorderRadius = 8,
                FillColor = Color.Gray,
                ForeColor = Color.White,
                Width = 150,
                Height = 40
            };
            btnUndo.Click += BtnUndo_Click;

            TableLayoutPanel form = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(20, 10, 20, 20),
                ColumnCount = 3,
                RowCount = 5,
                Height = 250
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            form.Controls.Add(new Label() { Text = "Nhân viên:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
            form.Controls.Add(cbEmployee, 1, 0);

            form.Controls.Add(new Label() { Text = "Ngày đánh giá:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            form.Controls.Add(dtReview, 1, 1);

            form.Controls.Add(new Label() { Text = "Điểm số:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 2);
            form.Controls.Add(numScore, 1, 2);

            form.Controls.Add(new Label() { Text = "Nhận xét:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 3);
            form.Controls.Add(txtNote, 1, 3);

            FlowLayoutPanel btnPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            form.Controls.Add(btnPanel, 1, 4);

            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowTemplate = { Height = 35 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgv.CellClick += Dgv_CellClick;

            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            main.Controls.Add(lblTitle, 0, 0);
            main.Controls.Add(searchPanel, 0, 1);

            TableLayoutPanel content = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 270));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            content.Controls.Add(form, 0, 0);
            content.Controls.Add(dgv, 0, 1);

            main.Controls.Add(content, 0, 2);
            this.Controls.Add(main);
        }

        // ===== LOAD NHÂN VIÊN =====
        private void LoadNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, TenNhanVien FROM NhanVien WHERE DaXoa = 0";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbEmployee.DataSource = dt;
                cbEmployee.DisplayMember = "TenNhanVien";
                cbEmployee.ValueMember = "id";
            }
        }

        // ===== LOAD DỮ LIỆU ĐÁNH GIÁ =====
        private void LoadDanhGia()
        {
            dtDanhGia = bllDanhGia.GetAll();
            dgv.DataSource = dtDanhGia;

            // thêm cột xóa nếu chưa có
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

        // ===== LỌC KẾT QUẢ =====
        private void FilterDanhGia()
        {
            if (dtDanhGia == null) return;
            string kw = txtSearch.Text.Trim().Replace("'", "''");

            if (string.IsNullOrEmpty(kw))
            {
                dtDanhGia.DefaultView.RowFilter = ""; // hiển thị tất cả
                return;
            }

            if (decimal.TryParse(kw, out decimal diem))
            {
                dtDanhGia.DefaultView.RowFilter = $"Convert([Điểm số], 'System.String') LIKE '%{diem}%'";
            }
            else
            {
                dtDanhGia.DefaultView.RowFilter =
                    $"[Nhân viên] LIKE '%{kw}%' OR [Nhận xét] LIKE '%{kw}%'";
            }
        }

        // ===== LƯU / CẬP NHẬT =====
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cbEmployee.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbEmployee.SelectedValue.ToString() == idNguoiDanhGia)
            {
                MessageBox.Show("Người đánh giá không được trùng với nhân viên được đánh giá!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ===== TẠO DTO =====
            DTODanhGiaNhanVien dg = new DTODanhGiaNhanVien
            {
                ID = selectedId ?? 0,
                IDNhanVien = cbEmployee.SelectedValue.ToString(),
                IDNguoiDanhGia = idNguoiDanhGia,
                NgayTao = dtReview.Value,
                DiemSo = (int)numScore.Value,
                NhanXet = txtNote.Text.Trim()
            };

            try
            {
                if (selectedId == null)
                {
                    // ===== THÊM MỚI =====
                    bllDanhGia.Save(dg, isNew: true);
                    MessageBox.Show("✅ Đã thêm đánh giá mới!");
                }
                else
                {
                    // ===== CẬP NHẬT =====
                    bllDanhGia.Save(dg, isNew: false);
                    MessageBox.Show("✏️ Đã cập nhật đánh giá!");
                }

                // ===== LÀM MỚI DỮ LIỆU =====
                LoadDanhGia();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu đánh giá: " + ex.Message);
            }
        }

        // ===== CLICK DGV =====
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã đánh giá"].Value);
                if (MessageBox.Show("Bạn có chắc muốn xóa đánh giá này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bllDanhGia.Delete(id);
                    LoadDanhGia();
                }
                return;
            }

            selectedId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã đánh giá"].Value);
            cbEmployee.Text = dgv.Rows[e.RowIndex].Cells["Nhân viên"].Value.ToString();
            dtReview.Value = DateTime.TryParse(dgv.Rows[e.RowIndex].Cells["Ngày đánh giá"].Value.ToString(), out DateTime dt) ? dt : DateTime.Now;
            numScore.Value = Convert.ToDecimal(dgv.Rows[e.RowIndex].Cells["Điểm số"].Value ?? 0);
            txtNote.Text = dgv.Rows[e.RowIndex].Cells["Nhận xét"].Value?.ToString();

            btnSave.Text = "✏️ Cập nhật";
            btnSave.FillColor = Color.Orange;
        }

        // ===== HOÀN TÁC =====
        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hoàn tác dữ liệu đang nhập?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ClearForm();
        }

        private void ClearForm()
        {
            selectedId = null;
            cbEmployee.SelectedIndex = -1;
            numScore.Value = 0;
            txtNote.Clear();
            dtReview.Value = DateTime.Now;
            btnSave.Text = "💾 Lưu đánh giá";
            btnSave.FillColor = Color.MediumSeaGreen;
            dgv.ClearSelection();
        }
    }
}
