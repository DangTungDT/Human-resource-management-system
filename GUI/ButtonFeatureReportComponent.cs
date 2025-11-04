using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ButtonFeatureReportComponent : UserControl
    {
        private readonly Panel _tpReport;
        public readonly string _idNhanVien, _conn;

        public ButtonFeatureReportComponent(Panel tpHome, string idNhanVien, string conn)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            _conn = conn;
            _tpReport = tpHome;
            _idNhanVien = idNhanVien;
        }

        private void btnChiTietLuong_Click(object sender, EventArgs e)
        {
            FormBaoCaoChamCongCaNhan form = new FormBaoCaoChamCongCaNhan();
            form.ShowDialog();
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
            FormBaoCaoTuyenDung form = new FormBaoCaoTuyenDung();
            form.ShowDialog();
        }

        private void btnKyLuat_Click(object sender, EventArgs e)
        {
            BaoCaoHopDong uc = new BaoCaoHopDong(_conn, _idNhanVien);
            DisplayUserControlPanel.ChildUserControl(uc, _tpReport);
        }

        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            BaoCaoKhenThuong uc = new BaoCaoKhenThuong(_conn, _idNhanVien);
            DisplayUserControlPanel.ChildUserControl(uc, _tpReport);
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            UCReportDanhSachLuongPBan uc = new UCReportDanhSachLuongPBan(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpReport);
        }

        private void ButtonFeatureReportComponent_Load(object sender, EventArgs e)
        {

        }

        private void btnDSNV_Click(object sender, EventArgs e)
        {
            FormBaoCaoNhanVien baoCaoNV = new FormBaoCaoNhanVien(_idNhanVien, _conn);
            baoCaoNV.Show();
        }

        private void btnThongKeBCDGNV_Click(object sender, EventArgs e)
        {
            ThongKeDanhGia uc = new ThongKeDanhGia(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpReport);
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            UCReportDanhSachKyLuat uc = new UCReportDanhSachKyLuat(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpReport);
        }
    }
}
