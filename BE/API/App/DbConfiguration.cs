using Api.Context;
using Microsoft.EntityFrameworkCore;

namespace API.App;

public static class DbConfiguration
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration config,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            services.AddDbContextPool<MobileDbContext>(options =>
            {
                options.UseNpgsql(config["ConnectionStrings:Url"]);
            });
        }
        else
        {
            services.AddDbContextPool<MobileDbContext>(options =>
            {
                options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTIONSTR"));
            });
        }
    }
}
