using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class TaoKhauTru : UserControl
    {
        private Guna2DataGridView dgv;
        private Guna2TextBox txtLoaiKhauTru, txtSoTien, txtMoTa, txtTimKiem;
        private Guna2Button btnLuu, btnHoanTac, btnTim;
        private Label lblTitle;

        private readonly BLLKhauTru bll;
        private int selectedId = -1;
        private const string DEFAULT_NGUOI_TAO = "ADMIN"; // Có thể lấy từ login sau

        public TaoKhauTru(string idNhanVien, string conn)
        {
            InitializeComponent();
            bll = new BLLKhauTru(conn);
            BuildUI();
            LoadDanhSach();
        }

        private void BuildUI()
        {
            // ====== TIÊU ĐỀ ======
            lblTitle = new Label
            {
                Text = "QUẢN LÝ KHẤU TRỪ",
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Height = 55,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(155, 45, 85),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ====== PANEL NHẬP LIỆU (TRÁI) ======
            Panel pnlLeft = new Panel
            {
                Dock = DockStyle.Left,
                Padding = new Padding(25),
                BackColor = Color.FromArgb(245, 247, 250)
            };

            // Tìm kiếm
            Label lblTimKiem = new Label { Text = "Tìm kiếm:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtTimKiem = new Guna2TextBox
            {
                Width = 415,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8,
                PlaceholderText = "Nhập loại khấu trừ hoặc số tiền..."
            };
            txtTimKiem.IconLeft = Properties.Resources.search;
            txtTimKiem.IconLeftSize = new Size(18, 18);
            txtTimKiem.KeyDown += TxtTimKiem_KeyDown;

            btnTim = new Guna2Button
            {
                Text = "Tìm",
                FillColor = Color.RoyalBlue,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Width = 60,
                BorderRadius = 8
            };
            btnTim.Click += BtnTim_Click;

            FlowLayoutPanel pnlSearch = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Width = 500,
                Height = 50,
                Margin = new Padding(0, 0, 0, 15)
            };
            pnlSearch.Controls.AddRange(new Control[] { txtTimKiem, btnTim });

            // Nhập liệu
            Label lblLoai = new Label { Text = "Loại khấu trừ:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtLoaiKhauTru = new Guna2TextBox
            {
                Width = 500,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8,
                PlaceholderText = "Nhập loại khấu trừ (bắt buộc)..."
            };

            Label lblSoTien = new Label { Text = "Số tiền:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtSoTien = new Guna2TextBox
            {
                Width = 500,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8,
                PlaceholderText = "Nhập số tiền (VD: 500000)..."
            };

            Label lblMoTa = new Label { Text = "Mô tả:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtMoTa = new Guna2TextBox
            {
                Width = 500,
                Height = 80,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8,
                PlaceholderText = "Nhập mô tả (không bắt buộc)...",
                Multiline = true
            };

            // Nút
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Width = 320,
                Height = 50,
                Margin = new Padding(0, 20, 0, 0)
            };

            btnLuu = new Guna2Button
            {
                Text = "Lưu",
                FillColor = Color.SeaGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Width = 100,
                BorderRadius = 8
            };
            btnLuu.Click += BtnLuu_Click;

            btnHoanTac = new Guna2Button
            {
                Text = "Hoàn tác",
                FillColor = Color.DarkOrange,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Width = 100,
                BorderRadius = 8
            };
            btnHoanTac.Click += BtnHoanTac_Click;

            pnlButtons.Controls.AddRange(new Control[] { btnLuu, btnHoanTac });

            // Input Panel
            FlowLayoutPanel inputPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                Padding = new Padding(0)
            };
            inputPanel.Controls.AddRange(new Control[]
            {
                lblTimKiem, pnlSearch,
                lblLoai, txtLoaiKhauTru,
                lblSoTien, txtSoTien,
                lblMoTa, txtMoTa,
                pnlButtons
            });
            pnlLeft.Controls.Add(inputPanel);

            // ====== DATA GRID VIEW (PHẢI) ======
            dgv = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                ThemeStyle =
                {
                    RowsStyle = { Font = new Font("Segoe UI", 10) },
                    HeaderStyle = { Font = new Font("Segoe UI", 11, FontStyle.Bold) }
                },
                AllowUserToOrderColumns = true,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(245, 245, 250) },
                GridColor = Color.LightGray,
                AllowUserToAddRows = false
            };
            dgv.DataBindingComplete += (s, e) => AddSttColumn();
            dgv.CellClick += Dgv_CellClick;
            dgv.CellMouseEnter += Dgv_CellMouseEnter;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;

            // Thêm controls
            Controls.Add(dgv);
            Controls.Add(pnlLeft);
            Controls.Add(lblTitle);

            // Tự động co giãn pnlLeft
            this.Resize += (s, e) =>
            {
                pnlLeft.Width = (int)(this.Width * 0.35);
            };
        }

        private void LoadDanhSach()
        {
            try
            {
                var list = bll.KtraDsKhauTru(); // ← BLL trả về List<DTOKhauTru>
                dgv.DataSource = list; // Gán trực tiếp
                AddSttColumn();

                // Ẩn cột không cần thiết
                if (dgv.Columns.Contains("id")) dgv.Columns["id"].Visible = false;
                if (dgv.Columns.Contains("idNguoiTao")) dgv.Columns["idNguoiTao"].Visible = false;
                if (dgv.Columns.Contains("NhanVien")) dgv.Columns["NhanVien"].Visible = false;

                // Đặt tiêu đề
                dgv.Columns["loaiKhauTru"].HeaderText = "Loại khấu trừ";
                dgv.Columns["soTien"].HeaderText = "Số tiền";
                dgv.Columns["moTa"].HeaderText = "Mô tả";

                // Thêm cột Xóa
                if (!dgv.Columns.Contains("btnDelete"))
                {
                    var deleteCol = new DataGridViewImageColumn
                    {
                        Name = "btnDelete",
                        HeaderText = "Xóa",
                        Image = Properties.Resources.delete,
                        ImageLayout = DataGridViewImageCellLayout.Zoom,
                        Width = 50
                    };
                    deleteCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    deleteCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns.Add(deleteCol);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string loai = txtLoaiKhauTru.Text.Trim();
                if (string.IsNullOrWhiteSpace(loai))
                {
                    MessageBox.Show("Vui lòng nhập loại khấu trừ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtSoTien.Text, out decimal soTien) || soTien < 0)
                {
                    MessageBox.Show("Số tiền không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new DTOKhauTru
                {
                    ID = selectedId,
                    LoaiKhauTru = loai,
                    SoTien = soTien,
                    MoTa = txtMoTa.Text.Trim(),
                    IDNguoiTao = DEFAULT_NGUOI_TAO
                };

                bool success;
                if (selectedId == -1)
                {
                    dto.ID = 0; // ID = 0 để thêm mới
                    success = bll.KtraThemKhauTru(dto);
                    MessageBox.Show(success ? "Thêm mới thành công!" : "Thêm thất bại!", "Thông báo",
                        MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
                else
                {
                    success = bll.KtraCapNhatKhauTru(dto);
                    MessageBox.Show(success ? "Cập nhật thành công!" : "Cập nhật thất bại!", "Thông báo",
                        MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }

                if (success)
                {
                    LoadDanhSach();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHoanTac_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
                string loai = dgv.Rows[e.RowIndex].Cells["loaiKhauTru"].Value.ToString();

                if (MessageBox.Show($"Xóa khấu trừ: '{loai}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var dto = new DTOKhauTru(id);
                        if (bll.KtraXoaKhauTru(dto))
                        {
                            LoadDanhSach();
                            ClearForm();
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa: " + ex.Message);
                    }
                }
                return;
            }

            // Chọn để sửa
            selectedId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
            txtLoaiKhauTru.Text = dgv.Rows[e.RowIndex].Cells["loaiKhauTru"].Value.ToString();
            txtSoTien.Text = dgv.Rows[e.RowIndex].Cells["soTien"].Value.ToString();
            txtMoTa.Text = dgv.Rows[e.RowIndex].Cells["moTa"].Value?.ToString() ?? "";
            btnLuu.Text = "Cập nhật";
        }

        private void Dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                dgv.Cursor = Cursors.Hand;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.trash;
            }
        }

        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                dgv.Cursor = Cursors.Default;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.delete;
            }
        }

        private void BtnTim_Click(object sender, EventArgs e) => TimKiem();
        private void TxtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKiem();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TimKiem()
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            try
            {
                var list = bll.KtraDsKhauTru();
                if (!string.IsNullOrEmpty(keyword))
                {
                    list = list.Where(x =>
                        x.loaiKhauTru.ToLower().Contains(keyword) ||
                        x.soTien.ToString().Contains(keyword)
                    ).ToList();
                }

                dgv.DataSource = list;
                AddSttColumn();

                if (dgv.Columns.Contains("id")) dgv.Columns["id"].Visible = false;
                if (dgv.Columns.Contains("idNguoiTao")) dgv.Columns["idNguoiTao"].Visible = false;
                dgv.Columns["loaiKhauTru"].HeaderText = "Loại khấu trừ";
                dgv.Columns["soTien"].HeaderText = "Số tiền";
                dgv.Columns["moTa"].HeaderText = "Mô tả";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void AddSttColumn()
        {
            if (dgv.Columns.Contains("STT")) dgv.Columns.Remove("STT");

            var sttCol = new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 60,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            };
            dgv.Columns.Insert(0, sttCol);

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells["STT"].Value = i + 1;
            }
        }

        private void ClearForm()
        {
            selectedId = -1;
            txtLoaiKhauTru.Clear();
            txtSoTien.Clear();
            txtMoTa.Clear();
            btnLuu.Text = "Lưu";
            dgv.ClearSelection();
        }
    }
}