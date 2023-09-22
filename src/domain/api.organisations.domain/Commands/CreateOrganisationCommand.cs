using api.organisations.domain.Model;
using api.organisations.domain.Model.Reference;
using MediatR;

namespace api.organisations.domain.Commands;

public record CreateOrganisationCommand(
    string OrganisationName,
    UserReference CreatedBy) : IRequest<CreateOrganisationResponse>
{
    public OrganisationId OrganisationId { get; init; } = new OrganisationId(Guid.NewGuid().ToString());
}
