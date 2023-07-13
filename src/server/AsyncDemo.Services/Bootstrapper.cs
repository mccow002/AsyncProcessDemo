using AsyncDemo.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncDemo.Services;

public static class Bootstrapper
{
    public static IServiceCollection AddAsyncDemo(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AsyncDemoContext>(
            opts => opts.UseSqlServer(config["DbConn"])
        );

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IServicesMarker>());
        services.AddValidatorsFromAssemblyContaining<IServicesMarker>();
        services.AddAutoMapper(cfg => cfg.AddProfile<AsyncDemoMaps>());

        return services;
    }
}