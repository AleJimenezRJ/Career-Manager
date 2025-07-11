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
public static class CareerEndpoints
{
    public static IEndpointRouteBuilder MapCareerEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/add-career", PostCareerHandler.HandleAsync)
            .WithName("AddCareer")
            .WithTags("Careers")
            .WithOpenApi();

        builder.MapGet("/search-career-by-name/{careerName}", GetCareerByNameHandler.HandleAsync)
            .WithName("GetCareerByName")
            .WithTags("Careers")
            .WithOpenApi();

        builder.MapGet("/list-careers", GetCareersListHandler.HandleAsync)
            .WithName("GetCareersList")
            .WithTags("Careers")
            .WithOpenApi();

        builder.MapGet("/calculate-scholarship/{careerName}", GetScholarshipHandler.HandleAsync)
            .WithName("CalculateScholarship")
            .WithTags("Careers")
            .WithOpenApi();

        builder.MapGet("/search-keyword/{keyword}", GetKeywordHandler.HandleAsync)
            .WithName("SearchKeyword")
            .WithTags("Careers")
            .WithOpenApi();

        return builder;
    }
}
