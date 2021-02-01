using App.Db;
using App.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Api.Extensions
{
    public static class UseDatabase
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //configure entity framecore
            services.AddDbContext<IdentityContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));            
            return services;
        }
    }
}
