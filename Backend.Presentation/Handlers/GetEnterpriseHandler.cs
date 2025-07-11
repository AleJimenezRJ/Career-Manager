using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the retrieval of a list of enterprises asynchronously.
/// </summary>
/// <remarks>This method queries the provided enterprise service to retrieve a list of enterprises. If no
/// enterprises are found, it returns a "Not Found" result with an appropriate error. Otherwise, it returns an "Ok"
/// result containing the list of enterprises.</remarks>
public static class GetEnterpriseHandler
{
    /// <summary>
    /// Handles the retrieval of a list of enterprises and returns the result as an HTTP response.
    /// </summary>
    /// <remarks>This method queries the enterprise service for a list of enterprises, maps the results to
    /// DTOs, and returns an appropriate HTTP response. If no enterprises are found, a "Not Found" error is
    /// returned.</remarks>
    /// <param name="enterpriseService">The service used to retrieve enterprise data. This parameter is provided via dependency injection.</param>
    /// <returns>A <see cref="Results{T1, T2}"/> containing either: <list type="bullet"> <item> <description>An <see
    /// cref="Ok{T}"/> result with a <see cref="GetEnterpriseListResponse"/> if enterprises are found.</description>
    /// </item> <item> <description>A <see cref="NotFound{T}"/> result with an <see cref="Error"/> if no enterprises are
    /// found.</description> </item> </list></returns>
    public static async Task<Results<Ok<GetEnterpriseListResponse>, NotFound<Error>>> HandleAsync(
        [FromServices] IEnterpriseServices enterpriseService)
    {
        var enterprises = await enterpriseService.ListEnterpriseAsync();
        var listEnterpriseDto = enterprises
            .Select(EnterpriseDtoMapper.ToListDto)
            .ToList();

        if (!listEnterpriseDto.Any())
        {
            var notFoundError = DomainErrors.FoundErrors.NotFound();
            return TypedResults.NotFound(notFoundError);
        }

        var response = new GetEnterpriseListResponse(listEnterpriseDto);
        return TypedResults.Ok(response);
    }
}
