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
    public partial class FormLogin : Form
    {
        public readonly string _idNhanVien, _conn;

        public FormLogin(string idNhanVien)
        {
            InitializeComponent();

            _idNhanVien = idNhanVien;
            _conn = ConnectionDB.TakeConnectionString();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new Main(_idNhanVien, _conn).Show();
            this.Hide();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
