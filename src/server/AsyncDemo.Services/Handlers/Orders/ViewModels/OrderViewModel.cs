using AsyncDemo.Domain.Domain;
using AsyncDemo.Services.Core;
using AutoMapper;

namespace AsyncDemo.Services.Handlers.Orders.ViewModels;

public class OrderViewModel : IHaveCustomMapping
{
    public int OrderId { get; set; }

    public string AssemblyName { get; set; } = string.Empty;

    public int PartCount { get; set; }

    public void CreateMappings(Profile configuration)
    {
        configuration.CreateMap<Order, OrderViewModel>()
            .ForMember(x => x.PartCount, opts => opts.MapFrom(src => src.OrderLineItems.Count));
    }
}