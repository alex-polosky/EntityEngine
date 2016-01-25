using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEditor
{
    public class AssetNode
    {
        public string name;
        public AssetNode parent;
        public List<AssetNode> children;
        public List<Asset> assetts;
        public List<string> childrenNames
        {
            get
            {
                List<string> names = new List<string>();
                foreach (AssetNode node in children)
                    names.Add(node.name);
                return names;
            }
        }
        public Dictionary<string, AssetNode> childrenNameMapping
        {
            get
            {
                Dictionary<string, AssetNode> mapping = new Dictionary<string, AssetNode>();
                foreach (AssetNode node in children)
                    mapping.Add(node.name, node);
                return mapping;
            }
        }

        public AssetNode()
        {
            name = "";
            parent = null;
            children = new List<AssetNode>();
            assetts = new List<Asset>();
        }

        public AssetNode(string name, AssetNode node)
        {
            this.name = name;
            parent = node;
            children = new List<AssetNode>();
            assetts = new List<Asset>();
        }
    }
}