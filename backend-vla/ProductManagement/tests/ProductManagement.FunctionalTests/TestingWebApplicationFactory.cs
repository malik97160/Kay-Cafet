
namespace ProductManagement.FunctionalTests;

using ProductManagement.Databases;
using ProductManagement.Resources;
using ProductManagement;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class TestingWebApplicationFactory : WebApplicationFactory<Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(LocalConfig.FunctionalTestingEnvName);

        builder.ConfigureServices(services =>
        {
            // Create a new service provider.
            var provider = services.BuildServiceProvider();

            // Add a database context (ProductsDbContext) using an in-memory database for testing.
            services.AddDbContext<ProductsDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseInternalServiceProvider(provider);
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context (ProductsDbContext).
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ProductsDbContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();
            }
        });
    }
}