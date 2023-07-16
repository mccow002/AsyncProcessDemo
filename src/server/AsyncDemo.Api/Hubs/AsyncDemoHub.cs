using AsyncDemo.Api.Models;
using AsyncDemo.Domain.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace AsyncDemo.Api.Hubs;

public class AsyncDemoHub : Hub<IAsyncDemoHub>
{
    private readonly ILogger<AsyncDemoHub> _logger;

    public AsyncDemoHub(ILogger<AsyncDemoHub> logger)
    {
        _logger = logger;
    }

    [HubMethodName("subscribe-to-group")]
    public async Task SubscribeToRoute(string groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
    }

    [HubMethodName("unsubscribe-from-group")]
    public async Task UnSubscribeFromRoute(string groupId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
    }
}

public interface IAsyncDemoHub
{
    [HubMethodName("notification")]
    Task Notification(ClientNotificationModel notification);

    [HubMethodName("error")]
    Task Error(ErrorContentModel error);

    [HubMethodName("success")]
    Task Success(SuccessNotificationModel message);
}