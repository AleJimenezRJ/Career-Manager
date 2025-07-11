using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Repositories;

namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure;

/// <summary>
/// Provides extension methods to register Infrastructure layer services with the application's dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the infrastructure layer services, including the database context and repositories.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="configuration">The application configuration used to retrieve connection strings.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register the Career repository implementation
        services.AddTransient<ICareerRepository, SqlCareerRepository>();
        // Register the WorkInformation repository implementation
        services.AddTransient<IWorkInformationRepository, SqlWorkInformationRepository>();
        // Register the WorkLife repository implementation
        services.AddTransient<IWorkLifeRepository, SqlWorkLifeRepository>();
        // Register the Industry repository implementation
        services.AddTransient<IIndustryRepository, SqlIndustryRepository>();
        // Register the Opportunity repository implementation
        services.AddTransient<IOpportunityRepository, SqlOpportunityRepository>();
        // Register the Enterprise repository implementation
        services.AddTransient<IEnterpriseRepository, SqlEnterpriseRepository>();
        // Register the Recruitment repository implementation
        services.AddTransient<IRecruitmentRepository, SqlRecruitmentRepository>();
        // Register the Language repository implementation
        services.AddTransient<ILanguageRepository, SqlLanguageRepository>();
        // Register the EF Core DbContext using SQL Server and the configured connection string
        services.AddDbContext<VRCampusDatabaseContext>(x =>
            x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
