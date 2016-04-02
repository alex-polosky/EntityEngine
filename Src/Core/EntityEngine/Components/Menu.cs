using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework;
using EntityFramework.Components;

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
        private List<Type> __dependencies = new List<Type>()
        {
            typeof(PositionComponent),
            typeof(RenderComponent),
            typeof(FontComponent),
            typeof(GroupComponent),
            typeof(StringComponent)
        };

        public override void Update(double timeDelta = 0.0f)
        {

        }

        public MenuSystem() : base() { }
    }
}
