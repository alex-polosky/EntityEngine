using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Components
{
    public class TagComponent : Component
    {
        public string name;
        public TagComponent() : base() { }
        public TagComponent(Entity e) : base(e) { }
        public TagComponent(string name)
            : base()
        { this.name = name; }
        public TagComponent(string name, Entity e)
            : base(e)
        { this.name = name; }
    }

    public class TagSystem : ComponentSystem<TagComponent>
    {
        public Entity getTaggedEntity(string name)
        {
            foreach (TagComponent com in this._components)
                if (com.name == name)
                    return com.entity;
            return null;
        }
        public TagSystem() : base() { }
    }
}
