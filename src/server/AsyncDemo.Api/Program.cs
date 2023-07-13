using AsyncDemo.Api.Hubs;
using AsyncDemo.Api.Services;
using AsyncDemo.Services;
using NLog.Web;
using Rebus.Config;
using Rebus.Pipeline;
using Rebus.Routing.TypeBased;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(x => x.AddDefaultPolicy(p => p
    .SetIsOriginAllowed(_ => true)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
));

builder.Services.AddAsyncDemo(builder.Configuration);
builder.Services.AddHostedService<AppStartService>();

builder.Services.AddRebus((cfg, provider) => cfg
    .Logging(x => x.MicrosoftExtensionsLogging(provider.GetService<ILoggerFactory>()))
    .Transport(x => x.UseRabbitMqAsOneWayClient(builder.Configuration["RabbitMqConn"]))
    .Routing(x => x.TypeBased().MapAssemblyOf<IServicesMarker>(builder.Configuration["CommandQueue"]))
    .Options(o =>
    {
        o.LogPipeline(true);

        o.Decorate<IPipeline>(c =>
        {
            var httpContext = provider.GetRequiredService<IHttpContextAccessor>();
            var connectionIdStep = new AddConnectionIdOutgoingStep(httpContext);

            var pipeline = c.Get<IPipeline>();
            return new PipelineStepConcatenator(pipeline)
                .OnSend(connectionIdStep, PipelineAbsolutePosition.Back);
        });

        
    })
);

builder.Logging.AddNLogWeb();

builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.MapHub<AsyncDemoHub>("/signalr");

app.Run();