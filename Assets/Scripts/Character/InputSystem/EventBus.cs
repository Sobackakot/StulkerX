
using System.Collections.Generic;
using System; 

public static class EventBus
{
    private static readonly Dictionary<Type, List<Delegate>> eventDictionary = new();
    private static readonly Dictionary<Type, Delegate> requestHandlers = new();

    public static void Subscribe<T>(Action<T> listener)
    {
        if (!eventDictionary.ContainsKey(typeof(T)))
            eventDictionary[typeof(T)] = new List<Delegate>();

        eventDictionary[typeof(T)].Add(listener);
    }

    public static void Unsubscribe<T>(Action<T> listener)
    {
        if (eventDictionary.ContainsKey(typeof(T)))
        {
            eventDictionary[typeof(T)].Remove(listener);
            if (eventDictionary[typeof(T)].Count == 0)
                eventDictionary.Remove(typeof(T));
        }
    }

    public static void Publish<T>(T eventData)
    {
        if (eventDictionary.ContainsKey(typeof(T)))
        {
            foreach (var listener in eventDictionary[typeof(T)])
            {
                ((Action<T>)listener).Invoke(eventData);
            }
        }
    }

    // ------------------- Request-Response Logic -------------------

    public static void RegisterRequest<TRequest, TResponse>(Func<TRequest, TResponse> handler)
    {
        requestHandlers[typeof(TRequest)] = handler;
    }

    public static void UnregisterRequest<TRequest>()
    {
        requestHandlers.Remove(typeof(TRequest));
    }

    public static TResponse Request<TRequest, TResponse>(TRequest request)
    {
        if (requestHandlers.ContainsKey(typeof(TRequest)))
        {
            return ((Func<TRequest, TResponse>)requestHandlers[typeof(TRequest)]).Invoke(request);
        }
        throw new Exception($"No handler registered for request type: {typeof(TRequest)}");
    }


    // ------------------- Request-Response Logic method overload-------------------
    public static void RegisterRequest<TRequest, TParam, TResponse>(Func<TParam, TResponse> handler)
    {
        requestHandlers[typeof(TRequest)] = handler;
    }
     
    public static TResponse Request<TRequest, TParam, TResponse>(TParam param)
    {
        if (requestHandlers.ContainsKey(typeof(TRequest)))
        {
            return ((Func<TParam, TResponse>)requestHandlers[typeof(TRequest)]).Invoke(param);
        }
        throw new Exception($"No handler registered for request type: {typeof(TRequest)}");
    }
}
