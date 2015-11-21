using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameEditor
{
    public partial class gameEditorMainForm : Form
    {
        public gameEditorMainForm()
        {
            InitializeComponent();

            Map map = new Map(@"P:\Code\Git\EntityEngine\Maps\Testing");
            var assetts0 = map.GetAssettsFromHierarchy("generic");
            var assetts1 = map.GetAssettsFromHierarchy("instruments.guitar");
            var assetts2 = map.GetAssettsFromHierarchy("instruments");
            var assetts3 = map.GetAssettsFromHierarchy("generic", AssettType.Model);
            var assetts4 = map.GetAssettsFromHierarchy("generic", AssettType.Shader);
            var assetts5 = map.GetAssettsFromHierarchy("generic", AssettType.Component);
        }
    }
}
