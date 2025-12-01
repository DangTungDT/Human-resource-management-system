using BLL;
using ClosedXML.Excel;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class TaoNhanVien_PhuCap : UserControl
    {
        private readonly BLLNhanVien_PhuCap bll;
        private readonly BLLPhongBan bllPhongBan;
        private readonly BLLNhanVien bllNhanVien;
        private readonly BLLPhuCap bllPhuCap;

        private Guna2ComboBox cbPhongBan, cbNhanVien, cbPhuCap, cbTrangThai;
        private Guna2TextBox txtSoTien, txtLyDoMoi;
        private Guna2Button btnSave, btnUndo, btnSearch, btnExport;
        private Guna2DataGridView dgv;

        private bool isUpdating = false;
        private string currentIdNhanVien = "";
        private int currentIdPhuCap = -1;
        private readonly string idNguoiTao;

        public TaoNhanVien_PhuCap(string idNhanVien, string conn)
        {
            InitializeComponent();
            idNguoiTao = idNhanVien;

            bll = new BLLNhanVien_PhuCap(conn);
            bllPhongBan = new BLLPhongBan(conn);
            bllNhanVien = new BLLNhanVien(conn);
            bllPhuCap = new BLLPhuCap(conn);

            BuildUI();
            LoadPhongBan();
            LoadPhuCap();
            LoadTrangThai();
            LoadNhanVienList();
            LoadData();
        }

        #region UI
        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            Label lblTitle = new Label
            {
                Text = "QUẢN LÝ PHỤ CẤP NHÂN VIÊN",
                Dock = DockStyle.Top,
                Height = 60,
                Font = new Font("Times New Roman", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 100, 180),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White
            };

            Label MakeLabel(string text) => new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight, // căn phải và giữa dọc
                ForeColor = Color.FromArgb(60, 60, 60),
                Padding = new Padding(0, 0, 5, 0), // đẩy chữ sát control hơn
            };

            Guna2Panel pnlFormCard = new Guna2Panel
            {
                BorderRadius = 12,
                FillColor = Color.White,
                ShadowDecoration = { Depth = 10, Enabled = true },
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 15, 30, 15)
            };

            // Tìm kiếm phòng ban
            FlowLayoutPanel pnlPhongBanSearch = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };

            cbPhongBan = new Guna2ComboBox
            {
                BorderRadius = 8,
                Size = new Size(200, 36),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Times New Roman", 10.5f),
                MaxDropDownItems = 6
            };
            cbPhongBan.SelectedIndexChanged += CbPhongBan_SelectedIndexChanged;

            btnSearch = new Guna2Button
            {
                Size = new Size(38, 36),
                Margin = new Padding(8, 0, 0, 0),
                Image = Properties.Resources.search,
                ImageSize = new Size(18, 18),
                FillColor = Color.FromArgb(90, 100, 255),
                BorderRadius = 8,
                Cursor = Cursors.Hand
            };
            btnSearch.Click += BtnTimKiem_Click;

            pnlPhongBanSearch.Controls.Add(cbPhongBan);
            pnlPhongBanSearch.Controls.Add(btnSearch);

            TableLayoutPanel pnlSearch = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                ColumnCount = 2,
                Padding = new Padding(20, 0, 0, 5)
            };
            pnlSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            pnlSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            pnlSearch.Controls.Add(MakeLabel("Tìm theo phòng ban:"), 0, 0);
            pnlSearch.Controls.Add(pnlPhongBanSearch, 1, 0);

            // Form chính
            TableLayoutPanel tlMainForm = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            tlMainForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));
            tlMainForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52));

            // Bên trái
            TableLayoutPanel tlLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(10, 5, 20, 5)
            };
            tlLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            tlLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 3; i++) tlLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            cbNhanVien = new Guna2ComboBox
            {
                BorderRadius = 8,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 10.5f),
                MaxDropDownItems = 6
            };

            cbPhuCap = new Guna2ComboBox
            {
                BorderRadius = 8,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 10.5f),
                MaxDropDownItems = 6
            };
            cbPhuCap.SelectedIndexChanged += CbPhuCap_SelectedIndexChanged;

            txtLyDoMoi = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập lý do mới...",
                Font = new Font("Times New Roman", 10.5f),
                Enabled = false
            };

            tlLeft.Controls.Add(MakeLabel("Nhân viên:"), 0, 0);
            tlLeft.Controls.Add(cbNhanVien, 1, 0);
            tlLeft.Controls.Add(MakeLabel("Phụ cấp có sẵn:"), 0, 1);
            tlLeft.Controls.Add(cbPhuCap, 1, 1);
            tlLeft.Controls.Add(MakeLabel("Phụ cấp mới:"), 0, 2);
            tlLeft.Controls.Add(txtLyDoMoi, 1, 2);

            // Bên phải
            TableLayoutPanel tlRight = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(20, 5, 10, 5)
            };
            tlRight.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
            tlRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 2; i++) tlRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            txtSoTien = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Số tiền...",
                Font = new Font("Times New Roman", 10.5f),
                Enabled = false
            };

            cbTrangThai = new Guna2ComboBox
            {
                BorderRadius = 8,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 10.5f)
            };

            tlRight.Controls.Add(MakeLabel("Số tiền:"), 0, 0);
            tlRight.Controls.Add(txtSoTien, 1, 0);
            tlRight.Controls.Add(MakeLabel("Trạng thái:"), 0, 1);
            tlRight.Controls.Add(cbTrangThai, 1, 1);
            tlRight.Controls.Add(MakeLabel(""), 0, 2);

            tlMainForm.Controls.Add(tlLeft, 0, 0);
            tlMainForm.Controls.Add(tlRight, 1, 0);

            pnlFormCard.Controls.Add(tlMainForm);
            pnlFormCard.Controls.Add(pnlSearch);

            // Nút chức năng
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 5, 40, 0),
                Height = 55
            };

            btnExport = new Guna2Button
            {
                Text = "Xuất Excel",
                Width = 140,
                Height = 42,
                BorderRadius = 8,
                FillColor = Color.FromArgb(0, 120, 215),
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                ForeColor = Color.White,
                ImageSize = new Size(20, 20),
                ImageAlign = HorizontalAlignment.Left,
                TextAlign = HorizontalAlignment.Center
            };
            btnExport.Click += BtnExport_Click;

            btnSave = new Guna2Button
            {
                Text = "Lưu phụ cấp",
                Width = 160,
                Height = 42,
                BorderRadius = 8,
                FillColor = Color.FromArgb(45, 140, 90),
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnSave.Click += BtnSave_Click;

            btnUndo = new Guna2Button
            {
                Text = "Hoàn tác",
                Width = 140,
                Height = 42,
                BorderRadius = 8,
                FillColor = Color.FromArgb(130, 130, 130),
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnUndo.Click += BtnUndo_Click;

            pnlButtons.Controls.Add(btnExport);
            pnlButtons.Controls.Add(btnSave);
            pnlButtons.Controls.Add(btnUndo);

            // DataGridView
            dgv = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ColumnHeadersHeight = 36,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(230, 235, 245),
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(248, 250, 255),
                    ForeColor = Color.Black // 👈 chữ màu đen cho hàng xen kẽ
                }
            };

            // Header
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0, 100, 180),
                ForeColor = Color.White,
                Font = new Font("Times New Roman", 10.5f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Dòng dữ liệu
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Times New Roman", 10),
                ForeColor = Color.Black, // 👈 chữ màu đen chính
                SelectionBackColor = Color.FromArgb(94, 148, 255),
                SelectionForeColor = Color.Black // 👈 chữ vẫn đen khi chọn
            };

            // Sự kiện
            dgv.CellClick += Dgv_CellClick;
            dgv.CellMouseEnter += Dgv_CellMouseEnter;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 230));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(pnlFormCard, 0, 1);
            mainLayout.Controls.Add(pnlButtons, 0, 2);
            mainLayout.Controls.Add(dgv, 0, 3);

            this.Controls.Add(mainLayout);
        }
        #endregion

        #region Định dạng DGV
        private void DinhDangCotDgv()
        {
            if (dgv.Columns.Count == 0) return;

            // Ẩn cột ID
            if (dgv.Columns.Contains("idNhanVien")) dgv.Columns["idNhanVien"].Visible = false;
            if (dgv.Columns.Contains("idPhuCap")) dgv.Columns["idPhuCap"].Visible = false;

            // Đổi tên cột
            if (dgv.Columns.Contains("TenNhanVien")) dgv.Columns["TenNhanVien"].HeaderText = "Nhân viên";
            if (dgv.Columns.Contains("TenPhongBan")) dgv.Columns["TenPhongBan"].HeaderText = "Phòng ban";
            if (dgv.Columns.Contains("LyDoPhuCap")) dgv.Columns["LyDoPhuCap"].HeaderText = "Phụ cấp";
            if (dgv.Columns.Contains("SoTien")) dgv.Columns["SoTien"].HeaderText = "Số tiền";
            if (dgv.Columns.Contains("trangThai")) dgv.Columns["trangThai"].HeaderText = "Trạng thái";

            // Format
            if (dgv.Columns.Contains("SoTien"))
                dgv.Columns["SoTien"].DefaultCellStyle.Format = "N0";
            if (dgv.Columns.Contains("trangThai"))
                dgv.Columns["trangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.EnableHeadersVisualStyles = false;
        }
        #endregion

        #region Load Data
        private void LoadPhongBan()
        {
            var dt = bllPhongBan.ComboboxPhongBan();

            // Đảm bảo cột id là int
            if (dt.Columns.Contains("id") && !dt.Columns["id"].DataType.Equals(typeof(int)))
            {
                dt.Columns["id"].DataType = typeof(int);
            }

            DataRow allRow = dt.NewRow();
            allRow["id"] = DBNull.Value;
            allRow["TenPhongBan"] = "Xem tất cả";
            dt.Rows.InsertAt(allRow, 0);

            cbPhongBan.DataSource = dt;
            cbPhongBan.DisplayMember = "TenPhongBan";
            cbPhongBan.ValueMember = "id"; // <-- Đảm bảo cột này là int
            cbPhongBan.SelectedIndex = 0;
        }

        private void LoadPhuCap()
        {
            var dt = bllPhuCap.GetAll();
            DataTable dtCopy = dt.Copy();

            DataRow newRow = dtCopy.NewRow();
            newRow["id"] = -1;
            newRow["lyDoPhuCap"] = "-- Thêm phụ cấp mới --";
            newRow["soTien"] = 0;
            dtCopy.Rows.Add(newRow); // Thêm vào cuối

            cbPhuCap.DataSource = dtCopy;
            cbPhuCap.DisplayMember = "lyDoPhuCap";
            cbPhuCap.ValueMember = "id";
            cbPhuCap.SelectedIndex = dtCopy.Rows.Count - 1; // Chọn dòng mới
        }

        private void LoadTrangThai()
        {
            var dt = new DataTable();
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Text", typeof(string));
            dt.Rows.Add("Hoạt động", "Hoạt động");
            dt.Rows.Add("Ngưng hoạt động", "Ngưng hoạt động");

            cbTrangThai.DataSource = dt;
            cbTrangThai.DisplayMember = "Text";
            cbTrangThai.ValueMember = "Value";
            cbTrangThai.SelectedIndex = 0;
        }

        private void LoadNhanVienList(int? idPhongBan = null)
        {
            var dt = bllNhanVien.ComboboxNhanVien(idPhongBan);
            DataRow empty = dt.NewRow();
            empty["id"] = "";
            empty["TenNhanVien"] = "-- Chọn nhân viên --";
            dt.Rows.InsertAt(empty, 0);

            cbNhanVien.DataSource = dt;
            cbNhanVien.DisplayMember = "TenNhanVien";
            cbNhanVien.ValueMember = "id";
            cbNhanVien.SelectedIndex = 0;
        }

        private void LoadData(int? idPhongBan = null)
        {
            var dt = bll.GetAllWithDetails(idPhongBan);
            dgv.DataSource = dt;
            DinhDangCotDgv();

            if (!dgv.Columns.Contains("Xoa"))
            {
                var col = new DataGridViewImageColumn
                {
                    Name = "Xoa",
                    HeaderText = "Xóa",
                    Image = Properties.Resources.delete,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 50
                };
                dgv.Columns.Add(col);
            }
        }
        #endregion

        #region Events
        private void CbPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPhongBan.SelectedItem == null) return;

            var selectedItem = cbPhongBan.SelectedItem as DataRowView;
            int? idPhongBan = selectedItem != null && selectedItem["id"] != DBNull.Value ? (int?)Convert.ToInt32(selectedItem["id"]) : null;
            LoadNhanVienList(idPhongBan);
            LoadData(idPhongBan);
        }

        private void CbPhuCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPhuCap.SelectedItem == null)
                return; // chưa chọn item nào thì thoát

            // Khi binding ComboBox bằng DataTable, trong lúc nạp dữ liệu SelectedValue có thể là DataRowView
            if (cbPhuCap.SelectedValue == null || cbPhuCap.SelectedValue is DataRowView)
                return;

            int id = Convert.ToInt32(cbPhuCap.SelectedValue);

            if (id == -1) // Nếu chọn "Thêm mới"
            {
                txtLyDoMoi.Enabled = true;
                txtLyDoMoi.FillColor = Color.White; // nền trắng để dễ nhìn
                txtLyDoMoi.Text = "";

                txtSoTien.Enabled = true;
                txtSoTien.Text = "";
            }
            else
            {
                // Nếu chọn phụ cấp có sẵn
                txtLyDoMoi.Enabled = false;
                txtLyDoMoi.Text = "";

                var drv = cbPhuCap.SelectedItem as DataRowView;
                if (drv != null)
                {
                    decimal soTien = 0;
                    decimal.TryParse(drv["soTien"]?.ToString(), out soTien);
                    txtSoTien.Text = soTien.ToString("0.##");
                    txtSoTien.Enabled = false;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Kiểm tra nhân viên
                if (cbNhanVien.SelectedValue == null || string.IsNullOrWhiteSpace(cbNhanVien.SelectedValue.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string idNhanVien = cbNhanVien.SelectedValue.ToString();
                int idPhuCap = -1;
                decimal soTien = 0;
                string lyDoPhuCap = string.Empty;

                // 2️⃣ Xử lý lý do phụ cấp (chọn có sẵn hoặc thêm mới)
                if (cbPhuCap.SelectedIndex < 0 || Convert.ToInt32(cbPhuCap.SelectedValue) == -1)
                {
                    // Người dùng chọn "Thêm mới"
                    if (string.IsNullOrWhiteSpace(txtLyDoMoi.Text))
                    {
                        MessageBox.Show("Vui lòng nhập lý do phụ cấp mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!decimal.TryParse(txtSoTien.Text.Replace(",", ""), out soTien) || soTien < 0)
                    {
                        MessageBox.Show("Số tiền không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    lyDoPhuCap = txtLyDoMoi.Text.Trim();

                    DTOPhuCap pc = new DTOPhuCap
                    {
                        LyDoPhuCap = lyDoPhuCap,
                        SoTien = soTien
                    };

                    // 🟢 Gọi BLL để thêm phụ cấp mới và nhận lại ID
                    idPhuCap = bllPhuCap.InsertPhuCapMoi(lyDoPhuCap, soTien);
                    if (idPhuCap <= 0)
                    {
                        MessageBox.Show("Không thể thêm phụ cấp mới!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Sau khi thêm mới, load lại combobox và chọn đúng dòng vừa thêm
                    LoadPhuCap();
                    cbPhuCap.SelectedValue = idPhuCap;
                }
                else
                {
                    // Người dùng chọn phụ cấp có sẵn
                    var selectedPC = cbPhuCap.SelectedItem as DataRowView;
                    if (selectedPC == null || selectedPC["id"] == DBNull.Value)
                    {
                        MessageBox.Show("Phụ cấp không hợp lệ hoặc không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    idPhuCap = Convert.ToInt32(selectedPC["id"]);
                    lyDoPhuCap = cbPhuCap.Text.Trim();
                    decimal.TryParse(selectedPC["soTien"]?.ToString(), out soTien);
                }

                // 3️⃣ Tạo DTO
                DTONhanVien_PhuCap nvpc = new DTONhanVien_PhuCap
                {
                    IDNhanVien = idNhanVien,
                    IDPhuCap = idPhuCap,
                    TrangThai = cbTrangThai.SelectedValue?.ToString() ?? "Hoạt động"
                };

                // 4️⃣ Thêm hoặc cập nhật
                if (!isUpdating)
                {
                    if (MessageBox.Show("Xác nhận thêm phụ cấp cho nhân viên?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    if (bll.KtraThemNhanVien_PhuCap1(nvpc))
                        MessageBox.Show("Thêm phụ cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Nhân viên này đã có phụ cấp này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Xác nhận cập nhật phụ cấp nhân viên?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    if (bll.KtraCapNhatNhanVien_PhuCap1(nvpc))
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Cập nhật thất bại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    isUpdating = false;
                    btnSave.Text = "💾 Lưu phụ cấp";
                }

                // 5️⃣ Làm mới dữ liệu
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUndo_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            cbNhanVien.SelectedIndex = 0;
            LoadPhuCap(); // Reset về dòng "Thêm mới"
            txtLyDoMoi.Text = "";
            txtSoTien.Text = "";
            cbTrangThai.SelectedIndex = 0;
            btnSave.Text = "Lưu phụ cấp";
            isUpdating = false;
            currentIdNhanVien = "";
            currentIdPhuCap = -1;
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                string idNV = dgv.Rows[e.RowIndex].Cells["idNhanVien"].Value.ToString();
                int idPC = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["idPhuCap"].Value);

                if (MessageBox.Show("Xóa phụ cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var dto = new DTONhanVien_PhuCap { IDNhanVien = idNV, IDPhuCap = idPC };
                    bll.KtraXoaNhanVien_PhuCap1(dto);
                    LoadData();
                }
                return;
            }

            // Chọn dòng để sửa
            currentIdNhanVien = dgv.Rows[e.RowIndex].Cells["idNhanVien"].Value.ToString();
            currentIdPhuCap = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["idPhuCap"].Value);
            string trangThai = dgv.Rows[e.RowIndex].Cells["trangThai"].Value.ToString();

            cbNhanVien.SelectedValue = currentIdNhanVien;
            cbPhuCap.SelectedValue = currentIdPhuCap;
            cbTrangThai.SelectedValue = trangThai;

            isUpdating = true;
            btnSave.Text = "Cập nhật";
        }

        private void Dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Hand;
                try { dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.trash; } catch { }
            }
        }

        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Default;
                try { dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.delete; } catch { }
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            int? id = cbPhongBan.SelectedValue is DBNull ? null : (int?)cbPhongBan.SelectedValue;
            LoadData(id);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = dgv.DataSource as DataTable;
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog save = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Xuất danh sách phụ cấp nhân viên",
                    FileName = $"PhuCap_NhanVien_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                };

                if (save.ShowDialog() != DialogResult.OK) return;

                // Tạo bản sao, loại bỏ cột không cần thiết
                DataTable exportDt = dt.Copy();
                foreach (var col in new[] { "Xoa", "idNhanVien", "idPhuCap" })
                    if (exportDt.Columns.Contains(col)) exportDt.Columns.Remove(col);

                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Phụ cấp");

                    // Chèn bảng dữ liệu (bắt đầu từ A1)
                    var table = ws.Cell(1, 1).InsertTable(exportDt, false);

                    // === ĐỊNH DẠNG TIÊU ĐỀ ===
                    var header = ws.Range(1, 1, 1, exportDt.Columns.Count);
                    header.Style
                        .Font.SetBold()
                        .Font.SetFontColor(XLColor.White)
                        .Fill.SetBackgroundColor(XLColor.FromArgb(0, 100, 180))
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                        .Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                    // === ĐỊNH DẠNG CỘT TIỀN ===
                    if (exportDt.Columns.Contains("Số tiền"))
                    {
                        var moneyCol = ws.Column(exportDt.Columns["Số tiền"].Ordinal + 1);
                        moneyCol.Style.NumberFormat.Format = "#,##0";
                        moneyCol.Width = 15;
                    }

                    // === ĐỊNH DẠNG CỘT TRẠNG THÁI ===
                    if (exportDt.Columns.Contains("Trạng thái"))
                    {
                        var statusCol = ws.Column(exportDt.Columns["Trạng thái"].Ordinal + 1);
                        statusCol.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }

                    // === TỰ ĐỘNG ĐIỀU CHỈNH ĐỘ RỘNG CỘT ===
                    ws.Columns().AdjustToContents();

                    // === THÊM ĐƯỜNG VIỀN CHO BẢNG ===
                    table.Theme = XLTableTheme.None;
                    table.ShowAutoFilter = false;
                    table.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    table.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    // === LƯU FILE ===
                    wb.SaveAs(save.FileName);
                }

                MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}