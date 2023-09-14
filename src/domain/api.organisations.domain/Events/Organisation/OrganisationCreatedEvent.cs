namespace api.organisations.domain.Events.Organisation;

public record OrganisationCreatedEvent(
    string Identity,
    string Name,
    string CreatedByIdentity) : IEventStoreEvent
{
    public DateTime TimeStamp { get; } = DateTime.UtcNow;

    public Guid EventId { get; } = Guid.NewGuid();
}
