namespace GUI
{
    partial class UCHopDong
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.grbFilter = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblFindType = new System.Windows.Forms.Label();
            this.cmbFindType = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblFindEmployee = new System.Windows.Forms.Label();
            this.txtFindEmployee = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnFilter = new Guna.UI2.WinForms.Guna2Button();
            this.btnShowAll = new Guna.UI2.WinForms.Guna2Button();
            this.grbActions = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnCreate = new Guna.UI2.WinForms.Guna2Button();
            this.btnEdit = new Guna.UI2.WinForms.Guna2Button();
            this.btnDelete = new Guna.UI2.WinForms.Guna2Button();
            this.headerLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvHopDong = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblContractType = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cmbContractType = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblNgayKy = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dtpNgayKy = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblNgayBatDau = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dtpNgayBatDau = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblNgayKetThuc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dtpNgayKetThuc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblLuong = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtLuong = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNhanVien = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cmbNhanVien = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblMoTa = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblHinhAnh = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnSelectImage = new Guna.UI2.WinForms.Guna2Button();
            this.ptbHinhHopDong = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pnlLeft.SuspendLayout();
            this.grbFilter.SuspendLayout();
            this.grbActions.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHopDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbHinhHopDong)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.SystemColors.MenuBar;
            this.pnlLeft.Controls.Add(this.grbFilter);
            this.pnlLeft.Controls.Add(this.grbActions);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 62);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(224, 768);
            this.pnlLeft.TabIndex = 0;
            // 
            // grbFilter
            // 
            this.grbFilter.BorderRadius = 10;
            this.grbFilter.Controls.Add(this.lblFindType);
            this.grbFilter.Controls.Add(this.cmbFindType);
            this.grbFilter.Controls.Add(this.lblFindEmployee);
            this.grbFilter.Controls.Add(this.txtFindEmployee);
            this.grbFilter.Controls.Add(this.btnFilter);
            this.grbFilter.Controls.Add(this.btnShowAll);
            this.grbFilter.FillColor = System.Drawing.SystemColors.MenuBar;
            this.grbFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grbFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.grbFilter.Location = new System.Drawing.Point(16, 220);
            this.grbFilter.Name = "grbFilter";
            this.grbFilter.Size = new System.Drawing.Size(195, 422);
            this.grbFilter.TabIndex = 2;
            this.grbFilter.Text = "Lọc - Tìm kiếm hợp đồng";
            // 
            // lblFindType
            // 
            this.lblFindType.AutoSize = true;
            this.lblFindType.Location = new System.Drawing.Point(12, 40);
            this.lblFindType.Name = "lblFindType";
            this.lblFindType.Size = new System.Drawing.Size(106, 20);
            this.lblFindType.TabIndex = 0;
            this.lblFindType.Text = "Loại hợp đồng";
            // 
            // cmbFindType
            // 
            this.cmbFindType.BackColor = System.Drawing.Color.Transparent;
            this.cmbFindType.BorderRadius = 7;
            this.cmbFindType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFindType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFindType.FocusedColor = System.Drawing.Color.Empty;
            this.cmbFindType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbFindType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbFindType.ItemHeight = 30;
            this.cmbFindType.Items.AddRange(new object[] {
            "",
            "xác định thời hạn",
            "không xác định thời hạn"});
            this.cmbFindType.Location = new System.Drawing.Point(12, 60);
            this.cmbFindType.Name = "cmbFindType";
            this.cmbFindType.Size = new System.Drawing.Size(173, 36);
            this.cmbFindType.TabIndex = 1;
            // 
            // lblFindEmployee
            // 
            this.lblFindEmployee.AutoSize = true;
            this.lblFindEmployee.Location = new System.Drawing.Point(12, 108);
            this.lblFindEmployee.Name = "lblFindEmployee";
            this.lblFindEmployee.Size = new System.Drawing.Size(56, 20);
            this.lblFindEmployee.TabIndex = 2;
            this.lblFindEmployee.Text = "Tên NV";
            // 
            // txtFindEmployee
            // 
            this.txtFindEmployee.BorderRadius = 7;
            this.txtFindEmployee.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFindEmployee.DefaultText = "";
            this.txtFindEmployee.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFindEmployee.Location = new System.Drawing.Point(12, 128);
            this.txtFindEmployee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFindEmployee.Name = "txtFindEmployee";
            this.txtFindEmployee.PlaceholderText = "";
            this.txtFindEmployee.SelectedText = "";
            this.txtFindEmployee.Size = new System.Drawing.Size(173, 36);
            this.txtFindEmployee.TabIndex = 3;
            // 
            // btnFilter
            // 
            this.btnFilter.BorderRadius = 10;
            this.btnFilter.BorderThickness = 1;
            this.btnFilter.FillColor = System.Drawing.Color.LightGray;
            this.btnFilter.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Location = new System.Drawing.Point(12, 197);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(173, 24);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.BorderRadius = 10;
            this.btnShowAll.BorderThickness = 1;
            this.btnShowAll.FillColor = System.Drawing.Color.LightGray;
            this.btnShowAll.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAll.ForeColor = System.Drawing.Color.Black;
            this.btnShowAll.Location = new System.Drawing.Point(12, 239);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(173, 24);
            this.btnShowAll.TabIndex = 5;
            this.btnShowAll.Text = "Xem tất cả";
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // grbActions
            // 
            this.grbActions.BorderRadius = 10;
            this.grbActions.Controls.Add(this.btnCreate);
            this.grbActions.Controls.Add(this.btnEdit);
            this.grbActions.Controls.Add(this.btnDelete);
            this.grbActions.FillColor = System.Drawing.SystemColors.MenuBar;
            this.grbActions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grbActions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.grbActions.Location = new System.Drawing.Point(16, 16);
            this.grbActions.Name = "grbActions";
            this.grbActions.Size = new System.Drawing.Size(195, 180);
            this.grbActions.TabIndex = 1;
            this.grbActions.Text = "Thao tác";
            // 
            // btnCreate
            // 
            this.btnCreate.BorderRadius = 10;
            this.btnCreate.BorderThickness = 1;
            this.btnCreate.FillColor = System.Drawing.Color.LightGray;
            this.btnCreate.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.Color.Black;
            this.btnCreate.Location = new System.Drawing.Point(12, 50);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(173, 24);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Tạo hợp đồng";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BorderRadius = 10;
            this.btnEdit.BorderThickness = 1;
            this.btnEdit.FillColor = System.Drawing.Color.LightGray;
            this.btnEdit.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Location = new System.Drawing.Point(12, 92);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(173, 24);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BorderRadius = 10;
            this.btnDelete.BorderThickness = 1;
            this.btnDelete.FillColor = System.Drawing.Color.LightGray;
            this.btnDelete.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(12, 134);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(173, 24);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = false;
            this.headerLabel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.headerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerLabel.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold);
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(1373, 62);
            this.headerLabel.TabIndex = 2;
            this.headerLabel.Text = "Hợp đồng lao động";
            this.headerLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controls.Add(this.dgvHopDong);
            this.mainPanel.Controls.Add(this.lblContractType);
            this.mainPanel.Controls.Add(this.cmbContractType);
            this.mainPanel.Controls.Add(this.lblNgayKy);
            this.mainPanel.Controls.Add(this.dtpNgayKy);
            this.mainPanel.Controls.Add(this.lblNgayBatDau);
            this.mainPanel.Controls.Add(this.dtpNgayBatDau);
            this.mainPanel.Controls.Add(this.lblNgayKetThuc);
            this.mainPanel.Controls.Add(this.dtpNgayKetThuc);
            this.mainPanel.Controls.Add(this.lblLuong);
            this.mainPanel.Controls.Add(this.txtLuong);
            this.mainPanel.Controls.Add(this.lblNhanVien);
            this.mainPanel.Controls.Add(this.cmbNhanVien);
            this.mainPanel.Controls.Add(this.lblMoTa);
            this.mainPanel.Controls.Add(this.txtMoTa);
            this.mainPanel.Controls.Add(this.lblHinhAnh);
            this.mainPanel.Controls.Add(this.btnSelectImage);
            this.mainPanel.Controls.Add(this.ptbHinhHopDong);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(224, 62);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1149, 768);
            this.mainPanel.TabIndex = 1;
            // 
            // dgvHopDong
            // 
            this.dgvHopDong.AllowUserToAddRows = false;
            this.dgvHopDong.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvHopDong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHopDong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHopDong.ColumnHeadersHeight = 30;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHopDong.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHopDong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvHopDong.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHopDong.Location = new System.Drawing.Point(0, 530);
            this.dgvHopDong.Name = "dgvHopDong";
            this.dgvHopDong.ReadOnly = true;
            this.dgvHopDong.RowHeadersVisible = false;
            this.dgvHopDong.RowHeadersWidth = 51;
            this.dgvHopDong.RowTemplate.Height = 24;
            this.dgvHopDong.Size = new System.Drawing.Size(1149, 238);
            this.dgvHopDong.TabIndex = 0;
            this.dgvHopDong.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvHopDong.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvHopDong.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvHopDong.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvHopDong.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvHopDong.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvHopDong.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHopDong.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvHopDong.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvHopDong.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvHopDong.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvHopDong.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvHopDong.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvHopDong.ThemeStyle.ReadOnly = true;
            this.dgvHopDong.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvHopDong.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHopDong.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvHopDong.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvHopDong.ThemeStyle.RowsStyle.Height = 24;
            this.dgvHopDong.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHopDong.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvHopDong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHopDong_CellClick);
            // 
            // lblContractType
            // 
            this.lblContractType.AutoSize = false;
            this.lblContractType.BackColor = System.Drawing.Color.Transparent;
            this.lblContractType.Location = new System.Drawing.Point(47, 20);
            this.lblContractType.Name = "lblContractType";
            this.lblContractType.Size = new System.Drawing.Size(150, 29);
            this.lblContractType.TabIndex = 1;
            this.lblContractType.Text = "Loại hợp đồng";
            // 
            // cmbContractType
            // 
            this.cmbContractType.BackColor = System.Drawing.Color.Transparent;
            this.cmbContractType.BorderRadius = 7;
            this.cmbContractType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbContractType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContractType.FocusedColor = System.Drawing.Color.Empty;
            this.cmbContractType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbContractType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbContractType.ItemHeight = 30;
            this.cmbContractType.Items.AddRange(new object[] {
            "xác định thời hạn",
            "không xác định thời hạn"});
            this.cmbContractType.Location = new System.Drawing.Point(211, 20);
            this.cmbContractType.Name = "cmbContractType";
            this.cmbContractType.Size = new System.Drawing.Size(279, 36);
            this.cmbContractType.TabIndex = 2;
            // 
            // lblNgayKy
            // 
            this.lblNgayKy.AutoSize = false;
            this.lblNgayKy.BackColor = System.Drawing.Color.Transparent;
            this.lblNgayKy.Location = new System.Drawing.Point(47, 80);
            this.lblNgayKy.Name = "lblNgayKy";
            this.lblNgayKy.Size = new System.Drawing.Size(150, 29);
            this.lblNgayKy.TabIndex = 3;
            this.lblNgayKy.Text = "Ngày ký";
            // 
            // dtpNgayKy
            // 
            this.dtpNgayKy.BorderRadius = 7;
            this.dtpNgayKy.Checked = true;
            this.dtpNgayKy.FillColor = System.Drawing.Color.LightGray;
            this.dtpNgayKy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayKy.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayKy.Location = new System.Drawing.Point(211, 73);
            this.dtpNgayKy.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayKy.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayKy.Name = "dtpNgayKy";
            this.dtpNgayKy.Size = new System.Drawing.Size(279, 36);
            this.dtpNgayKy.TabIndex = 4;
            this.dtpNgayKy.Value = new System.DateTime(2025, 11, 4, 22, 26, 40, 773);
            // 
            // lblNgayBatDau
            // 
            this.lblNgayBatDau.AutoSize = false;
            this.lblNgayBatDau.BackColor = System.Drawing.Color.Transparent;
            this.lblNgayBatDau.Location = new System.Drawing.Point(47, 140);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(150, 29);
            this.lblNgayBatDau.TabIndex = 5;
            this.lblNgayBatDau.Text = "Ngày bắt đầu";
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.BorderRadius = 7;
            this.dtpNgayBatDau.Checked = true;
            this.dtpNgayBatDau.FillColor = System.Drawing.Color.LightGray;
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(211, 140);
            this.dtpNgayBatDau.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayBatDau.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(279, 36);
            this.dtpNgayBatDau.TabIndex = 6;
            this.dtpNgayBatDau.Value = new System.DateTime(2025, 11, 4, 22, 26, 40, 838);
            // 
            // lblNgayKetThuc
            // 
            this.lblNgayKetThuc.AutoSize = false;
            this.lblNgayKetThuc.BackColor = System.Drawing.Color.Transparent;
            this.lblNgayKetThuc.Location = new System.Drawing.Point(47, 200);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(150, 29);
            this.lblNgayKetThuc.TabIndex = 7;
            this.lblNgayKetThuc.Text = "Ngày kết thúc";
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.BorderRadius = 7;
            this.dtpNgayKetThuc.Checked = true;
            this.dtpNgayKetThuc.FillColor = System.Drawing.Color.LightGray;
            this.dtpNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(211, 200);
            this.dtpNgayKetThuc.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayKetThuc.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(279, 36);
            this.dtpNgayKetThuc.TabIndex = 8;
            this.dtpNgayKetThuc.Value = new System.DateTime(2025, 11, 4, 22, 26, 40, 901);
            // 
            // lblLuong
            // 
            this.lblLuong.AutoSize = false;
            this.lblLuong.BackColor = System.Drawing.Color.Transparent;
            this.lblLuong.Location = new System.Drawing.Point(592, 20);
            this.lblLuong.Name = "lblLuong";
            this.lblLuong.Size = new System.Drawing.Size(150, 29);
            this.lblLuong.TabIndex = 9;
            this.lblLuong.Text = "Lương";
            // 
            // txtLuong
            // 
            this.txtLuong.BorderRadius = 7;
            this.txtLuong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLuong.DefaultText = "";
            this.txtLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLuong.Location = new System.Drawing.Point(769, 20);
            this.txtLuong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLuong.Name = "txtLuong";
            this.txtLuong.PlaceholderText = "0.00";
            this.txtLuong.SelectedText = "";
            this.txtLuong.Size = new System.Drawing.Size(279, 36);
            this.txtLuong.TabIndex = 10;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = false;
            this.lblNhanVien.BackColor = System.Drawing.Color.Transparent;
            this.lblNhanVien.Location = new System.Drawing.Point(592, 80);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(150, 29);
            this.lblNhanVien.TabIndex = 11;
            this.lblNhanVien.Text = "Nhân viên";
            // 
            // cmbNhanVien
            // 
            this.cmbNhanVien.BackColor = System.Drawing.Color.Transparent;
            this.cmbNhanVien.BorderRadius = 7;
            this.cmbNhanVien.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNhanVien.FocusedColor = System.Drawing.Color.Empty;
            this.cmbNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbNhanVien.ItemHeight = 30;
            this.cmbNhanVien.Location = new System.Drawing.Point(769, 80);
            this.cmbNhanVien.Name = "cmbNhanVien";
            this.cmbNhanVien.Size = new System.Drawing.Size(279, 36);
            this.cmbNhanVien.TabIndex = 12;
            this.cmbNhanVien.SelectedIndexChanged += new System.EventHandler(this.cmbNhanVien_SelectedIndexChanged);
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = false;
            this.lblMoTa.BackColor = System.Drawing.Color.Transparent;
            this.lblMoTa.Location = new System.Drawing.Point(47, 260);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(150, 29);
            this.lblMoTa.TabIndex = 13;
            this.lblMoTa.Text = "Mô tả";
            // 
            // txtMoTa
            // 
            this.txtMoTa.BorderRadius = 7;
            this.txtMoTa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMoTa.DefaultText = "";
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMoTa.Location = new System.Drawing.Point(211, 260);
            this.txtMoTa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.PlaceholderText = "";
            this.txtMoTa.SelectedText = "";
            this.txtMoTa.Size = new System.Drawing.Size(279, 124);
            this.txtMoTa.TabIndex = 14;
            // 
            // lblHinhAnh
            // 
            this.lblHinhAnh.AutoSize = false;
            this.lblHinhAnh.BackColor = System.Drawing.Color.Transparent;
            this.lblHinhAnh.Location = new System.Drawing.Point(592, 140);
            this.lblHinhAnh.Name = "lblHinhAnh";
            this.lblHinhAnh.Size = new System.Drawing.Size(120, 29);
            this.lblHinhAnh.TabIndex = 15;
            this.lblHinhAnh.Text = "Hình hợp đồng";
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.BorderRadius = 10;
            this.btnSelectImage.BorderThickness = 1;
            this.btnSelectImage.FillColor = System.Drawing.Color.LightGray;
            this.btnSelectImage.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectImage.ForeColor = System.Drawing.Color.Black;
            this.btnSelectImage.Location = new System.Drawing.Point(769, 140);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(279, 36);
            this.btnSelectImage.TabIndex = 16;
            this.btnSelectImage.Text = "Chọn hình";
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // ptbHinhHopDong
            // 
            this.ptbHinhHopDong.ImageRotate = 0F;
            this.ptbHinhHopDong.Location = new System.Drawing.Point(769, 200);
            this.ptbHinhHopDong.Name = "ptbHinhHopDong";
            this.ptbHinhHopDong.Size = new System.Drawing.Size(158, 228);
            this.ptbHinhHopDong.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbHinhHopDong.TabIndex = 17;
            this.ptbHinhHopDong.TabStop = false;
            // 
            // UCHopDong
            // 
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.headerLabel);
            this.Name = "UCHopDong";
            this.Size = new System.Drawing.Size(1373, 830);
            this.Load += new System.EventHandler(this.UCHopDong_Load);
            this.pnlLeft.ResumeLayout(false);
            this.grbFilter.ResumeLayout(false);
            this.grbFilter.PerformLayout();
            this.grbActions.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHopDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbHinhHopDong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private Guna.UI2.WinForms.Guna2GroupBox grbFilter;
        private Guna.UI2.WinForms.Guna2GroupBox grbActions;
        private Guna.UI2.WinForms.Guna2Button btnCreate;
        private Guna.UI2.WinForms.Guna2Button btnEdit;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnFilter;
        private Guna.UI2.WinForms.Guna2Button btnShowAll;
        private Guna.UI2.WinForms.Guna2TextBox txtFindEmployee;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFindType;
        private System.Windows.Forms.Label lblFindType;
        private System.Windows.Forms.Label lblFindEmployee;
        private Guna.UI2.WinForms.Guna2HtmlLabel headerLabel;
        private Guna.UI2.WinForms.Guna2Panel mainPanel;
        private Guna.UI2.WinForms.Guna2DataGridView dgvHopDong;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblContractType;
        private Guna.UI2.WinForms.Guna2ComboBox cmbContractType;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNgayKy;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayKy;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNgayBatDau;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayBatDau;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNgayKetThuc;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayKetThuc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLuong;
        private Guna.UI2.WinForms.Guna2TextBox txtLuong;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNhanVien;
        private Guna.UI2.WinForms.Guna2ComboBox cmbNhanVien;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMoTa;
        private Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHinhAnh;
        private Guna.UI2.WinForms.Guna2Button btnSelectImage;
        private Guna.UI2.WinForms.Guna2PictureBox ptbHinhHopDong;
    }
}
