using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Unit.Repositories;

/// <summary>
/// Provides unit tests for the <see cref="SqlRecruitmentRepository"/> to validate proper data retrieval behavior.
/// </summary>
public class SqlRecruitmentRepositoryTests
{
    private readonly List<Recruitment> _recruitmentData;
    private readonly Mock<DbSet<Recruitment>> _mockRecruitments;
    private readonly Mock<VRCampusDatabaseContext> _mockContext;
    private readonly SqlRecruitmentRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlRecruitmentRepositoryTests"/> class,
    /// setting up mocks and sample data.
    /// </summary>
    public SqlRecruitmentRepositoryTests()
    {
        _recruitmentData = new List<Recruitment>
        {
            new Recruitment(
                Description.FromDatabase("Internship program"),
                Description.FromDatabase("Follow these steps to apply."),
                Description.FromDatabase("Must be enrolled in a related field."),
                new List<Language>
                {
                    new(LanguageVO.FromDatabase("English")),
                    new(LanguageVO.FromDatabase("Spanish"))
                }
            )
        };

        _mockRecruitments = _recruitmentData.AsQueryable().BuildMockDbSet();

        _mockContext = new Mock<VRCampusDatabaseContext>(MockBehavior.Loose);
        _mockContext.Setup(c => c.Recruitments).Returns(_mockRecruitments.Object);

        _repository = new SqlRecruitmentRepository(_mockContext.Object);
    }

    /// <summary>
    /// Tests that <see cref="SqlRecruitmentRepository.ListAllAsync"/> returns all recruitment records.
    /// </summary>
    [Fact]
    public async Task ListAllAsync_ShouldReturnAllRecruitments_WhenRecruitmentsExist()
    {
        var result = await _repository.ListAllAsync();

        result.Should().BeEquivalentTo(_recruitmentData);
        _mockContext.Verify(c => c.Recruitments, Times.Once);
    }

    /// <summary>
    /// Tests that <see cref="SqlRecruitmentRepository.ListAllAsync"/> returns an empty list when there are no recruitments.
    /// </summary>
    [Fact]
    public async Task ListAllAsync_ShouldReturnEmpty_WhenNoRecruitmentsExist()
    {
        var emptyList = new List<Recruitment>();
        var mockEmptyDbSet = emptyList.AsQueryable().BuildMockDbSet();
        _mockContext.Setup(c => c.Recruitments).Returns(mockEmptyDbSet.Object);

        var repository = new SqlRecruitmentRepository(_mockContext.Object);

        var result = await repository.ListAllAsync();

        result.Should().BeEmpty();
        _mockContext.Verify(c => c.Recruitments, Times.Once);
    }
}
