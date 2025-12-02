namespace GUI
{
    partial class FrmXemDanhGiaChiTietNV
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grbNhanVien = new Guna.UI2.WinForms.Guna2GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPhanLoaiDiem = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbThang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbNam = new Guna.UI2.WinForms.Guna2ComboBox();
            this.rtNhanXet = new System.Windows.Forms.RichTextBox();
            this.txtNgayTao = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDiemSo = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvDSDanhGiaNV = new Guna.UI2.WinForms.Guna2DataGridView();
            this.grbNhanVien.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSDanhGiaNV)).BeginInit();
            this.SuspendLayout();
            // 
            // grbNhanVien
            // 
            this.grbNhanVien.BackColor = System.Drawing.Color.Transparent;
            this.grbNhanVien.BorderRadius = 10;
            this.grbNhanVien.Controls.Add(this.groupBox1);
            this.grbNhanVien.Controls.Add(this.rtNhanXet);
            this.grbNhanVien.Controls.Add(this.txtNgayTao);
            this.grbNhanVien.Controls.Add(this.txtDiemSo);
            this.grbNhanVien.Controls.Add(this.label1);
            this.grbNhanVien.Controls.Add(this.label2);
            this.grbNhanVien.Controls.Add(this.label3);
            this.grbNhanVien.Controls.Add(this.dgvDSDanhGiaNV);
            this.grbNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grbNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.grbNhanVien.Location = new System.Drawing.Point(13, 12);
            this.grbNhanVien.Margin = new System.Windows.Forms.Padding(4);
            this.grbNhanVien.Name = "grbNhanVien";
            this.grbNhanVien.Padding = new System.Windows.Forms.Padding(20);
            this.grbNhanVien.Size = new System.Drawing.Size(1243, 674);
            this.grbNhanVien.TabIndex = 34;
            this.grbNhanVien.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.grbNhanVien.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.UpperCase;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbPhanLoaiDiem);
            this.groupBox1.Controls.Add(this.cmbThang);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbNam);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.Location = new System.Drawing.Point(865, 81);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(360, 281);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc dữ liệu:";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label12.Location = new System.Drawing.Point(188, 55);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 25, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(157, 28);
            this.label12.TabIndex = 65;
            this.label12.Text = "Tháng";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(10, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 25, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 28);
            this.label4.TabIndex = 58;
            this.label4.Text = "Năm";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbPhanLoaiDiem
            // 
            this.cmbPhanLoaiDiem.BackColor = System.Drawing.Color.Transparent;
            this.cmbPhanLoaiDiem.BorderRadius = 10;
            this.cmbPhanLoaiDiem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPhanLoaiDiem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPhanLoaiDiem.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbPhanLoaiDiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbPhanLoaiDiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPhanLoaiDiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbPhanLoaiDiem.ItemHeight = 30;
            this.cmbPhanLoaiDiem.Location = new System.Drawing.Point(184, 171);
            this.cmbPhanLoaiDiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbPhanLoaiDiem.Name = "cmbPhanLoaiDiem";
            this.cmbPhanLoaiDiem.Size = new System.Drawing.Size(161, 36);
            this.cmbPhanLoaiDiem.TabIndex = 64;
            this.cmbPhanLoaiDiem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cmbPhanLoaiDiem.SelectionChangeCommitted += new System.EventHandler(this.cmbPhanLoaiDiem_SelectionChangeCommitted);
            // 
            // cmbThang
            // 
            this.cmbThang.BackColor = System.Drawing.Color.Transparent;
            this.cmbThang.BorderRadius = 10;
            this.cmbThang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThang.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbThang.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbThang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbThang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbThang.ItemHeight = 30;
            this.cmbThang.Location = new System.Drawing.Point(185, 104);
            this.cmbThang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbThang.Name = "cmbThang";
            this.cmbThang.Size = new System.Drawing.Size(161, 36);
            this.cmbThang.TabIndex = 64;
            this.cmbThang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cmbThang.SelectionChangeCommitted += new System.EventHandler(this.cmbThang_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(10, 173);
            this.label8.Margin = new System.Windows.Forms.Padding(7, 12, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 36);
            this.label8.TabIndex = 57;
            this.label8.Text = "Phân loại điểm:";
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
            this.cmbNam.Location = new System.Drawing.Point(8, 104);
            this.cmbNam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbNam.Name = "cmbNam";
            this.cmbNam.Size = new System.Drawing.Size(154, 36);
            this.cmbNam.TabIndex = 56;
            this.cmbNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cmbNam.SelectionChangeCommitted += new System.EventHandler(this.cmbNam_SelectionChangeCommitted);
            // 
            // rtNhanXet
            // 
            this.rtNhanXet.Location = new System.Drawing.Point(20, 266);
            this.rtNhanXet.Name = "rtNhanXet";
            this.rtNhanXet.ReadOnly = true;
            this.rtNhanXet.Size = new System.Drawing.Size(800, 96);
            this.rtNhanXet.TabIndex = 53;
            this.rtNhanXet.Text = "";
            // 
            // txtNgayTao
            // 
            this.txtNgayTao.BackColor = System.Drawing.Color.Transparent;
            this.txtNgayTao.BorderRadius = 10;
            this.txtNgayTao.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNgayTao.DefaultText = "";
            this.txtNgayTao.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNgayTao.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNgayTao.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNgayTao.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNgayTao.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNgayTao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNgayTao.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNgayTao.Location = new System.Drawing.Point(474, 148);
            this.txtNgayTao.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtNgayTao.Name = "txtNgayTao";
            this.txtNgayTao.PlaceholderText = "";
            this.txtNgayTao.ReadOnly = true;
            this.txtNgayTao.SelectedText = "";
            this.txtNgayTao.Size = new System.Drawing.Size(346, 55);
            this.txtNgayTao.TabIndex = 51;
            this.txtNgayTao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDiemSo
            // 
            this.txtDiemSo.BackColor = System.Drawing.Color.Transparent;
            this.txtDiemSo.BorderRadius = 10;
            this.txtDiemSo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiemSo.DefaultText = "";
            this.txtDiemSo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDiemSo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDiemSo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemSo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemSo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemSo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDiemSo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemSo.Location = new System.Drawing.Point(20, 148);
            this.txtDiemSo.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
            this.txtDiemSo.Name = "txtDiemSo";
            this.txtDiemSo.PlaceholderText = "";
            this.txtDiemSo.ReadOnly = true;
            this.txtDiemSo.SelectedText = "";
            this.txtDiemSo.Size = new System.Drawing.Size(346, 55);
            this.txtDiemSo.TabIndex = 52;
            this.txtDiemSo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(16, 208);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 55);
            this.label1.TabIndex = 50;
            this.label1.Text = "Nhận xét: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(16, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 55);
            this.label2.TabIndex = 50;
            this.label2.Text = "Điểm số:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(470, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 55);
            this.label3.TabIndex = 49;
            this.label3.Text = "Ngày tạo: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvDSDanhGiaNV
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDSDanhGiaNV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDSDanhGiaNV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDSDanhGiaNV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDSDanhGiaNV.ColumnHeadersHeight = 35;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDSDanhGiaNV.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDSDanhGiaNV.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDSDanhGiaNV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSDanhGiaNV.Location = new System.Drawing.Point(20, 392);
            this.dgvDSDanhGiaNV.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDSDanhGiaNV.MultiSelect = false;
            this.dgvDSDanhGiaNV.Name = "dgvDSDanhGiaNV";
            this.dgvDSDanhGiaNV.RowHeadersVisible = false;
            this.dgvDSDanhGiaNV.RowHeadersWidth = 51;
            this.dgvDSDanhGiaNV.Size = new System.Drawing.Size(1203, 262);
            this.dgvDSDanhGiaNV.TabIndex = 37;
            this.dgvDSDanhGiaNV.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSDanhGiaNV.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDSDanhGiaNV.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDSDanhGiaNV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDSDanhGiaNV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDSDanhGiaNV.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSDanhGiaNV.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSDanhGiaNV.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvDSDanhGiaNV.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDSDanhGiaNV.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgvDSDanhGiaNV.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDSDanhGiaNV.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDSDanhGiaNV.ThemeStyle.HeaderStyle.Height = 35;
            this.dgvDSDanhGiaNV.ThemeStyle.ReadOnly = false;
            this.dgvDSDanhGiaNV.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSDanhGiaNV.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDSDanhGiaNV.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgvDSDanhGiaNV.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDSDanhGiaNV.ThemeStyle.RowsStyle.Height = 22;
            this.dgvDSDanhGiaNV.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSDanhGiaNV.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDSDanhGiaNV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSDanhGiaNV_CellClick);
            // 
            // FrmXemDanhGiaChiTietNV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1269, 698);
            this.Controls.Add(this.grbNhanVien);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmXemDanhGiaChiTietNV";
            this.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmXemDanhGiaChiTietNV";
            this.Load += new System.EventHandler(this.FrmXemDanhGiaChiTietNV_Load);
            this.grbNhanVien.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSDanhGiaNV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion  
        private Guna.UI2.WinForms.Guna2GroupBox grbNhanVien;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDSDanhGiaNV;
        private Guna.UI2.WinForms.Guna2TextBox txtNgayTao;
        private Guna.UI2.WinForms.Guna2TextBox txtDiemSo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtNhanXet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2ComboBox cmbPhanLoaiDiem;
        private Guna.UI2.WinForms.Guna2ComboBox cmbThang;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2ComboBox cmbNam;
    }
}