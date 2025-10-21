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
    public partial class TaoPhuCap : UserControl
    {
        private Guna2DataGridView dgv;
        private Guna2TextBox txtLyDoPhuCap, txtSoTien;
        private Guna2Button btnLuu, btnHoanTac, btnXoa;
        private Label lblTitle;
        private Guna2TextBox txtTimKiem;
        private Guna2Button btnTim;

        private readonly BLLPhuCap bll;
        private int selectedId = -1;

        public TaoPhuCap(string conn)
        {
            InitializeComponent();
            bll = new BLLPhuCap(conn);
            BuildUI();
            LoadDanhSach();
        }

        private void BuildUI()
        {
            // ====== TIÊU ĐỀ ======
            lblTitle = new Label
            {
                Text = "QUẢN LÝ PHỤ CẤP",
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
                PlaceholderText = "Nhập lý do hoặc số tiền..."
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

            Label lblLyDo = new Label { Text = "Lý do:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtLyDoPhuCap = new Guna2TextBox
            {
                Width = 500,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8,
                PlaceholderText = "Nhập lý do phụ cấp..."
            };

            Label lblSoTien = new Label { Text = "Số tiền:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtSoTien = new Guna2TextBox
            {
                Width = 500,
                Font = new Font("Segoe UI", 10),
                BorderRadius = 8,
                PlaceholderText = "Nhập số tiền..."
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
                lblLyDo, txtLyDoPhuCap,
                lblSoTien, txtSoTien,
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
                pnlLeft.Width = (int)(this.Width * 0.30);
            };
        }

        private void LoadDanhSach()
        {
            var dt = bll.GetAll();
            dgv.DataSource = dt;
            AddSttColumn();

            if (dgv.Columns.Contains("id"))
                dgv.Columns["id"].Visible = false;

            dgv.Columns["lyDoPhuCap"].HeaderText = "Lý do";
            dgv.Columns["soTien"].HeaderText = "Số tiền";

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
                string lyDo = txtLyDoPhuCap.Text.Trim();
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

                DTOPhuCap pc = new DTOPhuCap
                {
                    LyDoPhuCap = lyDo,
                    SoTien = soTien
                };

                if (selectedId == -1)
                {
                    if (bll.Insert(pc))
                        MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Thêm mới thất bại. Vui lòng kiểm tra lại dữ liệu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (bll.Update(selectedId, pc))
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại dữ liệu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null) return;

            int id = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value);
            if (MessageBox.Show("Bạn có chắc muốn xóa phụ cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bll.Delete(id);
                LoadDanhSach();
                ClearForm();
            }
        }

        private void ClearForm()
        {
            selectedId = -1;
            txtLyDoPhuCap.Text = "";
            txtSoTien.Text = "";
            btnLuu.Text = "Lưu";
            dgv.ClearSelection();
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
                string lyDo = dgv.Rows[e.RowIndex].Cells["lyDoPhuCap"].Value.ToString();

                if (MessageBox.Show($"Bạn có chắc muốn xóa lý do: '{lyDo}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    bll.Delete(id);
                    LoadDanhSach();
                    ClearForm();
                }
                return;
            }

            selectedId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);
            txtLyDoPhuCap.Text = dgv.Rows[e.RowIndex].Cells["lyDoPhuCap"].Value.ToString();
            txtSoTien.Text = dgv.Rows[e.RowIndex].Cells["soTien"].Value.ToString();

            btnLuu.Text = "Cập nhật";
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
                var filteredRows = dt.AsEnumerable()
                    .Where(row =>
                        (row.Field<string>("lyDoPhuCap") ?? "").ToLower().Contains(keyword) ||
                        row.Field<decimal>("soTien").ToString().ToLower().Contains(keyword)
                    );

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

            if (dgv.Columns.Contains("id"))
                dgv.Columns["id"].Visible = false;

            if (dgv.Columns.Contains("lyDoPhuCap"))
                dgv.Columns["lyDoPhuCap"].HeaderText = "Lý do";
            if (dgv.Columns.Contains("soTien"))
                dgv.Columns["soTien"].HeaderText = "Số tiền";
        }

        private void AddSttColumn()
        {
            if (dgv.Columns.Contains("STT"))
                dgv.Columns.Remove("STT");

            DataGridViewTextBoxColumn sttCol = new DataGridViewTextBoxColumn();
            sttCol.Name = "STT";
            sttCol.HeaderText = "STT";
            sttCol.Width = 60;
            sttCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            sttCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Insert(0, sttCol);

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells["STT"].Value = i + 1;
            }
        }
    }
}