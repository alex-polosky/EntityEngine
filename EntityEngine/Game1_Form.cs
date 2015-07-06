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

            e = sys.AddNewEntity();
            sys.AddNewComponentToEntity<Components.WinComponent, Components.WinSystem>(e);
            sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
            sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);

            rCom = e.GetComponent<Components.RenderComponent>();
            rCom.shader = basicShader;
            rCom.mesh = new Components.Mesh3D(this.render.Device,
                new Components.VertexStructures.Pos[] {
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(-1.0f, 1.0f, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(1.0f, 1.0f, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(-1.0f, -1.0f, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(1.0f, -1.0f, 0.0f)),
                },
                new short[] {
                    0, 1, 2,
                    2, 1, 3
                }
            );
            e.GetComponent<Components.PositionComponent>().translationWorldMatrix.M41 = 2.0f;
            e.GetComponent<Components.PositionComponent>().translationWorldMatrix.M42 = 2.0f;

            e = sys.AddNewEntity();
            sys.AddNewComponentToEntity<Components.WinComponent, Components.WinSystem>(e);
            sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
            sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);

            rCom = e.GetComponent<Components.RenderComponent>();
            rCom.shader = colorShader;
            rCom.mesh = new Components.Mesh3D(this.render.Device,
                new Components.VertexStructures.Color[] {
new Components.VertexStructures.Color(
    new SharpDX.Vector3(-1.0f, 1.0f, 0.0f), new SharpDX.Vector4(1.0f, 0.0f, 0.0f, 1.0f)),
new Components.VertexStructures.Color(
    new SharpDX.Vector3(1.0f, 1.0f, 0.0f), new SharpDX.Vector4(0.0f, 1.0f, 0.0f, 1.0f)),
new Components.VertexStructures.Color(
    new SharpDX.Vector3(-1.0f, -1.0f, 0.0f), new SharpDX.Vector4(0.0f, 0.0f, 1.0f, 1.0f)),
new Components.VertexStructures.Color(
    new SharpDX.Vector3(1.0f, -1.0f, 0.0f), new SharpDX.Vector4(1.0f, 1.0f, 1.0f, 1.0f)),
                },
                new short[] {
                    0, 1, 2,
                    2, 1, 3
                }
            );
            e.GetComponent<Components.PositionComponent>().translationWorldMatrix.M41 = -2.0f;
            e.GetComponent<Components.PositionComponent>().translationWorldMatrix.M42 = -2.0f;

            // Try to render a cube xD
            e = sys.AddNewEntity();
            sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
            sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);

            rCom = e.GetComponent<Components.RenderComponent>();
            rCom.shader = basicShader;
            rCom.mesh = new Components.Mesh3D(this.render.Device,
                new Components.VertexStructures.TexturedNormal[] {
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, 1.0f, -1.0f), new SharpDX.Vector2(0.0f, 0.0f), new SharpDX.Vector3(0.0f, 0.0f, -1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, 1.0f, -1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(0.0f, 0.0f, -1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, -1.0f, -1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(0.0f, 0.0f, -1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, -1.0f, -1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(0.0f, 0.0f, -1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, 1.0f, -1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(0.0f, 0.0f, -1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, -1.0f, -1.0f), new SharpDX.Vector2(1.0f, 1.0f), new SharpDX.Vector3(0.0f, 0.0f, -1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, 1.0f, -1.0f), new SharpDX.Vector2(0.0f, 0.0f), new SharpDX.Vector3(1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, 1.0f, 1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, -1.0f, -1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, -1.0f, -1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, 1.0f, 1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, -1.0f, 1.0f), new SharpDX.Vector2(1.0f, 1.0f), new SharpDX.Vector3(1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, 1.0f, 1.0f), new SharpDX.Vector2(0.0f, 0.0f), new SharpDX.Vector3(0.0f, 0.0f, 1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, 1.0f, 1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(0.0f, 0.0f, 1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, -1.0f, 1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(0.0f, 0.0f, 1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, -1.0f, 1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(0.0f, 0.0f, 1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, 1.0f, 1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(0.0f, 0.0f, 1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, -1.0f, 1.0f), new SharpDX.Vector2(1.0f, 1.0f), new SharpDX.Vector3(0.0f, 0.0f, 1.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, 1.0f, 1.0f), new SharpDX.Vector2(0.0f, 0.0f), new SharpDX.Vector3(-1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, 1.0f, -1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(-1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, -1.0f, 1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(-1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, -1.0f, 1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(-1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, 1.0f, -1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(-1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, -1.0f, -1.0f), new SharpDX.Vector2(1.0f, 1.0f), new SharpDX.Vector3(-1.0f, 0.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, 1.0f, 1.0f), new SharpDX.Vector2(0.0f, 0.0f), new SharpDX.Vector3(0.0f, 1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, 1.0f, 1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(0.0f, 1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, 1.0f, -1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(0.0f, 1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, 1.0f, -1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(0.0f, 1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, 1.0f, 1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(0.0f, 1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, 1.0f, -1.0f), new SharpDX.Vector2(1.0f, 1.0f), new SharpDX.Vector3(0.0f, 1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, -1.0f, -1.0f), new SharpDX.Vector2(0.0f, 0.0f), new SharpDX.Vector3(0.0f, -1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, -1.0f, -1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(0.0f, -1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, -1.0f, 1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(0.0f, -1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(-1.0f, -1.0f, 1.0f), new SharpDX.Vector2(0.0f, 1.0f), new SharpDX.Vector3(0.0f, -1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, -1.0f, -1.0f), new SharpDX.Vector2(1.0f, 0.0f), new SharpDX.Vector3(0.0f, -1.0f, 0.0f)),
new Components.VertexStructures.TexturedNormal(new SharpDX.Vector3(1.0f, -1.0f, 1.0f), new SharpDX.Vector2(1.0f, 1.0f), new SharpDX.Vector3(0.0f, -1.0f, 0.0f)),
                },
                new short[] {
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35
                }
            );

            // This is a test of the.. hud stuff?
            e = sys.AddNewEntity();
            sys.AddNewComponentToEntity<Components.WinComponent, Components.WinSystem>(e);
            sys.AddNewComponentToEntity<Components.PositionComponent, Components.PositionSystem>(e);
            sys.AddNewComponentToEntity<Components.RenderComponent, Components.RenderSystem>(e);

            rCom = e.GetComponent<Components.RenderComponent>();
            rCom.shader = twoDShader;
            rCom.mesh = new Components.Mesh3D(this.render.Device,
                new Components.VertexStructures.Pos[] {
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(0.0f, 0.2f, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(0.5f, 0.2f, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(0.0f, 0.0f, 0.0f)),
new Components.VertexStructures.Pos(
    new SharpDX.Vector3(0.5f, 0.0f, 0.0f)),
                },
                new short[] {
                    0, 1, 2,
                    2, 1, 3
                }
            );
            //e.GetComponent<Components.PositionComponent>().translationWorldMatrix.M41 = 2.0f;
            //e.GetComponent<Components.PositionComponent>().translationWorldMatrix.M42 = 2.0f;
            rCom.camera = new Components.Camera(this.Width, this.Height, true);
            rCom.camera.eye = new SharpDX.Vector3(
                0f, //this.Width / 2, //x
                0f, //this.Height / 2, //y
                0f  //z
            );
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
            this.Width = 800;
            this.Height = 600;
            
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

            this.sys.GetAllEntities()[0].GetComponent<Components.PositionComponent>()
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
