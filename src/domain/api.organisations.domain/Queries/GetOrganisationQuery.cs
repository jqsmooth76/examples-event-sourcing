using api.organisations.domain.Model;
using MediatR;

namespace api.organisations.domain.Queries;

public record GetOrganisationQuery(OrganisationId organisationId) : IRequest<GetOrganisationResponse>
{

}
