using System;
using System.Windows.Forms;
using BLL;
using DTO;

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
            Application.Run(new FormThemHopDong());
            //Application.Run(new TestGiaoDien("TPCNTT0001"));


        }
    }
}
