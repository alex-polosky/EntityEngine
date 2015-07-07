using System;
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
        private FrameRate FPS;

        private clsPyInterface py;
        private Dictionary<string, object> pyVars;

        private SystemManager sys;
        private Components.RenderSystem render;

        public class PyHelp
        {
            public string c_typeof(object obj)
            {
                return obj.GetType().ToString();
            }
        }

        private void SetUpEnts()
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
            Components.Shader twoDShader = new Components.Shader(
                this.render.Device,
                "Shaders/2D.fx",
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

            var cam = new Components.Camera(this.Width, this.Height, true);

            for (int i = 0; i < this.Height; i += (int)lineDistance)
            {
                e = sys.AddNewEntity();
                sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
                sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);
                rCom = e.GetComponent<Components.RenderComponent>();
                rCom.shader = basicShader;
                rCom.mesh = horLine;
                rCom.camera = cam;
                e.GetComponent<Components.PositionComponent>().translationWorldMatrix =
                    SharpDX.Matrix.Translation(0, i, 0);
            }

            for (int i = 0; i < this.Width; i += (int)lineDistance)
            {
                e = sys.AddNewEntity();
                sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
                sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);
                rCom = e.GetComponent<Components.RenderComponent>();
                rCom.shader = basicShader;
                rCom.mesh = verLine;
                rCom.camera = cam;
                e.GetComponent<Components.PositionComponent>().translationWorldMatrix =
                    SharpDX.Matrix.Translation(i, 0, 0);
            }

            e = sys.AddNewEntity();
            sys.AddNewComponentToEntity<TagComponent, TagSystem>(e);
            e.GetComponent<TagComponent>().name = "win";
            sys.AddNewComponentToEntity<Components.WinComponent, Components.WinSystem>(e);
            sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
            sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);
            rCom = e.GetComponent<Components.RenderComponent>();
            rCom.shader = basicShader;
            rCom.mesh = meshSquare;
            e.GetComponent<Components.PositionComponent>().translationWorldMatrix.M41 = 2.0f;
            e.GetComponent<Components.PositionComponent>().translationWorldMatrix.M42 = 2.0f;

            e = sys.AddNewEntity();
            sys.AddNewComponentToEntity<Components.WinComponent, Components.WinSystem>(e);
            sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
            sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);
            rCom = e.GetComponent<Components.RenderComponent>();
            rCom.shader = colorShader;
            rCom.mesh = meshSquareColor;
            e.GetComponent<Components.PositionComponent>().translationWorldMatrix.M41 = -2.0f;
            e.GetComponent<Components.PositionComponent>().translationWorldMatrix.M42 = -2.0f;

            // Try to render a cube xD
            e = sys.AddNewEntity();
            sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
            sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);
            rCom = e.GetComponent<Components.RenderComponent>();
            rCom.shader = basicShader;
            rCom.mesh = meshCube;
        }

        public Game1_Form()
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

#if DEBUG
            // This code will cause the python window to appear, and allows access to the internals
            // This is useful for debugging while running, and setting different things
            // Think of it as a glorified console :)
            this.pyVars = new Dictionary<string, object>()
            {
                {"FPS", this.FPS},
                {"world", this.sys},
                {"render", this.render},
                {"PyHelp", new PyHelp()},
                {"this", this}
            };
            var pyForm = new PyForm(ref this.py, this.pyVars);
            Console.SetOut(new MultiTextWriter(Console.Out, new ControlWriter(pyForm.StdOut)));
            pyForm.Show();
#endif

            // Set up Width/Height 
            // TODO: load from file
            var renderMode = 6;
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
                default:
                    break;
            }
            
            // Init FPS
            this.FPS = new FrameRate();

            // Set up World
            sys = new SystemManager();
            sys.AddComponentSystem<Components.RenderComponent, Components.RenderSystem>();

            // Load up win condition
            //sys.AddComponentSystem<Components.WinComponent, Components.WinSystem>();
            //Components.WinSystem com = sys.GetComponentSystem<Components.WinComponent, Components.WinSystem>();
            //com.SetWorld(ref this.sys);
            //com.SetPy(ref this.py);
            //com.SetFPS(ref this.FPS);
            //com.LoadWinConditions();
            //com.SetWinCondition(com.WinConditions[0]);

            // Load the render system
            this.render = sys.GetComponentSystem<Components.RenderComponent, Components.RenderSystem>();
            render.InitializeD3D(this, this.Width, this.Height);
            render.SetTitle("Testing");

            // Start up File Manager
            var win = FileManager.LoadObjFromFile(this.sys, this.py, this.FPS);
            sys.AddComponentSystem<Components.WinComponent, Components.WinSystem>((Components.WinSystem)win);

            // Load any components
            this.SetUpEnts();

            // Start the game
            this.Shown += new EventHandler(this.StartRunning_Game);
            this.FPS.Start();
            render.ShowGame();
        }

        private void UpdateSys()
        {
            this.FPS.StartFrame();

            this.sys.GetComponentSystem<TagComponent, TagSystem>()
                .getTaggedEntity("win")
                .GetComponent<Components.PositionComponent>()
                .translationWorldMatrix.M41 += 0.001f;

            this.sys.Update(this.FPS.ElaspedMS);
            //this.sys.GetComponentSystem<Components.RenderComponent, Components.RenderSystem>()
            //    .Update(this.FPS.ElaspedMS);

            this.FPS.EndFrame();

            List<Components.WinComponent> win = this.sys.GetComponentSystem<Components.WinComponent, Components.WinSystem>().Winner;
            if (win != null)
            {
                long ms = this.FPS.ElaspedMS;
                this.render.SetTitle("'" + win[0].entity.guid.ToString() + "' won after " + ms.ToString() + " milliseconds!");
            }
        }

        private void StartRunning_Game(object sender, EventArgs e)
        {
            RenderLoop.Run(this, this.UpdateSys);
        }

        #region "TextWriters"
#if DEBUG
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
#endif
        #endregion
    }
}
