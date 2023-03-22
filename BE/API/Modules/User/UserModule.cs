using Api.Configurations.Extentions;
using Api.Modules.User.Endpoints;
using Asp.Versioning.Builder;

namespace Api.Modules.User;

public class UserModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection builder)
    {
        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version)
    {
        var userGroup = endpoints.MapGroup($"{GlobalConfig.BaseRoute}/user")
            .WithDisplayName("User")
            .WithApiVersionSet(version)
            .HasApiVersion(1);

        userGroup.GetUserEp($"/");
        
        return endpoints;
    }
}