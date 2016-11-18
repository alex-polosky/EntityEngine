using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public abstract class AComponentSystem<TComponent> : IComponentSystem<TComponent>
        where TComponent : Component
    {
        #region Protected Variables
        protected Type _genComType;
        protected List<Type> _dependencies;
        protected List<TComponent> _components;
        #endregion Protected Variables

        #region Public Variables
        public List<Type> Dependencies { get { return _dependencies.ToList(); } }
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
        public virtual void Init(Type comType)
        {
            if (comType.IsAbstract || comType.IsInterface)
                throw new TypeAccessException("'comType' must be able to be instantiated");
            bool isMatch = false;
            if (typeof(TComponent).IsInterface)
                isMatch = comType.GetInterfaces().Contains(typeof(TComponent));
            else
            {
                Type baseType = comType.BaseType;
                while (baseType != typeof(Component) && !isMatch)
                {
                    if (baseType == typeof(TComponent))
                        isMatch = true;
                    baseType = baseType.BaseType;
                }
            }
            if (!isMatch)
                throw new TypeLoadException(string.Format("'comType' is not descended from TComponent '{0}'", typeof(TComponent)));
            _genComType = comType;

            this._dependencies = new List<Type>();
            this._components = new List<TComponent>();
        }

        public virtual void Update(double timeDelta)
        {

        }

        public Type GetGenerateType()
        {
            return _genComType;
        }

        // ToDo: Force gen..com to add component?
        public Component GenerateComponent()
        {
            return GenerateTComponent();
        }

        public void AddComponent(Component com)
        {
            AddTComponent((TComponent)com);
        }

        public bool HasComponent(Component com)
        {
            return HasTComponent((TComponent)com);
        }

        public List<Component> GetComponents()
        {
            var ret = new List<Component>();
            foreach (var com in GetTComponents())
                ret.Add(com);
            return ret;
        }

        public void RemoveComponent(Component com)
        {
            RemoveTComponent((TComponent)com);
        }

        public TComponent GenerateTComponent()
        {
            return (TComponent)Activator.CreateInstance(_genComType);
        }

        public void AddTComponent(TComponent com)
        {
            if (HasTComponent(com)) { }
            else
            {
                bool hasAllD = true;
                foreach (Type depend in this._dependencies)
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

        public bool HasTComponent(TComponent com)
        {
            return this._components.Contains(com);
        }

        public List<TComponent> GetTComponents()
        {
            return this._components;
        }

        // This function has the side effect of removing the component from it's entity if it has one
        public void RemoveTComponent(TComponent com)
        {
            if (!HasTComponent(com)) { }
            else
            {
                this._components.Remove(com);
            }
        }
        #endregion
    }
}
