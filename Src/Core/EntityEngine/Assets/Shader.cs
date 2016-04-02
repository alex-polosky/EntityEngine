using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using SharpDX.Windows;
using D3D10 = SharpDX.Direct3D10;

using EntityFramework;
using EntityFramework.Components;

namespace EntityEngine.Assets
{
    [DataContract]
    public class Shader
    {
        #region Private Variables
        private Guid id;
        #endregion Private Variables

        #region Public Variables
        [DataMember]
        public Guid guid { get { return this.id; } private set { this.id = value; } }
        [DataMember]
        public string filePath { get; private set; }
        [DataMember]
        public InputElement[] shaderVars { get; private set; }
        [DataMember]
        public string shaderLevel { get; private set; }

        public ShaderFile assetFile { get; private set; }
        public Effect effect { get; private set; }
        public List<List<InputLayout>> inputLayouts { get; private set; }
        public List<KeyValuePair<object, Type>> vars { get; private set; }
        #endregion Public Variables

        #region Private Methods
        private void _generateFromFile(D3D10.Device device)
        {
            this.effect = new Effect(device, this.assetFile.shaderBytecode);
            this.inputLayouts = new List<List<InputLayout>>();
            this.vars = new List<KeyValuePair<object, Type>>();

            // Using this, it will allow you to contain multiple passes and techniques in one file
            // Since we can't just get a total amount of techniques and passes, we have to 
            //     check if it is valid or not
            // As of right now, we're using just one set of input vars
            // In the future, I will add a function to allow us to read directly from multiple ones
            int techniqueCount = 0; int passCount = 0;
            while (true)
            {
                var technique = this.effect.GetTechniqueByIndex(techniqueCount);
                if (!technique.IsValid)
                    break;
                this.inputLayouts.Add(new List<InputLayout>());
                while (true)
                {
                    var pass = technique.GetPassByIndex(passCount);
                    if (!pass.IsValid)
                        break;
                    var passSignature = pass.Description.Signature;
                    this.inputLayouts[techniqueCount].Add(
                        new InputLayout(device, passSignature, shaderVars)
                    );
                    passCount += 1;
                }
                techniqueCount += 1;
            }
        }
        #endregion Private Methods

        #region Public Methods
        #endregion Public Methods

        #region Constructor
        public Shader()
        {
        }

        public Shader(D3D10.Device device, string path, InputElement[] shaderVars, string shaderLevel = "fx_4_0")
        {
            this.id = Guid.NewGuid();

            this.filePath = path;
            this.effect = null;
            this.shaderVars = shaderVars;
            this.shaderLevel = shaderLevel;

            this._generateFromFile(device);
        }
        #endregion Constructor

        #region Handlers
        [OnDeserialized]
        private void _onDeserialized(StreamingContext context)
        {
            this.assetFile = new ShaderFile(FileManagerNS.FileManager.GetAssetFromPath(this.filePath), shaderLevel);
            this._generateFromFile(GlobalEnvironment.MainWindowDevice);
        }
        #endregion Handlers
    }

    public class ShaderFile : FileManagerNS.AssetFile
    {        
        #region Private Variables
        private byte[] _compiled;
        private string _shaderLevel;
        #endregion Private Variables

        #region Public Variables
        public byte[] shaderBytecode { get { return _compiled; } }
        public string shaderLevel { get { return _shaderLevel; } }
        #endregion Public Variables

        #region Protected Methods
        protected override void PreLoadFile()
        {
        }

        protected override void PostLoadFile()
        {
            // ToDo: this won't work -_-
            // Create shaderCode from raw bytes
            var sbc = ShaderBytecode.Compile(_raw, shaderLevel);
            _compiled = sbc.Bytecode;

            // Turn bytes into string
            //string s = System.Text.Encoding.ASCII.GetString(_raw);
            //ShaderFlags sf = ShaderFlags.None;
            //EffectFlags ef = EffectFlags.None;
            //_sbc = ShaderBytecode.Compile(
            //    s,
            //    "VS_IN",
            //    shaderLevel,
            //    sf,
            //    ef
            //);
        }
        #endregion Protected Methods

        #region Public Methods
        #endregion Public Methods

        #region Constructor
        public ShaderFile(Asset parent, string shaderLevel, bool autoLoad=true)
            : base(parent, false)
        {
            this._shaderLevel = shaderLevel;
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

namespace EntityEngine.Components
{
    [DataContract]
    public class Shader
    {
        private Guid id;
        [DataMember]
        public Guid guid { get { return this.id; } private set { this.id = value; } }
        [DataMember]
        public string filePath { get; private set; }
        [DataMember]
        public InputElement[] shaderVars { get; private set; }
        [DataMember]
        public string shaderLevel { get; private set; }
        public Effect effect { get; private set; }
        public List<List<InputLayout>> inputLayouts { get; private set; }
        public List<KeyValuePair<object, Type>> vars { get; private set; }

        public Shader()
        {
        }

        public Shader(D3D10.Device device, string path, InputElement[] shaderVars, string shaderLevel = "fx_4_0")
        {
            this.id = Guid.NewGuid();

            this.filePath = path;
            this.effect = null;
            this.shaderVars = shaderVars;
            this.shaderLevel = shaderLevel;

            this._generateFromFile(device);
        }

        private void _generateFromFile(D3D10.Device device)
        {
            // TODO: get path from filename through Map Directory Service?
            ShaderBytecode shaderCode = null;
            for (var x = 0; x < 5; x++)
            {
                string path = Directory.GetCurrentDirectory();
                for (var y = 0; y < x; y++)
                {
                    path = Path.Combine(path, "..");
                }
                path = Path.Combine(path, this.filePath).PathNormalize();
                try
                {
                    shaderCode = ShaderBytecode.CompileFromFile(
                        path,
                        shaderLevel
                    );
                    if (shaderCode != null)
                        break;
                }
                catch { if (x == (5 - 1)) throw; }
            }
            this.effect = new Effect(device, shaderCode.Data);
            this.inputLayouts = new List<List<InputLayout>>();
            this.vars = new List<KeyValuePair<object, Type>>();

            // Using this, it will allow you to contain multiple passes and techniques in one file
            // Since we can't just get a total amount of techniques and passes, we have to 
            //     check if it is valid or not
            // As of right now, we're using just one set of input vars
            // In the future, I will add a function to allow us to read directly from multiple ones
            int techniqueCount = 0; int passCount = 0;
            while (true)
            {
                var technique = this.effect.GetTechniqueByIndex(techniqueCount);
                if (!technique.IsValid)
                    break;
                this.inputLayouts.Add(new List<InputLayout>());
                while (true)
                {
                    var pass = technique.GetPassByIndex(passCount);
                    if (!pass.IsValid)
                        break;
                    var passSignature = pass.Description.Signature;
                    this.inputLayouts[techniqueCount].Add(
                        new InputLayout(device, passSignature, shaderVars)
                    );
                    passCount += 1;
                }
                techniqueCount += 1;
            }
        }

        [OnDeserialized]
        private void _onDeserialized(StreamingContext context)
        {
            this._generateFromFile(GlobalEnvironment.MainWindowDevice);
        }
    }
}
