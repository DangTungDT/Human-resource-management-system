using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using DAL;
using Guna.UI2.WinForms;
using System;
using System.IO;
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
    public partial class FormBaoCaoNhanVien : Form
    {
        private readonly BLLPhongBan bllPhongBan;
        private readonly string idNhanVien;
        private readonly string connectionString;

        public FormBaoCaoNhanVien(string idNhanVien, string conn)
        {
            InitializeComponent();
            this.connectionString = conn;
            this.idNhanVien = idNhanVien;
            bllPhongBan = new BLLPhongBan(conn); // khởi tạo BLL
        }

        private void FormBaoCaoNhanVien_Load(object sender, EventArgs e)
        {
            DataTable dt = bllPhongBan.ComboboxPhongBan();

            // Thêm dòng "Tất cả"
            DataRow allRow = dt.NewRow();
            allRow["id"] = DBNull.Value;
            allRow["TenPhongBan"] = "-- Tất cả phòng ban --";
            dt.Rows.InsertAt(allRow, 0);

            cbPhongBan.DataSource = dt;
            cbPhongBan.DisplayMember = "TenPhongBan";
            cbPhongBan.ValueMember = "id";
            cbPhongBan.SelectedIndex = 0;
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Xác định IdPhongBan
                int? idPhongBan = null;

                // Nếu cbPhongBan không phải dòng "Tất cả", lấy Id phòng ban
                if (cbPhongBan.SelectedValue != null && cbPhongBan.SelectedValue != DBNull.Value)
                {
                    if (int.TryParse(cbPhongBan.SelectedValue.ToString(), out int pb))
                    {
                        idPhongBan = pb;
                    }
                    else
                    {
                        MessageBox.Show("Phòng ban không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                // Nếu SelectedValue = DBNull.Value → idPhongBan = null → SP hiểu là tất cả

                // 2️⃣ Lấy dữ liệu từ Stored Procedure
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_XuatNhanVien", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@IdPhongBan", idPhongBan ?? (object)DBNull.Value);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    crvBaoCao.ReportSource = null;
                    return;
                }

                // 3️⃣ Load Crystal Report
                string reportPath = Path.Combine(Application.StartupPath, @"..\..\rptDanhSachNhanVien.rpt");
                reportPath = Path.GetFullPath(reportPath);

                if (!File.Exists(reportPath))
                {
                    MessageBox.Show("Không tìm thấy file Crystal Report:\n" + reportPath, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ReportDocument rpt = new ReportDocument();
                rpt.Load(reportPath);
                rpt.SetDataSource(dt);

                // 4️⃣ Hiển thị report
                crvBaoCao.ReportSource = rpt;
                crvBaoCao.ToolPanelView = ToolPanelViewType.None;
                crvBaoCao.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất báo cáo:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}


