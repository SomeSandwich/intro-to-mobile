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
            services.AddDbContextPool<MobileDbContext>(opt =>
            {
                opt.UseNpgsql(config["ConnectionStrings:Url"]);
                opt.EnableSensitiveDataLogging(true);
            });
        }
        else
        {
            services.AddDbContextPool<MobileDbContext>(opt =>
            {
                opt.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTIONSTR"));
            });
        }
    }
}
