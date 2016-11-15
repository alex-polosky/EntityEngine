using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Parts
{
    public class Project
    {
        public string name;
        public List<string> references;
        public List<Folder> folders;
        public List<File> files;

        public Project()
        {
            references = new List<string>();
            folders = new List<Folder>();
            files = new List<File>();
        }
    }
}
