using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Endpoints;

/// <summary>
/// Provides extension methods for mapping recruitment-related endpoints to an <see cref="IEndpointRouteBuilder"/>.
/// </summary>
/// <remarks>This class defines routes for handling recruitment-related operations, such as retrieving and posting
/// recruitment information. The endpoints are mapped with appropriate names, tags, and OpenAPI metadata for better
/// discoverability and documentation.</remarks>
public static class RecruitmentEndpoints
{
    /// <summary>
    /// Maps the recruitment-related endpoints to the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <remarks>This method registers the following endpoints: <list type="bullet"> <item> <description>A GET
    /// endpoint at <c>/work-information/recruitment</c> for retrieving recruitment information.</description> </item>
    /// <item> <description>A POST endpoint at
    /// <c>/careers/{careerInternalId}/enterprise/{enterpriseInternalId}/work-information/recruitment</c> for creating
    /// or updating recruitment information.</description> </item> </list> Each endpoint is tagged with "Recruitments"
    /// and includes OpenAPI metadata.</remarks>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> to which the recruitment endpoints will be added.</param>
    /// <returns>The <see cref="IEndpointRouteBuilder"/> with the recruitment endpoints mapped.</returns>
    public static IEndpointRouteBuilder MapRecruitmentEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/work-information/recruitment", GetRecruitmentHandler.HandleAsync)
            .WithName("GetRecruitment")
            .WithTags("Recruitments")
            .WithOpenApi();

        builder.MapPost("/careers/{careerInternalId}/work-information/recruitment", PostRecruitmentHandler.HandleAsync)
            .WithName("PostRecruitment")
            .WithTags("Recruitments")
            .WithOpenApi();

        return builder;
    }
}
