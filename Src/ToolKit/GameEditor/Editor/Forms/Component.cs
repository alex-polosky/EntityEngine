using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;

using GameEditor.Editor.Controls;

namespace GameEditor.Editor.Forms
{
    public partial class Component : System.Windows.Forms.UserControl
    {
        //TODO: Make the _controls var more modular, ie write a file format for them

        public static string NullDictionaryKey = "NULL_DICTIONARY_KEY_FLAT_STRING";
        public static string JsonDictionaryKey = "JSON_DICTIONARY_KEY_JSON_STRING";

        private static Dictionary<string, Type> _controls = new Dictionary<string, Type>()
        {
            {"EntityEngine.Components.Mesh3D", typeof(Editor.Controls.Mesh3D)},
            {"EntityEngine.Components.RenderTypeFlag", typeof(Editor.Controls.RenderTypeFlag)},
            {"EntityEngine.Components.Shader", typeof(Editor.Controls.Shader)},
            //{"EntityFramework.Entity", typeof(Editor.Controls.Entity)},
            {"SharpDX.Matrix", typeof(Editor.Controls.Matrix)},
            //{"System.Collections.Generic.List`1[System.String]", typeof(Editor.Controls.String)},
            //{"System.Guid", typeof(Editor.Controls.Guid)},
            //{"System.String", typeof(Editor.Controls.String)}
        };

        private Point _drawPoint;
        private static int _padding = 3;

        public Point drawPoint { get { return _drawPoint; } }

        private void PopulateDropDict()
        {
            //TODO: Load list of components from customized dll loads
            var d = new Dictionary<string, string>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (var cls in assembly.GetTypes())
                    if (cls.IsSubclassOf(typeof(EntityFramework.Component)))
                        d.Add(
                            cls.Name.Replace("Component", "") + " <" + assembly.FullName.Split(',')[0] + ">", 
                            cls.Namespace + '.' + cls.Name
                        );
            this.fieldCom.LoadDropDictionary(d);
        }

        public void LoadComponentS(string s)
        {
            _drawPoint = new Point(0, 108);

            var json = (Newtonsoft.Json.Linq.JObject)
                    JsonConvert.DeserializeObject(s, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            string assemblyName = json["assemblyName"].ToString();
            string className = json["className"].ToString();
            json = (Newtonsoft.Json.Linq.JObject)
                JsonConvert.DeserializeObject(json["json"].ToString(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            this.fieldGuid.LoadData(json["guid"].ToString());
            this.fieldEntity.LoadData(json["entity"]["guid"].ToString());
            this.fieldCom.LoadData(className);

            Assembly assembly = Assembly.Load(assemblyName);
            Type clas = assembly.GetType(className);

            //foreach (var prop in clas.GetProperties())
            //{
            //    if (_controls.Keys.Contains(prop.PropertyType.ToString()))
            //    {
            //    }
            //}

            foreach (var field in clas.GetFields())
            {
                bool a = false;
                try
                {
                    var _x = _controls[field.FieldType.ToString()];
                    a = true;
                }
                catch
                {
                    a = false;
                }
                //if (_controls.Keys.Contains(field.FieldType.ToString()))
                if (a)
                {
                    object objControlBlack = Activator.CreateInstance(_controls[field.FieldType.ToString()]);
                    GameEditor.Editor.Controls.IControlBlock iControlBlock = (GameEditor.Editor.Controls.IControlBlock)objControlBlack;
                    UserControl controlBlock = (UserControl)objControlBlack;
                    try
                    {
                        // Send through dictionary of string to string
                        iControlBlock.LoadData(JsonConvert.DeserializeObject<Dictionary<string, string>>(json[field.Name].ToString()));
                    }
                    catch
                    {
                        try
                        {
                            // Collapse the var into a single string
                            iControlBlock.LoadData(new Dictionary<string, string>() { { NullDictionaryKey, JsonConvert.DeserializeObject<string>(json[field.Name].ToString()) } });
                        }
                        catch
                        {
                            // Lastly, just send the entire json
                            iControlBlock.LoadData(new Dictionary<string, string>() { { JsonDictionaryKey, json[field.Name].ToString() } });
                        }
                    }
                    iControlBlock.SetGroupBoxTag(field.Name);

                    controlBlock.Location = _drawPoint;
                    _drawPoint.Y += _padding + controlBlock.Height;
                    this.Controls.Add(controlBlock);
                    this.Size = new Size(this.Size.Width, this.Size.Height + _padding + controlBlock.Height);

                    this.PerformLayout();
                }
            }
        }

        public Component()
        {
            InitializeComponent();
            this.fieldGuid.SetFields(IField.FieldType.Guid, "Guid", true, true);
            this.fieldEntity.SetFields(IField.FieldType.Guid, "Entity", true, true);
            this.fieldCom.SetFields(IField.FieldType.Dropdown, "Component Type");
            PopulateDropDict();
        }
    }
}
