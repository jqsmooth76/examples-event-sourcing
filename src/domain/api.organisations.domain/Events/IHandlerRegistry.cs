using api.organisations.domain.Model;

namespace api.organisations.domain.Events;

public interface IHandlerRegistry
{
    void RegisterEventHandler<TEvent>(DomainEventHandler<TEvent> handler) where TEvent : IEventStoreEvent;
}

