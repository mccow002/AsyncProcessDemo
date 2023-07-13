using System.Reflection;
using AsyncDemo.Services.Core;

namespace AsyncDemo.Services;

public class AsyncDemoMaps : SelfMappingProfile
{
    public AsyncDemoMaps() : base(typeof(IServicesMarker).Assembly)
    { }
}