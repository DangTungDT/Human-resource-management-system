using BLL;
using DAL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace GUI
{
    public partial class ucChamCongQuanLy : UserControl
    {
        public string[,] _arrIdSelected { get; set; }
        public readonly string _idNhanVien;
        public readonly BLLNhanVien _dbContextStaff;
        private readonly int _idStaffDepartment;
        private BLLChamCong _bllChamCong;
        private bool _checkedIn, _checkedOut = false;

        //Cham cong = hinh anh
        public ucChamCongQuanLy(string idEmployee, int idDepartment, string conn)
        {
            InitializeComponent();
            _idStaffDepartment = idDepartment;
            _dbContextStaff = new BLLNhanVien(conn);
            _idNhanVien = idEmployee;

            _bllChamCong = new BLLChamCong(conn);
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

            //Load dữ liệu image cho Datagridview
            LoadDGVImageStaff(null);

            //Kiểm tra checkin tất cả chưa
            if (_bllChamCong.CheckAttendance(_arrIdSelected))
            {
                _checkedIn = true;
            }
            rdoCheckedIn.Checked = true;
        }

        private void LoadDGVImageStaff(List<ImageStaff> listStaff)
        {
            if (listStaff == null)
            {
                listStaff = _dbContextStaff.GetStaffByDepartment(_idStaffDepartment);
            }
            if (listStaff.Count < 1)
            {
                MessageBox.Show("Không có dữ liệu image");
                return;
            }

            dgvEmployeeAttendance.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvEmployeeAttendance.RowTemplate.Height = 150;

            //Tạo 5 cột dữ liệu kiểu Image cho datagridview
            for (int i = 1; i < 6; i++)
            {
                var imgCol = new DataGridViewImageColumn();
                imgCol.HeaderText = i.ToString();
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

                //Tắt icon x khi image null
                imgCol.DefaultCellStyle.NullValue = null;

                dgvEmployeeAttendance.Columns.Add(imgCol);
            }

            //Lấy đường dẫn folder chứa hình ảnh nhân viên
            string imageFolder = Path.Combine(Application.StartupPath, @"..\..\..\Image");
            imageFolder = Path.GetFullPath(imageFolder);
            if (!Directory.Exists(imageFolder))
            {
                MessageBox.Show("Không tìm thấy thư mục Image!");
                return;
            }

            //Lấy id và hình của nhân viên có trong phòng ban
            

            // Tính số dòng cần thiết
            int dong = (listStaff.Count + 5 - 1) / 5;

            //Tạo mảng 2 chiều để gán giá trị id nhân viên theo cấu trúc của datagirdview hình
            _arrIdSelected = new string[dong, 5];

            int index = 0;
            for (int r = 0; r < dong; r++)
            {
                object[] row = new object[5];

                for (int c = 0; c < 5; c++)
                {
                    if (index < listStaff.Count)
                    {
                        var staff = listStaff[index];

                        row[c] = Image.FromFile($"{imageFolder}\\{staff.ImageName}");

                        _arrIdSelected[r, c] = staff.Id;
                    }
                    else
                    {
                        row[c] = null;
                    }

                    index++;
                }

                dgvEmployeeAttendance.Rows.Add(row);
            }

            dgvEmployeeAttendance.AllowUserToAddRows = false;
        }

        private void dgvEmployeeAttendance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvEmployeeAttendance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lấy vị trí dòng và cột
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            //Bỏ qua khi click vào header
            if (row < 0 || col < 0) return;

            if (_arrIdSelected[row, col] != null)
            {
                DTOChamCong dto = new DTOChamCong
                {
                    NgayChamCong = DateTime.Now,
                    GioVao = DateTime.Now.TimeOfDay,
                    IdNhanVien = _arrIdSelected[row, col]
                };
                string result = _bllChamCong.Add(dto).ToLower();
                if (result == "data already exists")
                {
                    //Dữ liệu đã tồn tại 
                    //Là chấm công ra
                    dto.GioRa = DateTime.Now.TimeOfDay;
                    if(_bllChamCong.UpdateGioRa(dto))
                    {
                        MessageBox.Show("Chấm công về thành công!", "Thông báo");
                        if (_bllChamCong.CheckAttendanceOut(_arrIdSelected))
                        {
                            _checkedOut = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chấm công về thất bại!", "Thông báo");
                    }
                }
                else if(result == "invalid data")
                {
                    //Dữ liệu sai
                    MessageBox.Show("Dữ liệu không hợp lệ!", "Thông báo");
                }
                else if(result == "data added successfully")
                {
                    //Thêm thành công
                    MessageBox.Show("Chấm công vào thành công!", "Thông báo");
                    if(_bllChamCong.CheckAttendance(_arrIdSelected))
                    {
                        _checkedIn = true;
                    }
                }
                else
                {
                    //Thêm thất bại
                    MessageBox.Show("Chấm công thất bại!", "Thông báo");
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

        private void btnChamCongVaoTatCa_Click(object sender, EventArgs e)
        {
            if(!_checkedIn)
            {
                foreach (string idStaff in _arrIdSelected)
                {
                    if (idStaff == null) continue;
                    DTOChamCong dto = new DTOChamCong
                    {
                        NgayChamCong = DateTime.Now,
                        GioVao = DateTime.Now.TimeOfDay,
                        IdNhanVien = idStaff
                    };
                    string result = _bllChamCong.Add(dto).ToLower();
                    if (result == "invalid data")
                    {
                        //Dữ liệu sai
                        MessageBox.Show("Dữ liệu không hợp lệ!", "Thông báo");
                        return;
                    }
                    else if (result == "failed to add data")
                    {
                        //Thêm thất bại
                        MessageBox.Show("Chấm công thất bại!", "Thông báo");
                        return;
                    }
                }
                MessageBox.Show("Chấm công thành công!", "Thông báo");
                _checkedIn = true;
            }
            {
                MessageBox.Show("Đã chấm công rồi!", "Thông báo");
            }
        }

        private void txtEmployeeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép phím điều khiển như Backspace, Delete
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Cho phép chữ, số, dấu chấm, gạch dưới, gạch ngang, và ký tự @
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != '_' &&
                e.KeyChar != '-' &&
                e.KeyChar != '@')
            {
                e.Handled = true; // chặn ký tự không hợp lệ
            }
        }
        bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void btnFilterEmployees_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text != "Nhập email nhân viên" && txtEmail.Text != "")
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email nhập không hợp lệ, vui lòng nhập lại email!", "Thông báo");
                    return;
                }
            }
            if(txtEmployeeName.Text == "Nhập họ tên nhân viên")
            {
                txtEmployeeName.Text = "";
            }
            if(txtEmail.Text == "Nhập email nhân viên")
            {
                txtEmail.Text = "";
            }
            if(rdoCheckedIn.Checked)
            {
                LoadDGVImageStaff(_dbContextStaff.GetStaffByNameEmailCheckin(txtEmployeeName.Text, txtEmail.Text, true, _idStaffDepartment));
            }
            else
            {
                LoadDGVImageStaff(_dbContextStaff.GetStaffByNameEmailCheckin(txtEmployeeName.Text, txtEmail.Text, false, _idStaffDepartment));
            }
            
        }

        private void btnChamCongRaTatCa_Click(object sender, EventArgs e)
        {
            if(_checkedIn)
            {
                if(!_checkedOut) {
                    //Đã chấm công vào cho tất cả
                    foreach (string idStaff in _arrIdSelected)
                    {
                        if (idStaff == null) continue;
                        DTOChamCong dto = new DTOChamCong
                        {
                            NgayChamCong = DateTime.Now,
                            GioVao = DateTime.Now.TimeOfDay,
                            IdNhanVien = idStaff
                        };
                        dto.GioRa = DateTime.Now.TimeOfDay;
                        if (!_bllChamCong.UpdateGioRa(dto))
                        {
                            MessageBox.Show("Chấm công về thất bại!", "Thông báo");
                            return;
                        }
                    }
                    MessageBox.Show("Chấm công về cho tất cả thành công!", "Thông báo");
                    _checkedOut = true;
                }
                else
                {
                    MessageBox.Show("Đã chấm công về cho tất cả rồi, vui lòng không chấm nữa!", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Chưa chấm công vào tất cả, vui lòng chấm công cho tất cả trước khi chấm ra!", "Thông báo");
            }
        }
    }
}
