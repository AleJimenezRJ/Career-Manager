using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the retrieval of all work information and returns it in a structured response format.
/// </summary>
/// <remarks>This method retrieves all available work information, maps it to DTOs, and wraps the result in a
/// response object.  The response is returned as a typed result, allowing the caller to handle success, not found, or
/// bad request scenarios.</remarks>
public static class GetWorkInformationHandler
{
    /// <summary>
    /// Handles the request to retrieve all work information and returns the result in a structured response.
    /// </summary>
    /// <remarks>This method processes various types of work information, including industries, opportunities,
    /// and work-life data, and maps them to their respective DTO representations. The response is encapsulated in a
    /// <see cref="GetWorkInformationResponse"/> object.</remarks>
    /// <param name="workInformationService">The service used to retrieve work information data. This parameter is provided via dependency injection.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains one of the following: <list
    /// type="bullet"> <item> <description><see cref="Results{T1, T2, T3}"/> with <see cref="Ok{T}"/> containing a <see
    /// cref="GetWorkInformationResponse"/> if the operation succeeds.</description> </item> <item> <description><see
    /// cref="NotFound{T}"/> with an <see cref="Error"/> if no work information is found.</description> </item> <item>
    /// <description><see cref="BadRequest{T}"/> with a list of <see cref="Error"/> objects if the request is
    /// invalid.</description> </item> </list></returns>
    /// <exception cref="NotImplementedException">Thrown if an unsupported type of work information is encountered during processing.</exception>
    public static async Task<Results<Ok<GetWorkInformationResponse>, NotFound<Error>, BadRequest<List<Error>>>> HandleAsync(
        [FromServices] IWorkInformationServices workInformationService)
    {
        var informations = await workInformationService.ListAllAsync();

        var dtoList = informations
            .Select<WorkInformation, ListWorkInformationDto>(x =>
            {
                return x switch
                {
                    Industry industry => IndustryDtoMapper.ToListDto(industry),
                    Opportunity opportunity => OpportunityDtoMapper.ToListDto(opportunity),
                    WorkLife workLife => WorkLifeDtoMapper.ToListDto(workLife),
                    Recruitment recruitment => RecruitmentDtoMapper.ToListDto(recruitment),
                    Enterprise enterprise => EnterpriseDtoMapper.ToListDto(enterprise),
                    _ => throw new NotImplementedException()
                };
            })
            .ToList();

        var response = new GetWorkInformationResponse(dtoList);
        return TypedResults.Ok(response);
    }
}
