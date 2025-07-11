using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the retrieval of industry data and returns a response containing the list of industries.
/// </summary>
/// <remarks>This method retrieves a list of industries from the provided <paramref name="industryService"/>.  If
/// no industries are found, a "Not Found" result is returned. Otherwise, the list of industries is  mapped to a DTO and
/// returned in the response.</remarks>
public static class GetIndustryHandler
{
    /// <summary>
    /// Handles the request to retrieve a list of industries.
    /// </summary>
    /// <remarks>This method queries the industry service for a list of industries. If no industries are
    /// found,  it returns a "Not Found" result with an appropriate error. Otherwise, it returns an "Ok" result 
    /// containing the list of industries.</remarks>
    /// <param name="industryService">The service used to retrieve industry data. This parameter is provided via dependency injection.</param>
    /// <returns>A <see cref="Results{T1, T2}"/> containing either: <list type="bullet"> <item> <description> An <see
    /// cref="Ok{T}"/> result with a <see cref="GetIndustryResponse"/> if industries are found. </description> </item>
    /// <item> <description> A <see cref="NotFound{T}"/> result with an <see cref="Error"/> if no industries are found.
    /// </description> </item> </list></returns>
    public static async Task<Results<Ok<GetIndustryResponse>, NotFound<Error>>> HandleAsync(
        [FromServices] IIndustryServices industryService)
    {
        var industries = await industryService.ListIndustriesAsync();
        var listIndustryDto = industries
            .Select(IndustryDtoMapper.ToListDto)
            .ToList();

        if (!listIndustryDto.Any())
        {
            var notFoundError = DomainErrors.FoundErrors.NotFound();
            return TypedResults.NotFound(notFoundError);
        }

        var response = new GetIndustryResponse(listIndustryDto);
        return TypedResults.Ok(response);
    }
}
