using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace GameEditor
{
    public partial class TestJsonEdit : Form
    {
        //TODO: Make the _controls var more modular, ie write a file format for them

        public static string NullDictionaryKey = "NULL_DICTIONARY_KEY_FLAT_STRING";
        public static string JsonDictionaryKey = "JSON_DICTIONARY_KEY_JSON_STRING";

        #region strings
        private List<string> s = new List<string>()
            {
@"{
		'json': {
			'groups': ['save', ],
			'entity': {
				'guid': '3a97e680-b883-42da-ba71-e6e729118bd2',
			},
			'guid': '93bbca35-ff95-4527-9e19-ed8c2e5c4bed',
		},
		'assemblyName': 'EntityFramework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
		'className': 'EntityFramework.Components.GroupComponent',
	}
".Replace("'", "\""),
@"{
		'json': {
			'name': 'X-Axis',
			'entity': {
				'guid': '3a97e680-b883-42da-ba71-e6e729118bd2',
			},
			'guid': '0577b8fa-9353-4fe5-81e0-143d805f7e65',
		},
		'assemblyName': 'EntityFramework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
		'className': 'EntityFramework.Components.TagComponent',
	}
".Replace("'", "\""),
@"{
		'json': {
			'rotationXMatrix': {
				'M11': '1',
				'M12': '0',
				'M13': '0',
				'M14': '0',
				'M21': '0',
				'M22': '1',
				'M23': '0',
				'M24': '0',
				'M31': '0',
				'M32': '0',
				'M33': '1',
				'M34': '0',
				'M41': '0',
				'M42': '0',
				'M43': '0',
				'M44': '1',
			},
			'rotationYMatrix': {
				'M11': '1',
				'M12': '0',
				'M13': '0',
				'M14': '0',
				'M21': '0',
				'M22': '1',
				'M23': '0',
				'M24': '0',
				'M31': '0',
				'M32': '0',
				'M33': '1',
				'M34': '0',
				'M41': '0',
				'M42': '0',
				'M43': '0',
				'M44': '1',
			},
			'rotationZMatrix': {
				'M11': '1',
				'M12': '0',
				'M13': '0',
				'M14': '0',
				'M21': '0',
				'M22': '1',
				'M23': '0',
				'M24': '0',
				'M31': '0',
				'M32': '0',
				'M33': '1',
				'M34': '0',
				'M41': '0',
				'M42': '0',
				'M43': '0',
				'M44': '1',
			},
			'scalingMatrix': {
				'M11': '10000',
				'M12': '0',
				'M13': '0',
				'M14': '0',
				'M21': '0',
				'M22': '0.01',
				'M23': '0',
				'M24': '0',
				'M31': '0',
				'M32': '0',
				'M33': '0.01',
				'M34': '0',
				'M41': '0',
				'M42': '0',
				'M43': '0',
				'M44': '1',
			},
			'translationLocalMatrix': {
				'M11': '1',
				'M12': '0',
				'M13': '0',
				'M14': '0',
				'M21': '0',
				'M22': '1',
				'M23': '0',
				'M24': '0',
				'M31': '0',
				'M32': '0',
				'M33': '1',
				'M34': '0',
				'M41': '0',
				'M42': '0',
				'M43': '0',
				'M44': '1',
			},
			'translationWorldMatrix': {
				'M11': '1',
				'M12': '0',
				'M13': '0',
				'M14': '0',
				'M21': '0',
				'M22': '1',
				'M23': '0',
				'M24': '0',
				'M31': '0',
				'M32': '0',
				'M33': '1',
				'M34': '0',
				'M41': '0',
				'M42': '0',
				'M43': '0',
				'M44': '1',
			},
			'entity': {
				'guid': '3a97e680-b883-42da-ba71-e6e729118bd2',
			},
			'guid': 'e2bf4727-3ed2-496b-bc4f-e7252bc69877',
		},
		'assemblyName': 'EntityEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
		'className': 'EntityEngine.Components.PositionComponent',
	}
".Replace("'", "\""),
@"{
		'json': {
			'renderFlags': '0',
			'mesh': {
				'guid': '583d3b13-82d6-460d-bd92-bc2e61f1b868',
				'filePath': 'Maps/Test/Models/cube.mesh',
			},
			'shader': {
				'guid': '6b7878aa-9329-481a-8277-8ec3fda6fbaf',
				'filePath': 'Shaders/color.fx',
				'shaderVars': [{
					'SemanticName': 'POSITION',
					'SemanticIndex': '0',
					'Format': '6',
					'Slot': '0',
					'AlignedByteOffset': '0',
					'Classification': '0',
					'InstanceDataStepRate': '0',
				}, {
					'SemanticName': 'COLOR',
					'SemanticIndex': '0',
					'Format': '2',
					'Slot': '0',
					'AlignedByteOffset': '12',
					'Classification': '0',
					'InstanceDataStepRate': '0',
				}, ],
				'shaderLevel': 'fx_4_0',
			},
			'entity': {
				'guid': '3a97e680-b883-42da-ba71-e6e729118bd2',
			},
			'guid': '3cebfb2d-a24c-401a-9612-9131bc0f0457',
		},
		'assemblyName': 'EntityEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
		'className': 'EntityEngine.Components.RenderComponent',
	}
".Replace("'", "\"")
            };
        #endregion

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            
            //using (StreamWriter w = File.AppendText("coms.txt"))
            //{
            //    foreach (var str in s)
            //    {
            //        var json = (Newtonsoft.Json.Linq.JObject)
            //            JsonConvert.DeserializeObject(str, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            //        string assemblyName = json["assemblyName"].ToString();
            //        string className = json["className"].ToString();
            //        json = json = (Newtonsoft.Json.Linq.JObject)
            //            JsonConvert.DeserializeObject(json["json"].ToString(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            //        Assembly assembly = Assembly.Load(assemblyName);
            //        Type clas = assembly.GetType(className);

            //        foreach (var prop in clas.GetProperties())
            //            w.Write(prop.PropertyType.ToString() + "\n");
            //        foreach (var field in clas.GetFields())
            //            w.Write(field.FieldType.ToString() + "\n");
            //    }
            //}
        }

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

        public TestJsonEdit()
        {
            InitializeComponent();

            //this.panel1.LoadComponentS(s[s.Count - 1]);

            this.Controls.Remove(this.panel1);

            Point location = new Point(3, 3);
            int padding = 3;
            foreach (string _s in s)
            {
                Editor.Forms.Component com = new Editor.Forms.Component();
                com.LoadComponentS(_s);
                com.Location = location;
                location.Y += com.Height + padding;
                this.Controls.Add(com);
                Editor.Controls.LineControl line = new Editor.Controls.LineControl();
                line.Location = location;
                line.Height = 3;
                location.Y += line.Height;
                this.Controls.Add(line);
            }

            //this.panel1.VerticalScroll.Visible = true;
            //this.panel1.VerticalScroll.Enabled = true;

            //Point location = new Point(0, 210+600);
            //int padding = 3;

            //foreach (string _s in s)
            //{
            //    var json = (Newtonsoft.Json.Linq.JObject)
            //        JsonConvert.DeserializeObject(_s, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            //    string assemblyName = json["assemblyName"].ToString();
            //    string className = json["className"].ToString();
            //    json = (Newtonsoft.Json.Linq.JObject)
            //        JsonConvert.DeserializeObject(json["json"].ToString(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            //    Assembly assembly = Assembly.Load(assemblyName);
            //    Type clas = assembly.GetType(className);

            //    foreach (var prop in clas.GetProperties())
            //    {
            //        if (_controls.Keys.Contains(prop.PropertyType.ToString()))
            //        {
            //        }
            //    }

            //    foreach (var field in clas.GetFields())
            //    {
            //        if (_controls.Keys.Contains(field.FieldType.ToString()))
            //        {
            //            object objControlBlack = Activator.CreateInstance(_controls[field.FieldType.ToString()]);
            //            GameEditor.Editor.Controls.IControlBlock iControlBlock = (GameEditor.Editor.Controls.IControlBlock)objControlBlack;
            //            UserControl controlBlock = (UserControl)objControlBlack;
            //            try
            //            {
            //                // Send through dictionary of string to string
            //                iControlBlock.LoadData(JsonConvert.DeserializeObject<Dictionary<string, string>>(json[field.Name].ToString()));
            //            }
            //            catch
            //            {
            //                try
            //                {
            //                    // Collapse the var into a single string
            //                    iControlBlock.LoadData(new Dictionary<string, string>() { { NullDictionaryKey, JsonConvert.DeserializeObject<string>(json[field.Name].ToString()) } });
            //                }
            //                catch
            //                {
            //                    // Lastly, just send the entire json
            //                    iControlBlock.LoadData(new Dictionary<string, string>() { { JsonDictionaryKey, json[field.Name].ToString() } });
            //                }
            //            }
            //            iControlBlock.SetGroupBoxTag(field.Name);

            //            controlBlock.Location = location;
            //            location.Y += padding + controlBlock.Height;
            //            this.panel1.Controls.Add(controlBlock);

            //            this.PerformLayout();
            //        }
            //    }

            //    this.panel1.Controls.Add(new Editor.Controls.LineControl() { Location = location, Height = 3 });
            //    location.Y += padding + 3;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = new List<Dictionary<string, string>>();
            foreach (var block in this.panel1.Controls)
            {
                data.Add(((GameEditor.Editor.Controls.IControlBlock)block).GetData());
            }
        }
    }
}
