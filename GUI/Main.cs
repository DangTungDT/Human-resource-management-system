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
            _userControls["UCHopDong"] = new UCHopDong(_idNV, _stringConnection);
            _userControls["ucChucVu"] = new ucChucVu(_stringConnection);
            _userControls["UCDuyetNghiPhep"] = new UCDuyetNghiPhep(_idNV, _stringConnection);
            _userControls["UCCapNhatMatKhau"] = new UCCapNhatMatkhau(_idNV, _stringConnection);
            _userControls["UCDanhGiaHieuSuat"] = new UCDanhGiaHieuSuat(_idNV, _stringConnection);
            _userControls["UCChiTietLuongCaNhan"] = new UCChiTietluongCaNhan(_idNV, _stringConnection);
            _userControls["ButtonFeatureHomeComponent"] = new ButtonFeatureHomeComponent(tpHome, _idNV, _stringConnection);
            _userControls["ButtonFeatureViewComponent"] = new ButtonFeatureViewComponent(tpView, _idNV, _stringConnection);
            _userControls["ButtonFeatureReportComponent"] = new ButtonFeatureReportComponent(tpReport, _idNV, _stringConnection);
            _userControls["ButtonFeatureCRUDComponent"] = new ButtonFeatureCRUDComponent(tpCRUD, _idNV, _stringConnection);
            _userControls["BaoCaoHopDong"] = new BaoCaoHopDong(_idNV, _stringConnection);
            _userControls["BaoCaoKhenThuong"] = new BaoCaoKhenThuong(_idNV, _stringConnection);
            _userControls["CapNhatThongTinNV"] = new CapNhatThongTinNV(_idNV, _stringConnection);
            _userControls["CapNhatThongTinRieng"] = new CapNhatThongTinRieng(_idNV, tpView, _stringConnection);
            _userControls["CRUDChucVu"] = new CRUDChucVu(_idNV, _stringConnection);
            _userControls["CRUDPhongBan"] = new CRUDPhongban(_idNV, _stringConnection);
            _userControls["CRUDTaiKhoan"] = new CRUDTaiKhoan(_idNV, _stringConnection);
            _userControls["ucChamCongQuanLyHinh"] = new ucChamCongQuanLy(_idNV, true);
            _userControls["ucChamCongQuanLy"] = new ucChamCongQuanLy(_idNV);
            _userControls["ucChiTietLuong"] = new ucChiTietLuong(_idNV, _stringConnection);
            _userControls["ucChiTietLuongCaNhan"] = new UCChiTietluongCaNhan(_idNV, _stringConnection);
            _userControls["UCDoiMatKhau"] = new UCDoiMatKhau();
            _userControls["ucDuyenTuyenDung"] = new ucDuyenTuyenDung();
            _userControls["UCHopDong"] = new UCHopDong(_idNV, _stringConnection);
            _userControls["ucKyLuong"] = new ucKyLuong(_idNV, _stringConnection);
            _userControls["UCNghiPhep"] = new UCNghiPhep(_idNV, _stringConnection);
            _userControls["ucTaoTuyenDung"] = new ucTaoTuyenDung();
            _userControls["ucUngVien"] = new ucUngVien(_idNV, _stringConnection);
            _userControls["ucXemChamCong"] = new ucXemChamCong(_idNV, _stringConnection);
            _userControls["UCXemKyLuat"] = new UCXemKyLuat(_idNV, _stringConnection);
            _userControls["ucXemTuyenDung"] = new ucXemTuyenDung(_idNV, _stringConnection);
            _userControls["XemNghiPhep"] = new XemNghiPhep(_stringConnection, _idNV);
            _userControls["XemThongTinCaNhan"] = new XemThongTinCaNhan(_idNV, tpView, _stringConnection);
            _userControls["TaoKhenThuong"] = new TaoKhenThuong(_stringConnection);
            _userControls["TaoKyLuat"] = new TaoKyLuat(_idNV, _stringConnection);
            _userControls["TaoThuongPhat"] = new TaoThuongPhat(_stringConnection);
            _userControls["TaoPhuCap"] = new TaoPhuCap(_stringConnection);
            _userControls["TaoNhanVien_KhauTru"] = new TaoNhanVien_KhauTru(_stringConnection);
            _userControls["TaoDanhGiaHieuSuat"] = new TaoDanhGiaHieuSuat(_idNV, _stringConnection);
            _userControls["UCReportDanhSachLuongPBan"] = new UCReportDanhSachLuongPBan(_idNV, _stringConnection);
            _userControls["UCReportDanhSachKyLuat"] = new UCReportDanhSachKyLuat(_idNV, _stringConnection);
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
                            user = new UCHopDong(_idNV, _stringConnection);
                            break;
                        case "ucChucVu":
                            user = new ucChucVu(_stringConnection);
                            break;
                        case "UCDuyetNghiPhep":
                            user = new UCDuyetNghiPhep(_idNV, _stringConnection);
                            break;
                        case "UCCapNhatMatKhau":
                            user = new UCCapNhatMatkhau(_idNV, _stringConnection);
                            break;
                        case "UCDanhGiaHieuSuat":
                            user = new UCDanhGiaHieuSuat(_idNV, _stringConnection);
                            break;
                        case "UCChiTietLuongCaNhan":
                            user = new UCChiTietluongCaNhan(_idNV, _stringConnection);
                            break;
                        case "ButtonFeatureHomeComponent":
                            user = new ButtonFeatureHomeComponent(tpHome, _idNV, _stringConnection);
                            break;
                        case "ButtonFeatureViewComponent":
                            user = new ButtonFeatureViewComponent(tpView, _idNV, _stringConnection);
                            break;
                        case "BaoCaoHopDong":
                            user = new BaoCaoHopDong(_idNV, _stringConnection);
                            break;
                        case "BaoCaoKhenThuong":
                            user = new BaoCaoKhenThuong(_idNV, _stringConnection);
                            break;
                        case "CapNhatThongTinNV":
                            user = new CapNhatThongTinNV(_idNV, _stringConnection);
                            break;
                        case "CapNhatThongTinRieng":
                            user = new CapNhatThongTinRieng(_idNV, tpView, _stringConnection);
                            break;
                        case "CRUDChucVu":
                            user = new CRUDChucVu(_idNV, _stringConnection);
                            break;
                        case "CRUDPhongBan":
                            user = new CRUDPhongban(_idNV, _stringConnection);
                            break;
                        case "CRUDTaiKhoan":
                            user = new CRUDTaiKhoan(_idNV, _stringConnection);
                            break;
                        case "ucChamCongQuanLyHinh":
                            user = new ucChamCongQuanLy(_idNV, true);
                            break;
                        case "ucChamCongQuanLy":
                            user = new ucChamCongQuanLy(_idNV);
                            break;
                        case "ucChiTietLuong":
                            user = new ucChiTietLuong(_idNV, _stringConnection);
                            break;
                        case "UCDoiMatKhau":
                            user = new UCDoiMatKhau();
                            break;
                        case "ucDuyenTuyenDung":
                            user = new ucDuyenTuyenDung();
                            break;
                        case "ucKyLuong":
                            user = new ucKyLuong(_idNV, _stringConnection);
                            break;
                        case "UCNghiPhep":
                            user = new UCNghiPhep(_idNV, _stringConnection);
                            break;
                        case "ucTaoTuyenDung":
                            user = new ucTaoTuyenDung();
                            break;
                        case "ucUngVien":
                            user = new ucUngVien(_idNV, _stringConnection);
                            break;
                        case "ucXemChamCong":
                            user = new ucXemChamCong(_idNV, _stringConnection);
                            break;
                        case "UCXemKyLuat":
                            user = new UCXemKyLuat(_idNV, _stringConnection);
                            break;
                        case "ucXemTuyenDung":
                            user = new ucXemTuyenDung(_idNV, _stringConnection);
                            break;
                        case "XemNghiPhep":
                            user = new XemNghiPhep(_idNV, _stringConnection);
                            break;
                        case "XemThongTinCaNhan":
                            user = new XemThongTinCaNhan(_idNV, tpView, _stringConnection);
                            break;
                        case "TaoKyLuat":
                            user = new TaoKyLuat(_idNV, _stringConnection);
                            break;
                        case "TaoKhenThuong":
                            user = new TaoKhenThuong(_stringConnection);
                            break;
                        case "UCReportDanhSachLuongPBan":
                            user = new UCReportDanhSachLuongPBan(_idNV, _stringConnection);
                            break;
                        case "UCReportDanhSachKyLuat":
                            user = new UCReportDanhSachKyLuat(_idNV, _stringConnection);
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
