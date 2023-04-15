using API.Services;
using Minio.AspNetCore;

namespace API.App;

public static class ServiceRegister
{
    public static void RegisterServices(this IServiceCollection builder)
    {
        builder.AddScoped<IAccountService, AccountService>();
        builder.AddScoped<ICategoryService, CategoryService>();
        builder.AddScoped<IMinioFileService, MinioFileService>();
        builder.AddScoped<IPostService, PostService>();
        builder.AddScoped<IUserService, UserService>();
        builder.AddScoped<ICartService, CartService>();
        builder.AddScoped<IReportService, ReportService>();
        builder.AddScoped<IOrderService, OrderService>();
        builder.AddScoped<IRateService, RateService>();
        builder.AddScoped<ICommentService, CommentService>();
    }
}
