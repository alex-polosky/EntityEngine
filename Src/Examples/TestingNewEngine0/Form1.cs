using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

using EntityFramework;
using EntityFramework.AssetFileInterface;
using EntityFramework.Engine;
using EntityFramework.Engine.Events;

namespace TestingNewEngine0
{
    public partial class Form1 : Form
    {
        private EE_Form entityEngine;
        private Thread thread;

        public Form1()
        {
            InitializeComponent();

            button1.Enabled = true;
            button2.Enabled = false;

            entityEngine = new EE_Form(Handle);

            cbSystem.Checked = false;
            entityEngine.FrameSystemUpdate = false;
            cbRender.Checked = true;
            entityEngine.FrameRenderDraw = true;
            cbPhysics.Checked = true;
            entityEngine.FrameRenderPhysics = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (thread.IsAlive)
                entityEngine.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread = EntityEngine.GetRunLoopThread(entityEngine, true);
            if (thread == null)
                return;
            thread.Start();
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;

            Console.WriteLine("Pausing for 1000 ms...");
            entityEngine.Pause();

            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("Resuming for 1000 ms...");
            entityEngine.Resume();

            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("Stopping...");
            entityEngine.Stop();

            thread.Join();
        }

        private void cbSystem_CheckedChanged(object sender, EventArgs e)
        {
            entityEngine.FrameSystemUpdate = cbSystem.Checked;
        }

        private void cbRender_CheckedChanged(object sender, EventArgs e)
        {
            entityEngine.FrameRenderDraw = cbRender.Checked;
        }

        private void cbPhysics_CheckedChanged(object sender, EventArgs e)
        {
            entityEngine.FrameRenderPhysics = cbPhysics.Checked;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            entityEngine.SystemManager.RenderDraw(0, IntPtr.Zero);
        }
    }

    class EE_Form : EntityEngine
    {
        private int r = 0;
        private int g = 0;
        private int b = 0;
        protected Form1 DrawForm { get { return (Form1)Form.FromHandle(drawHandle); } }

        protected override void OnETFSystemUpdate(object sender, EFTSystemUpdateEventArgs e)
        {
            base.OnETFSystemUpdate(sender, e);

            var form = DrawForm;
            if (++r > 255)
                r = 0;
            form.panel1.BackColor = System.Drawing.Color.FromArgb(r, g, b);
        }

        protected override void OnETFRenderDraw(object sender, EFTRenderDrawEventArgs e)
        {
            base.OnETFRenderDraw(sender, e);

            var form = DrawForm;
            if (--g < 0)
                g = 255;
            form.panel1.BackColor = System.Drawing.Color.FromArgb(r, g, b);
        }

        protected override void OnETFRenderPhysics(object sender, EFTRenderPhysicsEventArgs e)
        {
            base.OnETFRenderPhysics(sender, e);

            var form = DrawForm;
            if ((b += ((r + g) % 255) + 1) > 250)
                b = 0;
            form.panel1.BackColor = System.Drawing.Color.FromArgb(r, g, b);
        }

        protected override void OnEngineFrameTick(object sender, EngineFrameTickEventArgs e)
        { 
            base.OnEngineFrameTick(sender, e);

            Thread.Sleep((int)DrawForm.numThreadWait.Value);
        }

        protected override void OnEFTPostFrame(object sender, EFTPostFrameEventArgs e)
        {
            base.OnEFTPostFrame(sender, e);

            Console.WriteLine(FrameRate.CurrentFPS);
        }

        protected override void OnStartEvent(object sender, StartEventArgs e)
        {
            base.OnStartEvent(sender, e);

            var sysMan = this.SystemManager;
            EntityFramework.ComponentInterfaces.IRenderSystem renderSystem =
                new EntityFramework.Render.RenderSystem();
            renderSystem.Init(typeof(EntityFramework.Render.Render));
            sysMan.AddComponentSystem<EntityFramework.ComponentInterfaces.IRenderSystem>(renderSystem);

            EntityFramework.ComponentInterfaces.IStressTestSystem stressSystem =
                new EntityFramework.Components.StressTestSystem();
            stressSystem.Init(typeof(EntityFramework.Components.StressTest));
            sysMan.AddComponentSystem<EntityFramework.ComponentInterfaces.IStressTestSystem>(stressSystem);

            for (int i = 0; i < 1000; i++)
            {
                Guid id = Guid.NewGuid();
                sysMan.AddNewEntity(id);
                sysMan.AddComponentToEntity<EntityFramework.ComponentInterfaces.IStressTestSystem>(id);
            }

            foreach (var com in sysMan
                .GetComponentSystem<EntityFramework.ComponentInterfaces.IStressTestSystem>()
                .GetTComponents())
            {
                com.StressLevel = EntityFramework.ComponentInterfaces.IStressTest.Level.CompHi;
            }
        }

        protected override void OnStopEvent(object sender, StopEventArgs e)
        {
            base.OnStopEvent(sender, e);


        }

        public EE_Form(IntPtr handle)
            : base()
        {
            drawHandle = handle;

            var baseDir = @"P:\Code\Git\EntityEngine\Maps\";
            var Global = baseDir + "Global";
            var Menu = baseDir + "MainMenu";
            var Testing = baseDir + "Testing";
            this.FileManager = new EntityFramework.Manager.FileManager();
            this.FileManager.PrepareAllAssetFiles(Global);
            this.FileManager.PrepareAllAssetFiles(Menu);
            this.FileManager.PrepareAllAssetFiles(Testing);
            //var aPath = System.IO.Path.Combine(dir, "Audio", "test.audio");
            //var audio = (Serialize.Audio)engine.FileManager.GetAssetFileFromPath(aPath);
            //audio.Load(engine.FileManager);
            //engine.FileManager.LoadData(audio);
            //audio.Unload(engine.FileManager);
            //engine.FileManager.UnloadData(audio);
            //foreach (var a in engine.FileManager.LoadedAssets)
            //    a.Load(engine.FileManager);
        }
    }
}
