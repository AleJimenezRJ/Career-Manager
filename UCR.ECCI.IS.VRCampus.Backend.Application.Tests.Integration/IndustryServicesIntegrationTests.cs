using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Integration;

/// <summary>
/// Provides integration tests for the <see cref="IIndustryServices"/> implementation.
/// </summary>
/// <remarks>This class is designed to test the behavior of the <see cref="IIndustryServices"/> interface and its
/// interaction with the <see cref="IIndustryRepository"/> mock. It includes tests for listing industries and adding
/// industry information, ensuring the correctness of the service layer.</remarks>
public class IndustryServicesIntegrationTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="IIndustryRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="IIndustryRepository"/>. It is initialized as a readonly instance of <see cref="Mock{T}"/>.</remarks>
    private readonly Mock<IIndustryRepository> _industryRepositoryMock;

    /// <summary>
    /// Provides access to industry-related services.
    /// </summary>
    /// <remarks>This field is intended to store a reference to an implementation of the <see
    /// cref="IIndustryServices"/> interface. It is used internally to perform operations related to industry-specific
    /// functionality.</remarks>
    private readonly IIndustryServices _industryServices;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndustryServicesIntegrationTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary dependencies for testing the integration of  <see
    /// cref="IndustryServices"/> with a mocked implementation of <see cref="IIndustryRepository"/>.</remarks>
    public IndustryServicesIntegrationTests()
    {
        _industryRepositoryMock = new Mock<IIndustryRepository>();
        _industryServices = new IndustryServices(_industryRepositoryMock.Object);
    }

    /// <summary>
    /// Tests that the <see cref="IndustryServices.ListIndustriesAsync"/> method returns all industries.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="IndustryServices.ListIndustriesAsync"/> method
    /// correctly retrieves all industries from the repository and returns them as a result. It ensures that the
    /// repository's <see cref="IIndustryRepository.ListAllAsync"/> method is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListIndustriesAsync_ShouldReturnAllIndustries()
    {
        // Arrange
        var industries = new List<Industry>
        {
            new Industry(
                Description.Create("Manufacturing industry").Value!,
                EntityName.Create("AutoCorp").Value!,
                csRelated: false
            )
        };

        _industryRepositoryMock
            .Setup(r => r.ListAllAsync())
            .ReturnsAsync(industries);

        // Act
        var result = await _industryServices.ListIndustriesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _industryRepositoryMock.Verify(r => r.ListAllAsync(), Times.Once);
    }

    /// <summary>
    /// Tests the <c>AddIndustriesAsync</c> method to ensure it calls the repository and returns a success result.
    /// </summary>
    /// <remarks>This test verifies that the <c>AddIndustriesAsync</c> method correctly interacts with the
    /// repository by calling <c>AddInformationAsync</c> with the expected parameters and returns a successful result.
    /// It also ensures the repository method is invoked exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddIndustriesAsync_ShouldCallRepositoryAndReturnSuccess()
    {
        // Arrange
        int careerId = 2;
        var industry = new Industry(
            Description.Create("Cybersecurity").Value!,
            EntityName.Create("CyberSec Inc").Value!,
            csRelated: true
        );

        _industryRepositoryMock
            .Setup(r => r.AddInformationAsync(careerId, industry))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _industryServices.AddIndustriesAsync(careerId, industry);

        // Assert
        Assert.True(result.IsSuccess);
        _industryRepositoryMock.Verify(r => r.AddInformationAsync(careerId, industry), Times.Once);
    }
}
