using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvInt.Win32.FieldTech.Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                using (new SingleGlobalInstance(1000))
                {
                    if (args.Count() > 0)
                    {
                        Application.Run(new MainForm(args[0]));
                    }
                    else
                    {
                        Application.Run(new MainForm());
                    }
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("The application is already open in another window!");
            }
        }
    }
}
