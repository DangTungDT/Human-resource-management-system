using BLL;
using ClosedXML.Excel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using DocumentFormat.OpenXml.Math;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public partial class ThongKeTuyenDung : UserControl
    {
        private Guna2ComboBox cbQuy, cbNam, cbPhongBan, cbChucVu;
        private Guna2Button btnThongKe, btnXuatBaoCao, btnXuatExcel;
        private Guna2DataGridView dgv;
        private Chart chartCot, chartTron, chartTongQuan;
        private string connectionString;
        private readonly BLLPhongBan bllPhongBan;
        private readonly BLLTuyenDung bllTuyenDung;

        public ThongKeTuyenDung(string idNhanVien, string conn)
        {
            connectionString = conn;
            InitializeComponent();
            InitUI();
            bllPhongBan = new BLLPhongBan(conn);
            bllTuyenDung = new BLLTuyenDung(conn);
            LoadChucVu();
            LoadPhongBan();
        }

        private void InitUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            // ===== PANEL TRÊN =====
            var pnlTop = new Guna2Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                Padding = new Padding(20, 15, 20, 5)
            };

            var lblTitle = new Label
            {
                Text = "BÁO CÁO TUYỂN DỤNG THEO QUÝ",
                AutoSize = true,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.DodgerBlue,
                Location = new Point(10, 5)
            };
            pnlTop.Controls.Add(lblTitle);

            // ComboBox & Button
            cbQuy = new Guna2ComboBox { Width = 120 };
            cbQuy.Items.AddRange(new string[] { "Cả năm", "Q1", "Q2", "Q3", "Q4" });
            cbQuy.SelectedIndex = 0;

            cbNam = new Guna2ComboBox { Width = 90 };
            cbNam.Items.AddRange(new string[] { "2023", "2024", "2025" });
            cbNam.SelectedItem = "2025";

            cbPhongBan = new Guna2ComboBox { Width = 180 };
            cbChucVu = new Guna2ComboBox { Width = 180 };

            btnThongKe = new Guna2Button
            {
                Text = "Thống kê",
                Width = 120,
                Height = 36,
                BorderRadius = 6,
                FillColor = Color.RoyalBlue,
                ForeColor = Color.White
            };
            btnThongKe.Click += BtnThongKe_Click;

            btnXuatBaoCao = new Guna2Button
            {
                Text = "Xuất Crystal",
                Width = 130,
                Height = 36,
                BorderRadius = 6,
                FillColor = Color.Orange,
                ForeColor = Color.White
            };
            btnXuatBaoCao.Click += BtnXuatBaoCao_Click;

            btnXuatExcel = new Guna2Button
            {
                Text = "Xuất Excel",
                Width = 120,
                Height = 36,
                BorderRadius = 6,
                FillColor = Color.MediumSeaGreen,
                ForeColor = Color.White
            };
            btnXuatExcel.Click += BtnXuatExcel_Click;

            // FlowLayoutPanel căn hàng ngang đẹp
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = false,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            flow.Controls.AddRange(new Control[]
            {
                new Label { Text = "Quý:", AutoSize = true, Padding = new Padding(5,10,5,0) },
                cbQuy,
                new Label { Text = "Năm:", AutoSize = true, Padding = new Padding(10,10,5,0) },
                cbNam,
                new Label { Text = "Phòng ban:", AutoSize = true, Padding = new Padding(10,10,5,0) },
                cbPhongBan,
                new Label { Text = "Vị trí:", AutoSize = true, Padding = new Padding(10,10,5,0) },
                cbChucVu,
                btnThongKe,
                btnXuatBaoCao,
                btnXuatExcel
            });

            pnlTop.Controls.Add(flow);

            // ===== DATAGRIDVIEW =====
            dgv = new Guna2DataGridView
            {
                Dock = DockStyle.Top,
                Height = 250,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ThemeStyle = { RowsStyle = { Height = 28 } }
            };

            // ===== BIỂU ĐỒ =====
            chartCot = new Chart { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };
            var areaCot = new ChartArea("ChartArea1");
            areaCot.AxisX.Title = "Phòng ban";
            areaCot.AxisY.Title = "Số lượng";
            chartCot.ChartAreas.Add(areaCot);
            chartCot.Series.Add("Ứng tuyển");
            chartCot.Series.Add("Đang phỏng vấn");
            chartCot.Series.Add("Trúng tuyển");
            chartCot.Series.Add("Bị loại");
            foreach (var s in chartCot.Series)
            {
                s.ChartType = SeriesChartType.Column;
                s.IsValueShownAsLabel = true;
            }
            chartCot.Legends.Add(new Legend("Legend1"));

            chartTron = new Chart { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };
            var areaTron = new ChartArea("ChartArea2");
            chartTron.ChartAreas.Add(areaTron);
            chartTron.Series.Add("Đang phỏng vấn");
            chartTron.Series["Đang phỏng vấn"].ChartType = SeriesChartType.Pie;
            chartTron.Series["Đang phỏng vấn"].IsValueShownAsLabel = true;
            chartTron.Legends.Add(new Legend("Legend1"));

            chartTongQuan = new Chart { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };
            var areaTongQuan = new ChartArea("ChartArea3");
            chartTongQuan.ChartAreas.Add(areaTongQuan);
            chartTongQuan.Legends.Add(new Legend("Legend1"));

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.Controls.Add(chartCot, 0, 0);
            layout.Controls.Add(chartTongQuan, 1, 0);

            //layout.RowCount = 2;
            //layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            //layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            //layout.Controls.Add(chartTongQuan, 0, 1);
            //layout.SetColumnSpan(chartTongQuan, 2);

            this.Controls.Add(layout);
            this.Controls.Add(dgv);
            this.Controls.Add(pnlTop);
        }

        private void LoadChucVu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT c.id, c.TenChucVu, c.idPhongBan, p.TenPhongBan
                       FROM ChucVu c
                       INNER JOIN PhongBan p ON c.idPhongBan = p.id";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dtAllChucVu = new DataTable();
                da.Fill(dtAllChucVu);

                // Thêm dòng "Tất cả"
                DataRow allRow = dtAllChucVu.NewRow();
                allRow["id"] = DBNull.Value; // hoặc chuỗi rỗng ""
                allRow["TenChucVu"] = "-- Tất cả chứ vụ --";
                dtAllChucVu.Rows.InsertAt(allRow, 0);

                cbChucVu.DataSource = dtAllChucVu.Copy();
                cbChucVu.DisplayMember = "TenChucVu";
                cbChucVu.ValueMember = "id";
                cbChucVu.SelectedIndex = 0;
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

        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            string quy = cbQuy.SelectedItem?.ToString();
            int nam = int.Parse(cbNam.SelectedItem.ToString());
            string phongBan = cbPhongBan.Text;
            string viTri = cbChucVu.Text;
            if (quy == "Cả năm")
                quy = null;

            if (phongBan == "-- Tất cả phòng ban --")
                phongBan = null;

            if (viTri == "-- Tất cả chứ vụ --")
                viTri = null;

            DataTable dt = bllTuyenDung.GetBaoCaoTuyenDungTheoQuy(quy, nam, phongBan, viTri);
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu cần tìm");
                return;
            }
            dgv.DataSource = dt;

            // --- Biểu đồ cột ---
            foreach (var s in chartCot.Series) s.Points.Clear();

            var groupByPhongBan = dt.AsEnumerable()
                .GroupBy(r => r["TenPhongBan"].ToString())
                .Select(g => new
                {
                    PhongBan = g.Key,
                    UngTuyen = g.Sum(r => Convert.ToInt32(r["UngTuyen"])),
                    DangPV = g.Sum(r => Convert.ToInt32(r["DangPhongVan"])),
                    TrungTuyen = g.Sum(r => Convert.ToInt32(r["TrungTuyen"])),
                    BiLoai = g.Sum(r => Convert.ToInt32(r["BiLoai"]))
                });

            foreach (var g in groupByPhongBan)
            {
                chartCot.Series["Ứng tuyển"].Points.AddXY(g.PhongBan, g.UngTuyen);
                chartCot.Series["Đang phỏng vấn"].Points.AddXY(g.PhongBan, g.DangPV);
                chartCot.Series["Trúng tuyển"].Points.AddXY(g.PhongBan, g.TrungTuyen);
                chartCot.Series["Bị loại"].Points.AddXY(g.PhongBan, g.BiLoai);
            }

            // --- Biểu đồ tròn: Đang phỏng vấn ---
            //chartTron.Series["Đang phỏng vấn"].Points.Clear();

            //var groupByViTri = dt.AsEnumerable()
            //    .GroupBy(r => r["TenViTri"].ToString())
            //    .Select(g => new
            //    {
            //        ViTri = g.Key,
            //        DangPhongVan = g.Sum(r => Convert.ToInt32(r["DangPhongVan"]))
            //    })
            //    .Where(g => g.DangPhongVan > 0) // ✅ chỉ lấy có dữ liệu
            //    .ToList();

            //if (groupByViTri.Count == 0)
            //{
            //    chartTron.Titles.Clear();
            //    chartTron.Titles.Add("Không có dữ liệu đang phỏng vấn");
            //}
            //else
            //{
            //    chartTron.Titles.Clear();
            //    chartTron.Titles.Add("Tình trạng đang phỏng vấn");
            //    foreach (var g in groupByViTri)
            //    {
            //        chartTron.Series["Đang phỏng vấn"].Points.AddXY(g.ViTri, g.DangPhongVan);
            //    }
            //}
            // --- Biểu đồ tròn tổng quan (thêm mới riêng, không đè chartTron) ---
            LoadPieChart(dt);
        }


        //private void LoadPieChart(DataTable dt)
        //{
        //    chartTron.Series.Clear();
        //    chartTron.Titles.Clear();

        //    if (dt.Rows.Count == 0)
        //    {
        //        chartTron.Titles.Add("Không có dữ liệu");
        //        return;
        //    }

        //    // Tính tổng từng trạng thái
        //    int tongUngTuyen = dt.AsEnumerable().Sum(r => Convert.ToInt32(r["UngTuyen"]));
        //    int tongTrungTuyen = dt.AsEnumerable().Sum(r => Convert.ToInt32(r["TrungTuyen"]));
        //    int tongBiLoai = dt.AsEnumerable().Sum(r => Convert.ToInt32(r["BiLoai"]));

        //    // Nếu tổng đều = 0 thì không hiển thị
        //    if (tongUngTuyen == 0 && tongTrungTuyen == 0 && tongBiLoai == 0)
        //    {
        //        chartTron.Titles.Add("Không có dữ liệu biểu đồ tròn");
        //        return;
        //    }

        //    Series series = new Series("Tổng quan");
        //    series.ChartType = SeriesChartType.Pie;

        //    series.Points.AddXY("Ứng tuyển", tongUngTuyen);
        //    series.Points.AddXY("Trúng tuyển", tongTrungTuyen);
        //    series.Points.AddXY("Bị loại", tongBiLoai);

        //    series.Label = "#PERCENT{P1}";
        //    series.LegendText = "#VALX (#VAL)";

        //    chartTron.Series.Add(series);
        //    chartTron.Titles.Add("Tổng quan kết quả tuyển dụng");

        //    // Hiển thị số liệu rõ hơn
        //    chartTron.Legends[0].Docking = Docking.Right;
        //    //chartTron.Legends[0].Alignment = StringAlignment.Center;
        //}

        private void LoadPieChart(DataTable dt)
        {
            chartTongQuan.Series.Clear();
            chartTongQuan.Titles.Clear();

            if (dt.Rows.Count == 0)
            {
                chartTongQuan.Titles.Add("Không có dữ liệu");
                return;
            }

            int tongUngTuyen = dt.AsEnumerable().Sum(r => Convert.ToInt32(r["UngTuyen"]));
            int tongDangPV = dt.AsEnumerable().Sum(r => Convert.ToInt32(r["DangPhongVan"]));
            int tongTrungTuyen = dt.AsEnumerable().Sum(r => Convert.ToInt32(r["TrungTuyen"]));
            int tongBiLoai = dt.AsEnumerable().Sum(r => Convert.ToInt32(r["BiLoai"]));

            if (tongUngTuyen == 0 && tongDangPV == 0 && tongTrungTuyen == 0 && tongBiLoai == 0)
            {
                chartTongQuan.Titles.Add("Không có dữ liệu biểu đồ tròn");
                return;
            }

            Series series = new Series("Tổng quan");
            series.ChartType = SeriesChartType.Pie;
            series.Points.AddXY("Ứng tuyển", tongUngTuyen);
            series.Points.AddXY("Ứng tuyển", tongDangPV);
            series.Points.AddXY("Trúng tuyển", tongTrungTuyen);
            series.Points.AddXY("Bị loại", tongBiLoai);
            series.Label = "#PERCENT{P1}";
            series.LegendText = "#VALX (#VAL)";

            chartTongQuan.Series.Add(series);
            chartTongQuan.Titles.Add("Tổng quan kết quả tuyển dụng");
            chartTongQuan.Legends[0].Docking = Docking.Right;
        }

        private void BtnXuatBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDocument rpt = new ReportDocument();
                string rptPath = Application.StartupPath + @"\rptBaoCaoTuyenDungTheoQuy.rpt";
                rpt.Load(rptPath);
                CrystalReportViewer viewer = new CrystalReportViewer
                {
                    Dock = DockStyle.Fill,
                    ReportSource = rpt
                };

                Form f = new Form
                {
                    Text = "Báo cáo tuyển dụng theo quý",
                    WindowState = FormWindowState.Maximized
                };
                f.Controls.Add(viewer);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở báo cáo: " + ex.Message);
            }
        }

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgv.DataSource == null) return;

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Lưu báo cáo Excel"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var dt = (DataTable)dgv.DataSource;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "BaoCaoTuyenDung");
                    wb.SaveAs(sfd.FileName);
                }
                MessageBox.Show("Xuất Excel thành công!");
            }
        }
    }
}
