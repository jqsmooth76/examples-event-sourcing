namespace api.organisations.domain.Model;

public record OrganisationId(string Value)
{
    public static OrganisationId Empty = new OrganisationId(Guid.Empty.ToString());
}
