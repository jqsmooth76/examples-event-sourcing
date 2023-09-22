using api.organisations.domain.Model;
using api.organisations.domain.Model.Read;
using api.organisations.repositories.Read;

namespace api.organisations.repositories.organisationview;

public class OrganisationViewRepository : IOrganisationViewRepository
{
    public Task<OrganisationView> GetAsync(OrganisationId organisationId)
    {
        throw new NotImplementedException();
    }
}
