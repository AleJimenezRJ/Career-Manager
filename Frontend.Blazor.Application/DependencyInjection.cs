using Microsoft.Extensions.DependencyInjection;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application.Services;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application.Services.Implementations;

namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Application;

/// <summary>
/// Provides extension methods to register application layer services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all services from the application layer into the given <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance for chaining.</returns>
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        // Register ICareerServices with its concrete implementation CareerServices
        services.AddScoped<ICareerServices, CareerServices>();
        // Register IWorkInformationServices with its concrete implementation WorkInformationServices
        services.AddScoped<IWorkInformationServices, WorkInformationServices>();
        // Register IWorkLifeServices with its concrete implementation WorkLifeServices
        services.AddScoped<IWorkLifeServices, WorkLifeServices>();
        // Register IEnterpriseServices with its concrete implementation EnterpriseServices
        services.AddScoped<IEnterpriseServices, EnterpriseServices>();
        // Register IIndustriesServices with its concrete implementation IndustriesServices
        services.AddScoped<IIndustryServices, IndustryServices>();
        // Register IOpportunitiesServices with its concrete implementation OpportunitiesServices
        services.AddScoped<IOpportunityServices, OpportunityServices>();
        // Register IRecruitmentServices with its concrete implementation RecruitmentServices
        services.AddScoped<IRecruitmentServices, RecruitmentServices>();
        // Register ILanguageServices with its concrete implementation LanguageServices
        services.AddScoped<ILanguageServices, LanguageServices>();
        return services;
    }
}
