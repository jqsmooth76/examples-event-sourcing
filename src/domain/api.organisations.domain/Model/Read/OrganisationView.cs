namespace api.organisations.domain.Model.Read;

public class OrganisationView
{
    public string OrganisationId { get; set; }

    public string OrganisationName { get; set; }

    public List<OrganisationMember> Administrators { get; set; }

    public List<OrganisationMember> Members { get; set; }
}

public class OrganisationMember
{
    public string MemberId { get; set; }

    public string Name { get; set; }
}
