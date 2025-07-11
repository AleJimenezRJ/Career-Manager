using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using Xunit;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration.Repositories;

/// <summary>
/// Integration tests for the <see cref="SqlIndustryRepository"/>, verifying database interaction.
/// </summary>
[Collection("Database collection")]
public class SqlIndustryRepositoryTests
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes the test class with the provided test fixture.
    /// </summary>
    /// <param name="fixture">The shared integration test fixture.</param>
    public SqlIndustryRepositoryTests(IntegrationTestFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
    }

    [Fact]
    public async Task ListAllAsync_ShouldReturnAllIndustries()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IIndustryRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        // Arrange
        var industry = new Industry
        (
            Description.FromDatabase("Banking services."),
            EntityName.FromDatabase("Finance"),
            true
        );

        await context.Industries.AddAsync(industry);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.ListAllAsync();

        // Assert
        result.Should().ContainSingle(i => i.InformationDescription!.Content.Contains("Banking"));
    }

    [Fact]
    public async Task ListAllAsync_ShouldReturnEmpty_WhenNoIndustriesExist()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IIndustryRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        // Arrange
        context.Industries.RemoveRange(context.Industries);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.ListAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task ListAllAsync_ShouldReturnMultipleIndustries()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IIndustryRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        // Arrange
        var i1 = new Industry
        (
            Description.FromDatabase("Agriculture and food processing."),
            EntityName.FromDatabase("AgroCorp"),
            false
        );

        var i2 = new Industry
        (
            Description.FromDatabase("Cybersecurity and software development."),
            EntityName.FromDatabase("SecureSoft"),
            true
        );

        await context.Industries.AddRangeAsync(i1, i2);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.ListAllAsync();

        // Assert
        result.Should().Contain(i => i.Name!.Name == "AgroCorp");
        result.Should().Contain(i => i.Name!.Name == "SecureSoft");
        result.Count().Should().BeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task IndustryEntity_ShouldPersistSteamFlagCorrectly()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IIndustryRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        var industry = new Industry
        (
            Description.FromDatabase("Tech and hardware innovation."),
            EntityName.FromDatabase("TechGenius"),
            true
        );

        await context.Industries.AddAsync(industry);
        await context.SaveChangesAsync();

        var result = await repo.ListAllAsync();

        var saved = result.FirstOrDefault(i => i.Name!.Name == "TechGenius");
        saved.Should().NotBeNull();
        saved.InformationDescription!.Content.Should().Contain("hardware");
    }
}
