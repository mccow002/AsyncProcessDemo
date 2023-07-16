using AsyncDemo.Services.Core;
using MediatR;
using Rebus.Handlers;
using Rebus.Retry.Simple;
using System;

namespace AsyncDemo.MessageProcessor;

public class AsyncDemoErrorHandler : IHandleMessages<IFailed<IRequest>>
{
    private readonly IMediator _mediator;

    public AsyncDemoErrorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(IFailed<IRequest> message)
    {
        var exception = message.Exceptions.First();

        var reqType = message.Message.GetType();
        var errorReqType = typeof(ErrorRequest<>).MakeGenericType(reqType);
        var errorReq = Activator.CreateInstance(errorReqType, message.Message, exception);

        await _mediator.Send(errorReq);
    }
}