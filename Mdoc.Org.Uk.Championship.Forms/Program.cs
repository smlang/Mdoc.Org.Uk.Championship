using System;
using System.Windows.Forms;

namespace Mdoc.Org.Uk.Championship.Forms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            catch
            {
                //Ignore
            }
        }
    }
}
