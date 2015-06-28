using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace PyInterface
{
    public class clsPyInterface
    {
        public Dictionary<string, object> pythonVars;
        private ScriptEngine pyEngine;
        private ScriptScope pyScope;
        private ScriptSource pySource;
        private string pyCodePreRun;
        private string lastError;
        private const string pyDefaultError = "Error: SetSource(string code) must be called before running engine";

        public dynamic ConvertList(dynamic l)
        { return (IronPython.Runtime.List)l; }

        public Dictionary<string, object> Run()
        {
            foreach (string varKey in this.pythonVars.Keys.ToArray())
                this.pyScope.SetVariable(varKey, this.pythonVars[varKey]);

            try
            {
                this.lastError = "";
                this.pySource.Execute(this.pyScope);
                if (this.pySource.GetCode() == "")
                {
                    System.Diagnostics.Debug.Write("Warning: no source code");
                    this.lastError = "Warning: no source code\n";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("Error: ");
                System.Diagnostics.Debug.WriteLine(ex.Message);
                ExceptionOperations eo = this.pyEngine.GetService<ExceptionOperations>();
                this.lastError = eo.FormatException(ex);
            }

            foreach (string varKey in this.pythonVars.Keys.ToArray())
                this.pythonVars[varKey] = this.pyScope.GetVariable(varKey);

            return this.pythonVars;
        }

        public string GetLastError()
        {
            return this.lastError;
        }

        public void SetVariables(Dictionary<string, object> vars)
        {
            this.pythonVars = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> var in vars)
                this.pythonVars[var.Key] = var.Value;
            this.pythonVars["__original_python_cs_vars__"] = this.pythonVars;
        }

        public void SetSource(string code)
        {
            this.pySource = this.pyEngine.CreateScriptSourceFromString(this.pyCodePreRun + code);
        }

        //public void SetInput(System.IO.Stream stream, Encoding encoding)
        //{
        //    this.pyEngine.Runtime.IO.SetInput(stream, 
        //}

        public void SetOutput(System.IO.Stream stream, System.IO.TextWriter writer)
        {
            this.pyEngine.Runtime.IO.SetOutput(stream, writer);
        }

        public void SetErrorOutput(System.IO.Stream stream, System.IO.TextWriter writer)
        {
            this.pyEngine.Runtime.IO.SetErrorOutput(stream, writer);
        }

        public clsPyInterface(string codePreRun = "")
        {
            this.pythonVars = new Dictionary<string,object>();
            this.pyEngine = Python.CreateEngine(
#if DEBUG
                new Dictionary<string, object>() { {"Debug", true} }
#endif
            );
            this.pyScope = this.pyEngine.CreateScope();
            this.pySource = this.pyEngine.CreateScriptSourceFromString("raise BaseException('" + pyDefaultError + "')");
            this.pyCodePreRun = codePreRun + "\n";
            this.lastError = "";
        }
    }
}