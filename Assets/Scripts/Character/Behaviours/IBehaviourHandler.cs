using UnitContext;

namespace Behaviour.Handler
{
    public interface IBehaviourHandler
    {
        void Register<T>(T behaviour) where T : class, IUnitBehaviour;
        void RegisterAll(object target);
        T Get<T>() where T : class, IUnitBehaviour;
        bool TryGet<T>(out T behaviour) where T : class, IUnitBehaviour;
        bool Contains<T>() where T : class, IUnitBehaviour; 
    }
}


