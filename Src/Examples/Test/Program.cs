using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BootStrap0();
            //EntityEngine.BootStrap.Main(AppDomain.CurrentDomain);
        }

        static void BootStrap0()
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
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            Type t = asm.GetType(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".Program");
            t.GetMethod("BootStrap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).Invoke(null, null);
        }
        static void BootStrap()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
