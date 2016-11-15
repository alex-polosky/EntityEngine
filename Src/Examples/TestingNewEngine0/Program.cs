using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TestingNewEngine0
{
    static class Program
    {
        private static Form1 form;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            form = new Form1();
            Application.Run(form);

            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
