using System.Text;
using System.Text.Json;
using api.organisations.domain.Events;
using api.organisations.domain.Model;
using api.organisations.domain.Repository.Write;
using EventStore.Client;

namespace api.organisations.repositories;

public class OrganisationWriteRepository : IOrganisationWriteRepository
{
    private readonly EventStoreClient _eventStoreClient;

    public OrganisationWriteRepository(EventStoreClient eventStoreClient)
    {
        _eventStoreClient = eventStoreClient;
    }

    public async Task<bool> SaveAsync(Organisation organisation)
    {
        try
        {
            var eventData = GetEventDataFor(organisation.Events);

            await _eventStoreClient.AppendToStreamAsync(
                $"organisation-{organisation.Identity.Value}",
                StreamState.Any,
                eventData);
        }
        catch
        {
            return false;
        }

        return true;
    }

    private IList<EventData> GetEventDataFor(IReadOnlyList<IEventStoreEvent> events)
    {
        var eventDataList = new List<EventData>();

        foreach (var @event in events)
        {
            var eventDataJson = JsonSerializer.Serialize(@event, @event.GetType());

            var eventData = new EventData(
                    Uuid.NewUuid(),
                    @event.GetType().Name,
                    Encoding.UTF8.GetBytes(eventDataJson));

            eventDataList.Add(eventData);
        }

        return eventDataList;
    }
}
