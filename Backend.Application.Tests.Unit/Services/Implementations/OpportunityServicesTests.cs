using FluentAssertions;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Unit.Services.Implementations;

/// <summary>
/// Provides unit tests for the <see cref="OpportunityServices"/> class.
/// Validates the service layer logic by mocking interactions with <see cref="IOpportunityRepository"/>.
/// </summary>
public class OpportunityServicesTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="IOpportunityRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="IOpportunityRepository"/>. It allows controlled testing scenarios by providing predefined responses and
    /// behaviors.</remarks>
    private readonly Mock<IOpportunityRepository> _opportunityRepositoryMock;

    /// <summary>
    /// Represents the service used to perform operations related to opportunities.
    /// </summary>
    /// <remarks>This field is intended for internal use within the class and should not be accessed directly.
    /// It provides functionality for managing and interacting with opportunities.</remarks>
    private readonly OpportunityServices _serviceUnderTest;

    /// <summary>
    /// Represents an opportunity associated with the current context.
    /// </summary>
    /// <remarks>This field is read-only and intended for internal use within the class. It stores the details
    /// of an opportunity, which may include information such as its status, associated entities, or other relevant
    /// data.</remarks>
    private readonly Opportunity _opportunity;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpportunityServicesTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary mocks and test data for unit testing the <see
    /// cref="OpportunityServices"/> class. It creates a strict mock of <see cref="IOpportunityRepository"/> and
    /// initializes the service under test with the mock object. Additionally, it prepares a sample <see
    /// cref="Opportunity"/> instance for use in test scenarios.</remarks>
    public OpportunityServicesTests()
    {
        _opportunityRepositoryMock = new Mock<IOpportunityRepository>(MockBehavior.Strict);
        _serviceUnderTest = new OpportunityServices(_opportunityRepositoryMock.Object);

        _opportunity = new Opportunity
        (
            Description.FromDatabase("Remote frontend development opportunities."),
            Country.FromDatabase("Germany")
        );
    }

    /// <summary>
    /// Tests that the <see cref="ServiceUnderTest.ListOpportunitiesAsync"/> method returns a list of opportunities.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ServiceUnderTest.ListOpportunitiesAsync"/> method
    /// correctly retrieves all opportunities from the repository and returns them as a list. It ensures that the
    /// repository's  <see cref="IOpportunityRepository.ListAllAsync"/> method is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListOpportunitiesAsync_ReturnsOpportunitiesList()
    {
        var opportunities = new List<Opportunity> { _opportunity };

        _opportunityRepositoryMock
            .Setup(repo => repo.ListAllAsync())
            .ReturnsAsync(opportunities);

        var result = await _serviceUnderTest.ListOpportunitiesAsync();

        result.Should().ContainSingle().Which.Should().Be(_opportunity);
        _opportunityRepositoryMock.Verify(repo => repo.ListAllAsync(), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="IOpportunityService.AddOpportunitiesAsync(int, Opportunity)"/> method  returns a
    /// successful result when the repository successfully adds the opportunity.
    /// </summary>
    /// <remarks>This test verifies the interaction between the service and the repository, ensuring that the 
    /// repository's <see cref="IOpportunityRepository.AddInformationAsync(int, Opportunity)"/> method  is called
    /// exactly once with the expected parameters.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddOpportunitiesAsync_ReturnsSuccess_WhenRepositoryReturnsSuccess()
    {
        int careerInternalId = 15;
        var expected = Result.Success();

        _opportunityRepositoryMock
            .Setup(repo => repo.AddInformationAsync(careerInternalId, _opportunity))
            .ReturnsAsync(expected);

        var result = await _serviceUnderTest.AddOpportunitiesAsync(careerInternalId, _opportunity);

        result.Should().Be(expected);
        _opportunityRepositoryMock.Verify(repo => repo.AddInformationAsync(careerInternalId, _opportunity), Times.Once);
    }
}
