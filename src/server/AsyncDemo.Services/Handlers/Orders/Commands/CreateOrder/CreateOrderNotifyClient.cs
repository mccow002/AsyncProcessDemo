using AsyncDemo.Data;
using AsyncDemo.Domain.Domain;
using AsyncDemo.Domain.DomainEvents;
using AsyncDemo.Domain.Notifications;
using AsyncDemo.Services.Core;
using AsyncDemo.Services.Handlers.Orders.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsyncDemo.Services.Handlers.Orders.Commands.CreateOrder;

public record OrderCreatedClientMessage(OrderCreatedPayload Payload)
    : ClientNotification<OrderCreatedPayload>(Payload, "OrderCreated");

public record OrderCreatedPayload(OrderViewModel Order, int TempId) : IClientNotificationPayload;

public class CreateOrderNotifyClient : INotificationHandler<OrderCreatedEvent>
{
    private readonly AsyncDemoContext _context;
    private readonly IMapper _mapper;
    private readonly IClientUpdater _clientUpdater;

    public CreateOrderNotifyClient(
        AsyncDemoContext context,
        IMapper mapper,
        IClientUpdater clientUpdater
    )
    {
        _context = context;
        _mapper = mapper;
        _clientUpdater = clientUpdater;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var orderModel = await _context.Set<Order>()
            .Where(x => x.OrderId == notification.Order.OrderId)
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);

        var clientMessage = new OrderCreatedPayload(orderModel, notification.TempId);
        await _clientUpdater.SendAll(new OrderCreatedClientMessage(clientMessage), cancellationToken);
        await _clientUpdater.Success("Order Successfully Created!", cancellationToken);
    }
}