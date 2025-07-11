using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Integration;

/// <summary>
/// Provides integration tests for the <see cref="IOpportunityServices"/> implementation.
/// </summary>
/// <remarks>This class contains test methods to verify the behavior of the <see cref="IOpportunityServices"/>
/// interface and its interaction with the <see cref="IOpportunityRepository"/>. The tests ensure that the service
/// methods function correctly and adhere to expected business logic when interacting with the repository.</remarks>
public class OpportunityServicesIntegrationTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="IOpportunityRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="IOpportunityRepository"/>. It provides a controlled environment for testing without relying on the actual
    /// repository implementation.</remarks>
    private readonly Mock<IOpportunityRepository> _opportunityRepositoryMock;

    /// <summary>
    /// Provides access to operations related to opportunities.
    /// </summary>
    /// <remarks>This field is a read-only instance of <see cref="IOpportunityServices"/> used to perform 
    /// operations related to managing opportunities. It is intended for internal use within the class.</remarks>
    private readonly IOpportunityServices _opportunityServices;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpportunityServicesIntegrationTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary dependencies for testing the <see
    /// cref="OpportunityServices"/> class. It creates a mock implementation of <see cref="IOpportunityRepository"/> and
    /// uses it to initialize an instance of <see cref="OpportunityServices"/>.</remarks>
    public OpportunityServicesIntegrationTests()
    {
        _opportunityRepositoryMock = new Mock<IOpportunityRepository>();
        _opportunityServices = new OpportunityServices(_opportunityRepositoryMock.Object);
    }

    /// <summary>
    /// Tests that the <see cref="OpportunityServices.ListOpportunitiesAsync"/> method returns all available
    /// opportunities.
    /// </summary>
    /// <remarks>This test verifies that the service correctly retrieves opportunities from the repository and
    /// returns them as expected. It ensures that the repository's <c>ListAllAsync</c> method is called exactly once and
    /// that the returned result matches the expected data.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListOpportunitiesAsync_ShouldReturnAllOpportunities()
    {
        // Arrange
        var opportunities = new List<Opportunity>
        {
            new Opportunity(
                Description.Create("Software internship").Value!,
                Country.Create("Costa Rica").Value!
            )
        };

        _opportunityRepositoryMock
            .Setup(r => r.ListAllAsync())
            .ReturnsAsync(opportunities);

        // Act
        var result = await _opportunityServices.ListOpportunitiesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _opportunityRepositoryMock.Verify(r => r.ListAllAsync(), Times.Once);
    }

    /// <summary>
    /// Tests the <c>AddOpportunitiesAsync</c> method to ensure it calls the repository to add the specified opportunity
    /// and returns a success result.
    /// </summary>
    /// <remarks>This test verifies that the <c>AddOpportunitiesAsync</c> method correctly interacts with the
    /// repository and produces a successful result when valid inputs are provided. It also ensures that the repository
    /// method is called exactly once with the expected parameters.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddOpportunitiesAsync_ShouldCallRepositoryAndReturnSuccess()
    {
        // Arrange
        int careerId = 5;
        var opportunity = new Opportunity(
            Description.Create("Remote job").Value!,
            Country.Create("Germany").Value!
        );

        _opportunityRepositoryMock
            .Setup(r => r.AddInformationAsync(careerId, opportunity))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _opportunityServices.AddOpportunitiesAsync(careerId, opportunity);

        // Assert
        Assert.True(result.IsSuccess);
        _opportunityRepositoryMock.Verify(r => r.AddInformationAsync(careerId, opportunity), Times.Once);
    }
}
