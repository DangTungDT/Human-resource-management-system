namespace GUI
{
    partial class ButtonFeatureReportComponent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnChiTietLuong = new Guna.UI2.WinForms.Guna2TileButton();
            this.btnDanhGia = new Guna.UI2.WinForms.Guna2TileButton();
            this.btnKyLuat = new Guna.UI2.WinForms.Guna2TileButton();
            this.guna2TileButton4 = new Guna.UI2.WinForms.Guna2TileButton();
            this.guna2TileButton1 = new Guna.UI2.WinForms.Guna2TileButton();
            this.guna2TileButton2 = new Guna.UI2.WinForms.Guna2TileButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnChiTietLuong);
            this.flowLayoutPanel1.Controls.Add(this.btnDanhGia);
            this.flowLayoutPanel1.Controls.Add(this.btnKyLuat);
            this.flowLayoutPanel1.Controls.Add(this.guna2TileButton4);
            this.flowLayoutPanel1.Controls.Add(this.guna2TileButton1);
            this.flowLayoutPanel1.Controls.Add(this.guna2TileButton2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1422, 89);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btnChiTietLuong
            // 
            this.btnChiTietLuong.BackColor = System.Drawing.Color.Transparent;
            this.btnChiTietLuong.BorderRadius = 5;
            this.btnChiTietLuong.BorderThickness = 1;
            this.btnChiTietLuong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChiTietLuong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChiTietLuong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChiTietLuong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChiTietLuong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnChiTietLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChiTietLuong.ForeColor = System.Drawing.Color.Black;
            this.btnChiTietLuong.Image = global::GUI.Properties.Resources.salary;
            this.btnChiTietLuong.ImageOffset = new System.Drawing.Point(0, 8);
            this.btnChiTietLuong.ImageSize = new System.Drawing.Size(30, 30);
            this.btnChiTietLuong.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChiTietLuong.IndicateFocus = true;
            this.btnChiTietLuong.Location = new System.Drawing.Point(4, 4);
            this.btnChiTietLuong.Margin = new System.Windows.Forms.Padding(4);
            this.btnChiTietLuong.Name = "btnChiTietLuong";
            this.btnChiTietLuong.Size = new System.Drawing.Size(170, 64);
            this.btnChiTietLuong.TabIndex = 3;
            this.btnChiTietLuong.Text = "Chấm công";
            this.btnChiTietLuong.TextFormatNoPrefix = true;
            this.btnChiTietLuong.Click += new System.EventHandler(this.btnChiTietLuong_Click);
            // 
            // btnDanhGia
            // 
            this.btnDanhGia.BackColor = System.Drawing.Color.Transparent;
            this.btnDanhGia.BorderRadius = 5;
            this.btnDanhGia.BorderThickness = 1;
            this.btnDanhGia.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDanhGia.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDanhGia.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDanhGia.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDanhGia.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDanhGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDanhGia.ForeColor = System.Drawing.Color.Black;
            this.btnDanhGia.Image = global::GUI.Properties.Resources.resume;
            this.btnDanhGia.ImageOffset = new System.Drawing.Point(0, 8);
            this.btnDanhGia.ImageSize = new System.Drawing.Size(30, 30);
            this.btnDanhGia.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDanhGia.IndicateFocus = true;
            this.btnDanhGia.Location = new System.Drawing.Point(182, 4);
            this.btnDanhGia.Margin = new System.Windows.Forms.Padding(4);
            this.btnDanhGia.Name = "btnDanhGia";
            this.btnDanhGia.Size = new System.Drawing.Size(170, 64);
            this.btnDanhGia.TabIndex = 4;
            this.btnDanhGia.Text = "Tuyển dụng";
            this.btnDanhGia.TextFormatNoPrefix = true;
            this.btnDanhGia.Click += new System.EventHandler(this.btnDanhGia_Click);
            // 
            // btnKyLuat
            // 
            this.btnKyLuat.BackColor = System.Drawing.Color.Transparent;
            this.btnKyLuat.BorderRadius = 5;
            this.btnKyLuat.BorderThickness = 1;
            this.btnKyLuat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKyLuat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKyLuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKyLuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKyLuat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnKyLuat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKyLuat.ForeColor = System.Drawing.Color.Black;
            this.btnKyLuat.Image = global::GUI.Properties.Resources.Discipline;
            this.btnKyLuat.ImageOffset = new System.Drawing.Point(0, 8);
            this.btnKyLuat.ImageSize = new System.Drawing.Size(30, 30);
            this.btnKyLuat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnKyLuat.IndicateFocus = true;
            this.btnKyLuat.Location = new System.Drawing.Point(360, 4);
            this.btnKyLuat.Margin = new System.Windows.Forms.Padding(4);
            this.btnKyLuat.Name = "btnKyLuat";
            this.btnKyLuat.Size = new System.Drawing.Size(170, 64);
            this.btnKyLuat.TabIndex = 5;
            this.btnKyLuat.Text = "Hợp đồng nhân viên";
            this.btnKyLuat.TextFormatNoPrefix = true;
            this.btnKyLuat.Click += new System.EventHandler(this.btnKyLuat_Click);
            // 
            // guna2TileButton4
            // 
            this.guna2TileButton4.BorderRadius = 5;
            this.guna2TileButton4.BorderThickness = 1;
            this.guna2TileButton4.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2TileButton4.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2TileButton4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2TileButton4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2TileButton4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.guna2TileButton4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TileButton4.ForeColor = System.Drawing.Color.Black;
            this.guna2TileButton4.Image = global::GUI.Properties.Resources.Permission;
            this.guna2TileButton4.ImageOffset = new System.Drawing.Point(0, 8);
            this.guna2TileButton4.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2TileButton4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.guna2TileButton4.IndicateFocus = true;
            this.guna2TileButton4.Location = new System.Drawing.Point(538, 4);
            this.guna2TileButton4.Margin = new System.Windows.Forms.Padding(4);
            this.guna2TileButton4.Name = "guna2TileButton4";
            this.guna2TileButton4.Size = new System.Drawing.Size(170, 64);
            this.guna2TileButton4.TabIndex = 9;
            this.guna2TileButton4.Text = "Khen thưởng";
            this.guna2TileButton4.TextFormatNoPrefix = true;
            this.guna2TileButton4.Click += new System.EventHandler(this.guna2TileButton4_Click);
            // 
            // guna2TileButton1
            // 
            this.guna2TileButton1.BorderRadius = 5;
            this.guna2TileButton1.BorderThickness = 1;
            this.guna2TileButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2TileButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2TileButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2TileButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2TileButton1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.guna2TileButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TileButton1.ForeColor = System.Drawing.Color.Black;
            this.guna2TileButton1.Image = global::GUI.Properties.Resources.Permission;
            this.guna2TileButton1.ImageOffset = new System.Drawing.Point(0, 8);
            this.guna2TileButton1.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2TileButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.guna2TileButton1.IndicateFocus = true;
            this.guna2TileButton1.Location = new System.Drawing.Point(716, 4);
            this.guna2TileButton1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2TileButton1.Name = "guna2TileButton1";
            this.guna2TileButton1.Size = new System.Drawing.Size(170, 64);
            this.guna2TileButton1.TabIndex = 10;
            this.guna2TileButton1.Text = "DS Lương";
            this.guna2TileButton1.TextFormatNoPrefix = true;
            this.guna2TileButton1.Click += new System.EventHandler(this.guna2TileButton1_Click);
            // 
            // guna2TileButton2
            // 
            this.guna2TileButton2.BorderRadius = 5;
            this.guna2TileButton2.BorderThickness = 1;
            this.guna2TileButton2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2TileButton2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2TileButton2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2TileButton2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2TileButton2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.guna2TileButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TileButton2.ForeColor = System.Drawing.Color.Black;
            this.guna2TileButton2.Image = global::GUI.Properties.Resources.Permission;
            this.guna2TileButton2.ImageOffset = new System.Drawing.Point(0, 8);
            this.guna2TileButton2.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2TileButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.guna2TileButton2.IndicateFocus = true;
            this.guna2TileButton2.Location = new System.Drawing.Point(894, 4);
            this.guna2TileButton2.Margin = new System.Windows.Forms.Padding(4);
            this.guna2TileButton2.Name = "guna2TileButton2";
            this.guna2TileButton2.Size = new System.Drawing.Size(170, 64);
            this.guna2TileButton2.TabIndex = 11;
            this.guna2TileButton2.Text = "DS Kỷ luật";
            this.guna2TileButton2.TextFormatNoPrefix = true;
            this.guna2TileButton2.Click += new System.EventHandler(this.guna2TileButton2_Click);
            // 
            // ButtonFeatureReportComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ButtonFeatureReportComponent";
            this.Size = new System.Drawing.Size(1422, 89);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2TileButton btnChiTietLuong;
        private Guna.UI2.WinForms.Guna2TileButton btnDanhGia;
        private Guna.UI2.WinForms.Guna2TileButton btnKyLuat;
        private Guna.UI2.WinForms.Guna2TileButton guna2TileButton4;
        private Guna.UI2.WinForms.Guna2TileButton guna2TileButton1;
        private Guna.UI2.WinForms.Guna2TileButton guna2TileButton2;
    }
}
