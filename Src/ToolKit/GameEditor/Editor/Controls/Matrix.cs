using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameEditor.Editor.Controls
{
    public partial class Matrix : UserControl, IControlBlock
    {
        //TODO: Add a matrix helper form?

        private Dictionary<string, TextBox> _mColl;

        public void LoadData(Dictionary<string, string> data)
        {
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(data[Editor.Forms.Component.JsonDictionaryKey]);
            for (int i = 1; i <= 4; i++)
                for (int j = 1; j <= 4; j++)
                    _mColl["M" + i.ToString() + j.ToString()].Text = jsonObj["M" + i.ToString() + j.ToString()];
        }

        public Dictionary<string, string> GetData()
        {
            Dictionary<string, string> toret = new Dictionary<string, string>();
            for (int i = 1; i <= 4; i++)
                for (int j = 1; j <= 4; j++)
                    toret["M" + i.ToString() + j.ToString()] = _mColl["M" + i.ToString() + j.ToString()].Text;
            return new Dictionary<string, string>() { { Editor.Forms.Component.JsonDictionaryKey, Newtonsoft.Json.JsonConvert.SerializeObject(toret) } };
        }

        public void SetGroupBoxTag(string name)
        {
            this.groupMesh.Text = name + " : " + this.groupMesh.Text;
        }
        public Matrix()
        {
            InitializeComponent();
            _mColl = new Dictionary<string, TextBox>() {
                {"M11", textM11},
                {"M12", textM12},
                {"M13", textM13},
                {"M14", textM14},
                {"M21", textM21},
                {"M22", textM22},
                {"M23", textM23},
                {"M24", textM24},
                {"M31", textM31},
                {"M32", textM32},
                {"M33", textM33},
                {"M34", textM34},
                {"M41", textM41},
                {"M42", textM42},
                {"M43", textM43},
                {"M44", textM44},
            };
        }
    }
}
