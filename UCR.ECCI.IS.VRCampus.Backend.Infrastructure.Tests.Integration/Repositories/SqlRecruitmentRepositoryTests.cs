using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration.Repositories;

/// <summary>
/// Integration tests for the <see cref="SqlRecruitmentRepository"/> class, validating database interactions.
/// </summary>
[Collection("Database collection")]
public class SqlRecruitmentRepositoryTests
{
    private readonly IServiceProvider _serviceProvider;

    public SqlRecruitmentRepositoryTests(IntegrationTestFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
    }

    [Fact]
    public async Task AddRecruitment_ShouldPersistRecruitment_WhenValid()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IRecruitmentRepository>();
        var careerRepo = scope.ServiceProvider.GetRequiredService<ICareerRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        // Arrange: Ensure a career exists
        var career = new Career(
            EntityName.FromDatabase("Recruitment Career"),
            Description.FromDatabase("For testing recruitment additions."),
            SemestersNumber.FromDatabase(6),
            Modality.FromDatabase("Hybrid"),
            DegreeTitle.FromDatabase("Bachelor"),
            true
        );
        await careerRepo.AddCareerAsync(career);
        await context.SaveChangesAsync();

        var recruitment = new Recruitment(
            Description.FromDatabase("Hiring interns for summer."),
            Description.FromDatabase("Internship program description."),
            Description.FromDatabase("Internship program requirements."),
            new List<Language>()
            {
                new Language(LanguageVO.FromDatabase("English")),
                new Language(LanguageVO.FromDatabase("Spanish"))
            }
        );

        // Act
        var result = await repo.AddInformationAsync(career.CareerInternalId, recruitment);
        await context.SaveChangesAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

}
