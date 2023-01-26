using CoreTemplate.App.Db;
using CoreTemplate.App.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreTemplate.App.Api.Extensions
{
    public static class UseDatabase
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //configure entity framecore
            services.AddDbContext<IdentityContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));            
            return services;
        }
    }
}
