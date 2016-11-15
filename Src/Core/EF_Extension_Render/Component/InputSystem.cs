using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Render
{
    public class InputSystem : EntityFramework.ComponentInterfaces.IInputSystem
    {
        public override void Update(double timeDelta)
        {
            base.Update(timeDelta);
        }

        public override void Init()
        {
            base.Init();
        }
    }
}
