using BLL;
using ClosedXML.Excel;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class CRUDTaiKhoan : UserControl
    {
        private Guna2TextBox txtUsername, txtPassword, txtSearchUser;
        private Guna2ComboBox cbNhanVien;
        private Guna2Button btnSave, btnUndo, btnClearFilter;
        private Guna2DataGridView dgv;
        private bool isPasswordVisible = false;

        private string connectionString;
        private int? selectedId = null;
        private DataTable dtTaiKhoan;
        private readonly BLLTaiKhoan bllTaiKhoan;

        public CRUDTaiKhoan(string idNhanVien, string conn)
        {
            connectionString = conn;
            bllTaiKhoan = new BLLTaiKhoan(conn);

            InitializeComponent();
            BuildUI();
            LoadNhanVien();
            LoadTaiKhoan();
        }

        // ======================= GIAO DIỆN =======================
        private void BuildUI()
        {
            // === TOÀN BỘ FORM ===
            this.Dock = DockStyle.Fill; // Cho form con chiếm toàn bộ vùng chứa
            this.BackColor = Color.FromArgb(245, 247, 250); // Đặt màu nền sáng nhạt cho giao diện

            // === TIÊU ĐỀ CHÍNH ===
            Label lblTitle = new Label()
            {
                Text = "QUẢN LÝ TÀI KHOẢN", // Tiêu đề trang
                Font = new Font("Times New Roman", 20, FontStyle.Bold), // Font Times New Roman, cỡ 20, in đậm
                ForeColor = Color.FromArgb(33, 70, 139), // Màu chữ xanh đậm
                Dock = DockStyle.Top, // Căn trên cùng
                Height = 70, // Chiều cao vùng tiêu đề
                TextAlign = ContentAlignment.MiddleCenter // Căn giữa nội dung chữ
            };

            // === THANH TÌM KIẾM ===
            FlowLayoutPanel searchPanel = new FlowLayoutPanel()
            {
                AutoSize = true, // Tự động co giãn kích thước
                AutoSizeMode = AutoSizeMode.GrowAndShrink, // Cho phép mở rộng/thu hẹp theo nội dung
                Padding = new Padding(25, 15, 25, 10), // Khoảng cách lề trong (trên, trái, dưới, phải)
                BackColor = Color.White, // Màu nền trắng
                FlowDirection = FlowDirection.LeftToRight, // Các phần tử nằm ngang
                WrapContents = false, // Không xuống hàng
                Margin = new Padding(0), // Xóa khoảng cách ngoài
            };

            // Label tìm kiếm
            Label lblSearch = new Label()
            {
                Text = "🔍 Tìm kiếm:", // Nội dung hiển thị
                Font = new Font("Times New Roman", 13, FontStyle.Bold), // Font Times New Roman 13 in đậm
                ForeColor = Color.FromArgb(50, 50, 70), // Màu chữ xám đậm
                AutoSize = true, // Tự động theo kích thước nội dung
                Margin = new Padding(0, 10, 10, 0) // Cách top 10px, phải 10px
            };

            // Ô nhập tìm kiếm
            txtSearchUser = new Guna2TextBox()
            {
                PlaceholderText = "Nhập họ tên, tài khoản hoặc mật khẩu...", // Gợi ý trong ô nhập
                Width = 350, // Chiều rộng
                BorderRadius = 10, // Bo góc
                BorderThickness = 1, // Độ dày viền
                BorderColor = Color.Silver, // Màu viền bạc
                Font = new Font("Times New Roman", 12), // Font Times New Roman cỡ 12
                FillColor = Color.FromArgb(250, 250, 255), // hơi xanh nhẹ cho dịu
                Margin = new Padding(0, 5, 15, 0) // Khoảng cách xung quanh
            };
            txtSearchUser.TextChanged += (s, e) => FilterTaiKhoan(); // Gọi hàm lọc khi người dùng nhập

            // Nút "Làm mới"
            btnClearFilter = new Guna2Button()
            {
                Text = "Làm mới", // Nội dung nút
                BorderRadius = 10, // Bo góc
                FillColor = Color.FromArgb(40, 120, 220), // Màu nền xanh dương
                HoverState = { FillColor = Color.FromArgb(70, 145, 245) }, // Màu khi di chuột
                ForeColor = Color.White, // Màu chữ trắng
                Font = new Font("Times New Roman", 12, FontStyle.Bold), // Font Times New Roman 12 in đậm
                Height = 40, // Chiều cao
                Width = 120, // Chiều rộng
                Margin = new Padding(10, 5, 0, 0) // Khoảng cách xung quanh
            };
            btnClearFilter.Click += (s, e) =>
            {
                txtSearchUser.Clear(); // Xóa nội dung ô tìm kiếm
                FilterTaiKhoan(); // Gọi lại hàm lọc
            };

            // Thêm các control vào thanh tìm kiếm
            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(txtSearchUser);
            searchPanel.Controls.Add(btnClearFilter);

            // Gói panel tìm kiếm để căn giữa
            Panel searchContainer = new Panel()
            {
                Dock = DockStyle.Top, // Nằm phía trên form
                Height = 80, // Chiều cao
                BackColor = Color.White // Màu nền trắng
            };
            searchContainer.Controls.Add(searchPanel);
            searchContainer.Resize += (s, e) =>
            {
                // Căn giữa panel tìm kiếm khi thay đổi kích thước
                searchPanel.Left = (searchContainer.ClientSize.Width - searchPanel.Width) / 2;
                searchPanel.Top = (searchContainer.ClientSize.Height - searchPanel.Height) / 2;
            };

            // === FORM NHẬP THÔNG TIN TÀI KHOẢN ===
            Panel cardPanel = new Panel()
            {
                BackColor = Color.White, // Màu nền trắng
                Padding = new Padding(30), // Khoảng cách lề trong
                Dock = DockStyle.Top, // Nằm trên vùng chính
                Height = 240, // Chiều cao vùng nhập liệu
            };
            cardPanel.BorderStyle = BorderStyle.None; // Không có khung viền
            //cardPanel.Paint += (s, e) =>
            //{
            //    // Vẽ khung viền xám nhạt bao quanh panel
            //    ControlPaint.DrawBorder(e.Graphics, cardPanel.ClientRectangle,
            //        Color.Gainsboro, 1, ButtonBorderStyle.Solid,
            //        Color.Gainsboro, 1, ButtonBorderStyle.Solid,
            //        Color.Gainsboro, 1, ButtonBorderStyle.Solid,
            //        Color.Gainsboro, 1, ButtonBorderStyle.Solid);
            //};

            // Bảng nhập liệu dùng TableLayoutPanel
            TableLayoutPanel form = new TableLayoutPanel()
            {
                ColumnCount = 2, // 2 cột: nhãn - ô nhập
                Padding = new Padding(0,50,0,0),
                AutoSize = true // Tự co giãn
                
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120)); // Cột nhãn rộng 120px
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); // Cột nhập chiếm phần còn lại

            Font lblFont = new Font("Times New Roman", 12, FontStyle.Bold); // Font nhãn
            Color lblColor = Color.FromArgb(45, 45, 70); // Màu nhãn

            // Ô nhập tài khoản
            txtUsername = new Guna2TextBox()
            {
                PlaceholderText = "Nhập tài khoản",
                BorderRadius = 8,
                Font = new Font("Times New Roman", 12),
                Width = 600,
            };

            // Ô nhập mật khẩu
            txtPassword = new Guna2TextBox()
            {
                PlaceholderText = "Nhập mật khẩu",
                BorderRadius = 8,
                Font = new Font("Times New Roman", 12),
                Width = 600,
                PasswordChar = '●', // Ký tự ẩn mật khẩu
                UseSystemPasswordChar = true,
                IconRight = Properties.Resources.eyebrow, // Icon con mắt bên phải
                IconRightCursor = Cursors.Hand // Cho phép nhấp chuột
            };
            txtPassword.IconRightClick += TxtPassword_IconRightClick; // Sự kiện khi bấm icon

            // Combobox chọn nhân viên
            cbNhanVien = new Guna2ComboBox()
            {
                BorderRadius = 8,
                Font = new Font("Times New Roman", 12),
                DropDownStyle = ComboBoxStyle.DropDownList, // Không cho nhập tay
                Width = 600,
                MaxDropDownItems = 6, // ✅ Giới hạn số dòng hiển thị
                IntegralHeight = false, // ✅ Không co tự động theo số lượng
            };

            // Nút "Thêm mới"
            btnSave = new Guna2Button()
            {
                Text = "➕ Thêm mới",
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen, // Màu xanh lá
                HoverState = { FillColor = Color.SeaGreen }, // Màu khi di chuột
                ForeColor = Color.White, // Màu chữ trắng
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                Width = 140,
                Height = 40,
                Margin = new Padding(0, 10, 10, 0)
            };
            btnSave.Click += BtnSave_Click; // Gắn sự kiện thêm

            // Nút "Hoàn tác"
            btnUndo = new Guna2Button()
            {
                Text = "↩️ Hoàn tác",
                BorderRadius = 8,
                FillColor = Color.Gray,
                HoverState = { FillColor = Color.DimGray },
                ForeColor = Color.White,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                Width = 120,
                Height = 40,
                Margin = new Padding(10, 10, 0, 0)
            };
            btnUndo.Click += BtnUndo_Click; // Sự kiện hoàn tác

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
            

            // Thêm các control vào form nhập liệu
            form.Controls.Add(new Label() { Text = "Tài khoản:", AutoSize = true, Font = lblFont, ForeColor = lblColor, Anchor = AnchorStyles.Left }, 0, 0);
            form.Controls.Add(txtUsername, 1, 0);
            form.Controls.Add(new Label() { Text = "Mật khẩu:", AutoSize = true, Font = lblFont, ForeColor = lblColor, Anchor = AnchorStyles.Left }, 0, 1);
            form.Controls.Add(txtPassword, 1, 1);
            form.Controls.Add(new Label() { Text = "Nhân viên:", AutoSize = true, Font = lblFont, ForeColor = lblColor, Anchor = AnchorStyles.Left }, 0, 2);
            form.Controls.Add(cbNhanVien, 1, 2);

            // Panel chứa nút thao tác
            FlowLayoutPanel btnPanel = new FlowLayoutPanel() { FlowDirection = FlowDirection.LeftToRight, Dock = DockStyle.Fill };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            btnPanel.Controls.Add(btnExportExcel);
            form.Controls.Add(btnPanel, 1, 3);

            // Căn giữa form nhập trong panel
            cardPanel.Controls.Add(form);
            form.Anchor = AnchorStyles.None;
            cardPanel.Resize += (s, e) =>
            {
                form.Left = (cardPanel.ClientSize.Width - form.Width) / 2; // Căn giữa ngang
                form.Top = (cardPanel.ClientSize.Height - form.Height) / 2; // Căn giữa dọc
            };

            // === BẢNG DỮ LIỆU (DataGridView) ===
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true, // Không cho chỉnh sửa trực tiếp
                AllowUserToAddRows = false, // Không hiển thị dòng trống cuối
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, // Chọn nguyên dòng
                MultiSelect = false,
                BackgroundColor = Color.White,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.FromArgb(250, 250, 250) } // Xen kẽ màu hàng
            };
            dgv.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.LightGrid;
            dgv.ThemeStyle.HeaderStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold); // Font header
            dgv.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(230, 240, 255); // Màu nền header
            dgv.ThemeStyle.HeaderStyle.ForeColor = Color.FromArgb(30, 60, 110); // Màu chữ header
            dgv.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(220, 230, 255); // Màu dòng khi chọn
            dgv.DefaultCellStyle.Font = new Font("Times New Roman", 12); // Font dữ liệu
            dgv.CellClick += Dgv_CellClick; // Bắt sự kiện click vào dòng

            // === BỐ CỤC CHÍNH (MAIN LAYOUT) ===
            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));  // Hàng 1: tiêu đề
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));  // Hàng 2: thanh tìm kiếm
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 240)); // Hàng 3: form nhập liệu
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  // Hàng 4: bảng dữ liệu chiếm phần còn lại

            // Thêm các thành phần vào bố cục
            main.Controls.Add(lblTitle, 0, 0);
            main.Controls.Add(searchContainer, 0, 1);
            main.Controls.Add(cardPanel, 0, 2);
            main.Controls.Add(dgv, 0, 3);

            // Thêm bố cục chính vào form
            this.Controls.Add(main);
        }


        // ======================= HIỂN THỊ MẬT KHẨU =======================
        private void TxtPassword_IconRightClick(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            txtPassword.UseSystemPasswordChar = !isPasswordVisible;
            txtPassword.PasswordChar = isPasswordVisible ? '\0' : '●';
            txtPassword.IconRight = isPasswordVisible ? Properties.Resources.eye : Properties.Resources.eyebrow;
        }

        // ======================= LOAD DANH SÁCH NHÂN VIÊN =======================
        private void LoadNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT id, TenNhanVien 
                         FROM NhanVien 
                         WHERE DaXoa = 0 
                         ORDER BY TenNhanVien";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbNhanVien.DataSource = dt;
                cbNhanVien.DisplayMember = "TenNhanVien";
                cbNhanVien.ValueMember = "id";
                cbNhanVien.SelectedIndex = -1;
            }
        }

        // ======================= LOAD TÀI KHOẢN =======================
        private void LoadTaiKhoan()
        {
            dtTaiKhoan = bllTaiKhoan.GetAllAccounts();

            dgv.DataSource = null;
            dgv.Columns.Clear();
            dgv.DataSource = dtTaiKhoan;

            // Ẩn cột "Mật khẩu"
            if (dgv.Columns["Mật khẩu"] != null)
                dgv.Columns["Mật khẩu"].Visible = false;

            // Thêm cột Xóa
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

            // ===== Căn chỉnh giao diện =====
            dgv.Columns["Xóa"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Xóa"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Chiều cao dòng đồng đều, icon vừa khít
            dgv.RowTemplate.Height = 36; // Hoặc 40 nếu bạn muốn hàng cao hơn
            dgv.ColumnHeadersHeight = 36;

            // Tắt tự động co dòng
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Thêm padding để icon không bị dính lề
            dgv.Columns["Xóa"].DefaultCellStyle.Padding = new Padding(0, 4, 0, 4);
        }

        // ======================= LỌC NHANH =======================
        private void FilterTaiKhoan()
        {
            if (dtTaiKhoan == null) return;

            string kw = txtSearchUser.Text.Replace("'", "''");
            string filter = string.IsNullOrWhiteSpace(kw)
                ? ""
                : $"[Tài khoản] LIKE '%{kw}%' OR [Mật khẩu] LIKE '%{kw}%' OR [Nhân viên] LIKE '%{kw}%'";
            dtTaiKhoan.DefaultView.RowFilter = filter;
        }

        // =============== LƯU (THÊM/CẬP NHẬT) ===============
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo");
                return;
            }

            if (bllTaiKhoan.IsUsernameExists(txtUsername.Text.Trim(), selectedId))
            {
                MessageBox.Show("Tên tài khoản đã tồn tại, vui lòng chọn tên khác!", "Cảnh báo");
                return;
            }


            DTOTaiKhoan tk = new DTOTaiKhoan
            {
                Id = selectedId ?? 0,
                TaiKhoan = txtUsername.Text.Trim(),
                MatKhau = txtPassword.Text.Trim(),
                IdNhanVien = cbNhanVien.SelectedValue?.ToString()
            };

            try
            {
                if (selectedId == null)
                {
                    // === Thêm mới ===
                    bllTaiKhoan.SaveAccount(tk, isNew: true);
                    MessageBox.Show("✅ Đã thêm tài khoản mới!");
                }
                else
                {
                    // === Cập nhật ===
                    bllTaiKhoan.SaveAccount(tk, isNew: false);
                    MessageBox.Show("✏️ Đã cập nhật tài khoản!");
                }

                LoadTaiKhoan();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu tài khoản: " + ex.Message);
            }
        }


        // ======================= CLICK DGV =======================
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xóa")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã"].Value);
                if (MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM TaiKhoan WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadTaiKhoan();
                }
                return;
            }

            selectedId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã"].Value);
            txtUsername.Text = dgv.Rows[e.RowIndex].Cells["Tài khoản"].Value.ToString();
            //txtPassword.Text = dgv.Rows[e.RowIndex].Cells["Mật khẩu"].Value.ToString();
            cbNhanVien.Text = dgv.Rows[e.RowIndex].Cells["Nhân viên"].Value.ToString();

            btnSave.Text = "✏️ Cập nhật";
            btnSave.FillColor = Color.Orange;
        }

        // ======================= HOÀN TÁC =======================
        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hoàn tác dữ liệu đang nhập?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ClearForm();
        }

        private void ClearForm()
        {
            selectedId = null;
            txtUsername.Clear();
            txtPassword.Clear();
            cbNhanVien.SelectedIndex = -1;
            btnSave.Text = "➕ Thêm mới";
            btnSave.FillColor = Color.MediumSeaGreen;
            dgv.ClearSelection();
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog()
            { Filter = "Excel Files|*.xlsx", FileName = "DanhSachTaiKhoan.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add("Tài khoản");

                            // Ghi header
                            int colIndex = 1;
                            foreach (DataGridViewColumn col in dgv.Columns)
                            {
                                if (col is DataGridViewImageColumn) continue;
                                ws.Cell(1, colIndex).Value = col.HeaderText;
                                ws.Cell(1, colIndex).Style.Font.Bold = true;
                                ws.Cell(1, colIndex).Style.Fill.BackgroundColor = XLColor.LightGray;
                                ws.Cell(1, colIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                colIndex++;
                            }

                            // Ghi data
                            int rowIndex = 2;
                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                colIndex = 1;
                                foreach (DataGridViewColumn col in dgv.Columns)
                                {
                                    if (col is DataGridViewImageColumn) continue;
                                    ws.Cell(rowIndex, colIndex).Value = row.Cells[col.Index].Value?.ToString();
                                    colIndex++;
                                }
                                rowIndex++;
                            }

                            ws.Columns().AdjustToContents();
                            wb.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("✅ Xuất Excel thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message);
                    }
                }
            }
        }
    }
}