namespace AsyncDemo.MessageProcessor.Services;

public interface IAsyncDemoApiClient
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token);
}

public class AsyncDemoApiClient : IAsyncDemoApiClient
{
    private readonly HttpClient _client;

    public AsyncDemoApiClient(HttpClient client, IConfiguration config)
    {
        _client = client;
        _client.BaseAddress = new Uri(config["ApiUrl"]);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
        return await _client.SendAsync(request, token);
    }
}