using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.AssetFileInterface
{
    public interface IAssetFileInterface
    {
        string FileData { get; }
        string FileDirectory { get; }
        string FileName { get; }
        string FilePath { get; }
        string FileType { get; }
        bool IsLoaded { get; }
        byte[] RawFileData { get; }

        List<string> Hierarchy { get; }
        string Map { get; }
    }

    public static class IAssetFileInterfaceExtensions
    {
        public static bool Load<TAsset>(this TAsset obj, IFileManager fileManager)
            where TAsset : IAssetFileInterface
        {
            return fileManager.LoadData(obj);
        }

        public static bool Unload<TAsset>(this TAsset obj, IFileManager fileManager)
            where TAsset : IAssetFileInterface
        {
            return fileManager.UnloadData(obj);
        }
    }
}
