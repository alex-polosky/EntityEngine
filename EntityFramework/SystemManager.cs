using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public class SystemManager
    {
        private List<Entity> _entities;
        private Dictionary<string, ComponentSystem> _systems;

        public Entity AddNewEntity(Guid id)
        {
            this._entities.Add(new Entity(id));
            return this._entities.Last();
        }

        public Entity AddNewEntity()
        {
            this._entities.Add(new Entity());
            return this._entities.Last();
        }

        public void AddEntity(Entity e)
        {
            this._entities.Add(e);
        }

        public void RemoveEntity(Guid id)
        {
        }

        public void AddComponentToEntity<TComponent, TComponentSystem>(TComponent component, Entity e)
            where TComponent : Component, new()
            where TComponentSystem : ComponentSystem<TComponent>, new()
        {
            TComponentSystem sys = this.GetComponentSystem<TComponent, TComponentSystem>();

            component.SetEntity(e);

            sys.AddComponent(component);
        }

        public TComponent AddNewComponentToEntity<TComponent, TComponentSystem>(Entity e)
            where TComponent : Component, new()
            where TComponentSystem : ComponentSystem<TComponent>, new()
        {
            TComponentSystem sys = this.GetComponentSystem<TComponent, TComponentSystem>();

            TComponent component = new TComponent();
            component.SetEntity(e);

            sys.AddComponent(component);
            
            return component;
        }

        public void RemoveComponentFromEntity<TComponent, TComponentSystem>(Entity e)
            where TComponent : Component, new()
            where TComponentSystem : ComponentSystem<TComponent>, new()
        {
            TComponentSystem sys = this.GetComponentSystem<TComponent, TComponentSystem>();

            TComponent component = e.GetComponent<TComponent>();
            if (component != null)
            {
                component.RemoveEntity();
                sys.RemoveComponent(component);
            }
        }

        public List<Entity> GetAllEntities()
        {
            List<Entity> toret = new List<Entity>();
            foreach (Entity e in this._entities)
                toret.Add(e);
            return toret;
        }

        public List<Entity> GetEntitiesWithComponentType<TComponent>()
            where TComponent : Component, new()
        {
            List<Entity> toret = new List<Entity>();
            foreach (Entity e in this._entities)
                if (e.GetComponent<TComponent>() != null)
                    toret.Add(e);
            return toret;
        }

        public Entity GetEntityWithID(string id)
        {
            return this.GetComponentSystem<Components.TagComponent, Components.TagSystem>()
                .getTaggedEntity(id);
        }

        public bool HasComponentSystem<TComponent, TComponentSystem>()
            where TComponent : Component, new()
            where TComponentSystem : ComponentSystem<TComponent>, new()
        {
            if (this._systems.Keys.Contains(typeof(TComponentSystem).Name))
                return true;
            else
                return false;
        }

        public TComponentSystem GetComponentSystem<TComponent, TComponentSystem>()
            where TComponent : Component, new()
            where TComponentSystem : ComponentSystem<TComponent>, new()
        {
            this.AddComponentSystem<TComponent, TComponentSystem>();
            TComponentSystem sys =
                   (TComponentSystem)Convert.ChangeType(this._systems[typeof(TComponentSystem).Name], typeof(TComponentSystem));
            return sys;
        }

        public void AddComponentSystem<TComponent, TComponentSystem>(TComponentSystem sys = null)
            where TComponent : Component, new()
            where TComponentSystem : ComponentSystem<TComponent>, new()
        {
            if (!this.HasComponentSystem<TComponent, TComponentSystem>())
            {
                if (sys == null)
                {
                    sys = new TComponentSystem();
                    sys.Init();
                }

                this._systems.Add(typeof(TComponentSystem).Name, sys);
            }
        }

        public void RemoveComponentSystem<TComponent, TComponentSystem>()
            where TComponent : Component, new()
            where TComponentSystem : ComponentSystem<TComponent>, new()
        {
            if (this.HasComponentSystem<TComponent, TComponentSystem>())
                this._systems.Remove(typeof(TComponentSystem).Name);
        }

        public void Update(double timeDelta = 0.0f)
        {
            foreach (ComponentSystem sys in this._systems.Values)
                sys.Update(timeDelta);
        }

        public SystemManager()
        {
            this._entities = new List<Entity>();
            this._systems = new Dictionary<string, ComponentSystem>();
        }
    }
}
