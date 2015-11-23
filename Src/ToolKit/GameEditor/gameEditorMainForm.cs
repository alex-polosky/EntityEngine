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
        private void PopulateTree(HierarchyAssettNode hNode, TreeNode tNode)
        {
            tNode.Nodes.Add(hNode.name);
            if (hNode.children.Count > 0)
                foreach (HierarchyAssettNode node in hNode.children)
                    PopulateTree(node, tNode.Nodes[tNode.Nodes.Count - 1]);
            else
                foreach (Assett assett in hNode.assetts)
                    tNode.Nodes[tNode.Nodes.Count-1].Nodes.Add(assett.Name + assett.Extension);
        }

        public gameEditorMainForm()
        {
            InitializeComponent();

            Map map = new Map(@"P:\Code\Git\EntityEngine\Maps\Testing");
            var assetts0 = map.GetAssettsFromHierarchy("generic");
            var assetts1 = map.GetAssettsFromHierarchy("instruments.guitar");
            var assetts2 = map.GetAssettsFromHierarchy("instruments");
            var assetts3 = map.GetAssettsFromHierarchy("generic", AssettType.Model);
            var assetts4 = map.GetAssettsFromHierarchy("generic", AssettType.Shader);
            var assetts5 = map.GetAssettsFromHierarchy("generic", AssettType.Component);

            HierarchyAssettNode hNode = map.Hierarchy;
            this.treeView1.Nodes.Add(hNode.name);
            TreeNode root = this.treeView1.Nodes[0];
            foreach (HierarchyAssettNode node in hNode.children)
                PopulateTree(node, root);
        }
    }
}
