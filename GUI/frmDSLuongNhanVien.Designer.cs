namespace GUI
{
    partial class frmDSLuongNhanVien
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGuiLuongNV = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaoBaoCao = new Guna.UI2.WinForms.Guna2Button();
            this.cmbNhanVienPB = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPhongBan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbMoHinh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbThang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbNam = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtTimNV = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.DocumentMapWidth = 6;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = null;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.rptHopDongLaoDong.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(404, 0);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(659, 766);
            this.reportViewer1.TabIndex = 62;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTimNV);
            this.panel1.Controls.Add(this.cmbNam);
            this.panel1.Controls.Add(this.cmbThang);
            this.panel1.Controls.Add(this.cmbMoHinh);
            this.panel1.Controls.Add(this.cmbPhongBan);
            this.panel1.Controls.Add(this.btnTaoBaoCao);
            this.panel1.Controls.Add(this.btnGuiLuongNV);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbNhanVienPB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 766);
            this.panel1.TabIndex = 63;
            // 
            // btnGuiLuongNV
            // 
            this.btnGuiLuongNV.BorderRadius = 8;
            this.btnGuiLuongNV.BorderThickness = 1;
            this.btnGuiLuongNV.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGuiLuongNV.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGuiLuongNV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGuiLuongNV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGuiLuongNV.FillColor = System.Drawing.Color.LightGray;
            this.btnGuiLuongNV.FocusedColor = System.Drawing.Color.Black;
            this.btnGuiLuongNV.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnGuiLuongNV.ForeColor = System.Drawing.Color.Black;
            this.btnGuiLuongNV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnGuiLuongNV.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnGuiLuongNV.Location = new System.Drawing.Point(14, 462);
            this.btnGuiLuongNV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGuiLuongNV.Name = "btnGuiLuongNV";
            this.btnGuiLuongNV.Size = new System.Drawing.Size(170, 46);
            this.btnGuiLuongNV.TabIndex = 58;
            this.btnGuiLuongNV.Text = "Gửi mail lương cho NV";
            this.btnGuiLuongNV.Visible = false;
            this.btnGuiLuongNV.Click += new System.EventHandler(this.btnGuiLuongNV_Click);
            // 
            // btnTaoBaoCao
            // 
            this.btnTaoBaoCao.BorderRadius = 8;
            this.btnTaoBaoCao.BorderThickness = 1;
            this.btnTaoBaoCao.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoBaoCao.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoBaoCao.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaoBaoCao.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaoBaoCao.FillColor = System.Drawing.Color.LightGray;
            this.btnTaoBaoCao.FocusedColor = System.Drawing.Color.Black;
            this.btnTaoBaoCao.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTaoBaoCao.ForeColor = System.Drawing.Color.Black;
            this.btnTaoBaoCao.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnTaoBaoCao.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnTaoBaoCao.Location = new System.Drawing.Point(210, 462);
            this.btnTaoBaoCao.Margin = new System.Windows.Forms.Padding(2);
            this.btnTaoBaoCao.Name = "btnTaoBaoCao";
            this.btnTaoBaoCao.Size = new System.Drawing.Size(170, 46);
            this.btnTaoBaoCao.TabIndex = 58;
            this.btnTaoBaoCao.Text = "Tạo báo cáo";
            // 
            // cmbNhanVienPB
            // 
            this.cmbNhanVienPB.BackColor = System.Drawing.Color.Transparent;
            this.cmbNhanVienPB.BorderRadius = 5;
            this.cmbNhanVienPB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNhanVienPB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNhanVienPB.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbNhanVienPB.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbNhanVienPB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNhanVienPB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbNhanVienPB.ItemHeight = 30;
            this.cmbNhanVienPB.Location = new System.Drawing.Point(116, 346);
            this.cmbNhanVienPB.Margin = new System.Windows.Forms.Padding(2);
            this.cmbNhanVienPB.Name = "cmbNhanVienPB";
            this.cmbNhanVienPB.Size = new System.Drawing.Size(265, 36);
            this.cmbNhanVienPB.TabIndex = 53;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(10, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 56;
            this.label4.Text = "Phòng ban:";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(10, 353);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 56;
            this.label5.Text = "Nhân viên:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(10, 403);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 56;
            this.label2.Text = "Tháng áp dụng:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(10, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 56;
            this.label1.Text = "Mô hình:";
            // 
            // cmbPhongBan
            // 
            this.cmbPhongBan.BackColor = System.Drawing.Color.Transparent;
            this.cmbPhongBan.BorderRadius = 5;
            this.cmbPhongBan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPhongBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPhongBan.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbPhongBan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbPhongBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPhongBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbPhongBan.ItemHeight = 30;
            this.cmbPhongBan.Location = new System.Drawing.Point(116, 297);
            this.cmbPhongBan.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPhongBan.Name = "cmbPhongBan";
            this.cmbPhongBan.Size = new System.Drawing.Size(265, 36);
            this.cmbPhongBan.TabIndex = 52;
            // 
            // cmbMoHinh
            // 
            this.cmbMoHinh.BackColor = System.Drawing.Color.Transparent;
            this.cmbMoHinh.BorderRadius = 5;
            this.cmbMoHinh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMoHinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMoHinh.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbMoHinh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbMoHinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMoHinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbMoHinh.ItemHeight = 30;
            this.cmbMoHinh.Location = new System.Drawing.Point(116, 248);
            this.cmbMoHinh.Margin = new System.Windows.Forms.Padding(2);
            this.cmbMoHinh.Name = "cmbMoHinh";
            this.cmbMoHinh.Size = new System.Drawing.Size(265, 36);
            this.cmbMoHinh.TabIndex = 52;
            // 
            // cmbThang
            // 
            this.cmbThang.BackColor = System.Drawing.Color.Transparent;
            this.cmbThang.BorderRadius = 5;
            this.cmbThang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThang.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbThang.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbThang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbThang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbThang.ItemHeight = 30;
            this.cmbThang.Location = new System.Drawing.Point(116, 396);
            this.cmbThang.Margin = new System.Windows.Forms.Padding(2);
            this.cmbThang.Name = "cmbThang";
            this.cmbThang.Size = new System.Drawing.Size(122, 36);
            this.cmbThang.TabIndex = 52;
            this.cmbThang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cmbThang.SelectionChangeCommitted += new System.EventHandler(this.cmbThang_SelectionChangeCommitted);
            // 
            // cmbNam
            // 
            this.cmbNam.BackColor = System.Drawing.Color.Transparent;
            this.cmbNam.BorderRadius = 5;
            this.cmbNam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNam.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbNam.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbNam.ItemHeight = 30;
            this.cmbNam.Location = new System.Drawing.Point(258, 396);
            this.cmbNam.Margin = new System.Windows.Forms.Padding(2);
            this.cmbNam.Name = "cmbNam";
            this.cmbNam.Size = new System.Drawing.Size(122, 36);
            this.cmbNam.TabIndex = 52;
            this.cmbNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cmbNam.SelectionChangeCommitted += new System.EventHandler(this.cmbNam_SelectionChangeCommitted);
            // 
            // txtTimNV
            // 
            this.txtTimNV.BackColor = System.Drawing.Color.Transparent;
            this.txtTimNV.BorderRadius = 7;
            this.txtTimNV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimNV.DefaultText = "";
            this.txtTimNV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimNV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimNV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimNV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimNV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimNV.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimNV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimNV.Location = new System.Drawing.Point(116, 342);
            this.txtTimNV.Margin = new System.Windows.Forms.Padding(0);
            this.txtTimNV.Name = "txtTimNV";
            this.txtTimNV.PlaceholderText = "";
            this.txtTimNV.SelectedText = "";
            this.txtTimNV.Size = new System.Drawing.Size(264, 39);
            this.txtTimNV.TabIndex = 60;
            this.txtTimNV.Visible = false;
            // 
            // frmDSLuongNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 766);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmDSLuongNhanVien";
            this.Text = "frmDSLuongNhanVien";
            this.Load += new System.EventHandler(this.frmDSLuongNhanVien_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button btnGuiLuongNV;
        private Guna.UI2.WinForms.Guna2TextBox txtTimNV;
        private Guna.UI2.WinForms.Guna2ComboBox cmbNam;
        private Guna.UI2.WinForms.Guna2ComboBox cmbThang;
        private Guna.UI2.WinForms.Guna2ComboBox cmbMoHinh;
        private Guna.UI2.WinForms.Guna2ComboBox cmbPhongBan;
        private Guna.UI2.WinForms.Guna2Button btnTaoBaoCao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2ComboBox cmbNhanVienPB;
    }
}