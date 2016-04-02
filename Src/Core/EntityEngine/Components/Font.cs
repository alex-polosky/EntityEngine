using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework;
using EntityFramework.Components;

namespace EntityEngine.Components
{
    public class FontComponent : Component
    {
        public string name;
        public FontComponent() : base() { }
        public FontComponent(string name)
            : base()
        { this.name = name; }
        public FontComponent(string name, Entity e)
            : base(e)
        { this.name = name; }
    }

    public class FontSystem : ComponentSystem<FontComponent>
    {
        public Entity getFontgedEntity(string name)
        {
            foreach (FontComponent com in this._components)
                if (com.name == name)
                    return com.entity;
            return null;
        }
        public FontSystem() : base() { }
    }
}
