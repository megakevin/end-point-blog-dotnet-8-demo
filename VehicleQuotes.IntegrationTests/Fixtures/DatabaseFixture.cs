using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VehicleQuotes.WebApi;

namespace VehicleQuotes.IntegrationTests.Fixtures;

// For more info about this class, check:
// https://learn.microsoft.com/en-us/ef/core/testing/testing-with-the-database#creating-seeding-and-managing-a-test-database
public class DatabaseFixture
{
    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    // Initializes the database specified in the connection string defined in
    // the appsettings.Test.json file.
    public DatabaseFixture()
    {
        // Tests can run in parallel. This lock is meant to make this cod
        //  thread safe.
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var dbContext = CreateDbContext())
                {
                    // Delete the database and recreate it.
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();
                }

                _databaseInitialized = true;
            }
        }
    }

    // Creates a new VehicleQuotesContext instance configured with the
    // connection string defined in the appsettings.Test.json file.
    public VehicleQuotesContext CreateDbContext()
    {
        // Load up the appsettings.Test.json file
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();

        // Create an instance of DbContextOptions using the connection string
        // defined in the appsettings.Test.json file.
        var options = new DbContextOptionsBuilder<VehicleQuotesContext>()
            .UseNpgsql(config.GetConnectionString("VehicleQuotesContext"))
            .UseSnakeCaseNamingConvention()
            .Options;

        var dbContext = new VehicleQuotesContext(options);

        return dbContext;
    }

    // Runs the given "test" within a database transaction created using the
    // given "dbContext". It rolls back the transaction when the "test" is done.
    public async Task WithTransaction(VehicleQuotesContext dbContext, Func<Task> test)
    {
        dbContext.Database.BeginTransaction();

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
            dbContext.Database.RollbackTransaction();
        }
    }
}
