
using CoreTemplate.App.Db.Repositories;
using CoreTemplate.App.External.Email;
using CoreTemplate.App.Identity.Services;
using CoreTemplate.App.Utilites.ClaimPrinciple;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.UseIdentity();
builder.Services.UseAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreTemplate.App.Api", Version = "v1" });
});
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.UseHealthChecks();
builder.Services.AddAutomapper();


//helpers
builder.Services.AddScoped<IClaimPrinciple, ClaimPrinciple>();


//service
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<IAccountService, AccountService>();


//repositories
builder.Services.AddTransient<IAccountNatureRepository, AccountNatureRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseExceptionHandler("/api/Errors/Error");

if (app.Environment.IsDevelopment())
{
    //  app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "app.Api v1"));
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

app.Run();

