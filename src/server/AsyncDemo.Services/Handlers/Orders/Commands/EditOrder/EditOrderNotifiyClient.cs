using AsyncDemo.Domain.DomainEvents;
using AsyncDemo.Domain.Notifications;
using AsyncDemo.Services.Core;
using MediatR;

namespace AsyncDemo.Services.Handlers.Orders.Commands.EditOrder;

public record OrderEditedClientMessage(OrderEditedPayload Payload)
    : ClientNotification<OrderEditedPayload>(Payload, "OrderEdited");

public record OrderEditedPayload(int OrderId, string AssemblyName) : IClientNotificationPayload;

public class EditOrderNotifiyClient : INotificationHandler<OrderEditedEvent>
{
    private readonly IClientUpdater _clientUpdater;

    public EditOrderNotifiyClient(IClientUpdater clientUpdater)
    {
        _clientUpdater = clientUpdater;
    }


    public async Task Handle(OrderEditedEvent notification, CancellationToken cancellationToken)
    {
        var payload = new OrderEditedPayload(notification.Order.OrderId, notification.Order.AssemblyName);

        await _clientUpdater.SendAll(new OrderEditedClientMessage(payload), cancellationToken);
    }
}