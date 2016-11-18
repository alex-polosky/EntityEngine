using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public interface ISystemManager
    {
        void Update(double timeDelta);
        void RenderDraw(double timeDelta, IntPtr handle);
        void RenderPhysics(double timeDelta);

        void AddNewEntity(Guid id);
        void RemoveEntity(Guid id);
        //void AddEntity(Entity e);
        //void RemoveEntity(Entity e);

        void AddComponentToEntity<TSystem>(Guid id, Component com=null)
            where TSystem : IComponentSystem;
        void RemoveComponentFromEntity<TSystem>(Guid id)
            where TSystem : IComponentSystem;

        //void AddComponentToEntity<TComponentSystem>(TComponent com, Guid id)
        //    where TComponentSystem : IComponentSystem;
        //void AddComponentToEntity<TComponentSystem>(TComponent com, Entity e)
        //    where TComponentSystem : IComponentSystem;
        //TComponent AddNewComponentToEntity<TComponentSystem>(Guid id)
        //    where TComponentSystem : IComponentSystem;
        //TComponent AddNewComponentToEntity<TComponentSystem>(Entity e)
        //    where TComponentSystem : IComponentSystem;
        //void RemoveComponentFromEntity<TComponentSystem>(Guid id)
        //    where TComponentSystem : IComponentSystem;
        //void RemoveComponentFromEntity<TComponentSystem>(Entity e)
        //    where TComponentSystem : IComponentSystem;

        List<Entity> GetAllEntities();
        List<Entity> GetEntitiesWithComponentType<TComponent>()
            where TComponent : Component, new();
        Entity GetEntityFromId(Guid id);
        [Obsolete]
        Entity GetEntityWithTagId(string tagId);

        bool HasComponentSystem<TComponentSystem>()
            where TComponentSystem : IComponentSystem;
        TComponentSystem GetComponentSystem<TComponentSystem>()
            where TComponentSystem : IComponentSystem;
        void AddComponentSystem<TComponentSystem>(TComponentSystem sys = default(TComponentSystem))
            where TComponentSystem : IComponentSystem;
        void RemoveComponentSystem<TComponentSystem>()
            where TComponentSystem : IComponentSystem;

        void LoadMap(Map map);
        void LoadDefaultMap();
        void UnloadMap();
        void LoadScenario(AssetFileInterface.IScenario scene);
        void LoadDefaultScenario();
        void UnloadScenario();
    }
}
