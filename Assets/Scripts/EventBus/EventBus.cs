
using System;
using System.Collections.Generic;

public class EventBus
{
    private Dictionary<Type, List<WeakReference<IBaseEventReceiver>>> _receivers;
    private Dictionary<Type, Dictionary<int, WeakReference<IBaseEventReceiver>>> _receiverHashToReference;
    
    public EventBus()
    {
        _receivers = new Dictionary<Type, List<WeakReference<IBaseEventReceiver>>>();
        _receiverHashToReference = new Dictionary<Type, Dictionary<int, WeakReference<IBaseEventReceiver>>>();  
    }

    public void Register<T>(IEventReceiver<T> receiver) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        if (!_receivers.ContainsKey(eventType))
            _receivers[eventType] = new List<WeakReference<IBaseEventReceiver>>();

        if (!_receiverHashToReference.ContainsKey(eventType))
            _receiverHashToReference[eventType] = new Dictionary<int, WeakReference<IBaseEventReceiver>>();

        int receiverHash = receiver.GetHashCode();
        if (_receiverHashToReference[eventType].ContainsKey(receiverHash))
            return; // уже зарегистрирован для данного типа события

        WeakReference<IBaseEventReceiver> reference = new WeakReference<IBaseEventReceiver>(receiver);
        _receivers[eventType].Add(reference);
        _receiverHashToReference[eventType][receiverHash] = reference;
    }

    public void Unregister<T>(IEventReceiver<T> receiver) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        int receiverHash = receiver.GetHashCode();
        if (!_receivers.ContainsKey(eventType) || !_receiverHashToReference.ContainsKey(eventType))
            return;

        if (!_receiverHashToReference[eventType].ContainsKey(receiverHash))
            return;

        WeakReference<IBaseEventReceiver> reference = _receiverHashToReference[eventType][receiverHash];
        _receivers[eventType].Remove(reference);
        _receiverHashToReference[eventType].Remove(receiverHash);

        if (_receiverHashToReference[eventType].Count == 0)
            _receiverHashToReference.Remove(eventType);

        if (_receivers[eventType].Count == 0)
            _receivers.Remove(eventType);
    }

    public void Raise<T>(T @event) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        if(!_receivers.ContainsKey(eventType))
            return;

        foreach (WeakReference<IBaseEventReceiver> reference in _receivers[eventType])
        {
            if (reference.TryGetTarget(out IBaseEventReceiver receiver))
            {
                ((IEventReceiver<T>)receiver).OnEvent(@event);
            }
        }
    }
}
