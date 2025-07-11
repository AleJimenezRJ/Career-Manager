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
/// Handles the retrieval of work information by a specified internal career ID.
/// </summary>
/// <remarks>This method processes work information by mapping domain entities to their corresponding DTOs. If no
/// information is found for the given internal career ID, a "Not Found" result is returned. The method uses dependency
/// injection to resolve the required service.</remarks>
public static class GetWorkInformationByIdHandler
{
    /// <summary>
    /// Handles the retrieval of work information associated with a specific internal career ID.
    /// </summary>
    /// <remarks>
    /// This method queries all work information entities linked to the provided career ID and maps each known type 
    /// (<see cref="Industry"/>, <see cref="Opportunity"/>, <see cref="WorkLife"/>) to its corresponding DTO. 
    /// If the career ID does not exist or is not associated with any supported work information types, a "Not Found" 
    /// response is returned using the domain-specific error pattern.
    /// 
    /// This handler safely manages unexpected or unrecognized work information types by returning a structured 
    /// not-found error instead of throwing a runtime exception. Dependency injection is used to access the required service.
    /// </remarks>
    /// <param name="workInformationService">The injected service responsible for retrieving work information entities.</param>
    /// <param name="careerInternalId">The internal career ID used to filter work information records.</param>
    /// <returns>
    /// A result indicating one of the following outcomes:
    /// <list type="bullet">
    /// <item>
    /// <description><see cref="Ok{T}"/> with a <see cref="GetWorkInformationByIdResponse"/> if at least one known work information type is found and successfully mapped.</description>
    /// </item>
    /// <item>
    /// <description><see cref="NotFound{T}"/> with an <see cref="Error"/> if the ID does not exist or all entries are of unsupported types.</description>
    /// </item>
    /// <item>
    /// <description><see cref="BadRequest{T}"/> with a list of <see cref="Error"/> objects if input validation fails (reserved for future request validations).</description>
    /// </item>
    /// </list>
    /// </returns>
    public static async Task<Results<Ok<GetWorkInformationByIdResponse>, NotFound<Error>, BadRequest<List<Error>>>> HandleAsync(
    [FromServices] IWorkInformationServices workInformationService,
    [FromRoute] int careerInternalId)
    {
        var informations = await workInformationService.ListSpecificInformationAsync(careerInternalId);

        if (informations is null || !informations.Any())
        {
            var notFoundError = DomainErrors.FoundErrors.NotFound();
            return TypedResults.NotFound(notFoundError);
        }

        var dtoList = new List<ListWorkInformationDto>();

        foreach (var info in informations)
        {
            switch (info)
            {
                case Industry industry:
                    dtoList.Add(IndustryDtoMapper.ToListDto(industry));
                    break;

                case Opportunity opportunity:
                    dtoList.Add(OpportunityDtoMapper.ToListDto(opportunity));
                    break;

                case WorkLife workLife:
                    dtoList.Add(WorkLifeDtoMapper.ToListDto(workLife));
                    break;

                case Recruitment recruitment:
                    dtoList.Add(RecruitmentDtoMapper.ToListDto(recruitment));
                    break;

                case Enterprise enterprise:
                    dtoList.Add(EnterpriseDtoMapper.ToListDto(enterprise));
                    break;

                default:
                    var notFoundError = DomainErrors.FoundErrors.NotFound();
                    return TypedResults.NotFound(notFoundError);
            }
        }

        if (!dtoList.Any())
        {
            var notFoundError = DomainErrors.FoundErrors.NotFound();
            return TypedResults.NotFound(notFoundError);
        }

        var response = new GetWorkInformationByIdResponse(dtoList);
        return TypedResults.Ok(response);
    }
}
