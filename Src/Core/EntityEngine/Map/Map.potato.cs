using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

using EntityFramework;
using EntityFramework.Components;

namespace EntityEngine
{
    public class MapSetting
    {
        private static string pathSettings = "SETTINGS";
        private static string pathCom = "component.definition";
        private static string pathLib = "library.definition";
        private static string pathGid = "guid.definition";

        private Map _map;
        private Dictionary<string, string> _componentDefinition;
        private Dictionary<string, string> _libraryDefinition;
        private Dictionary<string, string> _guidDefinition;

        public Dictionary<string, Dictionary<string, string>> Definitions
        {
            get
            {
                return new Dictionary<string, Dictionary<string, string>>()
                {
                    {"Component", _componentDefinition},
                    {"Library", _libraryDefinition},
                    {"Guid", _guidDefinition}
                };
            }
        }

        private void LoadDefinition(string fileName, ref Dictionary<string, string> definition)
        {
            var path = System.IO.Path.Combine(_map.MapPath, pathSettings, fileName);
            string data = "";
            if (!System.IO.File.Exists(path))
                return;
            using (StreamReader sr = new StreamReader(path))
            {
                data = sr.ReadToEnd();
            }
            foreach (string line in data.Split('\n'))
            {
                if (line.Count() == 0)
                    continue;
                if (line[0] == '#')
                    continue;
                var set = line.Split('=');
                if (set.Count() < 2)
                    continue;
                definition[set[0]] = set[1];
            }
        }

        public void LoadComponentDefinition()
        {
            _componentDefinition = new Dictionary<string, string>();
            LoadDefinition(pathCom, ref _componentDefinition);
        }

        public void LoadLibraryDefinition()
        {
            _libraryDefinition = new Dictionary<string, string>();
            LoadDefinition(pathLib, ref _libraryDefinition);
        }

        public void LoadGuidDefinition()
        {
            _guidDefinition = new Dictionary<string, string>();
            LoadDefinition(pathGid, ref _guidDefinition);
        }

        public MapSetting(Map map, bool loadDefinitions = true)
        {
            _map = map;
            if (loadDefinitions)
            {
                LoadComponentDefinition();
                LoadLibraryDefinition();
                LoadGuidDefinition();
            }
        }
    }

    public class Map
    {
        #region Private Vars
        private string _mapPath;
        private MapSetting _settings;
        private Dictionary<AssetType, string> _mapFolderPaths;
        private Dictionary<AssetType, List<Asset>> _assetts;
        private AssetNode _hierarchyNodes;
        private AssetNode _typeNodes;
        #endregion

        #region Public Vars
        public string MapPath { get { return _mapPath; } }
        public MapSetting MapSettings { get { return _settings; } }
        public Dictionary<AssetType, string> MapFolderPaths { get { return _mapFolderPaths; } }
        public Dictionary<AssetType, List<Asset>> AssetsOfType { get { return _assetts; } }
        public List<Asset> Assets
        {
            get
            {
                List<Asset> assetts = new List<Asset>();
                foreach (AssetType assettType in Enum.GetValues(typeof(AssetType)))
                    foreach (Asset assett in _assetts[assettType])
                        assetts.Add(assett);
                return assetts;
            }
        }
        public AssetNode HierarchyNodes { get { return _hierarchyNodes; } }
        public AssetNode TypeNodes { get { return _typeNodes; } }
        #endregion

        #region Public Methods
        // searchQuery examples:
        // generic; instruments; instruments.guitar; 
        // hierarchy search must start with the top and works way down
        // searching for guitar would return nothing, as there's no top level for guitar
        public List<Asset> GetAssetsFromHierarchy(string searchQuery, AssetType assettTypeSearch)
        {
            List<Asset> assetts = new List<Asset>();

            string[] hierarchy = searchQuery.Split('.');
            foreach (Asset assett in _assetts[assettTypeSearch])
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
        public List<Asset> GetAssetsFromHierarchy(string searchQuery)
        {
            List<Asset> assetts = new List<Asset>();

            foreach (AssetType assettType in Enum.GetValues(typeof(AssetType)))
                foreach (Asset assett in GetAssetsFromHierarchy(searchQuery, assettType))
                    assetts.Add(assett);

            return assetts;
        }

        public Asset GetAssetFromPath(string path)
        {
            foreach (Asset asset in Assets)
                if (asset.AssetPath.PathNormalize() == path.PathNormalize())
                    return asset;
            return null;
        }

        public Asset GetAssetFromGuid(Guid guid, bool useCWD=true)
        {
            Asset asset = null;
            string path = GuidManager.GetFromGuid(guid);
            if (useCWD)
                path = Path.Combine(Directory.GetCurrentDirectory(), path);
            path = path.Split(':')[0] + ":" + path.Split(':')[1];
            if (path != null && path != "")
            {
                foreach (Asset a in Assets)
                {
                    if (a.AssetPath.PathNormalize() == path.PathNormalize())
                    {
                        asset = a;
                        break;
                    }
                }
            }
            return asset;
        }
        public Guid GetGuidFromAsset(Asset asset)
        {
            Guid guid = GuidManager.NULL;
            switch (asset.AssetType)
            {
                case AssetType.Component:
                case AssetType.Entity:
                    guid = GuidManager.GetGuidOfObject(asset.AssetPath);
                    break;
            }
            return guid;
        }
        #endregion

        #region Private Methods
        private void LoadMapPaths()
        {
            _mapFolderPaths = new Dictionary<AssetType, string>()
            {
                {AssetType.Audio, Path.Combine(_mapPath, Properties.Settings.Default.FolderAudio)},
                {AssetType.Component, Path.Combine(_mapPath, Properties.Settings.Default.FolderComponents)},
                {AssetType.Entity, Path.Combine(_mapPath, Properties.Settings.Default.FolderEntities)},
                {AssetType.Model, Path.Combine(_mapPath, Properties.Settings.Default.FolderModels)},
                //TODO : Add script path to settings
                {AssetType.Script, Path.Combine(_mapPath, "Scripts")},
                //TODO : Add scenarios path to settings
                {AssetType.Scenario, Path.Combine(_mapPath, "Scenarios")},
                {AssetType.Shader, Path.Combine(_mapPath, Properties.Settings.Default.FolderShaders)},
                {AssetType.String, Path.Combine(_mapPath, Properties.Settings.Default.FolderStrings)}
            };
        }

        private void LoadAllAssets()
        {
            _assetts = new Dictionary<AssetType, List<Asset>>();
            foreach (AssetType assettType in Enum.GetValues(typeof(AssetType)))
            {
                string _assettPath = _mapFolderPaths[assettType];
                if (!Directory.Exists(_assettPath))
                    Directory.CreateDirectory(_assettPath);
                _assetts[assettType] = new List<Asset>();
                LoadAssetsInDir(assettType, _assettPath);
            }
        }

        private void LoadAssetsInDir(AssetType assettType, string rootPath)
        {
            foreach (string path in Directory.GetDirectories(rootPath))
            {
                LoadAssetsInDir(assettType, path);
            }

            foreach (string file in Directory.GetFiles(rootPath))
            {
                _assetts[assettType].Add(new Asset(this, file));
            }
        }

        private void BuildHierarchyNodes()
        {
            _hierarchyNodes = new AssetNode();
            _hierarchyNodes.name = "Hierarchy Nodes";

            foreach (Asset assett in Assets)
            {
                List<string> assettHierarchy = assett.Hierarchy;
                AssetNode node = _hierarchyNodes;
                foreach (string level in assettHierarchy)
                {
                    if (!node.childrenNames.Contains(level))
                        node.children.Add(new AssetNode(level, node));
                    node = node.childrenNameMapping[level];
                }
                node.assetts.Add(assett);
            }
        }

        private void BuildTypeNodes()
        {
            _typeNodes = new AssetNode();
            _typeNodes.name = "Type Nodes";

            AssetNode node = _typeNodes;
            foreach (AssetType aType in Enum.GetValues(typeof(AssetType)))
                node.children.Add(new AssetNode(aType.ToString(), node));

            foreach (Asset assett in Assets)
            {
                var type = assett.AssetType.ToString();
                if (!node.childrenNames.Contains(type))
                    node.children.Add(new AssetNode(type, node));
                node.childrenNameMapping[type].assetts.Add(assett);
            }
        }

        private void BuildNodes()
        {
            BuildHierarchyNodes();
            BuildTypeNodes();
        }

        private void BuildGuidCollection()
        {
            foreach (Asset asset in _assetts[AssetType.Component])
            {
                string data;
                Newtonsoft.Json.Linq.JObject json;
                string guid = "";
                using (StreamReader sr = new StreamReader(asset.AssetPath))
                {
                    data = sr.ReadToEnd();
                }
                try
                {
                    json = (Newtonsoft.Json.Linq.JObject)
                        JsonConvert.DeserializeObject(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
                    try
                    {
                        guid = json["guid"].ToString();
                    }
                    catch
                    {
                        try
                        {
                            guid = json["json"]["guid"].ToString();
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                catch
                {
                    //TODO: Raise error regarding deserialization
                    guid = GuidManager.NewGuid().ToString();
                }
                GuidManager.RegisterGuid(Guid.Parse(guid), asset.AssetPath);
            }
            foreach (Asset asset in _assetts[AssetType.Entity])
            {
                string data = "";
                Newtonsoft.Json.Linq.JObject json = null;
                string guid = "";
                string path = "";
                using (StreamReader sr = new StreamReader(asset.AssetPath))
                {
                    data = sr.ReadToEnd();
                }
                try
                {
                    json = (Newtonsoft.Json.Linq.JObject)
                        JsonConvert.DeserializeObject(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
                    try
                    {
                        guid = json["json"]["guid"].ToString();
                    }
                    catch { throw; }
                }
                catch
                {
                    //TODO: Raise error regarding deserialization
                    guid = GuidManager.NewGuid().ToString();
                }
                path = asset.AssetPath;
                GuidManager.RegisterGuid(Guid.Parse(guid), path);
                Newtonsoft.Json.Linq.JToken coms = null;
                try
                {
                    coms = json["components"];
                }
                catch { }
                if (coms != null)
                {
                    int comCount = -1;
                    foreach (var com in coms.ToList())
                    {
                        comCount += 1;

                        // We want to skip the reference lists
                        try
                        {
                            guid = com["reference"].ToString();
                            continue;
                        }
                        catch { }
                        // Get the guid and naming convention
                        path = asset.AssetPath;
                        path += ":";
                        try
                        {
                            guid = com["json"]["guid"].ToString();
                            path += com["className"].ToString().Split('.').Last();
                        }
                        catch
                        {
                            guid = GuidManager.NewGuid().ToString();
                            path += "[" + comCount.ToString() + "]";
                        }

                        GuidManager.RegisterGuid(Guid.Parse(guid), path);
                    }
                }
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
            LoadAllAssets();
            BuildNodes();
            BuildGuidCollection();
            _settings = new MapSetting(this);
        }
        #endregion

        internal void UnLoad()
        {
            throw new NotImplementedException();
        }
    }
}
