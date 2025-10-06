using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ucChamCongQuanLy : UserControl
    {
        string IdManager = "";
        List<EmployeTemp> ListEmploye = new List<EmployeTemp>()
        {
            new EmployeTemp(){ HoTen = "Nguyễn Văn An", Email = "an.nguyen@company.com", TrangThaiChamCong = true },
            new EmployeTemp(){ HoTen = "Lê Thị Bích", Email = "bich.le@company.com", TrangThaiChamCong = false },
            new EmployeTemp(){ HoTen = "Trần Quốc Huy", Email = "huy.tran@company.com", TrangThaiChamCong = true },
            new EmployeTemp(){ HoTen = "Phạm Thảo Linh", Email = "linh.pham@company.com", TrangThaiChamCong = true },
            new EmployeTemp(){ HoTen = "Võ Thành Trung", Email = "trung.vo@company.com", TrangThaiChamCong = false },
            new EmployeTemp(){ HoTen = "Đặng Thanh Tùng", Email = "tung.dang@company.com", TrangThaiChamCong = true },
            new EmployeTemp(){ HoTen = "Nguyễn Hữu Phúc", Email = "phuc.nguyen@company.com", TrangThaiChamCong = true },
            new EmployeTemp(){ HoTen = "Huỳnh Ngọc Lan", Email = "lan.huynh@company.com", TrangThaiChamCong = false },
            new EmployeTemp(){ HoTen = "Bùi Minh Khang", Email = "khang.bui@company.com", TrangThaiChamCong = true },
            new EmployeTemp(){ HoTen = "Phan Thị Mai", Email = "mai.phan@company.com", TrangThaiChamCong = false }
        };
        public ucChamCongQuanLy(string idEmployee)
        {
            IdManager = idEmployee;
            InitializeComponent();
        }
        public ucChamCongQuanLy()
        {
            InitializeComponent();
            
        }

        private void ucChamCongQuanLy_Load(object sender, EventArgs e)
        {
            this.Padding = new Padding(0, 10, 0, 0);
            dgvEmployeeAttendance.DataSource = ListEmploye;
            dgvEmployeeAttendance.Columns.Clear();
            dgvEmployeeAttendance.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "HỌ TÊN NHÂN VIÊN",
                DataPropertyName = "HoTen"
            });
            dgvEmployeeAttendance.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Email",
                DataPropertyName = "Email"
            });
            dgvEmployeeAttendance.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                HeaderText = "Đã chấm công",
                DataPropertyName = "TrangThaiChamCong",
                Width = 120
            });
            dgvEmployeeAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvEmployeeAttendance.ColumnHeadersHeight = 35;

            //Gắn placeholder
            SetPlaceholder(txtEmployeeName, "Nhập họ tên nhân viên");
            SetPlaceholder(txtEmail, "Nhập email nhân viên");
            SetPlaceholder(txtPhoneNumber, "Nhập số điện thoại nhân viên");
        }

        private void SetPlaceholder(Guna2TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }
    }
}
