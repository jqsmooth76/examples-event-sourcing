using api.organisations.domain.Model.Write;

namespace api.organisations.domain.Events.Organisation;

public class UserJoinedOrganisationEvent : BaseEvent
{
    public string UserIdentity { get; set; }
    public string UserName { get; set; }
    public OrganisationRole Role { get; set; }
}