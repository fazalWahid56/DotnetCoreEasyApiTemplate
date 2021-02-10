using App.Api.Extensions;
using App.Db.Repositories;
using App.External.Email;
using App.Identity.Services;
using App.Services;
using App.Services.GeneralLeadger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace App.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDatabase(Configuration);
            services.UseIdentity();
            services.UseAuthentication(Configuration);
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddRazorPages();     
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App.Api", Version = "v1" });
            });
            services.UseHealthChecks();
            services.AddAutomapper();


            //service
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IGeneralLedgerService, GeneralLedgerService>();
            services.AddTransient<IVoucherService, VoucherService>();
            
            //repositories
            services.AddTransient<IChartOfAccountRepository , ChartOfAccountRepository>();
            services.AddTransient<IAccountNatureRepository, AccountNatureRepository>();
            services.AddTransient<IVoucherTypeRepository, VoucherTypeRepository>();
            services.AddTransient<IVoucherRepository, VoucherRepository>();
            services.AddTransient<IGeneralLedgerRepository, GeneralLedgerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/api/Errors/Error");

            if (env.IsDevelopment())
            {
              //  app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.Api v1"));
            }


            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
