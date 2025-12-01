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
    public partial class UCCapNhatMatkhau : UserControl
    {
        public readonly string _idNhanVien, _conn;
        public UCCapNhatMatkhau(string idNhanVien, string conn)
        {
            InitializeComponent();

            _conn = conn;
            _idNhanVien = idNhanVien;
        }


    }
}
