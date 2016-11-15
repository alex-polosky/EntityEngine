using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.ComponentInterfaces
{
    public abstract class IRenderSystem : AComponentSystem<IRender>
    {
        public abstract void Draw(IntPtr handle);
    }
}
