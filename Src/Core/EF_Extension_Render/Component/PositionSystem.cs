using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Render
{
    public class PositionSystem : EntityFramework.ComponentInterfaces.IPositionSystem
    {
        public override void Update(double timeDelta)
        {
            base.Update(timeDelta);
        }

        public override void Init(Type comType)
        {
            base.Init(comType);
        }
    }
}
