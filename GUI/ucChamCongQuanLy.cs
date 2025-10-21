using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ucChamCongQuanLy : UserControl
    {
        public readonly string _idNhanVien, _conn;

        public ucChamCongQuanLy(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;
        }

        string IdManager = "";
        bool Check = false;
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

        //Cham cong bang checkbox
        public ucChamCongQuanLy(string idEmployee)
        {
            IdManager = idEmployee;
            InitializeComponent();
        }

        //Cham cong = hinh anh
        public ucChamCongQuanLy(string idEmployee, bool check)
        {
            IdManager = idEmployee;
            InitializeComponent();
            Check = true;
        }
        public ucChamCongQuanLy()
        {
            InitializeComponent();

        }


        private void ucChamCongQuanLy_Load(object sender, EventArgs e)
        {
            //Gắn placeholder
            SetPlaceholder(txtEmployeeName, "Nhập họ tên nhân viên");
            SetPlaceholder(txtEmail, "Nhập email nhân viên");
            SetPlaceholder(txtPhoneNumber, "Nhập số điện thoại nhân viên");

            if (!Check)
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
            }
            else
            {
                dgvEmployeeAttendance.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgvEmployeeAttendance.RowTemplate.Height = 150;

                for (int i = 1; i < 6; i++)
                {
                    var imgCol = new DataGridViewImageColumn();
                    imgCol.HeaderText = i.ToString();
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dgvEmployeeAttendance.Columns.Add(imgCol);
                }

                string imageFolder = Path.Combine(Application.StartupPath, @"..\..\..\Image");
                imageFolder = Path.GetFullPath(imageFolder);

                if (!Directory.Exists(imageFolder))
                {
                    MessageBox.Show("Không tìm thấy thư mục Image!");
                    return;
                }

                // Lấy tất cả file ảnh
                string[] imageFiles = Directory.GetFiles(imageFolder, "*.*")
                                               .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                                                        || f.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                                                        || f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                                               .ToArray();

                // Thêm ảnh vào DataGridView, mỗi dòng chứa 5 ảnh
                for (int i = 0; i < imageFiles.Length; i += 5)
                {
                    object[] row = new object[5];
                    for (int j = 0; j < 5; j++)
                    {
                        int index = i + j;
                        if (index < imageFiles.Length)
                        {
                            row[j] = Image.FromFile(imageFiles[index]);
                        }
                        else
                        {
                            row[j] = null;
                        }
                    }
                    dgvEmployeeAttendance.Rows.Add(row);
                }
            }
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
