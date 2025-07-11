using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration;

/// <summary>
/// Provides a reusable fixture for integration tests, initializing
/// the application's infrastructure and ensuring a clean database context.
/// </summary>
public class IntegrationTestFixture
{
    /// <summary>
    /// The configured service provider used to resolve application services.
    /// </summary>
    public ServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegrationTestFixture"/> class.
    /// Sets up configuration, registers infrastructure services, and prepares a clean test database.
    /// </summary>
    public IntegrationTestFixture()
    {
        // Build configuration using user secrets for sensitive settings like connection strings
        var config = new ConfigurationBuilder()
            .AddUserSecrets<IntegrationTestFixture>() // Retrieves secrets from the UserSecrets stored in this project. Needs to be configured before use, same as in Backend.Api
            .Build();

        var services = new ServiceCollection();

        // Register infrastructure layer (including DbContext with actual connection string)
        services.AddInfrastructureLayerServices(config);

        ServiceProvider = services.BuildServiceProvider();

        // Ensure database is reset for a clean test environment
        using var scope = ServiceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<VRCampusDatabaseContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

    }
}