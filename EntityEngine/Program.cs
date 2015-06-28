using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EntityEngine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Game1_Form g = new Game1_Form();

            Application.Run(g);
            //Application.Run(new Embedded(g));
        }
    }
}
