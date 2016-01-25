using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEditor
{
    public enum AssetType
    {
        Audio,
        Component,
        Entity,
        Model,
        Script,
        Shader,
        String
    }

    public class Asset
    {
        private AssetType _type;
        private List<string> _hierarchy;
        private string _name;
        private string _ext;
        private string _path;
        private Map _map;

        public AssetType AssetType { get { return _type; } }
        public List<string> Hierarchy { get { return _hierarchy; } }
        public string Name { get { return _name; } }
        public string Extension { get { return _ext; } }
        public string AssetPath { get { return _path; } }
        public Map Map { get { return _map; } }

        public Asset(Map map, string assettPath)
        {
            if (!File.Exists(assettPath))
            {
                throw new FileNotFoundException(string.Format("Asset not found using assettPath: {0}", assettPath));
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

            foreach (AssetType assettType in Enum.GetValues(typeof(AssetType)))
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
}
