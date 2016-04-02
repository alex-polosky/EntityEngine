using System;
using System.Collections.Generic;
using System.IO;
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

        public static D3D10.Device MainWindowDevice;
        public static Map MapGlobal;
        public static Map MapMainMenu;
        public static Map MapLoaded;
    }

    public static partial class Extensions
    {
        // Thanks to nawfal (http://stackoverflow.com/questions/1266674/how-can-one-get-an-absolute-or-normalized-file-path-in-net)
        // (I prefer using lower case instead of upper case
        public static string PathNormalize(this string path)
        {
            if (path[0] == '.')
                path = Path.Combine(Directory.GetCurrentDirectory(), path);
            return Path.GetFullPath(new Uri(path).LocalPath)
               .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
               .ToLowerInvariant();
        }
    }
}
