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
    public partial class ucDuyenTuyenDung : UserControl
    {
        List<Recruitment> Recruitments = new List<Recruitment>()
        {
            new Recruitment() {Title = "Tuyển thực tập sinh phòng công nghệ thông tin", Status = "Đang duyệt"},
            new Recruitment() {Title = "Tuyển Leader công nghệ thông tin", Status = "Hoàn thành"},
            new Recruitment() {Title = "Tuyển Trưởng phòng nhân sự", Status = "Đang tuyển"},
            new Recruitment() {Title = "Tuyển Project Managment", Status = "Hoàn thành"},
            new Recruitment() {Title = "Tuyển kiểm toán trưởng", Status = "Đang tuyển"}
        };

        public ucDuyenTuyenDung()
        {
            InitializeComponent();

        }

        private void ucDuyenTuyenDung_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn comboboxCol = new DataGridViewComboBoxColumn();
            comboboxCol.HeaderText = "Trạng Thái";
            comboboxCol.Name = "Status";
            comboboxCol.Items.AddRange("Đang duyệt", "Đang tuyển", "Hoàn thành");
            comboboxCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            comboboxCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            comboboxCol.FillWeight = 10;

            DataGridViewTextBoxColumn colTitle = new DataGridViewTextBoxColumn();
            colTitle.HeaderText = "Title";
            colTitle.Name = "Title";
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTitle.FillWeight = 90;
            dgvRecruitment.Columns.Add(colTitle);
            dgvRecruitment.Columns.Add(comboboxCol);


            foreach (Recruitment item  in Recruitments)
            {
                dgvRecruitment.Rows.Add(item.Title, item.Status);
            }
        }
    }
}
