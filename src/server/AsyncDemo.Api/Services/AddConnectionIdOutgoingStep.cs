using AsyncDemo.Services.Core;
using Rebus.Messages;
using Rebus.Pipeline;

namespace AsyncDemo.Api.Services;

public class AddConnectionIdOutgoingStep : IOutgoingStep
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddConnectionIdOutgoingStep(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task Process(OutgoingStepContext context, Func<Task> next)
    {
        var message = context.Load<TransportMessage>();

        var connectionIdHeader = _httpContextAccessor.HttpContext.Request.Headers["ConnectionId"];
        if (connectionIdHeader.Any() && !string.IsNullOrWhiteSpace(connectionIdHeader.First()))
            message.Headers.Add(nameof(ICurrentConnectionIdService.ConnectionId), connectionIdHeader.First());

        return next();
    }
}