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
    public partial class LineControl : Control
    {
        public LineControl()
        {
            InitializeComponent();
            this.Width = 500;
            this.Height = 2;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Color back = this.BackColor;
            Color dark = Color.FromArgb(((int)back.R) >> 1, ((int)back.G) >> 1, ((int)back.B) >> 1);
            using (var pen = new Pen(dark))
            {
                e.Graphics.DrawLine(pen, 0, 0, Width, 0);
            }
            e.Graphics.DrawLine(Pens.White, 0, 1, Width, 1);
        }
    }
}
