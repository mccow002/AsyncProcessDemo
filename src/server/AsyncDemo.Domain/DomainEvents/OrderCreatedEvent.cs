﻿using AsyncDemo.Domain.Domain;
using MediatR;

namespace AsyncDemo.Domain.DomainEvents;

public record OrderCreatedEvent(int TempId, Order Order) : INotification;