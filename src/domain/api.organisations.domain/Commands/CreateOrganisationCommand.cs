using MediatR;

namespace api.organisations.domain.Commands;

public record CreateOrganisationCommand(string OrganisationName, string CreatedBy) : IRequest<CreateOrganisationResponse>
{

}
