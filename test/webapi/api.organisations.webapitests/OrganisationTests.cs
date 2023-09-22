using System.Net;
using api.organisations.ViewModels.v1.Organisation;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using apiTestHelpers;
using System.Text.Json;

namespace api.organisations.webapitests;

public class OrganisationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public OrganisationTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GivenCreatingANewOrganisation_Returns201_Created()
    {
        var organisationCreateModel = new OrganisationCreateRequestModel
        {
            OrganisationName = $"New Organisation {Guid.NewGuid()}",
            CreatedByUserIdentity = Guid.NewGuid().ToString(),
            CreatedByUserName = $"User {Guid.NewGuid()}",
        };

        var response = await _httpClient.PostAsync(organisationCreateModel, "/Organisation");

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GivenCreatingANewOrganisation_WithAnInvalidNameValue_Returns400_BadRequest()
    {
        var organisationCreateModel = new OrganisationCreateRequestModel
        {
            CreatedByUserIdentity = Guid.NewGuid().ToString(),
            CreatedByUserName = $"User {Guid.NewGuid()}",
        };

        var response = await _httpClient.PostAsync(organisationCreateModel, "/Organisation");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenCreatingANewOrganisation_WithAnInvalidCreatedByUserIdentity_Returns400_BadRequest()
    {
        var organisationCreateModel = new OrganisationCreateRequestModel
        {
            OrganisationName = "Some Organisation",
            CreatedByUserIdentity = "This is not a Guid",
            CreatedByUserName = "Test User",
        };

        var response = await _httpClient.PostAsync(organisationCreateModel, "/Organisation");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenCreatingANewOrganisation_WithAnInvalidCreatedByUserName_Returns400_BadRequest()
    {
        var organisationCreateModel = new OrganisationCreateRequestModel
        {
            OrganisationName = "Some Organisation",
            CreatedByUserIdentity = "This is not a Guid"
        };

        var response = await _httpClient.PostAsync(organisationCreateModel, "/Organisation");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenAValidOrganisationIsCreated_WhenRetrievingTheOrganisation_ThenItIsAvailableToRead()
    {
        var organisationCreateModel = new OrganisationCreateRequestModel
        {
            OrganisationName = $"New Organisation {Guid.NewGuid()}",
            CreatedByUserIdentity = Guid.NewGuid().ToString(),
            CreatedByUserName = "Test User",
        };

        var createResponse = await _httpClient.PostAsync(organisationCreateModel, "/Organisation");
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var responseString = await createResponse.Content.ReadAsStringAsync();
        var organisationResponseModel = JsonSerializer.Deserialize<OrganisationCreateResponseModel>(responseString, options);

        var getResponse = await _httpClient.GetAsync($"/Organisation/{organisationResponseModel?.OrganisationId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}