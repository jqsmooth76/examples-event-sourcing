namespace api.organisations.domain.Events.Organisation;

public class OrganisationNameChangedEvent : IEventStoreEvent
{
    public string OrganisationName { get; set; } = string.Empty;
}
