using Asp.Versioning;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Interfaces.Auth;
using Boostup.API.Interfaces.Employee;
using Boostup.API.Mapper;
using Boostup.API.Repositories.Auth;
using Boostup.API.Repositories.Employee;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;


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

            // Identity Options For Password
            // For employee onboarding password is randomly generated
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
            });

            // Add Default Authentication Scheme using Bearer Tokens
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
             {
                 //options.RequireHttpsMetadata = false;
                 //options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                 };
                 options.UseSecurityTokenValidators = true;
             });

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
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
