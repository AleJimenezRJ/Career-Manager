using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the retrieval of work-life data and returns the appropriate result based on the availability of data.
/// </summary>
/// <remarks>This method queries the work-life service for a list of work-life data. If no data is found, it
/// returns a "Not Found" result. Otherwise, it maps the data to a response DTO and returns it as an "Ok"
/// result.</remarks>
public static class GetWorkLifeHandler
{
    /// <summary>
    /// Handles the request to retrieve a list of work-life data and returns the appropriate result.
    /// </summary>
    /// <remarks>This method queries the work-life service for data, maps the results to a DTO, and returns a
    /// response based on whether any data is available. If no data is found, a "Not Found" result is returned with an
    /// appropriate error message.</remarks>
    /// <param name="workLifeService">The service used to retrieve work-life data. This parameter is required and must not be null.</param>
    /// <returns>A <see cref="Results{T1, T2}"/> containing either: <list type="bullet"> <item> <description> <see cref="Ok{T}"/>
    /// with a <see cref="GetWorkLifeResponse"/> if work-life data is found. </description> </item> <item> <description>
    /// <see cref="NotFound{T}"/> with an <see cref="Error"/> if no work-life data is found. </description> </item>
    /// </list></returns>
    public static async Task<Results<Ok<GetWorkLifeResponse>, NotFound<Error>>> HandleAsync(
        [FromServices] IWorkLifeServices workLifeService)
    {
        var workLives = await workLifeService.ListWorkLifeAsync();
        var listDto = workLives
            .Select(WorkLifeDtoMapper.ToListDto)
            .ToList();

        if (!listDto.Any())
        {
            var notFoundError = DomainErrors.FoundErrors.NotFound();
            return TypedResults.NotFound(notFoundError);
        }

        var response = new GetWorkLifeResponse(listDto);
        return TypedResults.Ok(response);
    }
}
