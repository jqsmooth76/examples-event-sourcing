using api.organisations.domain.Model;
using api.organisations.domain.Model.Read;
using api.organisations.repositories.organisationview.Dto;
using api.organisations.repositories.Read;
using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api.organisations.repositories.organisationview;

public class OrganisationViewRepository : IOrganisationViewRepository
{
    private IMongoCollection<OrganisationViewDto> _collection;

    public OrganisationViewRepository(IOptions<OrganisationViewDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<OrganisationViewDto>(settings.Value.CollectionName);
    }

    public async Task<OrganisationView> GetAsync(OrganisationId organisationId)
    {
        var result = await _collection.FindAsync(f => f.OrganisationId == organisationId.Value);
        return result.First();
    }

    public async Task<bool> SaveAsync(OrganisationView orgView)
    {
        var dto = orgView.Adapt<OrganisationViewDto>();
        try
        {
            await _collection.ReplaceOneAsync(
                f => f.OrganisationId == dto.OrganisationId,
                dto,
                new ReplaceOptions { IsUpsert = true });

            return true;
        }
        catch (MongoClientException)
        {
            // log the exception here
        }

        return false;
    }
}
