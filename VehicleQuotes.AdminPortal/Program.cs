using Microsoft.AspNetCore.DataProtection;
using VehicleQuotes.Core.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDataProtection().PersistKeysToFileSystem(
    new DirectoryInfo(
        builder.Configuration["AdminPortalDataProtectionKeysPath"] ??
            throw new InvalidOperationException("Config setting 'AdminPortalDataProtectionKeysPath' not found.")
    )
);

builder.Services.AddVehicleQuotesDbContext(
    builder.Configuration.GetConnectionString("VehicleQuotesContext") ??
        throw new InvalidOperationException("Connection string 'VehicleQuotesContext' not found.")
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.UsePathBase("/admin");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
