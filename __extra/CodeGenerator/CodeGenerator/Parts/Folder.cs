using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Parts
{
    public class Folder
    {
        public string name;
        public List<File> files;
        public List<Folder> folders;
        public bool isSolution;
        public string solutionName;
        public List<Project> projects;

        public Folder()
        {
            files = new List<File>();
            folders = new List<Folder>();
            projects = new List<Project>();
        }
    }
}
