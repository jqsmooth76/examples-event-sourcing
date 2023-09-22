using api.organisations.domain.Repository.Write;
using Microsoft.Extensions.DependencyInjection;

namespace api.organisations.repositories;

public static class ServiceRegistration
{
    public static IServiceCollection AddOrganisationWriteRepository(this IServiceCollection services)
    {
        services.AddEventStoreClient(new Uri("esdb://host.docker.internal:2113??tls=false&keepAliveTimeout=10000&keepAliveInterval=10000"));

        services.AddSingleton<IOrganisationWriteRepository, OrganisationWriteRepository>();
        // services.AddOptions<EventStoreClientOptions>()
        //     .BindConfiguration("EventStoreClientOptions")
        //     .ValidateDataAnnotations()
        //     .ValidateOnStart();

        return services;
    }
}
