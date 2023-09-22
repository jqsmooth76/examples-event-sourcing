using api.organisations.domain.Queries;
using api.organisations.repositories.Read;
using MediatR;

namespace api.organisations.domain.Handlers;

public class GetOrganisationQueryHandler : IRequestHandler<GetOrganisationQuery, GetOrganisationResponse>
{
    private readonly IOrganisationViewRepository _organisationViewRepository;

    public GetOrganisationQueryHandler(IOrganisationViewRepository organisationViewRepository)
    {
        _organisationViewRepository = organisationViewRepository;
    }

    public Task<GetOrganisationResponse> Handle(GetOrganisationQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
