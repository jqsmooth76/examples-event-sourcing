using api.organisations.domain.Model;
using MediatR;

namespace api.organisations.domain.Commands;

public record CreateOrganisationCommand(string OrganisationName, string CreatedBy) : IRequest<CreateOrganisationResponse>
{
    public OrganisationId OrganisationId { get; init; } = new OrganisationId(Guid.NewGuid().ToString());
}
