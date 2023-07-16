using AsyncDemo.Domain.DomainEvents;

namespace AsyncDemo.Domain.Domain;

public class Order : Entity
{
    private ICollection<OrderLineItem> _orderLineItems = new List<OrderLineItem>();

    internal Order()
    {}

    internal Order(string assemblyName, List<OrderLineItem> orderLineItems)
    {
        AssemblyName = assemblyName;
        _orderLineItems = orderLineItems;
    }

    public int OrderId { get; internal set; }

    public string AssemblyName { get; internal set; }

    public IReadOnlyCollection<OrderLineItem> OrderLineItems => _orderLineItems.ToList().AsReadOnly();

    public void UpdateAssemblyName(string assemblyName)
    {
        AssemblyName = assemblyName;
        AddDomainEvent(new OrderEditedEvent(this));
    }

    public static Order Create(int tempId, string partName, params OrderLineItem[] orderLineItems)
    {
        var order = new Order(partName, orderLineItems.ToList());
        order.AddDomainEvent(new OrderCreatedEvent(tempId, order));
        return order;
    }
}