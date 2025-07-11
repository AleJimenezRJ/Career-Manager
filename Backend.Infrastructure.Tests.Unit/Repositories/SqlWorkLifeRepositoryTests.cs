using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Unit.Repositories;

/// <summary>
/// Unit tests for <see cref="SqlWorkLifeRepository"/> to ensure correct behavior when retrieving work-life data.
/// </summary>
public class SqlWorkLifeRepositoryTests
{
    /// <summary>Mock data source for <see cref="WorkLife"/> entities.</summary>
    private readonly List<WorkLife> _workLifeData;

    /// <summary>Mocked <see cref="DbSet{WorkLife}"/> for simulating database operations.</summary>
    private readonly Mock<DbSet<WorkLife>> _mockWorkLives;

    /// <summary>Mocked database context for testing.</summary>
    private readonly Mock<VRCampusDatabaseContext> _mockContext;

    /// <summary>The repository under test.</summary>
    private readonly SqlWorkLifeRepository _repository;

    /// <summary>
    /// Initializes the test context, sets up mocks and sample data.
    /// </summary>
    public SqlWorkLifeRepositoryTests()
    {
        _workLifeData = new List<WorkLife>
        {
            new WorkLife(
                Description.FromDatabase("Flexible work schedules"),
                Workers.FromDatabase(45),
                Workers.FromDatabase(15)
            )
        };

        _mockWorkLives = _workLifeData.AsQueryable().BuildMockDbSet();

        _mockContext = new Mock<VRCampusDatabaseContext>(MockBehavior.Loose);
        _mockContext.Setup(c => c.WorkLives).Returns(_mockWorkLives.Object);

        _repository = new SqlWorkLifeRepository(_mockContext.Object);
    }

    /// <summary>
    /// Tests that <see cref="SqlWorkLifeRepository.ListAllAsync"/> returns all stored <see cref="WorkLife"/> records.
    /// </summary>
    [Fact]
    public async Task ListAllAsync_ShouldReturnAllWorkLife_WhenWorkLifeExists()
    {
        var result = await _repository.ListAllAsync();

        result.Should().BeEquivalentTo(_workLifeData);
        _mockContext.Verify(c => c.WorkLives, Times.Once);
    }

    /// <summary>
    /// Tests that <see cref="SqlWorkLifeRepository.ListAllAsync"/> returns an empty list when no <see cref="WorkLife"/> records exist.
    /// </summary>
    [Fact]
    public async Task ListAllAsync_ShouldReturnEmpty_WhenNoWorkLifeExists()
    {
        var emptyList = new List<WorkLife>();
        var mockEmptyDbSet = emptyList.AsQueryable().BuildMockDbSet();
        _mockContext.Setup(c => c.WorkLives).Returns(mockEmptyDbSet.Object);

        var repository = new SqlWorkLifeRepository(_mockContext.Object);

        var result = await repository.ListAllAsync();

        result.Should().BeEmpty();
        _mockContext.Verify(c => c.WorkLives, Times.Once);
    }
}
