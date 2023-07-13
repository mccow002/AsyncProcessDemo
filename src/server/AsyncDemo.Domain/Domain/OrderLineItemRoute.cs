using AsyncDemo.Domain.DomainEvents;

namespace AsyncDemo.Domain.Domain;

public class OrderLineItemRoute
{
    internal OrderLineItemRoute(int routeId, int sequence)
    {
        RouteId = routeId;
        Sequence = sequence;
    }

    public int OrderLineItemRouteId { get; internal set; }

    public int RouteId { get; internal set; }

    public int Sequence { get; internal set; }

    public DateTime? SignOffDate { get; internal set; }

    public int OrderLineItemId { get; internal set; }

    public OrderLineItem OrderLineItem { get; internal set; }

    public static OrderLineItemRoute Create(int routeId, int sequence)
    {
        return new OrderLineItemRoute(routeId, sequence);
    }
}