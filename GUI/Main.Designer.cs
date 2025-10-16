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
            this.tpCRUD = new System.Windows.Forms.TabPage();
            this.tpSystem = new System.Windows.Forms.TabPage();
            this.pnContent = new Guna.UI2.WinForms.Guna2Panel();
            this.tcMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMenu
            // 
            this.tcMenu.Controls.Add(this.tpHome);
            this.tcMenu.Controls.Add(this.tpView);
            this.tcMenu.Controls.Add(this.tpReport);
            this.tcMenu.Controls.Add(this.tpCRUD);
            this.tcMenu.Controls.Add(this.tpSystem);
            this.tcMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcMenu.ItemSize = new System.Drawing.Size(180, 30);
            this.tcMenu.Location = new System.Drawing.Point(0, 0);
            this.tcMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tcMenu.Name = "tcMenu";
            this.tcMenu.SelectedIndex = 0;
            this.tcMenu.Size = new System.Drawing.Size(1885, 151);
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
            this.tcMenu.TabIndexChanged += new System.EventHandler(this.tpHome_Click);
            this.tcMenu.Click += new System.EventHandler(this.tcMenu_Click);
            // 
            // tpHome
            // 
            this.tpHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpHome.ForeColor = System.Drawing.Color.Black;
            this.tpHome.Location = new System.Drawing.Point(4, 34);
            this.tpHome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpHome.Name = "tpHome";
            this.tpHome.Padding = new System.Windows.Forms.Padding(4);
            this.tpHome.Size = new System.Drawing.Size(1877, 113);
            this.tpHome.TabIndex = 0;
            this.tpHome.Text = "Trang chủ";
            this.tpHome.Click += new System.EventHandler(this.tpHome_Click);
            // 
            // tpView
            // 
            this.tpView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpView.ForeColor = System.Drawing.Color.Black;
            this.tpView.Location = new System.Drawing.Point(4, 34);
            this.tpView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpView.Name = "tpView";
            this.tpView.Padding = new System.Windows.Forms.Padding(4);
            this.tpView.Size = new System.Drawing.Size(1877, 113);
            this.tpView.TabIndex = 1;
            this.tpView.Text = "Hiển thị";
            this.tpView.Click += new System.EventHandler(this.tpView_Click);
            // 
            // tpReport
            // 
            this.tpReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpReport.ForeColor = System.Drawing.Color.Black;
            this.tpReport.Location = new System.Drawing.Point(4, 34);
            this.tpReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpReport.Name = "tpReport";
            this.tpReport.Size = new System.Drawing.Size(1877, 113);
            this.tpReport.TabIndex = 3;
            this.tpReport.Text = "Báo cáo";
            this.tpReport.Click += new System.EventHandler(this.tpReport_Click);
            // 
            // tpCRUD
            // 
            this.tpCRUD.Location = new System.Drawing.Point(4, 34);
            this.tpCRUD.Name = "tpCRUD";
            this.tpCRUD.Size = new System.Drawing.Size(1877, 113);
            this.tpCRUD.TabIndex = 4;
            this.tpCRUD.Text = "Quản lý";
            this.tpCRUD.UseVisualStyleBackColor = true;
            this.tpCRUD.Click += new System.EventHandler(this.tpCRUD_Click);
            // 
            // tpSystem
            // 
            this.tpSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpSystem.ForeColor = System.Drawing.Color.Black;
            this.tpSystem.Location = new System.Drawing.Point(4, 34);
            this.tpSystem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpSystem.Name = "tpSystem";
            this.tpSystem.Size = new System.Drawing.Size(1877, 113);
            this.tpSystem.TabIndex = 2;
            this.tpSystem.Text = "Hệ thống";
            this.tpSystem.Click += new System.EventHandler(this.tpSystem_Click);
            // 
            // pnContent
            // 
            this.pnContent.BackColor = System.Drawing.SystemColors.Control;
            this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnContent.Location = new System.Drawing.Point(0, 151);
            this.pnContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(1885, 403);
            this.pnContent.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1885, 554);
            this.Controls.Add(this.pnContent);
            this.Controls.Add(this.tcMenu);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần mềm quản lý nhân sự";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.tcMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TabControl tcMenu;
        private System.Windows.Forms.TabPage tpView;
        private System.Windows.Forms.TabPage tpReport;
        private System.Windows.Forms.TabPage tpSystem;
        private Guna.UI2.WinForms.Guna2Panel pnContent;
        private System.Windows.Forms.TabPage tpHome;
        private System.Windows.Forms.TabPage tpCRUD;
    }
}

