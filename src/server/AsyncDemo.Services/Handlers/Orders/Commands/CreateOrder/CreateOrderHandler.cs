using AsyncDemo.Data;
using AsyncDemo.Domain;
using AsyncDemo.Domain.Domain;
using MediatR;

namespace AsyncDemo.Services.Handlers.Orders.Commands.CreateOrder;

public record CreateOrderRequest(int TempId, string AssemblyName) : IRequest;

public class CreateOrderHandler : IRequestHandler<CreateOrderRequest>
{
    private readonly AsyncDemoContext _context;

    public CreateOrderHandler(AsyncDemoContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        if (request.AssemblyName == "Error")
        {
            throw new SimulatedException();
        }

        var lineItem1 = GenerateFakeOrderLineItem("part1");
        var lineItem2 = GenerateFakeOrderLineItem("part2");
        var lineItem3 = GenerateFakeOrderLineItem("part3");

        var order = Order.Create(request.TempId, request.AssemblyName, lineItem1, lineItem2, lineItem3);

        _context.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private OrderLineItem GenerateFakeOrderLineItem(string partName)
    {
        var p1 = OrderLineItemRoute.Create(RouteConsts.CutRoute, 0);
        var p2 = OrderLineItemRoute.Create(RouteConsts.BendRoute, 1);

        return OrderLineItem.Create(partName, p1, p2);
    }
}