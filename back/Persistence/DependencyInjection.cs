using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KayCafetDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("KayCafetDatabase")));

            services.AddScoped<IKayCafetDbContext>(provider => provider.GetService<KayCafetDbContext>());
            return services;
        }
    }
}
