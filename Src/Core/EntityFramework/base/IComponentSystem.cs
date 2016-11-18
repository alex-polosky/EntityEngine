using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public interface IComponentSystem
    {
        void Init(Type comType);
        void Update(double timeDelta);

        Type GetGenerateType();
        Component GenerateComponent();
        void AddComponent(Component com);
        bool HasComponent(Component com);
        List<Component> GetComponents();
        void RemoveComponent(Component com);
    }

    public interface IComponentSystem<TComponent> : IComponentSystem
        where TComponent : Component
    {
        Type GetGenerateType();
        TComponent GenerateTComponent();
        void AddTComponent(TComponent com);
        bool HasTComponent(TComponent com);
        List<TComponent> GetTComponents();
        void RemoveTComponent(TComponent com);
    }
}
