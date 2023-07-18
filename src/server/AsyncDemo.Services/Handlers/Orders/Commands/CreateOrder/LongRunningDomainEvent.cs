using AsyncDemo.Domain.DomainEvents;
using MediatR;

namespace AsyncDemo.Services.Handlers.Orders.Commands.CreateOrder;

public class LongRunningDomainEvent : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.Delay(3000, cancellationToken);
    }
}