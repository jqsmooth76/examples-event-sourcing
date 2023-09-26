using api.organisations.domain.Model;
using api.organisations.domain.Model.Read;
using api.organisations.repositories.organisationview;
using api.organisations.repositories.organisationview.Dto;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace organisationview;

public class OrganisationViewRepositoryTests
{
    private readonly IMongoCollection<OrganisationViewDto> _collection;
    private readonly OrganisationViewRepository _repository;
    private readonly Fixture _fixture;


    public OrganisationViewRepositoryTests()
    {
        var dbSettings = new OrganisationViewDatabaseSettings();

        var mongoClient = new MongoClient(dbSettings.ConnectionString);
        var database = mongoClient.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<OrganisationViewDto>(dbSettings.CollectionName);

        _repository = new OrganisationViewRepository(Options.Create(dbSettings));

        _fixture = new Fixture();
        _fixture.Customize(new TestConventions());
    }

    [Fact]
    public async Task GivenAnNewOrganisationView_WhenItIsSavedToTheRepository_ThenItExistsInTheDB()
    {
        var orgView = _fixture.Create<OrganisationView>();

        var saveResult = await _repository.SaveAsync(orgView);
        saveResult.Should().BeTrue();

        var findResult = await _collection.Find(f => f.OrganisationId == orgView.OrganisationId).FirstOrDefaultAsync();
        findResult.Should().NotBeNull();
        findResult.OrganisationName.Should().Be(orgView.OrganisationName);
    }

    [Fact]
    public async Task GivenAnNewOrganisationViewExistsInTheRepostiory_WhenFindingTheDocument_ThenItIsReturned()
    {
        var orgDto = _fixture.Create<OrganisationViewDto>();

        await _collection.InsertOneAsync(orgDto);

        var findResult = await _repository.GetAsync(new OrganisationId(orgDto.OrganisationId));
        findResult.Should().NotBeNull();
    }
}
