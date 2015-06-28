using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace PyInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.Main_MinOut(args);
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        // This is using the class, and it uses minimal output
        static void Main_MinOut(string[] args)
        {
            clsPyInterface py = new clsPyInterface();

            py.SetVariables(new Dictionary<string, object>() {
                {"result", 0},
                {"s", "This is a test"}
            });

            py.SetSource(@"
result = 432
s = 'Welcome... to Python :D'
for i in __original_python_cs_vars__: print i
");

            py.Run();

            Console.ReadLine();
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        // This is using the class, and I use a lot of print statements for debugging
        static void Main_ClassWithOutput(string[] args)
        {
            clsPyInterface py = new clsPyInterface();

            py.SetVariables(new Dictionary<string, object>() {
                {"result", 0},
                {"s", "This is a test"}
            });

            py.SetSource(@"
print 'You have entered Python territory'
print 'Result before:', result
result = 432
print 'Result after:', result
print 's before:', s
s = 'Welcome... to Python :D'
print 's after:', s
for i in __original_python_cs_vars__: print i
print 'You are leaving Python territory'");

            Console.WriteLine("Pre-Execution");
            foreach (string varKey in py.pythonVars.Keys)
            {
                Console.Write(varKey); Console.Write(": "); Console.Write(py.pythonVars[varKey]);
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Executing Python...");
            Console.WriteLine();
            py.Run();
            Console.WriteLine();
            Console.WriteLine("Python Execution Complete");
            Console.WriteLine();

            Console.WriteLine("Post-Execution");
            foreach (string varKey in py.pythonVars.Keys)
            {
                Console.Write(varKey); Console.Write(": "); Console.Write(py.pythonVars[varKey]);
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        // This is without using a class, and it's messy
        static void Main_NoClass(string[] args)
        {
            Dictionary<string, object> pythonVariables = new Dictionary<string, object>();

            pythonVariables.Add("result", 0);
            pythonVariables.Add("s", "This is a test");
            pythonVariables.Add("__original_vars__", pythonVariables);

            string pythonCode = @"
print 'You have entered Python territory'
print 'Result before:', result
result = 432
print 'Result after:', result
print 's before:', s
s = 'Welcome... to Python :D'
print 's after:', s
for i in __original_vars__: print i
print 'You are leaving Python territory'
";

            ScriptEngine m_engine = Python.CreateEngine();
            ScriptScope m_scope = m_engine.CreateScope();

            Console.WriteLine("Pre-Execution");
            foreach (string varKey in pythonVariables.Keys)
            {
                Console.Write(varKey); Console.Write(": "); Console.Write(pythonVariables[varKey]);
                Console.WriteLine();
            }

            foreach (KeyValuePair<string, object> var in pythonVariables)
                m_scope.SetVariable(var.Key, var.Value);

            Console.WriteLine();
            Console.WriteLine("Executing Python...");
            Console.WriteLine();
            ScriptSource source = m_engine.CreateScriptSourceFromString(pythonCode);
            source.Execute(m_scope);
            Console.WriteLine();
            Console.WriteLine("Python Execution Complete");
            Console.WriteLine();

            foreach (string varKey in pythonVariables.Keys.ToArray())
                pythonVariables[varKey] = m_scope.GetVariable(varKey);

            Console.WriteLine("Post-Execution");
            foreach (string varKey in pythonVariables.Keys)
            {
                Console.Write(varKey); Console.Write(": "); Console.Write(pythonVariables[varKey]);
                Console.WriteLine();
            }

            Console.ReadLine(); // Wait for keypress
        }
    }
}
