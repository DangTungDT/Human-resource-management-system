using BLL;
using DTO;
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

namespace GUI
{
    public partial class TaoNhanVien_KhauTru : UserControl
    {
        private readonly BLLNhanVien_KhauTru bll;
        private readonly BLLPhongBan bllPhongBan;
        private readonly BLLNhanVien bllNhanVien;
        private Guna2ComboBox cbPhongBan;
        private Guna2ComboBox cbNhanVien;
        private Guna2ComboBox cbLyDo;
        private Guna2TextBox txtAmount;
        private Guna2DateTimePicker dtThangApDung;
        private Guna2Button btnSave, btnUndo, btnSearch;
        private Guna2DataGridView dgv;
        private bool isUpdating = false;
        private int currentId = -1;

        public TaoNhanVien_KhauTru(string idNhanVien,string conn)
        {
            InitializeComponent();
            bll = new BLLNhanVien_KhauTru(conn);
            bllPhongBan = new BLLPhongBan(conn);
            bllNhanVien = new BLLNhanVien(conn);

            BuildUI();
            LoadPhongBan();
            LoadLyDo();
            LoadNhanVienList();
            LoadData();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // ======= TIÊU ĐỀ =======
            Label lblTitle = new Label
            {
                Text = "💸 QUẢN LÝ KHẤU TRỪ NHÂN VIÊN",
                Dock = DockStyle.Top,
                Height = 60,
                Font = new Font("Times New Roman", 18, FontStyle.Bold),
                ForeColor = Color.Maroon,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White
            };

            Label MakeLabel(string text) => new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            // ======= FORM CARD =======
            Guna2Panel pnlFormCard = new Guna2Panel
            {
                BorderRadius = 12,
                FillColor = Color.White,
                ShadowDecoration = { Depth = 10, Enabled = true },
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 15, 30, 15)
            };

            // ======= PANEL TÌM THEO PHÒNG BAN =======
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
                MaxDropDownItems = 6 // 👈 chỉ hiển thị tối đa 6 dòng
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
            btnSearch.Click += btnTimKiem_Click;
            pnlPhongBanSearch.Controls.Add(cbPhongBan);
            pnlPhongBanSearch.Controls.Add(btnSearch);

            // ======= PANEL TÌM THEO PHÒNG BAN (ĐỘC LẬP) =======
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

            // ======= FORM CHÍNH 2 CỘT =======
            TableLayoutPanel tlMainForm = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.White
            };
            tlMainForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));  // Bên trái nhỏ hơn chút
            tlMainForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52));  // Bên phải rộng hơn


            // ======= COMBOBOX & TEXTBOX =======
            cbNhanVien = new Guna2ComboBox
            {
                BorderRadius = 8,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 10.5f),
                MaxDropDownItems = 6 // 👈 chỉ hiển thị tối đa 6 dòng
            };
            //cbNhanVien.DropDownHeight = 150; // giới hạn chiều cao dropdown ~150px
            //cbNhanVien.IntegralHeight = false; // phải tắt để có hiệu lực

            cbLyDo = new Guna2ComboBox
            {
                BorderRadius = 8,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 10.5f),
                MaxDropDownItems = 6 // 👈 chỉ hiển thị tối đa 6 dòng
            };
            cbLyDo.SelectedIndexChanged += CbLyDo_SelectedIndexChanged;

            txtAmount = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập số tiền khấu trừ...",
                Font = new Font("Times New Roman", 10.5f),
                Enabled = false
            };

            dtThangApDung = new Guna2DateTimePicker
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MM/yyyy",
                Font = new Font("Times New Roman", 10.5f)
            };

            foreach (var cb in new[] { cbPhongBan, cbNhanVien, cbLyDo })
            {
                cb.DropDownHeight = 150;
                cb.IntegralHeight = false;
            }

            Guna2TextBox txtLyDoMoi = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Nhập lý do mới (nếu có)...",
                Font = new Font("Times New Roman", 10.5f)
            };
            txtLyDoMoi.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtLyDoMoi.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(cbLyDo.Items.Cast<DataRowView>().Select(x => x["loaiKhauTru"].ToString()).ToArray());
            txtLyDoMoi.AutoCompleteCustomSource = autoComplete;

            // ======= BÊN TRÁI =======
            TableLayoutPanel tlLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(10, 5, 20, 5)
            };
            tlLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            tlLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 3; i++)
                tlLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            tlLeft.Controls.Add(MakeLabel("Nhân viên:"), 0, 0);
            tlLeft.Controls.Add(cbNhanVien, 1, 0);
            tlLeft.Controls.Add(MakeLabel("Lý do có sẵn:"), 0, 1);
            tlLeft.Controls.Add(cbLyDo, 1, 1);
            tlLeft.Controls.Add(MakeLabel("Lý do mới:"), 0, 2);
            tlLeft.Controls.Add(txtLyDoMoi, 1, 2);

            // ======= BÊN PHẢI =======
            TableLayoutPanel tlRight = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(20, 5, 10, 5)
            };
            tlRight.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
            tlRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 2; i++)
                tlRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            tlRight.Controls.Add(MakeLabel("Số tiền:"), 0, 0);
            tlRight.Controls.Add(txtAmount, 1, 0);
            tlRight.Controls.Add(MakeLabel("Tháng áp dụng:"), 0, 1);
            tlRight.Controls.Add(dtThangApDung, 1, 1);
            

            // ======= GỘP FORM =======
            tlMainForm.Controls.Add(tlLeft, 0, 0);
            tlMainForm.Controls.Add(tlRight, 1, 0);

            // ======= THÊM VÀO FORM CARD =======
            pnlFormCard.Controls.Add(tlMainForm);
            pnlFormCard.Controls.Add(pnlSearch); // 👈 phần tìm kiếm nằm trên cùng, tách biệt

            // ======= NÚT CHỨC NĂNG =======
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 5, 40, 0),
                Height = 55,
                BackColor = Color.Transparent
            };

            btnSave = new Guna2Button
            {
                Text = "💾 Lưu khấu trừ",
                Width = 160,
                Height = 42,
                BorderRadius = 8,
                FillColor = Color.FromArgb(45, 140, 90),
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnSave.Click += btnSave_Click;

            btnUndo = new Guna2Button
            {
                Text = "↩️ Hoàn tác",
                Width = 140,
                Height = 42,
                BorderRadius = 8,
                FillColor = Color.FromArgb(130, 130, 130),
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnUndo.Click += BtnUndo_Click;

            pnlButtons.Controls.Add(btnSave);
            pnlButtons.Controls.Add(btnUndo);

            // ======= DGV =======
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
                    BackColor = Color.FromArgb(248, 250, 255)
                }
            };
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(45, 85, 155),
                ForeColor = Color.White,
                Font = new Font("Times New Roman", 10.5f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Times New Roman", 10),
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(94, 148, 255),
                SelectionForeColor = Color.Black
            };
            dgv.CellClick += Dgv_CellClick;
            DinhDangCotDgv();

            // ======= BỐ CỤC TỔNG =======
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Tiêu đề
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 230)); // Form gọn hơn
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Nút
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  // Bảng

            mainLayout.Controls.Add(lblTitle, 0, 0);
            mainLayout.Controls.Add(pnlFormCard, 0, 1);
            mainLayout.Controls.Add(pnlButtons, 0, 2);
            mainLayout.Controls.Add(dgv, 0, 3);

            this.Controls.Add(mainLayout);
        }



        private void DinhDangCotDgv()
        {
            if (dgv.Columns.Count == 0) return;

            dgv.Columns["id"].Visible = false;

            dgv.Columns["TenNhanVien"].HeaderText = "Nhân viên";
            dgv.Columns["TenPhongBan"].HeaderText = "Phòng ban";
            dgv.Columns["LyDo"].HeaderText = "Lý do";
            dgv.Columns["SoTien"].HeaderText = "Số tiền";
            dgv.Columns["thangApDung"].HeaderText = "Tháng áp dụng";

            dgv.Columns["SoTien"].DefaultCellStyle.Format = "N0";
            dgv.Columns["thangApDung"].DefaultCellStyle.Format = "MM/yyyy";

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 85, 155);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;

            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void LoadPhongBan()
        {
            var dt = bllPhongBan.ComboboxPhongBan();
            DataRow allRow = dt.NewRow();
            allRow["id"] = DBNull.Value;
            allRow["TenPhongBan"] = "Xem tất cả";
            dt.Rows.InsertAt(allRow, 0);

            cbPhongBan.DataSource = dt;
            cbPhongBan.DisplayMember = "TenPhongBan";
            cbPhongBan.ValueMember = "id";
            cbPhongBan.SelectedIndex = 0;
        }

        private void LoadLyDo()
        {
            var dt = bll.GetAllLyDo();
            cbLyDo.DisplayMember = "loaiKhauTru";
            cbLyDo.ValueMember = "id";
            cbLyDo.DataSource = dt;
            cbLyDo.SelectedIndex = -1;
        }

        private void LoadNhanVienList(int? idPhongBan = null)
        {
            var dt = bllNhanVien.ComboboxNhanVien(idPhongBan);
            DataRow emptyRow = dt.NewRow();
            emptyRow["id"] = "";
            emptyRow["TenNhanVien"] = "-- Chọn nhân viên --";
            dt.Rows.InsertAt(emptyRow, 0);

            cbNhanVien.DataSource = dt;
            cbNhanVien.DisplayMember = "TenNhanVien";
            cbNhanVien.ValueMember = "id";
            cbNhanVien.SelectedIndex = 0;
        }

        private void LoadData(int? idPhongBan = null)
        {
            dgv.DataSource = bll.GetAll(idPhongBan.HasValue ? idPhongBan.ToString() : null);
            if (!dgv.Columns.Contains("Xoa"))
            {
                DataGridViewImageColumn colDelete = new DataGridViewImageColumn
                {
                    Name = "Xoa",
                    HeaderText = "Xóa",
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 50
                };
                try { colDelete.Image = Properties.Resources.delete; } catch { }
                dgv.Columns.Add(colDelete);
            }
        }

        private void CbPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPhongBan.SelectedItem == null) return;

            var selectedItem = cbPhongBan.SelectedItem as DataRowView;
            int? idPhongBan = selectedItem != null && selectedItem["id"] != DBNull.Value ? (int?)Convert.ToInt32(selectedItem["id"]) : null;
            LoadNhanVienList(idPhongBan);
            LoadData(idPhongBan);
        }

        private void CbLyDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLyDo.SelectedIndex == -1 || cbLyDo.SelectedItem == null) return;

            var drv = cbLyDo.SelectedItem as DataRowView;
            if (drv != null)
            {
                decimal tien = 0;
                decimal.TryParse(drv["soTien"]?.ToString(), out tien);
                txtAmount.Text = tien.ToString("0.##");
                txtAmount.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbNhanVien.SelectedValue == null || cbNhanVien.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbLyDo.SelectedIndex == -1 || cbLyDo.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn lý do khấu trừ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idKhauTru;
                var selectedLyDo = cbLyDo.SelectedItem as DataRowView;
                if (selectedLyDo != null && selectedLyDo["id"] != DBNull.Value)
                {
                    idKhauTru = Convert.ToInt32(selectedLyDo["id"]);
                }
                else
                {
                    MessageBox.Show("Lý do khấu trừ không hợp lệ hoặc không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DTONhanVien_KhauTru nkt = new DTONhanVien_KhauTru
                {
                    IdNhanVien = cbNhanVien.SelectedValue.ToString(),
                    IdKhauTru = idKhauTru,
                    ThangApDung = dtThangApDung.Value
                };

                

                if (!isUpdating)
                {
                    if (MessageBox.Show("Xác nhận thêm nhân viên khấu trừ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                    if (bll.Insert(nkt))
                        MessageBox.Show("Thêm khấu trừ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Thêm nhân viên khấu trừ thất bại. Vui lòng kiểm tra lại dữ liệu hoặc đảm bảo lý do khấu trừ tồn tại trong cơ sở dữ liệu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Xác nhận cập nhật nhân viên khấu trừ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                    if (bll.Update(currentId, nkt))
                        MessageBox.Show("Cập nhật nhân viên khấu trừ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Cập nhật nhân viên khấu trừ thất bại. Vui lòng kiểm tra lại dữ liệu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isUpdating = false;
                    btnSave.Text = "💾 Lưu khấu trừ";
                }

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            cbNhanVien.SelectedIndex = 0;
            cbLyDo.SelectedIndex = -1;
            txtAmount.Text = "";
            dtThangApDung.Value = DateTime.Now;
            btnSave.Text = "💾 Lưu khấu trừ";
            isUpdating = false;
            currentId = -1;
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgv.Rows[e.RowIndex].IsNewRow) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
                if (MessageBox.Show("Xóa khấu trừ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bll.Delete(id);
                    LoadData();
                }
                return;
            }

            currentId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
            cbNhanVien.SelectedValue = dgv.Rows[e.RowIndex].Cells["idNhanVien"].Value.ToString();
            cbLyDo.Text = dgv.Rows[e.RowIndex].Cells["LyDo"].Value.ToString();
            txtAmount.Text = dgv.Rows[e.RowIndex].Cells["SoTien"].Value.ToString();
            dtThangApDung.Value = Convert.ToDateTime(dgv.Rows[e.RowIndex].Cells["thangApDung"].Value);

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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = cbPhongBan.SelectedItem as DataRowView;
                int? idPhongBan = selectedItem != null && selectedItem["id"] != DBNull.Value ? (int?)Convert.ToInt32(selectedItem["id"]) : null;

                string tenPhongBan = cbPhongBan.Text?.Trim();

                DataTable dtResult;
                if (!idPhongBan.HasValue)
                {
                    dtResult = bll.GetAll();
                    MessageBox.Show("Đang hiển thị danh sách khấu trừ của tất cả phòng ban.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dtResult = bll.GetAll(idPhongBan.ToString());
                    if (dtResult.Rows.Count == 0)
                    {
                        MessageBox.Show($"Không tìm thấy nhân viên khấu trừ trong phòng ban '{tenPhongBan}'.",
                                        "Kết quả trống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Đã tìm thấy {dtResult.Rows.Count} nhân viên khấu trừ trong phòng ban '{tenPhongBan}'.",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                dgv.DataSource = dtResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tìm kiếm: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}