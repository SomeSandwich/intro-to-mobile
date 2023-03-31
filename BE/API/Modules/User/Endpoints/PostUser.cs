using Api.Modules.User.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.User.Endpoints;

public static class PostUser
{
    public static IEndpointRouteBuilder PostUserEp(
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
