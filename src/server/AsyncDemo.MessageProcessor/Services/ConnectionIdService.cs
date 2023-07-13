using AsyncDemo.Services.Core;
using Rebus.Pipeline;

namespace AsyncDemo.MessageProcessor.Services;

public class ConnectionIdService : ICurrentConnectionIdService
{
    public ConnectionIdService(IMessageContext messageContext)
    {
        if (messageContext.Headers.TryGetValue(nameof(ICurrentConnectionIdService.ConnectionId), out var connIdHeader) && !string.IsNullOrWhiteSpace(connIdHeader))
            ConnectionId = connIdHeader;
    }

    public string? ConnectionId { get; }
}