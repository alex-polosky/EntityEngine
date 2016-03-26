using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameEditor
{
    public partial class gameEditorMainForm : Form
    {
        public gameEditorMainForm()
        {
            InitializeComponent();

            Map map = new Map(@"P:\Code\Git\EntityEngine\Maps\Testing");
            var assetts0 = map.GetAssetsFromHierarchy("generic");
            var assetts1 = map.GetAssetsFromHierarchy("instruments.guitar");
            var assetts2 = map.GetAssetsFromHierarchy("instruments");
            var assetts3 = map.GetAssetsFromHierarchy("generic", AssetType.Model);
            var assetts4 = map.GetAssetsFromHierarchy("generic", AssetType.Shader);
            var assetts5 = map.GetAssetsFromHierarchy("generic", AssetType.Component);

            AssetNode hNode = map.HierarchyNodes;
            this.hierarchyTreeView.Nodes.Add(hNode.name);
            TreeNode root = this.hierarchyTreeView.Nodes[0];
            this.TreeViewTabs.TabPages[0].Text = root.Text;
            foreach (AssetNode node in hNode.children)
                root.PopulateWithAssettNodes(node, root);

            hNode = map.TypeNodes;
            this.typeTreeView.Nodes.Add(hNode.name);
            root = this.typeTreeView.Nodes[0];
            this.TreeViewTabs.TabPages[1].Text = root.Text;
            foreach (AssetNode node in hNode.children)
                root.PopulateWithAssettNodes(node, root);
        }
    }
}
