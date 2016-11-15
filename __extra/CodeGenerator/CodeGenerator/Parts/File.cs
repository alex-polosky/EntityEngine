using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Parts
{
    public class File
    {
        public enum Type
        {
            NONE,
            CS,
            PY,
            SETTING
        }

        // Global
        public Type type;
        public string name;
        public string ext;

        // CS | PY
        public API api;

        // SETTING
        public Dictionary<string, string> mappings;

        public File()
        {
            mappings = new Dictionary<string, string>();
        }
    }
}
