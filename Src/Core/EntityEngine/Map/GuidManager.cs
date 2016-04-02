using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityEngine
{
    public static class GuidManager
    {
        private static DictionaryOTO<Guid, string> _guidCollection = new DictionaryOTO<Guid, string>();

        public static readonly Guid NULL = Guid.Empty;
        public static List<Guid> ActiveGuids { get { return _guidCollection.KeysByFirst; } }

        public static Guid NewGuid()
        {
            Guid guid = Guid.NewGuid();
            while (_guidCollection.KeysByFirst.Contains(guid))
                guid = Guid.NewGuid();
            return guid;
        }

        public static Guid NewGuidFromAsset(Asset asset)
        {
            //TODO: Set this up to generate from asset
            //TODO: Load asset definitions from file
            Guid guid = Guid.NewGuid();
            while (_guidCollection.KeysByFirst.Contains(guid))
                guid = Guid.NewGuid();
            return guid;
        }

        public static Guid RegisterNewGuid(string objPath)
        {
            Guid guid = Guid.NewGuid();
            while (_guidCollection.KeysByFirst.Contains(guid))
                guid = Guid.NewGuid();
            _guidCollection.Add(guid, objPath);
            return guid;
        }

        public static void RegisterGuid(Guid guid, string objPath)
        {
            //TODO: Set this up to use the true/false try methods
            if (_guidCollection.KeysByFirst.Contains(guid))
            {
                if (_guidCollection.GetByFirst(guid) != objPath)
                {
                    //TODO: throw error as there shouldn't be a duplicate with a different obj
                }
            }
            else
            {
                _guidCollection.Add(guid, objPath);
            }
        }

        public static string GetFromGuid(Guid guid)
        {
            //TODO: Set this up to use the true/false try methods
            if (_guidCollection.KeysByFirst.Contains(guid))
                return _guidCollection.GetByFirst(guid);
            else
            {
                //TODO: throw error?
            }
            return null;
        }

        public static Guid GetGuidOfObject(string objPath)
        {
            //TODO: Set this up to use the true/false try methods
            if (_guidCollection.KeysBySecond.Contains(objPath))
                return _guidCollection.GetBySecond(objPath);
            else
            {
                //TODO: throw error?
            }
            return Guid.Empty;
        }
    }
}
