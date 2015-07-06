using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Components
{
    public class StringComponent : Component
    {
        public StringComponent() : base() { }
        public StringComponent(Entity e)
            : base(e)
        { }
    }

    public class StringSystem : ComponentSystem<StringComponent>
    {
        public StringSystem() : base() { }
    }
}
