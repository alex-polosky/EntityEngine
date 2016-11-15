using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"..\..\folderstructure.xml";
            string target = @"..\..\__EntityEngine\";

            string xmlData = "";
            using (StreamReader sr = new StreamReader(path))
            {
                xmlData = sr.ReadToEnd();
            }

            Parser parser = new Parser();

            var folders = parser.ParseXmlDocumentS(xmlData);

            Generator.GenerateFolderStructure(target, folders);
        }
    }
}
