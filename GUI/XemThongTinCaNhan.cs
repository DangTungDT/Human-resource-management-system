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
    public partial class XemThongTinCaNhan : UserControl
    {
        private string connectionString;
        private string idNhanVien;

        // ======= Các control giao diện =======
        private Label lblName, lblDob, lblGender, lblAddress, lblQue, lblEmail, lblChucVu, lblPhongBan;
        private Guna2CirclePictureBox picAvatar;
        private Guna2Button btnUpdate;
        private Panel _panel;
        private BLLNhanVien bllNhanVien;

        public XemThongTinCaNhan(string idNV, Panel panel, string conn)
        {
            connectionString = conn;
            idNhanVien = idNV;
            bllNhanVien = new BLLNhanVien(conn);
            InitializeComponent();
            BuildUI();
            _panel = panel;
            // 🔹 Gọi lại load mỗi khi control được hiển thị
            this.Load += (s, e) => LoadThongTinNhanVien();

        }

        private void BuildUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 246, 248); // nền xám nhẹ

            // ===== TIÊU ĐỀ =====
            Label lblTitle = new Label()
            {
                Text = "THÔNG TIN CÁ NHÂN",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 60, 120),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ===== AVATAR =====
            picAvatar = new Guna2CirclePictureBox()
            {
                Size = new Size(200, 150),
                Image = Properties.Resources.user, // ảnh mặc định
                SizeMode = PictureBoxSizeMode.Zoom, //ảnh sẽ phóng/thu đều, không méo.
                //BorderThickness = 3,
                //BorderColor = Color.SteelBlue,
                Anchor = AnchorStyles.Top,
                Margin = new Padding(0)
            };

            // ===== PANEL BÊN TRÁI (AVATAR) =====
            Panel panelLeft = new Panel()   //vùng bên trái của giao diện, chứa avatar.
            {
                Dock = DockStyle.Left,  //nằm ở bên trái toàn bộ form.
                Width = 350,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            panelLeft.Controls.Add(picAvatar);
            picAvatar.Location = new Point((panelLeft.Width - picAvatar.Width) / 2, 40); // căn giữa theo chiều ngang

            // ===== BẢNG THÔNG TIN =====
            TableLayoutPanel tblInfo = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,    //2 cột → 1 cho nhãn, 1 cho giá trị.
                RowCount = 9,
                Padding = new Padding(40, 30, 40, 30),
                BackColor = Color.White,
                AutoSize = true
            };

            //Cột 1 chiếm 15%, cột 2 chiếm 65% (phần còn lại là padding).
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

            // label tiêu đề (cột trái) có biểu tượng emoji + text.
            Label MakeLabel(string text, string emoji) => new Label()
            {
                Text = $"{emoji}  {text}",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 60, 120),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0),
                Margin = new Padding(30,15,15,15)
            };

            //Label hiển thị giá trị (cột phải), ví dụ: “Nguyễn Văn A”, “01/01/1990”,
            Label MakeValueLabel() => new Label()
            {
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0),
                Margin = new Padding(15)
            };

            //khới tạo để gán giá trị
            lblName = MakeValueLabel();
            lblDob = MakeValueLabel();
            lblGender = MakeValueLabel();
            lblAddress = MakeValueLabel();
            lblQue = MakeValueLabel();
            lblEmail = MakeValueLabel();
            lblChucVu = MakeValueLabel();
            lblPhongBan = MakeValueLabel();

            tblInfo.Controls.Add(MakeLabel("Họ và tên:", "👤"), 0, 0);
            tblInfo.Controls.Add(lblName, 1, 0);

            tblInfo.Controls.Add(MakeLabel("Ngày sinh:", "📅"), 0, 1);
            tblInfo.Controls.Add(lblDob, 1, 1);

            tblInfo.Controls.Add(MakeLabel("Giới tính:", "⚧"), 0, 2);
            tblInfo.Controls.Add(lblGender, 1, 2);

            tblInfo.Controls.Add(MakeLabel("Địa chỉ:", "🏠"), 0, 3);
            tblInfo.Controls.Add(lblAddress, 1, 3);

            tblInfo.Controls.Add(MakeLabel("Quê quán:", "🌾"), 0, 4);
            tblInfo.Controls.Add(lblQue, 1, 4);

            tblInfo.Controls.Add(MakeLabel("Email:", "✉️"), 0, 5);
            tblInfo.Controls.Add(lblEmail, 1, 5);

            tblInfo.Controls.Add(MakeLabel("Chức vụ:", "💼"), 0, 6);
            tblInfo.Controls.Add(lblChucVu, 1, 6);

            tblInfo.Controls.Add(MakeLabel("Phòng ban:", "🏢"), 0, 7);
            tblInfo.Controls.Add(lblPhongBan, 1, 7);

            // ===== NÚT CẬP NHẬT =====
            btnUpdate = new Guna2Button()
            {
                Text = "✏️ Cập nhật thông tin",
                BorderRadius = 8,
                FillColor = Color.SteelBlue,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Width = 220,
                Height = 45,
                Anchor = AnchorStyles.None,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 15, 0, 15)
            };

            // Hiệu ứng hover
            btnUpdate.HoverState.FillColor = Color.MediumSeaGreen;
            btnUpdate.HoverState.ForeColor = Color.White;
            btnUpdate.Click += BtnUpdate_Click;

            Panel panelButton = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(245, 246, 248)
            };
            panelButton.Controls.Add(btnUpdate);
            btnUpdate.Location = new Point((panelButton.Width - btnUpdate.Width) / 2, 20);  //căn nút cập nhật ra giữa form.
            panelButton.Resize += (s, e) =>
            {
                btnUpdate.Location = new Point((panelButton.Width - btnUpdate.Width) / 2, 20);  // dù thay đổi kích thước nút luôn giữa
            };

            // ===== LAYOUT TỔNG =====
            TableLayoutPanel mainLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            mainLayout.Controls.Add(panelLeft, 0, 0);
            mainLayout.Controls.Add(tblInfo, 1, 0);
            mainLayout.Controls.Add(panelButton, 0, 1);
            mainLayout.SetColumnSpan(panelButton, 2);

            // ===== THÊM VÀO CONTROL CHÍNH =====
            this.Controls.Add(mainLayout);
            this.Controls.Add(lblTitle);
        }

        // ===============================
        // 🔹 Nút “Cập nhật thông tin”
        // ===============================
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            CapNhatThongTinRieng capNhatPage = new CapNhatThongTinRieng(idNhanVien, _panel, connectionString);
            var parent = this.ParentForm as Main;
            parent?.ShowUserControl("CapNhatThongTinRieng");
            parent.ChildFormComponent(_panel, "ButtonFeatureViewComponent");
        }

        // ===============================
        // 🔹 Load dữ liệu nhân viên
        // ===============================
        private void LoadThongTinNhanVien()
        {
            DTONhanVien nv = bllNhanVien.LayThongTin(idNhanVien);
            if (nv != null)
            {
                lblName.Text = nv.TenNhanVien;
                lblDob.Text = nv.NgaySinh.ToString("dd/MM/yyyy");
                lblGender.Text = nv.GioiTinh;
                lblAddress.Text = nv.DiaChi;
                lblQue.Text = nv.Que;
                lblEmail.Text = nv.Email;
                lblChucVu.Text = nv.TenChucVu;
                lblPhongBan.Text = nv.TenPhongBan;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        
        }

        // 🟢 Ghi đè sự kiện OnVisibleChanged
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                LoadThongTinNhanVien(); // tự động load lại khi hiển thị
            }
        }
    }
}