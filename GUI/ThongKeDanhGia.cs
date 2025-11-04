using BLL;
using ClosedXML.Excel;
using CrystalDecisions.CrystalReports.Engine;
using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; // để dùng DataTable
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public partial class ThongKeDanhGia : UserControl
    {

        private Guna2TabControl tabMain;
        private Guna2ComboBox cbThang, cbNam, cbPhongBan;
        private Guna2Button btnXemBaoCao, btnXuatExcel, btnXuatReport;
        private Guna2DataGridView dgvBaoCao;
        private Chart chartTongQuan;
        private readonly BLLDanhGiaNhanVien bll;
        private readonly BLLPhongBan bllPhongBan;
        private readonly string idNhanVien;
        private readonly string connectionString;

        public ThongKeDanhGia(string idNhanVien, string conn)
        {
            InitializeComponent();
            connectionString = conn;
            bll = new BLLDanhGiaNhanVien(conn);
            bllPhongBan = new BLLPhongBan(conn);
            this.Load += (s, e) =>
            {
                BuildUI();
                LoadPhongBan(); // ✅ bây giờ cbPhongBan đã tồn tại
            };
        }

        private void BuildUI()
        {
            tabMain = new Guna2TabControl
            {
                Dock = DockStyle.Fill,
                TabButtonHoverState = { Font = new Font("Segoe UI", 10, FontStyle.Bold) }
            };

            // Tab 1: Thống kê
            var tabThongKe = new TabPage("📊 Thống kê tổng quan");
            chartTongQuan = new Chart { Dock = DockStyle.Fill };
            tabThongKe.Controls.Add(chartTongQuan);

            // Tab 2: Báo cáo chi tiết
            var tabBaoCao = new TabPage("🧾 Báo cáo đánh giá");
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2 };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // Bộ lọc
            var filterPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(10, 10, 10, 10),
                WrapContents = false
            };

            // Tạo label
            var lblThang = new Label { Text = "Tháng:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Black, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(0, 8, 0, 0) };
            var lblNam = new Label { Text = "Năm:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Black, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(0, 8, 0, 0) };
            var lblPhongBan = new Label { Text = "Phòng ban:", AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Black, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(0, 8, 0, 0) };

            cbThang = new Guna2ComboBox { Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
            cbNam = new Guna2ComboBox { Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPhongBan = new Guna2ComboBox { Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            btnXemBaoCao = new Guna2Button { Text = "Xem báo cáo", Width = 120 };
            btnXuatExcel = new Guna2Button { Text = "Xuất Excel", Width = 120 };
            btnXuatReport = new Guna2Button { Text = "Xuất Báo Cáo", Width = 120 };
            btnXuatReport.Click += BtnXuatReport_Click;


            // Sắp xếp theo thứ tự có label đi kèm
            filterPanel.Controls.AddRange(new Control[] {
                lblThang, cbThang,
                lblNam, cbNam,
                lblPhongBan, cbPhongBan,
                btnXemBaoCao, btnXuatExcel, btnXuatReport
            });

            dgvBaoCao = new Guna2DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            layout.Controls.Add(filterPanel, 0, 0);
            layout.Controls.Add(dgvBaoCao, 0, 1);
            tabBaoCao.Controls.Add(layout);

            tabMain.TabPages.Add(tabThongKe);
            tabMain.TabPages.Add(tabBaoCao);
            Controls.Add(tabMain);

            LoadThangNam();
            btnXemBaoCao.Click += BtnXemBaoCao_Click;
            btnXuatExcel.Click += BtnXuatExcel_Click;
        }

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvBaoCao.DataSource == null)
            {
                MessageBox.Show("Chưa có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Files|*.xlsx",
                FileName = $"BaoCaoDanhGia_{cbThang.Text}_{cbNam.Text}.xlsx"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var dt = (DataTable)dgvBaoCao.DataSource;

                        using (var wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add("Báo cáo đánh giá");

                            // Tiêu đề lớn
                            ws.Cell(1, 1).Value = $"BÁO CÁO ĐÁNH GIÁ NHÂN VIÊN THÁNG {cbThang.Text}/{cbNam.Text}";
                            ws.Range(1, 1, 1, dt.Columns.Count).Merge();
                            ws.Cell(1, 1).Style.Font.Bold = true;
                            ws.Cell(1, 1).Style.Font.FontSize = 14;
                            ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            // Header
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                ws.Cell(3, i + 1).Value = dt.Columns[i].ColumnName;
                                ws.Cell(3, i + 1).Style.Font.Bold = true;
                                ws.Cell(3, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                                ws.Cell(3, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            }

                            // Dữ liệu
                            for (int i = 0; i < dt.Rows.Count; i++)
                                for (int j = 0; j < dt.Columns.Count; j++)
                                    ws.Cell(i + 4, j + 1).Value = dt.Rows[i][j]?.ToString();

                            ws.Columns().AdjustToContents();
                            wb.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadPhongBan()
        {
            DataTable dt = bllPhongBan.ComboboxPhongBan();

            // Thêm dòng "Tất cả"
            DataRow allRow = dt.NewRow();
            allRow["id"] = DBNull.Value; // hoặc chuỗi rỗng ""
            allRow["TenPhongBan"] = "-- Tất cả phòng ban --";
            dt.Rows.InsertAt(allRow, 0);

            cbPhongBan.DataSource = dt;
            cbPhongBan.DisplayMember = "TenPhongBan";
            cbPhongBan.ValueMember = "id";
            cbPhongBan.SelectedIndex = 0; // Mặc định chọn "Tất cả"
        }

        private void LoadChart(DataTable dt)
        {
            chartTongQuan.Series.Clear();
            chartTongQuan.ChartAreas.Clear();
            chartTongQuan.Legends.Clear();

            chartTongQuan.BackColor = Color.White;

            var chartArea = new ChartArea("Main")
            {
                AxisX = {
                    LabelStyle = { Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Black },
                    MajorGrid = { LineColor = Color.LightGray },
                    Title = "Phòng ban",
                    TitleFont = new Font("Segoe UI", 10, FontStyle.Bold),
                    TitleForeColor = Color.Black
                },
                AxisY = {
                    LabelStyle = { Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Black },
                    MajorGrid = { LineColor = Color.LightGray },
                    Title = "Điểm trung bình",
                    TitleFont = new Font("Segoe UI", 10, FontStyle.Bold),
                    TitleForeColor = Color.Black
                }
            };

            chartTongQuan.ChartAreas.Add(chartArea);

            var series = new Series("Điểm trung bình")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(72, 138, 255),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                IsValueShownAsLabel = true,
                LabelForeColor = Color.Black
            };

            foreach (DataRow row in dt.Rows)
            {
                string phongBan = row["TenPhongBan"].ToString();
                decimal diemTB = Convert.ToDecimal(row["DiemTrungBinh"]);
                series.Points.AddXY(phongBan, diemTB);
            }

            chartTongQuan.Series.Add(series);
            chartTongQuan.Titles.Clear();
            chartTongQuan.Titles.Add(new Title("THỐNG KÊ ĐIỂM TRUNG BÌNH THEO PHÒNG BAN", Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold), Color.Black));
        }


        private void LoadThangNam()
        {
            // Tháng
            for (int i = 1; i <= 12; i++)
                cbThang.Items.Add(i.ToString());
            cbThang.SelectedIndex = DateTime.Now.Month - 1;

            // Năm
            int year = DateTime.Now.Year;
            for (int i = year - 5; i <= year; i++)
                cbNam.Items.Add(i.ToString());
            cbNam.SelectedItem = year.ToString();
        }

        private void BtnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (cbThang.SelectedItem == null || cbNam.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tháng và năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int thang = int.Parse(cbThang.SelectedItem.ToString());
            int nam = int.Parse(cbNam.SelectedItem.ToString());

            string idPhongBan = cbPhongBan.SelectedValue == null || cbPhongBan.SelectedValue == DBNull.Value
                ? null
                : cbPhongBan.SelectedValue.ToString();

            // --- Biểu đồ tổng quan ---
            DataTable dtThongKe = bll.GetMonthlySummary(thang, nam);
            if (dtThongKe.Rows.Count > 0)
                LoadChart(dtThongKe);
            else
                chartTongQuan.Series.Clear();

            // --- Báo cáo chi tiết ---

            DataTable dt = bll.GetDetailedReport(thang, nam, idPhongBan);
            dgvBaoCao.DataSource = dt;
        }

        //private void BtnXuatReport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string reportPath = Path.Combine(Application.StartupPath, @"..\..\rptDanhGiaNhanVien.rpt");
        //        reportPath = Path.GetFullPath(reportPath);

        //        if (!File.Exists(reportPath))
        //        {
        //            MessageBox.Show("Không tìm thấy file Crystal Report tại:\n" + reportPath,
        //                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        var rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        //        rpt.Load(reportPath);

        //        DataTable dt = (DataTable)dgvBaoCao.DataSource;
        //        rpt.SetDataSource(dt);

        //        using (var viewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer())
        //        {
        //            viewer.ReportSource = rpt;
        //            Form f = new Form { Text = "Báo cáo đánh giá nhân viên", Width = 1000, Height = 700 };
        //            viewer.Dock = DockStyle.Fill;
        //            f.Controls.Add(viewer);
        //            f.ShowDialog();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi khi tải Crystal Report:\n" + ex.Message,
        //                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        private void BtnXuatReport_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Đường dẫn report
                string reportPath = Path.Combine(Application.StartupPath, @"..\..\rptDanhGiaNhanVien.rpt");
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show("Không tìm thấy file Crystal Report tại:\n" + reportPath,
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ReportDocument rpt = new ReportDocument();
                rpt.Load(reportPath);

                // 2. Lấy dữ liệu từ SP
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_XuatDanhGiaNhanVien", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lấy giá trị từ form/parameter
                    int thang = Convert.ToInt32(cbThang.SelectedValue ?? cbThang.Text);
                    int nam = Convert.ToInt32(cbNam.SelectedValue ?? cbNam.Text);
                    int? idPhongBan = cbPhongBan.SelectedValue as int?; // combobox chọn phòng ban

                    MessageBox.Show($"Tháng={thang}, Năm={nam}, IdPhongBan={idPhongBan}");


                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);
                    if (idPhongBan.HasValue)
                        cmd.Parameters.AddWithValue("@IdPhongBan", idPhongBan.Value);
                    else
                        cmd.Parameters.AddWithValue("@IdPhongBan", DBNull.Value);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                // 3. Gắn datasource cho report
                rpt.SetDataSource(dt);

                // 4. Hiển thị báo cáo
                Form f = new Form { Text = "Báo cáo đánh giá nhân viên", Width = 1000, Height = 700 };
                var viewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer
                {
                    Dock = DockStyle.Fill,
                    ReportSource = rpt,
                    ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                };
                f.Controls.Add(viewer);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất báo cáo:\n" + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
