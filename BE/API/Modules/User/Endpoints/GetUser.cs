using Api.Modules.User.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Api.Modules.User.Endpoints;

public static class GetUser
{
    public static IEndpointRouteBuilder GetUserEp(
        this IEndpointRouteBuilder endpoints,
        string router
        )
    {

        endpoints.MapGet($"{router}", async (
            [FromServices] IUserService userService) =>
        {
            return Results.Ok();
        });
        
        return endpoints;
    }
}