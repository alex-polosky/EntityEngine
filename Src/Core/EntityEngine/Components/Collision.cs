using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDX;

using EntityFramework;

namespace EntityEngine.Components
{
    public class CollisionComponent : EntityFramework.Component
    {
    }

    public class CollisionSystem : ComponentSystem<CollisionComponent>
    {
        public override void Update(double timeDelta = 0.0f)
        {
        }
    }
}
