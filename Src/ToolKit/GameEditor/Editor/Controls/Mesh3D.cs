using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameEditor.Editor.Controls
{
    public partial class Mesh3D : UserControl, IControlBlock
    {
        public void LoadData(Dictionary<string, string> data)
        {
            this.fieldGuid1.LoadData(data["guid"]);
            this.fieldAsset1.LoadData(data["filePath"]);
        }

        public Dictionary<string, string> GetData()
        {
            Dictionary<string, string> toret = new Dictionary<string, string>();
            toret.Add("guid", this.fieldGuid1.GetData());
            toret.Add("filePath", this.fieldAsset1.GetData());
            return toret;
        }

        public void SetGroupBoxTag(string name)
        {
            this.groupMesh.Text = name + " : " + this.groupMesh.Text;
        }

        public Mesh3D()
        {
            InitializeComponent();
            this.fieldAsset1.SetFields(IField.FieldType.PathAsset, "Asset Path", true, true);
            this.fieldGuid1.SetFields(IField.FieldType.Guid, "Guid", true, true);
        }
    }
}
