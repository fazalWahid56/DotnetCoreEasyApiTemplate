using App.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace App.Api.Extensions
{
    public static class HealthChecks
    {

        public static IServiceCollection UseHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks().AddDbContextCheck<IdentityContext>();
            return services;
        }
    }
}
