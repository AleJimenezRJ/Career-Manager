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
/// Handles the retrieval of a single work information entity by its internal career ID.
/// </summary>
/// <remarks>This method processes a request to fetch a single work information entity, such as an industry, 
/// opportunity, or work life, based on the provided internal career ID. The result is returned in a  standardized
/// response format, which may include success, not found, or bad request outcomes.</remarks>
public static class GetSingleWorkInformationByIdHandler
{
    /// <summary>
    /// Handles a request to retrieve detailed work information for a specific career ID.
    /// </summary>
    /// <param name="workInformationService">The service used to retrieve work information data.</param>
    /// <param name="internalWorkInformationId">The internal information ID used to identify the work information to retrieve.</param>
    /// <returns>A result containing one of the following: <list type="bullet"> <item> <description><see cref="Ok{T}"/> with a
    /// <see cref="GetSingleWorkInformationResponse"/> if the work information is successfully retrieved.</description>
    /// </item> <item> <description><see cref="NotFound{T}"/> with an <see cref="Error"/> if no work information is
    /// found for the specified career ID.</description> </item> <item> <description><see cref="BadRequest{T}"/> with a
    /// list of <see cref="Error"/> objects if the request is invalid.</description> </item> </list></returns>
    /// <exception cref="NotImplementedException">Thrown if the work information type is not supported.</exception>
    public static async Task<Results<Ok<GetSingleWorkInformationResponse>, NotFound<Error>, BadRequest<List<Error>>>> HandleAsync(
        [FromServices] IWorkInformationServices workInformationService,
        [FromRoute] int internalWorkInformationId)
    {

        var information = await workInformationService.ListSingleWorkInformationtAsync(internalWorkInformationId);

        ListWorkInformationDto dto = information switch
        {
            Industry industry => IndustryDtoMapper.ToListDto(industry),
            Opportunity opportunity => OpportunityDtoMapper.ToListDto(opportunity),
            WorkLife workLife => WorkLifeDtoMapper.ToListDto(workLife),
            Recruitment recruitment => RecruitmentDtoMapper.ToListDto(recruitment),
            Enterprise enterprise => EnterpriseDtoMapper.ToListDto(enterprise),
            _ => throw new NotImplementedException()
        };

        if (dto is null)
        {
            var notFoundError = DomainErrors.FoundErrors.NotFound();
            return TypedResults.NotFound(notFoundError);
        }


        var response = new GetSingleWorkInformationResponse(dto);
        return TypedResults.Ok(response);
    }
}
