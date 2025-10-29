using CrystalDecisions.Shared;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Windows.Forms;

namespace GUI
{
    public partial class ButtonFeatureCRUDComponent : UserControl
    {
        private readonly Panel _tpCRUD;
        private readonly string _idNhanVien, _conn;

        public ButtonFeatureCRUDComponent(Panel tpCRUD, string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _tpCRUD = tpCRUD;
            _idNhanVien = idNhanVien;
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
            UCHopDong uc = new UCHopDong(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void btnKyLuat_Click(object sender, EventArgs e)
        {
            CRUDTaiKhoan uc = new CRUDTaiKhoan(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void btnPhongBan_Click(object sender, EventArgs e)
        {
            CRUDPhongban uc = new CRUDPhongban(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }
        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            CRUDChucVu uc = new CRUDChucVu(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }
        private void btnChucVu_Click(object sender, EventArgs e)
        {
            ucChucVu uc = new ucChucVu(_conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            ucKyLuong uc = new ucKyLuong(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            ucChiTietLuong uc = new ucChiTietLuong(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void guna2TileButton5_Click(object sender, EventArgs e)
        {
            ucUngVien uc = new ucUngVien(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void guna2TileButton6_Click(object sender, EventArgs e)
        {
            TaoDanhGiaHieuSuat uc = new TaoDanhGiaHieuSuat(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void btnTaoKyLuat_Click(object sender, EventArgs e)
        {
            TaoKyLuat uc = new TaoKyLuat(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void btnTaoKhenThuong_Click(object sender, EventArgs e)
        {
            TaoKhenThuong uc = new TaoKhenThuong(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void btnCapNhatThongTinNV_Click(object sender, EventArgs e)
        {
            CapNhatThongTinNV uc = new CapNhatThongTinNV(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void btnTaoThuongPhat_Click_1(object sender, EventArgs e)
        {
            TaoThuongPhat uc = new TaoThuongPhat(_conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void btnTaoPhuCap_Click(object sender, EventArgs e)
        {
            TaoPhuCap uc = new TaoPhuCap(_conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void ButtonFeatureCRUDComponent_Load(object sender, EventArgs e)
        {
            if (_idNhanVien.Contains("GD") || _idNhanVien.Contains("TPNS"))
            {
                guna2TileButton2.Visible = true;
                guna2TileButton3.Visible = true;
            }
            else if (_idNhanVien.Contains("NVNS"))
            {
                foreach (var control in flowLayoutPanel1.Controls)
                {
                    if (control is Guna2TileButton button)
                    {
                        if (button.Name == "guna2TileButton2" || button.Name == "guna2TileButton3")
                        {
                            button.Visible = true;
                            continue;
                        }

                        button.Visible = false;
                    }
                }
            }
        }

        private void btnTPTuyenDung_Click(object sender, EventArgs e)
        {
            UCTuyenDung_TruongPhong uc = new UCTuyenDung_TruongPhong(_idNhanVien, _conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }

        private void btnTaoNhanVien_KhauTru_Click(object sender, EventArgs e)
        {
            TaoNhanVien_KhauTru uc = new TaoNhanVien_KhauTru(_conn);
            DisplayUserControlPanel.ChildUserControl(uc, _tpCRUD);
        }
    }
}
