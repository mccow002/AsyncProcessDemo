using AsyncDemo.Services.Core;

namespace AsyncDemo.Api.Services;

public class ConnectionIdService : ICurrentConnectionIdService
{
    public ConnectionIdService(IHttpContextAccessor context)
    {
        if (context.HttpContext != null)
        {
            var connectionIdHeader = context.HttpContext.Request.Headers["ConnectionId"];
            if (connectionIdHeader.Any() && !string.IsNullOrWhiteSpace(connectionIdHeader.First()))
                ConnectionId = connectionIdHeader.First();
        }
    }

    public string? ConnectionId { get; }
}