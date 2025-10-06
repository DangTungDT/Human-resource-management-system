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
    public partial class TaoDanhGiaHieuSuat : UserControl
    {
        private Guna2ComboBox cbEmployee;
        private Guna2DateTimePicker dtReview;
        private NumericUpDown numScore;
        private Guna2TextBox txtNote;
        private Guna2Button btnSave, btnUndo;
        private Guna2DataGridView dgv;

        private string connectionString = @"Data Source=DESKTOP-UM1I61K\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;";
        private string idNguoiDanhGia = "GD00000001"; // người đánh giá tạm
        private int? selectedId = null; // 🔹 lưu id bản ghi đang sửa

        public TaoDanhGiaHieuSuat()
        {
            InitializeComponent();
            BuildUI();
            LoadNhanVien();
            LoadDanhGia();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;

            Label lblTitle = new Label()
            {
                Text = "ĐÁNH GIÁ HIỆU SUẤT NHÂN VIÊN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            cbEmployee = new Guna2ComboBox() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            dtReview = new Guna2DateTimePicker() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Dock = DockStyle.Fill };
            numScore = new NumericUpDown() { Minimum = 0, Maximum = 100, Dock = DockStyle.Fill };
            txtNote = new Guna2TextBox() { PlaceholderText = "Nhận xét", Dock = DockStyle.Fill, Multiline = true, Height = 60 };

            // Nút Lưu
            btnSave = new Guna2Button()
            {
                Text = "💾 Lưu đánh giá",
                BorderRadius = 8,
                FillColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Width = 150,
                Height = 40,
                Cursor = Cursors.Hand
            };
            btnSave.Click += BtnSave_Click;

            // ✅ Nút Hoàn tác
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
            btnUndo.MouseEnter += (s, e) => btnUndo.FillColor = Color.DimGray;
            btnUndo.MouseLeave += (s, e) => btnUndo.FillColor = Color.Gray;

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

            // Form layout
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
            form.Controls.Add(new Label() { Text = "Ngày đánh giá:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 1);
            form.Controls.Add(dtReview, 1, 1);
            form.Controls.Add(new Label() { Text = "Điểm số:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 2);
            form.Controls.Add(numScore, 1, 2);
            form.Controls.Add(new Label() { Text = "Nhận xét:", ForeColor = Color.DarkBlue, AutoSize = true }, 0, 3);
            form.Controls.Add(txtNote, 1, 3);

            // Panel chứa nút
            FlowLayoutPanel btnPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 20, 0, 0)
            };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnUndo);
            form.Controls.Add(btnPanel, 1, 4);

            // Layout tổng
            TableLayoutPanel main = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 300));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.Controls.Add(form, 0, 0);
            main.Controls.Add(dgv, 0, 1);

            this.Controls.Add(main);
            this.Controls.Add(lblTitle);
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

        // ======================= LOAD ĐÁNH GIÁ =======================
        private void LoadDanhGia()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT dg.id, nv.TenNhanVien, dg.ngayTao, dg.DiemSo, dg.NhanXet
                                 FROM DanhGiaNhanVien dg
                                 JOIN NhanVien nv ON dg.idNhanVien = nv.id";
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

        // ======================= LƯU HOẶC CẬP NHẬT =======================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cbEmployee.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Cảnh báo");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;

                if (btnSave.Text.Contains("Lưu"))
                {
                    cmd = new SqlCommand(@"INSERT INTO DanhGiaNhanVien (DiemSo, NhanXet, ngayTao, idNhanVien, idNguoiDanhGia)
                                           VALUES (@diem, @nhanxet, @ngay, @idnv, @idng)", conn);
                }
                else
                {
                    if (selectedId == null)
                    {
                        MessageBox.Show("Không xác định được bản ghi cần cập nhật!", "Lỗi");
                        return;
                    }

                    cmd = new SqlCommand(@"UPDATE DanhGiaNhanVien 
                                           SET DiemSo=@diem, NhanXet=@nhanxet, ngayTao=@ngay, idNhanVien=@idnv 
                                           WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", selectedId);
                }

                cmd.Parameters.AddWithValue("@diem", numScore.Value);
                cmd.Parameters.AddWithValue("@nhanxet", txtNote.Text);
                cmd.Parameters.AddWithValue("@ngay", dtReview.Value);
                cmd.Parameters.AddWithValue("@idnv", cbEmployee.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@idng", idNguoiDanhGia);

                cmd.ExecuteNonQuery();
            }

            string msg = btnSave.Text.Contains("Lưu") ? "✅ Đã thêm đánh giá mới!" : "✏️ Đã cập nhật đánh giá!";
            MessageBox.Show(msg);
            LoadDanhGia();
            ClearForm();
        }

        // ======================= CLICK DGV =======================
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                var id = dgv.Rows[e.RowIndex].Cells["id"].Value.ToString();
                if (MessageBox.Show("Bạn có chắc muốn xóa đánh giá này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM DanhGiaNhanVien WHERE id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadDanhGia();
                }
                return;
            }

            // ✏️ Click vào hàng → đưa dữ liệu lên form
            DataGridViewRow row = dgv.Rows[e.RowIndex];
            selectedId = Convert.ToInt32(row.Cells["id"].Value);
            cbEmployee.Text = row.Cells["TenNhanVien"].Value?.ToString();
            dtReview.Value = DateTime.TryParse(row.Cells["ngayTao"].Value?.ToString(), out DateTime dt) ? dt : DateTime.Now;
            numScore.Value = Convert.ToDecimal(row.Cells["DiemSo"].Value ?? 0);
            txtNote.Text = row.Cells["NhanXet"].Value?.ToString();

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

        // ======================= CLEAR FORM =======================
        private void BtnUndo_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc muốn hoàn tác và xóa dữ liệu đang nhập?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
                ClearForm();
        }

        private void ClearForm()
        {
            cbEmployee.SelectedIndex = -1;
            numScore.Value = 0;
            txtNote.Clear();
            dtReview.Value = DateTime.Now;

            selectedId = null;
            btnSave.Text = "💾 Lưu đánh giá";
            btnSave.FillColor = Color.MediumSeaGreen;
            dgv.ClearSelection();
        }
    }

}
