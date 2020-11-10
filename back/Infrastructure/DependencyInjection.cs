using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("KayCafetDatabase")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //if (environment.IsEnvironment("Test"))
            //{
            //    services.AddIdentityServer()
            //        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
            //        {
            //            options.Clients.Add(new Client
            //            {
            //                ClientId = "Northwind.IntegrationTests",
            //                AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
            //                ClientSecrets = { new Secret("secret".Sha256()) },
            //                AllowedScopes = { "Northwind.WebUIAPI", "openid", "profile" }
            //            });
            //        }).AddTestUsers(new List<TestUser>
            //        {
            //            new TestUser
            //            {
            //                SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
            //                Username = "jason@northwind",
            //                Password = "Northwind1!",
            //                Claims = new List<Claim>
            //                {
            //                    new Claim(JwtClaimTypes.Email, "jason@northwind")
            //                }
            //            }
            //        });
            //}
            //else
            //{
                services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            //}

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
