using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using EntityFramework;
using EntityFramework.AssetFileInterface;
using EntityFramework.Engine;

namespace EntityFramework.Manager
{
    public class FileManager : IFileManager
    {
        #region Private Variables
        private List<IAssetFileInterface> loadedAssets;
        private EntityFramework.Engine.Events.ErrorEventHandler onError;
        #endregion Private Variables

        #region Public Variables
        public List<IAssetFileInterface> LoadedAssets { get { return loadedAssets; } }
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
        public IAssetFileInterface GetAssetFileFromPath(string filePath)
        {
            return loadedAssets.SingleOrDefault(a => a.FilePath == filePath);
        }

        public void PrepareAllAssetFiles(string folderPath)
        {
            PrepareAssetFiles<Serialize.Audio>(Path.Combine(folderPath, "Audio"));
            PrepareAssetFiles<Serialize.Component>(Path.Combine(folderPath, "Components"));
            PrepareAssetFiles<Serialize.Entity>(Path.Combine(folderPath, "Entities"));
            PrepareAssetFiles<Serialize.Model>(Path.Combine(folderPath, "Models"));
            PrepareAssetFiles<Serialize.Scenario>(Path.Combine(folderPath, "Scenarios"));
            PrepareAssetFiles<Serialize.Script>(Path.Combine(folderPath, "Scripts"));
            PrepareAssetFiles<Serialize.Shader>(Path.Combine(folderPath, "Shaders"));
            PrepareAssetFiles<Serialize.String>(Path.Combine(folderPath, "Strings"));
        }

        public void PrepareAssetFiles<T>(string folderPath, int depth = 0)
            where T : IAssetFileInterface, new()
        {
            foreach (var folderName in Directory.GetDirectories(folderPath))
            {
                PrepareAssetFiles<T>(Path.Combine(folderPath, folderName), depth + 1);
            }
            foreach (var filePath in Directory.GetFiles(folderPath))
            {
                var fileDirectory = Path.GetDirectoryName(filePath);
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var fileType = Path.GetExtension(filePath).Replace(".", "");
                var fileDirectorySplit = fileDirectory.Split(Path.DirectorySeparatorChar);

                if (loadedAssets.SingleOrDefault(a => a.FilePath == filePath) == null)
                {
                    var obj = new T();
                    var asset = (Serialize.AssetFileInterface)(object)obj;
                    asset.fileData = null;
                    asset.fileDirectory = fileDirectory;
                    asset.fileName = fileName;
                    asset.filePath = filePath;
                    asset.fileType = fileType;
                    asset.isLoaded = false;
                    asset.rawFileData = null;
                    asset.hierarchy = new List<string>();
                    for (int i = fileDirectorySplit.Count() - depth; i < fileDirectorySplit.Count(); i++ )
                    {
                        asset.hierarchy.Add(fileDirectorySplit[i]);
                    }
                    asset.map = fileDirectorySplit[fileDirectorySplit.Count() - 2 - depth];
                    obj = (T)(object)asset;

                    loadedAssets.Add(obj);
                }
                else
                {
                    // ToDo: find out why using EntityFramework.Engine isn't working
                    onError(this, new EntityFramework.Engine.Events.ErrorEventArgs
                    {
                        errorLevel = EntityFramework.Engine.Events.ErrorEventArgs.ErrorLevel.Warning,
                        message = string.Format("Asset File with path '{0}' already exists", filePath)
                    });
                }
            }
        }

        public bool LoadData(IAssetFileInterface assetFile)
        {
            if (!assetFile.IsLoaded)
            {
                var obj = (Serialize.AssetFileInterface)assetFile;

                obj.fileData = "TESTING! :D";
                obj.rawFileData = Encoding.Unicode.GetBytes(obj.FileData);
                obj.isLoaded = true;

                assetFile = (IAssetFileInterface)obj;

                return true;
            }
            else
            {
                // ToDo: find out why using EntityFramework.Engine isn't working
                onError(this, new EntityFramework.Engine.Events.ErrorEventArgs
                {
                    errorLevel = EntityFramework.Engine.Events.ErrorEventArgs.ErrorLevel.Warning,
                    message = string.Format("File '{0}' already loaded", assetFile.FilePath)
                });
                return false;
            }
        }

        public bool UnloadData(IAssetFileInterface assetFile)
        {
            if (assetFile.IsLoaded)
            {
                var obj = (Serialize.AssetFileInterface)assetFile;

                obj.fileData = null;
                obj.rawFileData = null;
                obj.isLoaded = false;

                assetFile = (IAssetFileInterface)obj;

                return true;
            }
            else
            {
                // ToDo: find out why using EntityFramework.Engine isn't working
                onError(this, new EntityFramework.Engine.Events.ErrorEventArgs
                {
                    errorLevel = EntityFramework.Engine.Events.ErrorEventArgs.ErrorLevel.Warning,
                    message = string.Format("File '{0}' is not loaded", assetFile.FilePath)
                });
                return false;
            }
        }
        #endregion Public Methods

        #region Constructor
        public FileManager(EntityFramework.Engine.Events.ErrorEventHandler onError)
            : this()
        {
            this.onError = onError;
        }

        public FileManager()
        {
            this.loadedAssets = new List<IAssetFileInterface>();
        }
        #endregion Constructor

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
