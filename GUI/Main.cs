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
        string _stringConnection = "";
        public Main(string stringConnection)
        {
            _stringConnection = stringConnection;
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
            _userControls["UCHopDong"] = new UCHopDong(_stringConnection);
            _userControls["UCDuyetNghiPhep"] = new UCDuyetNghiPhep();
            _userControls["UCCapNhatMatKhau"] = new UCCapNhatMatkhau();
            _userControls["UCDanhGiaHieuSuat"] = new UCDanhGiaHieuSuat(_stringConnection);
            _userControls["UCChiTietLuongCaNhan"] = new UCChiTietluongCaNhan();
            _userControls["ButtonFeatureHomeComponent"] = new ButtonFeatureHomeComponent(tpHome);
            _userControls["ButtonFeatureViewComponent"] = new ButtonFeatureViewComponent(tpView);
            _userControls["ButtonFeatureReportComponent"] = new ButtonFeatureReportComponent(tpReport);
            _userControls["ButtonFeatureCRUDComponent"] = new ButtonFeatureCRUDComponent(tpCRUD);
            _userControls["BaoCaoHopDong"] = new BaoCaoHopDong(_stringConnection);
            _userControls["BaoCaoKhenThuong"] = new BaoCaoKhenThuong(_stringConnection);
            _userControls["CapNhatThongTinNV"] = new CapNhatThongTinNV(_stringConnection);
            _userControls["CapNhatThongTinRieng"] = new CapNhatThongTinRieng(idNV, tpView, _stringConnection);
            _userControls["CRUDChucVu"] = new CRUDChucVu(_stringConnection);
            _userControls["CRUDPhongBan"] = new CRUDPhongban(_stringConnection);
            _userControls["CRUDTaiKhoan"] = new CRUDTaiKhoan(_stringConnection);
            _userControls["ucChamCongQuanLyHinh"] = new ucChamCongQuanLy(idNV, true);
            _userControls["ucChamCongQuanLy"] = new ucChamCongQuanLy(idNV);
            _userControls["ucChiTietLuong"] = new ucChiTietLuong();
            _userControls["ucChiTietLuongCaNhan"] = new UCChiTietluongCaNhan();
            _userControls["UCDoiMatKhau"] = new UCDoiMatKhau();
            _userControls["ucDuyenTuyenDung"] = new ucDuyenTuyenDung();
            _userControls["UCHopDong"] = new UCHopDong(_stringConnection);
            _userControls["ucKyLuong"] = new ucKyLuong();
            _userControls["UCNghiPhep"] = new UCNghiPhep();
            _userControls["ucTaoTuyenDung"] = new ucTaoTuyenDung();
            _userControls["ucUngVien"] = new ucUngVien();
            _userControls["ucXemChamCong"] = new ucXemChamCong();
            _userControls["UCXemKyLuat"] = new UCXemKyLuat(_stringConnection);
            _userControls["ucXemTuyenDung"] = new ucXemTuyenDung();
            _userControls["XemNghiPhep"] = new XemNghiPhep(_stringConnection, idNV);
            _userControls["XemThongTinCaNhan"] = new XemThongTinCaNhan(idNV, tpView, _stringConnection);
            _userControls["TaoKhenThuong"] = new TaoKhenThuong(_stringConnection);
            _userControls["TaoKyLuat"] = new TaoKyLuat(_stringConnection);
            _userControls["TaoDanhGiaHieuSuat"] = new TaoDanhGiaHieuSuat(_stringConnection);

            _userControls["UCReportDanhSachLuongPBan"] = new UCReportDanhSachLuongPBan();
            _userControls["UCReportDanhSachKyLuat"] = new UCReportDanhSachKyLuat();


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
                        case "UCHopDong":
                            user = new UCHopDong(_stringConnection);
                            break;
                        case "UCDuyetNghiPhep":
                            user = new UCDuyetNghiPhep();
                            break;
                        case "UCCapNhatMatKhau":
                            user = new UCCapNhatMatkhau();
                            break;
                        case "UCDanhGiaHieuSuat":
                            user = new UCDanhGiaHieuSuat(_stringConnection);
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
                        case "BaoCaoHopDong":
                            user = new BaoCaoHopDong(_stringConnection);
                            break;
                        case "BaoCaoKhenThuong":
                            user = new BaoCaoKhenThuong(_stringConnection);
                            break;
                        case "CapNhatThongTinNV":
                            user = new CapNhatThongTinNV(_stringConnection);
                            break;
                        case "CapNhatThongTinRieng":
                            user = new CapNhatThongTinRieng(idNV, tpView, _stringConnection);
                            break;
                        case "CRUDChucVu":
                            user = new CRUDChucVu(_stringConnection);
                            break;
                        case "CRUDPhongBan":
                            user = new CRUDPhongban(_stringConnection);
                            break;
                        case "CRUDTaiKhoan":
                            user = new CRUDTaiKhoan(_stringConnection);
                            break;
                        case "ucChamCongQuanLyHinh":
                            user = new ucChamCongQuanLy(idNV, true);
                            break;
                        case "ucChamCongQuanLy":
                            user = new ucChamCongQuanLy(idNV);
                            break;
                        case "ucChiTietLuong":
                            user = new ucChiTietLuong();
                            break;
                        case "UCDoiMatKhau":
                            user = new UCDoiMatKhau();
                            break;
                        case "ucDuyenTuyenDung":
                            user = new ucDuyenTuyenDung();
                            break;
                        case "ucKyLuong":
                            user = new ucKyLuong();
                            break;
                        case "UCNghiPhep":
                            user = new UCNghiPhep();
                            break;
                        case "ucTaoTuyenDung":
                            user = new ucTaoTuyenDung();
                            break;
                        case "ucUngVien":
                            user = new ucUngVien();
                            break;
                        case "ucXemChamCong":
                            user = new ucXemChamCong();
                            break;
                        case "UCXemKyLuat":
                            user = new UCXemKyLuat(_stringConnection);
                            break;
                        case "ucXemTuyenDung":
                            user = new ucXemTuyenDung();
                            break;
                        case "XemNghiPhep":
                            user = new XemNghiPhep(_stringConnection, idNV);
                            break;
                        case "XemThongTinCaNhan":
                            user = new XemThongTinCaNhan(idNV, tpView, _stringConnection);
                            break;
                        case "TaoKyLuat":
                            user = new TaoKyLuat(_stringConnection);
                            break;
                        case "TaoKhenThuong":
                            user = new TaoKhenThuong(_stringConnection);
                            break;
                        case "UCReportDanhSachLuongPBan":
                            user = new UCReportDanhSachLuongPBan();
                            break;
                        case "UCReportDanhSachKyLuat":
                            user = new UCReportDanhSachKyLuat();
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

                // Ẩn tất cả các control khác
                foreach (var control in _userControls.Values)
                {
                    control.Visible = false;
                }

                // Hiển thị control được chọn
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
            ChildFormComponent(tpReport, "ButtonFeatureReportComponent");
            ChildFormComponent(tpView, "ButtonFeatureViewComponent");
            ChildFormComponent(tpHome, "ButtonFeatureHomeComponent");
            ChildFormComponent(tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void Report_Click(object sender, EventArgs e)
        {
            ChildFormComponent(tpReport, "ButtonFeatureReportComponent");
        }

        private void LoadControl(UserControl uc)
        {
            pnContent.Controls.Clear();   // xóa control cũ
            uc.Dock = DockStyle.Fill;     // cho UserControl chiếm hết panel
            pnContent.Controls.Add(uc);   // thêm control mới
        }

        private void btnCapNhatTTNV_Click(object sender, EventArgs e)
        {
            LoadControl(new CapNhatThongTinNV(_stringConnection));
        }

        private void btnTaoDanhGiaHieuSuat_Click(object sender, EventArgs e)
        {
            LoadControl(new TaoDanhGiaHieuSuat(_stringConnection));
        }

        private void btnTaoKyLuat_Click(object sender, EventArgs e)
        {
            LoadControl(new TaoKyLuat(_stringConnection));
        }

        private void btnTaoKhenThuong_Click(object sender, EventArgs e)
        {
            LoadControl(new TaoKhenThuong(_stringConnection));
        }

        private void btnXemNghiPhep_Click(object sender, EventArgs e)
        {
            LoadControl(new XemNghiPhep(_stringConnection, idNV));
        }

        private void btnXemThongTinCaNhan_Click(object sender, EventArgs e)
        {
            LoadControl(new XemThongTinCaNhan(idNV, tpView, _stringConnection));
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LoadControl(new CapNhatThongTinRieng(idNV, tpView, _stringConnection));
        }

        private void btnTaoHDLD_Click(object sender, EventArgs e)
        {
            LoadControl(new BaoCaoHopDong(_stringConnection));
        }

        private void btnCRUDTaiKhoan_Click(object sender, EventArgs e)
        {
            LoadControl(new CRUDTaiKhoan(_stringConnection));
        }

        private void btnCRUDPhongBan_Click(object sender, EventArgs e)
        {
            LoadControl(new CRUDPhongban(_stringConnection));
        }

        private void btnCRUDChucVu_Click(object sender, EventArgs e)
        {
            LoadControl(new CRUDChucVu(_stringConnection));
        }

        private void btnBaoCaoKhenThuong_Click(object sender, EventArgs e)
        {
            LoadControl(new BaoCaoKhenThuong(_stringConnection));

        }
        private void OpenChildControl(UserControl uc)
        {
            pnContent.Padding = new Padding(0, 10, 0, 0);
            pnContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnContent.Controls.Add(uc);
            uc.BringToFront();
        }

        private void tpReport_Click(object sender, EventArgs e)
        {
            ChildFormComponent(tpReport, "ButtonFeatureReportComponent");
        }

        private void tpCRUD_Click(object sender, EventArgs e)
        {
            ChildFormComponent(tpCRUD, "ButtonFeatureCRUDComponent");
        }

        private void tpSystem_Click(object sender, EventArgs e)
        {

        }
    }
}
