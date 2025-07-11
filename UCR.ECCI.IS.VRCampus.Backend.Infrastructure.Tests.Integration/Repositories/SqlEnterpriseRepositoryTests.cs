using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration.Repositories;

/// <summary>
/// Integration tests for the <see cref="SqlEnterpriseRepository"/>, verifying database interaction.
/// </summary>
[Collection("Database collection")]
public class SqlEnterpriseRepositoryTests
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes the test class with the provided test fixture.
    /// </summary>
    /// <param name="fixture">The shared integration test fixture.</param>
    public SqlEnterpriseRepositoryTests(IntegrationTestFixture fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
    }

    [Fact]
    public async Task ListAllAsync_ShouldReturnAllEnterprises()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IEnterpriseRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        // Arrange
        var enterprise = new Enterprise
        (
            Description.FromDatabase("A leading provider of tech solutions."),
            EntityName.FromDatabase("Global Tech Solutions"),
            Country.FromDatabase("Costa Rica")
        );

        await context.Enterprises.AddAsync(enterprise);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.ListAllAsync();

        // Assert
        result.Should().ContainSingle(e => e.Name!.Name == "Global Tech Solutions");
    }

    [Fact]
    public async Task ListAllAsync_ShouldReturnEmpty_WhenNoEnterprisesExist()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IEnterpriseRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        // Arrange
        context.Enterprises.RemoveRange(context.Enterprises);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.ListAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task ListAllAsync_ShouldReturnMultipleEnterprises()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IEnterpriseRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        // Arrange
        var e1 = new Enterprise
        (
            Description.FromDatabase("International logistics and trade."),
            EntityName.FromDatabase("Global Freight Co."),
            Country.FromDatabase("United States")
        );

        var e2 = new Enterprise
        (
            Description.FromDatabase("Cloud storage and analytics."),
            EntityName.FromDatabase("Nimbus Data Inc."),
            Country.FromDatabase("Canada")
        );

        await context.Enterprises.AddRangeAsync(e1, e2);
        await context.SaveChangesAsync();

        // Act
        var result = await repo.ListAllAsync();

        // Assert
        result.Should().Contain(e => e.Name!.Name == "Global Freight Co.");
        result.Should().Contain(e => e.Name!.Name == "Nimbus Data Inc.");
        result.Count().Should().BeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task EnterpriseEntity_ShouldPersistCountryAndDescription()
    {
        using var scope = _serviceProvider.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IEnterpriseRepository>();
        var context = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();

        var enterprise = new Enterprise
        (
            Description.FromDatabase("Renewable energy manufacturing and export."),
            EntityName.FromDatabase("Green Innovations Ltd."),
            Country.FromDatabase("Germany")
        );

        await context.Enterprises.AddAsync(enterprise);
        await context.SaveChangesAsync();

        var result = await repo.ListAllAsync();

        var saved = result.FirstOrDefault(e => e.Name!.Name == "Green Innovations Ltd.");
        saved.Should().NotBeNull();
        saved!.Country!.Value.Should().Be("Germany");
        saved.InformationDescription!.Content.Should().Contain("Renewable");
    }
}
