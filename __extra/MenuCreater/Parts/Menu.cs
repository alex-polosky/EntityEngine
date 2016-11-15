using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MenuCreater.Parts
{
    public class Menu
    {
        #region Private Variables
        #endregion Private Variables

        #region Public Variables
        public string id;
        public List<Variable> variables;
        public List<Entry> entries;
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
        #endregion Public Methods

        #region Constructor
        public Menu()
        {
            variables = new List<Variable>();
            entries = new List<Entry>();
        }
        #endregion Constructor

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
