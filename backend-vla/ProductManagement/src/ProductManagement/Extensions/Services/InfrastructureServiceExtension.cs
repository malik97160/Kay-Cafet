namespace ProductManagement.Extensions.Services;

using ProductManagement.Databases;
using ProductManagement.Resources;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        // DbContext -- Do Not Delete
        if (env.IsEnvironment(LocalConfig.FunctionalTestingEnvName) || env.IsDevelopment())
        {
            services.AddDbContext<ProductsDbContext>(options =>
                options.UseInMemoryDatabase($"ProductManagement"));
        }
        else
        {
            services.AddDbContext<ProductsDbContext>(options =>
                options.UseNpgsql(
                    Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "placeholder-for-migrations",
                    builder => builder.MigrationsAssembly(typeof(ProductsDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());
        }

        // Auth -- Do Not Delete
    }
}
