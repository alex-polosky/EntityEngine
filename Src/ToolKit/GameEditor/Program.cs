using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GameEditor
{
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

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
            {
                var filename = new System.Reflection.AssemblyName(e.Name).Name;
                var path = System.IO.Directory.GetCurrentDirectory().Split(System.IO.Path.DirectorySeparatorChar).ToList();
                while (path[path.Count() - 1] != "Bin")
                    path.RemoveAt(path.Count() - 1);
                var pathBin = string.Join(System.IO.Path.DirectorySeparatorChar.ToString(), path);
                path[path.Count() - 1] = "Lib";
                var pathLib = string.Join(System.IO.Path.DirectorySeparatorChar.ToString(), path);
                // Attempt to load from lib first
                foreach (var file in System.IO.Directory.GetFiles(pathLib))
                {
                    if (file.Split(System.IO.Path.DirectorySeparatorChar).Last().Split(new string[]{".dll"}, StringSplitOptions.None)[0] == filename)
                        return System.Reflection.Assembly.LoadFrom(file);
                }
                foreach (var dir in System.IO.Directory.GetDirectories(pathLib))
                {
                    foreach (var file in System.IO.Directory.GetFiles(dir))
                    {
                        if (file.Split(System.IO.Path.DirectorySeparatorChar).Last().Split(new string[] { ".dll" }, StringSplitOptions.None)[0] == filename)
                            return System.Reflection.Assembly.LoadFrom(file);
                    }
                }
                // Attempt to load from bin
                foreach (var file in System.IO.Directory.GetFiles(pathBin))
                {
                    if (file.Split(System.IO.Path.DirectorySeparatorChar).Last().Split(new string[] { ".dll" }, StringSplitOptions.None)[0] == filename)
                        return System.Reflection.Assembly.LoadFrom(file);
                }
                foreach (var dir in System.IO.Directory.GetDirectories(pathBin))
                {
                    foreach (var file in System.IO.Directory.GetFiles(dir))
                    {
                        if (file.Split(System.IO.Path.DirectorySeparatorChar).Last().Split(new string[] { ".dll" }, StringSplitOptions.None)[0] == filename)
                            return System.Reflection.Assembly.LoadFrom(file);
                    }
                }
                return null;
            };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new gameEditorMainForm());
            //Application.Run(new TestJsonEdit());
            //Application.Run(new Dialog.Forms.GuidSelector());
            //Application.Run(new Dialog.Forms.GuidManagerForm());
            //Application.Run(new mainForm());

            new TestJsonEdit().Show();
            //new Dialog.Forms.GuidSelector().Show();
            //new Dialog.Forms.GuidManagerForm().Show();
            //new mainForm().Show();
            Application.Run(new Form());
        }
    }
}
