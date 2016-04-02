using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityEngine.FileManagerNS
{
    public class AssetFile
    {
#region Private Variables
        private Asset _parent;
#endregion Private Variables

#region Protected Variables
        protected byte[] _raw;
        protected bool _loaded;
#endregion Protected Variables

        #region Public Variables
        public Asset Parent { get { return _parent; } }
        public byte[] rawData { get { return _raw; } }
        public string dataAscii
        {
            get
            {
                return System.Text.Encoding.ASCII.GetString(rawData);
            }
        }
#endregion Public Variables

#region Protected Methods
        protected virtual void PreLoadFile()
        {
            //throw new NotImplementedException();
        }

        protected virtual void PostLoadFile()
        {
            //throw new NotImplementedException();
        }
#endregion Protected Methods

#region Public Methods
        public void LoadFile()
        {
            PreLoadFile();
            _raw = FileManager.LoadFileRaw(_parent.AssetPath);
            PostLoadFile();
        }
#endregion Public Methods

#region Constructor
        // ToDo: Make autoLoad functional when false
        // ToDo: turn autoLoad into a setting? [I think I have a setting for this already actually]
        public AssetFile(Asset parent, bool autoLoad=true)
        {
            _parent = parent;
            if (autoLoad)
                LoadFile();
        }
#endregion Constructor

#region Handlers
#region Default Handlers
#endregion Default Handlers
#endregion Handlers
    }
}
