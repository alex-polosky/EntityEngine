using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Components
{
    public class GroupComponent : Component
    {
        public List<string> groups = new List<string>();
        public GroupComponent() : base() { }
        public GroupComponent(Entity e) : base(e) { }
        public GroupComponent(string name, Entity e)
            : base(e)
        {
            this.groups.Add(name);
        }
    }

    public class GroupSystem : ComponentSystem<GroupComponent>
    {
        public List<Entity> getTaggedEntities(string name)
        {
            List<Entity> toret = new List<Entity>();

            foreach (GroupComponent com in this._components)
                if (com.groups.Contains(name))
                    toret.Add(com.entity);

            return toret;
        }
        public GroupSystem() : base() { }
    }
}
