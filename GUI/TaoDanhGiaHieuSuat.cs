using BLL;
using DAL;
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
        private Guna2Button btnSave, btnUndo, btnReload, btnExport;
        private Guna2DataGridView dgv;

        private string connectionString;
        private string idNguoiDanhGia = "GD00000001"; // người đánh giá giả định
        private int? selectedId = null;
        private DataTable dtDanhGia; // lưu dữ liệu toàn bộ để lọc tại chỗ
        private BLLDanhGiaNhanVien bllDanhGia;
        private BLLNhanVien bllNhanVien;

        public TaoDanhGiaHieuSuat(string idNhanVien, string conn)
        {
            connectionString = conn;
            InitializeComponent();
            bllDanhGia = new BLLDanhGiaNhanVien(conn);
            bllNhanVien = new BLLNhanVien(conn);
            BuildUI();
            LoadNhanVien();
            LoadDanhGia(); // tải dữ liệu ban đầu
        }

        private void BuildUI()
        {
            // === TOÀN BỘ FORM ===
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // === TIÊU ĐỀ CHÍNH ===
            Label lblTitle = new Label()
            {
                Text = "ĐÁNH GIÁ HIỆU SUẤT NHÂN VIÊN",
                Font = new Font("Times New Roman", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 70, 139),
                Dock = DockStyle.Top,
                Height = 70,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // === THANH TÌM KIẾM ===
            FlowLayoutPanel searchPanel = new FlowLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(25, 15, 25, 10),
                BackColor = Color.White,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(0)
            };

            Label lblSearch = new Label()
            {
                Text = "🔍 Tìm kiếm:",
                Font = new Font("Times New Roman", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 70),
                AutoSize = true,
                Margin = new Padding(0, 10, 10, 0)
            };

            txtSearch = new Guna2TextBox()
            {
                PlaceholderText = "Nhập tên nhân viên hoặc nhận xét...",
                Width = 350,
                BorderRadius = 10,
                BorderThickness = 1,
                BorderColor = Color.Silver,
                Font = new Font("Times New Roman", 12),
                FillColor = Color.FromArgb(250, 250, 255),
                Margin = new Padding(0, 5, 15, 0)
            };
            txtSearch.TextChanged += (s, e) => FilterDanhGia();

            Guna2Button btnClear = new Guna2Button()
            {
                Text = "Làm mới",
                BorderRadius = 10,
                FillColor = Color.FromArgb(40, 120, 220),
                HoverState = { FillColor = Color.FromArgb(70, 145, 245) },
                ForeColor = Color.White,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                Height = 40,
                Width = 120,
                Margin = new Padding(10, 5, 0, 0)
            };
            btnClear.Click += (s, e) =>
            {
                txtSearch.Clear();
                FilterDanhGia();
            };

            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(btnClear);

            Panel searchContainer = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White
            };
            searchContainer.Controls.Add(searchPanel);
            searchContainer.Resize += (s, e) =>
            {
                searchPanel.Left = (searchContainer.ClientSize.Width - searchPanel.Width) / 2;
                searchPanel.Top = (searchContainer.ClientSize.Height - searchPanel.Height) / 2;
            };

            // === FORM NHẬP THÔNG TIN ĐÁNH GIÁ ===
            Panel cardPanel = new Panel()
            {
                BackColor = Color.White,
                Padding = new Padding(30),
                Dock = DockStyle.Top,
                Height = 270,
            };

            TableLayoutPanel form = new TableLayoutPanel()
            {
                ColumnCount = 2,
                Padding = new Padding(0, 30, 0, 0),
                AutoSize = true
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            Font lblFont = new Font("Times New Roman", 12, FontStyle.Bold);
            Color lblColor = Color.FromArgb(45, 45, 70);

            // Combobox chọn nhân viên
            cbEmployee = new Guna2ComboBox()
            {
                BorderRadius = 8,
                Font = new Font("Times New Roman", 12),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 600,
                MaxDropDownItems = 6,
                IntegralHeight = false,
            };

            // Ô nhập điểm số
            numScore = new NumericUpDown()
            {
                Minimum = 0,
                Maximum = 100,
                Font = new Font("Times New Roman", 12),
                Width = 600
            };

            // Ô chọn ngày đánh giá
            dtReview = new Guna2DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                BorderRadius = 8,
                Font = new Font("Times New Roman", 12),
                Width = 600
            };

            // Ô nhận xét
            txtNote = new Guna2TextBox()
            {
                PlaceholderText = "Nhập nhận xét về hiệu suất...",
                BorderRadius = 8,
                Font = new Font("Times New Roman", 12),
                Width = 600,
                Multiline = true,
                Height = 70
            };

            // Nút Lưu
            btnSave = new Guna2Button()
            {
                Text = "💾 Lưu đánh giá",
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen,
                HoverState = { FillColor = Color.SeaGreen },
                ForeColor = Color.White,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                Width = 150,
                Height = 40,
                Margin = new Padding(0, 10, 10, 0)
            };
            btnSave.Click += BtnSave_Click;

            // Nút Hoàn tác
            btnUndo = new Guna2Button()
            {
                Text = "↩️ Hoàn tác",
                BorderRadius = 8,
                FillColor = Color.Gray,
                HoverState = { FillColor = Color.DimGray },
                ForeColor = Color.White,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                Width = 130,
                Height = 40,
                Margin = new Padding(10, 10, 0, 0)
            };
            btnUndo.Click += BtnUndo_Click;

            // Nút Xuất Excel
            Guna2Button btnExportExcel = new Guna2Button()
            {
                Text = "📊 Xuất Excel",
                BorderRadius = 10,
                FillColor = Color.FromArgb(60, 140, 230),
                HoverState = { FillColor = Color.FromArgb(80, 160, 250) },
                ForeColor = Color.White,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                Width = 140,
                Height = 40,
                Margin = new Padding(10, 10, 0, 0)
            };
            btnExportExcel.Click += BtnExportExcel_Click;

            // Thêm các control vào bảng nhập
            form.Controls.Add(new Label() { Text = "Nhân viên:", AutoSize = true, Font = lblFont, ForeColor = lblColor, Anchor = AnchorStyles.Left }, 0, 0);
            form.Controls.Add(cbEmployee, 1, 0);

            form.Controls.Add(new Label() { Text = "Điểm số:", AutoSize = true, Font = lblFont, ForeColor = lblColor, Anchor = AnchorStyles.Left }, 0, 1);
            form.Controls.Add(numScore, 1, 1);

            form.Controls.Add(new Label() { Text = "Ngày đánh giá:", AutoSize = true, Font = lblFont, ForeColor = lblColor, Anchor = AnchorStyles.Left }, 0, 2);
            form.Controls.Add(dtReview, 1, 2);

            form.Controls.Add(new Label() { Text = "Nhận xét:", AutoSize = true, Font = lblFont, ForeColor = lblColor, Anchor = AnchorStyles.Left }, 0, 3);
            form.Controls.Add(txtNote, 1, 3);

            // Panel chứa nút thao tác
            FlowLayoutPanel btnPanel = new FlowLayoutPanel() { FlowDirection = FlowDirection.LeftToRight, Dock = DockStyle.Fill };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            btnPanel.Controls.Add(btnExportExcel);
            form.Controls.Add(btnPanel, 1, 4);

            // Căn giữa form nhập
            cardPanel.Controls.Add(form);
            form.Anchor = AnchorStyles.None;
            cardPanel.Resize += (s, e) =>
            {
                form.Left = (cardPanel.ClientSize.Width - form.Width) / 2;
                form.Top = (cardPanel.ClientSize.Height - form.Height) / 2;
            };

            // === DỮ LIỆU (DGV) ===
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.FromArgb(250, 250, 250) }
            };
            dgv.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.LightGrid;
            dgv.ThemeStyle.HeaderStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            dgv.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(230, 240, 255);
            dgv.ThemeStyle.HeaderStyle.ForeColor = Color.FromArgb(30, 60, 110);
            dgv.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(220, 230, 255);
            dgv.DefaultCellStyle.Font = new Font("Times New Roman", 12);
            dgv.CellClick += Dgv_CellClick;

            // === BỐ CỤC CHÍNH ===
            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 270));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            main.Controls.Add(lblTitle, 0, 0);
            main.Controls.Add(searchContainer, 0, 1);
            main.Controls.Add(cardPanel, 0, 2);
            main.Controls.Add(dgv, 0, 3);

            this.Controls.Add(main);
        }





        // ===== LOAD NHÂN VIÊN =====
        private void LoadNhanVien()
        {
            cbEmployee.DataSource = bllNhanVien.ComboboxNhanVien();
            cbEmployee.DisplayMember = "TenNhanVien";
            cbEmployee.ValueMember = "id";
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

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Lưu file Excel"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var wb = new ClosedXML.Excel.XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("DanhGiaNhanVien");

                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            ws.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                            ws.Cell(1, i + 1).Style.Font.Bold = true;
                            ws.Cell(1, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                            ws.Cell(1, i + 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                        }

                        for (int r = 0; r < dgv.Rows.Count; r++)
                            for (int c = 0; c < dgv.Columns.Count; c++)
                                ws.Cell(r + 2, c + 1).Value = dgv.Rows[r].Cells[c].Value?.ToString();

                        ws.Columns().AdjustToContents();
                        wb.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
