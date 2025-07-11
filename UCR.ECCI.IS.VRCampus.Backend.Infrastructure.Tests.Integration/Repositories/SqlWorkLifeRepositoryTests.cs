using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration.Repositories;

/// <summary>
/// Integration tests for the <see cref="SqlWorkLifeRepository"/> implementation.
/// </summary>
[Collection("Database collection")]
public class SqlWorkLifeRepositoryTests
{
    private readonly IServiceProvider _serviceProvider;

    public SqlWorkLifeRepositoryTests(IntegrationTestFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
    }

    [Fact]
    public async Task ListAllAsync_ShouldReturnEmpty_WhenNoWorkLifeExists()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IWorkLifeRepository>();

        var result = await repo.ListAllAsync();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task AddWorkLife_ShouldPersistEntity_WhenValid()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IWorkLifeRepository>();
        var careerRepo = scope.ServiceProvider.GetRequiredService<ICareerRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        // Arrange: Add career
        var career = new Career(
            EntityName.FromDatabase("Life Engineering"),
            Description.FromDatabase("Focuses on work-life balance innovations."),
            SemestersNumber.FromDatabase(9),
            Modality.FromDatabase("Hybrid"),
            DegreeTitle.FromDatabase("Bachelor"),
            true
        );
        await careerRepo.AddCareerAsync(career);
        await context.SaveChangesAsync();

        var _workLife = new WorkLife
        (
            Description.FromDatabase("Flexible work schedules"),
            Workers.FromDatabase(45),
            Workers.FromDatabase(15)
        );

        // Act
        var result = await repo.AddInformationAsync(career.CareerInternalId, _workLife);
        await context.SaveChangesAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

}
