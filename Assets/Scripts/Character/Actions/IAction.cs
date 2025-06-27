using Character.Context;

namespace Character.Actions
{
    public interface IAction<in T> where T : IContext
    {
        void Subscribe(IContext context);
        void Unsubscribe(IContext context); 
    }
}

