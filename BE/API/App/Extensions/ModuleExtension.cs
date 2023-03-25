using System;
using System.Collections.Generic;
using System.Linq;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations.Extentions;

public interface IModule
{
    IServiceCollection RegisterModule(IServiceCollection builder);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version);
}

public static class ModuleExtensions
{
    // this could also be added into the DI container
    static readonly List<IModule> registeredModules = new List<IModule>();
    public static IServiceCollection RegisterModules(this IServiceCollection services)
    {
        var modules = DiscoverModules();
        foreach (var module in modules)
        {
            module.RegisterModule(services);
            registeredModules.Add(module);
        }
        return services;
    }
    public static WebApplication MapEndpoints(this WebApplication app, ApiVersionSet version)
    {
        foreach (var module in registeredModules)
        {
            module.MapEndpoints(app, version);
        }
        return app;
    }
    private static IEnumerable<IModule> DiscoverModules()
    {
        return typeof(IModule).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
    }
}