using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Components
{
    public class GroupSystem : ComponentInterfaces.IGroupSystem
    {
        #region Private Variables
        #endregion Private Variables

        #region Public Variables
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
        public override void Update(double timeDelta)
        {
            base.Update(timeDelta);
        }

        public override void Init(Type comType)
        {
            base.Init(comType);
        }
        #endregion Public Methods

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
