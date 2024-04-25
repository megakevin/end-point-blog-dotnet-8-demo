using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using VehicleQuotes.IntegrationTests.Fixtures;
using VehicleQuotes.WebApi.Models;
using VehicleQuotes.WebApi.ResourceModels;

namespace GifBackend.IntegrationTests.WebApi.Controllers;

public class QuotesControllerTests : BaseIntegrationTestSuite
{
    public QuotesControllerTests(WebApplicationFactory<Program> factory, DatabaseFixture database) :
        base(factory, database) { }

    private async Task<Quote> CreateNewQuote(string year, string make, string model)
    {
        var bodyType = await _dbContext.BodyTypes.SingleAsync(bt => bt.Name == "Sedan");
        var size = await _dbContext.Sizes.SingleAsync(s => s.Name == "Compact");

        var quote = new Quote {
            Year = year,
            Make = make,
            Model = model,
            BodyTypeID = bodyType.ID,
            SizeID = size.ID,
            ItMoves = true,
            HasAllWheels = true,
            HasAlloyWheels = false,
            HasAllTires = true,
            HasKey = true,
            HasTitle = true,
            RequiresPickup = true,
            HasEngine = true,
            HasTransmission = true,
            HasCompleteInterior = false,
            OfferedQuote = 123,
            Message = "test_message",
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Quotes.Add(quote);

        _dbContext.SaveChanges();

        return quote;
    }

    private async Task<HttpResponseMessage> RegisterUser(HttpClient client)
    {
        var response = await client.PostAsJsonAsync(
            "/api/Users",
            new
            {
                UserName = "test_user_name",
                Password = "test_password",
                Email = "test@email.com"
            }
        );

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        return response;
    }

    private async Task<HttpResponseMessage> Login(HttpClient client)
    {
        var response = await client.PostAsJsonAsync(
            "/api/Users/BearerToken",
            new
            {
                UserName = "test_user_name",
                Password = "test_password"
            }
        );

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return response;
    }

    [Fact]
    public async Task GetQuotes_ReturnsOK()
    {
        // Arrange
        var client = CreateHttpClient();

        // Act
        var response = await client.GetAsync("/api/Quotes");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetQuotesSecure_ReturnsOK_WhenTheUserHasLoggedIn()
    {
        await WithTransaction(async () => {
            // Arrange
            var client = CreateHttpClient();

            await RegisterUser(client);
            var response = await Login(client);
            var authResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

            Assert.NotNull(authResponse);

            // Act
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/Quotes/Secure");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

            response = await client.SendAsync(requestMessage);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        });
    }

    [Fact]
    public async Task GetQuotesSecure_ReturnsUnauthorized_WhenTheUserHasNotLoggedIn()
    {
        // Arrange
        var client = CreateHttpClient();

        // Act
        var response = await client.GetAsync("/api/Quotes/Secure");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetQuotes_ReturnsTheQuotesFromTheDatabase()
    {
        await WithTransaction(async () => {
            // Arrange
            await CreateNewQuote("2024", "Toyota", "Corolla");
            await CreateNewQuote("2024", "Honda", "Civic");

            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/Quotes");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var quotes = await response.Content.ReadFromJsonAsync<IEnumerable<SubmittedQuoteRequest>>();

            Assert.NotNull(quotes);
            Assert.Equal(2, quotes.Count());

            Assert.Equal("2024", quotes.First().Year);
            Assert.Equal("Toyota", quotes.First().Make);
            Assert.Equal("Corolla", quotes.First().Model);

            Assert.Equal("2024", quotes.Last().Year);
            Assert.Equal("Honda", quotes.Last().Make);
            Assert.Equal("Civic", quotes.Last().Model);
        });
    }

    [Fact]
    public async Task PostQuote_CreatesANewQuoteRecord()
    {
        await WithTransaction(async () => {
            // Arrange
            var client = CreateHttpClient();

            Assert.Empty(_dbContext.Quotes);

            // Act
            var response = await client.PostAsJsonAsync(
                "/api/Quotes",
                new
                {
                    Year = "1990",
                    Make = "Toyota",
                    Model = "Corolla",
                    BodyType = "Sedan",
                    Size = "Compact",
                    ItMoves = true,
                    HasAllWheels = true,
                    HasAlloyWheels = false,
                    HasAllTires = true,
                    HasKey = true,
                    HasTitle = true,
                    RequiresPickup = false,
                    HasEngine = true,
                    HasTransmission = true,
                    HasCompleteInterior = true
                }
            );

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Single(_dbContext.Quotes);

            var quote = _dbContext.Quotes.First();
            Assert.NotNull(quote);
            Assert.Equal("1990", quote.Year);
            Assert.Equal("Toyota", quote.Make);
            Assert.Equal("Corolla", quote.Model);
        });
    }
}
