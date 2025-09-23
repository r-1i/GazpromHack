public interface IEvent {}

public interface IBaseEventReceiver {}

public interface IEventReceiver<T> : IBaseEventReceiver where T : struct, IEvent
{ 
    void OnEvent(T @event);
}