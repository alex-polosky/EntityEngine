using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public abstract class AComponentSystem : IComponentSystem
    {
        public virtual void Init()
        {

        }

        public virtual void Update(double timeDelta)
        {

        }
    }

    public abstract class AComponentSystem<TComponent> : AComponentSystem
        where TComponent : Component
    {
        #region Protected Variables
        protected List<Type> dependencies;
        protected List<TComponent> _components;
        #endregion Protected Variables

        #region Public Variables
        public List<Type> Dependencies { get { return dependencies.ToList(); } }
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
        public void AddComponent(TComponent com)
        {
            if (HasComponent(com)) { }
            else
            {
                bool hasAllD = true;
                foreach (Type depend in this.dependencies)
                {
                    bool hasD = false;
                    foreach (Component entC in com.Entity.GetAllComponents())
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
                    throw new Exception("Entity does not have required dependencies");
            }
        }

        public bool HasComponent(TComponent com)
        {
            return this._components.Contains(com);
        }

        // This function has the side effect of removing the component from it's entity if it has one
        public void RemoveComponent(TComponent com)
        {
            if (!HasComponent(com)) { }
            else
            {
                this._components.Remove(com);
            }
        }

        public override void Init()
        {
            this.dependencies = new List<Type>();
            this._components = new List<TComponent>();
        }

        public override void Update(double timeDelta)
        {

        }
        #endregion
    }
}
