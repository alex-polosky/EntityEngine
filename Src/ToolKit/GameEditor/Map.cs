using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEditor
{
    public class HierarchyAssettNode
    {
        public string name;
        public HierarchyAssettNode parent;
        public List<HierarchyAssettNode> children;
        public List<Assett> assetts;
        public List<string> childrenNames 
        { 
            get 
            { 
                List<string> names = new List<string>();
                foreach (HierarchyAssettNode node in children)
                    names.Add(node.name);
                return names; 
            } 
        }
        public Dictionary<string, HierarchyAssettNode> childrenNameMap
        {
            get
            {
                Dictionary<string, HierarchyAssettNode> map = new Dictionary<string, HierarchyAssettNode>();
                foreach (HierarchyAssettNode node in children)
                    map.Add(node.name, node);
                return map;
            }
        }

        public HierarchyAssettNode()
        {
            name = "";
            parent = null;
            children = new List<HierarchyAssettNode>();
            assetts = new List<Assett>();
        }

        public HierarchyAssettNode(string name, HierarchyAssettNode node)
        {
            this.name = name;
            parent = node;
            children = new List<HierarchyAssettNode>();
            assetts = new List<Assett>();
        }
    }

    public class Map
    {
#region Private Vars
        private string _mapPath;
        private Dictionary<AssettType, string> _mapFolderPaths;
        private Dictionary<AssettType, List<Assett>> _assetts;
        private HierarchyAssettNode _hierarchy;
#endregion

#region Public Vars
        public string MapPath { get { return _mapPath; } }
        public Dictionary<AssettType, string> MapFolderPaths { get { return _mapFolderPaths; } }
        public Dictionary<AssettType, List<Assett>> AssettsOfType { get { return _assetts; } }
        public List<Assett> Assetts 
        { 
            get 
            { 
                List<Assett> assetts = new List<Assett>();
                foreach (AssettType assettType in Enum.GetValues(typeof(AssettType)))
                    foreach (Assett assett in _assetts[assettType])
                        assetts.Add(assett);
                return assetts;
            }
        }
        public HierarchyAssettNode Hierarchy { get { return _hierarchy; } }
#endregion

#region Public Methods
        // searchQuery examples:
        // generic; instruments; instruments.guitar; 
        // hierarchy search must start with the top and works way down
        // searching for guitar would return nothing, as there's no top level for guitar
        public List<Assett> GetAssettsFromHierarchy(string searchQuery, AssettType assettTypeSearch)
        {
            List<Assett> assetts = new List<Assett>();

            string[] hierarchy = searchQuery.Split('.');
            foreach (Assett assett in _assetts[assettTypeSearch])
            {
                if (hierarchy.Length > assett.Hierarchy.Count)
                    continue;
                bool addToGroup = true;
                for (int i = 0; i < hierarchy.Length; i++)
                    if (hierarchy[i] != assett.Hierarchy[i])
                        addToGroup = false;
                if (addToGroup)
                    assetts.Add(assett);
            }

            return assetts;
        }
        public List<Assett> GetAssettsFromHierarchy(string searchQuery)
        {
            List<Assett> assetts = new List<Assett>();

            foreach (AssettType assettType in Enum.GetValues(typeof(AssettType)))
                foreach (Assett assett in GetAssettsFromHierarchy(searchQuery, assettType))
                    assetts.Add(assett);

            return assetts;
        }
#endregion

#region Private Methods
        private void LoadMapPaths()
        {
            _mapFolderPaths = new Dictionary<AssettType, string>()
            {
                {AssettType.Audio, Path.Combine(_mapPath, Properties.Settings.Default.FolderAudio)},
                {AssettType.Component, Path.Combine(_mapPath, Properties.Settings.Default.FolderComponents)},
                {AssettType.Entity, Path.Combine(_mapPath, Properties.Settings.Default.FolderEntities)},
                {AssettType.Model, Path.Combine(_mapPath, Properties.Settings.Default.FolderModels)},
                {AssettType.Shader, Path.Combine(_mapPath, Properties.Settings.Default.FolderShaders)},
                {AssettType.String, Path.Combine(_mapPath, Properties.Settings.Default.FolderStrings)}
            };
        }

        private void LoadAllAssetts()
        {
            _assetts = new Dictionary<AssettType, List<Assett>>();
            foreach (AssettType assettType in Enum.GetValues(typeof(AssettType)))
            {
                string _assettPath = _mapFolderPaths[assettType];
                if (!Directory.Exists(_assettPath))
                    Directory.CreateDirectory(_assettPath);
                _assetts[assettType] = new List<Assett>();
                LoadAssettsInDir(assettType, _assettPath);
            }
        }

        private void LoadAssettsInDir(AssettType assettType, string rootPath)
        {
            foreach (string path in Directory.GetDirectories(rootPath))
            {
                LoadAssettsInDir(assettType, path);
            }

            foreach (string file in Directory.GetFiles(rootPath))
            {
                _assetts[assettType].Add(new Assett(this, file));
            }
        }

        private void BuildHierarchy()
        {
            _hierarchy = new HierarchyAssettNode();
            _hierarchy.name = "root";

            foreach (Assett assett in Assetts)
            {
                List<string> assettHierarchy = assett.Hierarchy;
                HierarchyAssettNode node = _hierarchy;
                foreach (string level in assettHierarchy)
                {
                    if (!node.childrenNames.Contains(level))
                        node.children.Add(new HierarchyAssettNode(level, node));
                    node = node.childrenNameMap[level];
                }
                node.assetts.Add(assett);
            }
        }
#endregion

#region Constructor
        public Map(string mapPath)
        {
            if (!Directory.Exists(mapPath))
            {
                throw new FileNotFoundException(string.Format("Folder not found using mapPath: {0}", mapPath));
            }
            _mapPath = mapPath;
            LoadMapPaths();
            LoadAllAssetts();
            BuildHierarchy();
        }
#endregion
    }
}
