using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EntityEngine;

namespace GameEditor
{
    public partial class mainForm : Form
    {
#region Private Variables
        private Map _currentMap;
        private Map _globalMap;
        private Map _mainMenuMap;
        private List<Map> _loadedMaps { get {
            List<Map> maps = new List<Map>();
            if (_globalMap != null)
                maps.Add(_globalMap);
            if (_mainMenuMap != null)
                maps.Add(_mainMenuMap);
            if (_currentMap != null)
                maps.Add(_currentMap);
            return maps;
        } }
#endregion Private Variables

#region Public Variables
#endregion Public Variables

#region Private Methods
        private void LoadMapAssets(Map map)
        {
            AssetNode hNode;
            TreeNode root;

            hNode = map.HierarchyNodes;
            this.assettHierarchyNodes.Nodes.Add(hNode.name);
            root = this.assettHierarchyNodes.Nodes[this.assettHierarchyNodes.Nodes.Count - 1];
            foreach (AssetNode node in hNode.children)
                root.PopulateWithAssettNodes(node, root);
            root.Text = map.MapPath.Split('\\')[map.MapPath.Split('\\').Count() - 1];

            hNode = map.TypeNodes;
            this.assettTypeNodes.Nodes.Add(hNode.name);
            root = this.assettTypeNodes.Nodes[this.assettHierarchyNodes.Nodes.Count - 1];
            foreach (AssetNode node in hNode.children)
                root.PopulateWithAssettNodes(node, root);
            root.Text = map.MapPath.Split('\\')[map.MapPath.Split('\\').Count() - 1];
        }

        private void SettingsPopUp()
        {
            new Dialog.SettingsForm().Show();
        }

        private void NewMap()
        {
        }

        private void OpenMap()
        {
            if (_currentMap == null)
            {
                _currentMap = new Map(@"P:\Code\Git\EntityEngine\Maps\Testing");
                LoadMapAssets(_currentMap);
                UpdateToolbar();

                //TODO: Add this chunk to a test x'D
                //var entityAsset = _currentMap.GetAssetFromGuid(Guid.Parse("4a97e680-b883-42da-ba71-e6e729118bd2"));
                //var entityG = _currentMap.GetAssetsFromHierarchy("instruments.guitar", AssetType.Entity)[0];
                //var entity0Guid = _currentMap.GetGuidFromAsset(entityAsset);
                //var entity1Guid = _currentMap.GetGuidFromAsset(entityG);
                
                //var comSaveGroup = _currentMap.GetAssetFromGuid(Guid.Parse("93bbca35-ff95-4527-9e19-ed8c2e5c4bed"));

                //var comNestedGuid = GuidManager.GetGuidOfObject(entityAsset.AssetPath + ":RenderComponent");
                //var nestedFind = _currentMap.GetAssetFromGuid(comNestedGuid);
            }
        }

        private void SaveMap()
        {
        }

        private void CloseMap()
        {
        }

        private void UpdateToolbar()
        {
            if (_currentMap == null)
            {
                this.newToolStripMenuItem.Enabled = true;
                this.openToolStripMenuItem.Enabled = true;
                this.saveToolStripMenuItem.Enabled = false;
                this.closeStripMenuItem.Enabled = false;
            }
            else
            {
                this.newToolStripMenuItem.Enabled = false;
                this.openToolStripMenuItem.Enabled = false;
                this.saveToolStripMenuItem.Enabled = true;
                this.closeStripMenuItem.Enabled = true;
            }
        }
#endregion Private Methods

#region Public Methods
#endregion Public Methods

#region Constructor
        public mainForm()
        {
            InitializeComponent();
        }
#endregion Constructor

#region Handlers
    #region Default Handlers
        protected override void OnShown(EventArgs e)
        {
            if (Properties.Settings.Default.FirstRun)
            {
                Properties.Settings.Default.FirstRun = false;
                Properties.Settings.Default.Save();
                SettingsPopUp();
            }
            if (Properties.Settings.Default.AutoLoadMaps)
            {
                _globalMap = new Map(Properties.Settings.Default.MapGlobal);
                _mainMenuMap = new Map(Properties.Settings.Default.MapMainMenu);
                LoadMapAssets(_globalMap);
                LoadMapAssets(_mainMenuMap);
            }
        }
    #endregion Default Handlers

    #region Menu Strip File
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMap();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMap();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveMap();
        }

        private void closeStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseMap();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    #endregion Menu Strip File

    #region Menu Strip Tools
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsPopUp();
        }
    #endregion Menu Strip Tools

    #region Node Handlers
        private void treeView_MouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var a = (Asset)e.Node.Tag;
            if (a != null)
            {
                switch (a.AssetType)
                {
                    //case AssetType.Component:
                    //    var tje = new TestJsonEdit();
                    //    string data;
                    //    using (var sr = new System.IO.StreamReader(a.AssetPath))
                    //    {
                    //        data = sr.ReadToEnd();
                    //    }
                    //    tje.LoadComponentS(data);
                    //    tje.Show();
                    //    break;
                    case AssetType.Component:
                        string data;
                        using (var sr = new System.IO.StreamReader(a.AssetPath))
                        {
                            data = sr.ReadToEnd();
                        }
                        var panel = new Form();
                        panel.Name = "form.component";
                        var com = new GameEditor.Editor.Forms.Component();
                        com.Left = 3;
                        com.Top = 3;
                        //panel.Width = com.//com.Width + 15;
                        //panel.Height = //com.Height + 15;
                        com.LoadComponentS(data);
                        panel.Controls.Add(com);
                        panel.Show();
                        break;
                    case AssetType.Entity:

                        break;
                    default:
                        break;
                }
            }
        }
    #endregion Node Handlers

#endregion Handlers
    }
}
