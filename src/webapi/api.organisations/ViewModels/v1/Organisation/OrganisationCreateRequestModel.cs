namespace api.organisations.ViewModels.v1.Organisation;

public class OrganisationCreateRequestModel
{
    public string OrganisationName { get; set; } = string.Empty;
    public string CreatedByUserIdentity { get; set; } = string.Empty;
    public string CreatedByUserName { get; set; } = string.Empty;
}
