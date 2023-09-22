using System.Text;
using System.Text.Json;

namespace apiTestHelpers;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient httpClient, T model, string url) where T : class
    {
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);
        return response;
    }
}
