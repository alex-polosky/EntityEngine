using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework.AssetFileInterface;

namespace EntityFramework
{
    public interface IGuidManager
    {
        List<Guid> ActiveGuids { get; }

        Guid NewGuid();
        Guid NewGuidFromObjPath(string objPath);
        Guid NewGuidFromAsset(IAssetFileInterface asset);
        void RegisterNewGuid(string objPath);
        void RegisterNewGuid(IAssetFileInterface asset);
        void RegisterGuid(Guid id, string objPath);
        void RegisterGuid(Guid id, IAssetFileInterface asset);
        string GetObjPathFromGuid(Guid id);
        IAssetFileInterface GetAssetFromGuid(Guid id);
        Guid GetGuidFromObjPath(string objPath);
        Guid GetGuidFromAsset(IAssetFileInterface asset);
    }
}
