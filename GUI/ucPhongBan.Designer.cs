using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GUI
{
    partial class ucPhongBan
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
        /// Required method for Designer support — do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2HtmlLabelHeader = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2PanelLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.grpFind = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblFindName = new System.Windows.Forms.Label();
            this.txtFindName = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnFind = new Guna.UI2.WinForms.Guna2Button();
            this.btnReset = new Guna.UI2.WinForms.Guna2Button();
            this.grbCRUD = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.btnUpdate = new Guna.UI2.WinForms.Guna2Button();
            this.btnDelete = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PanelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabelTen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTenPhongBan = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabelMoTa = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtMota = new Guna.UI2.WinForms.Guna2TextBox();
            this.dgvPhongBan = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2PanelLeft.SuspendLayout();
            this.grpFind.SuspendLayout();
            this.grbCRUD.SuspendLayout();
            this.guna2PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongBan)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2HtmlLabelHeader
            // 
            this.guna2HtmlLabelHeader.AutoSize = false;
            this.guna2HtmlLabelHeader.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.guna2HtmlLabelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2HtmlLabelHeader.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabelHeader.Location = new System.Drawing.Point(0, 0);
            this.guna2HtmlLabelHeader.Name = "guna2HtmlLabelHeader";
            this.guna2HtmlLabelHeader.Size = new System.Drawing.Size(1464, 62);
            this.guna2HtmlLabelHeader.TabIndex = 0;
            this.guna2HtmlLabelHeader.TabStop = false;
            this.guna2HtmlLabelHeader.Text = "Phòng ban";
            this.guna2HtmlLabelHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2PanelLeft
            // 
            this.guna2PanelLeft.BackColor = System.Drawing.SystemColors.MenuBar;
            this.guna2PanelLeft.BorderColor = System.Drawing.Color.Black;
            this.guna2PanelLeft.BorderThickness = 1;
            this.guna2PanelLeft.Controls.Add(this.grpFind);
            this.guna2PanelLeft.Controls.Add(this.grbCRUD);
            this.guna2PanelLeft.CustomBorderColor = System.Drawing.Color.Black;
            this.guna2PanelLeft.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.guna2PanelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2PanelLeft.Location = new System.Drawing.Point(0, 62);
            this.guna2PanelLeft.Name = "guna2PanelLeft";
            this.guna2PanelLeft.Size = new System.Drawing.Size(300, 600);
            this.guna2PanelLeft.TabIndex = 1;
            // 
            // grpFind
            // 
            this.grpFind.BorderRadius = 10;
            this.grpFind.Controls.Add(this.lblFindName);
            this.grpFind.Controls.Add(this.txtFindName);
            this.grpFind.Controls.Add(this.btnFind);
            this.grpFind.Controls.Add(this.btnReset);
            this.grpFind.FillColor = System.Drawing.SystemColors.MenuBar;
            this.grpFind.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpFind.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.grpFind.Location = new System.Drawing.Point(23, 200);
            this.grpFind.Name = "grpFind";
            this.grpFind.Size = new System.Drawing.Size(255, 260);
            this.grpFind.TabIndex = 20;
            this.grpFind.Text = "Lọc - Tìm kiếm phòng ban";
            // 
            // lblFindName
            // 
            this.lblFindName.AutoSize = true;
            this.lblFindName.BackColor = System.Drawing.Color.Transparent;
            this.lblFindName.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.lblFindName.ForeColor = System.Drawing.Color.Black;
            this.lblFindName.Location = new System.Drawing.Point(15, 60);
            this.lblFindName.Name = "lblFindName";
            this.lblFindName.Size = new System.Drawing.Size(106, 19);
            this.lblFindName.TabIndex = 1;
            this.lblFindName.Text = "Tên phòng ban";
            // 
            // txtFindName
            // 
            this.txtFindName.BorderRadius = 7;
            this.txtFindName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFindName.DefaultText = "";
            this.txtFindName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFindName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFindName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFindName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFindName.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.txtFindName.ForeColor = System.Drawing.Color.Black;
            this.txtFindName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFindName.Location = new System.Drawing.Point(12, 80);
            this.txtFindName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFindName.MaxLength = 255;
            this.txtFindName.Name = "txtFindName";
            this.txtFindName.PlaceholderText = "";
            this.txtFindName.SelectedText = "";
            this.txtFindName.Size = new System.Drawing.Size(231, 36);
            this.txtFindName.TabIndex = 3;
            // 
            // btnFind
            // 
            this.btnFind.BorderRadius = 10;
            this.btnFind.BorderThickness = 1;
            this.btnFind.FillColor = System.Drawing.Color.LightGray;
            this.btnFind.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.btnFind.ForeColor = System.Drawing.Color.Black;
            this.btnFind.Location = new System.Drawing.Point(12, 130);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(231, 30);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // btnReset
            // 
            this.btnReset.BorderRadius = 10;
            this.btnReset.BorderThickness = 1;
            this.btnReset.FillColor = System.Drawing.Color.LightGray;
            this.btnReset.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(12, 170);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(231, 30);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Xem tất cả";
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // grbCRUD
            // 
            this.grbCRUD.BorderRadius = 10;
            this.grbCRUD.Controls.Add(this.btnAdd);
            this.grbCRUD.Controls.Add(this.btnUpdate);
            this.grbCRUD.Controls.Add(this.btnDelete);
            this.grbCRUD.FillColor = System.Drawing.SystemColors.MenuBar;
            this.grbCRUD.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.grbCRUD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.grbCRUD.Location = new System.Drawing.Point(23, 10);
            this.grbCRUD.Name = "grbCRUD";
            this.grbCRUD.Size = new System.Drawing.Size(255, 180);
            this.grbCRUD.TabIndex = 10;
            this.grbCRUD.Text = "Thêm - Sửa - Xóa phòng ban";
            this.grbCRUD.Click += new System.EventHandler(this.grbCRUD_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BorderRadius = 10;
            this.btnAdd.BorderThickness = 1;
            this.btnAdd.FillColor = System.Drawing.Color.LightGray;
            this.btnAdd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(12, 55);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(231, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Tạo phòng ban";
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BorderRadius = 10;
            this.btnUpdate.BorderThickness = 1;
            this.btnUpdate.FillColor = System.Drawing.Color.LightGray;
            this.btnUpdate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnUpdate.Location = new System.Drawing.Point(12, 95);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(231, 30);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BorderRadius = 10;
            this.btnDelete.BorderThickness = 1;
            this.btnDelete.FillColor = System.Drawing.Color.LightGray;
            this.btnDelete.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(12, 135);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(231, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // guna2PanelMain
            // 
            this.guna2PanelMain.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.guna2PanelMain.Controls.Add(this.guna2HtmlLabelTen);
            this.guna2PanelMain.Controls.Add(this.txtTenPhongBan);
            this.guna2PanelMain.Controls.Add(this.guna2HtmlLabelMoTa);
            this.guna2PanelMain.Controls.Add(this.txtMota);
            this.guna2PanelMain.Controls.Add(this.dgvPhongBan);
            this.guna2PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2PanelMain.Location = new System.Drawing.Point(300, 62);
            this.guna2PanelMain.Name = "guna2PanelMain";
            this.guna2PanelMain.Size = new System.Drawing.Size(1164, 600);
            this.guna2PanelMain.TabIndex = 2;
            // 
            // guna2HtmlLabelTen
            // 
            this.guna2HtmlLabelTen.AutoSize = false;
            this.guna2HtmlLabelTen.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabelTen.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.guna2HtmlLabelTen.Location = new System.Drawing.Point(30, 30);
            this.guna2HtmlLabelTen.Name = "guna2HtmlLabelTen";
            this.guna2HtmlLabelTen.Size = new System.Drawing.Size(200, 29);
            this.guna2HtmlLabelTen.TabIndex = 10;
            this.guna2HtmlLabelTen.TabStop = false;
            this.guna2HtmlLabelTen.Text = "Tên phòng ban";
            // 
            // txtTenPhongBan
            // 
            this.txtTenPhongBan.BorderRadius = 7;
            this.txtTenPhongBan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenPhongBan.DefaultText = "";
            this.txtTenPhongBan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenPhongBan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenPhongBan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenPhongBan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenPhongBan.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.txtTenPhongBan.ForeColor = System.Drawing.Color.Black;
            this.txtTenPhongBan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenPhongBan.Location = new System.Drawing.Point(250, 26);
            this.txtTenPhongBan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenPhongBan.MaxLength = 255;
            this.txtTenPhongBan.Name = "txtTenPhongBan";
            this.txtTenPhongBan.PlaceholderText = "";
            this.txtTenPhongBan.SelectedText = "";
            this.txtTenPhongBan.Size = new System.Drawing.Size(854, 36);
            this.txtTenPhongBan.TabIndex = 6;
            // 
            // guna2HtmlLabelMoTa
            // 
            this.guna2HtmlLabelMoTa.AutoSize = false;
            this.guna2HtmlLabelMoTa.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabelMoTa.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.guna2HtmlLabelMoTa.Location = new System.Drawing.Point(30, 90);
            this.guna2HtmlLabelMoTa.Name = "guna2HtmlLabelMoTa";
            this.guna2HtmlLabelMoTa.Size = new System.Drawing.Size(200, 29);
            this.guna2HtmlLabelMoTa.TabIndex = 11;
            this.guna2HtmlLabelMoTa.TabStop = false;
            this.guna2HtmlLabelMoTa.Text = "Mô tả";
            // 
            // txtMota
            // 
            this.txtMota.BorderRadius = 7;
            this.txtMota.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMota.DefaultText = "";
            this.txtMota.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMota.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMota.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMota.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMota.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.txtMota.ForeColor = System.Drawing.Color.Black;
            this.txtMota.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMota.Location = new System.Drawing.Point(250, 86);
            this.txtMota.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMota.MaxLength = 255;
            this.txtMota.Multiline = true;
            this.txtMota.Name = "txtMota";
            this.txtMota.PlaceholderText = "";
            this.txtMota.SelectedText = "";
            this.txtMota.Size = new System.Drawing.Size(854, 90);
            this.txtMota.TabIndex = 7;
            // 
            // dgvPhongBan
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dgvPhongBan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhongBan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPhongBan.ColumnHeadersHeight = 30;
            this.dgvPhongBan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhongBan.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPhongBan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvPhongBan.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPhongBan.Location = new System.Drawing.Point(0, 220);
            this.dgvPhongBan.MultiSelect = false;
            this.dgvPhongBan.Name = "dgvPhongBan";
            this.dgvPhongBan.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhongBan.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvPhongBan.RowHeadersVisible = false;
            this.dgvPhongBan.RowHeadersWidth = 51;
            this.dgvPhongBan.RowTemplate.Height = 24;
            this.dgvPhongBan.Size = new System.Drawing.Size(1164, 380);
            this.dgvPhongBan.TabIndex = 12;
            this.dgvPhongBan.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPhongBan.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvPhongBan.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvPhongBan.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvPhongBan.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvPhongBan.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvPhongBan.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPhongBan.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvPhongBan.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPhongBan.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPhongBan.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPhongBan.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvPhongBan.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvPhongBan.ThemeStyle.ReadOnly = true;
            this.dgvPhongBan.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPhongBan.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPhongBan.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPhongBan.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvPhongBan.ThemeStyle.RowsStyle.Height = 24;
            this.dgvPhongBan.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPhongBan.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvPhongBan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_CellClick);
            // 
            // ucPhongBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2PanelMain);
            this.Controls.Add(this.guna2PanelLeft);
            this.Controls.Add(this.guna2HtmlLabelHeader);
            this.Name = "ucPhongBan";
            this.Size = new System.Drawing.Size(1464, 662);
            this.Load += new System.EventHandler(this.ucPhongBan_Load);
            this.guna2PanelLeft.ResumeLayout(false);
            this.grpFind.ResumeLayout(false);
            this.grpFind.PerformLayout();
            this.grbCRUD.ResumeLayout(false);
            this.guna2PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongBan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabelHeader;
        private Guna.UI2.WinForms.Guna2Panel guna2PanelLeft;
        private Guna.UI2.WinForms.Guna2GroupBox grbCRUD;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnUpdate;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2GroupBox grpFind;
        private System.Windows.Forms.Label lblFindName;
        private Guna.UI2.WinForms.Guna2TextBox txtFindName;
        private Guna.UI2.WinForms.Guna2Button btnFind;
        private Guna.UI2.WinForms.Guna2Button btnReset;
        private Guna.UI2.WinForms.Guna2Panel guna2PanelMain;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabelTen;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabelMoTa;
        private Guna.UI2.WinForms.Guna2TextBox txtTenPhongBan;
        private Guna.UI2.WinForms.Guna2TextBox txtMota;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPhongBan;
    }
}