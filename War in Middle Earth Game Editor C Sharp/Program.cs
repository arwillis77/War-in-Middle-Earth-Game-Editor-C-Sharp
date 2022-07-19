using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        
      
        [STAThread]
        static void Main()
        {
           
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new SplashScreen());
            //Application.Run(new frmWIMEEditorMain());
        }

      

    }
}
