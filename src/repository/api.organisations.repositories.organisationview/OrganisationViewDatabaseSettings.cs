namespace api.organisations.repositories.organisationview;

public class OrganisationViewDatabaseSettings
{
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string DatabaseName { get; set; } = "Organisations";
    public string CollectionName { get; set; } = "ORGANISATION_VIEW";
}