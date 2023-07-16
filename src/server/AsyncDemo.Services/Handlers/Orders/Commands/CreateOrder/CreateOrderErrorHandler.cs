using AsyncDemo.Domain.Notifications;
using AsyncDemo.Services.Core;
using MediatR;

namespace AsyncDemo.Services.Handlers.Orders.Commands.CreateOrder;

public record OrderCreatedErrorNotification(int TempId) : IClientNotificationPayload;

public class CreateOrderErrorHandler : IRequestHandler<ErrorRequest<CreateOrderRequest>>
{
    private readonly IClientUpdater _clientUpdater;

    public CreateOrderErrorHandler(IClientUpdater clientUpdater)
    {
        _clientUpdater = clientUpdater;
    }

    public async Task Handle(ErrorRequest<CreateOrderRequest> request, CancellationToken cancellationToken)
    {
        await _clientUpdater.Error(
            "An Error Occurred created your Order!",
            request.Exception,
            new ClientNotification<OrderCreatedErrorNotification>(new(request.Request.TempId), "OrderCreatedError"),
            cancellationToken
        );
    }
}