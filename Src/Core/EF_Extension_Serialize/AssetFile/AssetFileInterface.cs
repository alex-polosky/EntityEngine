using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework.AssetFileInterface;

namespace EntityFramework.Serialize
{
    public class AssetFileInterface : IAssetFileInterface
    {
        #region Internal Variables
        internal string fileData;
        internal string fileDirectory;
        internal string fileName;
        internal string filePath;
        internal string fileType;
        internal bool isLoaded;
        internal byte[] rawFileData;

        internal List<string> hierarchy;
        internal string map;
        #endregion Internal Variables

        #region Public Variables
        public string FileData { get { return fileData; } }
        public string FileDirectory { get { return fileDirectory; } }
        public string FileName { get { return fileName; } }
        public string FilePath { get { return filePath; } }
        public string FileType { get { return fileType; } }
        public bool IsLoaded { get { return isLoaded; } }
        public byte[] RawFileData { get { return rawFileData; } }

        public List<string> Hierarchy { get { return hierarchy; } }
        public string Map { get { return map; } }
        #endregion Public Variables
    }
}
