using api.organisations.domain.Events;

namespace api.organisations.domain.Model;

public abstract class AggregateRoot<T> : Entity<T>, IHandlerRegistry
{
    private readonly Dictionary<Type, DomainEventHandler<IEventStoreEvent>> _eventHandlers = new();
    private readonly List<IEventStoreEvent> _domainEvents = new();

    public IReadOnlyList<IEventStoreEvent> Events => _domainEvents.AsReadOnly();
    public int Version { get; private set; }


    protected AggregateRoot(T identity = default) : base(identity)
    {
        AddEventHandlers(this);
    }

    protected AggregateRoot(IList<IEventStoreEvent> events) : this()
    {
        Rehydrate(events);
    }

    protected void Apply<TEvent>(TEvent @event)
        where TEvent : IEventStoreEvent
    {
        Mutate(@event);
        _domainEvents.Add(@event);
    }

    private void Rehydrate<TEvent>(IList<TEvent> events)
        where TEvent : IEventStoreEvent
    {
        foreach (var @event in events)
        {
            Mutate(@event);
            Version++;
        }
    }

    private void Mutate(IEventStoreEvent @event)
    {
        _eventHandlers[@event.GetType()](@event);
    }

    protected virtual void AddEventHandlers(IHandlerRegistry registry) { }

    void IHandlerRegistry.RegisterEventHandler<TEvent>(DomainEventHandler<TEvent> handler)
    {
        _eventHandlers.Add(typeof(TEvent), e => handler.Invoke((TEvent)e));
    }
}

public delegate void DomainEventHandler<T>(T @event) where T : IEventStoreEvent;
