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
    public partial class TaoKhenThuong : UserControl
    {
        private Guna2ComboBox cbEmployee, cbPhongBan;
        private Guna2DateTimePicker dtReward;
        private Guna2TextBox txtReason, txtAmount;
        private Guna2Button btnSave, btnUndo, btnSearch;
        private Guna2DataGridView dgv;

        private string connectionString = ConnectionDB.conn;
        private string idNguoiTao = "GD00000001";
        private int? selectedId = null;

        public TaoKhenThuong()
        {
            InitializeComponent();
            BuildUI();
            LoadPhongBan();
            LoadNhanVien();
            LoadKhenThuong();
        }

        // ======================= DỰNG GIAO DIỆN =======================
        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            Label lblTitle = new Label()
            {
                Text = "KHEN THƯỞNG NHÂN VIÊN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ==== THANH TÌM KIẾM ====
            Label lblSearch = new Label()
            {
                Text = "📋 Tìm kiếm theo phòng ban:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Margin = new Padding(10, 10, 0, 0)
            };

            cbPhongBan = new Guna2ComboBox()
            {
                Width = 250,
                BorderRadius = 6,
                Margin = new Padding(10, 5, 10, 5),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            btnSearch = new Guna2Button()
            {
                Text = "🔍 Tìm kiếm",
                BorderRadius = 8,
                FillColor = Color.SteelBlue,
                ForeColor = Color.White,
                Height = 36,
                Width = 120,
                Margin = new Padding(10, 5, 0, 5)
            };
            btnSearch.Click += BtnSearch_Click;

            FlowLayoutPanel searchPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(20, 5, 0, 10)
            };
            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(cbPhongBan);
            searchPanel.Controls.Add(btnSearch);

            // ==== INPUT FORM ====
            cbEmployee = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            dtReward = new Guna2DateTimePicker() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Dock = DockStyle.Fill };
            txtAmount = new Guna2TextBox() { PlaceholderText = "Số tiền thưởng (VD: 500000)", Dock = DockStyle.Fill };
            txtReason = new Guna2TextBox() { PlaceholderText = "Lý do khen thưởng", Dock = DockStyle.Fill, Multiline = true, Height = 60 };

            btnSave = new Guna2Button()
            {
                Text = "💾 Lưu khen thưởng",
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Width = 150,
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
                Width = 150,
                Height = 40,
                Cursor = Cursors.Hand
            };
            btnUndo.Click += BtnUndo_Click;

            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 35 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            dgv.CellClick += Dgv_CellClick;
            dgv.CellMouseEnter += Dgv_CellMouseEnter;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;

            // ==== FORM INPUT ====
            TableLayoutPanel form = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(10, 10, 0, 90),
                AutoScroll = true,
                ColumnCount = 3,
                RowCount = 5,
                Height = 250
            };
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23));

            form.Controls.Add(new Label() { Text = "Nhân viên:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 0);
            form.Controls.Add(cbEmployee, 1, 0);
            form.Controls.Add(new Label() { Text = "Ngày thưởng:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            form.Controls.Add(dtReward, 1, 1);
            form.Controls.Add(new Label() { Text = "Số tiền thưởng:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 2);
            form.Controls.Add(txtAmount, 1, 2);
            form.Controls.Add(new Label() { Text = "Lý do:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 3);
            form.Controls.Add(txtReason, 1, 3);

            FlowLayoutPanel btnPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            form.Controls.Add(btnPanel, 1, 4);

            // ==== MAIN ====
            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); // Thanh tìm kiếm
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            main.Controls.Add(lblTitle, 0, 0);
            main.Controls.Add(searchPanel, 0, 1);

            TableLayoutPanel content = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 300));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            content.Controls.Add(form, 0, 0);
            content.Controls.Add(dgv, 0, 1);

            main.Controls.Add(content, 0, 2);
            this.Controls.Add(main);
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

                DataRow allRow = dt.NewRow();
                allRow["id"] = DBNull.Value;
                allRow["TenPhongBan"] = "Tất cả phòng ban";
                dt.Rows.InsertAt(allRow, 0);

                cbPhongBan.DataSource = dt;
                cbPhongBan.DisplayMember = "TenPhongBan";
                cbPhongBan.ValueMember = "id";
                cbPhongBan.SelectedIndex = 0;
            }
        }

        // ======================= TÌM KIẾM =======================
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (cbPhongBan.SelectedValue == null || cbPhongBan.SelectedIndex == 0)
                LoadKhenThuong();
            else
                LoadKhenThuong(cbPhongBan.SelectedValue.ToString());
        }

        // ======================= LOAD KHEN THƯỞNG =======================
        private void LoadKhenThuong(string idPhongBan = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT tp.id, nv.TenNhanVien, pb.TenPhongBan, tp.tienThuongPhat AS [Số tiền thưởng], 
                           tp.lyDo AS [Lý do], nvtp.thangApDung AS [Ngày áp dụng]
                    FROM ThuongPhat tp
                    JOIN NhanVien_ThuongPhat nvtp ON tp.id = nvtp.idThuongPhat
                    JOIN NhanVien nv ON nvtp.idNhanVien = nv.id
                    JOIN PhongBan pb ON nv.idPhongBan = pb.id
                    WHERE tp.loai = N'Thưởng'";

                if (!string.IsNullOrEmpty(idPhongBan))
                    query += " AND pb.id = @idPB";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(idPhongBan))
                    cmd.Parameters.AddWithValue("@idPB", idPhongBan);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
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

        // ======================= LOAD NHÂN VIÊN =======================
        private void LoadNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, TenNhanVien FROM NhanVien WHERE DaXoa = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                cbEmployee.DataSource = dt;
                cbEmployee.DisplayMember = "TenNhanVien";
                cbEmployee.ValueMember = "id";
            }
        }

        // ======================= LƯU / CẬP NHẬT =======================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cbEmployee.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Cảnh báo");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal soTien))
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ!", "Lỗi");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;
                int idThuongPhat;

                if (btnSave.Text.Contains("Lưu"))
                {
                    cmd = new SqlCommand(@"INSERT INTO ThuongPhat (tienThuongPhat, loai, lyDo, idNguoiTao)
                                           OUTPUT INSERTED.id
                                           VALUES (@tien, N'Thưởng', @lydo, @idng)", conn);
                    cmd.Parameters.AddWithValue("@tien", soTien);
                    cmd.Parameters.AddWithValue("@lydo", txtReason.Text);
                    cmd.Parameters.AddWithValue("@idng", idNguoiTao);
                    idThuongPhat = (int)cmd.ExecuteScalar();

                    SqlCommand cmd2 = new SqlCommand(@"INSERT INTO NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung)
                                                       VALUES (@idnv, @idtp, @ngay)", conn);
                    cmd2.Parameters.AddWithValue("@idnv", cbEmployee.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@idtp", idThuongPhat);
                    cmd2.Parameters.AddWithValue("@ngay", dtReward.Value);
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    if (selectedId == null)
                    {
                        MessageBox.Show("Không xác định được bản ghi cần cập nhật!", "Lỗi");
                        return;
                    }

                    cmd = new SqlCommand(@"UPDATE ThuongPhat SET lyDo=@lydo, tienThuongPhat=@tien WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@lydo", txtReason.Text);
                    cmd.Parameters.AddWithValue("@tien", soTien);
                    cmd.Parameters.AddWithValue("@id", selectedId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show(btnSave.Text.Contains("Lưu") ? "✅ Đã thêm khen thưởng mới!" : "✏️ Đã cập nhật khen thưởng!");
            LoadKhenThuong();
            ClearForm();
        }

        // ======================= CLICK DGV =======================
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                var id = dgv.Rows[e.RowIndex].Cells["id"].Value.ToString();
                if (MessageBox.Show("Bạn có chắc muốn xóa khen thưởng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM NhanVien_ThuongPhat WHERE idThuongPhat=@id; DELETE FROM ThuongPhat WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadKhenThuong();
                }
                return;
            }

            DataGridViewRow row = dgv.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["id"].Value);
            cbEmployee.Text = row.Cells["TenNhanVien"].Value?.ToString();
            txtReason.Text = row.Cells["Lý do"].Value?.ToString();
            txtAmount.Text = row.Cells["Số tiền thưởng"].Value?.ToString();

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
            if (MessageBox.Show("Bạn có chắc muốn hoàn tác và xóa dữ liệu đang nhập?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ClearForm();
        }

        private void ClearForm()
        {
            cbEmployee.SelectedIndex = -1;
            txtAmount.Clear();
            txtReason.Clear();
            dtReward.Value = DateTime.Now;
            selectedId = null;
            btnSave.Text = "💾 Lưu khen thưởng";
            btnSave.FillColor = Color.MediumSeaGreen;
            dgv.ClearSelection();
        }
    }
}