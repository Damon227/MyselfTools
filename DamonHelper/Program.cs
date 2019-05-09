using System;
using System.Windows.Forms;
using DamonHelper.Settings;

namespace DamonHelper
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!Config.InitConfig(Settings.Environment.Development.ToString())) //Production Development
            {
                return;
            }
            Application.Run(new Home());
        }
    }
}
