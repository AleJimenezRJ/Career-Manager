using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Endpoints;

/// <summary>
/// Provides extension methods for mapping career-related API endpoints to an <see cref="IEndpointRouteBuilder"/>.
/// </summary>
/// <remarks>This class defines endpoints for managing career-related operations, such as adding a career or
/// searching for a career by name. The endpoints are mapped to the provided <see cref="IEndpointRouteBuilder"/>
/// instance and are configured with appropriate metadata, including route names, tags, and OpenAPI
/// documentation.</remarks>
public static class LanguageEndpoints
{
    public static IEndpointRouteBuilder MapLanguageEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/add-language/{internalWorkInformationId}", PostLanguageHandler.HandleAsync)
            .WithName("AddLanguage")
            .WithTags("Recruitments")
            .WithOpenApi();

        builder.MapGet("/list-languages", GetLanguageHandler.HandleAsync)
            .WithName("GetLanguages")
            .WithTags("Recruitments")
            .WithOpenApi();

        return builder;
    }
}