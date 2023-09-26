using api.organisations.domain.Commands;
using api.organisations.domain.Events;
using api.organisations.domain.Events.Organisation;
using api.organisations.domain.Model.Reference;
using api.organisations.domain.Model.Write;

namespace api.organisations.domain.Model;

public class Organisation : AggregateRoot<OrganisationId>
{
    private readonly IList<UserReference> _organisationMembers = new List<UserReference>();
    private readonly IList<UserReference> _organisationAdmins = new List<UserReference>();

    public Organisation(CreateOrganisationCommand createOrganisationCommand)
        : base(createOrganisationCommand.OrganisationId)
    {
        // validate the command has what i need

        var createdEvent = new OrganisationCreatedEvent
        {
            Identity = createOrganisationCommand.OrganisationId.Value,
            Name = createOrganisationCommand.OrganisationName,
            CreatedByIdentity = createOrganisationCommand.CreatedBy.UserIdentity.Identity,
            CreatedByName = createOrganisationCommand.CreatedBy.Name
        };

        Apply(createdEvent);
    }

    public Organisation(IList<IEventStoreEvent> events) : base(events)
    {
    }

    public string Name { get; private set; } = string.Empty;
    public UserReference CreatedBy { get; private set; } = UserReference.Empty;


    public bool ChangeOrganisationName(ChangeOrganisationNameCommand command)
    {
        if (command.NewOrganisationName == null)
            return false;

        var orgNameChangeEvent = new OrganisationNameChangedEvent { OrganisationName = command.NewOrganisationName };

        Apply(orgNameChangeEvent);

        return true;
    }


    protected override void AddEventHandlers(IHandlerRegistry registry)
    {
        registry.RegisterEventHandler<OrganisationCreatedEvent>(When);
        registry.RegisterEventHandler<UserJoinedOrganisationEvent>(When);
        registry.RegisterEventHandler<OrganisationNameChangedEvent>(When);
    }

    private void When(OrganisationCreatedEvent @event)
    {
        var userReference = UserReference.From(@event.CreatedByIdentity, @event.CreatedByName);

        Identity = new OrganisationId(@event.Identity);
        Name = @event.Name;
        CreatedBy = userReference;

        _organisationAdmins.Add(userReference);
    }

    private void When(UserJoinedOrganisationEvent @event)
    {
        var userRefernce = UserReference.From(@event.UserIdentity, @event.UserName);
        switch (@event.Role)
        {
            case OrganisationRole.Admin:
                _organisationAdmins.Add(userRefernce);
                break;
            default:
                _organisationMembers.Add(userRefernce);
                break;
        };

    }

    private void When(OrganisationNameChangedEvent @event)
    {
        Name = @event.OrganisationName;
    }

    public bool IsAdministrator(UserReference userReference)
    {
        return _organisationAdmins.Select(u => u.UserIdentity).Contains(userReference.UserIdentity);
    }

    public bool IsMember(UserReference userReference)
    {
        return _organisationMembers.Select(u => u.UserIdentity).Contains(userReference.UserIdentity)
        || IsAdministrator(userReference);
    }
}
