using api.organisations.domain.Commands;
using api.organisations.domain.Model;
using api.organisations.domain.Model.Reference;
using api.organisations.repositories;
using EventStore.Client;
using FluentAssertions;

namespace organisationWrite;

public class OrganisationWriteRepositoryTests
{
    private OrganisationWriteRepository _repository;

    public OrganisationWriteRepositoryTests()
    {
        var settings = EventStoreClientSettings
            .Create("{connectionString}");
        _repository = new OrganisationWriteRepository(client);
    }

    [Fact]
    public async Task GivenANewOrganisation_ShouldWriteOrganisationSuccessfully()
    {
        var createdByUserReference = UserReference.From(Guid.NewGuid().ToString(), "Created By User");
        var createOrganisationCommand = new CreateOrganisationCommand(
            $"Org Name {Guid.NewGuid()}",
            createdByUserReference);

        var organisation = new Organisation(createOrganisationCommand);

        var result = await _repository.SaveAsync(organisation);

        result.Should().BeTrue();
    }
}