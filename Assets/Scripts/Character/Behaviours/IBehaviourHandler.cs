namespace Behaviour.Handler
{
    public interface IBehaviourHandler
    {
        T Get<T>() where T : class, IUnitBehaviour;
        void Register<T>(T behaviour) where T : class, IUnitBehaviour;



        void RegisterAll(object target);
      
        bool TryGet<T>(out T behaviour) where T : class, IUnitBehaviour;
        bool Contains<T>() where T : class, IUnitBehaviour; 
    }
}


