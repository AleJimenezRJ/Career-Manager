using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Integration;

public class EnterpriseServicesIntegrationTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="IEnterpriseRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="IEnterpriseRepository"/>. It provides a controlled environment for testing without relying on the actual
    /// repository implementation.</remarks>
    private readonly Mock<IEnterpriseRepository> _enterpriseRepositoryMock;

    /// <summary>
    /// Represents the enterprise services used to perform operations related to business processes.
    /// </summary>
    /// <remarks>This field is intended to store a reference to an implementation of the <see
    /// cref="IEnterpriseServices"/> interface, which provides methods and properties for managing enterprise-level
    /// functionality. It is read-only and must be initialized during object construction.</remarks>
    private readonly IEnterpriseServices _enterpriseServices;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnterpriseServicesIntegrationTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary dependencies for testing the <see
    /// cref="EnterpriseServices"/> class by creating a mock implementation of <see
    /// cref="IEnterpriseRepository"/>.</remarks>
    public EnterpriseServicesIntegrationTests()
    {
        _enterpriseRepositoryMock = new Mock<IEnterpriseRepository>();
        _enterpriseServices = new EnterpriseServices(_enterpriseRepositoryMock.Object);
    }

    /// <summary>
    /// Tests that the <see cref="EnterpriseServices.ListEnterpriseAsync"/> method returns all enterprises.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="EnterpriseServices.ListEnterpriseAsync"/> method
    /// correctly retrieves  all enterprises from the repository and ensures the returned result is not null and
    /// contains the expected number of items. It also validates that the repository's <see
    /// cref="IEnterpriseRepository.ListAllAsync"/> method is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListEnterpriseAsync_ShouldReturnAllEnterprises()
    {
        // Arrange
        var enterprises = new List<Enterprise>
        {
            new Enterprise(Description.Create("Software company").Value!, EntityName.Create("TechCorp").Value!, Country.Create("Costa Rica").Value!)
        };

        _enterpriseRepositoryMock
            .Setup(r => r.ListAllAsync())
            .ReturnsAsync(enterprises);

        // Act
        var result = await _enterpriseServices.ListEnterpriseAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _enterpriseRepositoryMock.Verify(r => r.ListAllAsync(), Times.Once);
    }

    /// <summary>
    /// Tests the <c>AddEnterpriseAsync</c> method to ensure it calls the repository and returns a success result.  
    /// </summary>
    /// <remarks>This test verifies that the <c>AddEnterpriseAsync</c> method interacts with the repository as
    /// expected and  correctly handles the success scenario. It ensures the repository's <c>AddInformationAsync</c>
    /// method is  called with the specified parameters and that the result indicates success.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddEnterpriseAsync_ShouldCallRepositoryAndReturnSuccess()
    {
        // Arrange
        int careerId = 1;
        var enterprise = new Enterprise(
            Description.Create("Tech support").Value!,
            EntityName.Create("Innova").Value!,
            Country.Create("Costa Rica").Value!
        );

        _enterpriseRepositoryMock
            .Setup(r => r.AddInformationAsync(careerId, enterprise))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _enterpriseServices.AddEnterpriseAsync(careerId, enterprise);

        // Assert
        Assert.True(result.IsSuccess);
        _enterpriseRepositoryMock.Verify(r => r.AddInformationAsync(careerId, enterprise), Times.Once);
    }
}
