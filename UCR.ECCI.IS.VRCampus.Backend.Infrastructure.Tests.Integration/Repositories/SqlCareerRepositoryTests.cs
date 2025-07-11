using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration.Repositories;

/// <summary>
/// Provides unit tests for the SQL-based implementation of the <see cref="ICareerRepository"/> interface.
/// </summary>
/// <remarks>This test class is part of the integration test suite and is designed to verify the behavior of  the
/// SQL-backed career repository in scenarios such as adding new careers, handling duplicates,  and retrieving career
/// data. It uses a shared database fixture to ensure consistent test setup  and teardown.</remarks>
[Collection("Database collection")]
public class SqlCareerRepositoryTests
{
    /// <summary>
    /// Provides access to the application's service provider for resolving dependencies.
    /// </summary>
    /// <remarks>This field is read-only and intended for internal use to facilitate dependency injection. It
    /// holds a reference to an <see cref="IServiceProvider"/> instance, which can be used to retrieve services
    /// registered in the application's service container.</remarks>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlCareerRepositoryTests"/> class.
    /// </summary>
    /// <param name="fixture">The integration test fixture that provides the required service provider for the tests.</param>
    public SqlCareerRepositoryTests(IntegrationTestFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
    }

    /// <summary>
    /// Tests the <c>AddCareerAsync</c> method to ensure it successfully adds a new career to the repository.
    /// </summary>
    /// <remarks>This test verifies that the <c>AddCareerAsync</c> method correctly handles the addition of a
    /// new career and that the operation succeeds when the career does not already exist in the repository.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddCareerAsync_ShouldSucceed_WhenCareerIsNew()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ICareerRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        var career = new Career
        (
            EntityName.FromDatabase("Bioinformatics"),
            Description.FromDatabase("Study of computational biology and genomics."),
            SemestersNumber.FromDatabase(10),
            Modality.FromDatabase("Virtual"),
            DegreeTitle.FromDatabase("Bachelor"),
            true
        );

        var result = await repo.AddCareerAsync(career);
        await context.SaveChangesAsync();

        result.IsSuccess.Should().BeTrue();
    }

    /// <summary>
    /// Tests that the <see cref="ICareerRepository.AddCareerAsync(Career)"/> method fails when attempting to add a
    /// duplicate career.
    /// </summary>
    /// <remarks>This test verifies that the repository correctly prevents the addition of a career with
    /// identical properties to one already present in the database. It ensures that the <see
    /// cref="ICareerRepository.AddCareerAsync(Career)"/> method returns a failure result when a duplicate career is
    /// added.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddCareerAsync_ShouldFail_WhenCareerIsDuplicate()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ICareerRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        var career = new Career
        (
            EntityName.FromDatabase("Tourism"),
            Description.FromDatabase("Career in tourism and hospitality."),
            SemestersNumber.FromDatabase(8),
            Modality.FromDatabase("Virtual"),
            DegreeTitle.FromDatabase("Bachelor"),
            false
        );

        await repo.AddCareerAsync(career);
        await context.SaveChangesAsync();

        var result = await repo.AddCareerAsync(career);

        result.IsFailure.Should().BeTrue();
    }

    /// <summary>
    /// Tests that the <see cref="ICareerRepository.ListCareersAsync"/> method returns all careers.
    /// </summary>
    /// <remarks>This test verifies that the result of <see cref="ICareerRepository.ListCareersAsync"/> is not
    /// null  and contains only instances of the <see cref="Career"/> type.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListCareersAsync_ShouldReturnAllCareers()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ICareerRepository>();

        var result = await repo.ListCareersAsync();

        result.Should().NotBeNull();
        result.Should().OnlyContain(c => c is Career);
    }

    /// <summary>
    /// Tests the <see cref="ICareerRepository.ListSpecificCareerAsync(string)"/> method to ensure it returns the
    /// correct career when a career with the specified name exists in the database.
    /// </summary>
    /// <remarks>This test verifies that the repository correctly retrieves a career by name after it has been
    /// added to the database.  It ensures that the returned career is not null and that its name matches the expected
    /// value.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListSpecificCareerAsync_ShouldReturnCareer_WhenExists()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ICareerRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        var name = "Engineering";
        var career = new Career
        (
            EntityName.FromDatabase(name),
            Description.FromDatabase("Engineering and problem solving."),
            SemestersNumber.FromDatabase(9),
            Modality.FromDatabase("Hybrid"),
            DegreeTitle.FromDatabase("Bachelor"),
            true
        );

        await repo.AddCareerAsync(career);
        await context.SaveChangesAsync();

        var result = await repo.ListSpecificCareerAsync(name);

        result.Should().NotBeNull();
        result!.Name!.Name.Should().Be(name);
    }
}
