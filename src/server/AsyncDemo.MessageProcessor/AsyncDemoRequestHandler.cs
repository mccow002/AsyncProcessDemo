using MediatR;
using Rebus.Handlers;
using Rebus.Pipeline;

namespace AsyncDemo.MessageProcessor;

public class AsyncDemoRequestHandler : IHandleMessages<IRequest>
{
    private readonly IMessageContext _messageContext;
    private readonly IServiceProvider _provider;

    public AsyncDemoRequestHandler(
        IMessageContext messageContext,
        IServiceProvider provider)
    {
        _messageContext = messageContext;
        _provider = provider;
    }

    public async Task Handle(IRequest message)
    {
        var token = _messageContext.IncomingStepContext.Load<CancellationToken>();

        using var scope = _provider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(message, token);
    }
}