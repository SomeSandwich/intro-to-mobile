using Api.Modules.GlobalTypes;
using Api.Modules.Post.Services;
using Api.Modules.Post.Type;
using Api.Modules.User.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Post.Endpoints;

public static class PostEndpoint
{
    #region Get

    public static IEndpointRouteBuilder GetAllPostEndpoint(
        this IEndpointRouteBuilder endpoints,
        string router
    )
    {
        endpoints.MapGet($"{router}", async (
            [FromServices] IPostService portService) =>
        {
            return Results.Ok();
        });

        return endpoints;
    }

    public static IEndpointRouteBuilder GetPostEndpoint(
        this IEndpointRouteBuilder endpoints,
        string router
    )
    {
        endpoints.MapGet($"{router}", async (
            [FromServices] IPostService portService,
            [FromRoute] int postId) =>
        {
            return Results.Ok();
        });

        return endpoints;
    }

    #endregion

    #region Post

    public static IEndpointRouteBuilder CreatePostEndpoint(
        this IEndpointRouteBuilder endpoints,
        string router
    )
    {
        endpoints.MapPost($"{router}", async (
            [FromServices] IUserService userService,
            [FromServices] IPostService portService,
            ReqCreatePost request) =>
        {
            bool userExist = await userService.IsUserExist(request.UserId);
            if (!userExist)
            {
                return Results.BadRequest(new ResFailure { Message = $"UserId:{request.UserId} not found" });
            }




            return Results.Ok();
        });

        return endpoints;
    }

    #endregion
}
