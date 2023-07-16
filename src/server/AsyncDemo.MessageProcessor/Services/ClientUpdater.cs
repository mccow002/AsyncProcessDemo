using AsyncDemo.Domain.Notifications;
using AsyncDemo.Services.Core;
using System.Text.Json;
using System.Text;

namespace AsyncDemo.MessageProcessor.Services;

public class ClientUpdater : IClientUpdater
{
    private readonly IAsyncDemoApiClient _client;
    private readonly ICurrentConnectionIdService _connectionIdService;

    private readonly JsonSerializerOptions _jsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ClientUpdater(IAsyncDemoApiClient client, ICurrentConnectionIdService connectionIdService)
    {
        _client = client;
        _connectionIdService = connectionIdService;
    }

    public async Task SendGroup<T>(GroupNotification<T> notification, CancellationToken token) where T : IClientNotificationPayload
    {
        var req = new HttpRequestMessage(HttpMethod.Post, "notifications/send-group");
        req.Content = new StringContent(JsonSerializer.Serialize(notification, _jsonOpts), Encoding.UTF8, "application/json");
        var rsp = await _client.SendAsync(req, token);

        if (!rsp.IsSuccessStatusCode)
        {
            var json = await rsp.Content.ReadAsStringAsync(token);
            throw new ClientUpdaterException(
                rsp.RequestMessage?.RequestUri.ToString(),
                rsp.StatusCode,
                string.IsNullOrWhiteSpace(json) ? rsp.ReasonPhrase : json
            );
        }
    }

    public async Task SendAll<T>(ClientNotification<T> clientNotification, CancellationToken token) where T : IClientNotificationPayload
    {
        var req = new HttpRequestMessage(HttpMethod.Post, "notifications/send-all");
        req.Content = new StringContent(JsonSerializer.Serialize(clientNotification, _jsonOpts), Encoding.UTF8, "application/json");
        var rsp = await _client.SendAsync(req, token);

        if (!rsp.IsSuccessStatusCode)
        {
            var json = await rsp.Content.ReadAsStringAsync(token);
            throw new ClientUpdaterException(
                rsp.RequestMessage?.RequestUri.ToString(),
                rsp.StatusCode,
                string.IsNullOrWhiteSpace(json) ? rsp.ReasonPhrase : json
            );
        }
    }

    public async Task Error<T>(string clientErrorMessage, Exception? ex, ClientNotification<T> clientNotification, CancellationToken token = default) where T : IClientNotificationPayload
    {
        var req = new HttpRequestMessage(HttpMethod.Post, "notifications/error");

        var evt = new ErrorNotification<T>(clientNotification, clientErrorMessage, ex?.ToString() ?? string.Empty);
        var reqJson = JsonSerializer.Serialize(new ErrorModel<T>(_connectionIdService.ConnectionId, evt), _jsonOpts);
        req.Content = new StringContent(reqJson, Encoding.UTF8, "application/json");

        var rsp = await _client.SendAsync(req, token);

        if (!rsp.IsSuccessStatusCode)
        {
            var json = await rsp.Content.ReadAsStringAsync(token);
            throw new ClientUpdaterException(
                rsp.RequestMessage?.RequestUri.ToString(),
                rsp.StatusCode,
                string.IsNullOrWhiteSpace(json) ? rsp.ReasonPhrase : json
            );
        }
    }

    public async Task Success(string clientSuccessMessage, CancellationToken token = default)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, "notifications/success");

        var evt = new SuccessModel(_connectionIdService.ConnectionId, clientSuccessMessage);
        var reqJson = JsonSerializer.Serialize(evt, _jsonOpts);
        req.Content = new StringContent(reqJson, Encoding.UTF8, "application/json");

        var rsp = await _client.SendAsync(req, token);

        if (!rsp.IsSuccessStatusCode)
        {
            var json = await rsp.Content.ReadAsStringAsync(token);
            throw new ClientUpdaterException(
                rsp.RequestMessage?.RequestUri.ToString(),
                rsp.StatusCode,
                string.IsNullOrWhiteSpace(json) ? rsp.ReasonPhrase : json
            );
        }
    }
}

public record ErrorModel<T>(string ConnectionId, ErrorNotification<T> Payload) where T : IClientNotificationPayload;

public record SuccessModel(string ConnectionId, string SuccessMessage);