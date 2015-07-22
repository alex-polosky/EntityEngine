using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public abstract class ComponentSystem
    {
        public abstract void Update(double timeDelta);
    }

    public class ComponentSystem<TComponent> : ComponentSystem
        where TComponent : Component
    {
        protected List<Type> dependencies;
        protected List<TComponent> _components;

        public void AddComponent(TComponent com)
        {
            //if (com == null)
            //    com = new TComponent();
            if (this._components.Contains(com)) { }
            else
            {
                bool hasAllD = true;
                foreach (Type depend in this.dependencies)
                {
                    bool hasD = false;
                    foreach (Component entC in com.entity.GetAllComponents())
                    {
                        if (depend == entC.GetType())
                            hasD = true;
                    }
                    if (!hasD)
                        hasAllD = false;
                }
                if (hasAllD)
                    this._components.Add(com);
                else
                    throw new Exception("Entity does not have required dependecies");
            }
        }

        // This function has the side effect of removing the component from it's entity if it has one
        public void RemoveComponent(TComponent com)
        {
            if (!this._components.Contains(com)) { }
            else
            {
                this._components.Remove(com);
            }
        }

        public override void Update(double timeDelta = 0)
        {
        }

        public void Init()
        {
        }

        public ComponentSystem()
        {
            this.dependencies = new List<Type>();
            this._components = new List<TComponent>();
        }
    }
}
