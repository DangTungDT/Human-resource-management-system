using Guna.UI2.WinForms;
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
    public partial class Main : Form
    {
        private Dictionary<string, UserControl> _userControls = new Dictionary<string, UserControl>();

        string idNV = "GD00000001";
        public Main()
        {
            InitializeComponent();
            this.DoubleBuffered = false;
            PreLoadUserControl();
        }

        // hien thi UserContrl trong panel
        public void ChildFormComponent(Panel pn, string controlName)
        {
            pn.SuspendLayout();
            try
            {
                if (_userControls.ContainsKey(controlName))
                {

                    pn.Controls.Clear();
                    var user = _userControls[controlName];
                    pn.Controls.Add(user);

                    user.Visible = true;
                    user.BringToFront();
                    user.Dock = DockStyle.Fill;
                    user.Show();
                }
            }
            finally
            {
                pn.ResumeLayout();
            }

        }

        public void ChildFormMain(UserControl user)
        {
            pnContent.Controls.Clear();
            pnContent.Controls.Add(user);

            user.BringToFront();
            user.Dock = DockStyle.Fill;
            user.Show();
        }

        // Tai truoc va luu tru UserControl
        public void PreLoadUserControl()
        {
            _userControls["UCHopDong"] = new UCHopDong();
            _userControls["UCNghiPhep"] = new UCNghiPhep();
            _userControls["UCXemKyLuat"] = new UCXemKyLuat();
            _userControls["UCDuyetNghiPhep"] = new UCDuyetNghiPhep();
            _userControls["UCCapNhatMatKhau"] = new UCCapNhatMatkhau();
            _userControls["UCDanhGiaHieuSuat"] = new UCDanhGiaHieuSuat();
            _userControls["UCChiTietLuongCaNhan"] = new UCChiTietluongCaNhan();
            _userControls["ButtonFeatureHomeComponent"] = new ButtonFeatureHomeComponent(tpHome);
            _userControls["ButtonFeatureViewComponent"] = new ButtonFeatureViewComponent(tpView);

            // Them tat ca UserControl vao pnContent nhung an di
            foreach (var control in _userControls.Values)
            {
                control.Visible = false;
                control.Dock = DockStyle.Fill;
                pnContent.Controls.Add(control);
            }

            ShowUserControl("ButtonFeatureHomeComponent");
        }

        // Hien thi UserControl tren Panel chinh
        // Chỉ khởi tạo khi cần
        public void ShowUserControl(string controlName)
        {
            pnContent.SuspendLayout();
            try
            {
                if (!_userControls.ContainsKey(controlName))
                {
                    UserControl user = null;
                    switch (controlName)
                    {
                        case "UCNghiPhep":
                            user = new UCNghiPhep();
                            break;
                        case "UCDuyetNghiPhep":
                            user = new UCDuyetNghiPhep();
                            break;
                        case "UCCapNhatMatKhau":
                            user = new UCCapNhatMatkhau();
                            break;
                        case "UCChiTietLuongCaNhan":
                            user = new UCChiTietluongCaNhan();
                            break;
                        case "ButtonFeatureHomeComponent":
                            user = new ButtonFeatureHomeComponent(tpHome);
                            break;
                        case "ButtonFeatureViewComponent":
                            user = new ButtonFeatureViewComponent(tpView);
                            break;
                        case "UCDanhGiaHieuSuat":
                            user = new ButtonFeatureViewComponent(tpView);
                            break;
                        case "UCXemKyLuat":
                            user = new ButtonFeatureViewComponent(tpView);
                            break;
                        case "UCHopDong":
                            user = new ButtonFeatureHomeComponent(tpHome);
                            break;
                        default:
                            user = null;
                            break;
                    }

                    if (user != null)
                    {
                        user.Dock = DockStyle.Fill;
                        _userControls[controlName] = user;
                        pnContent.Controls.Add(user);
                    }
                }

                foreach (var control in _userControls.Values)
                {
                    control.Visible = false;
                }

                if (_userControls.ContainsKey(controlName))
                {
                    _userControls[controlName].Visible = true;
                    _userControls[controlName].BringToFront();
                }
            }
            finally
            {
                pnContent.ResumeLayout();

            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ChildFormComponent(tpHome, "ButtonFeatureHomeComponent");
        }

        private void tpView_Click(object sender, EventArgs e)
        {

            ChildFormComponent(tpView, "ButtonFeatureViewComponent");
        }

        private void tpHome_Click(object sender, EventArgs e)
        {

            ChildFormComponent(tpView, "ButtonFeatureViewComponent");

        }

        private void tcMenu_Click(object sender, EventArgs e)
        {
            ChildFormComponent(tpHome, "ButtonFeatureHomeComponent");
            ChildFormComponent(tpView, "ButtonFeatureViewComponent");

        }

        private void LoadControl(UserControl uc)
        {
            pnContent.Controls.Clear();   // xóa control cũ
            uc.Dock = DockStyle.Fill;     // cho UserControl chiếm hết panel
            pnContent.Controls.Add(uc);   // thêm control mới
        }

        private void btnCapNhatTTNV_Click(object sender, EventArgs e)
        {
            LoadControl(new CapNhatThongTinNV());
        }

        private void btnTaoDanhGiaHieuSuat_Click(object sender, EventArgs e)
        {
            LoadControl(new TaoDanhGiaHieuSuat());
        }

        private void btnTaoKyLuat_Click(object sender, EventArgs e)
        {
            LoadControl(new TaoKyLuat());
        }

        private void btnTaoKhenThuong_Click(object sender, EventArgs e)
        {
            LoadControl(new TaoKhenThuong());
        }

        private void btnXemNghiPhep_Click(object sender, EventArgs e)
        {
            LoadControl(new XemNghiPhep());
        }

        private void btnXemThongTinCaNhan_Click(object sender, EventArgs e)
        {
            LoadControl(new XemThongTinCaNhan(idNV));
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LoadControl(new CapNhatThongTinRieng(idNV));
        }

        private void btnTaoHDLD_Click(object sender, EventArgs e)
        {
            LoadControl(new BaoCaoHopDong());
        }

        private void btnCRUDTaiKhoan_Click(object sender, EventArgs e)
        {
            LoadControl(new CRUDTaiKhoan());
        }

        private void btnCRUDPhongBan_Click(object sender, EventArgs e)
        {
            LoadControl(new CRUDPhongban());
        }

        private void btnCRUDChucVu_Click(object sender, EventArgs e)
        {
            LoadControl(new CRUDChucVu());
        }

        private void btnBaoCaoKhenThuong_Click(object sender, EventArgs e)
        {
            LoadControl(new BaoCaoKhenThuong());
        private void OpenChildControl(UserControl uc)
        {
            pnContent.Padding = new Padding(0, 10, 0, 0);
            pnContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnContent.Controls.Add(uc);
            uc.BringToFront();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            OpenChildControl(new ucXemTuyenDung());
        }
    }
}
