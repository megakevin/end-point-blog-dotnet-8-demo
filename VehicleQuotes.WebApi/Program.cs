using System.Reflection;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VehicleQuotes.Core.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDataProtection().PersistKeysToFileSystem(
    new DirectoryInfo(
        builder.Configuration["WebApiDataProtectionKeysPath"] ??
            throw new InvalidOperationException("Config setting 'WebApiDataProtectionKeysPath' not found.")
    )
);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleQuotes", Version = "v1" });

    c.IncludeXmlComments(
        Path.Combine(
            AppContext.BaseDirectory,
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
        )
    );
});

builder.Services.AddVehicleQuotesDbContext(
    builder.Configuration.GetConnectionString("VehicleQuotesContext") ??
        throw new InvalidOperationException("Connection string 'VehicleQuotesContext' not found.")
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddCustomIdentity();
builder.Services.AddAuthenticationSchemes(builder.Configuration);

builder.Services.AddAppServices();
builder.Services.AddMailerServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleQuotes v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
