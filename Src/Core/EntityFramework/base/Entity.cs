using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public sealed class Entity
    {
        #region Private Variables
        private Guid id;
        private Dictionary<string, Component> _components;
        #endregion Private Variables

        #region Public Variables
        public Guid Guid { get { return id; } set { id = value; } }
        #endregion Public Variables

        #region Internal Methods
        // These can only be called by the com accessing it. 
        // Technically any com can access it, but that is not intended behavior
        // Please don't hack around that.
        // It's only useful in com.SetEntity
        // Really, don't try doing com.SetEntity(this), it'll make an endless loop
        // please do not override these methods
        internal void AddComponent(Component com)
        {
            if (this._components.Keys.Contains(com.GetType().Name))
                throw new Exception(string.Format("Entity already contains a component with type {0}", com.GetType().Name));
            else
                this._components.Add(com.GetType().Name, com);
        }

        internal void RemoveComponent<TComponent>()
        {
            if (this._components.Keys.Contains(typeof(TComponent).Name))
                this._components.Remove(typeof(TComponent).Name);
            else
                throw new Exception(string.Format("Entity does not contain a component with type %s", typeof(TComponent).Name));
        }

        internal void RemoveComponent(Component com)
        {
            if (this._components.Keys.Contains(com.GetType().Name))
                this._components.Remove(com.GetType().Name);
            else
                throw new Exception(string.Format("Entity does not contain a component with type %s", com.GetType().Name));
        }
        #endregion Internal Methods

        #region Public Methods
        public TComponent GetComponent<TComponent>()
            where TComponent : Component
        {
            if (this._components.Keys.Contains(typeof(TComponent).Name))
                return (TComponent)this._components[typeof(TComponent).Name];
            else
                return null;
        }

        public List<Component> GetAllComponents()
        {
            List<Component> toret = new List<Component>();
            foreach (Component com in this._components.Values)
                toret.Add(com);
            return toret;
        }
        #endregion Public Methods

        #region Constructor
        public Entity()
            : this(Guid.NewGuid())
        {
            // ToDo: implement new guid using guid manager
            //throw new NotImplementedException("GuidManager needs to be made static");
        }

        public Entity(Guid id)
        {
            this.id = id;
            this._components = new Dictionary<string, Component>();
        }
        #endregion Constructor

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
