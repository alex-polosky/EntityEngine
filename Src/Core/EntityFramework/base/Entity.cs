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
            string comType = NormalizeTypeName(com.GetType());
            if (this._components.Keys.Contains(comType))
                throw new Exception(string.Format("Entity already contains a component with type {0}", com.GetType().Name));
            else
                this._components.Add(comType, com);
        }

        internal void RemoveComponent<TComponent>()
        {
            string comType = NormalizeTypeName(typeof(TComponent));
            if (this._components.Keys.Contains(comType))
                this._components.Remove(comType);
            else
                throw new Exception(string.Format("Entity does not contain a component with type {0}", typeof(TComponent).Name));
        }

        internal void RemoveComponent(Component com)
        {
            string comType = NormalizeTypeName(com.GetType());
            if (this._components.Keys.Contains(comType))
                this._components.Remove(comType);
            else
                throw new Exception(string.Format("Entity does not contain a component with type {0}", com.GetType().Name));
        }
        #endregion Internal Methods

        #region Private Methods
        /// <summary>
        /// This method ensures that we get the base type right above Component
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string NormalizeTypeName(Type type)
        {
            Type baseType = type.BaseType;
            Type lastType = null;
            Type targetType = null;
            while (baseType != typeof(Component))
            {
                lastType = baseType;
                baseType = baseType.BaseType;
                if (baseType == typeof(Component))
                {
                    targetType = lastType;
                }
            }
            return targetType.ToString();
        }
        #endregion

        #region Public Methods
        public Component GetComponent(string typeName)
        {
            if (this._components.Keys.Contains(typeName))
                return this._components[typeName];
            else
                return null;
        }

        public Component GetComponent(Type type)
        {
            return GetComponent(NormalizeTypeName(type));
        }

        public TComponent GetComponent<TComponent>()
            where TComponent : Component
        {
            return (TComponent)GetComponent(NormalizeTypeName(typeof(TComponent)));
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
