using api.organisations.domain.Commands;
using api.organisations.domain.Model;
using MediatR;

namespace api.organisations.domain.Handlers;

public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, CreateOrganisationResponse>
{
    public Task<CreateOrganisationResponse> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
    {
        var organisationId = new OrganisationId(Guid.NewGuid().ToString());
        return Task.FromResult(new CreateOrganisationResponse(organisationId));
    }
}
