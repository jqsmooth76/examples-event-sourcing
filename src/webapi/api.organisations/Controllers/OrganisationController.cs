using api.organisations.domain.Commands;
using api.organisations.ViewModels.v1.Organisation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;

namespace api.organisations.Controllers;

[Route("[controller]")]
[FluentValidationAutoValidation]
public class OrganisationController : Controller
{
    private readonly ILogger<OrganisationController> _logger;
    private readonly IMediator _mediator;

    public OrganisationController(ILogger<OrganisationController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrganisationCreateResponseModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrganisationCreateResponseModel>> PostAsync([FromBody] OrganisationCreateRequestModel organisationCreate)
    {
        var command = new CreateOrganisationCommand(organisationCreate.OrganisationName, organisationCreate.CreatedBy);
        var commandResponse = await _mediator.Send(command);

        var httpResponse = new OrganisationCreateResponseModel
        {
            OrganisationId = commandResponse.organisationId.Id,
            OrganisationName = command.OrganisationName
        };

        return Created($"Organisation/{commandResponse.organisationId.Id}", httpResponse);
    }
}
