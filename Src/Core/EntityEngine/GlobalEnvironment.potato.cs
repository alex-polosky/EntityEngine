using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using SharpDX.Windows;
using D3D10 = SharpDX.Direct3D10;

using EntityFramework;
using EntityFramework.Components;

namespace EntityEngine
{
    public static class GlobalEnvironment
    {
        public const string Version = "alpha_build_100";

        private static D3D10.Device _device;

        public static D3D10.Device MainWindowDevice
        {
            get
            {
                return _device;
            }
            set
            {
                _device = value;
            }
        }
    }
}
