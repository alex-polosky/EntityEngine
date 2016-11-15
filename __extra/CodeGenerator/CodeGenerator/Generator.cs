using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    class Generator
    {
        public static void GenerateFolderStructure(string target, List<Parts.Folder> folders)
        {
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);

            foreach (var folder in folders)
            {
                foreach (var project in folder.projects)
                {
                    GenerateFolderStructure(Path.Combine(target, folder.name, project.name), project.folders);
                    foreach (var file in project.files)
                    {
                        if (!File.Exists(Path.Combine(target, folder.name, project.name, file.name + '.' + file.ext)))
                            File.Create(Path.Combine(target, folder.name, project.name, file.name + '.' + file.ext));
                    }
                }
                
                GenerateFolderStructure(Path.Combine(target, folder.name), folder.folders);

                foreach (var file in folder.files)
                {
                    if (!File.Exists(Path.Combine(target, folder.name, file.name + '.' + file.ext)))
                        File.Create(Path.Combine(target, folder.name, file.name + '.' + file.ext));
                }
            }
        }

        public Generator()
        {
        }
    }
}
