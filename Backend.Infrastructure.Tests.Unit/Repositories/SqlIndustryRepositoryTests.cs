using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Unit.Repositories;

/// <summary>
/// Provides unit tests for the <see cref="SqlIndustryRepository"/> class to verify correct database access behavior for <see cref="Industry"/> entities.
/// </summary>
public class SqlIndustryRepositoryTests
{
    /// <summary>
    /// In-memory industry data used to simulate the database table contents.
    /// </summary>
    private readonly List<Industry> _industryData;

    /// <summary>
    /// Mocked <see cref="DbSet{Industry}"/> representing the EF Core DbSet for industries.
    /// </summary>
    private readonly Mock<DbSet<Industry>> _mockIndustries;

    /// <summary>
    /// Mocked database context to isolate repository logic from the actual database.
    /// </summary>
    private readonly Mock<VRCampusDatabaseContext> _mockContext;

    /// <summary>
    /// The repository under test.
    /// </summary>
    private readonly SqlIndustryRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlIndustryRepositoryTests"/> class.
    /// </summary>
    public SqlIndustryRepositoryTests()
    {
        _industryData = new List<Industry>
        {
            new Industry(Description.FromDatabase("Tech Sector"), EntityName.FromDatabase("Microsoft"), true)
            {
                WorkInformationInternalId = 1
            },
            new Industry(Description.FromDatabase("Healthcare"), EntityName.FromDatabase("Pfizer"), false)
            {
                WorkInformationInternalId = 2
            }
        };

        _mockIndustries = _industryData.AsQueryable().BuildMockDbSet();

        _mockContext = new Mock<VRCampusDatabaseContext>(MockBehavior.Loose);

        _mockContext.Setup(c => c.Industries).Returns(_mockIndustries.Object);

        _repository = new SqlIndustryRepository(_mockContext.Object);
    }

    /// <summary>
    /// Verifies that <see cref="SqlIndustryRepository.ListAllAsync"/> returns all industries in the database.
    /// </summary>
    [Fact]
    public async Task ListAllAsync_ShouldReturnAllIndustries_WhenIndustriesExist()
    {
        var result = await _repository.ListAllAsync();

        result.Should().BeEquivalentTo(_industryData);
        _mockContext.Verify(c => c.Industries, Times.Once);
    }

    /// <summary>
    /// Verifies that <see cref="SqlIndustryRepository.ListAllAsync"/> returns an empty list when no industries exist.
    /// </summary>
    [Fact]
    public async Task ListAllAsync_ShouldReturnEmpty_WhenNoIndustriesExist()
    {
        var emptyList = new List<Industry>();
        var mockEmptyDbSet = emptyList.AsQueryable().BuildMockDbSet();

        var mockContext = new Mock<VRCampusDatabaseContext>(MockBehavior.Loose);
        mockContext.Setup(c => c.Industries).Returns(mockEmptyDbSet.Object);

        var repository = new SqlIndustryRepository(mockContext.Object);

        var result = await repository.ListAllAsync();

        result.Should().BeEmpty();
        mockContext.Verify(c => c.Industries, Times.Once);
    }
}
