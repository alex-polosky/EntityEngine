using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Windows.Media;

using PyInterface;

namespace EntityEngine
{
    public partial class PyForm : Form
    {
        private clsPyInterface py;
        private MemoryStream outputMs;
        private EventRaisingStreamWriter outputWr;
        private MemoryStream errorMs;
        private EventRaisingStreamWriter errorWr;
        private Dictionary<string, object> variablesToPass;

        public TextBox StdOut { get { return this.stdOut; } }
        public TextBox StdErr { get { return this.stdErr; } }

        public PyForm(ref clsPyInterface py, Dictionary<string, object> variablesToPass)
        {
            InitializeComponent();

            this.py = py;

            this.outputMs = new MemoryStream();
            this.outputWr = new EventRaisingStreamWriter(outputMs);
            this.outputWr.StringWritten += new EventHandler<MyEvtArgs<string>>(this.output_StringWritten);
            py.SetOutput(this.outputMs, this.outputWr);

            this.errorMs = new MemoryStream();
            this.errorWr = new EventRaisingStreamWriter(errorMs);
            this.errorWr.StringWritten += new EventHandler<MyEvtArgs<string>>(this.error_StringWritten);
            py.SetErrorOutput(this.errorMs, this.errorWr);

            this.variablesToPass = variablesToPass;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            py.SetVariables(this.variablesToPass);

            py.SetSource(textBox1.Text);

            py.Run();

            this.stdErr.AppendText(py.GetLastError());
        }

        void output_StringWritten(object sender, MyEvtArgs<string> e)
        {
            //stdOut.Text += e.Value;
            stdOut.AppendText(e.Value);
        }

        void error_StringWritten(object sender, MyEvtArgs<string> e)
        {
            //stdErr.Text += e.Value;
            stdErr.AppendText(e.Value);
        }
    }

    public class MyEvtArgs<T> : EventArgs
    {
        public T Value
        {
            get;
            private set;
        }
        public MyEvtArgs(T value)
        {
            this.Value = value;
        }
    }


    public class EventRaisingStreamWriter : StreamWriter
    {
        #region Event
        public event EventHandler<MyEvtArgs<string>> StringWritten;
        #endregion

        #region CTOR
        public EventRaisingStreamWriter(Stream s)
            : base(s)
        { }
        #endregion

        #region Private Methods
        private void LaunchEvent(string txtWritten)
        {
            if (StringWritten != null)
            {
                StringWritten(this, new MyEvtArgs<string>(txtWritten));
            }
        }
        #endregion

        #region Overrides

        public override void Write(string value)
        {
            base.Write(value);
            LaunchEvent(value);
        }
        public override void Write(bool value)
        {
            base.Write(value);
            LaunchEvent(value.ToString());
        }
        // here override all writing methods...

        #endregion
    }
}
