using AsyncDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace AsyncDemo.Api.Services;

public class AppStartService : IHostedService
{
    private readonly IServiceProvider _provider;

    public AppStartService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AsyncDemoContext>();
        await context.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}