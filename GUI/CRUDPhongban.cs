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

namespace GUI
{
    public partial class CRUDPhongban : UserControl
    {
        private Guna2TextBox txtTenPhongBan, txtMoTa;
        private Guna2Button btnSave, btnUndo;
        private Guna2DataGridView dgv;
        private string connectionString = @"Data Source=DESKTOP-UM1I61K\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;";
        private int? selectedId = null; // lưu id đang chọn

        public CRUDPhongban()
        {
            InitializeComponent();
            BuildUI();
            LoadPhongBan();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            Label lblTitle = new Label()
            {
                Text = "QUẢN LÝ PHÒNG BAN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            txtTenPhongBan = new Guna2TextBox() { PlaceholderText = "Tên phòng ban", Dock = DockStyle.Fill };
            txtMoTa = new Guna2TextBox() { PlaceholderText = "Mô tả", Dock = DockStyle.Fill, Multiline = true, Height = 60 };

            btnSave = new Guna2Button()
            {
                Text = "➕ Thêm mới",
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Width = 140,
                Height = 40,
                Cursor = Cursors.Hand
            };
            btnSave.Click += BtnSave_Click;

            btnUndo = new Guna2Button()
            {
                Text = "↩️ Hoàn tác",
                BorderRadius = 8,
                FillColor = Color.Gray,
                ForeColor = Color.White,
                Width = 120,
                Height = 40,
                Cursor = Cursors.Hand
            };
            btnUndo.Click += BtnUndo_Click;

            // ==== Form input layout ====
            TableLayoutPanel form = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(0, 0, 0, 0),
                ColumnCount = 3,
                RowCount = 3,
                Height = 160
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 07));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23));

            form.Controls.Add(new Label() { Text = "Tên phòng ban:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
            form.Controls.Add(txtTenPhongBan, 1, 0);
            form.Controls.Add(new Label() { Text = "Mô tả:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            form.Controls.Add(txtMoTa, 1, 1);

            FlowLayoutPanel btnPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            form.Controls.Add(btnPanel, 1, 2);

            // ==== DGV ====
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowTemplate = { Height = 35 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgv.CellClick += Dgv_CellClick;
            dgv.CellMouseEnter += Dgv_CellMouseEnter;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;

            // Layout tổng
            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.Controls.Add(form, 0, 0);
            main.Controls.Add(dgv, 0, 1);

            this.Controls.Add(main);
            this.Controls.Add(lblTitle);
        }

        // ======================= LOAD DỮ LIỆU =======================
        private void LoadPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id AS [Mã phòng ban], TenPhongBan AS [Tên phòng ban], MoTa AS [Mô tả] FROM PhongBan";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }

            // Thêm icon XÓA nếu chưa có
            if (!dgv.Columns.Contains("Xoa"))
            {
                DataGridViewImageColumn colDelete = new DataGridViewImageColumn()
                {
                    Name = "Xoa",
                    HeaderText = "Xóa",
                    Image = Properties.Resources.delete, // bạn nhớ thêm icon delete.png vào Resources
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 50
                };
                dgv.Columns.Add(colDelete);
                dgv.Columns["Xoa"].DisplayIndex = dgv.Columns.Count - 1;
            }
        }

        // ======================= LƯU / CẬP NHẬT =======================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhongBan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên phòng ban!", "Thông báo");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;

                if (btnSave.Text.Contains("Thêm"))
                {
                    cmd = new SqlCommand("INSERT INTO PhongBan (TenPhongBan, MoTa) VALUES (@Ten, @MoTa)", conn);
                }
                else
                {
                    if (selectedId == null)
                    {
                        MessageBox.Show("Không xác định được bản ghi cần cập nhật!", "Lỗi");
                        return;
                    }
                    cmd = new SqlCommand("UPDATE PhongBan SET TenPhongBan=@Ten, MoTa=@MoTa WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", selectedId);
                }

                cmd.Parameters.AddWithValue("@Ten", txtTenPhongBan.Text);
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(btnSave.Text.Contains("Thêm") ? "✅ Đã thêm phòng ban mới!" : "✏️ Đã cập nhật phòng ban!");
            LoadPhongBan();
            ClearForm();
        }

        // ======================= XÓA DỮ LIỆU =======================
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã phòng ban"].Value);
                if (MessageBox.Show("Bạn có chắc muốn xóa phòng ban này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM PhongBan WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadPhongBan();
                }
                return;
            }

            // Click để chỉnh sửa
            selectedId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã phòng ban"].Value);
            txtTenPhongBan.Text = dgv.Rows[e.RowIndex].Cells["Tên phòng ban"].Value.ToString();
            txtMoTa.Text = dgv.Rows[e.RowIndex].Cells["Mô tả"].Value.ToString();

            btnSave.Text = "✏️ Cập nhật";
            btnSave.FillColor = Color.Orange;
        }

        // ======================= HIỆU ỨNG HOVER ICON =======================
        private void Dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Hand;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.trash;
            }
        }

        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                dgv.Cursor = Cursors.Default;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Properties.Resources.delete;
            }
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
            txtTenPhongBan.Clear();
            txtMoTa.Clear();
            btnSave.Text = "➕ Thêm mới";
            btnSave.FillColor = Color.MediumSeaGreen;
            dgv.ClearSelection();
        }
    }

}
