namespace api.organisations.domain.Events.Organisation;

public class OrganisationCreatedEvent : BaseEvent
{
    public string Identity { get; set; }
    public string Name { get; set; }
    public string CreatedByIdentity { get; set; }
    public string CreatedByName { get; set; }
}
