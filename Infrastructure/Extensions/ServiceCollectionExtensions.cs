using Microsoft.Extensions.Configuration;
using verticalSliceArchitecture.Data;
using verticalSliceArchitecture.Shared;
using Microsoft.EntityFrameworkCore;
using verticalSliceArchitecture.Infrastructure.Security;
using System.Reflection;
using FluentValidation;
using verticalSliceArchitecture.Features.Auth.Services.Implementation;
using verticalSliceArchitecture.Features.Auth.Services.Interface;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using verticalSliceArchitecture.Features.Upload.Services.Interface;
using verticalSliceArchitecture.Features.Upload.Services.Implementation;

namespace verticalSliceArchitecture.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUploadService, UploadService>();
            // Register MediatR - Scans the application for IRequestHandlers
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register FluentValidation - Automatically scans for validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("AppSettings:JWTSecret"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetValue<string>("AppSettings:JWTIssuer"),
                    ValidAudience = configuration.GetValue<string>("AppSettings:JWTAudience"),
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            return services;

        }
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            services.AddSingleton<IAppSettings>(sp =>
                sp.GetRequiredService<IOptions<AppSettings>>().Value);
            services.AddSingleton<IUserContext, UserContext>();
            services.AddHttpContextAccessor();
            services.AddApplicationServices();
            services.AddInfrastructureServices(configuration);
            services.AddControllers();
            services.AddJwtAuthentication(configuration);

            services.AddOpenApi();

            return services;
        }
    }
}
