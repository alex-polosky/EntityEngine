using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EntityFramework;
using EntityEngine;
using EntityFramework.Components;
using EntityEngine.Components;

namespace MainMenuTest
{
    public partial class Main : Game1_Form
    {
        protected override void SetUpEnts()
        {
            
        }

        public Main(bool launchToMainMenu = true, int renderMode = 6, bool usePyConsole = true, bool useGrid = false, int customWidth = -1, int customHeight = -1)
            : base(launchToMainMenu, renderMode, usePyConsole, useGrid, customWidth, customHeight)
        {
            InitializeComponent();
        }
    }
}
