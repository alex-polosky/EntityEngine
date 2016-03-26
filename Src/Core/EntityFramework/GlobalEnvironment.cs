using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public static class GlobalEnvironment
    {
        public const string Version = "alpha_build_100";
    }

    public static partial class Extensions
    {
        // Thanks to nawfal (http://stackoverflow.com/questions/1266674/how-can-one-get-an-absolute-or-normalized-file-path-in-net)
        // (I prefer using lower case instead of upper case
        public static string PathNormalize(this string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
               .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
               .ToLowerInvariant();
        }
    }
}
