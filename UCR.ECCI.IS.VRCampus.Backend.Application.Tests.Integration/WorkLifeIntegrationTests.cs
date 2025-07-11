using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Integration;

/// <summary>
/// Provides integration tests for the <see cref="IWorkLifeServices"/> implementation.
/// </summary>
/// <remarks>This class contains tests that verify the behavior of the <see cref="IWorkLifeServices"/> interface
/// and its interaction with the <see cref="IWorkLifeRepository"/>. The tests ensure that the service methods function
/// correctly and adhere to expected business logic.</remarks>
public class WorkLifeServicesIntegrationTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="IWorkLifeRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="IWorkLifeRepository"/>. It is initialized as a readonly instance of <see cref="Mock{T}"/>.</remarks>
    private readonly Mock<IWorkLifeRepository> _workLifeRepositoryMock;

    /// <summary>
    /// Represents the work-life services used to manage and interact with work-life balance features.
    /// </summary>
    /// <remarks>This field is read-only and is intended to provide access to an implementation of <see
    /// cref="IWorkLifeServices"/>. It is typically used internally within the class to perform operations related to
    /// work-life services.</remarks>
    private readonly IWorkLifeServices _workLifeServices;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkLifeServicesIntegrationTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary dependencies for testing the integration of  <see
    /// cref="WorkLifeServices"/> by creating a mock implementation of <see cref="IWorkLifeRepository"/>.</remarks>
    public WorkLifeServicesIntegrationTests()
    {
        _workLifeRepositoryMock = new Mock<IWorkLifeRepository>();
        _workLifeServices = new WorkLifeServices(_workLifeRepositoryMock.Object);
    }

    /// <summary>
    /// Tests that the <see cref="WorkLifeServices.ListWorkLifeAsync"/> method returns all work-life records.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="WorkLifeServices.ListWorkLifeAsync"/> method correctly
    /// retrieves all work-life records from the repository and ensures the returned data matches the expected results.
    /// It also validates that the repository's <c>ListAllAsync</c> method is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListWorkLifeAsync_ShouldReturnAllWorkLifeRecords()
    {
        // Arrange
        var workLives = new List<WorkLife>
        {
            new WorkLife(
                Description.Create("Inclusive environment").Value!,
                Workers.Create(15).Value!,
                Workers.Create(20).Value!
            )
        };

        _workLifeRepositoryMock
            .Setup(r => r.ListAllAsync())
            .ReturnsAsync(workLives);

        // Act
        var result = await _workLifeServices.ListWorkLifeAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _workLifeRepositoryMock.Verify(r => r.ListAllAsync(), Times.Once);
    }

    /// <summary>
    /// Tests the <c>AddWorkLifeAsync</c> method to ensure that it calls the repository to add work-life information and
    /// returns a success result.
    /// </summary>
    /// <remarks>This test verifies that the <c>AddWorkLifeAsync</c> method correctly interacts with the
    /// repository and produces a successful result when valid inputs are provided. It also ensures that the repository
    /// method is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddWorkLifeAsync_ShouldCallRepositoryAndReturnSuccess()
    {
        // Arrange
        int careerId = 42;
        var workLife = new WorkLife(
            Description.Create("Remote work flexibility").Value!,
            Workers.Create(10).Value!,
            Workers.Create(25).Value!
        );

        _workLifeRepositoryMock
            .Setup(r => r.AddInformationAsync(careerId, workLife))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _workLifeServices.AddWorkLifeAsync(careerId, workLife);

        // Assert
        Assert.True(result.IsSuccess);
        _workLifeRepositoryMock.Verify(r => r.AddInformationAsync(careerId, workLife), Times.Once);
    }
}
