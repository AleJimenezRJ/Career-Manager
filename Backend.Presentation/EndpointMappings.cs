using Microsoft.AspNetCore.Routing;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Endpoints;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation;

/// <summary>
/// Provides extension methods for mapping endpoints in the presentation layer.
/// </summary>
/// <remarks>This class contains methods to simplify the registration of endpoints for the presentation layer. This is used to configure and organize endpoint mappings in a consistent manner.</remarks>
public static class EndpointMappings
{
    /// <summary>
    /// Maps the presentation layer endpoints to the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> to which the endpoints will be mapped.</param>
    /// <returns>The same <see cref="IEndpointRouteBuilder"/> instance, allowing for method chaining.</returns>
    public static IEndpointRouteBuilder MapPresentationLayerEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapCareerEndpoints();
        builder.MapEnterpriseEndpoints();
        builder.MapIndustryEndpoints();
        builder.MapOpportunityEndpoints();
        builder.MapWorkInformationEndpoints();
        builder.MapWorkLifeEndpoints();
        builder.MapRecruitmentEndpoints();
        builder.MapLanguageEndpoints();

        return builder;
    }
}
