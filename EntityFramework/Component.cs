using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public class Component
    {
        private Entity _entity;
        public Entity entity { get { return this._entity; } }

        // please do not override these methods
        public void SetEntity(Entity e)
        {
            System.Diagnostics.Trace.Assert((_entity == null), "Component's Entity was set, but wasn't removed before adding a new one!");
            if (this._entity != null)
            {
                this.RemoveEntity();
            }
            this._entity = e;
            this._entity.AddComponent(this);
        }

        public void RemoveEntity()
        {
            System.Diagnostics.Trace.Assert((_entity != null), "Component's Entity wasn't set, but tried to remove!");
            if (_entity != null)
            {
                this._entity.RemoveComponent(this);
                this._entity = null;
            }
        }

        public Component()
        {
        }

        public Component(Entity e)
        {
            this.SetEntity(e);
        }
    }
}
