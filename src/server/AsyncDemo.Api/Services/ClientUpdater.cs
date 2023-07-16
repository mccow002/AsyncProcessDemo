using AsyncDemo.Api.Hubs;
using AsyncDemo.Api.Models;
using AsyncDemo.Domain.Notifications;
using AsyncDemo.Services.Core;
using Microsoft.AspNetCore.SignalR;

namespace AsyncDemo.Api.Services;

public class ClientUpdater : IClientUpdater
{
    private readonly IHubContext<AsyncDemoHub, IAsyncDemoHub> _hubContext;
    private readonly ICurrentConnectionIdService _connectionIdService;

    public ClientUpdater(
        IHubContext<AsyncDemoHub, IAsyncDemoHub> hubContext,
        ICurrentConnectionIdService connectionIdService)
    {
        _hubContext = hubContext;
        _connectionIdService = connectionIdService;
    }

    public async Task SendGroup<T>(GroupNotification<T> notification, CancellationToken token) where T : IClientNotificationPayload
    {
        await _hubContext.Clients.Groups(notification.GroupName)
            .Notification(new ClientNotificationModel(
                notification.Notification.Payload,
                notification.Notification.EventName
            ));
    }

    public async Task SendAll<T>(ClientNotification<T> notification, CancellationToken token) where T : IClientNotificationPayload
    {
        await _hubContext.Clients.All
            .Notification(new ClientNotificationModel(
                notification.Payload,
                notification.EventName
            ));
    }

    public async Task Error<T>(string clientErrorMessage, Exception ex, ClientNotification<T> notification, CancellationToken token = default) where T : IClientNotificationPayload
    {
        await _hubContext.Clients.Client(_connectionIdService.ConnectionId)
            .Error(new ErrorContentModel(
                new ClientNotificationModel(
                    notification.Payload,
                    notification.EventName
                ), 
                clientErrorMessage, 
                ex.ToString()
            ));
    }

    public async Task Success(string clientSuccessMessage, CancellationToken token = default)
    {
        await _hubContext.Clients.Client(_connectionIdService.ConnectionId)
            .Success(new SuccessNotificationModel(clientSuccessMessage, _connectionIdService.ConnectionId));
    }
}