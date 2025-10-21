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
using System.Web.ModelBinding;
using System.Windows.Forms;

namespace GUI
{
    public partial class BaoCaoHopDong : UserControl
    {
        private Guna2ComboBox cbPhongBan, cbLoaiHopDong;
        private Guna2DateTimePicker dtFrom, dtTo;
        private Guna2Button btnSearch, btnExcel, btnPDF, btnXuatReport;
        private Guna2DataGridView dgv;
        private readonly string _idNhanVien, _connectionString;

        public BaoCaoHopDong(string stringConnection, string idNhanVien)
        {
            InitializeComponent();
            BuildUI();
            LoadFilterData();
            LoadHopDong();

            _idNhanVien = idNhanVien;
            _connectionString = stringConnection;
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 246, 248);

            Label lblTitle = new Label()
            {
                Text = "BÁO CÁO HỢP ĐỒNG LAO ĐỘNG",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // --- Bộ lọc ---
            cbPhongBan = new Guna2ComboBox() { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cbLoaiHopDong = new Guna2ComboBox() { Width = 350, DropDownStyle = ComboBoxStyle.DropDownList };
            dtFrom = new Guna2DateTimePicker()
            {
                Width = 150,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                ShowCheckBox = true,
                Checked = false // mặc định không lọc
            };
            dtTo = new Guna2DateTimePicker()
            {
                Width = 150,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                ShowCheckBox = true,
                Checked = false
            };

            btnSearch = new Guna2Button() { Text = "🔍 Tìm kiếm", Width = 120, BorderRadius = 6, FillColor = Color.SteelBlue, ForeColor = Color.White };
            btnSearch.Click += BtnSearch_Click;

            btnExcel = new Guna2Button() { Text = "📄 Xuất Excel", Width = 120, BorderRadius = 6, FillColor = Color.SeaGreen, ForeColor = Color.White };
            btnPDF = new Guna2Button() { Text = "🖨️ Xuất PDF", Width = 120, BorderRadius = 6, FillColor = Color.IndianRed, ForeColor = Color.White };
            btnXuatReport = new Guna2Button() { Text = "📃 Xem báo cáo", Width = 140, BorderRadius = 6, FillColor = Color.Orange, ForeColor = Color.White };
            btnXuatReport.Click += BtnXuatReport_Click;

            FlowLayoutPanel filterPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                AutoSize = true
            };
            filterPanel.Controls.AddRange(new Control[] { cbPhongBan, cbLoaiHopDong, dtFrom, dtTo, btnSearch, btnExcel, btnPDF, btnXuatReport });

            // --- DataGridView ---
            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 35 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false
            };


            dgv.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    BtnXuatReport_Click(s, e);
            };
            // --- Layout tổng ---
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.Controls.Add(filterPanel, 0, 0);
            layout.Controls.Add(dgv, 0, 1);

            this.Controls.Add(layout);
            this.Controls.Add(lblTitle);
        }

        private void LoadFilterData()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // --- Phòng ban ---
                SqlDataAdapter da1 = new SqlDataAdapter("SELECT id, TenPhongBan FROM PhongBan", conn);
                DataTable dtPhong = new DataTable();
                da1.Fill(dtPhong);
                cbPhongBan.DataSource = dtPhong;
                cbPhongBan.DisplayMember = "TenPhongBan";
                cbPhongBan.ValueMember = "id";
                cbPhongBan.SelectedIndex = -1;

                // --- Loại hợp đồng ---
                cbLoaiHopDong.Items.AddRange(new object[] {
                    "Hợp đồng lao động xác định thời hạn",
                    "Hợp đồng lao động không xác định thời hạn"
                });
                cbLoaiHopDong.SelectedIndex = -1;
            }
        }

        private void LoadHopDong()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                StringBuilder query = new StringBuilder(@"
                            SELECT 
                                hd.id AS [Mã HĐ], 
                                nv.TenNhanVien AS [Tên nhân viên], 
                                hd.LoaiHopDong AS [Loại HĐ], 
                                hd.NgayKy AS [Ngày ký], 
                                hd.NgayKetThuc AS [Ngày hết hạn], 
                                cv.luongCoBan AS [Lương], 
                                CASE 
                                    WHEN hd.NgayKetThuc >= GETDATE() THEN N'Còn hiệu lực'
                                    ELSE N'Hết hạn'
                                END AS [Trạng thái]
                            FROM HopDongLaoDong hd
                            JOIN NhanVien nv ON hd.idNhanVien = nv.id
                            JOIN ChucVu cv ON nv.idChucVu = cv.id
                            WHERE 1=1
                        ");

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (cbPhongBan.SelectedValue != null && cbPhongBan.SelectedIndex >= 0)
                {
                    query.Append(" AND nv.idPhongBan = @phongban");
                    cmd.Parameters.AddWithValue("@phongban", cbPhongBan.SelectedValue);
                }

                if (!string.IsNullOrEmpty(cbLoaiHopDong.Text))
                {
                    query.Append(" AND hd.LoaiHopDong = @loaiHD");
                    cmd.Parameters.AddWithValue("@loaiHD", cbLoaiHopDong.Text);
                }

                // 🔹 Chỉ lọc ngày nếu người dùng chỉnh datepicker
                DateTime minDate = DateTime.MinValue;
                DateTime maxDate = DateTime.MaxValue;

                if (dtFrom.Checked) // Checked = người dùng chọn
                    minDate = dtFrom.Value.Date;
                if (dtTo.Checked)
                    maxDate = dtTo.Value.Date;

                if (dtFrom.Checked || dtTo.Checked)
                {
                    query.Append(" AND hd.NgayKy BETWEEN @from AND @to");
                    cmd.Parameters.AddWithValue("@from", minDate);
                    cmd.Parameters.AddWithValue("@to", maxDate);
                }

                cmd.CommandText = query.ToString();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadHopDong();
        }

        // 📃 Nút Xuất Report
        private void BtnXuatReport_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hợp đồng để xuất báo cáo!", "Thông báo");
                return;
            }

            // Lấy Mã HĐ của dòng được chọn
            string maHD = dgv.SelectedRows[0].Cells["Mã HĐ"].Value.ToString();

            // Truyền Mã HĐ vào form report
            FrmReportHopDong frm = new FrmReportHopDong(maHD);
            frm.ShowDialog();
        }
    }
}
