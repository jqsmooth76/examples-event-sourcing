using api.organisations.domain.Commands;
using api.organisations.domain.Events.Organisation;
using api.organisations.domain.Model;
using api.organisations.domain.Model.Reference;

namespace api.organisations.domain;

public class OrganisationTests
{
    [Fact]
    public void When_CreateOrganisationCommand_ShouldSet_OrganisationId_Name()
    {
        var createdByUserReference = UserReference.From(Guid.NewGuid().ToString(), "Created By User");
        var createOrganisationCommand = new CreateOrganisationCommand(
            $"Org Name {Guid.NewGuid()}",
            createdByUserReference);

        var organisation = new Organisation(createOrganisationCommand);

        organisation.Identity.Value.Should().Be(createOrganisationCommand.OrganisationId.Value);
        organisation.Name.Should().Be(createOrganisationCommand.OrganisationName);
    }

    [Fact]
    public void When_CreateOrganisationCommand_ShouldSet_InitialAdministrator()
    {
        var createdByUserReference = UserReference.From(Guid.NewGuid().ToString(), "Created By User");
        var createOrganisationCommand = new CreateOrganisationCommand(
            $"Org Name {Guid.NewGuid()}",
            createdByUserReference);

        var organisation = new Organisation(createOrganisationCommand);

        organisation.IsAdministrator(createdByUserReference).Should().BeTrue();
        organisation.IsMember(createdByUserReference).Should().BeTrue();
    }

    [Fact]
    public void When_OrganisationIsRehydratedWithEvents_TheOrganisationExistsWithTheCorrectValues()
    {
        var createdByUser = UserReference.From(Guid.NewGuid().ToString(), "Joe Bloggs");
        var organisationCreatedEvent = new OrganisationCreatedEvent
        {
            CreatedByIdentity = createdByUser.UserIdentity.Identity,
            CreatedByName = createdByUser.Name,
            Identity = Guid.NewGuid().ToString(),
            Name = $"Organisation Name {Guid.NewGuid()}"
        };

        var organisation = new Organisation(new[] { organisationCreatedEvent });

        organisation.Name.Should().Be(organisationCreatedEvent.Name);
        organisation.Identity.Value.Should().Be(organisationCreatedEvent.Identity);
        organisation.IsAdministrator(createdByUser).Should().BeTrue();
    }

    [Fact]
    public void When_ChangeOrganisationNameCommand_ThenTheOragnisationNameIsChanged()
    {
        var newOrganisationName = $"OrgName{Guid.NewGuid()}";
        var changeOrgNameCommand = new ChangeOrganisationNameCommand(newOrganisationName);

        var organisation = CreateOrganisation();

        organisation.ChangeOrganisationName(changeOrgNameCommand);

        organisation.Name.Should().Be(newOrganisationName);
    }

    private Organisation CreateOrganisation()
    {
        var createdByUserReference = UserReference.From(Guid.NewGuid().ToString(), "Created By User");
        var createOrganisationCommand = new CreateOrganisationCommand(
            $"Org Name {Guid.NewGuid()}",
            createdByUserReference);

        return new Organisation(createOrganisationCommand);
    }
}