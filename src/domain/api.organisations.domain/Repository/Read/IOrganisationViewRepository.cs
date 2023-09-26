using api.organisations.domain.Model;
using api.organisations.domain.Model.Read;

namespace api.organisations.repositories.Read;

public interface IOrganisationViewRepository
{
    Task<OrganisationView> GetAsync(OrganisationId organisationId);

    Task<bool> SaveAsync(OrganisationView organisationView);
}
