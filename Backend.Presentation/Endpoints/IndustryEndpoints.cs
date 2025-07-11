using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Endpoints;

/// <summary>
/// Provides extension methods for mapping endpoints related to industry operations.
/// </summary>
/// <remarks>This class contains methods to register HTTP endpoints for retrieving and managing industry-related
/// data. The endpoints are added to the specified <see cref="IEndpointRouteBuilder"/> instance.</remarks>
public static class IndustryEndpoints
{
    /// <summary>
    /// Maps endpoints related to industry information to the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <remarks>This method registers the following endpoints: <list type="bullet"> <item> <description>A GET
    /// endpoint at <c>/work-information/industry</c> for retrieving industry information.</description> </item> <item>
    /// <description>A POST endpoint at <c>/careers/{careerInternalId}/work-information/industry</c> for submitting
    /// industry information for a specific career.</description> </item> </list> Each endpoint is tagged with
    /// "Industries" and includes OpenAPI metadata.</remarks>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> to which the industry endpoints will be added.</param>
    /// <returns>The <see cref="IEndpointRouteBuilder"/> with the industry endpoints mapped.</returns>
    public static IEndpointRouteBuilder MapIndustryEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/work-information/industry", GetIndustryHandler.HandleAsync)
            .WithName("GetIndustry")
            .WithTags("Industries")
            .WithOpenApi();

        builder.MapPost("/careers/{careerInternalId}/work-information/industry", PostIndustryHandler.HandleAsync)
            .WithName("PostIndustry")
            .WithTags("Industries")
            .WithOpenApi();

        return builder;
    }
}
