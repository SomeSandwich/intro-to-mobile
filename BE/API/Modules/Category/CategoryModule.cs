using API.App.Extensions;
using Api.Modules.Category.Endpoints;
using Api.Modules.Category.Service;
using Asp.Versioning.Builder;

namespace Api.Modules.Category;

public class CategoryModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection builder)
    {
        builder.AddScoped<ICategoryService, CategoryService>();

        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints, ApiVersionSet version)
    {
        var categoryGroup = endpoints.MapGroup($"{GlobalConfig.BaseRoute}/category")
            .WithDisplayName("Category")
            .WithTags("Category")
            .WithApiVersionSet(version)
            .HasApiVersion(1);

        categoryGroup.GetAllCategoriesEndPoint("/");
        categoryGroup.GetOneCategoryEndpoint("/{id}");

        categoryGroup.CreateCategoryEndpoint("/");

        categoryGroup.UpdateCategoryEndpoint("/{id}");

        categoryGroup.DeleteCategoryEndpoint("/{id}");

        return endpoints;
    }
}
