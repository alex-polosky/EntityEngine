using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using EntityFramework.Engine;

namespace TestingNewEngine
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

            //GlobalEnvironment.GenerateFrameRate();
            //GlobalEnvironment.frameRate.Start();

            //GameLoop gl = new GameLoop(new Form1());

            //if (gl.Control == null)
            //    throw new ArgumentException("form");
            ////gl.Control.Show();
            //while (gl.NextFrame())
            //{
            //    GlobalEnvironment.frameRate.StartFrame();
            //    System.Threading.Thread.Sleep(1);
            //    GlobalEnvironment.frameRate.EndFrame();

            //    Console.WriteLine(GlobalEnvironment.frameRate.CurrentFPS);
            //}

            GameLoop gl = new GameLoop();
            int a = 0;
            int b = 0;
            gl.PreProcess += new EventHandler((sender, e) =>
            {
                a++;
            });
            gl.PostProcess += new EventHandler((sender, e) =>
            {
                if (a >= 1000)
                {
                    a = 0;
                    System.Threading.Thread.Sleep(1000);
                    b++;
                }
                if (b >= 10)
                    gl.IsControlAlive = false;
            });
            gl.ProcessFrame += new EventHandler((sender, e) =>
            {
                System.Threading.Thread.Sleep(1);
                Console.WriteLine(gl.FrameRate.CurrentFPS);
            });

            new Form1().Show();
            gl.RunLoop();

            //Application.Run();
        }
    }
}
