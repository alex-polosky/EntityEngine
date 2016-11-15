using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework;
using EntityFramework.AssetFileInterface;

namespace EntityFramework.Manager
{
    public class GuidManager : IGuidManager
    {
        #region Private Variables
        private List<Guid> activeGuids;
        #endregion Private Variables

        #region Public Variables
        public static readonly Guid NULL = Guid.Empty;
        public List<Guid> ActiveGuids { get { return activeGuids; } }
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
        public Guid NewGuid()
        {
            //var a = new Guid(new byte[] { 
            //    0x00, 0x00, 0x00, 0x00,             // Game uid (2**32)
            //    0x00, 0x00,                         // Map uid  (2**16)
            //    0x00, 0x00,                         // AFI type (0xyzzz) (2**12) [16 types]
            //    0x00, 0x00,                         // Hierarchy Depth (2**16)
            //    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // Actual uid (2**48)
            //});
            return NULL;
        }

        public Guid NewGuidFromObjPath(string objPath)
        {
            return NULL;
        }

        public Guid NewGuidFromAsset(IAssetFileInterface asset)
        {
            return NULL;
        }

        public void RegisterNewGuid(string objPath)
        {
            RegisterGuid(NewGuidFromObjPath(objPath), objPath);
        }

        public void RegisterNewGuid(IAssetFileInterface asset)
        {
            RegisterGuid(NewGuidFromAsset(asset), asset);
        }

        public void RegisterGuid(Guid id, string objPath)
        {

        }

        public void RegisterGuid(Guid id, IAssetFileInterface asset)
        {

        }
        
        public string GetObjPathFromGuid(Guid id)
        {
            return null;
        }

        public IAssetFileInterface GetAssetFromGuid(Guid id)
        {
            return null;
        }

        public Guid GetGuidFromObjPath(string objPath)
        {
            return NULL;
        }

        public Guid GetGuidFromAsset(IAssetFileInterface asset)
        {
            return NULL;
        }
        #endregion Public Methods

        #region Constructor
        public GuidManager()
        {
            activeGuids = new List<Guid>();
        }
        #endregion Constructor

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
