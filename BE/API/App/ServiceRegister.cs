// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using API.Services;

namespace API.App;

public static class ServiceRegister
{
    public static void RegisterServices(this IServiceCollection builder)
    {
        builder.AddScoped<IAccountService, AccountService>();
        builder.AddScoped<ICategoryService, CategoryService>();
        builder.AddScoped<IPostService, PostService>();
        builder.AddScoped<IUserService, UserService>();
        builder.AddScoped<ICartService, CartService>();
        builder.AddScoped<IReportService, ReportService>();
        builder.AddScoped<IOrderService, OrderService>();
    }
}
