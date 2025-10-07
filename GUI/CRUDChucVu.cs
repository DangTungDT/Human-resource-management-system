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
    public partial class CRUDChucVu : UserControl
    {
        private Guna2TextBox txtTenChucVu, txtMoTa, txtLuong, txtTyLe;
        private Guna2ComboBox cbPhongBan;
        private Guna2Button btnSave, btnUndo;
        private Guna2DataGridView dgv;

        private string connectionString = @"Data Source=DESKTOP-UM1I61K\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;";
        private int? selectedId = null;
        public CRUDChucVu()
        {
            InitializeComponent();
            BuildUI();
            LoadPhongBan();
            LoadChucVu();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            Label lblTitle = new Label()
            {
                Text = "QUẢN LÝ CHỨC VỤ",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            txtTenChucVu = new Guna2TextBox() { PlaceholderText = "Tên chức vụ", Dock = DockStyle.Fill };
            txtLuong = new Guna2TextBox() { PlaceholderText = "Lương cơ bản", Dock = DockStyle.Fill };
            txtTyLe = new Guna2TextBox() { PlaceholderText = "Tỷ lệ hoa hồng (%)", Dock = DockStyle.Fill };
            txtMoTa = new Guna2TextBox() { PlaceholderText = "Mô tả", Dock = DockStyle.Fill, Multiline = true, Height = 60 };
            cbPhongBan = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

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

            // ==== FORM INPUT ====
            TableLayoutPanel form = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(0, 0, 0, 0),
                ColumnCount = 3,
                RowCount = 6,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 07));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23));

            form.Controls.Add(new Label() { Text = "Tên chức vụ:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
            form.Controls.Add(txtTenChucVu, 1, 0);

            form.Controls.Add(new Label() { Text = "Phòng ban:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            form.Controls.Add(cbPhongBan, 1, 1);

            form.Controls.Add(new Label() { Text = "Lương cơ bản:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 2);
            form.Controls.Add(txtLuong, 1, 2);

            form.Controls.Add(new Label() { Text = "Tỷ lệ hoa hồng:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 3);
            form.Controls.Add(txtTyLe, 1, 3);

            form.Controls.Add(new Label() { Text = "Mô tả:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 4);
            form.Controls.Add(txtMoTa, 1, 4);

            // ==== HÀNG NÚT (THÊM / HOÀN TÁC) ====
            FlowLayoutPanel btnPanel = new FlowLayoutPanel()
            {
                AutoSize = true,
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 10, 0, 0),
                WrapContents = false
            };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            form.Controls.Add(btnPanel, 1, 5);

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

            // ==== LAYOUT CHÍNH ====
            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 350));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.Controls.Add(form, 0, 0);
            main.Controls.Add(dgv, 0, 1);

            this.Controls.Add(main);
            this.Controls.Add(lblTitle);
        }

        // ======================= LOAD PHÒNG BAN =======================
        private void LoadPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, TenPhongBan FROM PhongBan";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbPhongBan.DataSource = dt;
                cbPhongBan.DisplayMember = "TenPhongBan";
                cbPhongBan.ValueMember = "id";
            }
        }

        // ======================= LOAD CHỨC VỤ =======================
        private void LoadChucVu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT cv.id AS [Mã chức vụ], cv.TenChucVu AS [Tên chức vụ], cv.luongCoBan AS [Lương cơ bản],
                                 cv.tyLeHoaHong AS [Tỷ lệ hoa hồng], pb.TenPhongBan AS [Phòng ban], cv.moTa AS [Mô tả]
                                 FROM ChucVu cv
                                 JOIN PhongBan pb ON cv.idPhongBan = pb.id";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }

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

        // ======================= THÊM / CẬP NHẬT =======================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenChucVu.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chức vụ!", "Thông báo");
                return;
            }
            if (cbPhongBan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban!", "Thông báo");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;

                if (btnSave.Text.Contains("Thêm"))
                {
                    cmd = new SqlCommand(@"INSERT INTO ChucVu (TenChucVu, luongCoBan, tyLeHoaHong, moTa, idPhongBan)
                                           VALUES (@Ten, @Luong, @TyLe, @MoTa, @idPB)", conn);
                }
                else
                {
                    if (selectedId == null)
                    {
                        MessageBox.Show("Không xác định được bản ghi cần cập nhật!", "Lỗi");
                        return;
                    }

                    cmd = new SqlCommand(@"UPDATE ChucVu SET TenChucVu=@Ten, luongCoBan=@Luong,
                                           tyLeHoaHong=@TyLe, moTa=@MoTa, idPhongBan=@idPB WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", selectedId);
                }

                cmd.Parameters.AddWithValue("@Ten", txtTenChucVu.Text);
                cmd.Parameters.AddWithValue("@Luong", decimal.TryParse(txtLuong.Text, out var luong) ? luong : 0);
                cmd.Parameters.AddWithValue("@TyLe", decimal.TryParse(txtTyLe.Text, out var tyle) ? tyle : 0);
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                cmd.Parameters.AddWithValue("@idPB", cbPhongBan.SelectedValue);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show(btnSave.Text.Contains("Thêm") ? "✅ Đã thêm chức vụ mới!" : "✏️ Đã cập nhật chức vụ!");
            LoadChucVu();
            ClearForm();
        }

        // ======================= XÓA =======================
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã chức vụ"].Value);
                if (MessageBox.Show("Bạn có chắc muốn xóa chức vụ này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM ChucVu WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadChucVu();
                }
                return;
            }

            // Click chọn để sửa
            selectedId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Mã chức vụ"].Value);
            txtTenChucVu.Text = dgv.Rows[e.RowIndex].Cells["Tên chức vụ"].Value.ToString();
            txtLuong.Text = dgv.Rows[e.RowIndex].Cells["Lương cơ bản"].Value.ToString();
            txtTyLe.Text = dgv.Rows[e.RowIndex].Cells["Tỷ lệ hoa hồng"].Value.ToString();
            txtMoTa.Text = dgv.Rows[e.RowIndex].Cells["Mô tả"].Value.ToString();
            cbPhongBan.Text = dgv.Rows[e.RowIndex].Cells["Phòng ban"].Value.ToString();

            btnSave.Text = "✏️ Cập nhật";
            btnSave.FillColor = Color.Orange;
        }

        // ======================= HOVER ICON =======================
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
            txtTenChucVu.Clear();
            txtLuong.Clear();
            txtTyLe.Clear();
            txtMoTa.Clear();
            cbPhongBan.SelectedIndex = -1;

            btnSave.Text = "➕ Thêm mới";
            btnSave.FillColor = Color.MediumSeaGreen;
            dgv.ClearSelection();
        }
    }
}
