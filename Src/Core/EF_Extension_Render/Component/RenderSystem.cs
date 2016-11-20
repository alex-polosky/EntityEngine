using System;
using System.Collections.Generic;
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
using EntityFramework.ComponentInterfaces;

namespace EntityFramework.Render
{
    public class RenderSystem : EntityFramework.ComponentInterfaces.IRenderSystem
    {
        #region Private Variables

        private IntPtr drawHandle;
        private Control drawForm
        {
            get
            {
                if (drawHandle == null || drawHandle == IntPtr.Zero)
                    return null;
                else
                    return Control.FromHandle(drawHandle);
            }
        }

        private SwapChainDescription swapChainDesc;
        private SwapChain swapChain;
        private D3D10.Device d3d10Device;
        private RenderTargetView renderTargetView;
        private DepthStencilView depthStencilView;
        private DepthStencilStateDescription depthState;
        private DepthStencilStateDescription depthNonState;

        private Viewport viewport;

        #endregion Private Variables

        #region Private functions

        private void initD3DEventWrapper(object sender, EventArgs e)
        {
            initD3D();
        }

        private void initD3D()
        {
            if (drawForm == null)
                return;

            swapChainDesc = new SwapChainDescription
            {
                BufferCount = 2,
                ModeDescription = new ModeDescription
                {
                    Width = drawForm.Width,
                    Height = drawForm.Height,
                    Format = Format.R8G8B8A8_UNorm,
                },
                Usage = Usage.RenderTargetOutput,
            };

            swapChainDesc.ModeDescription.RefreshRate.Numerator = 60;
            swapChainDesc.ModeDescription.RefreshRate.Denominator = 1;

            swapChainDesc.SampleDescription.Quality = 0;
            swapChainDesc.SampleDescription.Count = 1;

            swapChainDesc.OutputHandle = drawHandle;
            swapChainDesc.IsWindowed = true;

            D3D10.Device.CreateWithSwapChain(
                D3D10.DriverType.Hardware,
                DeviceCreationFlags.None,
                swapChainDesc,
                out d3d10Device,
                out swapChain
            );

            using (Texture2D backBuffer = swapChain.GetBackBuffer<Texture2D>(0))
                renderTargetView = new RenderTargetView(d3d10Device, backBuffer);

            d3d10Device.OutputMerger.SetTargets(renderTargetView);

            viewport = new Viewport
            {
                Width = drawForm.Width,
                Height = drawForm.Height,
                MinDepth = 0.0f,
                MaxDepth = 1.0f,
                X = 0,
                Y = 0
            };

            d3d10Device.Rasterizer.SetViewports(viewport);

            var zBufferTextureDescription = new Texture2DDescription
            {
                Format = Format.D16_UNorm,
                ArraySize = 1,
                MipLevels = 1,
                Width = drawForm.Width,
                Height = drawForm.Height,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };
            using (var zBufferTexture = new Texture2D(this.d3d10Device, zBufferTextureDescription))
                depthStencilView = new DepthStencilView(this.d3d10Device, zBufferTexture);
            d3d10Device.OutputMerger.SetTargets(depthStencilView, renderTargetView);

            depthState = new DepthStencilStateDescription()
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
            depthNonState = new DepthStencilStateDescription()
            {
                IsDepthEnabled = false
            };
            d3d10Device.OutputMerger.SetDepthStencilState(
                new DepthStencilState(
                    d3d10Device,
                    depthState
                ), 1
            );

            // Generic font?

            // Camera?

            d3d10Device.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            d3d10Device.Rasterizer.State = new RasterizerState(d3d10Device,
                new RasterizerStateDescription
                {
                    CullMode = CullMode.Back,
                    FillMode = FillMode.Solid
                }
            );
        }

        #endregion Private functions

        #region Public Interface functions

        public override void Update(double timeDelta)
        {
            base.Update(timeDelta);
        }

        public override void Init(Type comType)
        {
            base.Init(comType);
        }

        public override void Draw(IntPtr handle)
        {
            if (drawHandle != handle)
            {
                if (drawForm != null)
                    drawForm.SizeChanged -= initD3DEventWrapper;

                drawHandle = handle;
                initD3D();
                drawForm.SizeChanged += initD3DEventWrapper;
            }

            if (drawForm != null && drawForm.Visible)
            {
                d3d10Device.ClearRenderTargetView(this.renderTargetView, (Color4)SharpDX.Color.CornflowerBlue);
                d3d10Device.ClearDepthStencilView(depthStencilView, DepthStencilClearFlags.Depth, 1f, 0);

                foreach (IRender renderCom in _components)
                {
                    if (!renderCom.Active)
                        continue;

                    IPosition posCom = renderCom.Entity.GetComponent<IPosition>();

                    var inputAssembler = d3d10Device.InputAssembler;

                    // ToDo: Set this up for each individual component
                    inputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

                    //inputAssembler.SetVertexBuffers(0, renderCom.GetShader())
                }

                swapChain.Present(1, PresentFlags.None);
            }
        }

        #endregion Public Interface functions
    }
}
