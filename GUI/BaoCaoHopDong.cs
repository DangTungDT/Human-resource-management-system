//using Aspose.Words;
using ClosedXML.Excel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Guna.UI2.WinForms;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class BaoCaoHopDong : UserControl
    {
        private Guna2ComboBox cbPhongBan, cbLoaiHopDong;
        private Guna2DateTimePicker dtFrom, dtTo;
        private Guna2Button btnSearch, btnExcel, btnPDF, btnWord, btnCrystal;
        private Guna2DataGridView dgv;
        private readonly string _idNhanVien, _connectionString;
        private Panel _panel;
        public BaoCaoHopDong(string stringConnection, string idNhanVien, Panel panel)
        {
            InitializeComponent();
            _idNhanVien = idNhanVien;
            _connectionString = stringConnection;
            //BuildUI();
            //LoadFilterData();
            //LoadHopDong();
            _panel = panel;

            BuildUI();
            LoadFilterData();
            LoadHopDong();
        }

        //private void BuildUI()
        //{
        //    this.Dock = DockStyle.Fill;
        //    this.BackColor = Color.FromArgb(245, 246, 248);

        //    Label lblTitle = new Label()
        //    {
        //        Text = "BÁO CÁO HỢP ĐỒNG LAO ĐỘNG",
        //        Font = new Font("Segoe UI", 14, FontStyle.Bold),
        //        ForeColor = Color.DarkBlue,
        //        Dock = DockStyle.Top,
        //        Height = 50,
        //        TextAlign = ContentAlignment.MiddleCenter
        //    };

        //    cbPhongBan = new Guna2ComboBox() { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
        //    cbLoaiHopDong = new Guna2ComboBox() { Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
        //    dtFrom = new Guna2DateTimePicker() { Width = 150, Format = DateTimePickerFormat.Short, ShowCheckBox = true };
        //    dtTo = new Guna2DateTimePicker() { Width = 150, Format = DateTimePickerFormat.Short, ShowCheckBox = true };

        //    btnSearch = new Guna2Button() { Text = "🔍 Tìm kiếm", Width = 110, FillColor = Color.SteelBlue, ForeColor = Color.White };
        //    btnSearch.Click += (s, e) => LoadHopDong();

        //    btnExcel = new Guna2Button() { Text = "📊 Xuất Excel", Width = 120, FillColor = Color.SeaGreen, ForeColor = Color.White };
        //    btnExcel.Click += BtnExcel_Click;

        //    btnPDF = new Guna2Button() { Text = "📄 Xuất PDF", Width = 120, FillColor = Color.IndianRed, ForeColor = Color.White };
        //    btnPDF.Click += BtnPDF_Click;

        //    btnWord = new Guna2Button() { Text = "📝 Xuất Word", Width = 120, FillColor = Color.RoyalBlue, ForeColor = Color.White };
        //    btnWord.Click += BtnWord_Click;

        //    btnCrystal = new Guna2Button() { Text = "📃 Crystal Report", Width = 150, FillColor = Color.Orange, ForeColor = Color.White };
        //    btnCrystal.Click += BtnCrystal_Click;

        //    FlowLayoutPanel filterPanel = new FlowLayoutPanel()
        //    {
        //        Dock = DockStyle.Top,
        //        AutoSize = true,
        //        Padding = new Padding(10),
        //        FlowDirection = FlowDirection.LeftToRight,
        //        WrapContents = false
        //    };
        //    filterPanel.Controls.AddRange(new Control[] {
        //        cbPhongBan, cbLoaiHopDong, dtFrom, dtTo, btnSearch, btnExcel, btnPDF, btnWord, btnCrystal
        //    });

        //    dgv = new Guna2DataGridView()
        //    {
        //        Dock = DockStyle.Fill,
        //        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        //        ReadOnly = true,
        //        AllowUserToAddRows = false,
        //        RowTemplate = { Height = 35 }
        //    };

        //    TableLayoutPanel layout = new TableLayoutPanel() { Dock = DockStyle.Fill, RowCount = 2 };
        //    layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        //    layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        //    layout.Controls.Add(filterPanel, 0, 0);
        //    layout.Controls.Add(dgv, 0, 1);

        //    this.Controls.Add(layout);
        //    this.Controls.Add(lblTitle);
        //}

        //private void LoadFilterData()
        //{
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        conn.Open();
        //        SqlDataAdapter da = new SqlDataAdapter("SELECT id, TenPhongBan FROM PhongBan", conn);
        //        DataTable dtPhong = new DataTable();
        //        da.Fill(dtPhong);
        //        cbPhongBan.DataSource = dtPhong;
        //        cbPhongBan.DisplayMember = "TenPhongBan";
        //        cbPhongBan.ValueMember = "id";
        //        cbPhongBan.SelectedIndex = -1;

        //        cbLoaiHopDong.Items.AddRange(new object[] {
        //            "Hợp đồng lao động xác định thời hạn",
        //            "Hợp đồng lao động không xác định thời hạn"
        //        });
        //        cbLoaiHopDong.SelectedIndex = -1;
        //    }
        //}

        //private DataTable GetData()
        //{
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        conn.Open();
        //        StringBuilder query = new StringBuilder(@"
        //            SELECT 
        //                hd.id AS [Mã HĐ], 
        //                nv.TenNhanVien AS [Tên nhân viên], 
        //                hd.LoaiHopDong AS [Loại HĐ], 
        //                hd.NgayKy AS [Ngày ký], 
        //                hd.NgayKetThuc AS [Ngày hết hạn], 
        //                cv.luongCoBan AS [Lương], 
        //                CASE WHEN hd.NgayKetThuc >= GETDATE() THEN N'Còn hiệu lực' ELSE N'Hết hạn' END AS [Trạng thái]
        //            FROM HopDongLaoDong hd
        //            JOIN NhanVien nv ON hd.idNhanVien = nv.id
        //            JOIN ChucVu cv ON nv.idChucVu = cv.id
        //            WHERE 1=1
        //        ");

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = conn;

        //        if (cbPhongBan.SelectedValue != null)
        //        {
        //            query.Append(" AND nv.idPhongBan = @phongban");
        //            cmd.Parameters.AddWithValue("@phongban", cbPhongBan.SelectedValue);
        //        }
        //        if (!string.IsNullOrEmpty(cbLoaiHopDong.Text))
        //        {
        //            query.Append(" AND hd.LoaiHopDong = @loaiHD");
        //            cmd.Parameters.AddWithValue("@loaiHD", cbLoaiHopDong.Text);
        //        }
        //        if (dtFrom.Checked || dtTo.Checked)
        //        {
        //            query.Append(" AND hd.NgayKy BETWEEN @from AND @to");
        //            cmd.Parameters.AddWithValue("@from", dtFrom.Checked ? dtFrom.Value : DateTime.MinValue);
        //            cmd.Parameters.AddWithValue("@to", dtTo.Checked ? dtTo.Value : DateTime.MaxValue);
        //        }

        //        cmd.CommandText = query.ToString();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        return dt;
        //    }
        //}

        //private void LoadHopDong()
        //{
        //    dgv.DataSource = GetData();
        //}

        //// Excel
        //private void BtnExcel_Click(object sender, EventArgs e)
        //{
        //    var dt = GetData();
        //    if (dt.Rows.Count == 0) { MessageBox.Show("Không có dữ liệu!"); return; }

        //    string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Export\\HopDong_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        //    Directory.CreateDirectory(Path.GetDirectoryName(file));

        //    using (SaveFileDialog sfd = new SaveFileDialog()
        //    {
        //        Filter = "Excel files (*.xlsx)|*.xlsx",
        //        FileName = "BaoCaoHopDong.xlsx"
        //    })
        //    {
        //        if (sfd.ShowDialog() == DialogResult.OK)
        //        {
        //            using (var workbook = new XLWorkbook())
        //            {
        //                var worksheet = workbook.Worksheets.Add("Báo cáo hợp đồng");
        //                worksheet.Cell(1, 1).Value = "Mã hợp đồng";
        //                worksheet.Cell(1, 2).Value = "Tên nhân viên";
        //                worksheet.Cell(1, 3).Value = "Ngày ký";

        //                worksheet.Cell(2, 1).Value = "HD001";
        //                worksheet.Cell(2, 2).Value = "Nguyễn Văn A";
        //                worksheet.Cell(2, 3).Value = DateTime.Now.ToShortDateString();

        //                worksheet.Columns().AdjustToContents();
        //                workbook.SaveAs(sfd.FileName);
        //            }

        //            MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //}

        //// PDF
        //private void BtnPDF_Click(object sender, EventArgs e)
        //{
        //    var dt = GetData();
        //    if (dt.Rows.Count == 0) { MessageBox.Show("Không có dữ liệu!"); return; }

        //    string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Export\\HopDong_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        //    Directory.CreateDirectory(Path.GetDirectoryName(file));

        //    Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
        //    PdfWriter.GetInstance(doc, new FileStream(file, FileMode.Create));
        //    doc.Open();
        //    PdfPTable table = new PdfPTable(dt.Columns.Count);
        //    foreach (DataColumn col in dt.Columns) table.AddCell(new Phrase(col.ColumnName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)));
        //    foreach (DataRow row in dt.Rows)
        //        foreach (var cell in row.ItemArray)
        //            table.AddCell(cell?.ToString() ?? "");
        //    doc.Add(table);
        //    doc.Close();

        //    MessageBox.Show($"Đã xuất PDF: {file}");
        //}

        //// Word
        //private void BtnWord_Click(object sender, EventArgs e)
        //{
        //    var dt = GetData();
        //    if (dt.Rows.Count == 0) { MessageBox.Show("Không có dữ liệu!"); return; }

        //    string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Export\\HopDong_{DateTime.Now:yyyyMMddHHmmss}.docx");
        //    Directory.CreateDirectory(Path.GetDirectoryName(file));

        //    StringBuilder sb = new StringBuilder("<table border='1'><tr>");
        //    foreach (DataColumn col in dt.Columns) sb.Append($"<th>{col.ColumnName}</th>");
        //    sb.Append("</tr>");
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        sb.Append("<tr>");
        //        foreach (var cell in row.ItemArray) sb.Append($"<td>{cell}</td>");
        //        sb.Append("</tr>");
        //    }
        //    sb.Append("</table>");

        //    Document doc = new Document();
        //    doc.SaveFromHtml(sb.ToString());
        //    doc.Save(file);

        //    MessageBox.Show($"Đã xuất Word: {file}");
        //}

        //// Crystal Report
        //private void BtnCrystal_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = GetData();
        //    if (dt.Rows.Count == 0) { MessageBox.Show("Không có dữ liệu để xuất báo cáo!"); return; }

        //    ReportDocument rpt = new ReportDocument();
        //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports\\rptHopDongLaoDong.rpt");
        //    rpt.Load(path);
        //    rpt.SetDataSource(dt);

        //    //FrmReport frm = new FrmReport(rpt);
        //    //frm.ShowDialog();
        //}
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
            //UCBaoCaoHopDong uc = new UCBaoCaoHopDong(_connectionString);
            //DisplayUserControlPanel.ChildUserControl(uc, _panel);

            frmBaoCaoHopDong frm = new frmBaoCaoHopDong(_connectionString);
            frm.ShowDialog(this);
        }
    }
}
