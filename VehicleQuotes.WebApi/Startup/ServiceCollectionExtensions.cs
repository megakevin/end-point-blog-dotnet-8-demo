using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VehicleQuotes.RazorTemplates.Services;
using VehicleQuotes.WebApi.Configuration;
using VehicleQuotes.WebApi.Repositories;

namespace VehicleQuotes.WebApi.Startup;

public static class ServiceCollectionExtensions
{
    public static void AddVehicleQuotesDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<VehicleQuotesContext>(options =>
            options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .EnableSensitiveDataLogging()
        );
    }

    public static void AddCustomIdentity(this IServiceCollection services)
    {
        services
            .AddIdentityCore<IdentityUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<VehicleQuotesContext>();
    }

    public static void AddAuthenticationSchemes(this IServiceCollection services, ConfigurationManager config)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = config["Jwt:Audience"] ??
                        throw new InvalidOperationException("Config setting 'Jwt:Audience' not found."),

                    ValidIssuer = config["Jwt:Issuer"] ??
                        throw new InvalidOperationException("Config setting 'Jwt:Issuer' not found."),

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            config["Jwt:Key"] ??
                                throw new InvalidOperationException("Config setting 'Jwt:Key' not found.")
                        )
                    )
                };
            })
            .AddApiKey();
    }

    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<Services.QuoteService>();
        services.AddScoped<Services.ApiKeyService>();
        services.AddScoped<Services.JwtService>();
        services.AddScoped<IMakeRepository, MakeRepository>();
    }

    public static void AddMailerServices(this IServiceCollection services, ConfigurationManager config)
    {
        services.Configure<MailSettings>(config.GetSection("MailSettings"));
        services.AddScoped<Services.QuoteGeneratedMailer>();
        services.AddMvcCore().AddRazorViewEngine();
        services.AddTransient<IRazorViewRenderer, RazorViewRenderer>();
        services.AddTransient<Services.IMailer, Services.Mailer>();
    }
}