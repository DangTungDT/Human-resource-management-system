namespace GUI
{
    partial class ucChamCongQuanLy
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnFilterEmployees = new Guna.UI2.WinForms.Guna2Button();
            this.rdoNotCheckedIn = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdoCheckedIn = new Guna.UI2.WinForms.Guna2RadioButton();
            this.txtPhoneNumber = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEmployeeName = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvEmployeeAttendance = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeAttendance)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.btnFilterEmployees);
            this.guna2Panel1.Controls.Add(this.rdoNotCheckedIn);
            this.guna2Panel1.Controls.Add(this.rdoCheckedIn);
            this.guna2Panel1.Controls.Add(this.txtPhoneNumber);
            this.guna2Panel1.Controls.Add(this.txtEmail);
            this.guna2Panel1.Controls.Add(this.txtEmployeeName);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(289, 562);
            this.guna2Panel1.TabIndex = 0;
            // 
            // btnFilterEmployees
            // 
            this.btnFilterEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterEmployees.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFilterEmployees.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFilterEmployees.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFilterEmployees.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFilterEmployees.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilterEmployees.ForeColor = System.Drawing.Color.White;
            this.btnFilterEmployees.Location = new System.Drawing.Point(56, 486);
            this.btnFilterEmployees.Name = "btnFilterEmployees";
            this.btnFilterEmployees.Size = new System.Drawing.Size(180, 45);
            this.btnFilterEmployees.TabIndex = 4;
            this.btnFilterEmployees.Text = "Lọc nhân viên";
            // 
            // rdoNotCheckedIn
            // 
            this.rdoNotCheckedIn.AutoSize = true;
            this.rdoNotCheckedIn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdoNotCheckedIn.CheckedState.BorderThickness = 0;
            this.rdoNotCheckedIn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdoNotCheckedIn.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdoNotCheckedIn.CheckedState.InnerOffset = -4;
            this.rdoNotCheckedIn.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNotCheckedIn.Location = new System.Drawing.Point(29, 267);
            this.rdoNotCheckedIn.Name = "rdoNotCheckedIn";
            this.rdoNotCheckedIn.Size = new System.Drawing.Size(142, 23);
            this.rdoNotCheckedIn.TabIndex = 3;
            this.rdoNotCheckedIn.Text = "Chưa chấm công";
            this.rdoNotCheckedIn.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdoNotCheckedIn.UncheckedState.BorderThickness = 2;
            this.rdoNotCheckedIn.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdoNotCheckedIn.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // rdoCheckedIn
            // 
            this.rdoCheckedIn.AutoSize = true;
            this.rdoCheckedIn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdoCheckedIn.CheckedState.BorderThickness = 0;
            this.rdoCheckedIn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdoCheckedIn.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdoCheckedIn.CheckedState.InnerOffset = -4;
            this.rdoCheckedIn.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckedIn.Location = new System.Drawing.Point(29, 226);
            this.rdoCheckedIn.Name = "rdoCheckedIn";
            this.rdoCheckedIn.Size = new System.Drawing.Size(126, 23);
            this.rdoCheckedIn.TabIndex = 2;
            this.rdoCheckedIn.Text = "Đã chấm công";
            this.rdoCheckedIn.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdoCheckedIn.UncheckedState.BorderThickness = 2;
            this.rdoCheckedIn.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdoCheckedIn.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPhoneNumber.DefaultText = "";
            this.txtPhoneNumber.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPhoneNumber.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPhoneNumber.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPhoneNumber.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPhoneNumber.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPhoneNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPhoneNumber.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPhoneNumber.Location = new System.Drawing.Point(29, 170);
            this.txtPhoneNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.PlaceholderText = "";
            this.txtPhoneNumber.SelectedText = "";
            this.txtPhoneNumber.Size = new System.Drawing.Size(229, 36);
            this.txtPhoneNumber.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Location = new System.Drawing.Point(29, 113);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(229, 36);
            this.txtEmail.TabIndex = 1;
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmployeeName.DefaultText = "";
            this.txtEmployeeName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEmployeeName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEmployeeName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmployeeName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmployeeName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmployeeName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmployeeName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmployeeName.Location = new System.Drawing.Point(29, 58);
            this.txtEmployeeName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.PlaceholderText = "";
            this.txtEmployeeName.SelectedText = "";
            this.txtEmployeeName.Size = new System.Drawing.Size(229, 36);
            this.txtEmployeeName.TabIndex = 1;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(0, 0);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(289, 51);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "Tìm kiếm";
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.dgvEmployeeAttendance);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel2.Location = new System.Drawing.Point(289, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1041, 562);
            this.guna2Panel2.TabIndex = 1;
            // 
            // dgvEmployeeAttendance
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dgvEmployeeAttendance.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmployeeAttendance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvEmployeeAttendance.ColumnHeadersHeight = 4;
            this.dgvEmployeeAttendance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEmployeeAttendance.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvEmployeeAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployeeAttendance.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvEmployeeAttendance.Location = new System.Drawing.Point(0, 0);
            this.dgvEmployeeAttendance.MultiSelect = false;
            this.dgvEmployeeAttendance.Name = "dgvEmployeeAttendance";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmployeeAttendance.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvEmployeeAttendance.RowHeadersVisible = false;
            this.dgvEmployeeAttendance.RowHeadersWidth = 51;
            this.dgvEmployeeAttendance.RowTemplate.Height = 24;
            this.dgvEmployeeAttendance.Size = new System.Drawing.Size(1041, 562);
            this.dgvEmployeeAttendance.TabIndex = 0;
            this.dgvEmployeeAttendance.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvEmployeeAttendance.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvEmployeeAttendance.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvEmployeeAttendance.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvEmployeeAttendance.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvEmployeeAttendance.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvEmployeeAttendance.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvEmployeeAttendance.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvEmployeeAttendance.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvEmployeeAttendance.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvEmployeeAttendance.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvEmployeeAttendance.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvEmployeeAttendance.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvEmployeeAttendance.ThemeStyle.ReadOnly = false;
            this.dgvEmployeeAttendance.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvEmployeeAttendance.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvEmployeeAttendance.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvEmployeeAttendance.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvEmployeeAttendance.ThemeStyle.RowsStyle.Height = 24;
            this.dgvEmployeeAttendance.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvEmployeeAttendance.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // ucChamCongQuanLy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "ucChamCongQuanLy";
            this.Size = new System.Drawing.Size(1330, 562);
            this.Load += new System.EventHandler(this.ucChamCongQuanLy_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeAttendance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2RadioButton rdoNotCheckedIn;
        private Guna.UI2.WinForms.Guna2RadioButton rdoCheckedIn;
        private Guna.UI2.WinForms.Guna2TextBox txtEmployeeName;
        private Guna.UI2.WinForms.Guna2TextBox txtPhoneNumber;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2Button btnFilterEmployees;
        private Guna.UI2.WinForms.Guna2DataGridView dgvEmployeeAttendance;
    }
}
