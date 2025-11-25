namespace GUI
{
    partial class frmBaoCaoHopDong
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnTaoBaoCao = new Guna.UI2.WinForms.Guna2Button();
            this.cmbPhongBan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbLoaiHopDong = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbNhanVienPB = new Guna.UI2.WinForms.Guna2ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.DocumentMapWidth = 6;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = null;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.rptHopDongLaoDong.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(404, 0);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(659, 763);
            this.reportViewer1.TabIndex = 49;
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
            this.btnTaoBaoCao.Location = new System.Drawing.Point(66, 439);
            this.btnTaoBaoCao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTaoBaoCao.Name = "btnTaoBaoCao";
            this.btnTaoBaoCao.Size = new System.Drawing.Size(267, 46);
            this.btnTaoBaoCao.TabIndex = 58;
            this.btnTaoBaoCao.Text = "Tạo báo cáo";
            this.btnTaoBaoCao.Click += new System.EventHandler(this.btnTaoBaoCao_Click);
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
            this.cmbPhongBan.Location = new System.Drawing.Point(118, 253);
            this.cmbPhongBan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbPhongBan.Name = "cmbPhongBan";
            this.cmbPhongBan.Size = new System.Drawing.Size(265, 36);
            this.cmbPhongBan.TabIndex = 52;
            this.cmbPhongBan.SelectionChangeCommitted += new System.EventHandler(this.cmbPhongBan_SelectionChangeCommitted);
            // 
            // cmbLoaiHopDong
            // 
            this.cmbLoaiHopDong.BackColor = System.Drawing.Color.Transparent;
            this.cmbLoaiHopDong.BorderRadius = 5;
            this.cmbLoaiHopDong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLoaiHopDong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoaiHopDong.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbLoaiHopDong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbLoaiHopDong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbLoaiHopDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbLoaiHopDong.ItemHeight = 30;
            this.cmbLoaiHopDong.Location = new System.Drawing.Point(118, 298);
            this.cmbLoaiHopDong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbLoaiHopDong.Name = "cmbLoaiHopDong";
            this.cmbLoaiHopDong.Size = new System.Drawing.Size(265, 36);
            this.cmbLoaiHopDong.TabIndex = 53;
            this.cmbLoaiHopDong.SelectionChangeCommitted += new System.EventHandler(this.cmbLoaiHopDong_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(14, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 56;
            this.label4.Text = "Phòng ban:";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(14, 308);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 57;
            this.label3.Text = "Loại hợp đồng:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbPhongBan);
            this.panel1.Controls.Add(this.btnTaoBaoCao);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbNhanVienPB);
            this.panel1.Controls.Add(this.cmbLoaiHopDong);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 763);
            this.panel1.TabIndex = 59;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(14, 355);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 56;
            this.label5.Text = "Nhân viên:";
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
            this.cmbNhanVienPB.Location = new System.Drawing.Point(118, 349);
            this.cmbNhanVienPB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbNhanVienPB.Name = "cmbNhanVienPB";
            this.cmbNhanVienPB.Size = new System.Drawing.Size(265, 36);
            this.cmbNhanVienPB.TabIndex = 53;
            this.cmbNhanVienPB.SelectionChangeCommitted += new System.EventHandler(this.cmbNhanVienPB_SelectionChangeCommitted);
            // 
            // frmBaoCaoHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 763);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmBaoCaoHopDong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBaoCaoHopDong";
            this.Load += new System.EventHandler(this.frmBaoCaoHopDong_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Guna.UI2.WinForms.Guna2Button btnTaoBaoCao;
        private Guna.UI2.WinForms.Guna2ComboBox cmbPhongBan;
        private Guna.UI2.WinForms.Guna2ComboBox cmbLoaiHopDong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2ComboBox cmbNhanVienPB;
    }
}