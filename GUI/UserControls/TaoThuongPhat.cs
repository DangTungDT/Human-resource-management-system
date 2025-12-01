using BLL;
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
    public partial class TaoThuongPhat : UserControl
    {
        private Guna2DataGridView dgv;
        private Guna2ComboBox cbLoai;
        private Guna2TextBox txtLyDo, txtSoTien;
        private Guna2DateTimePicker dtNgayApDung;
        private Guna2Button btnLuu, btnHoanTac, btnXoa;
        private Label lblTitle;
        private Guna2TextBox txtTimKiem;
        private Guna2Button btnTim;

        private readonly BLLThuongPhat bll;
        private int selectedId = -1;

        public TaoThuongPhat(string idNhanVien, string conn)
        {
            InitializeComponent();
            bll = new BLLThuongPhat(conn);
            BuildUI();
            LoadDanhSach();
        }

        private void BuildUI()
        {
            // ====== TIÊU ĐỀ ======
            lblTitle = new Label
            {
                Text = "QUẢN LÝ THƯỞNG PHẠT",
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Height = 55,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 85, 155),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ====== PANEL NHẬP LIỆU ======
            Panel pnlLeft = new Panel
            {
                Dock = DockStyle.Left,
                Padding = new Padding(25),
                BackColor = Color.FromArgb(245, 247, 250)
            };
            Label lblTimKiem = new Label { Text = "Tìm kiếm:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtTimKiem = new Guna2TextBox
            {
                Width = 415,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8,
                PlaceholderText = "Nhập lý do hoặc loại..."
            };
            txtTimKiem.IconLeft = Properties.Resources.search; // thêm icon search.png vào Resources
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

            Label lblLoai = new Label { Text = "Loại:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            cbLoai = new Guna2ComboBox
            {
                Width = 500,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8
            };
            cbLoai.Items.AddRange(new string[] { "Thưởng", "Phạt" });
            cbLoai.SelectedIndexChanged += cbLoai_SelectedIndexChanged;

            Label lblLyDo = new Label { Text = "Lý do:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtLyDo = new Guna2TextBox
            {
                Width = 500,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8,
                PlaceholderText = "Nhập lý do..."
            };

            Label lblSoTien = new Label { Text = "Số tiền:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtSoTien = new Guna2TextBox
            {
                Width = 500,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8,
                PlaceholderText = "Nhập số tiền..."
            };

            Label lblNgay = new Label { Text = "Ngày áp dụng:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            dtNgayApDung = new Guna2DateTimePicker
            {
                Width = 500,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8
            };

            // ====== NÚT ======
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Width = 320,
                Height = 50,
                Margin = new Padding(0, 15, 0, 0)
            };

            btnLuu = new Guna2Button
            {
                Text = "Lưu",
                FillColor = Color.SeaGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Width = 90,
                BorderRadius = 8
            };
            btnLuu.Click += BtnLuu_Click;

            btnHoanTac = new Guna2Button
            {
                Text = "Hoàn tác",
                FillColor = Color.DarkOrange,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Width = 90,
                BorderRadius = 8
            };
            btnHoanTac.Click += BtnHoanTac_Click;

            //btnXoa = new Guna2Button
            //{
            //    Text = "Xóa",
            //    FillColor = Color.IndianRed,
            //    Font = new Font("Segoe UI", 10, FontStyle.Bold),
            //    Width = 90,
            //    BorderRadius = 8
            //};
            //btnXoa.Click += BtnXoa_Click;

            pnlButtons.Controls.AddRange(new Control[] { btnLuu, btnHoanTac, btnXoa });

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
                lblLoai, cbLoai,
                lblLyDo, txtLyDo,
                lblSoTien, txtSoTien,
                lblNgay, dtNgayApDung,
                pnlButtons
            });
            pnlLeft.Controls.Add(inputPanel);

            // ====== DGV ======
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

            Controls.Add(dgv);
            Controls.Add(pnlLeft);
            Controls.Add(lblTitle);

            // Cập nhật lại kích thước pnlLeft theo tỷ lệ form
            this.Resize += (s, e) =>
            {
                pnlLeft.Width = (int)(this.Width * 0.30); // 38% chiều rộng form
            };
        }

        private void LoadDanhSach()
        {
            var dt = bll.GetAll();
            dgv.DataSource = dt;
            AddSttColumn();

            if (dgv.Columns.Contains("id"))
                dgv.Columns["id"].Visible = false;

            dgv.Columns["lyDo"].HeaderText = "Lý do";
            dgv.Columns["tienThuongPhat"].HeaderText = "Số tiền";
            dgv.Columns["loai"].HeaderText = "Loại";
            dgv.Columns["idNguoiTao"].HeaderText = "Người tạo";

            // Nếu cột Xóa chưa tồn tại thì thêm
            if (!dgv.Columns.Contains("btnDelete"))
            {
                DataGridViewImageColumn deleteCol = new DataGridViewImageColumn();
                deleteCol.Name = "btnDelete";
                deleteCol.HeaderText = "Xóa";
                deleteCol.Image = Properties.Resources.delete;
                deleteCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                deleteCol.Width = 50;
                deleteCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                deleteCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns.Add(deleteCol);
            }

        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string loai = cbLoai.SelectedItem.ToString();
                string lyDo = txtLyDo.Text.Trim();
                decimal soTien = 0;

                if (string.IsNullOrWhiteSpace(lyDo))
                {
                    MessageBox.Show("Vui lòng nhập lý do.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtSoTien.Text, out soTien))
                {
                    MessageBox.Show("Số tiền không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (selectedId == -1)
                {
                    // Thêm mới
                    bll.Insert(loai, lyDo, soTien, "GD00000001");
                    MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Cập nhật
                    bll.Update(selectedId, loai, lyDo, soTien);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadDanhSach();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void BtnHoanTac_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            selectedId = -1;
            txtLyDo.Text = "";
            txtSoTien.Text = "";
            dtNgayApDung.Value = DateTime.Now;
            btnLuu.Text = "Lưu";
            dgv.ClearSelection();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null) return;

            int id = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value);
            if (MessageBox.Show("Bạn có chắc muốn xóa lý do này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bll.Delete(id);
                LoadDanhSach();
                ClearForm();
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Nếu click vào cột Xóa
            if (dgv.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
                string lyDo = dgv.Rows[e.RowIndex].Cells["lyDo"].Value.ToString();

                if (MessageBox.Show($"Bạn có chắc muốn xóa lý do: '{lyDo}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    bll.Delete(id);
                    LoadDanhSach();
                    ClearForm();
                }
                return;
            }

            // Khi click vào dòng bình thường → chọn để chỉnh sửa
            selectedId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
            cbLoai.Text = dgv.Rows[e.RowIndex].Cells["loai"].Value.ToString();
            txtLyDo.Text = dgv.Rows[e.RowIndex].Cells["lyDo"].Value.ToString();
            txtSoTien.Text = dgv.Rows[e.RowIndex].Cells["tienThuongPhat"].Value.ToString();

            btnLuu.Text = "Cập nhật";
        }

        private void cbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void Dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgv.Columns["btnDelete"].Index)
            {
                dgv.Cursor = Cursors.Hand;
                dgv.Rows[e.RowIndex].Cells["btnDelete"].Value = Properties.Resources.trash;
            }
        }

        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgv.Columns["btnDelete"].Index)
            {
                dgv.Cursor = Cursors.Default;
                dgv.Rows[e.RowIndex].Cells["btnDelete"].Value = Properties.Resources.delete;
            }
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

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
            var dt = bll.GetAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                // Dùng LINQ để lọc theo Lý do, Loại, hoặc Số tiền
                var filteredRows = dt.AsEnumerable()
                    .Where(row =>
                        (row.Field<string>("lyDo") ?? "").ToLower().Contains(keyword) ||
                        (row.Field<string>("loai") ?? "").ToLower().Contains(keyword) ||
                        row.Field<decimal>("tienThuongPhat").ToString().ToLower().Contains(keyword)
                    );

                // Nếu có kết quả thì gán vào DataGridView
                if (filteredRows.Any())
                    dgv.DataSource = filteredRows.CopyToDataTable();
                else
                    dgv.DataSource = null;
            }
            else
            {
                dgv.DataSource = dt;
                AddSttColumn();
            }

            // Cấu hình lại tiêu đề cột
            if (dgv.Columns.Contains("id"))
                dgv.Columns["id"].Visible = false;

            if (dgv.Columns.Contains("lyDo"))
                dgv.Columns["lyDo"].HeaderText = "Lý do";
            if (dgv.Columns.Contains("loai"))
                dgv.Columns["loai"].HeaderText = "Loại";
            if (dgv.Columns.Contains("tienThuongPhat"))
                dgv.Columns["tienThuongPhat"].HeaderText = "Số tiền";
            if (dgv.Columns.Contains("idNguoiTao"))
                dgv.Columns["idNguoiTao"].HeaderText = "Người tạo";

        }

        private void AddSttColumn()
        {
            // Nếu đã có cột STT thì xóa để thêm lại
            if (dgv.Columns.Contains("STT"))
                dgv.Columns.Remove("STT");

            // Thêm cột STT vào đầu tiên
            DataGridViewTextBoxColumn sttCol = new DataGridViewTextBoxColumn();
            sttCol.Name = "STT";
            sttCol.HeaderText = "STT";
            sttCol.Width = 60;
            sttCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            sttCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Insert(0, sttCol);

            // Gán số thứ tự
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells["STT"].Value = i + 1;
            }
        }
    }
}
