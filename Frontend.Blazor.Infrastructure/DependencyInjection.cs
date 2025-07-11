using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Repositories;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Domain.Repositories;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure;

public static class DependencyInjection
{

    /// <summary>
    /// Registers infrastructure layer services into the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the services will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the registered services.</returns>
    public static IServiceCollection AddInfrastructureLayerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient(serviceProvider =>
        {
            // API requires no authentication, so use the anonymous
            // authentication provider
            var authProvider = new AnonymousAuthenticationProvider();
            // Create request adapter using the HttpClient-based implementation
            // Workaround taken from https://github.com/microsoft/kiota-dotnet/issues/481
            var httpClient = KiotaClientFactory.Create(finalHandler: new HttpClientHandler { AllowAutoRedirect = false });
            var adapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
            // Set Base URL.
            adapter.BaseUrl = configuration["ApiBaseUrl"];

            // Create the API client
            return new ApiClient(adapter);
        });
        // Register the KiotaCareerRepository as the implementation of ICareerRepository
        services.AddTransient<ICareerRepository, KiotaCareerRepository>();
        services.AddTransient<IWorkInformationRepository, KiotaWorkInformationRepository>();

        return services;
    }
}