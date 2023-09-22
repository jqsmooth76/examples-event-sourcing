using api.organisations.repositories.organisationview;
using api.organisations.repositories.Read;
using Microsoft.Extensions.DependencyInjection;

namespace api.organisations.repositories;

public static class ServiceRegistration
{
    public static IServiceCollection AddOrganisationViewRepository(this IServiceCollection services)
    {
        return services.AddSingleton<IOrganisationViewRepository, OrganisationViewRepository>();
    }
}
