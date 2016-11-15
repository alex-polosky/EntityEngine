using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicWinFormDefaultEngine
{
    public partial class MainParent : Form
    {
        private const int _wMod = 16;
        private const int _hMod = 39;

        private EngineControl engine;
        private Form engineContainer;

        public MainParent()
        {
            InitializeComponent();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (engine != null)
                engine.StopEngine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            engine = new EngineControl();
            engine.Dock = DockStyle.Fill;
            engine.InitEngine();
            engineContainer = new Form();
            engineContainer.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            engineContainer.Controls.Add(engine);
            engineContainer.FormClosed += ((s1, e1) =>
            {
                engine.StopEngine();
                comboBox1.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            });
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = 1;
            engineContainer.Width = 800;
            engineContainer.Height = 600;
            engineContainer.Show();

            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            engine.StartEngineThread();

            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            engine.StopEngine();

            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            engine.PauseEngine();

            button4.Enabled = false;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            engine.ResumeEngine();

            button4.Enabled = true;
            button5.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            engine.FrameSystemUpdate = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            engine.FrameRenderDraw = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            engine.FrameRenderPhysics = checkBox3.Checked;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            int w;
            int h;

            if (int.TryParse(comboBox1.Text.Split('x')[0], out w))
            {
                if (int.TryParse(comboBox1.Text.Split('x')[1].Split(' ')[0], out h))
                {
                    if (engineContainer != null && !engineContainer.IsDisposed)
                        engineContainer.Size = new Size(w + _wMod, h + _hMod);
                }
            }
        }
    }
}
