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
    public partial class ButtonFeatureCRUDComponent : UserControl
    {
        private Panel _tpCRUD;
        public ButtonFeatureCRUDComponent(Panel tpCRUD)
        {
            InitializeComponent();
            _tpCRUD = tpCRUD;
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("UCHopDong");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void btnKyLuat_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("CRUDTaiKhoan");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("CRUDPhongBan");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("CRUDChucVu");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("ucKyLuong");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("ucChiTietLuong");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void guna2TileButton5_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("ucUngVien");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void guna2TileButton6_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("TaoDanhGiaHieuSuat");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void btnTaoKyLuat_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("TaoKyLuat");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void btnTaoKhenThuong_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("TaoKhenThuong");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void btnCapNhatThongTinNV_Click(object sender, EventArgs e)
        {
            var main = this.ParentForm as Main;
            main?.ShowUserControl("CapNhatThongTinNV");
            main.ChildFormComponent(_tpCRUD, "ButtonFeatureCRUDComponent");
        }
    }
}
