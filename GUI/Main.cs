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
            pnMain.Controls.Clear();
            pnMain.Controls.Add(user);

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

            // Them tat ca UserControl vao pnMain nhung an di
            foreach (var control in _userControls.Values)
            {
                control.Visible = false;
                control.Dock = DockStyle.Fill;
                pnMain.Controls.Add(control);
            }

            ShowUserControl("ButtonFeatureHomeComponent");
        }

        // Hien thi UserControl tren Panel chinh
        // Chỉ khởi tạo khi cần
        public void ShowUserControl(string controlName)
        {
            pnMain.SuspendLayout();
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
                        pnMain.Controls.Add(user);
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
                pnMain.ResumeLayout();

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
    }
}
