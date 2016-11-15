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

        Entity AddNewEntity(Guid id);
        Entity AddNewEntity();
        void AddEntity(Entity e);
        void RemoveEntity(Guid id);
        void RemoveEntity(Entity e);

        void AddComponentToEntity<TComponent, TComponentSystem>(TComponent com, Guid id)
            where TComponent : EntityFramework.Component, new()
            where TComponentSystem : IComponentSystem<TComponent>;
        void AddComponentToEntity<TComponent, TComponentSystem>(TComponent com, Entity e)
            where TComponent : EntityFramework.Component, new()
            where TComponentSystem : IComponentSystem<TComponent>;
        TComponent AddNewComponentToEntity<TComponent, TComponentSystem>(Guid id)
            where TComponent : EntityFramework.Component, new()
            where TComponentSystem : IComponentSystem<TComponent>;
        TComponent AddNewComponentToEntity<TComponent, TComponentSystem>(Entity e)
            where TComponent : EntityFramework.Component, new()
            where TComponentSystem : IComponentSystem<TComponent>;
        void RemoveComponentFromEntity<TComponent, TComponentSystem>(Guid id)
            where TComponent : EntityFramework.Component, new()
            where TComponentSystem : IComponentSystem<TComponent>;
        void RemoveComponentFromEntity<TComponent, TComponentSystem>(Entity e)
            where TComponent : EntityFramework.Component, new()
            where TComponentSystem : IComponentSystem<TComponent>;

        List<Entity> GetAllEntities();
        List<Entity> GetEntitiesWithComponentType<TComponent>()
            where TComponent : EntityFramework.Component, new();
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
