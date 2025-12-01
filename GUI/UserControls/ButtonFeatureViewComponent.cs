using BLL;
using GUI.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ButtonFeatureViewComponent : UserControl
    {
        private readonly Panel _tpView;
        private readonly BLLNhanVien _bllNhanVien;
        public readonly string _idNhanVien, _conn;

        public ButtonFeatureViewComponent(Panel tpView, string idNhanVien, string conn)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            _conn = conn;
            _tpView = tpView;
            _idNhanVien = idNhanVien;
            _bllNhanVien = new BLLNhanVien(conn);
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
            UCDanhGiaHieuSuat uc = new UCDanhGiaHieuSuat(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpView);
        }

        private void btnChiTietLuong_Click(object sender, EventArgs e)
        {
            UCChiTietluongCaNhan uc = new UCChiTietluongCaNhan(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpView);
        }

        private void btnKyLuat_Click(object sender, EventArgs e)
        {
            UCXemKyLuat uc = new UCXemKyLuat(_conn, _idNhanVien);
            DisplayUserControlPanel.ChildUserControl(uc, _tpView);
        }

        private void btnThongTinCaNhan_Click(object sender, EventArgs e)
        {
            XemThongTinCaNhan uc = new XemThongTinCaNhan(_idNhanVien, _tpView, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpView);
        }

        private void btnNghiPhepCaNhan_Click(object sender, EventArgs e)
        {
            XemNghiPhep uc = new XemNghiPhep(_conn, _idNhanVien);
            DisplayUserControlPanel.ChildUserControl(uc, _tpView);
        }

        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            ucXemTuyenDung uc = new ucXemTuyenDung(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpView);
        }

        private void ButtonFeatureViewComponent_Load(object sender, EventArgs e)
        {
            if (_idNhanVien.Contains("NV"))
            {
                btnDanhGia.Visible = false;
                guna2TileButton3.Visible = false;
            }
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            int idDepartment = int.Parse(_bllNhanVien.GetStaffById(_idNhanVien).IdPhongBan);
            ucChamCongQuanLy uc = new ucChamCongQuanLy(_idNhanVien, idDepartment, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpView);
        }
    }
}
