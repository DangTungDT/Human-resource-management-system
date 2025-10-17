using BLL;
using DTO;
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
    public partial class XemNghiPhep : UserControl
    {
        private Guna2DataGridView dgv;
        private string connectionString;
        private string idNhanVien;
        private BLLNghiPhep bllNghiPhep;

        public XemNghiPhep(string conn, string idNV)
        {
            connectionString = conn;
            idNhanVien = idNV;
            bllNghiPhep = new BLLNghiPhep(conn);
            InitializeComponent();
            BuildUI();
            LoadDanhSachNghiPhep();
        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;

            Label lblTitle = new Label()
            {
                Text = "Danh sách nghỉ phép",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter
            };

            dgv = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            this.Controls.Add(dgv);
            this.Controls.Add(lblTitle);
        }

        private void LoadDanhSachNghiPhep()
        {
            List<DTONghiPhep> ds = bllNghiPhep.LayDanhSachNghiPhep(idNhanVien);

            DataTable dt = new DataTable();
            dt.Columns.Add("Ngày bắt đầu", typeof(DateTime));
            dt.Columns.Add("Ngày kết thúc", typeof(DateTime));
            dt.Columns.Add("Lý do nghỉ", typeof(string));
            dt.Columns.Add("Loại nghỉ phép", typeof(string));
            dt.Columns.Add("Trạng thái", typeof(string));

            foreach (var item in ds)
            {
                dt.Rows.Add(item.NgayBatDau, item.NgayKetThuc, item.LyDoNghi, item.LoaiNghiPhep, item.TrangThai);
            }

            dgv.DataSource = dt;
        }
    }
}
