﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SharpDX.Windows;

using EntityFramework;
using EntityFramework.Components;

using PyInterface;

namespace EntityEngine
{
    public class FrameRate
    {
        private System.Diagnostics.Stopwatch sW;
        private long lastTime;
        private long currentTime;
        private int currentFPS;
        private float currentPastTime;

        public int CurrentFPS { get { return this.currentFPS; } }
        public float ElaspedTime { get { return this.currentPastTime; } }
        public long ElaspedMS { get { return this.sW.ElapsedMilliseconds; } }

        public void StartFrame()
        {
            this.lastTime = this.currentTime;
        }

        public void EndFrame()
        {
            this.currentTime = this.sW.ElapsedMilliseconds;
            this.currentPastTime = this.currentTime - this.lastTime;
            this.currentFPS = (int)(1000.0f / this.currentPastTime);
        }

        public void Start()
        {
            this.sW.Start();
        }

        public FrameRate()
        {
            this.sW = new System.Diagnostics.Stopwatch();
            this.lastTime = 0;
            this.currentTime = 0;
        }
    }

    public partial class Game1_Form : Form
    {
        protected FrameRate FPS;

        protected clsPyInterface py;
        protected Dictionary<string, object> pyVars;

        protected SystemManager sys;
        protected Components.RenderSystem render;

        protected PyForm pyForm;

        public SystemManager world { get { return this.sys; } }

        public class PyHelp
        {
            public string c_typeof(object obj)
            {
                return obj.GetType().ToString();
            }
        }

        protected virtual void SetUpEnts() { }

        [Obsolete]
        private void SetUpEnts_Example_Dep()
        {
            Entity e;
            Components.RenderComponent rCom;
            Components.Shader basicShader = new Components.Shader(
                this.render.Device,
                "Shaders/basicEffect.fx",
                new SharpDX.Direct3D10.InputElement[] {
                    new SharpDX.Direct3D10.InputElement("POSITION", 0, SharpDX.DXGI.Format.R32G32B32_Float, 0, 0),
                }
             );
            Components.Shader colorShader = new Components.Shader(
                this.render.Device,
                "Shaders/color.fx",
                new SharpDX.Direct3D10.InputElement[] {
                    new SharpDX.Direct3D10.InputElement("POSITION", 0, SharpDX.DXGI.Format.R32G32B32_Float, 0, 0),
                    new SharpDX.Direct3D10.InputElement("COLOR", 0, SharpDX.DXGI.Format.R32G32B32A32_Float, 12, 0),
                }
             );

            EntityEngine.Components.Mesh3D meshCube = FileManager.LoadMeshFromFile(
                this.render.Device, "Maps/Test/Models/cube.mesh");
            EntityEngine.Components.Mesh3D meshSquare = FileManager.LoadMeshFromFile(
                this.render.Device, "Maps/Test/Models/square.mesh");
            EntityEngine.Components.Mesh3D meshSquareColor = FileManager.LoadMeshFromFile(
                this.render.Device, "Maps/Test/Models/squarecolor.mesh");

            // 2D rendering first yo!
            var lineThick = 1f;
            var lineDistance = 20f;
            var horLine = new Components.Mesh3D(this.render.Device,
                new Components.VertexStructures.Pos[] {
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(0.0f, lineThick, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(this.Width, lineThick, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(0.0f, 0.0f, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(this.Width, 0.0f, 0.0f)),
                },
                new short[] {
                    0, 1, 2,
                    2, 1, 3
                }
            );
            var verLine = new Components.Mesh3D(this.render.Device,
                new Components.VertexStructures.Pos[] {
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(0.0f, this.Height, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(lineThick, this.Height, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(0.0f, 0.0f, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(lineThick, 0.0f, 0.0f)),
                },
                new short[] {
                    0, 1, 2,
                    2, 1, 3
                }
            );

            for (int i = 0; i < this.Height; i += (int)lineDistance)
            {
                e = sys.AddNewEntity();
                sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
                sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);
                sys.AddComponentToEntity<Components.CameraComponent, Components.CameraSystem>
                    (new Components.CameraComponent(this.Width, this.Height, true, zBuffer: false), e);
                rCom = e.GetComponent<Components.RenderComponent>();
                rCom.shader = basicShader;
                rCom.mesh = horLine;
                e.GetComponent<Components.PositionComponent>().translationWorldMatrix =
                    SharpDX.Matrix.Translation(0, i, 0);
            }

            for (int i = 0; i < this.Width; i += (int)lineDistance)
            {
                e = sys.AddNewEntity();
                sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
                sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);
                sys.AddComponentToEntity<Components.CameraComponent, Components.CameraSystem>
                    (new Components.CameraComponent(this.Width, this.Height, true, zBuffer: false), e);
                rCom = e.GetComponent<Components.RenderComponent>();
                rCom.shader = basicShader;
                rCom.mesh = verLine;
                e.GetComponent<Components.PositionComponent>().translationWorldMatrix =
                    SharpDX.Matrix.Translation(i, 0, 0);
            }
        }

        public Game1_Form(int renderMode = 6, bool usePyConsole = true, bool useGrid = false, int customWidth=-1, int customHeight=-1)
        {
            InitializeComponent();

            // Set up python engine first, as this will allow us to launch the debug window
            // first, and we will redirect output to there
            this.py = new clsPyInterface(@"\
import sys
sys.path.append(r'C:\\Program Files (x86)\\IronPython 2.7\\Lib')
del sys

import clr
clr.AddReference('System')
clr.AddReference('System.Collections')
clr.AddReference('EntityFramework')
clr.AddReference('EntityEngine')
clr.AddReference('PyInterface')

import EntityFramework.Components
import EntityEngine.Components
#typeof = PyHelp.c_typeof

from System.Collections import Generic
List = Generic.List
");

            // This code will cause the python window to appear, and allows access to the internals
            // This is useful for debugging while running, and setting different things
            // Think of it as a glorified console :)
            if (usePyConsole)
            {
                this.pyVars = new Dictionary<string, object>()
                {
                    {"FPS", this.FPS},
                    {"world", this.sys},
                    {"render", this.render},
                    {"PyHelp", new PyHelp()},
                    {"this", this}
                };
                this.pyForm = new PyForm(ref this.py, this.pyVars);
                Console.SetOut(new MultiTextWriter(Console.Out, new ControlWriter(pyForm.StdOut)));
                pyForm.Show();
            }

            // Set up Width/Height 
            // TODO: load from file
            switch (renderMode)
            {
                // 4:3
                case (0):
                    this.Width = 640;
                    this.Height = 480;
                    break;
                case (1):
                    this.Width = 800;
                    this.Height = 600;
                    break;
                case (2):
                    this.Width = 1024;
                    this.Height = 768;
                    break;
                case (3):
                    this.Width = 1280;
                    this.Height = 960;
                    break;
                // 16:9
                case (4):
                    this.Width = 640;
                    this.Height = 360;
                    break;
                case (5):
                    this.Width = 960;
                    this.Height = 540;
                    break;
                case (6):
                    this.Width = 1280;
                    this.Height = 720;
                    break;
                case (7):
                    this.Width = 1920;
                    this.Height = 1080;
                    break;
                // Custom
                case (8):
                    this.Width = customWidth;
                    this.Height = customHeight;
                    break;
                default:
                    break;
            }

            // Init FPS
            this.FPS = new FrameRate();

            // Set up World
            sys = new SystemManager();
            sys.AddComponentSystem<Components.RenderComponent, Components.RenderSystem>();

            // Load the render system
            this.render = sys.GetComponentSystem<Components.RenderComponent, Components.RenderSystem>();
            render.InitializeD3D(this, this.Width, this.Height);
            render.SetTitle("Testing");

            // Load Camera system and set render camera
            var cam = new Components.CameraComponent(this.Width, this.Height);
            sys.GetComponentSystem<Components.CameraComponent, Components.CameraSystem>()
                .AddComponent(cam);
            render.SetCamera(cam);

            // Load the Win Conditions
            sys.GetComponentSystem<Components.WinComponent, Components.WinSystem>()
                .initFromSerial(ref sys, ref py, ref FPS);

            // Load up input manager
            sys.AddComponentSystem<Components.InputComponent, Components.InputSystem>();

            // Load any components
            if (useGrid)
                this.SetUpEnts_Example_Dep();
            this.SetUpEnts();

            if (usePyConsole)
            {
                this.pyVars = new Dictionary<string, object>()
                {
                    {"FPS", this.FPS},
                    {"world", this.sys},
                    {"render", this.render},
                    {"PyHelp", new PyHelp()},
                    {"this", this}
                };
                pyForm.SetVars(this.pyVars);
            }

            // Start the game
            this.Shown += new EventHandler(this.StartRunning_Game);
            this.FPS.Start();
            render.ShowGame();
        }

        private void UpdateSys()
        {
            this.FPS.StartFrame();

            this.sys.Update(this.FPS.ElaspedMS);

            this.FPS.EndFrame();
        }

        private void StartRunning_Game(object sender, EventArgs e)
        {
            this.sys.GetComponentSystem<Components.InputComponent, Components.InputSystem>()
                .Enable();
            RenderLoop.Run(this, this.UpdateSys);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.sys.GetComponentSystem<Components.InputComponent, Components.InputSystem>()
                .Enable();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.sys.GetComponentSystem<Components.InputComponent, Components.InputSystem>()
                .Disable();
        }

        #region "TextWriters"
        private class ControlWriter : TextWriter
        {
            private Control textbox;
            public ControlWriter(Control textbox)
            {
                this.textbox = textbox;
            }

            public override void Write(char value)
            {
                textbox.Text += value;
            }

            public override void Write(string value)
            {
                textbox.Text += value;
            }

            public override Encoding Encoding
            {
                get { return Encoding.ASCII; }
            }
        }
        private class MultiTextWriter : TextWriter
        {
            private IEnumerable<TextWriter> writers;
            public MultiTextWriter(IEnumerable<TextWriter> writers)
            {
                this.writers = writers.ToList();
            }
            public MultiTextWriter(params TextWriter[] writers)
            {
                this.writers = writers;
            }

            public override void Write(char value)
            {
                foreach (var writer in writers)
                    writer.Write(value);
            }

            public override void Write(string value)
            {
                foreach (var writer in writers)
                    writer.Write(value);
            }

            public override void Flush()
            {
                foreach (var writer in writers)
                    writer.Flush();
            }

            public override void Close()
            {
                foreach (var writer in writers)
                    writer.Close();
            }

            public override Encoding Encoding
            {
                get { return Encoding.ASCII; }
            }
        }
        #endregion
    }
}