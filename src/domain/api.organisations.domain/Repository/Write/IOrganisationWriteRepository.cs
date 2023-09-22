using api.organisations.domain.Model;

namespace api.organisations.domain.Repository.Write;

public interface IOrganisationWriteRepository
{
    Task<bool> SaveAsync(Organisation organisation);
}
