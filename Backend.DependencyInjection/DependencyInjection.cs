using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UCR.ECCI.IS.VRCampus.Backend.Application;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure;
using UCR.ECCI.IS.VRCampus.Backend.Presentation;

namespace UCR.ECCI.IS.VRCampus.Backend.DependencyInjection;

/// <summary>
/// Provides extension methods for configuring dependency injection in the application.
/// This class centralizes the registration of application and infrastructure layer services,
/// promoting a clean separation of concerns following the Clean Architecture pattern.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the services for the Application and Infrastructure layers into the dependency injection container.
    /// This method is intended to be called during application startup.
    /// </summary>
    /// <param name="services">The service collection to which the services are added.</param>
    /// <param name="configuration">The application configuration used by the Infrastructure layer.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddCleanArchitectureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register services from the Application layer
        services.AddApplicationLayerServices();

        // Register services and database context from the Infrastructure layer
        services.AddInfrastructureLayerServices(configuration);

        return services;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        return builder.MapPresentationLayerEndpoints();
    }
}
