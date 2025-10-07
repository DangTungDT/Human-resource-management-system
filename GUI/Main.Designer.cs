namespace GUI
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcMenu = new Guna.UI2.WinForms.Guna2TabControl();
            this.tpHome = new System.Windows.Forms.TabPage();
            this.btnCRUDChucVu = new Guna.UI2.WinForms.Guna2Button();
            this.btnCRUDPhongBan = new Guna.UI2.WinForms.Guna2Button();
            this.btnCRUDTaiKhoan = new Guna.UI2.WinForms.Guna2Button();
            this.btnCapNhatThongTinRieng = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaoKhenThuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaoDanhGiaHieuSuat = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaoKyLuat = new Guna.UI2.WinForms.Guna2Button();
            this.btnCapNhatTTNV = new Guna.UI2.WinForms.Guna2Button();
            this.tpView = new System.Windows.Forms.TabPage();
            this.btnXemThongTinCaNhan = new Guna.UI2.WinForms.Guna2Button();
            this.btnXemNghiPhep = new Guna.UI2.WinForms.Guna2Button();
            this.tpReport = new System.Windows.Forms.TabPage();
            this.btnBaoCaoKhenThuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaoHDLD = new Guna.UI2.WinForms.Guna2Button();
            this.tpSystem = new System.Windows.Forms.TabPage();
            this.pnContent = new Guna.UI2.WinForms.Guna2Panel();
            this.tcMenu.SuspendLayout();
            this.tpHome.SuspendLayout();
            this.tpView.SuspendLayout();
            this.tpReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMenu
            // 
            this.tcMenu.Controls.Add(this.tpHome);
            this.tcMenu.Controls.Add(this.tpView);
            this.tcMenu.Controls.Add(this.tpReport);
            this.tcMenu.Controls.Add(this.tpSystem);
            this.tcMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcMenu.ItemSize = new System.Drawing.Size(180, 30);
            this.tcMenu.Location = new System.Drawing.Point(0, 0);
            this.tcMenu.Name = "tcMenu";
            this.tcMenu.SelectedIndex = 0;
            this.tcMenu.Size = new System.Drawing.Size(1414, 123);
            this.tcMenu.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tcMenu.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.tcMenu.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcMenu.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tcMenu.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tcMenu.TabButtonIdleState.BorderColor = System.Drawing.Color.White;
            this.tcMenu.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tcMenu.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcMenu.TabButtonIdleState.ForeColor = System.Drawing.Color.Black;
            this.tcMenu.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tcMenu.TabButtonSelectedState.BorderColor = System.Drawing.Color.White;
            this.tcMenu.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tcMenu.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcMenu.TabButtonSelectedState.ForeColor = System.Drawing.Color.Blue;
            this.tcMenu.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tcMenu.TabButtonSize = new System.Drawing.Size(180, 30);
            this.tcMenu.TabIndex = 0;
            this.tcMenu.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tcMenu.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            // 
            // tpHome
            // 
            this.tpHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpHome.Controls.Add(this.btnCRUDChucVu);
            this.tpHome.Controls.Add(this.btnCRUDPhongBan);
            this.tpHome.Controls.Add(this.btnCRUDTaiKhoan);
            this.tpHome.Controls.Add(this.btnCapNhatThongTinRieng);
            this.tpHome.Controls.Add(this.btnTaoKhenThuong);
            this.tpHome.Controls.Add(this.btnTaoDanhGiaHieuSuat);
            this.tpHome.Controls.Add(this.btnTaoKyLuat);
            this.tpHome.Controls.Add(this.btnCapNhatTTNV);
            this.tpHome.ForeColor = System.Drawing.Color.Black;
            this.tpHome.Location = new System.Drawing.Point(4, 34);
            this.tpHome.Name = "tpHome";
            this.tpHome.Padding = new System.Windows.Forms.Padding(3);
            this.tpHome.Size = new System.Drawing.Size(1406, 85);
            this.tpHome.TabIndex = 0;
            this.tpHome.Text = "Trang chủ";
            // 
            // btnCRUDChucVu
            // 
            this.btnCRUDChucVu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCRUDChucVu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCRUDChucVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCRUDChucVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCRUDChucVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCRUDChucVu.ForeColor = System.Drawing.Color.White;
            this.btnCRUDChucVu.Location = new System.Drawing.Point(1092, 17);
            this.btnCRUDChucVu.Name = "btnCRUDChucVu";
            this.btnCRUDChucVu.Size = new System.Drawing.Size(133, 45);
            this.btnCRUDChucVu.TabIndex = 7;
            this.btnCRUDChucVu.Text = "Cập nhật chức vụ";
            this.btnCRUDChucVu.Click += new System.EventHandler(this.btnCRUDChucVu_Click);
            // 
            // btnCRUDPhongBan
            // 
            this.btnCRUDPhongBan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCRUDPhongBan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCRUDPhongBan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCRUDPhongBan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCRUDPhongBan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCRUDPhongBan.ForeColor = System.Drawing.Color.White;
            this.btnCRUDPhongBan.Location = new System.Drawing.Point(940, 17);
            this.btnCRUDPhongBan.Name = "btnCRUDPhongBan";
            this.btnCRUDPhongBan.Size = new System.Drawing.Size(133, 45);
            this.btnCRUDPhongBan.TabIndex = 6;
            this.btnCRUDPhongBan.Text = "Cập nhật phòng ban";
            this.btnCRUDPhongBan.Click += new System.EventHandler(this.btnCRUDPhongBan_Click);
            // 
            // btnCRUDTaiKhoan
            // 
            this.btnCRUDTaiKhoan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCRUDTaiKhoan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCRUDTaiKhoan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCRUDTaiKhoan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCRUDTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCRUDTaiKhoan.ForeColor = System.Drawing.Color.White;
            this.btnCRUDTaiKhoan.Location = new System.Drawing.Point(784, 17);
            this.btnCRUDTaiKhoan.Name = "btnCRUDTaiKhoan";
            this.btnCRUDTaiKhoan.Size = new System.Drawing.Size(133, 45);
            this.btnCRUDTaiKhoan.TabIndex = 5;
            this.btnCRUDTaiKhoan.Text = "Cập nhật tài khoản";
            this.btnCRUDTaiKhoan.Click += new System.EventHandler(this.btnCRUDTaiKhoan_Click);
            // 
            // btnCapNhatThongTinRieng
            // 
            this.btnCapNhatThongTinRieng.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCapNhatThongTinRieng.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCapNhatThongTinRieng.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCapNhatThongTinRieng.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCapNhatThongTinRieng.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCapNhatThongTinRieng.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatThongTinRieng.Location = new System.Drawing.Point(8, 17);
            this.btnCapNhatThongTinRieng.Name = "btnCapNhatThongTinRieng";
            this.btnCapNhatThongTinRieng.Size = new System.Drawing.Size(133, 45);
            this.btnCapNhatThongTinRieng.TabIndex = 4;
            this.btnCapNhatThongTinRieng.Text = "Cập nhật Thông Tin Cá Nhân";
            this.btnCapNhatThongTinRieng.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // btnTaoKhenThuong
            // 
            this.btnTaoKhenThuong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoKhenThuong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoKhenThuong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaoKhenThuong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaoKhenThuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaoKhenThuong.ForeColor = System.Drawing.Color.White;
            this.btnTaoKhenThuong.Location = new System.Drawing.Point(628, 17);
            this.btnTaoKhenThuong.Name = "btnTaoKhenThuong";
            this.btnTaoKhenThuong.Size = new System.Drawing.Size(133, 45);
            this.btnTaoKhenThuong.TabIndex = 3;
            this.btnTaoKhenThuong.Text = "Tạo khen thưởng";
            this.btnTaoKhenThuong.Click += new System.EventHandler(this.btnTaoKhenThuong_Click);
            // 
            // btnTaoDanhGiaHieuSuat
            // 
            this.btnTaoDanhGiaHieuSuat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoDanhGiaHieuSuat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoDanhGiaHieuSuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaoDanhGiaHieuSuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaoDanhGiaHieuSuat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaoDanhGiaHieuSuat.ForeColor = System.Drawing.Color.White;
            this.btnTaoDanhGiaHieuSuat.Location = new System.Drawing.Point(317, 17);
            this.btnTaoDanhGiaHieuSuat.Name = "btnTaoDanhGiaHieuSuat";
            this.btnTaoDanhGiaHieuSuat.Size = new System.Drawing.Size(133, 45);
            this.btnTaoDanhGiaHieuSuat.TabIndex = 2;
            this.btnTaoDanhGiaHieuSuat.Text = "Tao đánh giá hiệu suất";
            this.btnTaoDanhGiaHieuSuat.Click += new System.EventHandler(this.btnTaoDanhGiaHieuSuat_Click);
            // 
            // btnTaoKyLuat
            // 
            this.btnTaoKyLuat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoKyLuat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoKyLuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaoKyLuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaoKyLuat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaoKyLuat.ForeColor = System.Drawing.Color.White;
            this.btnTaoKyLuat.Location = new System.Drawing.Point(473, 17);
            this.btnTaoKyLuat.Name = "btnTaoKyLuat";
            this.btnTaoKyLuat.Size = new System.Drawing.Size(133, 45);
            this.btnTaoKyLuat.TabIndex = 1;
            this.btnTaoKyLuat.Text = "Tạo kỷ luật";
            this.btnTaoKyLuat.Click += new System.EventHandler(this.btnTaoKyLuat_Click);
            // 
            // btnCapNhatTTNV
            // 
            this.btnCapNhatTTNV.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCapNhatTTNV.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCapNhatTTNV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCapNhatTTNV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCapNhatTTNV.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCapNhatTTNV.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatTTNV.Location = new System.Drawing.Point(162, 17);
            this.btnCapNhatTTNV.Name = "btnCapNhatTTNV";
            this.btnCapNhatTTNV.Size = new System.Drawing.Size(133, 45);
            this.btnCapNhatTTNV.TabIndex = 0;
            this.btnCapNhatTTNV.Text = "Cập nhật Thông Tin Nhân Viên";
            this.btnCapNhatTTNV.Click += new System.EventHandler(this.btnCapNhatTTNV_Click);
            // 
            // tpView
            // 
            this.tpView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpView.Controls.Add(this.btnXemThongTinCaNhan);
            this.tpView.Controls.Add(this.btnXemNghiPhep);
            this.tpView.ForeColor = System.Drawing.Color.Black;
            this.tpView.Location = new System.Drawing.Point(4, 34);
            this.tpView.Name = "tpView";
            this.tpView.Padding = new System.Windows.Forms.Padding(3);
            this.tpView.Size = new System.Drawing.Size(1406, 85);
            this.tpView.TabIndex = 1;
            this.tpView.Text = "Hiện thị";
            // 
            // btnXemThongTinCaNhan
            // 
            this.btnXemThongTinCaNhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXemThongTinCaNhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXemThongTinCaNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXemThongTinCaNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXemThongTinCaNhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXemThongTinCaNhan.ForeColor = System.Drawing.Color.White;
            this.btnXemThongTinCaNhan.Location = new System.Drawing.Point(179, 17);
            this.btnXemThongTinCaNhan.Name = "btnXemThongTinCaNhan";
            this.btnXemThongTinCaNhan.Size = new System.Drawing.Size(133, 45);
            this.btnXemThongTinCaNhan.TabIndex = 2;
            this.btnXemThongTinCaNhan.Text = "Xem Thông Tin Cá Nhân";
            this.btnXemThongTinCaNhan.Click += new System.EventHandler(this.btnXemThongTinCaNhan_Click);
            // 
            // btnXemNghiPhep
            // 
            this.btnXemNghiPhep.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXemNghiPhep.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXemNghiPhep.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXemNghiPhep.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXemNghiPhep.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXemNghiPhep.ForeColor = System.Drawing.Color.White;
            this.btnXemNghiPhep.Location = new System.Drawing.Point(18, 17);
            this.btnXemNghiPhep.Name = "btnXemNghiPhep";
            this.btnXemNghiPhep.Size = new System.Drawing.Size(133, 45);
            this.btnXemNghiPhep.TabIndex = 1;
            this.btnXemNghiPhep.Text = "Xem thông tin nghỉ phép";
            this.btnXemNghiPhep.Click += new System.EventHandler(this.btnXemNghiPhep_Click);
            // 
            // tpReport
            // 
            this.tpReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpReport.Controls.Add(this.btnBaoCaoKhenThuong);
            this.tpReport.Controls.Add(this.btnTaoHDLD);
            this.tpReport.ForeColor = System.Drawing.Color.Black;
            this.tpReport.Location = new System.Drawing.Point(4, 34);
            this.tpReport.Name = "tpReport";
            this.tpReport.Size = new System.Drawing.Size(1406, 85);
            this.tpReport.TabIndex = 3;
            this.tpReport.Text = "Báo cáo";
            // 
            // btnBaoCaoKhenThuong
            // 
            this.btnBaoCaoKhenThuong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBaoCaoKhenThuong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBaoCaoKhenThuong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBaoCaoKhenThuong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBaoCaoKhenThuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBaoCaoKhenThuong.ForeColor = System.Drawing.Color.White;
            this.btnBaoCaoKhenThuong.Location = new System.Drawing.Point(185, 17);
            this.btnBaoCaoKhenThuong.Name = "btnBaoCaoKhenThuong";
            this.btnBaoCaoKhenThuong.Size = new System.Drawing.Size(133, 45);
            this.btnBaoCaoKhenThuong.TabIndex = 6;
            this.btnBaoCaoKhenThuong.Text = "Khen Thưởng";
            this.btnBaoCaoKhenThuong.Click += new System.EventHandler(this.btnBaoCaoKhenThuong_Click);
            // 
            // btnTaoHDLD
            // 
            this.btnTaoHDLD.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoHDLD.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoHDLD.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaoHDLD.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaoHDLD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaoHDLD.ForeColor = System.Drawing.Color.White;
            this.btnTaoHDLD.Location = new System.Drawing.Point(22, 17);
            this.btnTaoHDLD.Name = "btnTaoHDLD";
            this.btnTaoHDLD.Size = new System.Drawing.Size(133, 45);
            this.btnTaoHDLD.TabIndex = 5;
            this.btnTaoHDLD.Text = "Hợp đồng lao động";
            this.btnTaoHDLD.Click += new System.EventHandler(this.btnTaoHDLD_Click);
            // 
            // tpSystem
            // 
            this.tpSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpSystem.ForeColor = System.Drawing.Color.Black;
            this.tpSystem.Location = new System.Drawing.Point(4, 34);
            this.tpSystem.Name = "tpSystem";
            this.tpSystem.Size = new System.Drawing.Size(1406, 85);
            this.tpSystem.TabIndex = 2;
            this.tpSystem.Text = "Hệ thống";
            // 
            // pnContent
            // 
            this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnContent.Location = new System.Drawing.Point(0, 123);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(1414, 327);
            this.pnContent.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1414, 450);
            this.Controls.Add(this.pnContent);
            this.Controls.Add(this.tcMenu);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Name = "Main";
            this.Text = "Phần mềm quản lý nhân sự";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tcMenu.ResumeLayout(false);
            this.tpHome.ResumeLayout(false);
            this.tpView.ResumeLayout(false);
            this.tpReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TabControl tcMenu;
        private System.Windows.Forms.TabPage tpHome;
        private System.Windows.Forms.TabPage tpView;
        private System.Windows.Forms.TabPage tpReport;
        private System.Windows.Forms.TabPage tpSystem;
        private Guna.UI2.WinForms.Guna2Panel pnContent;
        private Guna.UI2.WinForms.Guna2Button btnCapNhatTTNV;
        private Guna.UI2.WinForms.Guna2Button btnTaoDanhGiaHieuSuat;
        private Guna.UI2.WinForms.Guna2Button btnTaoKyLuat;
        private Guna.UI2.WinForms.Guna2Button btnTaoKhenThuong;
        private Guna.UI2.WinForms.Guna2Button btnXemThongTinCaNhan;
        private Guna.UI2.WinForms.Guna2Button btnXemNghiPhep;
        private Guna.UI2.WinForms.Guna2Button btnCapNhatThongTinRieng;
        private Guna.UI2.WinForms.Guna2Button btnTaoHDLD;
        private Guna.UI2.WinForms.Guna2Button btnCRUDPhongBan;
        private Guna.UI2.WinForms.Guna2Button btnCRUDTaiKhoan;
        private Guna.UI2.WinForms.Guna2Button btnCRUDChucVu;
        private Guna.UI2.WinForms.Guna2Button btnBaoCaoKhenThuong;
    }
}

