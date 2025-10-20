using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // GD00000001, TPCNTT0001, NVKD000004
            Application.Run(new FormLogin("NVKD000004"));
            //Application.Run(new TestGiaoDien("TPCNTT0001"));
        }
    }
}
