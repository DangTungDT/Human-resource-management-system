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

        public TaoNhanVien_KhauTru(string conn)
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

            Label lblTitle = new Label
            {
                Text = "💸 QUẢN LÝ KHAU TRỪ NHÂN VIÊN",
                Dock = DockStyle.Top,
                Height = 65,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 70, 140),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White
            };

            Guna2Panel pnlFormCard = new Guna2Panel
            {
                BorderRadius = 12,
                FillColor = Color.White,
                ShadowDecoration = { Depth = 10, Enabled = true },
                Dock = DockStyle.Fill,
                Padding = new Padding(50, 30, 50, 30)
            };

            TableLayoutPanel tlForm = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                BackColor = Color.White,
                Padding = new Padding(0, 10, 0, 0)
            };
            tlForm.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
            tlForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 5; i++)
                tlForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            Label MakeLabel(string text) => new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 10.5f),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(60, 60, 60)
            };

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
            cbPhongBan.SelectedIndexChanged += CbPhongBan_SelectedIndexChanged;

            btnSearch = new Guna2Button
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
                btnSearch.Image = Properties.Resources.search;
                btnSearch.FillColor = Color.MediumSlateBlue;
            };

            pnlPhongBanSearch.Controls.Add(cbPhongBan);
            pnlPhongBanSearch.Controls.Add(btnSearch);

            cbNhanVien = new Guna2ComboBox
            {
                BorderRadius = 8,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };

            cbLyDo = new Guna2ComboBox
            {
                BorderRadius = 8,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            cbLyDo.SelectedIndexChanged += CbLyDo_SelectedIndexChanged;

            txtAmount = new Guna2TextBox
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                PlaceholderText = "Số tiền khấu trừ...",
                Font = new Font("Segoe UI", 10),
                Enabled = false
            };

            dtThangApDung = new Guna2DateTimePicker
            {
                BorderRadius = 8,
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MM/yyyy",
                Font = new Font("Segoe UI", 10)
            };

            tlForm.Controls.Add(MakeLabel("Tìm theo phòng ban:"), 0, 0);
            tlForm.Controls.Add(pnlPhongBanSearch, 1, 0);
            tlForm.Controls.Add(MakeLabel("Nhân viên:"), 0, 1);
            tlForm.Controls.Add(cbNhanVien, 1, 1);
            tlForm.Controls.Add(MakeLabel("Lý do khấu trừ:"), 0, 2);
            tlForm.Controls.Add(cbLyDo, 1, 2);
            tlForm.Controls.Add(MakeLabel("Số tiền:"), 0, 3);
            tlForm.Controls.Add(txtAmount, 1, 3);
            tlForm.Controls.Add(MakeLabel("Tháng áp dụng:"), 0, 4);
            tlForm.Controls.Add(dtThangApDung, 1, 4);

            pnlFormCard.Controls.Add(tlForm);

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
                Text = "💾 Lưu khấu trừ",
                Width = 180,
                Height = 45,
                BorderRadius = 8,
                FillColor = Color.FromArgb(45, 140, 90),
                Font = new Font("Segoe UI Semibold", 10.5f),
                ForeColor = Color.White
            };
            btnSave.Click += btnSave_Click;

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
            DinhDangCotDgv();

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 330));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 350));

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
                    if (bll.Insert(nkt))
                        MessageBox.Show("Thêm khấu trừ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Thêm khấu trừ thất bại. Vui lòng kiểm tra lại dữ liệu hoặc đảm bảo lý do khấu trừ tồn tại trong cơ sở dữ liệu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (bll.Update(currentId, nkt))
                        MessageBox.Show("Cập nhật khấu trừ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Cập nhật khấu trừ thất bại. Vui lòng kiểm tra lại dữ liệu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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