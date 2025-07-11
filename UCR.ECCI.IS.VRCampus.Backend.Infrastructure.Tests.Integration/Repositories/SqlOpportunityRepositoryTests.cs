using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration.Repositories;

/// <summary>
/// Provides integration tests for the <see cref="SqlOpportunityRepository"/> class.
/// </summary>
[Collection("Database collection")]
public class SqlOpportunityRepositoryTests
{
    private readonly IServiceProvider _serviceProvider;

    public SqlOpportunityRepositoryTests(IntegrationTestFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
    }

    [Fact]
    public async Task ListAllAsync_ShouldReturnEmptyList_WhenNoOpportunitiesExist()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IOpportunityRepository>();

        var result = await repo.ListAllAsync();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task AddOpportunity_ShouldPersistOpportunity_WhenValid()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IOpportunityRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        // Arrange
        var opportunity = new Opportunity
        (
            Description.FromDatabase("Backend developer role in fintech."),
            Country.FromDatabase("Canada")
        );

        // Act
        var result = await repo.AddInformationAsync(1, opportunity); // Assumes Career with ID 1 exists
        await context.SaveChangesAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

}
