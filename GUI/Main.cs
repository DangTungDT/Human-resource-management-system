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

        public readonly string _idNV;
        public readonly string _stringConnection;

        public Main() { }

        public Main(string idNV, string stringConnection)
        {
            this.DoubleBuffered = true;

            _idNV = idNV;
            _stringConnection = stringConnection;

            InitializeComponent();
            PreLoadUserControl();
        }

        // hien thi UserControl trong panel
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

        public void DisplayUserControl(Panel panel, string controlName)
        {
            panel.SuspendLayout();
            try
            {
                if (_userControls.ContainsKey(controlName))
                {

                    panel.Controls.Clear();
                    var user = _userControls[controlName];
                    panel.Controls.Add(user);

                    user.Visible = true;
                    user.BringToFront();
                    user.Dock = DockStyle.Fill;
                    user.Show();
                }
            }
            finally
            {
                panel.ResumeLayout();
            }
        }


        // Tai truoc va luu tru UserControl
        public void PreLoadUserControl()
        {
            _userControls["UCHopDong"] = new UCHopDong(_stringConnection);
            _userControls["ucChucVu"] = new ucChucVu(_stringConnection);
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
            _userControls["TaoThuongPhat"] = new TaoThuongPhat(_stringConnection);
            _userControls["TaoPhuCap"] = new TaoPhuCap(_stringConnection);
            _userControls["TaoNhanVien_KhauTru"] = new TaoNhanVien_KhauTru(_stringConnection);
            _userControls["TaoDanhGiaHieuSuat"] = new TaoDanhGiaHieuSuat(_stringConnection);
            _userControls["UCReportDanhSachLuongPBan"] = new UCReportDanhSachLuongPBan();
            _userControls["UCReportDanhSachKyLuat"] = new UCReportDanhSachKyLuat();
            _userControls["ButtonFeatureHomeComponent"] = new ButtonFeatureHomeComponent(pnContent, _idNV, _stringConnection);
            _userControls["ButtonFeatureViewComponent"] = new ButtonFeatureViewComponent(pnContent, _idNV, _stringConnection);
            _userControls["ButtonFeatureCRUDComponent"] = new ButtonFeatureCRUDComponent(pnContent, _idNV, _stringConnection);
            _userControls["ButtonFeatureReportComponent"] = new ButtonFeatureReportComponent(pnContent, _idNV, _stringConnection);

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
                        case "ucChucVu":
                            user = new ucChucVu(_stringConnection);
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
                            user = new XemNghiPhep(_stringConnection);
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

            if (_idNV.Contains("NV"))
            {
                var rp = tcMenu.TabPages["tpReport"];
                var ql = tcMenu.TabPages["tpCRUD"];

                tcMenu.TabPages.Remove(rp);
                tcMenu.TabPages.Remove(ql);
            }
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
            ChildFormComponent(tpView, "ButtonFeatureViewComponent");
            ChildFormComponent(tpHome, "ButtonFeatureHomeComponent");
            ChildFormComponent(tpCRUD, "ButtonFeatureCRUDComponent");
            ChildFormComponent(tpReport, "ButtonFeatureReportComponent");
        }

        private void Report_Click(object sender, EventArgs e)
        {
            ChildFormComponent(tpReport, "ButtonFeatureReportComponent");
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

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        // Tat han chuong trinh sau khi an form Main -> khoi thong qua login
        private void Main_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();
    }
}
