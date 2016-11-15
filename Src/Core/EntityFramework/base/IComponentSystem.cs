using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public interface IComponentSystem
    {
        void Update(double timeDelta);
        void Init();
    }

    public interface IComponentSystem<TComponent> : IComponentSystem
        where TComponent : Component
    {
        void AddComponent(TComponent com);
        bool HasComponent(TComponent com);
        void RemoveComponent(TComponent com);
    }
}
