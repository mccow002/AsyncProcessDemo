using AsyncDemo.Domain.DomainEvents;

namespace AsyncDemo.Domain.Domain;

public class OrderLineItem : Entity
{
    private ICollection<OrderLineItemRoute> _routes = new List<OrderLineItemRoute>();

    internal OrderLineItem()
    {}

    internal OrderLineItem(string partName, List<OrderLineItemRoute> routes)
    {
        PartName = partName;
        _routes = routes;
    }

    public int OrderLineItemId { get; internal set; }

    public DateTime CreatedDate { get; internal set; }

    public string PartName { get; internal set; }

    public Order Order { get; internal set; }

    public int OrderId { get; internal set; }

    public IReadOnlyCollection<OrderLineItemRoute> Routes => _routes.ToList().AsReadOnly();

    public static OrderLineItem Create(string partName, params OrderLineItemRoute[] routes)
    {
        var orderLineItem = new OrderLineItem(partName, routes.ToList());
        orderLineItem.AddDomainEvent(new OrderLineItemCreatedEvent(orderLineItem));
        return orderLineItem;
    }
}