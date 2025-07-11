using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the retrieval of opportunities and returns the result as an HTTP response.
/// </summary>
/// <remarks>This method retrieves a list of opportunities using the provided <see cref="IOpportunityServices"/> 
/// and maps them to a DTO format. If no opportunities are found, it returns a 404 Not Found response  with an
/// appropriate error. Otherwise, it returns a 200 OK response containing the list of opportunities.</remarks>
public static class GetOpportunityHandler
{
    /// <summary>
    /// Handles the retrieval of opportunities and returns the result as an HTTP response.
    /// </summary>
    /// <remarks>This method queries the opportunity service for a list of opportunities. If no opportunities
    /// are found,  it returns a 404 Not Found response with an appropriate error. Otherwise, it maps the opportunities
    /// to a  DTO and returns them in a 200 OK response.</remarks>
    /// <param name="opportunityService">The service used to retrieve the list of opportunities. This parameter is resolved from the application's
    /// services.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains an HTTP 200 OK response with a  <see
    /// cref="GetOpportunityResponse"/> if opportunities are found, or an HTTP 404 Not Found response with an  <see
    /// cref="Error"/> if no opportunities are available.</returns>
    public static async Task<Results<Ok<GetOpportunityResponse>, NotFound<Error>>> HandleAsync(
        [FromServices] IOpportunityServices opportunityService)
    {
        var opportunities = await opportunityService.ListOpportunitiesAsync();
        var listOpportunitiesDto = opportunities
            .Select(OpportunityDtoMapper.ToListDto)
            .ToList();

        if (!listOpportunitiesDto.Any())
        {
            var notFoundError = DomainErrors.FoundErrors.NotFound();
            return TypedResults.NotFound(notFoundError);
        }

        var response = new GetOpportunityResponse(listOpportunitiesDto);
        return TypedResults.Ok(response);
    }
}
