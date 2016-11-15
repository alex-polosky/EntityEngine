using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Render
{
    public class CollisionSystem : EntityFramework.ComponentInterfaces.ICollisionSystem
    {
        public override void Update(double timeDelta)
        {
            base.Update(timeDelta);
        }

        public override void Init()
        {
            base.Init();
        }

        public override void RenderPhysics()
        {
            return;
            throw new NotImplementedException();
        }
    }
}
