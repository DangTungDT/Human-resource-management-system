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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnChamCongRaTatCa = new Guna.UI2.WinForms.Guna2Button();
            this.btnChamCongVaoTatCa = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnFilterEmployees = new Guna.UI2.WinForms.Guna2Button();
            this.rdoNotCheckedIn = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdoCheckedIn = new Guna.UI2.WinForms.Guna2RadioButton();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEmployeeName = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvEmployeeAttendance = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2Panel1.SuspendLayout();
            this.guna2GroupBox2.SuspendLayout();
            this.guna2GroupBox1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeAttendance)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.SystemColors.MenuBar;
            this.guna2Panel1.Controls.Add(this.guna2GroupBox2);
            this.guna2Panel1.Controls.Add(this.guna2GroupBox1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(289, 875);
            this.guna2Panel1.TabIndex = 0;
            // 
            // guna2GroupBox2
            // 
            this.guna2GroupBox2.Controls.Add(this.btnChamCongRaTatCa);
            this.guna2GroupBox2.Controls.Add(this.btnChamCongVaoTatCa);
            this.guna2GroupBox2.Controls.Add(this.guna2Button1);
            this.guna2GroupBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2GroupBox2.Location = new System.Drawing.Point(18, 21);
            this.guna2GroupBox2.Name = "guna2GroupBox2";
            this.guna2GroupBox2.Size = new System.Drawing.Size(253, 319);
            this.guna2GroupBox2.TabIndex = 0;
            this.guna2GroupBox2.Text = "Chấm công";
            // 
            // btnChamCongRaTatCa
            // 
            this.btnChamCongRaTatCa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChamCongRaTatCa.BorderRadius = 7;
            this.btnChamCongRaTatCa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChamCongRaTatCa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChamCongRaTatCa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChamCongRaTatCa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChamCongRaTatCa.FillColor = System.Drawing.Color.LightGray;
            this.btnChamCongRaTatCa.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamCongRaTatCa.ForeColor = System.Drawing.Color.Black;
            this.btnChamCongRaTatCa.Location = new System.Drawing.Point(12, 118);
            this.btnChamCongRaTatCa.Name = "btnChamCongRaTatCa";
            this.btnChamCongRaTatCa.Size = new System.Drawing.Size(229, 45);
            this.btnChamCongRaTatCa.TabIndex = 10;
            this.btnChamCongRaTatCa.Text = "Chấm công ra tất cả";
            this.btnChamCongRaTatCa.Click += new System.EventHandler(this.btnChamCongRaTatCa_Click);
            // 
            // btnChamCongVaoTatCa
            // 
            this.btnChamCongVaoTatCa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChamCongVaoTatCa.BorderRadius = 7;
            this.btnChamCongVaoTatCa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChamCongVaoTatCa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChamCongVaoTatCa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChamCongVaoTatCa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChamCongVaoTatCa.FillColor = System.Drawing.Color.LightGray;
            this.btnChamCongVaoTatCa.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamCongVaoTatCa.ForeColor = System.Drawing.Color.Black;
            this.btnChamCongVaoTatCa.Location = new System.Drawing.Point(12, 53);
            this.btnChamCongVaoTatCa.Name = "btnChamCongVaoTatCa";
            this.btnChamCongVaoTatCa.Size = new System.Drawing.Size(229, 45);
            this.btnChamCongVaoTatCa.TabIndex = 10;
            this.btnChamCongVaoTatCa.Text = "Chấm công vào tất cả";
            this.btnChamCongVaoTatCa.Click += new System.EventHandler(this.btnChamCongVaoTatCa_Click);
            // 
            // guna2Button1
            // 
            this.guna2Button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Button1.BorderRadius = 7;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.LightGray;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.Black;
            this.guna2Button1.Location = new System.Drawing.Point(21, -81);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(229, 45);
            this.guna2Button1.TabIndex = 10;
            this.guna2Button1.Text = "Lọc nhân viên";
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.Controls.Add(this.btnFilterEmployees);
            this.guna2GroupBox1.Controls.Add(this.rdoNotCheckedIn);
            this.guna2GroupBox1.Controls.Add(this.rdoCheckedIn);
            this.guna2GroupBox1.Controls.Add(this.txtEmail);
            this.guna2GroupBox1.Controls.Add(this.txtEmployeeName);
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2GroupBox1.Location = new System.Drawing.Point(18, 346);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(253, 443);
            this.guna2GroupBox1.TabIndex = 0;
            this.guna2GroupBox1.Text = "Tìm kiếm ";
            // 
            // btnFilterEmployees
            // 
            this.btnFilterEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterEmployees.BorderRadius = 7;
            this.btnFilterEmployees.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFilterEmployees.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFilterEmployees.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFilterEmployees.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFilterEmployees.FillColor = System.Drawing.Color.LightGray;
            this.btnFilterEmployees.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterEmployees.ForeColor = System.Drawing.Color.Black;
            this.btnFilterEmployees.Location = new System.Drawing.Point(12, 378);
            this.btnFilterEmployees.Name = "btnFilterEmployees";
            this.btnFilterEmployees.Size = new System.Drawing.Size(229, 45);
            this.btnFilterEmployees.TabIndex = 10;
            this.btnFilterEmployees.Text = "Lọc nhân viên";
            this.btnFilterEmployees.Click += new System.EventHandler(this.btnFilterEmployees_Click);
            // 
            // rdoNotCheckedIn
            // 
            this.rdoNotCheckedIn.AutoSize = true;
            this.rdoNotCheckedIn.BackColor = System.Drawing.Color.Transparent;
            this.rdoNotCheckedIn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdoNotCheckedIn.CheckedState.BorderThickness = 0;
            this.rdoNotCheckedIn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdoNotCheckedIn.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdoNotCheckedIn.CheckedState.InnerOffset = -4;
            this.rdoNotCheckedIn.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNotCheckedIn.ForeColor = System.Drawing.Color.Black;
            this.rdoNotCheckedIn.Location = new System.Drawing.Point(12, 217);
            this.rdoNotCheckedIn.Name = "rdoNotCheckedIn";
            this.rdoNotCheckedIn.Size = new System.Drawing.Size(169, 23);
            this.rdoNotCheckedIn.TabIndex = 9;
            this.rdoNotCheckedIn.Text = "Chưa chấm công vào";
            this.rdoNotCheckedIn.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdoNotCheckedIn.UncheckedState.BorderThickness = 2;
            this.rdoNotCheckedIn.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdoNotCheckedIn.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.rdoNotCheckedIn.UseVisualStyleBackColor = false;
            // 
            // rdoCheckedIn
            // 
            this.rdoCheckedIn.AutoSize = true;
            this.rdoCheckedIn.BackColor = System.Drawing.Color.Transparent;
            this.rdoCheckedIn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdoCheckedIn.CheckedState.BorderThickness = 0;
            this.rdoCheckedIn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdoCheckedIn.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdoCheckedIn.CheckedState.InnerOffset = -4;
            this.rdoCheckedIn.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckedIn.ForeColor = System.Drawing.Color.Black;
            this.rdoCheckedIn.Location = new System.Drawing.Point(12, 176);
            this.rdoCheckedIn.Name = "rdoCheckedIn";
            this.rdoCheckedIn.Size = new System.Drawing.Size(153, 23);
            this.rdoCheckedIn.TabIndex = 8;
            this.rdoCheckedIn.Text = "Đã chấm công vào";
            this.rdoCheckedIn.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdoCheckedIn.UncheckedState.BorderThickness = 2;
            this.rdoCheckedIn.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdoCheckedIn.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.rdoCheckedIn.UseVisualStyleBackColor = false;
            // 
            // txtEmail
            // 
            this.txtEmail.BorderRadius = 7;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Location = new System.Drawing.Point(12, 118);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Email nhân viên";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(229, 36);
            this.txtEmail.TabIndex = 6;
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.BorderRadius = 7;
            this.txtEmployeeName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmployeeName.DefaultText = "";
            this.txtEmployeeName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEmployeeName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEmployeeName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmployeeName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmployeeName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmployeeName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmployeeName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmployeeName.Location = new System.Drawing.Point(12, 63);
            this.txtEmployeeName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmployeeName.MaxLength = 255;
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.PlaceholderText = "Tên nhân viên";
            this.txtEmployeeName.SelectedText = "";
            this.txtEmployeeName.Size = new System.Drawing.Size(229, 36);
            this.txtEmployeeName.TabIndex = 7;
            this.txtEmployeeName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmployeeName_KeyPress);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.dgvEmployeeAttendance);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel2.Location = new System.Drawing.Point(289, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1173, 875);
            this.guna2Panel2.TabIndex = 1;
            // 
            // dgvEmployeeAttendance
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvEmployeeAttendance.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmployeeAttendance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEmployeeAttendance.ColumnHeadersHeight = 4;
            this.dgvEmployeeAttendance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEmployeeAttendance.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvEmployeeAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployeeAttendance.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvEmployeeAttendance.Location = new System.Drawing.Point(0, 0);
            this.dgvEmployeeAttendance.MultiSelect = false;
            this.dgvEmployeeAttendance.Name = "dgvEmployeeAttendance";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmployeeAttendance.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvEmployeeAttendance.RowHeadersVisible = false;
            this.dgvEmployeeAttendance.RowHeadersWidth = 51;
            this.dgvEmployeeAttendance.RowTemplate.Height = 24;
            this.dgvEmployeeAttendance.Size = new System.Drawing.Size(1173, 875);
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
            this.dgvEmployeeAttendance.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployeeAttendance_CellClick);
            this.dgvEmployeeAttendance.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployeeAttendance_CellContentClick);
            this.dgvEmployeeAttendance.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployeeAttendance_CellDoubleClick);
            // 
            // ucChamCongQuanLy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "ucChamCongQuanLy";
            this.Size = new System.Drawing.Size(1462, 875);
            this.Load += new System.EventHandler(this.ucChamCongQuanLy_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2GroupBox2.ResumeLayout(false);
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeAttendance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2DataGridView dgvEmployeeAttendance;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private Guna.UI2.WinForms.Guna2Button btnFilterEmployees;
        private Guna.UI2.WinForms.Guna2RadioButton rdoNotCheckedIn;
        private Guna.UI2.WinForms.Guna2RadioButton rdoCheckedIn;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtEmployeeName;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox2;
        private Guna.UI2.WinForms.Guna2Button btnChamCongRaTatCa;
        private Guna.UI2.WinForms.Guna2Button btnChamCongVaoTatCa;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}
