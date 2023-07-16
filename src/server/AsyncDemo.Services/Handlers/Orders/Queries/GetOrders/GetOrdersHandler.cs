using AsyncDemo.Data;
using AsyncDemo.Domain.Domain;
using AsyncDemo.Services.Handlers.Orders.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsyncDemo.Services.Handlers.Orders.Queries.GetOrders;

public record GetOrdersRequest : IRequest<List<OrderViewModel>>;

public class GetOrdersHandler : IRequestHandler<GetOrdersRequest, List<OrderViewModel>>
{
    private readonly AsyncDemoContext _context;
    private readonly IMapper _mapper;

    public GetOrdersHandler(AsyncDemoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrderViewModel>> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
    {
        return await _context.Set<Order>()
            .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}