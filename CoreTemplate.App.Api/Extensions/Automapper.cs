using CoreTemplate.App.Services.AutoMapper;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.App.Api.Extensions
{
    public static class Automapper
    {

        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            //configure entity framecore
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }

        
    }
}
