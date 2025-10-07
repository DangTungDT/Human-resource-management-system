namespace GUI
{
    partial class ButtonFeatureViewComponent
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
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnChiTietLuong);
            this.flowLayoutPanel1.Controls.Add(this.btnDanhGia);
            this.flowLayoutPanel1.Controls.Add(this.btnKyLuat);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(784, 62);
            this.flowLayoutPanel1.TabIndex = 4;
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
            this.btnChiTietLuong.ForeColor = System.Drawing.Color.White;
            this.btnChiTietLuong.Image = global::GUI.Properties.Resources.salary;
            this.btnChiTietLuong.ImageOffset = new System.Drawing.Point(0, 8);
            this.btnChiTietLuong.ImageSize = new System.Drawing.Size(30, 30);
            this.btnChiTietLuong.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChiTietLuong.IndicateFocus = true;
            this.btnChiTietLuong.Location = new System.Drawing.Point(3, 3);
            this.btnChiTietLuong.Name = "btnChiTietLuong";
            this.btnChiTietLuong.Size = new System.Drawing.Size(113, 52);
            this.btnChiTietLuong.TabIndex = 3;
            this.btnChiTietLuong.Text = "Lương cá nhân";
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
            this.btnDanhGia.ForeColor = System.Drawing.Color.White;
            this.btnDanhGia.Image = global::GUI.Properties.Resources.resume;
            this.btnDanhGia.ImageOffset = new System.Drawing.Point(0, 8);
            this.btnDanhGia.ImageSize = new System.Drawing.Size(30, 30);
            this.btnDanhGia.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDanhGia.IndicateFocus = true;
            this.btnDanhGia.Location = new System.Drawing.Point(122, 3);
            this.btnDanhGia.Name = "btnDanhGia";
            this.btnDanhGia.Size = new System.Drawing.Size(85, 52);
            this.btnDanhGia.TabIndex = 4;
            this.btnDanhGia.Text = "Đánh giá";
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
            this.btnKyLuat.ForeColor = System.Drawing.Color.White;
            this.btnKyLuat.Image = global::GUI.Properties.Resources.Discipline;
            this.btnKyLuat.ImageOffset = new System.Drawing.Point(0, 8);
            this.btnKyLuat.ImageSize = new System.Drawing.Size(30, 30);
            this.btnKyLuat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnKyLuat.IndicateFocus = true;
            this.btnKyLuat.Location = new System.Drawing.Point(213, 3);
            this.btnKyLuat.Name = "btnKyLuat";
            this.btnKyLuat.Size = new System.Drawing.Size(85, 52);
            this.btnKyLuat.TabIndex = 5;
            this.btnKyLuat.Text = "Kỷ luật";
            this.btnKyLuat.TextFormatNoPrefix = true;
            this.btnKyLuat.Click += new System.EventHandler(this.btnKyLuat_Click);
            // 
            // ButtonFeatureViewComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ButtonFeatureViewComponent";
            this.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Size = new System.Drawing.Size(794, 72);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2TileButton btnChiTietLuong;
        private Guna.UI2.WinForms.Guna2TileButton btnDanhGia;
        private Guna.UI2.WinForms.Guna2TileButton btnKyLuat;
    }
}
