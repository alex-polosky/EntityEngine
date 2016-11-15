using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework.AssetFileInterface;

namespace EntityFramework
{
    public interface IFileManager
    {
        IAssetFileInterface GetAssetFileFromPath(string fileName);

        void PrepareAllAssetFiles(string folderPath);

        void PrepareAssetFiles<T>(string folderPath, int depth) where T : IAssetFileInterface, new();

        bool LoadData(IAssetFileInterface assetFile);
        bool UnloadData(IAssetFileInterface assetFile);
    }
}
