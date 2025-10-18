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
        private Guna2ComboBox cbLyDo;
        private Guna2TextBox txtSoTien;
        private Guna2TextBox txtLyDoMoi;
        private Guna2DateTimePicker dtThangApDung;
        private CheckedListBox lstNhanVien;
        private Guna2Button btnSave, btnUndo;
        private Guna2DataGridView dgv;
        private Guna2ComboBox cbPhongBan;
        private readonly BLLNhanVien bllNhanVien;
        private readonly BLLNhanVien_ThuongPhat bllNhanVienThuongPhat;
        private readonly BLLPhongBan bllPhongBan;

        private string idNguoiTao = "GD00000001";
        private string connectionString;
        private bool isUpdating = false;  // true nếu đang cập nhật
        private int currentGroupId = -1;  // lưu Id nhóm đang edit

        public TaoKyLuat(string conn)
        {
            connectionString = conn;
            InitializeComponent();
            bllNhanVien = new BLLNhanVien(conn);
            bllNhanVienThuongPhat = new BLLNhanVien_ThuongPhat(conn);
            bllPhongBan = new BLLPhongBan(conn);
            BuildUI(); 
            LoadPhongBan();     // Load danh sách phòng ban vào cbPhongBan
            LoadLyDo();         // Load danh sách lý do thưởng vào cbLyDo
            LoadNhanVienList(); // Load danh sách nhân viên vào CheckedListBox
            LoadDanhSachKyLuat();
        }

        // ====================== GIAO DIỆN ======================
        private void BuildUI()
        {
            // Dock toàn bộ UserControl
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // ===== HEADER =====
            Label lblTitle = new Label()
            {
                Text = "📋 KỶ LUẬT NHÂN VIÊN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Maroon,
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ===== KHỐI THÔNG TIN =====
            var pnlFormCard = new Guna2Panel()
            {
                BorderRadius = 12,
                FillColor = Color.White,
                Padding = new Padding(50, 30, 50, 30),
                ShadowDecoration = { Depth = 10, Enabled = true },
                Dock = DockStyle.Fill // fill để TableLayoutPanel bên trong hiển thị đầy đủ
            };

            // === TABLELAYOUT CHO FORM NHẬP LIỆU ===
            TableLayoutPanel tlForm = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                BackColor = Color.White,
                Padding = new Padding(0, 10, 0, 0)
            };
            tlForm.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
            tlForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Mỗi dòng cao 50px, riêng nhân viên 100px
            for (int i = 0; i < 6; i++)
                tlForm.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 1 ? 100 : 50));

            Label MakeLabel(string text) => new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 10.5f),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            // ===== CONTROL INPUT =====
            // Panel combo + nút tìm kiếm
            FlowLayoutPanel pnlPhongBanSearch = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };

            cbPhongBan = new Guna2ComboBox
            {
                BorderRadius = 8,
                Size = new Size(250, 36),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };

            Guna2Button btnSearch = new Guna2Button
            {
                Size = new Size(40, 36),
                Margin = new Padding(8, 0, 0, 0),
                Image = Properties.Resources.search,
                ImageSize = new Size(18, 18),
                FillColor = Color.MediumSlateBlue,
                BorderRadius = 8,
                Cursor = Cursors.Hand
            };
            btnSearch.Click += btnTimKiem_Click;
            btnSearch.MouseEnter += (s, e) =>
            {
                btnSearch.Image = Properties.Resources.magnifying_glass;
                btnSearch.FillColor = Color.SlateBlue;
            };
            btnSearch.MouseLeave += (s, e) =>
            {
                btnSearch.Image = Properties.Resources.search; // trở lại icon cũ
                btnSearch.FillColor = Color.MediumSlateBlue; // khôi phục màu
            };

            pnlPhongBanSearch.Controls.Add(cbPhongBan);
            pnlPhongBanSearch.Controls.Add(btnSearch);

            lstNhanVien = new CheckedListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White
            };

            cbLyDo = new Guna2ComboBox
            {
                BorderRadius = 8,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            cbLyDo.SelectedIndexChanged += CbLyDo_SelectedIndexChanged;

            txtLyDoMoi = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập lý do mới...",
                Font = new Font("Segoe UI", 10),
                DisabledState = { FillColor = Color.FromArgb(245, 245, 245) }
            };

            txtSoTien = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập số tiền phạt...",
                Font = new Font("Segoe UI", 10)
            };

            dtThangApDung = new Guna2DateTimePicker
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Font = new Font("Segoe UI", 10)
            };

            // === THÊM CONTROL VÀO TABLELAYOUT ===
            tlForm.Controls.Add(MakeLabel("Tìm theo phòng ban:"), 0, 0);
            tlForm.Controls.Add(pnlPhongBanSearch, 1, 0);
            tlForm.Controls.Add(MakeLabel("Nhân viên:"), 0, 1);
            tlForm.Controls.Add(lstNhanVien, 1, 1);
            tlForm.Controls.Add(MakeLabel("Lý do có sẵn:"), 0, 2);
            tlForm.Controls.Add(cbLyDo, 1, 2);
            tlForm.Controls.Add(MakeLabel("Hoặc lý do mới:"), 0, 3);
            tlForm.Controls.Add(txtLyDoMoi, 1, 3);
            tlForm.Controls.Add(MakeLabel("Số tiền:"), 0, 4);
            tlForm.Controls.Add(txtSoTien, 1, 4);
            tlForm.Controls.Add(MakeLabel("Ngày áp dụng:"), 0, 5);
            tlForm.Controls.Add(dtThangApDung, 1, 5);

            pnlFormCard.Controls.Add(tlForm);

            // ===== NÚT CHỨC NĂNG =====
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 10, 40, 0),
                BackColor = Color.Transparent,
                Height = 60
            };

            btnSave = new Guna2Button
            {
                Text = "💾 Lưu kỷ luật",
                Width = 180,
                Height = 45,
                BorderRadius = 8,
                FillColor = Color.FromArgb(45, 140, 90),
                Font = new Font("Segoe UI Semibold", 10.5f),
                ForeColor = Color.White
            };
            btnSave.Click += BtnSave_Click;

            btnUndo = new Guna2Button
            {
                Text = "↩️ Hoàn tác",
                Width = 160,
                Height = 45,
                BorderRadius = 8,
                FillColor = Color.FromArgb(130, 130, 130),
                Font = new Font("Segoe UI Semibold", 10.5f),
                ForeColor = Color.White
            };
            btnUndo.Click += BtnUndo_Click;

            pnlButtons.Controls.Add(btnSave);
            pnlButtons.Controls.Add(btnUndo);

            // ===== DATAGRIDVIEW =====
            dgv = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ColumnHeadersHeight = 38,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(230, 235, 245),
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(248, 250, 255)
                }
            };

            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(45, 85, 155),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(94, 148, 255),
                SelectionForeColor = Color.Black
            };

            dgv.CellClick += Dgv_CellClick;
            dgv.CellMouseEnter += Dgv_CellMouseEnter;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;

            // ===== LAYOUT CHÍNH =====
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));   // tiêu đề
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 430));  // form nhập liệu
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));   // nút
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));   // DataGridView

            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(pnlFormCard, 0, 1);
            mainLayout.Controls.Add(pnlButtons, 0, 2);
            mainLayout.Controls.Add(dgv, 0, 3);

            this.Controls.Add(mainLayout);
        }


        // ====================== LOAD DỮ LIỆU ======================

        private void LoadPhongBan()
        {
            var dt = bllPhongBan.ComboboxPhongBan();    // gọi BLL trả về DataTable

            // Tạo một dòng "Xem tất cả" thủ công
            DataRow allRow = dt.NewRow();
            allRow["id"] = DBNull.Value;                          // giá trị rỗng để khi SelectedValue = "" => hiểu là xem tất cả
            allRow["TenPhongBan"] = "Xem tất cả";
            dt.Rows.InsertAt(allRow, 0);                // chèn lên đầu danh sách

            cbPhongBan.DataSource = dt;                 // bind datatable
            cbPhongBan.DisplayMember = "TenPhongBan";   // trường hiển thị cho người dùng
            cbPhongBan.ValueMember = "id";              // giá trị tương ứng (dùng khi lấy SelectedValue)
            cbPhongBan.SelectedIndex = 0;               // mặc định chọn "Xem tất cả"
        }

        // Load danh sách lý do (Thưởng) vào combobox cbLyDo
        private void LoadLyDo()
        {
            var dt = bllNhanVienThuongPhat.GetAllLyDo("Phạt"); // gọi BLL lấy lý do theo loại "Phạt"
            // Copy DataTable để thêm dòng "Thêm lý do mới"
            DataTable dt2 = dt.Copy();
            DataRow r = dt2.NewRow();
            r["id"] = -1;                            // id = -1 biểu thị "Thêm mới"
            r["lyDo"] = "-- Thêm lý do mới --";     // hiển thị cho người dùng
            r["tienThuongPhat"] = 0;                 // giá trị tiền mặc định
            dt2.Rows.Add(r);                         // thêm dòng ở cuối (có thể InsertAt để ở đầu) => dt2.Rows.InsertAt(r, 0);.

            // Bind combobox
            cbLyDo.DisplayMember = "lyDo";
            cbLyDo.ValueMember = "id";
            cbLyDo.DataSource = dt2;
            cbLyDo.SelectedIndex = -1;               // không chọn item nào khi load xong
        }

        // Load danh sách nhân viên vào CheckedListBox (hiển thị "id - TenNhanVien")
        private void LoadNhanVienList()
        {
            var dt = bllNhanVien.ComboboxNhanVien(); // lấy DataTable nhân viên
            lstNhanVien.Items.Clear();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                // Lấy id và name từ row và thêm CLBItem (class helper) vào CheckedListBox
                string id = row["id"].ToString();
                string name = row["TenNhanVien"].ToString();
                lstNhanVien.Items.Add(new CLBItem(id, name));
            }
        }

        private void LoadDanhSachKyLuat(string idPhongBan = "")
        {
            dgv.DataSource = bllNhanVienThuongPhat.GetAll("Phạt", idPhongBan); // bind DataTable trả về từ BLL
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

        // ====================== SỰ KIỆN ======================
        // Event: khi combobox lý do thay đổi -> fill tiền hoặc bật ô nhập lý do mới
        private void CbLyDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLyDo.SelectedItem == null) return;  // nếu chưa chọn item thì thoát

            // Khi binding với DataTable, SelectedValue đôi khi là DataRowView (trong quá trình bind)
            if (cbLyDo.SelectedValue == null || cbLyDo.SelectedValue is DataRowView)
                return; // bỏ qua nếu SelectedValue chưa phải là giá trị id thực sự

            int id = Convert.ToInt32(cbLyDo.SelectedValue); // chuyển SelectedValue về int (id)

            if (id == -1) // Nếu chọn "Thêm mới"
            {
                txtLyDoMoi.Enabled = true;           // bật textbox nhập lý do mới
                txtLyDoMoi.FillColor = Color.White;  // set nền trắng (để rõ là có thể nhập)
                txtSoTien.Enabled = true;            // bật textbox số tiền để nhập
                txtSoTien.Text = "";                 // xóa giá trị hiện có
            }
            else
            {
                // Nếu chọn lý do có sẵn
                txtLyDoMoi.Enabled = false;          // tắt nhập lý do mới
                txtLyDoMoi.Text = "";                // clear nội dung ô lý do mới

                // Lấy DataRowView ứng với item được chọn để đọc giá trị tỉ lệ tiền
                var drv = cbLyDo.SelectedItem as DataRowView;
                if (drv != null)
                {
                    decimal tien = 0;
                    // parse giá trị tiền từ trường 'tienThuongPhat' (nếu có)
                    decimal.TryParse(drv["tienThuongPhat"]?.ToString(), out tien);
                    txtSoTien.Text = tien.ToString("0.##"); // format hiển thị
                    txtSoTien.Enabled = false;              // không cho sửa nếu chọn lý do có sẵn
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị được chọn trong combobox
                var selectedValue = cbPhongBan.SelectedValue;
                string idPhongBan = (selectedValue == DBNull.Value || selectedValue == null)
                    ? null
                    : selectedValue.ToString();

                // Lấy tên phòng ban để hiển thị thông báo
                string tenPhongBan = cbPhongBan.Text?.Trim();

                DataTable dtResult;

                // Nếu chọn “Xem tất cả” hoặc chưa chọn phòng ban
                if (string.IsNullOrEmpty(idPhongBan))
                {
                    dtResult = bllNhanVienThuongPhat.GetAll("Phạt");
                    MessageBox.Show("Đang hiển thị danh sách kỷ luật của tất cả phòng ban.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dtResult = bllNhanVienThuongPhat.GetAll("Phạt", idPhongBan);

                    if (dtResult.Rows.Count == 0)
                    {
                        MessageBox.Show($"Không tìm thấy nhân viên bị kỷ luật trong phòng ban '{tenPhongBan}'.",
                                        "Kết quả trống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Đã tìm thấy {dtResult.Rows.Count} nhân viên bị kỷ luật trong phòng ban '{tenPhongBan}'.",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Gán kết quả vào DataGridView
                dgv.DataSource = dtResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tìm kiếm: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Xác định loại từ form
                string loai = this.Tag?.ToString() ?? "Phạt"; // nếu form "Thưởng" thì Tag = "Thưởng"
                List<string> selectedIds = new List<string>();

                foreach (CLBItem item in lstNhanVien.CheckedItems)
                    selectedIds.Add(item.Id);

                if (selectedIds.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một nhân viên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string lyDo;
                decimal soTien;

                // Xử lý lý do (chọn cũ hoặc nhập mới)
                if (cbLyDo.SelectedIndex < 0 || Convert.ToInt32(cbLyDo.SelectedValue) == -1)
                {
                    if (string.IsNullOrWhiteSpace(txtLyDoMoi.Text))
                    {
                        MessageBox.Show("Vui lòng nhập lý do mới.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!decimal.TryParse(txtSoTien.Text, out soTien))
                    {
                        MessageBox.Show("Số tiền không hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    lyDo = txtLyDoMoi.Text.Trim();
                }
                else
                {
                    lyDo = cbLyDo.Text.Trim();
                    soTien = string.IsNullOrEmpty(txtSoTien.Text) ? 0 : Convert.ToDecimal(txtSoTien.Text);
                }

                DateTime ngayApDung = dtThangApDung.Value;

                // === Cập nhật nhóm cũ ===
                if (dgv.CurrentRow != null && dgv.CurrentRow.Cells["id"].Value != null)
                {
                    int idThuongPhat = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value);
                    bllNhanVienThuongPhat.UpdateMultiSmart(loai, idThuongPhat, selectedIds, lyDo, soTien, ngayApDung);

                    MessageBox.Show($"Cập nhật {loai.ToLower()} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // === Tạo mới nhóm ===
                    bllNhanVienThuongPhat.SaveMulti(loai, selectedIds, lyDo, soTien, ngayApDung, idNguoiTao);

                    MessageBox.Show($"Thêm mới {loai.ToLower()} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadLyDo();
                LoadDanhSachKyLuat(); // hoặc LoadDanhSachThuong() nếu là form thưởng
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hoàn tác thay đổi?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ClearForm();
        }

        private void ClearForm()
        {
            for (int i = 0; i < lstNhanVien.Items.Count; i++)
                lstNhanVien.SetItemChecked(i, false);

            cbLyDo.SelectedIndex = 0;
            txtLyDoMoi.Text = "";
            txtSoTien.Text = "";
            dtThangApDung.Value = DateTime.Now;
            btnSave.Text = "Thêm mới";
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgv.Rows[e.RowIndex].IsNewRow) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                // Xử lý xóa
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
                if (MessageBox.Show("Xóa nhóm phạt này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bllNhanVienThuongPhat.Delete(id);
                    LoadData();
                }
                return;
            }

            // 1️⃣ Lấy dữ liệu dòng hiện tại
            currentGroupId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
            txtSoTien.Text = dgv.Rows[e.RowIndex].Cells["SoTien"].Value.ToString();
            cbLyDo.Text = dgv.Rows[e.RowIndex].Cells["LyDo"].Value.ToString();
            dtThangApDung.Value = Convert.ToDateTime(dgv.Rows[e.RowIndex].Cells["NgayApDung"].Value);

            // 2️⃣ Lấy danh sách nhân viên thuộc nhóm này
            var empIds = bllNhanVienThuongPhat.GetNhanVienByThuongPhatId(currentGroupId);

            // 3️⃣ Reset check
            for (int i = 0; i < lstNhanVien.Items.Count; i++)
            {
                var item = lstNhanVien.Items[i] as CLBItem;
                lstNhanVien.SetItemChecked(i, empIds.Contains(item.Id));
            }

            // 4️⃣ Chuyển sang chế độ cập nhật
            isUpdating = true;
            btnSave.Text = "Cập nhật";
        }

        private void LoadData(string idPhongBan = "")
        {
            dgv.DataSource = bllNhanVienThuongPhat.GetAll("Phạt", idPhongBan); // bind DataTable trả về từ BLL

            // ensure delete column exists: nếu chưa có cột "Xoa" (icon) thì thêm vào
            if (!dgv.Columns.Contains("Xoa"))
            {
                DataGridViewImageColumn colDelete = new DataGridViewImageColumn
                {
                    Name = "Xoa",
                    HeaderText = "Xóa",
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 50
                };
                // cố gắng set icon từ resources nếu có, nếu không có thì bỏ qua (không ném lỗi)
                try { colDelete.Image = Properties.Resources.delete; } catch { }
                dgv.Columns.Add(colDelete);
            }
        }

        // DataGridView: khi di chuột vào cột Xóa thì đổi con trỏ và icon
        private void Dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            // kiểm tra index hợp lệ và tên cột là "Xoa"
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Hand; // đổi con trỏ chuột
                try { dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.trash; } catch { }
                // cố gắng gán icon trash (nếu resource có), try-catch để tránh ném lỗi
            }
        }

        // DataGridView: khi chuột rời cột Xóa thì phục hồi icon mặc định
        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Default;
                try { dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.delete; } catch { }
            }
        }

        // helper class cho CheckedListBox item: lưu Id + Name, ToString() trả về "id - Name"
        private class CLBItem
        {
            public string Id { get; }
            public string Name { get; }
            public CLBItem(string id, string name) { Id = id; Name = name; }
            public override string ToString() => $"{Id} - {Name}";
        }
    }
}