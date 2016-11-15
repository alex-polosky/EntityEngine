using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework.AssetFileInterface;

namespace EntityFramework.Serialize
{
    public static partial class LoadFunctions
    {
        private static T LoadAssetFile<T>(IFileManager fileManager, string fileName)
            where T : IAssetFileInterface, new()
        {
            T o = new T();
            //o.LoadFromFile(fileManager, fileName);
            return o;
        }

        public static Audio LoadAudioAsset(IFileManager fileManager, string fileName)
        {
            return LoadAssetFile<Audio>(fileManager, fileName);
        }

        public static Component LoadComponentAsset(IFileManager fileManager, string fileName)
        {
            return LoadAssetFile<Component>(fileManager, fileName);
        }

        public static Entity LoadEntityAsset(IFileManager fileManager, string fileName)
        {
            return LoadAssetFile<Entity>(fileManager, fileName);
        }

        public static MapCollection LoadMapCollectionAsset(IFileManager fileManager, string fileName)
        {
            return LoadAssetFile<MapCollection>(fileManager, fileName);
        }

        public static Model LoadModelAsset(IFileManager fileManager, string fileName)
        {
            return LoadAssetFile<Model>(fileManager, fileName);
        }

        public static Scenario LoadScenarioAsset(IFileManager fileManager, string fileName)
        {
            return LoadAssetFile<Scenario>(fileManager, fileName);
        }

        public static Script LoadScriptAsset(IFileManager fileManager, string fileName)
        {
            return LoadAssetFile<Script>(fileManager, fileName);
        }

        public static Shader LoadShaderAsset(IFileManager fileManager, string fileName)
        {
            return LoadAssetFile<Shader>(fileManager, fileName);
        }

        public static String LoadStringAsset(IFileManager fileManager, string fileName)
        {
            return LoadAssetFile<String>(fileManager, fileName);
        }
    }
}
