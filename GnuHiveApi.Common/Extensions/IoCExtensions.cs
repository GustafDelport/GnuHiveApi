using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GnuHiveApi.Common.Extensions;

public static class IoCExtensions
{
    public static void RegisterByConvention(
        this IServiceCollection services,
        Assembly assembly,
        string regexPattern,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var regex = new Regex(regexPattern);

        assembly
            .ExportedTypes
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => regex.IsMatch(t.Name))
            .SelectMany(t => t.GetInterfaces(), (c, i) => new { Class = c, Interface = i })
            .ToList()
            .ForEach(x => services.Add(new ServiceDescriptor(x.Interface, x.Class, lifetime)));
    }
    
    public static void ReplaceScoped<TInterface, TConcrete>(
        this IServiceCollection services
    )
        where TConcrete : class
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(TInterface), typeof(TConcrete), ServiceLifetime.Scoped);
        services.Replace(serviceDescriptor);
    }
    
    public static void ReplaceSingleton<TInterface, TConcrete>(
        this IServiceCollection services
    )
        where TConcrete : class
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(TInterface), typeof(TConcrete), ServiceLifetime.Singleton);
        services.Replace(serviceDescriptor);
    }
}