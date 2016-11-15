using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MenuCreater
{
    class Parser
    {
        #region Private Variables
        #endregion Private Variables

        #region Public Variables
        #endregion Public Variables

        #region Private Methods
        private Parts.Menu ParseMenuNode(XmlNode xNode)
        {
            var menu = new Parts.Menu();

            menu.id = xNode.Attributes["id"].Value;

            foreach (XmlNode node in xNode.ChildNodes)
            {
                if (node.Name == "variable")
                {
                    menu.variables.Add(ParseVariableNode(node));
                }
                if (node.Name == "entry")
                {
                    menu.entries.Add(ParseEntryNode(node));
                }
            }

            return menu;
        }

        private Parts.Variable ParseVariableNode(XmlNode xNode)
        {
            var variable = new Parts.Variable();

            variable.language = 
                (Parts.Variable.Language)
                    Enum.Parse(typeof(Parts.Variable.Language), xNode.Attributes["language"].Value.ToUpper());
            variable.name = xNode.Attributes["name"].Value;
            variable.value = xNode.Attributes["value"].Value;

            return variable;
        }

        private Parts.Entry ParseEntryNode(XmlNode xNode)
        {
            var entry = new Parts.Entry();

            entry.id = xNode.Attributes["id"].Value;

            foreach (XmlNode node in xNode.ChildNodes)
            {
                if (node.Name == "action")
                {
                    entry.action = ParseActionNode(node);
                }
                if (node.Name == "text")
                {
                    entry.text = node.InnerText;
                }
                if (node.Name == "picture")
                {
                    entry.picture = node.InnerText;
                }
                if (node.Name == "description")
                {
                    entry.description = node.InnerText;
                }
            }

            return entry;
        }

        private Parts.Action ParseActionNode(XmlNode xNode)
        {
            var action = new Parts.Action();

            var language = xNode.Attributes["language"];
            if (language != null)
                action.language =
                    (Parts.Variable.Language)Enum.Parse(typeof(Parts.Variable.Language), language.Value.ToUpper());
            var passin = xNode.Attributes["passin"];
            if (passin != null)
                action.passIn = passin.Value;
            if (action.language != Parts.Variable.Language.None)
                action.code = xNode.InnerText;

            foreach (XmlNode node in xNode.ChildNodes)
            {
                if (node.Name == "menu")
                {
                    action.menu = ParseMenuNode(node);
                }
            }

            return action;
        }
        #endregion Private Methods

        #region Public Methods
        public Parts.Menu ParseXmlDocument(XmlDocument xDoc)
        {
            XmlNode root = xDoc.ChildNodes[0];
            if (root.Name == "xml")
                root = xDoc.ChildNodes[1];
            if (root.Name != "menu")
                throw new XmlException("First child node must be of type 'menu'");

            Parts.Menu menu = ParseMenuNode(root);

            return menu;
        }

        public Parts.Menu ParseXmlDocumentS(string xmlString)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xmlString);
            return ParseXmlDocument(xDoc);
        }
        #endregion Public Methods

        #region Constructor
        public Parser()
        {
        }
        #endregion Constructor

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
