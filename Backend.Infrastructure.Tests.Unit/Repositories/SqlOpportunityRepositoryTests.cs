using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using Xunit;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Unit.Repositories;

/// <summary>
/// Provides unit tests for the <see cref="SqlOpportunityRepository"/> to ensure correct behavior when managing opportunity data.
/// </summary>
public class SqlOpportunityRepositoryTests
{
    /// <summary>
    /// A mock list of opportunities used as the in-memory data store for testing.
    /// </summary>
    private readonly List<Opportunity> _opportunityData;

    /// <summary>
    /// Mocked DbSet representing the <see cref="Opportunity"/> entity set.
    /// </summary>
    private readonly Mock<DbSet<Opportunity>> _mockOpportunities;

    /// <summary>
    /// Mocked instance of the VR Campus database context.
    /// </summary>
    private readonly Mock<VRCampusDatabaseContext> _mockContext;

    /// <summary>
    /// The repository under test.
    /// </summary>
    private readonly SqlOpportunityRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlOpportunityRepositoryTests"/> class and sets up mocks.
    /// </summary>
    public SqlOpportunityRepositoryTests()
    {
        _opportunityData = new List<Opportunity>
{
    new Opportunity(
        Description.FromDatabase("Internship at Intel"),
        Country.FromDatabase("Costa Rica")
    ),
    new Opportunity(
        Description.FromDatabase("Scholarship for Women in Tech"),
        Country.FromDatabase("United States")
    )
};

        _mockOpportunities = _opportunityData.AsQueryable().BuildMockDbSet();

        _mockContext = new Mock<VRCampusDatabaseContext>(MockBehavior.Loose);
        _mockContext.Setup(c => c.Opportunities).Returns(_mockOpportunities.Object);

        _repository = new SqlOpportunityRepository(_mockContext.Object);
    }

    /// <summary>
    /// Tests that <see cref="SqlOpportunityRepository.ListAllAsync"/> returns all stored opportunities when present.
    /// </summary>
    [Fact]
    public async Task ListAllAsync_ShouldReturnAllOpportunities_WhenOpportunitiesExist()
    {
        var result = await _repository.ListAllAsync();

        result.Should().BeEquivalentTo(_opportunityData);
        _mockContext.Verify(c => c.Opportunities, Times.Once);
    }

    /// <summary>
    /// Tests that <see cref="SqlOpportunityRepository.ListAllAsync"/> returns an empty collection when no opportunities exist.
    /// </summary>
    [Fact]
    public async Task ListAllAsync_ShouldReturnEmpty_WhenNoOpportunitiesExist()
    {
        var emptyList = new List<Opportunity>();
        var mockEmptyDbSet = emptyList.AsQueryable().BuildMockDbSet();
        _mockContext.Setup(c => c.Opportunities).Returns(mockEmptyDbSet.Object);

        var repository = new SqlOpportunityRepository(_mockContext.Object);

        var result = await repository.ListAllAsync();

        result.Should().BeEmpty();
        _mockContext.Verify(c => c.Opportunities, Times.Once);
    }
}
