using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
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
    [DataContract]
    public class RenderComponent : EntityFramework.Component
    {
        [DataMember]
        public RenderTypeFlag renderFlags;
        [DataMember]
        public Mesh3D mesh;
        [DataMember]
        public EntityEngine.Assets.Shader shader;

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
        private DepthStencilView depthStencilView;
        private DepthStencilStateDescription depthState;
        private DepthStencilStateDescription depthNonState;
        private bool isOrthoCurrent = false;
        public Viewport viewport;

        private SharpDX.Direct3D10.Font font;

        private CameraComponent camera;
        public CameraComponent Camera { get { return this.camera; } }

        public D3D10.Device Device { get { return this.d3d10Device; } }

        public void SetTitle(string title)
        {
            this.renderForm.Text = title;
        }

        public void SetCamera(CameraComponent cam)
        {
            this.camera = cam;
        }

        public void ShowGame()
        {
            this.renderForm.Show();
        }

        public void HideGame()
        {
            this.renderForm.Hide();
        }

        public void InitializeD3D(Form form = null, int width = 800, int height = 600, CameraComponent cam = null)
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
                DeviceCreationFlags.None,//DeviceCreationFlags.Debug,
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

            // Z-Buffer!?
            var zBufferTextureDescription = new Texture2DDescription
            {
                Format = Format.D16_UNorm,
                ArraySize = 1,
                MipLevels = 1,
                Width = this.targetWidth,
                Height = this.targetHeight,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };
            using (var zBufferTexture = new Texture2D(this.d3d10Device, zBufferTextureDescription))
                this.depthStencilView = new DepthStencilView(this.d3d10Device, zBufferTexture);
            this.d3d10Device.OutputMerger.SetTargets(depthStencilView, this.renderTargetView);

            this.depthState = new DepthStencilStateDescription()
            {
                IsDepthEnabled = true,
                DepthWriteMask = DepthWriteMask.All,
                DepthComparison = Comparison.Less,
                IsStencilEnabled = true,
                StencilReadMask = 0xFF,
                StencilWriteMask = 0xFF,
                FrontFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Increment,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                },
                BackFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Decrement,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                }
            };
            this.depthNonState = new DepthStencilStateDescription()
            {
                IsDepthEnabled = false
            };
            this.d3d10Device.OutputMerger.SetDepthStencilState(new DepthStencilState(this.d3d10Device,
                this.depthState), 1);

            // Font
            this.font = new D3D10.Font(this.d3d10Device, new FontDescription()
                {
                    CharacterSet = FontCharacterSet.Default,
                    FaceName = "Courier New",
                    Height = 72 / 4,
                    Italic = false,
                    MipLevels = 1,
                    OutputPrecision = FontPrecision.Default,
                    PitchAndFamily = FontPitchAndFamily.Default,
                    Quality = FontQuality.Default,
                    Weight = FontWeight.Normal,
                    //Width = 1
                }
            );

            // Set up camera
            if (cam == null)
                this.camera =
                    new CameraComponent(this.targetWidth, this.targetHeight);
            else
                this.camera = cam;

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
            if (this.renderForm.Visible)
            {
                // Clear our backbuffer with the rainbow color
                d3d10Device.ClearRenderTargetView(this.renderTargetView, (Color4)SharpDX.Color.CornflowerBlue);
                this.d3d10Device.ClearDepthStencilView(depthStencilView, DepthStencilClearFlags.Depth, 1f, 0);

                this.d3d10Device.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

                // We're going to rotate our camera around the y axis
                float time = (float)(timeDelta / 1000.0f); // time in milliseconds?

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
                    var camCom = com.entity.GetComponent<CameraComponent>();
                    if (camCom == null)
                        camCom = this.camera;
                    if (!camCom.IsZBuffer)
                        this.d3d10Device.OutputMerger.SetDepthStencilState(new DepthStencilState(this.d3d10Device, this.depthNonState), 1);
                    else
                        this.d3d10Device.OutputMerger.SetDepthStencilState(new DepthStencilState(this.d3d10Device, this.depthState), 1);
                    com.shader.effect.GetVariableByIndex(0).AsMatrix().SetMatrix(camCom.ViewMatrix);
                    com.shader.effect.GetVariableByIndex(1).AsMatrix().SetMatrix(camCom.projectionMatrix);
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
                //font.DrawText(null, "SIMPLE TEXT", 0, 0, Color4.Black);
                //font.DrawText(null, "SIMPLE TEXT", 1, 1, Color4.White);
                //this.font.DrawText(null, "Camera Eye: " + this.camera.Eye.ToString(), 3, 3, Color4.White);
                //font.DrawText(null, String.Format("Yaw: {0}\nPitch: {1}\nRoll: {2}", camera.RadianY, camera.RadianX, camera.RadianZ), 3, 3, Color4.White);

                // Present our drawn scene waiting for one vertical sync
                this.swapChain.Present(1, PresentFlags.None);
            }
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
