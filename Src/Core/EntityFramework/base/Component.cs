using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public abstract class Component
    {
        #region Private Variables
        private Guid id;
        private Entity _entity;
        #endregion Private Variables

        #region Public Variables
        public Entity Entity { get { return this._entity; } private set { this._entity = value; } }
        public Guid Guid { get { return this.id; } private set { this.id = value; } }
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
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
        #endregion Public Methods

        #region Constructor
        public Component()
        {
            // ToDo: add get new guid, define accessible NULL Guid
            id = Guid.Empty;
        }
        public Component(Entity e)
        {
            // ToDo: add get new guid, define accessible NULL Guid
            id = Guid.Empty;
            this.SetEntity(e);
        }
        public Component(Guid id)
        {
            this.id = id;
        }
        public Component(Entity e, Guid id)
        {
            this.id = id;
            this.SetEntity(e);
        }
        #endregion Constructor

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
