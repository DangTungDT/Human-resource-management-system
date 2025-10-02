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
            this.tpView = new System.Windows.Forms.TabPage();
            this.tpReport = new System.Windows.Forms.TabPage();
            this.tpSystem = new System.Windows.Forms.TabPage();
            this.pnContent = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCapNhatTTNV = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaoKyLuat = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaoDanhGiaHieuSuat = new Guna.UI2.WinForms.Guna2Button();
            this.tcMenu.SuspendLayout();
            this.tpHome.SuspendLayout();
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
            this.tcMenu.Size = new System.Drawing.Size(800, 123);
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
            this.tpHome.Controls.Add(this.btnTaoDanhGiaHieuSuat);
            this.tpHome.Controls.Add(this.btnTaoKyLuat);
            this.tpHome.Controls.Add(this.btnCapNhatTTNV);
            this.tpHome.ForeColor = System.Drawing.Color.Black;
            this.tpHome.Location = new System.Drawing.Point(4, 34);
            this.tpHome.Name = "tpHome";
            this.tpHome.Padding = new System.Windows.Forms.Padding(3);
            this.tpHome.Size = new System.Drawing.Size(792, 85);
            this.tpHome.TabIndex = 0;
            this.tpHome.Text = "Trang chủ";
            // 
            // tpView
            // 
            this.tpView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpView.ForeColor = System.Drawing.Color.Black;
            this.tpView.Location = new System.Drawing.Point(4, 34);
            this.tpView.Name = "tpView";
            this.tpView.Padding = new System.Windows.Forms.Padding(3);
            this.tpView.Size = new System.Drawing.Size(792, 85);
            this.tpView.TabIndex = 1;
            this.tpView.Text = "Hiện thị";
            // 
            // tpReport
            // 
            this.tpReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpReport.ForeColor = System.Drawing.Color.Black;
            this.tpReport.Location = new System.Drawing.Point(4, 34);
            this.tpReport.Name = "tpReport";
            this.tpReport.Size = new System.Drawing.Size(792, 85);
            this.tpReport.TabIndex = 3;
            this.tpReport.Text = "Báo cáo";
            // 
            // tpSystem
            // 
            this.tpSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpSystem.ForeColor = System.Drawing.Color.Black;
            this.tpSystem.Location = new System.Drawing.Point(4, 34);
            this.tpSystem.Name = "tpSystem";
            this.tpSystem.Size = new System.Drawing.Size(792, 85);
            this.tpSystem.TabIndex = 2;
            this.tpSystem.Text = "Hệ thống";
            // 
            // pnContent
            // 
            this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnContent.Location = new System.Drawing.Point(0, 123);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(800, 327);
            this.pnContent.TabIndex = 1;
            // 
            // btnCapNhatTTNV
            // 
            this.btnCapNhatTTNV.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCapNhatTTNV.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCapNhatTTNV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCapNhatTTNV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCapNhatTTNV.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCapNhatTTNV.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatTTNV.Location = new System.Drawing.Point(19, 17);
            this.btnCapNhatTTNV.Name = "btnCapNhatTTNV";
            this.btnCapNhatTTNV.Size = new System.Drawing.Size(133, 45);
            this.btnCapNhatTTNV.TabIndex = 0;
            this.btnCapNhatTTNV.Text = "Cập nhật Thông Tin Nhân Viên";
            this.btnCapNhatTTNV.Click += new System.EventHandler(this.btnCapNhatTTNV_Click);
            // 
            // btnTaoKyLuat
            // 
            this.btnTaoKyLuat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoKyLuat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoKyLuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaoKyLuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaoKyLuat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaoKyLuat.ForeColor = System.Drawing.Color.White;
            this.btnTaoKyLuat.Location = new System.Drawing.Point(317, 17);
            this.btnTaoKyLuat.Name = "btnTaoKyLuat";
            this.btnTaoKyLuat.Size = new System.Drawing.Size(133, 45);
            this.btnTaoKyLuat.TabIndex = 1;
            this.btnTaoKyLuat.Text = "Tạo kỷ luật";
            this.btnTaoKyLuat.Click += new System.EventHandler(this.btnTaoKyLuat_Click);
            // 
            // btnTaoDanhGiaHieuSuat
            // 
            this.btnTaoDanhGiaHieuSuat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoDanhGiaHieuSuat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoDanhGiaHieuSuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaoDanhGiaHieuSuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaoDanhGiaHieuSuat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaoDanhGiaHieuSuat.ForeColor = System.Drawing.Color.White;
            this.btnTaoDanhGiaHieuSuat.Location = new System.Drawing.Point(168, 17);
            this.btnTaoDanhGiaHieuSuat.Name = "btnTaoDanhGiaHieuSuat";
            this.btnTaoDanhGiaHieuSuat.Size = new System.Drawing.Size(133, 45);
            this.btnTaoDanhGiaHieuSuat.TabIndex = 2;
            this.btnTaoDanhGiaHieuSuat.Text = "Tao đánh giá hiệu suất";
            this.btnTaoDanhGiaHieuSuat.Click += new System.EventHandler(this.btnTaoDanhGiaHieuSuat_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnContent);
            this.Controls.Add(this.tcMenu);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Name = "Main";
            this.Text = "Phần mềm quản lý nhân sự";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tcMenu.ResumeLayout(false);
            this.tpHome.ResumeLayout(false);
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
    }
}

