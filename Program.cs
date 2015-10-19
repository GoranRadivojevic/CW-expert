using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace CWExpert
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
                if (!File.Exists(Application.StartupPath + "\\wisdom"))
                {
                    if (File.Exists(Application.StartupPath + "\\fftw_wisdom.exe"))
                    {
                        Process p = Process.Start(Application.StartupPath + "\\fftw_wisdom.exe", ".\\");
                        MessageBox.Show("Running one time optimization.  Please wait patiently for " +
                            "this process to finish.\n",
                            "Optimizing...",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        p.WaitForExit();
                    }
                    else
                    {
                        MessageBox.Show("Error:missing fftw_wisdom.exe!", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CWExpert());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Fatal error!");
            }
        }
    }
}
