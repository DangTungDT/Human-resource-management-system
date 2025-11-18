namespace GUI
{
    partial class FormBaoCaoChamCong
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
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.btnIn = new Guna.UI2.WinForms.Guna2Button();
            this.cmbThang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cmbNam = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbPhongBan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.crystalReportViewerChamCong = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.crpChamCong1 = new GUI.report.crpChamCong();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Controls.Add(this.label1);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2HtmlLabel2);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2CustomGradientPanel1.Controls.Add(this.cmbPhongBan);
            this.guna2CustomGradientPanel1.Controls.Add(this.cmbNam);
            this.guna2CustomGradientPanel1.Controls.Add(this.cmbThang);
            this.guna2CustomGradientPanel1.Controls.Add(this.btnIn);
            this.guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(1301, 93);
            this.guna2CustomGradientPanel1.TabIndex = 0;
            // 
            // btnIn
            // 
            this.btnIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIn.BorderRadius = 7;
            this.btnIn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnIn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnIn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnIn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnIn.FillColor = System.Drawing.Color.LightGray;
            this.btnIn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIn.ForeColor = System.Drawing.Color.Black;
            this.btnIn.Location = new System.Drawing.Point(1109, 21);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(180, 45);
            this.btnIn.TabIndex = 0;
            this.btnIn.Text = "In báo cáo";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // cmbThang
            // 
            this.cmbThang.BackColor = System.Drawing.Color.Transparent;
            this.cmbThang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThang.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbThang.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbThang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbThang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbThang.ItemHeight = 30;
            this.cmbThang.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbThang.Location = new System.Drawing.Point(79, 21);
            this.cmbThang.Name = "cmbThang";
            this.cmbThang.Size = new System.Drawing.Size(198, 36);
            this.cmbThang.TabIndex = 1;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(31, 31);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(42, 18);
            this.guna2HtmlLabel1.TabIndex = 2;
            this.guna2HtmlLabel1.Text = "Tháng";
            this.guna2HtmlLabel1.Click += new System.EventHandler(this.guna2HtmlLabel1_Click);
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(326, 31);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(32, 18);
            this.guna2HtmlLabel2.TabIndex = 2;
            this.guna2HtmlLabel2.Text = "Năm";
            this.guna2HtmlLabel2.Click += new System.EventHandler(this.guna2HtmlLabel1_Click);
            // 
            // cmbNam
            // 
            this.cmbNam.BackColor = System.Drawing.Color.Transparent;
            this.cmbNam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNam.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbNam.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbNam.ItemHeight = 30;
            this.cmbNam.Items.AddRange(new object[] {
            "2024",
            "2025"});
            this.cmbNam.Location = new System.Drawing.Point(364, 21);
            this.cmbNam.Name = "cmbNam";
            this.cmbNam.Size = new System.Drawing.Size(198, 36);
            this.cmbNam.TabIndex = 1;
            // 
            // cmbPhongBan
            // 
            this.cmbPhongBan.BackColor = System.Drawing.Color.Transparent;
            this.cmbPhongBan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPhongBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPhongBan.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbPhongBan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbPhongBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPhongBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbPhongBan.ItemHeight = 30;
            this.cmbPhongBan.Location = new System.Drawing.Point(727, 21);
            this.cmbPhongBan.Name = "cmbPhongBan";
            this.cmbPhongBan.Size = new System.Drawing.Size(260, 36);
            this.cmbPhongBan.TabIndex = 1;
            // 
            // crystalReportViewerChamCong
            // 
            this.crystalReportViewerChamCong.ActiveViewIndex = 0;
            this.crystalReportViewerChamCong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerChamCong.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerChamCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerChamCong.Location = new System.Drawing.Point(0, 93);
            this.crystalReportViewerChamCong.Name = "crystalReportViewerChamCong";
            this.crystalReportViewerChamCong.ReportSource = this.crpChamCong1;
            this.crystalReportViewerChamCong.Size = new System.Drawing.Size(1301, 357);
            this.crystalReportViewerChamCong.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(649, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Phòng ban";
            // 
            // FormBaoCaoChamCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 450);
            this.Controls.Add(this.crystalReportViewerChamCong);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Name = "FormBaoCaoChamCong";
            this.Text = "Form báo cáo chấm công";
            this.Load += new System.EventHandler(this.FormBaoCaoChamCong_Load);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2Button btnIn;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbThang;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2ComboBox cmbPhongBan;
        private Guna.UI2.WinForms.Guna2ComboBox cmbNam;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerChamCong;
        private report.crpChamCong crpChamCong1;
        private System.Windows.Forms.Label label1;
    }
}