using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using CommandLine;
using VehicleQuotes.WebApi;
using VehicleQuotes.WebApi.Startup;
using VehicleQuotes.WebApi.Services;
using VehicleQuotes.CreateUser;

void Run(CliOptions options)
{
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .Build();

    var builder = Host.CreateDefaultBuilder(args)
        .UseContentRoot(AppContext.BaseDirectory)
        .ConfigureServices(services => {
            services.AddVehicleQuotesDbContext(
                configuration.GetConnectionString("VehicleQuotesContext") ??
                    throw new InvalidOperationException("Connection string 'VehicleQuotesContext' not found.")
            );

            services.AddCustomIdentity();

            services.AddTransient<UserCreator>();
        })
        .Build();

    var userCreator = builder.Services.GetRequiredService<UserCreator>();
    userCreator.Run(options.Username, options.Email, options.Password);
}

Parser.Default
    .ParseArguments<CliOptions>(args)
    .WithParsed(Run);
