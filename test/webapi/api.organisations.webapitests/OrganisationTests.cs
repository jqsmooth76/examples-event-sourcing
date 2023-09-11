using System.Net;
using System.Text;
using System.Text.Json;
using api.organisations.ViewModels.v1.Organisation;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace api.organisations.webapitests;

public class OrganisationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public OrganisationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenCreatingANewOrganisation_Returns201_Created()
    {
        var organisationCreateModel = new OrganisationCreateRequestModel
        {
            OrganisationName = $"New Organisation {Guid.NewGuid()}",
            CreatedBy = Guid.NewGuid().ToString(),
        };
        var content = new StringContent(JsonSerializer.Serialize(organisationCreateModel), Encoding.UTF8, "application/json");

        var response = await _factory.CreateClient().PostAsync("/Organisation", content);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GivenCreatingANewOrganisation_WithAnInvalidNameValue_Returns400_BadRequest()
    {
        var organisationCreateModel = new OrganisationCreateRequestModel
        {
            CreatedBy = Guid.NewGuid().ToString(),
        };
        var content = new StringContent(JsonSerializer.Serialize(organisationCreateModel), Encoding.UTF8, "application/json");

        var response = await _factory.CreateClient().PostAsync("/Organisation", content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenCreatingANewOrganisation_WithAnInvalidCreatedBy_Returns400_BadRequest()
    {
        var organisationCreateModel = new OrganisationCreateRequestModel
        {
            OrganisationName = "Some Organisation",
            CreatedBy = "This is not a Guid"
        };
        var content = new StringContent(JsonSerializer.Serialize(organisationCreateModel), Encoding.UTF8, "application/json");

        var response = await _factory.CreateClient().PostAsync("/Organisation", content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}