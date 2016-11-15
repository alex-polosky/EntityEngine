using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework;

namespace EntityFramework.Manager
{
    public class SystemManager : ISystemManager
    {
        #region Private Variables
        private List<Entity> entities;
        private Dictionary<Type, IComponentSystem> systems;
        #endregion Private Variables

        #region Public Variables
        #endregion Public Variables

        #region Private Methods
        #endregion Private Methods

        #region Public Methods
        public void Update(double timeDelta)
        {
            foreach (IComponentSystem comSys in systems.Values)
            {
                comSys.Update(timeDelta);
            }
        }

        public void RenderDraw(double timeDelta, IntPtr handle)
        {
            var render = GetComponentSystem<EntityFramework.ComponentInterfaces.IRenderSystem>();
            if (render != null)
                render.Draw(handle);
        }

        public void RenderPhysics(double timeDelta)
        {
            var physics = GetComponentSystem<EntityFramework.ComponentInterfaces.ICollisionSystem>();
            if (physics != null)
                physics.RenderPhysics();
        }

        #region Entity Generation
        public Entity AddNewEntity(Guid id)
        {
            // ToDo: guid checking?
            Entity e = new Entity(id);
            AddEntity(e);
            return e;
        }

        public Entity AddNewEntity()
        {
            Entity e = new Entity();
            AddEntity(e);
            return e;
        }

        public void AddEntity(Entity e)
        {
            if (!entities.Contains(e))
                entities.Add(e);
        }

        public void RemoveEntity(Guid id)
        {
            var e = GetEntityFromId(id);
            if (e != null)
                RemoveEntity(e);
        }

        public void RemoveEntity(Entity e)
        {
            if (entities.Contains(e))
                entities.Remove(e);
        }
        #endregion

        #region Entity Components
        public void AddComponentToEntity<TComponent, TComponentSystem>(TComponent com, Guid id)
            where TComponent : Component, new()
            where TComponentSystem : IComponentSystem<TComponent>
        {
            AddComponentToEntity<TComponent, TComponentSystem>(com, GetEntityFromId(id));
        }
        public void AddComponentToEntity<TComponent, TComponentSystem>(TComponent com, Entity e)
            where TComponent : Component, new()
            where TComponentSystem : IComponentSystem<TComponent>
        {
            com.SetEntity(e);

            var comSys = GetComponentSystem<TComponentSystem>();
            if (!comSys.HasComponent(com))
            {
                comSys.AddComponent(com);
            }
        }

        public TComponent AddNewComponentToEntity<TComponent, TComponentSystem>(Guid id)
            where TComponent : Component, new()
            where TComponentSystem : IComponentSystem<TComponent>
        {
            return AddNewComponentToEntity<TComponent, TComponentSystem>(GetEntityFromId(id));
        }
        public TComponent AddNewComponentToEntity<TComponent, TComponentSystem>(Entity e)
            where TComponent : Component, new()
            where TComponentSystem : IComponentSystem<TComponent>
        {
            var com = new TComponent();
            com.SetEntity(e);

            var comSys = GetComponentSystem<TComponentSystem>();
            if (!comSys.HasComponent(com))
            {
                comSys.AddComponent(com);
            }

            return com;
        }

        public void RemoveComponentFromEntity<TComponent, TComponentSystem>(Guid id)
            where TComponent : Component, new()
            where TComponentSystem : IComponentSystem<TComponent>
        {
            RemoveComponentFromEntity<TComponent, TComponentSystem>(GetEntityFromId(id));
        }
        public void RemoveComponentFromEntity<TComponent, TComponentSystem>(Entity e)
            where TComponent : Component, new()
            where TComponentSystem : IComponentSystem<TComponent>
        {
            var com = e.GetComponent<TComponent>();
            if (com != null)
                com.RemoveEntity();
        }
        #endregion

        #region Get Entity
        public List<Entity> GetAllEntities()
        {
            return entities;
        }

        public List<Entity> GetEntitiesWithComponentType<TComponent>()
            where TComponent : Component, new()
        {
            return entities.Where(e => e.GetComponent<TComponent>() != null).ToList();
        }

        public Entity GetEntityFromId(Guid id)
        {
            return entities.SingleOrDefault(e => e.Guid == id);
        }

        [Obsolete]
        public Entity GetEntityWithTagId(string tagId)
        {
            return null;
        }
        #endregion

        #region Component Systems
        public bool HasComponentSystem<TComponentSystem>()
            where TComponentSystem : IComponentSystem
        {
            return systems.Keys.Contains(typeof(TComponentSystem));
        }

        public TComponentSystem GetComponentSystem<TComponentSystem>()
            where TComponentSystem : IComponentSystem
        {
            if (HasComponentSystem<TComponentSystem>())
                return (TComponentSystem)systems[typeof(TComponentSystem)];
            return default(TComponentSystem);
        }

        public void AddComponentSystem<TComponentSystem>(TComponentSystem sys)
            where TComponentSystem : IComponentSystem
        {
            if (!HasComponentSystem<TComponentSystem>())
                systems.Add(typeof(TComponentSystem), sys);
        }

        public void RemoveComponentSystem<TComponentSystem>()
            where TComponentSystem : IComponentSystem
        {
            if (HasComponentSystem<TComponentSystem>())
            {
                systems.Remove(typeof(TComponentSystem));
            }
        }
        #endregion

        #region Map Loading
        public void LoadMap(Map map)
        {

        }

        public void LoadDefaultMap()
        {

        }

        public void UnloadMap()
        {

        }

        public void LoadScenario(AssetFileInterface.IScenario scene)
        {

        }

        public void LoadDefaultScenario()
        {

        }

        public void UnloadScenario()
        {

        }
        #endregion
        #endregion Public Methods

        #region Constructor
        public SystemManager()
        {
            entities = new List<Entity>();
            systems = new Dictionary<Type, IComponentSystem>();
        }
        #endregion Constructor

        #region Handlers
        #region Default Handlers
        #endregion Default Handlers
        #endregion Handlers
    }
}
