using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration.Repositories;

/// <summary>
/// Contains unit tests for verifying the behavior of the SQL-based implementation of the <see
/// cref="ILanguageRepository"/> interface.
/// </summary>
/// <remarks>This test class is part of the integration test suite and relies on a shared database fixture to
/// ensure consistent test execution. It validates the functionality of methods in <see cref="ILanguageRepository"/>,
/// including listing languages, adding new languages, and handling edge cases such as duplicate entries or invalid
/// inputs.</remarks>
[Collection("Database collection")]
public class SqlLanguageRepositoryTests
{
    private readonly IServiceProvider _serviceProvider;

    public SqlLanguageRepositoryTests(IntegrationTestFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
    }

    [Fact]
    public async Task ListLanguagesAsync_ShouldReturnAllLanguages()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ILanguageRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        // Arrange
        var recruitment = new Recruitment(
            Description.FromDatabase("Internship program"),
            Description.FromDatabase("Follow these steps to apply."),
            Description.FromDatabase("Must be enrolled in a related field."),
            new List<Language> { new Language(LanguageVO.FromDatabase("English")), new Language(LanguageVO.FromDatabase("Spanish")) }
        );

        await context.Recruitments.AddAsync(recruitment);
        await context.SaveChangesAsync();
        var language = new Language(LanguageVO.FromDatabase("Japanese"));
        await repo.AddLanguageAsync(recruitment.WorkInformationInternalId, language);
        await context.SaveChangesAsync();

        // Act
        var sucess = await repo.ListLanguagesAsync();

        // Assert
        sucess.Should().Contain(l => l.LanguageValue.Value == "Japanese");
    }

    [Fact]
    public async Task AddLanguageAsync_ShouldSucceed_WhenLanguageIsNew()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ILanguageRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        var recruitment = new Recruitment(
            Description.FromDatabase("Internship program"),
            Description.FromDatabase("Follow these steps to apply."),
            Description.FromDatabase("Must be enrolled in a related field."),
            new List<Language> { new Language(LanguageVO.FromDatabase("English")), new Language(LanguageVO.FromDatabase("Spanish")) }
        );

        await context.Recruitments.AddAsync(recruitment);
        await context.SaveChangesAsync();

        var language = new Language(LanguageVO.FromDatabase("German"));

        // Act
        var result = await repo.AddLanguageAsync(recruitment.WorkInformationInternalId, language);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var updated = await context.Recruitments.FindAsync(recruitment.WorkInformationInternalId);
        updated!.LanguageRequested.Should().ContainSingle(l => l.LanguageValue.Value == "German");
    }

    [Fact]
    public async Task AddLanguageAsync_ShouldFail_WhenLanguageIsDuplicate()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ILanguageRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        var language = new Language(LanguageVO.FromDatabase("French"));
        var recruitment = new Recruitment
        (
            Description.FromDatabase("Internship program"),
            Description.FromDatabase("Follow these steps to apply."),
            Description.FromDatabase("Must be enrolled in a related field."),
            new List<Language> { new Language(LanguageVO.FromDatabase("French")), new Language(LanguageVO.FromDatabase("Spanish")) }
        );

        await context.Recruitments.AddAsync(recruitment);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.AddLanguageAsync(recruitment.WorkInformationInternalId, language);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task AddLanguageAsync_ShouldFail_WhenRecruitmentNotFound()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ILanguageRepository>();

        var language = new Language(LanguageVO.FromDatabase("Italian"));

        // Act
        var result = await repo.AddLanguageAsync(99999, language); // ID inexistente

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task AddLanguageAsync_ShouldFail_WhenLanguageIsNull()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ILanguageRepository>();

        // Act
        var result = await repo.AddLanguageAsync(1, null!);

        // Assert
        result.IsFailure.Should().BeTrue();
    }
}
