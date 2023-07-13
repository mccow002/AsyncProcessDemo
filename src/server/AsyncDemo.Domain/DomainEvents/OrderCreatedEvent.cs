using AsyncDemo.Domain.Domain;
using MediatR;

namespace AsyncDemo.Domain.DomainEvents;

public record OrderCreatedEvent(Order Order) : INotification;