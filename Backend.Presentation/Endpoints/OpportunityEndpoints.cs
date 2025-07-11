using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Endpoints;

/// <summary>
/// Provides extension methods for mapping endpoints related to opportunities in the application's routing system.
/// </summary>
/// <remarks>This class defines methods to register HTTP GET and POST endpoints for managing opportunities. The
/// endpoints are added to the specified <see cref="IEndpointRouteBuilder"/> instance and are configured with
/// appropriate names, tags, and OpenAPI metadata.</remarks>
public static class OpportunityEndpoints
{
    /// <summary>
    /// Maps the endpoints for managing opportunities to the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <remarks>This method registers the following endpoints: <list type="bullet"> <item> <description> A
    /// GET endpoint at <c>/work-information/opportunity</c> for retrieving opportunities, handled by <see
    /// cref="GetOpportunityHandler.HandleAsync"/>. </description> </item> <item> <description> A POST endpoint at
    /// <c>/careers/{careerInternalId}/work-information/opportunity</c> for creating opportunities, handled by <see
    /// cref="PostOpportunityHandler.HandleAsync"/>. </description> </item> </list> Both endpoints are tagged as
    /// "Opportunities" and include OpenAPI metadata.</remarks>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> to which the opportunity endpoints will be added.</param>
    /// <returns>The <see cref="IEndpointRouteBuilder"/> with the opportunity endpoints mapped.</returns>
    public static IEndpointRouteBuilder MapOpportunityEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/work-information/opportunity", GetOpportunityHandler.HandleAsync)
            .WithName("GetOpportunity")
            .WithTags("Opportunities")
            .WithOpenApi();

        builder.MapPost("/careers/{careerInternalId}/work-information/opportunity", PostOpportunityHandler.HandleAsync)
            .WithName("PostOpportunity")
            .WithTags("Opportunities")
            .WithOpenApi();

        return builder;
    }
}
