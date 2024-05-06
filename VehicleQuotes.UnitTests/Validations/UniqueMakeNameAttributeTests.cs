using System.ComponentModel.DataAnnotations;
using Moq;
using VehicleQuotes.WebApi.Models;
using VehicleQuotes.WebApi.Repositories;
using VehicleQuotes.WebApi.Validation;

namespace GifBackend.UnitTests.Domain.Validation;

public class UniqueMakeNameAttributeTests
{
    private UniqueMakeNameAttribute BuildTestSubject() => new();

    private Mock<IMakeRepository> BuildMockRepository(Make exsitingMake)
    {
        var mockRepository = new Mock<IMakeRepository>();

        mockRepository
            .Setup(m => m.FindByName(It.IsAny<string>()))
            .Returns(exsitingMake);

        return mockRepository;
    }

    protected static Mock<IServiceProvider> BuildMockServiceProvider(IMakeRepository mockRepository)
    {
        var mockServiceProvider = new Mock<IServiceProvider>();

        mockServiceProvider
            .Setup(m => m.GetService(typeof(IMakeRepository)))
            .Returns(mockRepository);

        return mockServiceProvider;
    }

    [Fact]
    public void GetValidationResult_ReturnsFailure_WhenAnotherCouponExistsWithTheGivenName()
    {
        // Arrange
        var attribute = BuildTestSubject();

        var existingMake = new Make() { ID = 10, Name = "test_name" };
        var makeToValidate = new Make() { ID = 20, Name = "test_name" };

        var mockRepository = BuildMockRepository(existingMake);
        var mockServiceProvider = BuildMockServiceProvider(mockRepository.Object);

        var context = new ValidationContext(makeToValidate, mockServiceProvider.Object, null);

        // Act
        var result = attribute.GetValidationResult(makeToValidate, context);

        // Assert
        Assert.NotEqual(ValidationResult.Success, result);
        Assert.Equal("The Make name 'test_name' is already in use.", result?.ErrorMessage);
    }
}
