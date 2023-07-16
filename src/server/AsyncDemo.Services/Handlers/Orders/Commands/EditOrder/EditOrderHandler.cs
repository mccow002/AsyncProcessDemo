using AsyncDemo.Data;
using AsyncDemo.Domain.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsyncDemo.Services.Handlers.Orders.Commands.EditOrder;

public record EditOrderRequest(int OrderId, string AssemblyName) : IRequest;

public class EditOrderHandler : IRequestHandler<EditOrderRequest>
{
    private readonly AsyncDemoContext _context;

    public EditOrderHandler(AsyncDemoContext context)
    {
        _context = context;
    }

    public async Task Handle(EditOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _context.Set<Order>()
            .FirstAsync(x => x.OrderId == request.OrderId, cancellationToken);

        order.UpdateAssemblyName(request.AssemblyName);
        await _context.SaveChangesAsync(cancellationToken);

    }
}