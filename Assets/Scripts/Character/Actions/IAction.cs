using Character.Context;

namespace Character.Actions
{
    public interface IAction<in T> where T : IContextEvents
    {
        void Subscribe(IContextEvents context);
        void Unsubscribe(IContextEvents context); 
    }
}

