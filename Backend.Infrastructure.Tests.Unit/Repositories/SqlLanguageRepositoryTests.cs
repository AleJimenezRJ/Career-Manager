using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;
using Xunit;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Unit.Repositories;

/// <summary>
/// Provides unit tests for the <see cref="SqlLanguageRepository"/> to validate its behavior for managing languages.
/// </summary>
public class SqlLanguageRepositoryTests
{
    private readonly List<Language> _languageData;
    private readonly List<Recruitment> _recruitmentData;
    private readonly Mock<DbSet<Language>> _mockLanguages;
    private readonly Mock<DbSet<Recruitment>> _mockRecruitments;
    private readonly Mock<VRCampusDatabaseContext> _mockContext;
    private readonly SqlLanguageRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlLanguageRepositoryTests"/> class.
    /// </summary>
    public SqlLanguageRepositoryTests()
    {
        _languageData = new List<Language>
        {
            new Language(LanguageVO.FromDatabase("English")),
            new Language(LanguageVO.FromDatabase("Spanish"))
        };

        _recruitmentData = new List<Recruitment>
{
            new Recruitment(
                Description.FromDatabase("A original description"),
                Description.FromDatabase("Some steps"),
                Description.FromDatabase("Required"),
            new List<Language>()) 
    {
        WorkInformationInternalId = 1 
    }
};


        _mockLanguages = _languageData.AsQueryable().BuildMockDbSet();
        _mockRecruitments = _recruitmentData.AsQueryable().BuildMockDbSet();

        _mockContext = new Mock<VRCampusDatabaseContext>(MockBehavior.Loose);
        _mockContext.Setup(c => c.Languages).Returns(_mockLanguages.Object);
        _mockContext.Setup(c => c.Recruitments).Returns(_mockRecruitments.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

        _repository = new SqlLanguageRepository(_mockContext.Object);
    }

    /// <summary>
    /// Ensures all languages are listed correctly from the database.
    /// </summary>
    [Fact]
    public async Task ListLanguagesAsync_ShouldReturnAllLanguages()
    {
        var result = await _repository.ListLanguagesAsync();

        result.Should().BeEquivalentTo(_languageData);
        _mockContext.Verify(c => c.Languages, Times.Once);
    }

    /// <summary>
    /// Adds a valid language to an existing recruitment entry.
    /// </summary>
    [Fact]
    public async Task AddLanguageAsync_ShouldAdd_WhenValidAndNotExists()
    {
        var newLanguage = new Language(LanguageVO.FromDatabase("French"));

        var result = await _repository.AddLanguageAsync(1, newLanguage);

        result.IsSuccess.Should().BeTrue();
        _recruitmentData[0].LanguageRequested.Should().ContainSingle(l => l.LanguageValue.Value == "French");
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }


    /// <summary>
    /// Fails when the provided language is null.
    /// </summary>
    [Fact]
    public async Task AddLanguageAsync_ShouldFail_WhenLanguageIsNull()
    {
        var result = await _repository.AddLanguageAsync(1, null!);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Language.Null");
    }

    /// <summary>
    /// Fails when the recruitment entry is not found.
    /// </summary>
    [Fact]
    public async Task AddLanguageAsync_ShouldFail_WhenRecruitmentNotFound()
    {
        var result = await _repository.AddLanguageAsync(999, new Language(LanguageVO.FromDatabase("German")));

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Recruitment.NotFound");
    }

    /// <summary>
    /// Fails when the language already exists in the recruitment entry.
    /// </summary>
    [Fact]
    public async Task AddLanguageAsync_ShouldFail_WhenLanguageAlreadyExists()
    {
        var language = new Language(LanguageVO.FromDatabase("English"));
        _recruitmentData[0].LanguageRequested.Add(new Language(LanguageVO.FromDatabase("English")));

        var result = await _repository.AddLanguageAsync(1, language);

        result.IsFailure.Should().BeTrue();
        result.Error!.Code.Should().Be("Language.Duplicate");

    }
}
