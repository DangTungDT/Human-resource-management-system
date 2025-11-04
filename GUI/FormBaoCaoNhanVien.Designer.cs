namespace GUI
{
    partial class FormBaoCaoNhanVien
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2ComboBox cbPhongBan;
        private Guna.UI2.WinForms.Guna2Button btnXemBaoCao;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvBaoCao;
        private System.Windows.Forms.Label lblPhongBan;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbPhongBan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnXemBaoCao = new Guna.UI2.WinForms.Guna2Button();
            this.crvBaoCao = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.lblPhongBan = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbPhongBan
            // 
            this.cbPhongBan.BackColor = System.Drawing.Color.Transparent;
            this.cbPhongBan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPhongBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPhongBan.FocusedColor = System.Drawing.Color.Empty;
            this.cbPhongBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbPhongBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbPhongBan.FormattingEnabled = true;
            this.cbPhongBan.ItemHeight = 30;
            this.cbPhongBan.Location = new System.Drawing.Point(100, 30);
            this.cbPhongBan.Name = "cbPhongBan";
            this.cbPhongBan.Size = new System.Drawing.Size(291, 36);
            this.cbPhongBan.TabIndex = 3;
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXemBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCao.Location = new System.Drawing.Point(397, 30);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(225, 35);
            this.btnXemBaoCao.TabIndex = 4;
            this.btnXemBaoCao.Text = "Xem báo cáo";
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // crvBaoCao
            // 
            this.crvBaoCao.ActiveViewIndex = -1;
            this.crvBaoCao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvBaoCao.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvBaoCao.Location = new System.Drawing.Point(0, 90);
            this.crvBaoCao.Name = "crvBaoCao";
            this.crvBaoCao.Size = new System.Drawing.Size(1000, 600);
            this.crvBaoCao.TabIndex = 5;
            this.crvBaoCao.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // lblPhongBan
            // 
            this.lblPhongBan.AutoSize = true;
            this.lblPhongBan.Location = new System.Drawing.Point(20, 35);
            this.lblPhongBan.Name = "lblPhongBan";
            this.lblPhongBan.Size = new System.Drawing.Size(75, 16);
            this.lblPhongBan.TabIndex = 2;
            this.lblPhongBan.Text = "Phòng ban:";
            // 
            // FormBaoCaoNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.crvBaoCao);
            this.Controls.Add(this.btnXemBaoCao);
            this.Controls.Add(this.cbPhongBan);
            this.Controls.Add(this.lblPhongBan);
            this.Name = "FormBaoCaoNhanVien";
            this.Text = "Báo cáo danh sách nhân viên";
            this.Load += new System.EventHandler(this.FormBaoCaoNhanVien_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
