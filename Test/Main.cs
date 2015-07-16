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

namespace Test
{
    public partial class Main : Game1_Form
    {
        protected override void SetUpEnts()
        {
            FileManager.LoadAllEntities(Path.Combine("Maps", "Test", "ObjDefs", "Entities"), this.sys);
        }

        public Main()
            : base()
        {
            InitializeComponent();
        }
    }
}
