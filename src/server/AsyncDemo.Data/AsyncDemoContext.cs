using AsyncDemo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsyncDemo.Data;

public class AsyncDemoContext : DbContext
{
    private readonly IMediator _mediator;

    public AsyncDemoContext(DbContextOptions<AsyncDemoContext> opts, IMediator mediator) : base(opts)
    {
        _mediator = mediator;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = entities.SelectMany(x => x.Entity.DomainEvents).ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}