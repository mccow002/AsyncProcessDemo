using AsyncDemo.Domain.Domain;
using MediatR;

namespace AsyncDemo.Domain.DomainEvents;

public record OrderEditedEvent(Order Order) : INotification;