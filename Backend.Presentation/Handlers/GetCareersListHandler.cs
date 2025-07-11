using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the retrieval of a list of careers.
/// </summary>
/// <remarks>This method queries the career repository to retrieve a list of careers and maps them to a response
/// DTO.  If no careers are found, a "Not Found" result is returned. If the operation is successful, an "OK" result 
/// containing the list of careers is returned.</remarks>
public static class GetCareersListHandler
{
    /// <summary>
    /// Handles the retrieval of a list of careers and returns the result as an HTTP response.
    /// </summary>
    /// <param name="careerRepository">The repository used to access career data. This parameter is provided via dependency injection.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains an HTTP 200 OK response  with a <see
    /// cref="GetCareerListResponse"/> if careers are found, or an HTTP 404 Not Found response  with an <see
    /// cref="Error"/> if no careers are available.</returns>
    public static async Task<Results<Ok<GetCareerListResponse>, NotFound<Error>>> HandleAsync(
        [FromServices] ICareerRepository careerRepository)
    {
        var careers = await careerRepository.ListCareersAsync();
        var listCareerDto = careers
            .Select(CareerDtoMapper.ToListDto)
            .ToList();
        if (!listCareerDto.Any())
        {
            var notFoundError = DomainErrors.FoundErrors.NotFound();
            return TypedResults.NotFound(notFoundError);
        }

        var response = new GetCareerListResponse(listCareerDto);
        return TypedResults.Ok(response);
    }
}
