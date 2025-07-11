using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the retrieval of recruitment data and returns the appropriate result based on the availability of data.
/// </summary>
/// <remarks>This method retrieves a list of recruitments from the provided <paramref name="recruitmentService"/>.
/// If no recruitments are found, it returns a "Not Found" result with an appropriate error message.  Otherwise, it maps
/// the recruitments to a DTO and returns them in the response.</remarks>
public static class GetRecruitmentHandler
{
    /// <summary>
    /// Handles the retrieval of recruitment data and returns the result as an HTTP response.
    /// </summary>
    /// <remarks>This method queries the recruitment service for a list of recruitments. If no recruitments
    /// are found, it returns a 404 Not Found response with an appropriate error. Otherwise, it returns a 200 OK
    /// response with the recruitment data.</remarks>
    /// <param name="recruitmentService">The recruitment service used to fetch recruitment data. This parameter is resolved from the service container.</param>
    /// <returns>A <see cref="Results{T1, T2}"/> containing either: <list type="bullet"> <item> <description> <see cref="Ok{T}"/>
    /// with a <see cref="GetRecruitmentResponse"/> if recruitment data is found. </description> </item> <item>
    /// <description> <see cref="NotFound{T}"/> with an <see cref="Error"/> if no recruitment data is available.
    /// </description> </item> </list></returns>
    public static async Task<Results<Ok<GetRecruitmentResponse>, NotFound<Error>>> HandleAsync(
        [FromServices] IRecruitmentServices recruitmentService)
    {
        var recruitments = await recruitmentService.ListRecruitmentsAsync();
        var listRecruitmentDto = recruitments
            .Select(RecruitmentDtoMapper.ToListDto)
            .ToList();

        if (!listRecruitmentDto.Any())
        {
            var notFoundError = DomainErrors.FoundErrors.NotFound();
            return TypedResults.NotFound(notFoundError);
        }

        var response = new GetRecruitmentResponse(listRecruitmentDto);
        return TypedResults.Ok(response);
    }
}
