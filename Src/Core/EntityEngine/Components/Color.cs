using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDX;

using EntityFramework;

namespace EntityEngine.Components
{
    public class ColorComponent : EntityFramework.Component
    {
    }

    public class ColorSystem : ComponentSystem<ColorComponent>
    {
        public override void Update(double timeDelta = 0.0f)
        {
        }
    }
}
