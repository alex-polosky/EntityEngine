using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework.Components
{
    public class ChildrenComponent : Component
    {
        private List<Entity> _children;

        public Entity parent;
        public List<Entity> children{ get { return this._children; } set {
            foreach (Entity c in children)
                this._children.Add(c);
        }}

        public ChildrenComponent() : base() { this._children = new List<Entity>(); }
        public ChildrenComponent(Entity e, Entity parent = null, List<Entity> children = null)
            : base(e)
        {
            this._children = new List<Entity>();
            if (parent != null)
                this.parent = parent;
            if (children != null)
                foreach (Entity c in children)
                    this.children.Add(c);
        }

        ~ChildrenComponent()
        {
            // It's completely possible that the parent/children relationships aren't destroyed..
            try
            {
                if (this.parent != null)
                    this.parent.GetComponent<ChildrenComponent>().children.Remove(this.parent);
                foreach (Entity child in this.children)
                    child.GetComponent<ChildrenComponent>().parent = null;
            }
            catch { }
        }
    }

    public class ChildrenSystem : ComponentSystem<ChildrenComponent>
    {
        public ChildrenSystem() : base() { }

        public void SetParent(Entity entityParent, Entity entityFutureChild)
        {
            if (entityFutureChild.GetComponent<ChildrenComponent>() == null)
                throw new Exception("Entity '" + entityFutureChild.guid.ToString() + "' does not have a children component!");
            if (entityFutureChild.GetComponent<ChildrenComponent>().parent != null)
                throw new Exception("Entity '" + entityFutureChild.guid.ToString() + "' already has a parent set!");

            entityFutureChild.GetComponent<ChildrenComponent>().parent = entityParent;
            entityParent.GetComponent<ChildrenComponent>().children.Add(entityFutureChild);
        }

        public void RemoveEntity(Entity e)
        {
            if (e.GetComponent<ChildrenComponent>().parent != null)
                e.GetComponent<ChildrenComponent>().parent.GetComponent<ChildrenComponent>().children.Remove(e);
            foreach (Entity child in e.GetComponent<ChildrenComponent>().children)
                child.GetComponent<ChildrenComponent>().parent = null;
        }
    }
}
