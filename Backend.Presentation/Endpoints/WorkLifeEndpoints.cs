using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Endpoints;

/// <summary>
/// Provides extension methods for mapping Work Life-related API endpoints to an <see cref="IEndpointRouteBuilder"/>.
/// </summary>
/// <remarks>This class contains methods to register API endpoints for managing Work Life information, such as
/// retrieving and posting Work Life data. The endpoints are added to the provided <see cref="IEndpointRouteBuilder"/>
/// instance and are configured with appropriate route names, tags, and OpenAPI metadata.</remarks>
public static class WorkLifeEndpoints
{
    /// <summary>
    /// Maps the Work Life API endpoints to the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <remarks>This method registers the following endpoints: <list type="bullet"> <item> <description> A
    /// GET endpoint at <c>/work-information/workLife</c> for retrieving work life information. </description> </item>
    /// <item> <description> A POST endpoint at <c>/careers/{careerInternalId}/work-information/workLife</c> for
    /// creating or updating work life information. </description> </item> </list> Each endpoint is tagged with "Work
    /// Lives" and includes OpenAPI metadata.</remarks>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> to which the endpoints will be added.</param>
    /// <returns>The <see cref="IEndpointRouteBuilder"/> with the Work Life API endpoints mapped.</returns>
    public static IEndpointRouteBuilder MapWorkLifeEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/work-information/workLife", GetWorkLifeHandler.HandleAsync)
            .WithName("GetWorkLife")
            .WithTags("Work Lives")
            .WithOpenApi();

        builder.MapPost("/careers/{careerInternalId}/work-information/workLife", PostWorkLifeHandler.HandleAsync)
            .WithName("PostWorkLife")
            .WithTags("Work Lives")
            .WithOpenApi();

        return builder;
    }
}
