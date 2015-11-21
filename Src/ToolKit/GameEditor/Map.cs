using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEditor
{
    public static class Extensions
    {
        // Thanks to nawfal (http://stackoverflow.com/questions/1266674/how-can-one-get-an-absolute-or-normalized-file-path-in-net)
        // (I prefer using lower case instead of upper case
        public static string PathNormalize(this string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
               .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
               .ToLowerInvariant();
        }
    }

    public enum AssettType
    {
        Audio,
        Component,
        Entity,
        Model,
        Shader
    }

    public class Assett
    {
        private AssettType _type;
        private List<string> _hierarchy;
        private string _name;
        private string _ext;
        private string _path;
        private Map _map;

        public AssettType AssettType { get { return _type; } }
        public List<string> Hierarchy { get { return _hierarchy; } }
        public string Name { get { return _name; } }
        public string Extension { get { return _ext; } }
        public string AssettPath { get { return _path; } }
        public Map Map { get { return _map; } }

        public Assett(Map map, string assettPath)
        {
            if (!File.Exists(assettPath))
            {
                throw new FileNotFoundException(string.Format("Assett not found using assettPath: {0}", assettPath));
            }
            _map = map;
            _path = assettPath; 

            string[] mapDirectories = map.MapPath.PathNormalize().Split(Path.DirectorySeparatorChar);
            string[] assettDirectories = _path.PathNormalize().Split(Path.DirectorySeparatorChar);
            string assettBaseDirectory = "";
            for (int i = 0; i <= mapDirectories.Length; i++)
            {
                assettBaseDirectory += assettDirectories[i];
                assettBaseDirectory += Path.DirectorySeparatorChar;
            }

            foreach (AssettType assettType in Enum.GetValues(typeof(AssettType)))
            {
                if (Map.MapFolderPaths[assettType].PathNormalize() == assettBaseDirectory.PathNormalize())
                {
                    _type = assettType;
                    break;
                }
            }

            _hierarchy = new List<string>();
            for (int i = mapDirectories.Length + 1; i < assettDirectories.Length - 1; i++)
            {
                _hierarchy.Add(assettDirectories[i]);
            }

            _name = Path.GetFileNameWithoutExtension(assettPath);
            _ext = Path.GetExtension(assettPath);
        }
    }

    public class Map
    {
        private string _mapPath;
        private Dictionary<AssettType, string> _mapFolderPaths;
        private Dictionary<AssettType, List<Assett>> _assetts;

        public string MapPath { get { return _mapPath; } }
        public Dictionary<AssettType, string> MapFolderPaths { get { return _mapFolderPaths; } }
        public Dictionary<AssettType, List<Assett>> Assetts { get { return _assetts; } }


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

        private void LoadMapPaths()
        {
            _mapFolderPaths = new Dictionary<AssettType, string>()
            {
                {AssettType.Audio, Path.Combine(_mapPath, Properties.Settings.Default.FolderAudio)},
                {AssettType.Component, Path.Combine(_mapPath, Properties.Settings.Default.FolderComponents)},
                {AssettType.Entity, Path.Combine(_mapPath, Properties.Settings.Default.FolderEntities)},
                {AssettType.Model, Path.Combine(_mapPath, Properties.Settings.Default.FolderModels)},
                {AssettType.Shader, Path.Combine(_mapPath, Properties.Settings.Default.FolderShaders)}
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

        public Map(string mapPath)
        {
            if (!Directory.Exists(mapPath))
            {
                throw new FileNotFoundException(string.Format("Folder not found using mapPath: {0}", mapPath));
            }
            _mapPath = mapPath;
            LoadMapPaths();
            LoadAllAssetts();
        }
    }
}
