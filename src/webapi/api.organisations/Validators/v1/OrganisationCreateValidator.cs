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
            .MinimumLength(5).WithMessage("OrganisationName should be minimum 5 characters");
        RuleFor(organisation => organisation.CreatedByUserIdentity)
            .NotEmpty()
            .Must(ValidateGuid).WithMessage("CreatedByUserIdentity must be a Guid");
        RuleFor(organisation => organisation.CreatedByUserIdentity)
            .NotEmpty()
            .MinimumLength(5).WithMessage("CreatedByUserName should be minimum 5 characters"); ;
    }

    private bool ValidateGuid(string guidToTest)
    {
        return Guid.TryParse(guidToTest, out _);
    }
}
