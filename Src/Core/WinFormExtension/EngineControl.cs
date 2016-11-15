using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using EntityFramework;
using Engine = EntityFramework.Engine;
using Interfaces = EntityFramework.ComponentInterfaces;
using Render = EntityFramework.Render;

namespace WinFormExtension
{
    public class EngineControl : UserControl
    {
        protected class Engine0 : Engine.EntityEngine
        {
            protected Control Control { get { return Control.FromHandle(drawHandle); } }

            protected override void OnStartEvent(object sender, Engine.Events.StartEventArgs e)
            {
                base.OnStartEvent(sender, e);

                var sysMan = SystemManager;
                sysMan.AddComponentSystem<Interfaces.IRenderSystem>(new Render.RenderSystem());
                sysMan.GetComponentSystem<Interfaces.IRenderSystem>().Init();
            }

            public Engine0(IntPtr handle)
                : base()
            {
                drawHandle = handle;
            }
        }
        private System.ComponentModel.IContainer components = null;

        protected Engine0 engine;
        protected Thread thread;

        public bool FrameSystemUpdate
        { get { return engine.FrameSystemUpdate; } set { engine.FrameSystemUpdate = value; } }
        public bool FrameRenderDraw
        { get { return engine.FrameRenderDraw; } set { engine.FrameRenderDraw = value; } }
        public bool FrameRenderPhysics
        { get { return engine.FrameRenderPhysics; } set { engine.FrameRenderPhysics = value; } }

        public Engine.EntityEngine EntityEngine { get { return (Engine.EntityEngine)engine; } }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void StartEngineThread()
        {
            if (thread == null || !thread.IsAlive)
            {
                thread = Engine.EntityEngine.GetRunLoopThread(engine, true);
                if (thread == null)
                    throw new Exception("Engine not ready and did not return loop");
            }
            thread.Start();
        }

        public void PauseEngine()
        {
            engine.Pause();
        }

        public void ResumeEngine()
        {
            engine.Resume();
        }

        public void StopEngine()
        {
            if (engine.IsRunning)
            {
                engine.Stop();
                thread.Join();
            }
        }

        public void InitEngine()
        {
            engine = new Engine0(Handle);
            thread = Engine.EntityEngine.GetRunLoopThread(engine, true);
            if (thread == null)
                throw new Exception("Engine not ready and did not return loop");
        }

        public EngineControl()
        {
            InitializeComponent();
        }
    }
}
