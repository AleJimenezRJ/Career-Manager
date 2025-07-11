using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Endpoints;

/// <summary>
/// Provides extension methods for mapping endpoints related to work information in an application.
/// </summary>
/// <remarks>This class contains methods to register API endpoints for retrieving work information.  The endpoints
/// include operations for fetching all work information, retrieving a specific work information  entry by its
/// identifier, and retrieving work information associated with a specific career.</remarks>
public static class WorkInformationEndpoints
{
    /// <summary>
    /// Maps the endpoints related to work information to the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <remarks>This method registers the following endpoints: <list type="bullet"> <item>
    /// <description><c>GET /work-information</c>: Retrieves a list of work information.</description> </item> <item>
    /// <description><c>GET /work-information/{internalWorkInformationId}</c>: Retrieves a specific work information
    /// entry by its internal ID.</description> </item> <item> <description><c>GET
    /// /careers/{internalCareerId}/work-information</c>: Retrieves work information associated with a specific career
    /// by its internal career ID.</description> </item> </list> Each endpoint is tagged with "Work Information" and
    /// includes OpenAPI metadata.</remarks>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> to which the endpoints will be added.</param>
    /// <returns>The <see cref="IEndpointRouteBuilder"/> with the work information endpoints mapped.</returns>
    public static IEndpointRouteBuilder MapWorkInformationEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/work-information", GetWorkInformationHandler.HandleAsync)
            .WithName("GetWorkInformation")
            .WithTags("Work Information")
            .WithOpenApi();

        builder.MapGet("/work-information/{internalWorkInformationId}", GetSingleWorkInformationByIdHandler.HandleAsync)
            .WithName("GetSingleLearningWorkInformationById")
            .WithTags("Work Information")
            .WithOpenApi();

        builder.MapGet("/careers/{careerInternalId}/work-information", GetWorkInformationByIdHandler.HandleAsync)
            .WithName("GetWorkInformationById")
            .WithTags("Work Information")
            .WithOpenApi();

        return builder;
    }
}