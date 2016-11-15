﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.ComponentInterfaces
{
    public abstract class ICollisionSystem : AComponentSystem<ICollision>
    {
        public abstract void RenderPhysics();
    }
}
