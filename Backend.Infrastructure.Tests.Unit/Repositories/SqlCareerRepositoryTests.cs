using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Unit.Repositories;

public class SqlCareerRepositoryTests
{
    private readonly Mock<DbSet<Career>> _mockCareers;
    private readonly Mock<VRCampusDatabaseContext> _mockContext;
    private readonly Mock<IWorkInformationRepository> _mockWorkInfoRepo;
    private readonly List<Career> _careerData;
    private readonly SqlCareerRepository _repository;

    public SqlCareerRepositoryTests()
    {
        _careerData = new List<Career>();
        _mockCareers = _careerData.AsQueryable().BuildMockDbSet();

        _mockContext = new Mock<VRCampusDatabaseContext>();
        _mockContext.Setup(c => c.Careers).Returns(_mockCareers.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

        _mockWorkInfoRepo = new Mock<IWorkInformationRepository>();
        _repository = new SqlCareerRepository(_mockContext.Object, _mockWorkInfoRepo.Object);
    }

    [Fact]
    public async Task AddCareerAsync_ShouldAdd_WhenCareerIsNew()
    {
        var career = TestCareer();
        _mockContext.Setup(c => c.Careers.AddAsync(career, default)).Callback(() => _careerData.Add(career));

        var result = await _repository.AddCareerAsync(career);

        result.IsSuccess.Should().BeTrue();
        _careerData.Should().Contain(career);
    }

    [Fact]
    public async Task AddCareerAsync_ShouldFail_WhenCareerIsDuplicated()
    {
        var career = TestCareer();
        _careerData.Add(career);

        var result = await _repository.AddCareerAsync(career);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be("Entity.DuplicatedConflict");
    }

    [Fact]
    public async Task ListCareersAsync_ShouldReturnAllCareers()
    {
        _careerData.Add(TestCareer());

        var result = await _repository.ListCareersAsync();

        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task ListSpecificCareerAsync_ShouldReturnNull_WhenNotExists()
    {
        var result = await _repository.ListSpecificCareerAsync("NonExistentCareer");

        result.Should().BeNull();
    }

    private Career TestCareer()
    {
        return new Career(
            EntityName.FromDatabase("TestCareer"),
            Description.FromDatabase("A test career"),
            SemestersNumber.FromDatabase(8),
            Modality.FromDatabase("Virtual"),
            DegreeTitle.FromDatabase("Bachelor"),
            true
        );
    }
}
