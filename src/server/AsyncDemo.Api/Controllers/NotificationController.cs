using AsyncDemo.Api.Hubs;
using AsyncDemo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AsyncDemo.Api.Controllers;

[ApiController]
[Route("notifications")]
public class NotificationController : ControllerBase
{
    private readonly IHubContext<AsyncDemoHub, IAsyncDemoHub> _hubContext;

    public NotificationController(IHubContext<AsyncDemoHub, IAsyncDemoHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("send-group")]
    public async Task<IActionResult> Send(GroupNotificationModel notification)
    {
        await _hubContext.Clients.Groups(notification.GroupName)
            .Notification(notification.Notification);

        return Ok();
    }

    [HttpPost("send-all")]
    public async Task<IActionResult> SendAll(ClientNotificationModel notification)
    {
        await _hubContext.Clients.All
            .Notification(notification);

        return Ok();
    }

    [HttpPost("error")]
    public async Task<IActionResult> Error(ErrorNotificationModel error)
    {
        await _hubContext.Clients.Client(error.ConnectionId)
            .Error(error.Payload);

        return Ok();
    }

    [HttpPost("success")]
    public async Task<IActionResult> Success(SuccessNotificationModel success)
    {
        await _hubContext.Clients.Client(success.ConnectionId)
            .Success(success);

        return Ok();
    }
}