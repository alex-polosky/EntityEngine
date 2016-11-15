using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using EntityFramework;
using Engine = EntityFramework.Engine;
using Interfaces = EntityFramework.ComponentInterfaces;
using Render = EntityFramework.Render;

namespace WF_HC_Triangles
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

            WinFormExtension.EngineControl engine = new WinFormExtension.EngineControl();
            engine.Dock = DockStyle.Fill;
            engine.InitEngine();

            Form engineContainer = new Form();
            engineContainer.FormBorderStyle = FormBorderStyle.FixedSingle;
            engineContainer.Width = 800;
            engineContainer.Height = 600;
            engineContainer.MaximizeBox = false;
            engineContainer.SizeGripStyle = SizeGripStyle.Hide;
            engineContainer.Controls.Add(engine);
            engineContainer.Shown += new EventHandler((sender, e) => 
            {
                engine.StartEngineThread();
            });
            engineContainer.FormClosed += new FormClosedEventHandler((sender, e) => 
            {
                engine.StopEngine();
            });
            engine.EntityEngine.EFTPostFrameEvent += new Engine.Events.EFTPostFrameHandler((sender, e) =>
            {
                Console.WriteLine(string.Format("FPS: {0}", engine.EntityEngine.FrameRate.CurrentFPS));
            });

            //Entity ent0 = new Entity();
            //engine.EntityEngine.SystemManager.AddEntity(ent0);
            //engine.EntityEngine.SystemManager
            //    .AddComponentToEntity<Render.Render, Interfaces.IRenderSystem>
            //    (new Render.Render(), ent0);

            engine.FrameRenderDraw = true;
            engine.FrameRenderPhysics = true;
            engine.FrameSystemUpdate = true;

            Application.Run(engineContainer);
        }
    }
}
