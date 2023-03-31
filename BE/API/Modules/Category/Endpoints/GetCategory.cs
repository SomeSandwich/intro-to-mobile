using Api.Modules.Category.Service;
using Api.Modules.Category.Type;
using Api.Modules.GlobalTypes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Category.Endpoints;

public static class GetCategory
{
    public static IEndpointRouteBuilder GetAllCategoriesEndPoint(
        this IEndpointRouteBuilder endpoints,
        string router
    )
    {
        endpoints.MapGet($"{router}", async (
            [FromServices] ICategoryService catService) =>
        {
            var categories = await catService.Get();

            return TypedResults.Ok(categories);
        });

        return endpoints;
    }

    public static IEndpointRouteBuilder GetOneCategoryEndpoint(
        this IEndpointRouteBuilder endpoints,
        string router
    )
    {
        endpoints.MapGet($"{router}", async (
            [FromServices] ICategoryService catService,
            [FromRoute] int id) =>
        {
            var categories = await catService.Get(id);

            return categories is null ? Results.BadRequest() : TypedResults.Ok(categories);
        });

        return endpoints;
    }

    public static IEndpointRouteBuilder CreateCategoryEndpoint(
        this IEndpointRouteBuilder endpoints,
        string router
    )
    {
        endpoints.MapPost($"{router}", async Task<IResult>(
            [FromServices] ICategoryService catService,
            [FromBody] CreateCategoryReq request) =>
        {
            if (!await catService.Create(request))
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(new ResSuccess());
        });

        return endpoints;
    }

    public static IEndpointRouteBuilder UpdateCategoryEndpoint(
        this IEndpointRouteBuilder endpoints,
        string router
    )
    {
        endpoints.MapPatch($"{router}", async Task<IResult>(
            [FromServices] ICategoryService catService,
            [FromRoute] int id,
            [FromBody] UpdateCategoryReq request) =>
        {
            if (!await catService.Update(id, request))
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(new ResSuccess());
        });

        return endpoints;
    }

    public static IEndpointRouteBuilder DeleteCategoryEndpoint(
        this IEndpointRouteBuilder endpoints,
        string router
    )
    {
        endpoints.MapDelete($"{router}", async Task<IResult>(
            [FromServices] ICategoryService catService,
            [FromRoute] int id) =>
        {
            if (!await catService.Delete(id))
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(new ResSuccess());
        });

        return endpoints;
    }
}
