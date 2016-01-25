using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEditor.Editor.Controls
{
    public interface IControlBlock
    {
        void LoadData(Dictionary<string, string> data);
        Dictionary<string, string> GetData();
        void SetGroupBoxTag(string name);
    }
}
