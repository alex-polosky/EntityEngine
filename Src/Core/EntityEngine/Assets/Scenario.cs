using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using EntityFramework;
using EntityFramework.Components;

namespace EntityEngine.Assets
{
    [DataContract]
    public class Scenario
    {
        #region Public Nested Classes
        [DataContract]
        public class MetaEntity
        {
            [DataMember]
            public Guid guid { get; set; }
            [DataMember]
            public bool respawn { get; set; }
            [DataMember]
            public bool position { get; set; }
            [DataMember]
            public MetaRespawn respawnField { get; set; }
            [DataMember]
            public MetaPosition positionField { get; set; }
        }
        [DataContract]
        public class MetaWinCondition
        {
            [DataMember]
            public string path { get; set; }
        }
        [DataContract]
        public class MetaRespawn
        {
            [DataMember]
            public uint timer { get; set; }
            [DataMember]
            public bool kill { get; set; }
        }
        [DataContract]
        public class MetaPosition
        {
            [DataMember]
            public int x { get; set; }
            [DataMember]
            public int y { get; set; }
            [DataMember]
            public int z { get; set; }
        }
        #endregion Public Nested Classes

        #region Private Variables
        private Guid id;
        #endregion Private Variables

        #region Public Variables
        [DataMember]
        public Guid guid { get { return this.id; } private set { this.id = value; } }
        [DataMember]
        public string filePath { get; private set; }
        [DataMember]
        public MetaEntity[] entities { get; private set; }
        [DataMember]
        public MetaWinCondition wincondition { get; private set; }

        public ScenarioFile assetFile { get; private set; }
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
        [Obsolete]
        public void __test__generate__()
        {
            guid = Guid.NewGuid();
            filePath = "Global/Scenarios/testing/test.scen";
            entities = new MetaEntity[] 
            {
                new MetaEntity() { guid=Guid.NewGuid(), position = false, respawn=false, positionField=null, respawnField=null},
                new MetaEntity() { guid=Guid.NewGuid(), position = true, respawn=false, 
                    positionField=new MetaPosition() {x=1, y=2, z=3}, respawnField=null},
                new MetaEntity() { guid=Guid.NewGuid(), position = false, respawn=true, positionField=null, 
                    respawnField=new MetaRespawn() { kill=false, timer=60} },
                new MetaEntity() { guid=Guid.NewGuid(), position = true, respawn=true, 
                    positionField=new MetaPosition() {x=1, y=2, z=3}, 
                    respawnField=new MetaRespawn() { kill=true, timer=60*60} }
            };
            wincondition = new MetaWinCondition() { path = "Bloop" };


            var a = Newtonsoft.Json.JsonConvert.SerializeObject(this);

            Scenario t = Newtonsoft.Json.JsonConvert.DeserializeObject<Scenario>(a);
        }
        #endregion Public Methods

        #region Constructor
        public Scenario()
        {
        }
        #endregion Constructor

        #region Handlers
        [OnDeserialized]
        private void _onDeserialized(StreamingContext context)
        {
            this.assetFile = new ScenarioFile(FileManagerNS.FileManager.GetAssetFromPath(this.filePath));
        }
        #endregion Handlers
    }

    public class ScenarioFile : FileManagerNS.AssetFile
    {
        #region Private Variables
        #endregion Private Variables

        #region Public Variables
        #endregion Public Variables

        #region Protected Methods
        protected override void PreLoadFile()
        {
        }

        protected override void PostLoadFile()
        {
        }
        #endregion Protected Methods

        #region Public Methods
        #endregion Public Methods

        #region Constructor
        public ScenarioFile(Asset parent)
            : base(parent)
        {
        }
        #endregion Constructor

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
