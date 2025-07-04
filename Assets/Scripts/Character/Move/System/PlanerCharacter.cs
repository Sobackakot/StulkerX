using Character.Actions;
using Character.Context;
using System.Collections.Generic;

public class PlanerCharacter <T> where T : IContextEvents
{ 
    public List<IAction<T>> actions = new();

    public void AddAction(IAction<T> action) => actions.Add(action);
    public void OnEnable(T context)
    {
        foreach(var action in actions)
        {
            action?.Subscribe(context);
        }
    }
    public void OnDisable(T context)
    {
        foreach (var action in actions)
        {
            action?.Unsubscribe(context);
        }
    } 
}
