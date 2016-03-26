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
    public partial class RenderTypeFlag : UserControl, IControlBlock
    {
        EntityEngine.Components.RenderTypeFlag flags;

        public void LoadData(Dictionary<string, string> data)
        {
            bool success = Enum.TryParse<EntityEngine.Components.RenderTypeFlag>(data[Editor.Forms.Component.NullDictionaryKey], out flags);
            if (success)
            {
                if (flags.HasFlag(EntityEngine.Components.RenderTypeFlag.WireFrame))
                    boolWireframe.Checked = true;
            }
            else
            {
                //TODO: error out if it doesn't contain a valid flag
            }
        }

        public Dictionary<string, string> GetData()
        {
            Dictionary<string, string> toret = new Dictionary<string, string>();
            toret.Add(Editor.Forms.Component.NullDictionaryKey, ((int)flags).ToString());
            return toret;
        }

        public void SetGroupBoxTag(string name)
        {
            this.groupMesh.Text = name + " : " + this.groupMesh.Text;
        }

        public RenderTypeFlag()
        {
            InitializeComponent();
            this.flags = EntityEngine.Components.RenderTypeFlag.None;
        }

        private void boolWireframe_CheckedChanged(object sender, EventArgs e)
        {
            if (boolWireframe.Checked)
                flags |= EntityEngine.Components.RenderTypeFlag.WireFrame; //SetFlag
            else
                flags &= ~EntityEngine.Components.RenderTypeFlag.WireFrame; //Clearflag
        }
    }
}
