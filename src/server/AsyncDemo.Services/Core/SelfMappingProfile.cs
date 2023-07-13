using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using AutoMapper;

namespace AsyncDemo.Services.Core;

public interface IMapFrom<TEntity>
{ }

public interface IHaveCustomMapping
{
    void CreateMappings(Profile configuration);
}

public sealed class Map
{
    public Map(Type source, Type destination)
    {
        Source = source;
        Destination = destination;
    }
    public Type Destination { get; }

    public Type Source { get; }
}

public abstract class SelfMappingProfile : Profile
{
    private readonly Assembly _assembly;

    protected SelfMappingProfile(Assembly assembly)
    {
        _assembly = assembly;

        LoadStandardMappings();
        LoadCustomMappings();
    }

    private void LoadCustomMappings()
    {
        var types = _assembly.GetExportedTypes();

        var mapsFrom = (
            from type in types
            where
                typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                !type.IsAbstract &&
                !type.IsInterface
            select (IHaveCustomMapping)Activator.CreateInstance(type)).ToList();

        foreach (var map in mapsFrom)
        {
            map.CreateMappings(this);
        }
    }

    private void LoadStandardMappings()
    {
        var types = _assembly.GetExportedTypes();

        var mapsFrom = types
            .SelectMany(
                x => x.GetInterfaces().Where(i => x.BaseType == null || !x.BaseType.GetInterfaces().Contains(i)),
                (type, instance) => new { type, instance }
            )
            .Where(x =>
                x.instance.IsGenericType &&
                x.instance.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                !x.type.IsAbstract &&
                !x.type.IsInterface
            )
            .Select(x => new Map(
                x.type.GetInterfaces().First().GetGenericArguments().First(),
                x.type
            ))
            .ToList();

        foreach (var map in mapsFrom)
        {
            CreateMap(map.Source, map.Destination).ReverseMap();
        }
    }
}