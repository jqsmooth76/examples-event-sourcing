using api.organisations.domain.Commands;
using api.organisations.domain.Events;
using api.organisations.domain.Events.Organisation;

namespace api.organisations.domain.Model;

public class Organisation : IHandlerRegistry
{
    private readonly IList<IEventStoreEvent> _domainEvents = new List<IEventStoreEvent>();

    private readonly Dictionary<Type, Events.EventHandler<IEventStoreEvent>> _eventHandlers = new Dictionary<Type, Events.EventHandler<IEventStoreEvent>>();

    public Organisation(CreateOrganisationCommand createOrganisationCommand)
    {
        AddEventHandlers(this);

        var createdEvent = new OrganisationCreatedEvent(
            createOrganisationCommand.OrganisationId.Value,
            createOrganisationCommand.OrganisationName,
            createOrganisationCommand.CreatedBy
        );

        Apply(createdEvent);
    }

    public OrganisationId Id { get; private set; } = OrganisationId.Empty;
    public string Name { get; private set; }
    public string CreatedByUserIdentity { get; private set; }

    #region #EventGubbins
    private void Apply<TEvent>(TEvent @event) where TEvent : IEventStoreEvent
    {
        Mutate(@event);
        _domainEvents.Add(@event);
    }
    private void Mutate(IEventStoreEvent @event)
    {
        _eventHandlers[@event.GetType()](@event);
    }
    protected void AddEventHandlers(IHandlerRegistry registry)
    {
        registry.RegisterEventHandler<OrganisationCreatedEvent>(When);
    }
    #endregion

    private void When(OrganisationCreatedEvent @event)
    {
        Id = new OrganisationId(@event.Identity);
        Name = @event.Name;
        CreatedByUserIdentity = @event.CreatedByIdentity;
    }

    public void RegisterEventHandler<TEvent>(Events.EventHandler<TEvent> handler) where TEvent : IEventStoreEvent
    {
        _eventHandlers.Add(typeof(TEvent), e => handler.Invoke((TEvent)e));
    }
}
