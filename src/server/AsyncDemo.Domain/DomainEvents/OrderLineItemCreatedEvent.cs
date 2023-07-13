using AsyncDemo.Domain.Domain;
using MediatR;

namespace AsyncDemo.Domain.DomainEvents;

public record OrderLineItemCreatedEvent(OrderLineItem OrderLineItem) : INotification;