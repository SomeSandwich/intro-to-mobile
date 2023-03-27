using API.App.Extensions;
using Api.Modules.Post.Endpoints;
using Api.Modules.Post.Services;
using Asp.Versioning.Builder;

namespace Api.Modules.Post;

public class PostModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection builder)
    {
        builder.AddScoped<IPostService, PostService>();
        
        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version)
    {
        var postGroup = endpoints.MapGroup($"{GlobalConfig.BaseRoute}/post")
            .WithDisplayName("User")
            .WithApiVersionSet(version)
            .HasApiVersion(1);

        postGroup.GetAllPostEndpoint("/all");
        postGroup.GetPostEndpoint("/{postId}");
        
        return endpoints;
    }
}