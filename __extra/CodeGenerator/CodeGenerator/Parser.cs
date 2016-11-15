using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CodeGenerator
{
    class Parser
    {
        private Parts.Folder ParseFolderNode(XmlNode xNode)
        {
            var toret = new Parts.Folder();

            if (xNode.Attributes["name"] != null)
                toret.name = xNode.Attributes["name"].Value;

            foreach (XmlNode node in xNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Folder":
                        toret.folders.Add(ParseFolderNode(node));
                        break;
                    case "FolderTemplate":
                        foreach (var fName in new string[] { "Global", "MainMenu" })
                        {
                            XmlElement xElem = (XmlElement)node;
                            xElem.SetAttribute("name", fName);

                            toret.folders.Add(ParseFolderNode((XmlNode)xElem));
                        }
                        break;
                    case "File":
                        toret.files.Add(ParseFileNode(node));
                        break;
                    case "Project":
                        toret.projects.Add(ParseProjectNode(node));
                        break;
                    case "Solution":
                        toret.isSolution = true;
                        if (node.Attributes["name"] != null)
                            toret.solutionName = node.Attributes["name"].Value;
                        break;
                    default:
                        break;
                }
            }

            return toret;
        }

        private Parts.File ParseFileNode(XmlNode xNode)
        {
            var toret = new Parts.File();

            if (xNode.Attributes["name"] != null)
                toret.name = xNode.Attributes["name"].Value;
            if (xNode.Attributes["type"] != null)
                toret.type = (Parts.File.Type)
                    Enum.Parse(typeof(Parts.File.Type), xNode.Attributes["type"].Value.ToUpper());

            switch (toret.type)
            {
                case Parts.File.Type.NONE:
                    break;
                case Parts.File.Type.CS:
                    toret.ext = "cs";
                    break;
                case Parts.File.Type.PY:
                    toret.ext = "py";
                    break;
                case Parts.File.Type.SETTING:
                    toret.ext = "setting";
                    break;
                default:
                    break;
            }

            foreach (XmlNode node in xNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Name":
                        toret.name = node.InnerText;
                        break;
                    case "Ext":
                        toret.ext = node.InnerText;
                        break;
                    case "Mapping":
                        toret.mappings.Add(
                            node.Attributes["name"].Value,
                            node.Attributes["value"].Value);
                        break;
                    case "API":
                        toret.api = ParseAPINode(node);
                        break;
                    default:
                        break;
                }
            }

            return toret;
        }

        private Parts.Project ParseProjectNode(XmlNode xNode)
        {
            var toret = new Parts.Project();

            if (xNode.Attributes["name"] != null)
                toret.name = xNode.Attributes["name"].Value;

            foreach (XmlNode node in xNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Folder":
                        toret.folders.Add(ParseFolderNode(node));
                        break;
                    case "File":
                        toret.files.Add(ParseFileNode(node));
                        break;
                    case "Reference":
                        toret.references.Add(node.Attributes["name"].Value);
                        break;
                    default:
                        break;
                }
            }

            return toret;
        }

        private Parts.API ParseAPINode(XmlNode xNode)
        {
            var toret = new Parts.API();

            return toret;
        }

        public List<Parts.Folder> ParseXmlDocument(XmlDocument xDoc)
        {
            XmlNode root = xDoc.ChildNodes[0];
            if (root.Name.ToLower() == "xml")
                root = xDoc.ChildNodes[1];
            if (root.Name.ToLower() != "folderstructure")
                throw new XmlException("First child node must be of type 'folderstructure'");

            List<Parts.Folder> folders = new List<Parts.Folder>();
            foreach (XmlNode node in root.ChildNodes)
                folders.Add(ParseFolderNode(node));

            return folders;
        }

        public List<Parts.Folder> ParseXmlDocumentS(string xmlString)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xmlString);
            return ParseXmlDocument(xDoc);
        }

        public Parser()
        {
        }
    }
}
