namespace GUI
{
    partial class UCDanhGiaHieuSuat
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLoadDuLieu = new Guna.UI2.WinForms.Guna2GradientButton();
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbQuy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbNam = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtLocDTB = new Guna.UI2.WinForms.Guna2TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLocSoLanDG = new Guna.UI2.WinForms.Guna2TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMucTieu = new Guna.UI2.WinForms.Guna2TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTimTenNV = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTimEmailNV = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlNghiPhep = new System.Windows.Forms.Panel();
            this.grbDanhGiaHS = new Guna.UI2.WinForms.Guna2GroupBox();
            this.dgvDSHieuSuatNV = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblIDNV = new System.Windows.Forms.Label();
            this.txtChucVu = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNgaySinh = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDiemDG = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtGioiTinh = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNhanVien = new Guna.UI2.WinForms.Guna2TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDsNV = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlNghiPhep.SuspendLayout();
            this.grbDanhGiaHS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSHieuSuatNV)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadDuLieu
            // 
            this.btnLoadDuLieu.BorderRadius = 5;
            this.btnLoadDuLieu.BorderThickness = 1;
            this.btnLoadDuLieu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLoadDuLieu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLoadDuLieu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoadDuLieu.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoadDuLieu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLoadDuLieu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoadDuLieu.FillColor2 = System.Drawing.Color.Silver;
            this.btnLoadDuLieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLoadDuLieu.ForeColor = System.Drawing.Color.White;
            this.btnLoadDuLieu.Location = new System.Drawing.Point(24, 929);
            this.btnLoadDuLieu.Margin = new System.Windows.Forms.Padding(4, 420, 4, 4);
            this.btnLoadDuLieu.Name = "btnLoadDuLieu";
            this.btnLoadDuLieu.Size = new System.Drawing.Size(316, 54);
            this.btnLoadDuLieu.TabIndex = 50;
            this.btnLoadDuLieu.Text = "Load dữ liệu ";
            this.btnLoadDuLieu.Click += new System.EventHandler(this.btnLoadDuLieu_Click);
            // 
            // error
            // 
            this.error.ContainerControl = this;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.label13);
            this.flowLayoutPanel1.Controls.Add(this.txtTimTenNV);
            this.flowLayoutPanel1.Controls.Add(this.txtTimEmailNV);
            this.flowLayoutPanel1.Controls.Add(this.btnLoadDuLieu);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(11, 10);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(20, 25, 3, 25);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(368, 1000);
            this.flowLayoutPanel1.TabIndex = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbQuy);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbNam);
            this.groupBox1.Controls.Add(this.txtLocDTB);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtLocSoLanDG);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtMucTieu);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.Location = new System.Drawing.Point(24, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(331, 277);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc điểm:";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label12.Location = new System.Drawing.Point(172, 33);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 25, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(141, 28);
            this.label12.TabIndex = 65;
            this.label12.Text = "Quý";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(17, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 25, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 28);
            this.label1.TabIndex = 58;
            this.label1.Text = "Năm";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbQuy
            // 
            this.cmbQuy.BackColor = System.Drawing.Color.Transparent;
            this.cmbQuy.BorderRadius = 10;
            this.cmbQuy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbQuy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbQuy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbQuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbQuy.ItemHeight = 30;
            this.cmbQuy.Location = new System.Drawing.Point(169, 64);
            this.cmbQuy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbQuy.Name = "cmbQuy";
            this.cmbQuy.Size = new System.Drawing.Size(140, 36);
            this.cmbQuy.TabIndex = 64;
            this.cmbQuy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cmbQuy.SelectedIndexChanged += new System.EventHandler(this.cmbQuy_SelectedIndexChanged);
            this.cmbQuy.SelectionChangeCommitted += new System.EventHandler(this.cmbQuy_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(11, 123);
            this.label8.Margin = new System.Windows.Forms.Padding(7, 12, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 32);
            this.label8.TabIndex = 57;
            this.label8.Text = "Điểm trung bình: ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbNam
            // 
            this.cmbNam.BackColor = System.Drawing.Color.Transparent;
            this.cmbNam.BorderRadius = 10;
            this.cmbNam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNam.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbNam.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbNam.ItemHeight = 30;
            this.cmbNam.Location = new System.Drawing.Point(15, 64);
            this.cmbNam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbNam.Name = "cmbNam";
            this.cmbNam.Size = new System.Drawing.Size(133, 36);
            this.cmbNam.TabIndex = 56;
            this.cmbNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cmbNam.SelectedIndexChanged += new System.EventHandler(this.cmbNam_SelectedIndexChanged);
            this.cmbNam.SelectionChangeCommitted += new System.EventHandler(this.cmbNam_SelectionChangeCommitted);
            // 
            // txtLocDTB
            // 
            this.txtLocDTB.BorderRadius = 10;
            this.txtLocDTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLocDTB.DefaultText = "";
            this.txtLocDTB.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLocDTB.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLocDTB.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLocDTB.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLocDTB.Enabled = false;
            this.txtLocDTB.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLocDTB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtLocDTB.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLocDTB.Location = new System.Drawing.Point(169, 123);
            this.txtLocDTB.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtLocDTB.Name = "txtLocDTB";
            this.txtLocDTB.PlaceholderText = "";
            this.txtLocDTB.SelectedText = "";
            this.txtLocDTB.Size = new System.Drawing.Size(141, 32);
            this.txtLocDTB.TabIndex = 59;
            this.txtLocDTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.Location = new System.Drawing.Point(11, 171);
            this.label9.Margin = new System.Windows.Forms.Padding(7, 12, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(139, 32);
            this.label9.TabIndex = 60;
            this.label9.Text = "Số lần đánh giá:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLocSoLanDG
            // 
            this.txtLocSoLanDG.BorderRadius = 10;
            this.txtLocSoLanDG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLocSoLanDG.DefaultText = "";
            this.txtLocSoLanDG.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLocSoLanDG.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLocSoLanDG.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLocSoLanDG.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLocSoLanDG.Enabled = false;
            this.txtLocSoLanDG.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLocSoLanDG.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtLocSoLanDG.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLocSoLanDG.Location = new System.Drawing.Point(169, 171);
            this.txtLocSoLanDG.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtLocSoLanDG.Name = "txtLocSoLanDG";
            this.txtLocSoLanDG.PlaceholderText = "";
            this.txtLocSoLanDG.SelectedText = "";
            this.txtLocSoLanDG.Size = new System.Drawing.Size(141, 32);
            this.txtLocSoLanDG.TabIndex = 61;
            this.txtLocSoLanDG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label10.Location = new System.Drawing.Point(11, 219);
            this.label10.Margin = new System.Windows.Forms.Padding(7, 12, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 32);
            this.label10.TabIndex = 62;
            this.label10.Text = "Mục tiêu: ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMucTieu
            // 
            this.txtMucTieu.BorderRadius = 10;
            this.txtMucTieu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMucTieu.DefaultText = "";
            this.txtMucTieu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMucTieu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMucTieu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMucTieu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMucTieu.Enabled = false;
            this.txtMucTieu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMucTieu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtMucTieu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMucTieu.Location = new System.Drawing.Point(169, 219);
            this.txtMucTieu.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtMucTieu.Name = "txtMucTieu";
            this.txtMucTieu.PlaceholderText = "";
            this.txtMucTieu.SelectedText = "";
            this.txtMucTieu.Size = new System.Drawing.Size(141, 32);
            this.txtMucTieu.TabIndex = 63;
            this.txtMucTieu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label13.Location = new System.Drawing.Point(24, 330);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 20, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(316, 49);
            this.label13.TabIndex = 47;
            this.label13.Text = "Tìm nhân viên";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTimTenNV
            // 
            this.txtTimTenNV.BackColor = System.Drawing.Color.Transparent;
            this.txtTimTenNV.BorderRadius = 10;
            this.txtTimTenNV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimTenNV.DefaultText = "";
            this.txtTimTenNV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimTenNV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimTenNV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimTenNV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimTenNV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimTenNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimTenNV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimTenNV.Location = new System.Drawing.Point(24, 391);
            this.txtTimTenNV.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtTimTenNV.Name = "txtTimTenNV";
            this.txtTimTenNV.PlaceholderText = "Nhập tên ";
            this.txtTimTenNV.SelectedText = "";
            this.txtTimTenNV.Size = new System.Drawing.Size(316, 49);
            this.txtTimTenNV.TabIndex = 48;
            this.txtTimTenNV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTimTenNV.TextChanged += new System.EventHandler(this.txtTimTenNV_TextChanged);
            // 
            // txtTimEmailNV
            // 
            this.txtTimEmailNV.BackColor = System.Drawing.Color.Transparent;
            this.txtTimEmailNV.BorderRadius = 10;
            this.txtTimEmailNV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimEmailNV.DefaultText = "";
            this.txtTimEmailNV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimEmailNV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimEmailNV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimEmailNV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimEmailNV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimEmailNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimEmailNV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimEmailNV.Location = new System.Drawing.Point(24, 456);
            this.txtTimEmailNV.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtTimEmailNV.Name = "txtTimEmailNV";
            this.txtTimEmailNV.PlaceholderText = "Nhập email ";
            this.txtTimEmailNV.SelectedText = "";
            this.txtTimEmailNV.Size = new System.Drawing.Size(316, 49);
            this.txtTimEmailNV.TabIndex = 57;
            this.txtTimEmailNV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTimEmailNV.TextChanged += new System.EventHandler(this.txtTimEmailNV_TextChanged);
            // 
            // pnlNghiPhep
            // 
            this.pnlNghiPhep.Controls.Add(this.grbDanhGiaHS);
            this.pnlNghiPhep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNghiPhep.Location = new System.Drawing.Point(379, 10);
            this.pnlNghiPhep.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlNghiPhep.Name = "pnlNghiPhep";
            this.pnlNghiPhep.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlNghiPhep.Size = new System.Drawing.Size(1602, 1000);
            this.pnlNghiPhep.TabIndex = 46;
            // 
            // grbDanhGiaHS
            // 
            this.grbDanhGiaHS.BackColor = System.Drawing.Color.Transparent;
            this.grbDanhGiaHS.BorderRadius = 10;
            this.grbDanhGiaHS.BorderThickness = 2;
            this.grbDanhGiaHS.Controls.Add(this.dgvDSHieuSuatNV);
            this.grbDanhGiaHS.Controls.Add(this.lblIDNV);
            this.grbDanhGiaHS.Controls.Add(this.txtChucVu);
            this.grbDanhGiaHS.Controls.Add(this.txtNgaySinh);
            this.grbDanhGiaHS.Controls.Add(this.txtEmail);
            this.grbDanhGiaHS.Controls.Add(this.txtDiemDG);
            this.grbDanhGiaHS.Controls.Add(this.txtGioiTinh);
            this.grbDanhGiaHS.Controls.Add(this.txtNhanVien);
            this.grbDanhGiaHS.Controls.Add(this.label7);
            this.grbDanhGiaHS.Controls.Add(this.label5);
            this.grbDanhGiaHS.Controls.Add(this.label6);
            this.grbDanhGiaHS.Controls.Add(this.label2);
            this.grbDanhGiaHS.Controls.Add(this.label4);
            this.grbDanhGiaHS.Controls.Add(this.label3);
            this.grbDanhGiaHS.Controls.Add(this.lblDsNV);
            this.grbDanhGiaHS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbDanhGiaHS.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grbDanhGiaHS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.grbDanhGiaHS.Location = new System.Drawing.Point(13, 12);
            this.grbDanhGiaHS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grbDanhGiaHS.Name = "grbDanhGiaHS";
            this.grbDanhGiaHS.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.grbDanhGiaHS.Size = new System.Drawing.Size(1576, 976);
            this.grbDanhGiaHS.TabIndex = 37;
            this.grbDanhGiaHS.Text = "Người đánh giá: ";
            this.grbDanhGiaHS.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.UpperCase;
            // 
            // dgvDSHieuSuatNV
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDSHieuSuatNV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDSHieuSuatNV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDSHieuSuatNV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDSHieuSuatNV.ColumnHeadersHeight = 35;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDSHieuSuatNV.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDSHieuSuatNV.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDSHieuSuatNV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSHieuSuatNV.Location = new System.Drawing.Point(13, 617);
            this.dgvDSHieuSuatNV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDSHieuSuatNV.MultiSelect = false;
            this.dgvDSHieuSuatNV.Name = "dgvDSHieuSuatNV";
            this.dgvDSHieuSuatNV.RowHeadersVisible = false;
            this.dgvDSHieuSuatNV.RowHeadersWidth = 51;
            this.dgvDSHieuSuatNV.Size = new System.Drawing.Size(1550, 347);
            this.dgvDSHieuSuatNV.TabIndex = 0;
            this.dgvDSHieuSuatNV.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSHieuSuatNV.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDSHieuSuatNV.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDSHieuSuatNV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDSHieuSuatNV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDSHieuSuatNV.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSHieuSuatNV.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSHieuSuatNV.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvDSHieuSuatNV.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDSHieuSuatNV.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgvDSHieuSuatNV.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDSHieuSuatNV.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDSHieuSuatNV.ThemeStyle.HeaderStyle.Height = 35;
            this.dgvDSHieuSuatNV.ThemeStyle.ReadOnly = false;
            this.dgvDSHieuSuatNV.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSHieuSuatNV.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDSHieuSuatNV.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgvDSHieuSuatNV.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDSHieuSuatNV.ThemeStyle.RowsStyle.Height = 22;
            this.dgvDSHieuSuatNV.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSHieuSuatNV.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDSHieuSuatNV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSHieuSuatNV_CellClick);
            this.dgvDSHieuSuatNV.DoubleClick += new System.EventHandler(this.dgvDSHieuSuatNV_DoubleClick);
            // 
            // lblIDNP
            // 
            this.lblIDNV.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblIDNV.Location = new System.Drawing.Point(17, 62);
            this.lblIDNV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIDNV.Name = "lblIDNP";
            this.lblIDNV.Size = new System.Drawing.Size(133, 28);
            this.lblIDNV.TabIndex = 47;
            this.lblIDNV.Visible = false;
            // 
            // txtChucVu
            // 
            this.txtChucVu.BackColor = System.Drawing.Color.Transparent;
            this.txtChucVu.BorderRadius = 10;
            this.txtChucVu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtChucVu.DefaultText = "";
            this.txtChucVu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtChucVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtChucVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtChucVu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtChucVu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtChucVu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtChucVu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtChucVu.Location = new System.Drawing.Point(935, 354);
            this.txtChucVu.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.PlaceholderText = "";
            this.txtChucVu.ReadOnly = true;
            this.txtChucVu.SelectedText = "";
            this.txtChucVu.Size = new System.Drawing.Size(397, 55);
            this.txtChucVu.TabIndex = 48;
            this.txtChucVu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNgaySinh
            // 
            this.txtNgaySinh.BackColor = System.Drawing.Color.Transparent;
            this.txtNgaySinh.BorderRadius = 10;
            this.txtNgaySinh.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNgaySinh.DefaultText = "";
            this.txtNgaySinh.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNgaySinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNgaySinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNgaySinh.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNgaySinh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNgaySinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNgaySinh.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNgaySinh.Location = new System.Drawing.Point(313, 354);
            this.txtNgaySinh.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtNgaySinh.Name = "txtNgaySinh";
            this.txtNgaySinh.PlaceholderText = "";
            this.txtNgaySinh.ReadOnly = true;
            this.txtNgaySinh.SelectedText = "";
            this.txtNgaySinh.Size = new System.Drawing.Size(397, 55);
            this.txtNgaySinh.TabIndex = 48;
            this.txtNgaySinh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.Transparent;
            this.txtEmail.BorderRadius = 10;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Location = new System.Drawing.Point(313, 265);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(397, 55);
            this.txtEmail.TabIndex = 48;
            this.txtEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDiemDG
            // 
            this.txtDiemDG.BackColor = System.Drawing.Color.Transparent;
            this.txtDiemDG.BorderRadius = 10;
            this.txtDiemDG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiemDG.DefaultText = "";
            this.txtDiemDG.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDiemDG.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDiemDG.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemDG.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemDG.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemDG.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDiemDG.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemDG.Location = new System.Drawing.Point(935, 265);
            this.txtDiemDG.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtDiemDG.Name = "txtDiemDG";
            this.txtDiemDG.PlaceholderText = "";
            this.txtDiemDG.ReadOnly = true;
            this.txtDiemDG.SelectedText = "";
            this.txtDiemDG.Size = new System.Drawing.Size(397, 55);
            this.txtDiemDG.TabIndex = 48;
            this.txtDiemDG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGioiTinh
            // 
            this.txtGioiTinh.BackColor = System.Drawing.Color.Transparent;
            this.txtGioiTinh.BorderRadius = 10;
            this.txtGioiTinh.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGioiTinh.DefaultText = "";
            this.txtGioiTinh.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtGioiTinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtGioiTinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGioiTinh.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGioiTinh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGioiTinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtGioiTinh.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGioiTinh.Location = new System.Drawing.Point(935, 183);
            this.txtGioiTinh.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtGioiTinh.Name = "txtGioiTinh";
            this.txtGioiTinh.PlaceholderText = "";
            this.txtGioiTinh.ReadOnly = true;
            this.txtGioiTinh.SelectedText = "";
            this.txtGioiTinh.Size = new System.Drawing.Size(397, 55);
            this.txtGioiTinh.TabIndex = 48;
            this.txtGioiTinh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNhanVien
            // 
            this.txtNhanVien.BackColor = System.Drawing.Color.Transparent;
            this.txtNhanVien.BorderRadius = 10;
            this.txtNhanVien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhanVien.DefaultText = "";
            this.txtNhanVien.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNhanVien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNhanVien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNhanVien.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNhanVien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNhanVien.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNhanVien.Location = new System.Drawing.Point(313, 183);
            this.txtNhanVien.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.PlaceholderText = "";
            this.txtNhanVien.ReadOnly = true;
            this.txtNhanVien.SelectedText = "";
            this.txtNhanVien.Size = new System.Drawing.Size(397, 55);
            this.txtNhanVien.TabIndex = 48;
            this.txtNhanVien.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(749, 354);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 39);
            this.label7.TabIndex = 43;
            this.label7.Text = "Chức vụ:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(172, 265);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 39);
            this.label5.TabIndex = 43;
            this.label5.Text = "Email:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(749, 265);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(209, 49);
            this.label6.TabIndex = 46;
            this.label6.Text = "Điểm đánh giá:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(172, 183);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 49);
            this.label2.TabIndex = 47;
            this.label2.Text = "Nhân viên: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(172, 350);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 49);
            this.label4.TabIndex = 44;
            this.label4.Text = "Ngày sinh:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(749, 183);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 49);
            this.label3.TabIndex = 45;
            this.label3.Text = "Giới tính:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDsNV
            // 
            this.lblDsNV.BackColor = System.Drawing.Color.Transparent;
            this.lblDsNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblDsNV.Location = new System.Drawing.Point(17, 585);
            this.lblDsNV.Margin = new System.Windows.Forms.Padding(4, 37, 4, 0);
            this.lblDsNV.Name = "lblDsNV";
            this.lblDsNV.Size = new System.Drawing.Size(1315, 28);
            this.lblDsNV.TabIndex = 49;
            this.lblDsNV.Text = "Danh sách đánh giá nhân viên:";
            this.lblDsNV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCDanhGiaHieuSuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlNghiPhep);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UCDanhGiaHieuSuat";
            this.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.Size = new System.Drawing.Size(1992, 1020);
            this.Load += new System.EventHandler(this.UCDanhGiaHieuSuat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.pnlNghiPhep.ResumeLayout(false);
            this.grbDanhGiaHS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSHieuSuatNV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2GradientButton btnLoadDuLieu;
        private System.Windows.Forms.ErrorProvider error;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel pnlNghiPhep;
        private Guna.UI2.WinForms.Guna2GroupBox grbDanhGiaHS;
        private System.Windows.Forms.Label lblDsNV;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDSHieuSuatNV;
        private System.Windows.Forms.Label lblIDNV;
        private Guna.UI2.WinForms.Guna2TextBox txtDiemDG;
        private Guna.UI2.WinForms.Guna2TextBox txtGioiTinh;
        private Guna.UI2.WinForms.Guna2TextBox txtNhanVien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbQuy;
        private Guna.UI2.WinForms.Guna2ComboBox cmbNam;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2TextBox txtLocDTB;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2TextBox txtLocSoLanDG;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2TextBox txtMucTieu;
        private Guna.UI2.WinForms.Guna2TextBox txtTimTenNV;
        private System.Windows.Forms.Label label13;
        private Guna.UI2.WinForms.Guna2TextBox txtTimEmailNV;
        private Guna.UI2.WinForms.Guna2TextBox txtChucVu;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2TextBox txtNgaySinh;
    }
}
