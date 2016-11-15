using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MenuCreater.Parts
{
    public class Variable
    {
        public enum Language
        {
            None,
            CS,
            PY
        }

        #region Private Variables
        #endregion Private Variables

        #region Public Variables
        public Language language;
        public string name;
        public object value;
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
        #endregion Public Methods

        #region Constructor
        public Variable()
        {
            language = Language.None;
        }
        #endregion Constructor

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
