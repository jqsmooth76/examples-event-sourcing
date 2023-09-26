using api.organisations.domain.Model.Read;
using MongoDB.Bson;

namespace api.organisations.repositories.organisationview.Dto;

public class OrganisationViewDto : OrganisationView
{
    public ObjectId Id { get; set; }

    public OrganisationViewDto()
    {
        Id = ObjectId.GenerateNewId();
    }
}
