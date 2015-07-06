using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework;

namespace EntityEngine.Components
{
    public class MenuComponent : Component
    {
        public MenuComponent() : base() { }
        public MenuComponent(Entity e)
            : base(e)
        { }
    }

    public class MenuSystem : ComponentSystem<MenuComponent>
    {
        public MenuSystem() : base() { }
    }
}
