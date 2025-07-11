using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Endpoints;

/// <summary>
/// Configures enterprise-related endpoints for the specified <see cref="IEndpointRouteBuilder"/>.
/// </summary>
/// <remarks>This method maps the following endpoints: <list type="bullet"> <item> <description>A GET endpoint at
/// <c>/work-information/enterprise</c> for retrieving enterprise opportunities.</description> </item> <item>
/// <description>A POST endpoint at <c>/careers/{careerInternalId}/work-information/enterprise</c> for creating
/// enterprise opportunities.</description> </item> </list> Each endpoint is tagged with "Enterprises" and includes
/// OpenAPI metadata.</remarks>
public static class EnterpriseEndpoints
{
    /// <summary>
    /// Configures enterprise-related endpoints for the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <remarks>This method maps the following endpoints: <list type="bullet"> <item> <description> A GET
    /// endpoint at <c>/work-information/enterprise</c> for retrieving enterprise opportunities. </description> </item>
    /// <item> <description> A POST endpoint at <c>/careers/{careerInternalId}/work-information/enterprise</c> for
    /// creating enterprise opportunities. </description> </item> </list> Each endpoint is tagged with "Enterprises" and
    /// includes OpenAPI metadata.</remarks>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> to which the enterprise endpoints will be added.</param>
    /// <returns>The <see cref="IEndpointRouteBuilder"/> with the enterprise endpoints configured.</returns>
    public static IEndpointRouteBuilder MapEnterpriseEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/work-information/enterprise", GetEnterpriseHandler.HandleAsync)
            .WithName("GetEnterprises")
            .WithTags("Enterprises")
            .WithOpenApi();

        builder.MapPost("/careers/{careerInternalId}/work-information/enterprise", PostEnterpriseHandler.HandleAsync)
            .WithName("PostEnterprise")
            .WithTags("Enterprises")
            .WithOpenApi();

        return builder;
    }
}
