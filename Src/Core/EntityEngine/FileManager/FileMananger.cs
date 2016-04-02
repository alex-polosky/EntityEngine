using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EntityEngine.FileManagerNS
{
    public static class FileManager
    {
#region Private Variables
        //public static AssetFile 
#endregion Private Variables

#region Public Variables
#endregion Public Variables

#region Private Methods
#endregion Private Methods

#region Public Methods
        public static byte[] LoadFileRaw(string p)
        {
            // ToDo: Cache them files
            byte[] toret = null;

            try
            {
                toret = File.ReadAllBytes(p);
            }
            catch
            {
                throw;
            }

            return toret;
        }

        public static T LoadAssetJson<T>(Asset asset)
        {
            if (typeof(T) == typeof(EntityFramework.Entity))
                throw new Exception("Cannot load Entity using this method");
            T obj = default(T);
            try
            {
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(new AssetFile(FileManagerNS.FileManager.GetAssetFromPath(asset.AssetPath)).dataAscii);
            }
            catch
            {
                // ToDo: decent error handling
                throw;
            }
            return obj;
        }

        public static List<Asset> GetAssetsFromHierarchy(string searchQuery, AssetType assettTypeSearch)
        {
            List<Asset> toret = new List<Asset>();

            if (GlobalEnvironment.MapGlobal != null)
                foreach (Asset asset in GlobalEnvironment.MapGlobal.GetAssetsFromHierarchy(searchQuery, assettTypeSearch))
                    toret.Add(asset);

            if (GlobalEnvironment.MapMainMenu != null)
                foreach (Asset asset in GlobalEnvironment.MapMainMenu.GetAssetsFromHierarchy(searchQuery, assettTypeSearch))
                    toret.Add(asset);

            if (GlobalEnvironment.MapLoaded != null)
                foreach (Asset asset in GlobalEnvironment.MapLoaded.GetAssetsFromHierarchy(searchQuery, assettTypeSearch))
                    toret.Add(asset);

            return toret;
        }
        public static List<Asset> GetAssetsFromHierarchy(string searchQuery)
        {
            List<Asset> toret = new List<Asset>();

            if (GlobalEnvironment.MapGlobal != null)
                foreach (Asset asset in GlobalEnvironment.MapGlobal.GetAssetsFromHierarchy(searchQuery))
                    toret.Add(asset);

            if (GlobalEnvironment.MapMainMenu != null)
                foreach (Asset asset in GlobalEnvironment.MapMainMenu.GetAssetsFromHierarchy(searchQuery))
                    toret.Add(asset);

            if (GlobalEnvironment.MapLoaded != null)
                foreach (Asset asset in GlobalEnvironment.MapLoaded.GetAssetsFromHierarchy(searchQuery))
                    toret.Add(asset);

            return toret;
        }

        public static Asset GetAssetFromPath(string path, bool useBaseMap = true)
        {
            Asset toret = null;
            Asset temp = null;

            if (GlobalEnvironment.MapGlobal != null)
                if (useBaseMap)
                    temp = GlobalEnvironment.MapGlobal.GetAssetFromPath(Path.Combine(GlobalEnvironment.MapGlobal.MapPath, path));
                else
                    temp = GlobalEnvironment.MapGlobal.GetAssetFromPath(path);
            if (temp != null)
                toret = temp;
            if (GlobalEnvironment.MapMainMenu != null)
                if (useBaseMap)
                    temp = GlobalEnvironment.MapMainMenu.GetAssetFromPath(Path.Combine(GlobalEnvironment.MapMainMenu.MapPath, path));
                else
                    temp = GlobalEnvironment.MapMainMenu.GetAssetFromPath(path);
            if (temp != null)
                toret = temp;
            if (GlobalEnvironment.MapLoaded != null)
                if (useBaseMap)
                    temp = GlobalEnvironment.MapLoaded.GetAssetFromPath(Path.Combine(GlobalEnvironment.MapLoaded.MapPath, path));
                else
                    temp = GlobalEnvironment.MapLoaded.GetAssetFromPath(path);
            if (temp != null)
                toret = temp;

            return toret;
        }

        public static Asset GetAssetFromGuid(Guid guid)
        {
            Asset toret = null;
            Asset temp = null;

            if (GlobalEnvironment.MapGlobal != null)
                temp = GlobalEnvironment.MapGlobal.GetAssetFromGuid(guid);
            if (temp != null)
                toret = temp;
            if (GlobalEnvironment.MapMainMenu != null)
                temp = GlobalEnvironment.MapMainMenu.GetAssetFromGuid(guid);
            if (temp != null)
                toret = temp;
            if (GlobalEnvironment.MapLoaded != null)
                temp = GlobalEnvironment.MapLoaded.GetAssetFromGuid(guid);
            if (temp != null)
                toret = temp;

            return toret;
        }

        public static Guid GetGuidFromAsset(Asset asset)
        {
            Guid toret = GuidManager.NULL;

            if (GlobalEnvironment.MapGlobal != null)
                toret = GlobalEnvironment.MapGlobal.GetGuidFromAsset(asset);
            if (GlobalEnvironment.MapMainMenu != null)
                toret = GlobalEnvironment.MapMainMenu.GetGuidFromAsset(asset);
            if (GlobalEnvironment.MapLoaded != null)
                toret = GlobalEnvironment.MapLoaded.GetGuidFromAsset(asset);

            return toret;
        }
#endregion Public Methods

#region Handlers
#region Default Handlers
#endregion Default Handlers
#endregion Handlers
    }
}
