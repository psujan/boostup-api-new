using Asp.Versioning;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Interfaces.Auth;
using Boostup.API.Mapper;
using Boostup.API.Repositories.Auth;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Boostup.API.Services
{
    public static class ServiceResgistration
    {
        public static IServiceCollection RegisterService(this IServiceCollection services, WebApplicationBuilder builder) 
        {
            // Identity , Token Provider  
            services.AddIdentity<User, IdentityRole>()
                   .AddRoles<IdentityRole>()
                   .AddTokenProvider<DataProtectorTokenProvider<User>>("Boostup")
                   .AddEntityFrameworkStores<ApplicationDbContext>()
                   .AddDefaultTokenProviders();

            //Api Versioning
            builder.Services.AddApiVersioning(options => {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            })
            .AddMvc() // This is needed for controllers
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            //Logging
            builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            // Dependency Injection For Interfaces
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            return services;
        }
    }
}
