using MediatR;

namespace AsyncDemo.Domain;

public abstract class Entity
{
    private readonly List<INotification> _domainEvents = new();

    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    internal void AddDomainEvent(INotification @event) => _domainEvents.Add(@event);
}