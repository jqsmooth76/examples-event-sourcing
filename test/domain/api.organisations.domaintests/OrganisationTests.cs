using api.organisations.domain.Commands;
using api.organisations.domain.Model;

namespace api.organisations.domain;

public class OrganisationTests
{
    [Fact]
    public void When_CreateOrganisationCommand_ShouldSet_OrganisationId_Name_CreatedBy()
    {
        var createOrganisationCommand = new CreateOrganisationCommand(
            $"Org Name {Guid.NewGuid()}",
             Guid.NewGuid().ToString());

        var organisation = new Organisation(createOrganisationCommand);

        organisation.Id.Value.Should().Be(createOrganisationCommand.OrganisationId.Value);
        organisation.Name.Should().Be(createOrganisationCommand.OrganisationName);
        organisation.CreatedByUserIdentity.Should().Be(createOrganisationCommand.CreatedBy);
    }
}