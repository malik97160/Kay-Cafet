using Domain.Entities;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using System.Collections.Generic;
using System.Security.Claims;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("KayCafetDatabase")));

            /*services.AddDefaultIdentity<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<KayCafetDbContext>();*/

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            /*if (environment.IsEnvironment("Test"))
            {
                services.AddIdentityServer()
                    .AddApiAuthorization<User, ApplicationDbContext>(options =>
                    {
                        options.Clients.Add(new Client
                        {
                            ClientId = "KayCafet.IntegrationTests",
                            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                            ClientSecrets = { new Secret("secret".Sha256()) },
                            AllowedScopes = { "KayCafet.WebUIAPI", "openid", "profile" }
                        });
                    }).AddTestUsers(new List<TestUser>
                    {
                        new TestUser
                        {
                            SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
                            Username = "malik.couchy@gmail.com",
                            Password = "malik971",
                            Claims = new List<Claim>
                            {
                                new Claim(JwtClaimTypes.Email, "malik.couchy@gmail.com")
                            }
                        }
                    });
            }
            else
            {*/
            services.AddIdentityServer()
                    .AddApiAuthorization<IdentityUser, ApplicationDbContext>();
            //}

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
