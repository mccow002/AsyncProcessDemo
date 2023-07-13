using AsyncDemo.MessageProcessor;
using AsyncDemo.MessageProcessor.Services;
using AsyncDemo.Services;
using AsyncDemo.Services.Core;
using NLog.Extensions.Logging;
using Rebus.Config;
using Rebus.Retry.Simple;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((ctx, logging) =>
    {
        logging.ClearProviders();
        logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));
        logging.AddNLog(ctx.Configuration);
    })
    .ConfigureServices((ctx, services) =>
    {
        services.AddAsyncDemo(ctx.Configuration);

        services.AddHttpClient<IAsyncDemoApiClient, AsyncDemoApiClient>();
        services.AddTransient<IClientUpdater, ClientUpdater>();
        services.AddTransient<ICurrentConnectionIdService, ConnectionIdService>();

        services.AddRebus((cfg, provider) => cfg
            .Logging(x => x.MicrosoftExtensionsLogging(provider.GetService<ILoggerFactory>()))
            .Transport(x => x.UseRabbitMq(ctx.Configuration["RabbitMqConn"], ctx.Configuration["CommandQueue"]))
            .Options(o =>
            {
                o.LogPipeline(true);
                o.SimpleRetryStrategy(maxDeliveryAttempts: 3, secondLevelRetriesEnabled: true);
            })
        );

        services.AutoRegisterHandlersFromAssemblyOf<Program>();

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
