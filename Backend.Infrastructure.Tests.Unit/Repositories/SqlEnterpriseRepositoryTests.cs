using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Unit.Repositories;

/// <summary>
/// Provides unit tests for the <see cref="SqlEnterpriseRepository"/> class, ensuring its methods behave as expected
/// when interacting with enterprise data in a mocked database context.
/// </summary>
/// <remarks>This test class uses mocked instances of <see cref="DbSet{T}"/> and <see
/// cref="VRCampusDatabaseContext"/> to simulate database operations. It verifies the behavior of the repository methods
/// under various conditions, such as when enterprise data exists or when the database is empty.</remarks>
public class SqlEnterpriseRepositoryTests
{
    /// <summary>
    /// Represents a collection of enterprise data.
    /// </summary>
    /// <remarks>This field is intended for internal use and stores a list of <see cref="Enterprise"/>
    /// objects. It is read-only and cannot be modified directly after initialization.</remarks>
    private readonly List<Enterprise> _enterpriseData;

    /// <summary>
    /// Represents a mocked instance of a <see cref="DbSet{T}"/> for the <see cref="Enterprise"/> entity.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate database operations involving the
    /// <see cref="Enterprise"/> entity. It provides a mock implementation of <see cref="DbSet{T}"/> that can be
    /// configured to return specific data or behaviors.</remarks>
    private readonly Mock<DbSet<Enterprise>> _mockEnterprises;

    /// <summary>
    /// Represents a mock instance of the <see cref="VRCampusDatabaseContext"/> used for testing purposes.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate database interactions without
    /// requiring a real database connection.</remarks>
    private readonly Mock<VRCampusDatabaseContext> _mockContext;

    /// <summary>
    /// Represents the repository used for accessing enterprise-level SQL data.
    /// </summary>
    /// <remarks>This field is read-only and is intended to store an instance of <see
    /// cref="SqlEnterpriseRepository"/>  for performing database operations. It is typically initialized during object
    /// construction.</remarks>
    private readonly SqlEnterpriseRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlEnterpriseRepositoryTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up mock data and dependencies required for testing the <see
    /// cref="SqlEnterpriseRepository"/>. It creates a collection of mock <see cref="Enterprise"/> objects, configures a
    /// mock database context, and initializes the repository instance to be tested.</remarks>
    public SqlEnterpriseRepositoryTests()
    {
        // Arrange mock data
        _enterpriseData = new List<Enterprise>
        {
            new Enterprise(Description.FromDatabase("Information"), EntityName.FromDatabase("A company"), Country.FromDatabase("United States")),
            new Enterprise(Description.FromDatabase("Information1"), EntityName.FromDatabase("A different company"), Country.FromDatabase("Germany")),
            new Enterprise(Description.FromDatabase("Information2"), EntityName.FromDatabase("A original enterprise"), Country.FromDatabase("Costa Rica"))
        };

        _mockEnterprises = _enterpriseData.AsQueryable().BuildMockDbSet();

        _mockContext = new Mock<VRCampusDatabaseContext>();
        _mockContext.Setup(c => c.Enterprises).Returns(_mockEnterprises.Object);

        _repository = new SqlEnterpriseRepository(_mockContext.Object);
    }

    /// <summary>
    /// Tests that the <see cref="_repository.ListAllAsync"/> method returns all enterprises when enterprises exist in
    /// the data source.
    /// </summary>
    /// <remarks>This test verifies that the result of <see cref="_repository.ListAllAsync"/> matches the
    /// expected enterprise data. It ensures the method correctly retrieves all available enterprises.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListAllAsync_ShouldReturnAllEnterprises_WhenEnterprisesExist()
    {
        // Act
        var result = await _repository.ListAllAsync();

        // Assert
        result.Should().BeEquivalentTo(_enterpriseData);
    }

    /// <summary>
    /// Tests that <see cref="SqlEnterpriseRepository.ListAllAsync"/> returns an empty collection when no enterprises
    /// exist in the database.
    /// </summary>
    /// <remarks>This test verifies the behavior of the <see cref="SqlEnterpriseRepository.ListAllAsync"/>
    /// method when the database contains no enterprise records. It ensures that the method correctly handles an empty
    /// data set and returns an empty collection.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListAllAsync_ShouldReturnEmpty_WhenNoEnterprisesExist()
    {
        // Arrange
        var emptyList = new List<Enterprise>();
        var mockEmptyDbSet = emptyList.AsQueryable().BuildMockDbSet();
        _mockContext.Setup(c => c.Enterprises).Returns(mockEmptyDbSet.Object);

        var repository = new SqlEnterpriseRepository(_mockContext.Object);

        // Act
        var result = await repository.ListAllAsync();

        // Assert
        result.Should().BeEmpty();
    }
}
