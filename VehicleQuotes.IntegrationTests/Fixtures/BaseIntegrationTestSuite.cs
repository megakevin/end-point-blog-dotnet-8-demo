using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using VehicleQuotes.Core.Data;

namespace VehicleQuotes.IntegrationTests.Fixtures;

public abstract class BaseIntegrationTestSuite : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseFixture>
{
    private readonly WebApplicationFactory<Program> _factory;

    protected readonly DatabaseFixture _database;
    protected readonly VehicleQuotesContext _dbContext;

    public BaseIntegrationTestSuite(WebApplicationFactory<Program> factory, DatabaseFixture database)
    {
        _factory = factory;
        _database = database;

        _dbContext = database.CreateDbContext();
    }

    protected HttpClient CreateHttpClient()
    {
        return _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(_ => _dbContext);
            });
        })
        .CreateClient();
    }

    protected async Task WithTransaction(Func<Task> test)
    {
        _dbContext.Database.BeginTransaction();

        try
        {
            await test.Invoke();
        }
        catch
        {
            throw;
        }
        finally
        {
            _dbContext.Database.RollbackTransaction();
        }
    }
}
