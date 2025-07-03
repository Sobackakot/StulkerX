using System;
using System.Collections.Generic;

namespace Behaviour.Handler
{
    public class BehaviourHandler : IBehaviourHandler
    {
        private readonly Dictionary<Type, IUnitBehaviour> behaviours = new();


        public T Get<T>() where T : class, IUnitBehaviour
        {
            return behaviours.TryGetValue(typeof(T), out var behaviour)
                ? behaviour as T
                : null;
        }
        public void Register<T>(T behaviour) where T : class, IUnitBehaviour
        {
            if (behaviour == null || behaviours.ContainsKey(typeof(T))) return;
            behaviours.Add(typeof(T), behaviour);
        }








        public bool TryGet<T>(out T behaviour) where T : class, IUnitBehaviour
        {
            if (behaviours.TryGetValue(typeof(T), out var value) && value is T typed)
            {
                behaviour = typed;
                return true;
            }

            behaviour = null;
            return false;
        }

        public bool Contains<T>() where T : class, IUnitBehaviour =>
           behaviours.ContainsKey(typeof(T));


     
        public void RegisterAll(object target)
        {
            var interfaces = target.GetType().GetInterfaces();

            foreach (var iface in interfaces)
            {
                if (!typeof(IUnitBehaviour).IsAssignableFrom(iface) || iface == typeof(IUnitBehaviour) || behaviours.ContainsKey(iface)) continue;  
                behaviours[iface] = target as IUnitBehaviour;
            }
        }
        public void Unregister<T>() where T : class, IUnitBehaviour
        { 
            if (!behaviours.Remove(typeof(T))) return;
        }

        public void Clear()
        {
            behaviours.Clear();
        }

    }
}

