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
            _userControls["ButtonFeatureHomeComponent"] = new ButtonFeatureHomeComponent(pnContent, _idNV, _stringConnection);
            _userControls["ButtonFeatureViewComponent"] = new ButtonFeatureViewComponent(pnContent, _idNV, _stringConnection);
            _userControls["ButtonFeatureCRUDComponent"] = new ButtonFeatureCRUDComponent(pnContent, _idNV, _stringConnection);
            _userControls["ButtonFeatureReportComponent"] = new ButtonFeatureReportComponent(pnContent, _idNV, _stringConnection);
            _userControls["ButtonFeatureSystemComponent"] = new ButtonFeatureSystemComponent(pnContent, _idNV, _stringConnection);

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
            ChildFormComponent(tpSystem, "ButtonFeatureSystemComponent");
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
            ChildFormComponent(tpSystem, "ButtonFeatureSystemComponent");
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