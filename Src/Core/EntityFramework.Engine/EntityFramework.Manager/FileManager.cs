using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework;
using EntityFramework.AssetFileInterface;

namespace EntityFramework.Manager
{
    public class FileManager : IFileManager
    {
        #region Private Variables
        private List<IAssetFileInterface> loadedAssets;
        #endregion Private Variables

        #region Public Variables
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
        public IAssetFileInterface GetAssetFileFromPath(string fileName)
        {
            throw new NotImplementedException();
        }

        public void LoadAllAssetFiles(string folderPath)
        {
            LoadAllAudioAssets(folderPath);
            LoadAllComponentAssets(folderPath);
            LoadAllEntityAssets(folderPath);
            LoadAllModelAssets(folderPath);
            LoadAllScenarioAssets(folderPath);
            LoadAllScriptAssets(folderPath);
            LoadAllShaderAssets(folderPath);
            LoadAllStringAssets(folderPath);
        }

        public void LoadAllAudioAssets(string folderPath)
        {
            Serialize.String s = new Serialize.String() { fileData = "" };
            throw new NotImplementedException();
        }

        public void LoadAllComponentAssets(string folderPath)
        {
            throw new NotImplementedException();
        }

        public void LoadAllEntityAssets(string folderPath)
        {
            throw new NotImplementedException();
        }

        public void LoadAllModelAssets(string folderPath)
        {
            throw new NotImplementedException();
        }

        public void LoadAllScenarioAssets(string folderPath)
        {
            throw new NotImplementedException();
        }

        public void LoadAllScriptAssets(string folderPath)
        {
            throw new NotImplementedException();
        }

        public void LoadAllShaderAssets(string folderPath)
        {
            throw new NotImplementedException();
        }

        public void LoadAllStringAssets(string folderPath)
        {
            throw new NotImplementedException();
        }
        #endregion Public Methods

        #region Constructor
        public FileManager()
        {
        }
        #endregion Constructor

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
