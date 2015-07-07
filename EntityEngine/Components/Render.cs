using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using SharpDX.Windows;
using D3D10 = SharpDX.Direct3D10;

using EntityFramework;
using EntityFramework.Components;

namespace EntityEngine.Components
{
    public class Camera
    {
        public Matrix projectionMatrix;
        public bool IsOrtho = false;
        // TODO: figure out formula of this camera translation...
        public Matrix orthoOffset;

        public Vector3 eye;
        public Vector3 view;
        public Vector3 up;
        public Matrix viewMatrix { get { return Matrix.LookAtLH(this.eye, this.view, this.up); } }

        public Camera(int targetWidth, int targetHeight, bool ortho=false)
        {
            this.IsOrtho = ortho;

            if (ortho)
            {
                var aspect = (float)targetWidth / (float)targetHeight;
                var scale = 0;
                var near = 0f;
                var far = 1f;
                this.projectionMatrix = Matrix.OrthoLH(
                    targetWidth + aspect * scale, targetHeight + scale, near, far
                );

                //this.eye = Vector3.Zero;
                //this.view = Vector3.UnitZ;
                //this.up = Vector3.UnitY;
                this.eye = new SharpDX.Vector3(
                    targetWidth / 2,
                    targetHeight / 2,
                    0f  //z
                );
                this.view = new SharpDX.Vector3(
                    targetWidth / 2, 
                    targetHeight / 2,
                    1f
                );
                this.up = Vector3.UnitY;

                //this.orthoOffset = Matrix.Translation(-408.0f, 319.0f, 0.0f);
                //this.orthoOffset = Matrix.Translation(
                //    ((float)targetWidth) / 2,
                //    ((float)targetHeight) / 2,
                //    0);
                this.orthoOffset = Matrix.Translation(0, 0, 0);
            }
            else
            {
                // Set up projection matrix
                var fov = MathUtil.Pi / 4.0f;
                var aspect = (float)targetWidth / (float)targetHeight;
                var near = 0.1f;
                var far = 100.0f;
                this.projectionMatrix = Matrix.PerspectiveFovLH(
                    fov, aspect, near, far
                );

                // View matrix, and projection matrix
                this.eye = new Vector3(1.0f, 1.0f, -10.0f);
                this.view = new Vector3(0.0f, 0.0f, 0.0f);
                this.up = Vector3.UnitY;
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////

    public class RenderComponent : EntityFramework.Component
    {
        public RenderTypeFlag renderFlags;
        public Mesh3D mesh;
        public Shader shader;
        public Camera camera;

        public RenderComponent() : base() { }
        public RenderComponent(Entity e) : base(e) { }
    }

    public class RenderSystem : ComponentSystem<RenderComponent>
    {
        private List<Type> __dependencies = new List<Type>()
        {
            typeof(PositionComponent)
        };

        private int targetWidth;
        private int targetHeight;

        private Form renderForm;

        private SwapChainDescription swapChainDesc;
        private SwapChain swapChain;
        private D3D10.Device d3d10Device;
        private RenderTargetView renderTargetView;
        public Viewport viewport;

        private Camera camera;

        public D3D10.Device Device { get { return this.d3d10Device; } }

        public void SetTitle(string title)
        {
            this.renderForm.Text = title;
        }

        public void ShowGame()
        {
            this.renderForm.Show();
        }

        public void HideGame()
        {
            this.renderForm.Hide();
        }

        public void InitializeD3D(Form form = null, int width = 800, int height = 600)
        {
            // Setting up the host form
            if (form == null)
            {
                this.renderForm = new Form();
            }
            else
                this.renderForm = form;

            this.targetWidth = width;
            this.targetHeight = height;

            renderForm.ClientSize = new Size(this.targetWidth, this.targetHeight);
            //renderForm.StartPosition = FormStartPosition.CenterScreen;
            renderForm.StartPosition = FormStartPosition.Manual;
            renderForm.Location = Screen.PrimaryScreen.Bounds.Location;

            // Start Direct3D stuff here
            this.swapChainDesc = new SwapChainDescription();

            // Buffer dimensions and format
            this.swapChainDesc.BufferCount = 2;
            this.swapChainDesc.ModeDescription.Width = this.targetWidth;
            this.swapChainDesc.ModeDescription.Height = this.targetHeight;
            this.swapChainDesc.Usage = Usage.RenderTargetOutput;
            this.swapChainDesc.ModeDescription.Format = Format.R8G8B8A8_UNorm;

            // Refresh rate
            this.swapChainDesc.ModeDescription.RefreshRate.Numerator = 60;
            this.swapChainDesc.ModeDescription.RefreshRate.Denominator = 1;

            // sampling settings
            this.swapChainDesc.SampleDescription.Quality = 0;
            this.swapChainDesc.SampleDescription.Count = 1;

            // Output window
            this.swapChainDesc.OutputHandle = this.renderForm.Handle;
            this.swapChainDesc.IsWindowed = true;

            // Start up D3D
            D3D10.Device.CreateWithSwapChain(
                D3D10.DriverType.Hardware,
#if DEBUG
                DeviceCreationFlags.Debug,
#else
                DeviceCreationFlags.None,
#endif
                this.swapChainDesc,
                out this.d3d10Device,
                out this.swapChain
            );

            // Create Render Target View
            using (Texture2D backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
            {
                this.renderTargetView = new RenderTargetView(this.d3d10Device, backBuffer);
            }

            this.d3d10Device.OutputMerger.SetTargets(this.renderTargetView);

            // Set up viewport
            this.viewport = new Viewport();
            this.viewport.Width = targetWidth;
            this.viewport.Height = targetHeight;
            this.viewport.MinDepth = 0.0f;
            this.viewport.MaxDepth = 1.0f;
            this.viewport.X = 0;
            this.viewport.Y = 0;

            this.d3d10Device.Rasterizer.SetViewports(this.viewport);

            // Set up camera
            this.camera = new Camera(this.targetWidth, this.targetHeight, ortho:false);

            // Generic input assembler
            this.d3d10Device.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            // Rasterizer settings
            this.d3d10Device.Rasterizer.State = new RasterizerState(this.d3d10Device,
                new RasterizerStateDescription()
                {
                    CullMode = CullMode.Back,
                    FillMode = FillMode.Solid
                }
            );
        }

        public override void Update(double timeDelta = 0.0f)
        {
            // Clear our backbuffer with the rainbow color
            d3d10Device.ClearRenderTargetView(this.renderTargetView, (Color4)SharpDX.Color.CornflowerBlue);

            this.d3d10Device.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            // We're going to rotate our camera around the y axis
            float time = (float)(timeDelta / 1000.0f); // time in milliseconds?

            if (timeDelta > 5000)
            {
                float angle = 1f;
                float rotCos = (float)Math.Cos(time * angle);
                float rotSin = (float)Math.Sin(time * angle);
                var x = this.camera.eye.X;
                var y = this.camera.eye.Y;
                this.camera.eye.X = ((x - 5.0f) * rotCos) - ((y - 5.0f) * rotSin);
                this.camera.eye.Y = ((x - 5.0f) * rotSin) - ((y - 5.0f) * rotCos);
            }

            // Do actual drawing here
            foreach (RenderComponent com in this._components)
            {
                // Get any required components
                PositionComponent pos = com.entity.GetComponent<PositionComponent>();

                // Set up required buffers
                var inputAssembler = this.d3d10Device.InputAssembler;
                inputAssembler.SetVertexBuffers(0, com.mesh.vertexBuffer);
                if (com.mesh.indexBuffer != null)
                    inputAssembler.SetIndexBuffer(com.mesh.indexBuffer, Format.R16_UInt, 0);

                // Set up effect variables
                // These matrices should always be defined in the shader, even if they're not used
                if (com.camera == null)
                {
                    com.shader.effect.GetVariableByIndex(0).AsMatrix().SetMatrix(this.camera.viewMatrix);
                    com.shader.effect.GetVariableByIndex(1).AsMatrix().SetMatrix(this.camera.projectionMatrix);
                }
                else
                {
                    com.shader.effect.GetVariableByIndex(0).AsMatrix().SetMatrix(com.camera.viewMatrix * com.camera.orthoOffset);
                    com.shader.effect.GetVariableByIndex(1).AsMatrix().SetMatrix(com.camera.projectionMatrix);
                }
                com.shader.effect.GetVariableByIndex(2).AsMatrix().SetMatrix(pos.rotationXMatrix);
                com.shader.effect.GetVariableByIndex(3).AsMatrix().SetMatrix(pos.rotationYMatrix);
                com.shader.effect.GetVariableByIndex(4).AsMatrix().SetMatrix(pos.rotationZMatrix);
                com.shader.effect.GetVariableByIndex(5).AsMatrix().SetMatrix(pos.scalingMatrix);
                com.shader.effect.GetVariableByIndex(6).AsMatrix().SetMatrix(pos.translationLocalMatrix);
                com.shader.effect.GetVariableByIndex(7).AsMatrix().SetMatrix(pos.translationWorldMatrix);
                foreach (var shaderVars in com.shader.vars)
                {
                    // Eventually, we'll use this to set all the required variables needed
                }

                // Run through each technique, pass, draw
                int i = 0, j = 0;
                foreach (var techniqueContainer in com.shader.inputLayouts)
                {
                    var technique = com.shader.effect.GetTechniqueByIndex(i);
                    foreach (var passContainer in techniqueContainer)
                    {
                        var pass = technique.GetPassByIndex(j);
                        inputAssembler.InputLayout = passContainer;

                        pass.Apply();
                        if (com.mesh.indexBuffer != null)
                            this.d3d10Device.DrawIndexed(com.mesh.numberOfIndices, 0, 0);
                        else
                            this.d3d10Device.Draw(com.mesh.numberOfVertices, 0);

                        j += 1;
                    }
                    i += 1;
                }
            }

            // text?
            //var font = new D3D10.Font(this.d3d10Device, new FontDescription()
            //    {
            //        CharacterSet = FontCharacterSet.Default,
            //        FaceName = "Courier New",
            //        Height = 72,
            //        Italic = false,
            //        MipLevels = 1,
            //        OutputPrecision = FontPrecision.Default,
            //        PitchAndFamily = FontPitchAndFamily.Default,
            //        Quality = FontQuality.Default,
            //        Weight = FontWeight.Normal,
            //        //Width = 1
            //    }
            //);
            //font.DrawText(null, "SIMPLE TEXT", 0, 0, Color4.Black);
            //font.DrawText(null, "SIMPLE TEXT", 1, 1, Color4.White);

            // Present our drawn scene waiting for one vertical sync
            this.swapChain.Present(1, PresentFlags.None);
        }

        public RenderSystem()
            : base()
        {
            Console.WriteLine("RenderSystem()");
            foreach (Type type in this.__dependencies)
                this.dependencies.Add(type);
        }

        ~RenderSystem()
        {
            this.swapChain.Dispose();
            this.renderTargetView.Dispose();
            this.d3d10Device.Dispose();
        }
    }
}
