using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EntityEngine;
using EntityFramework;
using EntityFramework.Components;

namespace Pong
{
    public partial class Main : Game1_Form
    {
        protected override void SetUpEnts()
        {
            Entity e;

        }

        public Main(bool launchToMainMenu = true, int renderMode = 6, bool usePyConsole = true, bool useGrid = false, int customWidth = -1, int customHeight = -1)
            : base(launchToMainMenu, renderMode, usePyConsole, useGrid, customWidth, customHeight)
        {
            InitializeComponent();
        }
    }
}
