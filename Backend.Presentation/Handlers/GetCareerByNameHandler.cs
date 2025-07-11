using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;

/// <summary>
/// Handles the retrieval of a career by its name.
/// </summary>
/// <remarks>This method processes a request to retrieve a specific career by its name.  If the career is found,
/// it returns the career details in the response.  If the career does not exist, a conflict result is returned with an
/// appropriate error message.</remarks>
public static class GetCareerByNameHandler
{
    /// <summary>
    /// Handles a request to retrieve a specific career by its name.
    /// </summary>
    /// <param name="careerRepository">The repository used to access career data.</param>
    /// <param name="careerName">The name of the career to retrieve. Cannot be null or empty.</param>
    /// <returns>A result containing one of the following: <list type="bullet"> <item><description><see cref="Ok{T}"/> with a
    /// <see cref="GetCareerResponse"/> if the career is found.</description></item> <item><description><see
    /// cref="NotFound{T}"/> with an <see cref="Error"/> if the career is not found.</description></item>
    /// <item><description><see cref="BadRequest{T}"/> with a list of <see cref="Error"/> objects if the request is
    /// invalid.</description></item> </list></returns>
    public static async Task<Results<Ok<GetCareerResponse>, NotFound<Error>, BadRequest<List<Error>>>> HandleAsync(
        [FromServices] ICareerRepository careerRepository,
        [FromRoute] string careerName)
    {
        await careerRepository.CalculateScholarshipAsync(careerName);
        var careerEntity = await careerRepository.ListSpecificCareerAsync(careerName);
        if (careerEntity is null)
        {
            var conflictError = DomainErrors.FoundErrors.NotFound(careerName);
            return TypedResults.NotFound(conflictError);
        }
        var careerDto = CareerDtoMapper.ToListDto(careerEntity);
        var response = new GetCareerResponse(careerDto);
        return TypedResults.Ok(response);
    }
}
