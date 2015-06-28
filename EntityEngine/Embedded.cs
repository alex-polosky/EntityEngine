using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntityEngine
{
    public partial class Embedded : Form
    {
        public System.Windows.Forms.Panel Panel1 { get { return this.panel1; } }

        public Embedded(Form form)
        {
            InitializeComponent();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            this.Panel1.Controls.Add(form);
        }
    }
}
