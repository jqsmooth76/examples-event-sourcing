using api.organisations.ViewModels.v1.Organisation;
using FluentValidation;
using FluentValidation.Results;

namespace api.organisations.Validators.v1;

public class OrganisationCreateValidator : AbstractValidator<OrganisationCreateRequestModel>
{
    public OrganisationCreateValidator()
    {
        RuleFor(organisation => organisation.OrganisationName)
            .NotEmpty()
            .MinimumLength(5).WithMessage("OrganisationName");
        RuleFor(organisation => organisation.CreatedBy)
            .NotEmpty()
            .Must(ValidateGuid).WithMessage("Not a Guid");
    }

    private bool ValidateGuid(string guidToTest)
    {
        return Guid.TryParse(guidToTest, out _);
    }
}
