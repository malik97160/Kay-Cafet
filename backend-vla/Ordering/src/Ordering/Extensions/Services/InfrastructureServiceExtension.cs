namespace Ordering.Extensions.Services;

using Ordering.Databases;
using Ordering.Resources;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        // DbContext -- Do Not Delete
        if (env.IsEnvironment(LocalConfig.FunctionalTestingEnvName) || env.IsDevelopment())
        {
            services.AddDbContext<OrdersDbContext>(options =>
                options.UseInMemoryDatabase($"Ordering"));
        }
        else
        {
            services.AddDbContext<OrdersDbContext>(options =>
                options.UseNpgsql(
                    Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "placeholder-for-migrations",
                    builder => builder.MigrationsAssembly(typeof(OrdersDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());
        }

        // Auth -- Do Not Delete
    }
}
