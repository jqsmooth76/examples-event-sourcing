namespace api.organisations.domain.Events;

public interface IHandlerRegistry
{
    void RegisterEventHandler<TEvent>(EventHandler<TEvent> handler) where TEvent : IEventStoreEvent;
}

public delegate void EventHandler<T>(T @event) where T : IEventStoreEvent;
