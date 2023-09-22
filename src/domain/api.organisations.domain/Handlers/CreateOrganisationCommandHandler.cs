using api.organisations.domain.Commands;
using api.organisations.domain.Model;
using api.organisations.domain.Repository.Write;
using api.organisations.repositories.Read;
using MediatR;

namespace api.organisations.domain.Handlers;

public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, CreateOrganisationResponse>
{
    private readonly IOrganisationWriteRepository _organisationWriteRepository;

    public CreateOrganisationCommandHandler(IOrganisationWriteRepository organisationWriteRepository)
    {
        _organisationWriteRepository = organisationWriteRepository;
    }

    public async Task<CreateOrganisationResponse> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
    {
        // validate org name uniqueness
        // 1. go to the materialised view repo
        // 2. query for existing names
        // 3. return an error signal (dont throw an exception)

        var organisation = new Organisation(request);
        await _organisationWriteRepository.SaveAsync(organisation);

        return new CreateOrganisationResponse(organisation.Identity);
    }

    //
    // 1 . get the stream from teh evnt store
    // 2 . play all the existing events in order onto the aggregate root
    // 3. mutate the object by running the new command
}
