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

using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class BaoCaoKhenThuong : UserControl
    {
        private Guna2ComboBox cbPhongBan;
        private Guna2DateTimePicker dtFrom, dtTo;
        private Guna2Button btnSearch, btnExcel, btnPDF, btnXuatReport;
        private Guna2DataGridView dgv;

        private string connectionString =
            @"Data Source=DESKTOP-UM1I61K\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;";

        public BaoCaoKhenThuong()
        {
            InitializeComponent();
            BuildUI();
            LoadPhongBan();
            LoadKhenThuong();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 246, 248);

            Label lblTitle = new Label()
            {
                Text = "BÁO CÁO KHEN THƯỞNG",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            cbPhongBan = new Guna2ComboBox() { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            dtFrom = new Guna2DateTimePicker()
            {
                Width = 150,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                ShowCheckBox = true,
                Checked = false
            };
            dtTo = new Guna2DateTimePicker()
            {
                Width = 150,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                ShowCheckBox = true,
                Checked = false
            };

            btnSearch = new Guna2Button()
            {
                Text = "🔍 Tìm kiếm",
                Width = 120,
                BorderRadius = 6,
                FillColor = Color.SteelBlue,
                ForeColor = Color.White
            };
            btnSearch.Click += BtnSearch_Click;

            btnExcel = new Guna2Button()
            {
                Text = "📄 Xuất Excel",
                Width = 120,
                BorderRadius = 6,
                FillColor = Color.SeaGreen,
                ForeColor = Color.White
            };

            btnPDF = new Guna2Button()
            {
                Text = "🖨️ Xuất PDF",
                Width = 120,
                BorderRadius = 6,
                FillColor = Color.IndianRed,
                ForeColor = Color.White
            };

            btnXuatReport = new Guna2Button()
            {
                Text = "📃 Xem báo cáo",
                Width = 140,
                BorderRadius = 6,
                FillColor = Color.Orange,
                ForeColor = Color.White
            };
            btnXuatReport.Click += BtnXuatReport_Click;

            FlowLayoutPanel filterPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                AutoSize = true
            };
            filterPanel.Controls.AddRange(new Control[] { cbPhongBan, dtFrom, dtTo, btnSearch, btnExcel, btnPDF, btnXuatReport });

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

        private void LoadPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT id, TenPhongBan FROM PhongBan", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbPhongBan.DataSource = dt;
                cbPhongBan.DisplayMember = "TenPhongBan";
                cbPhongBan.ValueMember = "id";
                cbPhongBan.SelectedIndex = -1;
            }
        }

        private void LoadKhenThuong()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                StringBuilder query = new StringBuilder(@"
                    SELECT 
                        tp.id AS [Mã KT],
                        nv.TenNhanVien AS [Tên nhân viên],
                        pb.TenPhongBan AS [Phòng ban],
                        tp.lyDo AS [Lý do],
                        tp.tienThuongPhat AS [Tiền thưởng],
                        nv_tp.thangApDung AS [Tháng áp dụng],
                        tp.idNguoiTao AS [Người tạo]
                    FROM ThuongPhat tp
                    JOIN NhanVien_ThuongPhat nv_tp ON tp.id = nv_tp.idThuongPhat
                    JOIN NhanVien nv ON nv_tp.idNhanVien = nv.id
                    JOIN PhongBan pb ON nv.idPhongBan = pb.id
                    WHERE tp.loai LIKE N'Thưởng'
                ");

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (cbPhongBan.SelectedValue != null && cbPhongBan.SelectedIndex >= 0)
                {
                    query.Append(" AND nv.idPhongBan = @pb");
                    cmd.Parameters.AddWithValue("@pb", cbPhongBan.SelectedValue);
                }

                if (dtFrom.Checked || dtTo.Checked)
                {
                    query.Append(" AND ISNULL(CONVERT(date, nv_tp.thangApDung, 103), GETDATE()) BETWEEN @from AND @to");
                    cmd.Parameters.AddWithValue("@from", dtFrom.Checked ? dtFrom.Value.Date : new DateTime(1900, 1, 1));
                    cmd.Parameters.AddWithValue("@to", dtTo.Checked ? dtTo.Value.Date : DateTime.MaxValue);
                }

                cmd.CommandText = query.ToString();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;

                if (dt.Rows.Count == 0)
                    MessageBox.Show("Không có dữ liệu khen thưởng phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadKhenThuong();
        }

        private void BtnXuatReport_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi để xuất báo cáo!", "Thông báo");
                return;
            }

            string maKT = dgv.SelectedRows[0].Cells[0].Value.ToString();
            FrmReportKhenThuong frm = new FrmReportKhenThuong(maKT);
            frm.ShowDialog();
        }
    }
}
