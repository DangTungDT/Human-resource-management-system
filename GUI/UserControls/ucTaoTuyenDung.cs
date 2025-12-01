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
    public partial class ucTaoTuyenDung : UserControl
    {
        List<Recruitment> Recruitments = new List<Recruitment>()
        {
            new Recruitment() {Title = "Tuyển thực tập sinh phòng công nghệ thông tin", Status = "Đang tuyển"},
            new Recruitment() {Title = "Tuyển Leader công nghệ thông tin", Status = "Hoàn thành"},
            new Recruitment() {Title = "Tuyển Trưởng phòng nhân sự", Status = "Đang tuyển"},
            new Recruitment() {Title = "Tuyển Project Managment", Status = "Hoàn thành"},
            new Recruitment() {Title = "Tuyển kiểm toán trưởng", Status = "Đang tuyển"}
        };
        public ucTaoTuyenDung()
        {
            InitializeComponent();
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void ucTaoTuyenDung_Load(object sender, EventArgs e)
        {
            //SetPlaceholder(txtTitle, "Nhập tiêu đề tuyển dụng");
            //dgvRecruitment.DataSource = Recruitments;
            //txtCreateDate.Text = DateTime.Now.ToString();
        }
    }
}
